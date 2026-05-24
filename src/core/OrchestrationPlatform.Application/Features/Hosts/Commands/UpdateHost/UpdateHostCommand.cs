using MediatR;
using OrchestrationPlatform.Domain.Enums;

namespace OrchestrationPlatform.Application.Features.Hosts.Commands.UpdateHost;

public sealed record UpdateHostCommand(
    Guid Id,
    string Name,
    string IpAddress,
    int SshPort,
    string Username,
    OperatingSystemFamily OperatingSystemFamily,
    string OperatingSystemVersion,
    CpuArchitecture Architecture,
    string? Description) : IRequest;