using MediatR;

namespace OrchestrationPlatform.Application.Features.Operations.Commands.UpdateProgress;

public record UpdateOperationProgressCommand(
    Guid OperationId,
    string Status,
    int ProgressPercent,
    string LogLevel,
    string Message) : IRequest;