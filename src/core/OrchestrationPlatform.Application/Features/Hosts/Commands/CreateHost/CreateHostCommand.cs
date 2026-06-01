using MediatR;
using OrchestrationPlatform.Domain.Enums;

namespace OrchestrationPlatform.Application.Features.Hosts.Commands.CreateHost;

public sealed record CreateHostCommand(
    string Name,
    string IpAddress,
    int SshPort,
    string Username,
    OperatingSystemFamily OperatingSystemFamily,
    string OperatingSystemVersion,
    CpuArchitecture Architecture
) : IRequest<Guid>;