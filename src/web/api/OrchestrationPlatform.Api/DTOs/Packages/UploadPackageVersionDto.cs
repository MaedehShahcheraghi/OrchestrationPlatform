using OrchestrationPlatform.Domain.Enums;

namespace OrchestrationPlatform.Api.DTOs.Packages;

public record UploadPackageVersionDto
{
    public string Version { get; init; } = null!;
    public PackageType PackageType { get; init; }
    public OperatingSystemFamily OperatingSystemFamily { get; init; }
    public string OperatingSystemVersion { get; init; } = null!;
    public CpuArchitecture Architecture { get; init; }
    public IFormFile File { get; init; } = null!;
}