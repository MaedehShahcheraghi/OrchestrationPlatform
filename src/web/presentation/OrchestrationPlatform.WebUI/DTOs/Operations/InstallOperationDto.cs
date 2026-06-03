namespace OrchestrationPlatform.WebUI.DTOs.Operations;

public record InstallOperationDto(
    Guid SoftwarePackageVersionId,
    List<Guid> OperatingSystemHostIds);