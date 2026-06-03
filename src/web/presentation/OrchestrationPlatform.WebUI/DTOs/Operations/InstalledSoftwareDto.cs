namespace OrchestrationPlatform.WebUI.DTOs.Operations;

public record InstalledSoftwareDto(Guid Id, string PackageName, string Version, DateTime InstalledAtUtc);