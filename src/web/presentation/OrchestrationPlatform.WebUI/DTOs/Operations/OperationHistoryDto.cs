using OrchestrationPlatform.Domain.Enums;

namespace OrchestrationPlatform.WebUI.DTOs.Operations;

public record OperationHistoryDto(
    Guid OperationId,
    OrchestrationOperationType operationType,
    OrchestrationOperationStatus Status,
    string PackageNameSnapshot,
    string VersionSnapshot,
    DateTime RequestedAtUtc);