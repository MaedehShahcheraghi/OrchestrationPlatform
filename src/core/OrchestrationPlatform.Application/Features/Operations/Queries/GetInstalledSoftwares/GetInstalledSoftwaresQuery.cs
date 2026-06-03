using MediatR;
using OrchestrationPlatform.Application.Features.Operations.Queries.DTOs;

namespace OrchestrationPlatform.Application.Features.Operations.Queries.GetInstalledSoftwares;

public sealed record GetInstalledSoftwaresQuery(Guid HostId) : IRequest<IReadOnlyList<InstalledSoftwareDto>>;