namespace OrchestrationPlatform.Domain.Enums;

public enum OrchestrationOperationStatus
{
    Pending = 1,
    Preparing = 2,
    Downloading = 3,
    Installing = 4,
    Configuring = 5,
    Verifying = 6,
    Succeeded = 7,
    Failed = 8,
    Canceled = 9
}