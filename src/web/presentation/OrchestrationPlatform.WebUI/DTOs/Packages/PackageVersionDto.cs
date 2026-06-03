using OrchestrationPlatform.WebUI.Enums;

namespace OrchestrationPlatform.WebUI.DTOs.Packages;

public record PackageVersionDto(
    Guid Id,
    string Version,
    CpuArchitecture Architecture,
    OperatingSystemFamily OperatingSystemFamily,
    string OperatingSystemVersion,
    DateTime CreatedAt);