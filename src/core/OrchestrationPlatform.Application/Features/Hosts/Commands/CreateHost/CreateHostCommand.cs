using MediatR;
using OrchestrationPlatform.Domain.Enums;

// فرض بر این است که Enum ها اینجا هستند

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