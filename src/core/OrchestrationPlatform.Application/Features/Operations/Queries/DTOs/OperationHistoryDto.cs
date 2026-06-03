using OrchestrationPlatform.Domain.Enums;

namespace OrchestrationPlatform.Application.Features.Operations.Queries.DTOs;

public record OperationHistoryDto(
    Guid OperationId,
    InstallOperationType operationType,
    InstallOperationStatus Status,
    string PackageNameSnapshot,
    string VersionSnapshot,
    DateTime RequestedAtUtc);