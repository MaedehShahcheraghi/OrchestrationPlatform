namespace OrchestrationPlatform.Application.Features.Packages.Queries.GetPackagesForSelect;

public sealed record PackageSelectItemDto(
    Guid Id,
    string Name,
    string Description);