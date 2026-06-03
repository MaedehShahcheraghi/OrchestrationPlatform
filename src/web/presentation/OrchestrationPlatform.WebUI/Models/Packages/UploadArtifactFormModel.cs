using System.ComponentModel.DataAnnotations;
using OrchestrationPlatform.WebUI.Enums;

namespace OrchestrationPlatform.WebUI.Models.Packages;

public class UploadArtifactFormModel
{
    [Required] public string Version { get; set; } = string.Empty;

    [Required] public CpuArchitecture? Architecture { get; set; }

    [Required] public OperatingSystemFamily? OperatingSystemFamily { get; set; }

    [Required] public string OperatingSystemVersion { get; set; } = string.Empty;

    [Required] public PackageType? PackageType { get; set; }
}