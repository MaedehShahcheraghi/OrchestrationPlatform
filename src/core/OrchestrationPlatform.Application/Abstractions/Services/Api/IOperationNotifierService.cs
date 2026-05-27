namespace OrchestrationPlatform.Application.Abstractions.Services.Api;

public interface IOperationNotifierService
{
    Task NotifyProgressAsync(
        Guid operationId,
        string status,
        int progressPercent,
        string message,
        CancellationToken cancellationToken = default);
}