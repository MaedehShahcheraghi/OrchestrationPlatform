using OrchestrationPlatform.Domain.Enums;

namespace OrchestrationPlatform.WebUI.DTOs.Operations;

public record ConfigureOperationDto(
    List<Guid> OperatingSystemHostIds,
    OrchestrationOperationType OperationType,
    string ConfigAction,
    string ConfigValue);