using MediatR;
using OrchestrationPlatform.Domain.Enums;

namespace OrchestrationPlatform.Application.Features.Hosts.Queries.GetAllHostsQuery;

public sealed record GetAllHostsQuery : IRequest<List<HostResponse>>;

public sealed record HostResponse(
    Guid Id,
    string Name,
    string IpAddress,
    int SshPort,
    string Username,
    OperatingSystemFamily OperatingSystemFamily,
    string OperatingSystemVersion,
    CpuArchitecture Architecture,
    HostStatus Status,
    bool IsActive);