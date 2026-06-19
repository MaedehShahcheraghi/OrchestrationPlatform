using OrchestrationPlatform.Domain.Common;
using OrchestrationPlatform.Domain.Enums;

namespace OrchestrationPlatform.Domain.Entities;

public sealed class OrchestrationOperation : AuditableEntity
{
    private OrchestrationOperation()
    {
    }

    private OrchestrationOperation(
        Guid? softwarePackageVersionId,
        Guid operatingSystemHostId,
        OrchestrationOperationType operationType,
        DateTime requestedAtUtc)
    {
        SoftwarePackageVersionId = softwarePackageVersionId;
        OperatingSystemHostId = operatingSystemHostId;
        OperationType = operationType;
        Status = OrchestrationOperationStatus.Pending;
        ProgressPercent = 0;
        RequestedAtUtc = requestedAtUtc;
    }

    public Guid? SoftwarePackageVersionId { get; private set; }
    public SoftwarePackageVersion? SoftwarePackageVersion { get; }
    public Guid OperatingSystemHostId { get; private set; }
    public OperatingSystemHost OperatingSystemHost { get; private set; } = null!;
    public OrchestrationOperationType OperationType { get; private set; }
    public OrchestrationOperationStatus Status { get; private set; }
    public int ProgressPercent { get; private set; }
    public DateTime RequestedAtUtc { get; private set; }
    public DateTime? StartedAtUtc { get; private set; }
    public DateTime? FinishedAtUtc { get; private set; }
    public int? ExitCode { get; private set; }
    public string? ErrorMessage { get; private set; }
    public string? ExternalWorkflowId { get; private set; }
    public string PackageNameSnapshot { get; private set; } = string.Empty;
    public string VersionSnapshot { get; private set; } = string.Empty;
    public string? PayloadJson { get; private set; }
    public ICollection<OperationLog> Logs { get; } = [];

    public static OrchestrationOperation CreateSoftwareOperation(
        Guid softwarePackageVersionId,
        Guid operatingSystemHostId,
        OrchestrationOperationType operationType,
        string packageName,
        string version)
    {
        if (string.IsNullOrWhiteSpace(packageName))
            throw new ArgumentException("Package name snapshot cannot be empty.", nameof(packageName));

        var operation = new OrchestrationOperation(
            softwarePackageVersionId,
            operatingSystemHostId,
            operationType,
            DateTime.UtcNow)
        {
            PackageNameSnapshot = packageName,
            VersionSnapshot = version
        };

        return operation;
    }

    public static OrchestrationOperation CreateConfigurationOperation(
        Guid operatingSystemHostId,
        OrchestrationOperationType operationType,
        string payloadJson)
    {
        if (string.IsNullOrWhiteSpace(payloadJson))
            throw new ArgumentException("Payload JSON cannot be empty.", nameof(payloadJson));

        var operation = new OrchestrationOperation(
            null,
            operatingSystemHostId,
            operationType,
            DateTime.UtcNow)
        {
            PayloadJson = payloadJson
        };

        return operation;
    }

    public void Start(DateTime startedAtUtc)
    {
        Status = OrchestrationOperationStatus.Preparing;
        ProgressPercent = 10;
        StartedAtUtc = startedAtUtc;
    }

    public void MarkDownloading()
    {
        Status = OrchestrationOperationStatus.Downloading;
        ProgressPercent = 30;
    }

    public void MarkInstalling()
    {
        Status = OrchestrationOperationStatus.Installing;
        ProgressPercent = 60;
    }

    public void MarkConfiguring()
    {
        Status = OrchestrationOperationStatus.Configuring;
        ProgressPercent = 70;
    }

    public void MarkVerifying()
    {
        Status = OrchestrationOperationStatus.Verifying;
        ProgressPercent = 90;
    }

    public void Succeed(DateTime finishedAtUtc)
    {
        Status = OrchestrationOperationStatus.Succeeded;
        ProgressPercent = 100;
        FinishedAtUtc = finishedAtUtc;
        ErrorMessage = null;
    }

    public void Fail(string errorMessage, DateTime finishedAtUtc)
    {
        Status = OrchestrationOperationStatus.Failed;
        ErrorMessage = errorMessage;
        FinishedAtUtc = finishedAtUtc;
    }

    public void Cancel(DateTime finishedAtUtc)
    {
        Status = OrchestrationOperationStatus.Canceled;
        FinishedAtUtc = finishedAtUtc;
    }

    public void SetProgress(int progressPercent)
    {
        ProgressPercent = Math.Clamp(progressPercent, 0, 100);
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