using MediatR;
using OrchestrationPlatform.Application.Abstractions.Persistence.Common;
using OrchestrationPlatform.Domain.Entities;

namespace OrchestrationPlatform.Application.Features.Packages.Queries.GetPackageVersionsForSelect;

internal sealed class GetPackageVersionsForSelectQueryHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<GetPackageVersionsForSelectQuery, IReadOnlyList<PackageVersionSelectItemDto>>
{
    public async Task<IReadOnlyList<PackageVersionSelectItemDto>> Handle(
        GetPackageVersionsForSelectQuery request,
        CancellationToken cancellationToken)
    {
        var readRepository = unitOfWork.GetReadRepository<SoftwarePackageVersion>();

        var versions = await readRepository.ListProjectedAsync(
            v => new PackageVersionSelectItemDto(
                v.Id,
                v.Version,
                v.PackageType.ToString(),
                v.OperatingSystemFamily.ToString(),
                v.OperatingSystemVersion,
                v.Architecture.ToString()
            ),
            v => v.SoftwarePackage != null && v.SoftwarePackageId == request.SoftwarePackageId && v.IsActive,
            q => q.OrderByDescending(v => v.CreatedAtUtc),
            cancellationToken: cancellationToken
        );

        return versions;
    }
}