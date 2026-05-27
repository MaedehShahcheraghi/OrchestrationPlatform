namespace OrchestrationPlatform.Application.Features.Packages.Queries.GetPackageVersionsForSelect;

public sealed record PackageVersionSelectItemDto(
    Guid Id,
    string Version,
    string PackageType,
    string OperatingSystemFamily,
    string OperatingSystemVersion,
    string Architecture);