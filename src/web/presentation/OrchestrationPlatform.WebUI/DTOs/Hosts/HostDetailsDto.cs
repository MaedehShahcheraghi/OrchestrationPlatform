using OrchestrationPlatform.WebUI.Enums;

namespace OrchestrationPlatform.WebUI.DTOs.Hosts;

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