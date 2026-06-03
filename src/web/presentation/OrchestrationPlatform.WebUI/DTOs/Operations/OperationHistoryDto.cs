using OrchestrationPlatform.WebUI.Enums;

namespace OrchestrationPlatform.WebUI.DTOs.Operations;

public record OperationHistoryDto(
    Guid OperationId,
    InstallOperationType operationType,
    InstallOperationStatus Status,
    string PackageNameSnapshot,
    string VersionSnapshot,
    DateTime RequestedAtUtc);