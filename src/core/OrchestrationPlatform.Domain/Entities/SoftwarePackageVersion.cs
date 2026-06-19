using OrchestrationPlatform.Domain.Common;
using OrchestrationPlatform.Domain.Enums;

namespace OrchestrationPlatform.Domain.Entities;

public sealed class SoftwarePackageVersion : AuditableEntity
{
    #region Foreign Keys

    public Guid SoftwarePackageId { get; private set; }

    #endregion

    #region Constructors

    private SoftwarePackageVersion()
    {
    }

    public SoftwarePackageVersion(
        Guid softwarePackageId,
        string version,
        PackageType packageType,
        OperatingSystemFamily operatingSystemFamily,
        string operatingSystemVersion,
        CpuArchitecture architecture)
    {
        SoftwarePackageId = softwarePackageId;
        Version = version;
        PackageType = packageType;
        OperatingSystemFamily = operatingSystemFamily;
        OperatingSystemVersion = operatingSystemVersion;
        Architecture = architecture;
        IsActive = true;
    }

    #endregion

    #region Properties

    public string Version { get; private set; } = null!;

    public PackageType PackageType { get; private set; }

    public OperatingSystemFamily OperatingSystemFamily { get; private set; }

    public string OperatingSystemVersion { get; private set; } = null!;

    public CpuArchitecture Architecture { get; private set; }

    public bool IsActive { get; private set; }

    #endregion

    #region Navigation Properties

    public SoftwarePackage SoftwarePackage { get; private set; } = null!;

    public PackageArtifact? Artifact { get; }

    public ICollection<OrchestrationOperation> OrchestrationOperations { get; private set; } = [];

    public ICollection<InstalledSoftware> InstalledSoftwares { get; private set; } = [];

    #endregion

    #region Behaviors

    public void Update(
        string version,
        PackageType packageType,
        OperatingSystemFamily operatingSystemFamily,
        string operatingSystemVersion,
        CpuArchitecture architecture)
    {
        Version = version;
        PackageType = packageType;
        OperatingSystemFamily = operatingSystemFamily;
        OperatingSystemVersion = operatingSystemVersion;
        Architecture = architecture;
    }

    public void Enable()
    {
        IsActive = true;
    }

    public void Disable()
    {
        IsActive = false;
    }

    #endregion
}