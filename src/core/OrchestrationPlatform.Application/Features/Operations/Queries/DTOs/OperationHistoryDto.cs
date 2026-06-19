using OrchestrationPlatform.Domain.Enums;

namespace OrchestrationPlatform.Application.Features.Operations.Queries.DTOs;

public record OperationHistoryDto(
    Guid OperationId,
    OrchestrationOperationType operationType,
    OrchestrationOperationStatus Status,
    string PackageNameSnapshot,
    string VersionSnapshot,
    DateTime RequestedAtUtc);