namespace OrchestrationPlatform.Application.Features.Packages.Queries.DTOs;

public record SoftwarePackageDto(
    Guid Id,
    string Name,
    string? Description,
    bool IsActive,
    DateTime CreatedAtUtc);