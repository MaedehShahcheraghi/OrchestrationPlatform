using MediatR;
using OrchestrationPlatform.Application.Features.Hosts.Queries.DTOs;

namespace OrchestrationPlatform.Application.Features.Hosts.Queries.GetHost;

public sealed record GetHostByIdQuery(Guid Id) : IRequest<HostDetailsDto>;