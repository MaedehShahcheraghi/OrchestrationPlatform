using System.Net.Http.Json;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using OrchestrationPlatform.Application.Abstractions.Models.ServiceModels;
using OrchestrationPlatform.Application.Abstractions.Services.External;

namespace OrchestrationPlatform.Infrastructure.External.Services;

public sealed class N8NOrchestrationService(
    HttpClient httpClient,
    ILogger<N8NOrchestrationService> logger,
    IConfiguration configuration)
    : IOrchestrationService
{
    private readonly string _baseUrl = configuration["PlatformSettings:BaseUrl"] ?? "http://localhost:5232";

    private readonly string _n8NWebhookBaseUrl = configuration["Orchestration:N8nWebhookBaseUrl"]
                                                 ?? throw new ArgumentNullException(
                                                     "N8nWebhookBaseUrl is not configured.");

    private string WebhookUrl => $"{_n8NWebhookBaseUrl}/trigger-ansible-operation";

    public async Task<string> TriggerInstallWorkflowAsync(
        List<BulkTargetModel> targets,
        string downloadUrl,
        CancellationToken cancellationToken = default)
    {
        var payload = new
        {
            OperationType = "install",
            PackageDownloadUrl = downloadUrl,
            PackageName = (string?)null,
            Timestamp = DateTime.UtcNow,
            Targets = FormatTargets(targets)
        };

        return await SendWebhookRequestAsync(payload, cancellationToken);
    }

    public async Task<string> TriggerUninstallWorkflowAsync(
        List<BulkTargetModel> targets,
        string packageName,
        CancellationToken cancellationToken = default)
    {
        var payload = new
        {
            OperationType = "uninstall",
            PackageDownloadUrl = (string?)null,
            PackageName = packageName,
            Timestamp = DateTime.UtcNow,
            Targets = FormatTargets(targets)
        };

        return await SendWebhookRequestAsync(payload, cancellationToken);
    }

    public async Task CancelWorkflowAsync(string externalWorkflowId, CancellationToken cancellationToken = default)
    {
        var cancelUrl = $"{_n8NWebhookBaseUrl}/cancel-execution/{externalWorkflowId}";

        try
        {
            logger.LogInformation("Attempting to cancel workflow execution: {ExternalWorkflowId}", externalWorkflowId);
            var response = await httpClient.PostAsync(cancelUrl, null, cancellationToken);

            if (!response.IsSuccessStatusCode)
                logger.LogWarning("Cancel workflow returned non-success status code: {StatusCode}",
                    response.StatusCode);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error occurred while trying to cancel workflow {ExternalWorkflowId}",
                externalWorkflowId);
        }
    }

    private object FormatTargets(List<BulkTargetModel> targets)
    {
        return targets.Select(t => new
        {
            Host = t.HostIp,
            User = t.SshUsername,
            CallbackUrl = $"{_baseUrl.TrimEnd('/')}/api/operations/{t.OperationId}/callback"
        }).ToList();
    }

    private async Task<string> SendWebhookRequestAsync(object payload, CancellationToken cancellationToken)
    {
        try
        {
            var response = await httpClient.PostAsJsonAsync(WebhookUrl, payload, cancellationToken);
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<N8nWebhookResponse>(cancellationToken);
            return result?.ExecutionId ?? Guid.NewGuid().ToString();
        }
        catch (HttpRequestException ex)
        {
            logger.LogError(ex, "Failed to communicate with n8n Webhook at {Url}", WebhookUrl);
            throw new ApplicationException("Failed to trigger orchestration workflow.", ex);
        }
    }
}

public record N8nWebhookResponse(string ExecutionId, string Message);