namespace OrchestrationPlatform.WebUI.Enums;

public enum InstallOperationStatus
{
    Pending = 1,
    Preparing = 2,
    Downloading = 3,
    Installing = 4,
    Verifying = 5,
    Succeeded = 6,
    Failed = 7,
    Canceled = 8
}