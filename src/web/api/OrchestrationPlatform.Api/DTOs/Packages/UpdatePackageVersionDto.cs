using OrchestrationPlatform.Domain.Enums;

namespace OrchestrationPlatform.Api.DTOs.Packages;

public class UpdatePackageVersionDto
{
    public string Version { get; set; } = null!;
    public PackageType PackageType { get; set; }
    public OperatingSystemFamily OperatingSystemFamily { get; set; }
    public string OperatingSystemVersion { get; set; } = null!;
    public CpuArchitecture Architecture { get; set; }

    public IFormFile? File { get; set; }
}