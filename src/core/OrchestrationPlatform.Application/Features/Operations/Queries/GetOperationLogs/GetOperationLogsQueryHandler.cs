using MediatR;
using OrchestrationPlatform.Application.Abstractions.Persistence.Common;
using OrchestrationPlatform.Application.Features.Operations.Queries.DTOs;
using OrchestrationPlatform.Domain.Entities;

namespace OrchestrationPlatform.Application.Features.Operations.Queries.GetOperationLogs;

internal sealed class GetOperationLogsQueryHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<GetOperationLogsQuery, IReadOnlyList<OperationLogDto>>
{
    public async Task<IReadOnlyList<OperationLogDto>> Handle(GetOperationLogsQuery request,
        CancellationToken cancellationToken)
    {
        var repo = unitOfWork.GetReadRepository<OperationLog>();
        var logs = await repo.ListProjectedAsync(
            x => new OperationLogDto(x.Id, x.Level, x.Message, x.Details, x.LoggedAtUtc),
            x => x.OrchestrationOperationId == request.OperationId, cancellationToken: cancellationToken);

        return logs;
    }
}