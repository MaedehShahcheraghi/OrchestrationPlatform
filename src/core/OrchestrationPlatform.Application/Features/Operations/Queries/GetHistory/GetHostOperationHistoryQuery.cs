using MediatR;
using OrchestrationPlatform.Application.Abstractions.Models.Base;
using OrchestrationPlatform.Application.Features.Operations.Queries.DTOs;

namespace OrchestrationPlatform.Application.Features.Operations.Queries.GetHistory;

public sealed record GetHostOperationHistoryQuery(
    Guid HostId,
    int PageNumber = 1,
    int PageSize = 10)
    : IRequest<PagedResult<OperationHistoryDto>>;