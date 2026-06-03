using OrchestrationPlatform.WebUI.Enums;

namespace OrchestrationPlatform.WebUI.DTOs.Operations;

public record OperationLogDto(Guid Id, OperationLogLevel Level, string Message, string? Details, DateTime LoggedAtUtc);