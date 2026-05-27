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

    public async Task<string> TriggerInstallWorkflowAsync(
        List<BulkTargetModel> targets,
        string downloadUrl,
        CancellationToken cancellationToken = default)
    {
        var webhookUrl = $"{_n8NWebhookBaseUrl}/trigger-ansible-install";

        var formattedTargets = targets.Select(t => new
        {
            Host = t.HostIp,
            User = t.SshUsername,
            CallbackUrl = $"{_baseUrl.TrimEnd('/')}/api/operations/{t.OperationId}/callback"
        }).ToList();

        var payload = new
        {
            PackageDownloadUrl = downloadUrl,
            Timestamp = DateTime.UtcNow,
            Targets = formattedTargets // آرایه ماشین‌ها
        };

        var response = await httpClient.PostAsJsonAsync(webhookUrl, payload, cancellationToken);
        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadFromJsonAsync<N8nWebhookResponse>(cancellationToken);
        return result?.ExecutionId ?? Guid.NewGuid().ToString();
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
}

public record N8nWebhookResponse(string ExecutionId, string Message);