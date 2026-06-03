namespace OrchestrationPlatform.WebUI.DTOs.Operations;

public record TriggerOperationResponseDto(string Message, Dictionary<Guid, Guid> OperationHostMapping);