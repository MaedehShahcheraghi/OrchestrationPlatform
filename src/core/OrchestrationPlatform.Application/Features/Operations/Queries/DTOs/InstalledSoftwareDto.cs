namespace OrchestrationPlatform.Application.Features.Operations.Queries.DTOs;

public record InstalledSoftwareDto(Guid Id, string PackageName, string Version, DateTime InstalledAtUtc);