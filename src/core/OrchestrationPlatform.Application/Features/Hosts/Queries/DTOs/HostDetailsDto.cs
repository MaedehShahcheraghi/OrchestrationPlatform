using OrchestrationPlatform.Domain.Enums;

namespace OrchestrationPlatform.Application.Features.Hosts.Queries.DTOs;

public record HostDetailsDto(
    Guid Id,
    string Name,
    string IpAddress,
    int SshPort,
    string Username,
    OperatingSystemFamily OperatingSystemFamily,
    string OperatingSystemVersion,
    CpuArchitecture Architecture,
    string? Description,
    bool IsActive,
    DateTime CreatedAt);