using MediatR;
using OrchestrationPlatform.Domain.Enums;

namespace OrchestrationPlatform.Application.Features.Operations.Queries.GetHistory;

public record GetHostOperationHistoryQuery(Guid HostId, Guid PackageVersionId)
    : IRequest<IReadOnlyList<OperationHistoryDto>>;

public record OperationHistoryDto(
    Guid OperationId,
    InstallOperationType operationType,
    string Status,
    int ProgressPercent,
    DateTime RequestedAtUtc,
    DateTime? FinishedAtUtc,
    string? ErrorMessage);