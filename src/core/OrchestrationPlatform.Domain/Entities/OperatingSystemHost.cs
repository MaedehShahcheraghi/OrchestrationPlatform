using OrchestrationPlatform.Domain.Common;
using OrchestrationPlatform.Domain.Enums;

namespace OrchestrationPlatform.Domain.Entities;

public sealed class OperatingSystemHost : AuditableEntity
{
    #region Constructors

    private OperatingSystemHost()
    {
    }

    public OperatingSystemHost(
        string name,
        string ipAddress,
        int sshPort,
        string username,
        OperatingSystemFamily operatingSystemFamily,
        string operatingSystemVersion,
        CpuArchitecture architecture)
    {
        Name = name;
        IpAddress = ipAddress;
        SshPort = sshPort;
        Username = username;
        OperatingSystemFamily = operatingSystemFamily;
        OperatingSystemVersion = operatingSystemVersion;
        Architecture = architecture;
        Status = HostStatus.Unknown;
        IsActive = true;
    }

    #endregion

    #region Properties

    public string Name { get; private set; } = null!;

    public string IpAddress { get; private set; } = null!;

    public int SshPort { get; private set; }

    public string Username { get; private set; } = null!;

    public OperatingSystemFamily OperatingSystemFamily { get; private set; }

    public string OperatingSystemVersion { get; private set; } = null!;

    public CpuArchitecture Architecture { get; private set; }

    public HostStatus Status { get; private set; }

    public string? Description { get; private set; }

    public string? SshKeyPath { get; private set; }

    public string? LastConnectionError { get; private set; }

    public DateTime? LastSeenAtUtc { get; private set; }

    public bool IsActive { get; private set; }

    #endregion

    #region Navigation Properties

    public ICollection<InstallOperation> InstallOperations { get; private set; } = [];

    public ICollection<InstalledSoftware> InstalledSoftwares { get; private set; } = [];

    #endregion

    #region Behaviors

    public void Update(
        string name,
        string ipAddress,
        int sshPort,
        string username,
        OperatingSystemFamily operatingSystemFamily,
        string operatingSystemVersion,
        CpuArchitecture architecture,
        string? description)
    {
        Name = name;
        IpAddress = ipAddress;
        SshPort = sshPort;
        Username = username;
        OperatingSystemFamily = operatingSystemFamily;
        OperatingSystemVersion = operatingSystemVersion;
        Architecture = architecture;
        Description = description;
    }

    public void SetSshKeyPath(string? sshKeyPath)
    {
        SshKeyPath = sshKeyPath;
    }

    public void MarkOnline(DateTime lastSeenAtUtc)
    {
        Status = HostStatus.Online;
        LastSeenAtUtc = lastSeenAtUtc;
        LastConnectionError = null;
    }

    public void MarkOffline(string? errorMessage)
    {
        Status = HostStatus.Offline;
        LastConnectionError = errorMessage;
    }

    public void SetMaintenance()
    {
        Status = HostStatus.Maintenance;
    }

    public void Enable()
    {
        IsActive = true;
        Status = HostStatus.Unknown;
        LastConnectionError = null;
    }

    public void Disable()
    {
        IsActive = false;
        Status = HostStatus.Disabled;
    }

    #endregion
}