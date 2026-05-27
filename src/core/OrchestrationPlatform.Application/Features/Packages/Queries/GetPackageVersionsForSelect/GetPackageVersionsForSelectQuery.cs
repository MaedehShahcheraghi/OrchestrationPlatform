using MediatR;

namespace OrchestrationPlatform.Application.Features.Packages.Queries.GetPackageVersionsForSelect;

public sealed record GetPackageVersionsForSelectQuery(Guid SoftwarePackageId)
    : IRequest<IReadOnlyList<PackageVersionSelectItemDto>>;