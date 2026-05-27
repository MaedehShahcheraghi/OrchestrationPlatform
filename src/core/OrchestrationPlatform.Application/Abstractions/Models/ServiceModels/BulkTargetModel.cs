namespace OrchestrationPlatform.Application.Abstractions.Models.ServiceModels;

public record BulkTargetModel(Guid OperationId, string HostIp, string SshUsername);