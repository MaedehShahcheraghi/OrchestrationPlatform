namespace OrchestrationPlatform.Application.Abstractions.Services.External;

public interface IOrchestrationService
{
    Task<string> TriggerInstallWorkflowAsync(
        Guid operationId,
        string hostIp,
        string sshUsername,
        string downloadUrl,
        CancellationToken cancellationToken = default);

    Task CancelWorkflowAsync(string externalWorkflowId, CancellationToken cancellationToken = default);
}