using MediatR;
using OrchestrationPlatform.Application.Abstractions.Persistence.Common;
using OrchestrationPlatform.Domain.Entities;

namespace OrchestrationPlatform.Application.Features.Operations.Queries.GetHistory;

internal sealed class GetHostOperationHistoryQueryHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<GetHostOperationHistoryQuery, IReadOnlyList<OperationHistoryDto>>
{
    public async Task<IReadOnlyList<OperationHistoryDto>> Handle(GetHostOperationHistoryQuery request,
        CancellationToken cancellationToken)
    {
        var operationRepo = unitOfWork.GetReadRepository<InstallOperation>();

        var history = await operationRepo.ListProjectedAsync(
            x => new OperationHistoryDto(
                x.Id,
                x.Status.ToString(),
                x.ProgressPercent,
                x.RequestedAtUtc,
                x.FinishedAtUtc,
                x.ErrorMessage),
            x => x.OperatingSystemHostId == request.HostId && x.SoftwarePackageVersionId == request.PackageVersionId,
            q => q.OrderByDescending(x => x.RequestedAtUtc),
            cancellationToken: cancellationToken);

        return history;
    }
}