using System.Net.Http.Json;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using OrchestrationPlatform.Application.Abstractions.Services.External;

namespace OrchestrationPlatform.Infrastructure.External.Services;

public sealed class N8NOrchestrationService : IOrchestrationService
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<N8NOrchestrationService> _logger;
    private readonly string _n8NWebhookBaseUrl;

    public N8NOrchestrationService(
        HttpClient httpClient,
        ILogger<N8NOrchestrationService> logger,
        IConfiguration configuration)
    {
        _httpClient = httpClient;
        _logger = logger;

        _n8NWebhookBaseUrl = configuration["Orchestration:N8nWebhookBaseUrl"]
                             ?? throw new ArgumentNullException("N8nWebhookBaseUrl is not configured.");
    }

    public async Task<string> TriggerInstallWorkflowAsync(
        Guid operationId,
        string hostIp,
        string sshUsername,
        string downloadUrl,
        CancellationToken cancellationToken = default)
    {
        var webhookUrl = $"{_n8NWebhookBaseUrl}/trigger-ansible-install";

        var payload = new
        {
            OperationId = operationId,
            TargetHost = hostIp,
            SshUser = sshUsername,
            PackageDownloadUrl = downloadUrl,
            Timestamp = DateTime.UtcNow
        };

        try
        {
            _logger.LogInformation("Triggering n8n workflow for OperationId: {OperationId} on Host: {HostIp}",
                operationId, hostIp);

            var response = await _httpClient.PostAsJsonAsync(webhookUrl, payload, cancellationToken);
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<N8nWebhookResponse>(cancellationToken);

            return result?.ExecutionId ?? Guid.NewGuid().ToString();
        }
        catch (HttpRequestException ex)
        {
            _logger.LogError(ex, "Failed to communicate with n8n Webhook. OperationId: {OperationId}", operationId);
            throw new ApplicationException("Failed to trigger orchestration workflow.", ex);
        }
    }

    public async Task CancelWorkflowAsync(string externalWorkflowId, CancellationToken cancellationToken = default)
    {
        var cancelUrl = $"{_n8NWebhookBaseUrl}/cancel-execution/{externalWorkflowId}";

        try
        {
            _logger.LogInformation("Attempting to cancel workflow execution: {ExternalWorkflowId}", externalWorkflowId);
            var response = await _httpClient.PostAsync(cancelUrl, null, cancellationToken);

            if (!response.IsSuccessStatusCode)
                _logger.LogWarning("Cancel workflow returned non-success status code: {StatusCode}",
                    response.StatusCode);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while trying to cancel workflow {ExternalWorkflowId}",
                externalWorkflowId);
        }
    }
}

public record N8nWebhookResponse(string ExecutionId, string Message);