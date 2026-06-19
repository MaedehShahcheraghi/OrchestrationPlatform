using OrchestrationPlatform.Domain.Common;

namespace OrchestrationPlatform.Domain.Entities;

public sealed class InstalledSoftware : AuditableEntity
{
    #region Constructors

    private InstalledSoftware()
    {
    }

    public InstalledSoftware(
        Guid softwarePackageVersionId,
        Guid operatingSystemHostId,
        Guid installOperationId,
        string installedName,
        string installedVersion,
        DateTime installedAtUtc)
    {
        SoftwarePackageVersionId = softwarePackageVersionId;
        OperatingSystemHostId = operatingSystemHostId;
        OrchestrationOperationId = installOperationId;
        InstalledName = installedName;
        InstalledVersion = installedVersion;
        InstalledAtUtc = installedAtUtc;
        IsActive = true;
    }

    #endregion

    #region Foreign Keys

    public Guid SoftwarePackageVersionId { get; private set; }

    public Guid OperatingSystemHostId { get; private set; }

    public Guid OrchestrationOperationId { get; }

    #endregion

    #region Properties

    public string InstalledName { get; private set; } = null!;

    public string InstalledVersion { get; private set; } = null!;

    public DateTime InstalledAtUtc { get; private set; }

    public DateTime? RemovedAtUtc { get; private set; }

    public bool IsActive { get; private set; }

    #endregion

    #region Navigation Properties

    public SoftwarePackageVersion SoftwarePackageVersion { get; private set; } = null!;

    public OperatingSystemHost OperatingSystemHost { get; private set; } = null!;

    public OrchestrationOperation OrchestrationOperation { get; private set; } = null!;

    #endregion

    #region Behaviors

    public void MarkRemoved(DateTime removedAtUtc)
    {
        IsActive = false;
        RemovedAtUtc = removedAtUtc;
    }

    public void UpdateVersion(string installedVersion)
    {
        InstalledVersion = installedVersion;
    }

    #endregion
}