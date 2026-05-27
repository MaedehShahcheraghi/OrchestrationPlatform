using OrchestrationPlatform.Application.Abstractions.Models.ServiceModels;

namespace OrchestrationPlatform.Application.Abstractions.Services.External;

public interface IOrchestrationService
{
    public Task<string> TriggerInstallWorkflowAsync(
        List<BulkTargetModel> targets,
        string downloadUrl,
        CancellationToken cancellationToken = default);

    Task CancelWorkflowAsync(string externalWorkflowId, CancellationToken cancellationToken = default);
}