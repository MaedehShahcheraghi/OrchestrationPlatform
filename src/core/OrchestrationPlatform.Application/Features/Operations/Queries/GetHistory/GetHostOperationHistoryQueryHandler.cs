using MediatR;
using OrchestrationPlatform.Application.Abstractions.Models.Base;
using OrchestrationPlatform.Application.Abstractions.Persistence.Common;
using OrchestrationPlatform.Application.Features.Operations.Queries.DTOs;
using OrchestrationPlatform.Domain.Entities;
using OrchestrationPlatform.Domain.Enums;

namespace OrchestrationPlatform.Application.Features.Operations.Queries.GetHistory;

internal sealed class GetHostOperationHistoryQueryHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<GetHostOperationHistoryQuery, PagedResult<OperationHistoryDto>>
{
    public async Task<PagedResult<OperationHistoryDto>> Handle(GetHostOperationHistoryQuery request,
        CancellationToken cancellationToken)
    {
        var operationRepo = unitOfWork.GetReadRepository<OrchestrationOperation>();

        var pagedEntities = await operationRepo.PageAsync(
            request.PageNumber,
            request.PageSize,
            x => x.OperatingSystemHostId == request.HostId && (x.OperationType == OrchestrationOperationType.Install ||
                                                               x.OperationType == OrchestrationOperationType.Uninstall),
            q => q.OrderByDescending(x => x.RequestedAtUtc),
            cancellationToken: cancellationToken);

        var dtos = pagedEntities.Items.Select(x => new OperationHistoryDto(
            x.Id,
            x.OperationType,
            x.Status,
            x.PackageNameSnapshot,
            x.VersionSnapshot,
            x.RequestedAtUtc)).ToList();

        return new PagedResult<OperationHistoryDto>(
            dtos,
            pagedEntities.TotalCount,
            pagedEntities.PageNumber,
            pagedEntities.PageSize);
    }
}