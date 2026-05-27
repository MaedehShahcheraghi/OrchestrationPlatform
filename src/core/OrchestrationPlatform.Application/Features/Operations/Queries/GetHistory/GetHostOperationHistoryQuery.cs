using MediatR;

namespace OrchestrationPlatform.Application.Features.Operations.Queries.GetHistory;

public record GetHostOperationHistoryQuery(Guid HostId, Guid PackageVersionId)
    : IRequest<IReadOnlyList<OperationHistoryDto>>;

public record OperationHistoryDto(
    Guid OperationId,
    string Status,
    int ProgressPercent,
    DateTime RequestedAtUtc,
    DateTime? FinishedAtUtc,
    string? ErrorMessage);