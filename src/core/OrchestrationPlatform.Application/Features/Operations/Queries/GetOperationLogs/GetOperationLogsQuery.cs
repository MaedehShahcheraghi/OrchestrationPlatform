using MediatR;
using OrchestrationPlatform.Application.Features.Operations.Queries.DTOs;

namespace OrchestrationPlatform.Application.Features.Operations.Queries.GetOperationLogs;

public sealed record GetOperationLogsQuery(Guid OperationId) : IRequest<IReadOnlyList<OperationLogDto>>;