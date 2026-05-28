namespace OrchestrationPlatform.Application.Features.SoftwarePackageVersion.Queries.DTOs;

public record SoftwarePackageVersionDto(
    Guid Id,
    Guid SoftwarePackageId,
    string Version,
    string PackageType,
    string OperatingSystemFamily,
    string OperatingSystemVersion,
    string Architecture,
    bool IsActive);