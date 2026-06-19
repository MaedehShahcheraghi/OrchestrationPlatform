using OrchestrationPlatform.Application.Abstractions.Models.ServiceModels;

namespace OrchestrationPlatform.Application.Abstractions.Services.External;

public interface IOrchestrationService
{
    Task<string> TriggerWorkflowAsync(
        OrchestrationPayload payload,
        CancellationToken cancellationToken = default);

    Task CancelWorkflowAsync(
        string externalWorkflowId,
        CancellationToken cancellationToken = default);
}

public record OrchestrationPayload(
    string OperationType,
    List<BulkTargetModel> Targets,
    string? PackageDownloadUrl = null,
    string? PackageName = null,
    string? ConfigAction = null,
    string? ConfigValue = null
);