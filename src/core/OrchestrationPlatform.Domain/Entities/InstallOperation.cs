using OrchestrationPlatform.Domain.Common;
using OrchestrationPlatform.Domain.Enums;

namespace OrchestrationPlatform.Domain.Entities;

public sealed class InstallOperation : AuditableEntity
{
    private InstallOperation()
    {
    }

    public InstallOperation(
        Guid softwarePackageVersionId,
        Guid operatingSystemHostId,
        InstallOperationType operationType,
        DateTime requestedAtUtc)
    {
        SoftwarePackageVersionId = softwarePackageVersionId;
        OperatingSystemHostId = operatingSystemHostId;
        OperationType = operationType;
        Status = InstallOperationStatus.Pending;
        ProgressPercent = 0;
        RequestedAtUtc = requestedAtUtc;
    }

    public Guid SoftwarePackageVersionId { get; private set; }

    public SoftwarePackageVersion SoftwarePackageVersion { get; private set; } = null!;

    public Guid OperatingSystemHostId { get; private set; }

    public OperatingSystemHost OperatingSystemHost { get; private set; } = null!;

    public InstallOperationType OperationType { get; private set; }

    public InstallOperationStatus Status { get; private set; }

    public int ProgressPercent { get; private set; }

    public DateTime RequestedAtUtc { get; private set; }

    public DateTime? StartedAtUtc { get; private set; }

    public DateTime? FinishedAtUtc { get; private set; }

    public int? ExitCode { get; private set; }
    public string? ErrorMessage { get; private set; }

    public string? AnsiblePlaybookPath { get; private set; }

    public string? AnsibleInventoryPath { get; private set; }

    public string? ExternalWorkflowId { get; private set; }

    public ICollection<OperationLog> Logs { get; } = [];

    public void Start(DateTime startedAtUtc)
    {
        Status = InstallOperationStatus.Preparing;
        ProgressPercent = 10;
        StartedAtUtc = startedAtUtc;
    }

    public void MarkDownloading()
    {
        Status = InstallOperationStatus.Downloading;
        ProgressPercent = 30;
    }

    public void MarkInstalling()
    {
        Status = InstallOperationStatus.Installing;
        ProgressPercent = 70;
    }

    public void MarkVerifying()
    {
        Status = InstallOperationStatus.Verifying;
        ProgressPercent = 90;
    }

    public void Succeed(DateTime finishedAtUtc)
    {
        Status = InstallOperationStatus.Succeeded;
        ProgressPercent = 100;
        FinishedAtUtc = finishedAtUtc;
        ErrorMessage = null;
    }

    public void Fail(string errorMessage, DateTime finishedAtUtc)
    {
        Status = InstallOperationStatus.Failed;
        ErrorMessage = errorMessage;
        FinishedAtUtc = finishedAtUtc;
    }

    public void Cancel(DateTime finishedAtUtc)
    {
        Status = InstallOperationStatus.Canceled;
        FinishedAtUtc = finishedAtUtc;
    }

    public void SetProgress(int progressPercent)
    {
        ProgressPercent = Math.Clamp(progressPercent, 0, 100);
    }

    public void SetAnsibleInfo(string? playbookPath, string? inventoryPath)
    {
        AnsiblePlaybookPath = playbookPath;
        AnsibleInventoryPath = inventoryPath;
    }

    public void SetExternalWorkflowId(string? externalWorkflowId)
    {
        ExternalWorkflowId = externalWorkflowId;
    }

    public void AddLog(
        OperationLogLevel level,
        string message,
        string? details,
        DateTime loggedAtUtc)
    {
        Logs.Add(new OperationLog(Id, level, message, details, loggedAtUtc));
    }

    public void SetExitCode(int exitCode)
    {
        ExitCode = exitCode;
    }
}