using OrchestrationPlatform.Domain.Enums;

namespace OrchestrationPlatform.Application.Features.Operations.Queries.DTOs;

public record OperationLogDto(Guid Id, OperationLogLevel Level, string Message, string? Details, DateTime LoggedAtUtc);