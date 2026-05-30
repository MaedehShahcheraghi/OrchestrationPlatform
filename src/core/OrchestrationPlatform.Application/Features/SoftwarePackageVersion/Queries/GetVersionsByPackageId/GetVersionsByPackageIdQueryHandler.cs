using MediatR;
using OrchestrationPlatform.Application.Abstractions.Persistence.Common;
using OrchestrationPlatform.Application.Features.SoftwarePackageVersion.Queries.DTOs;

namespace OrchestrationPlatform.Application.Features.SoftwarePackageVersion.Queries.GetVersionsByPackageId;

internal sealed class GetVersionsByPackageIdQueryHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<GetVersionsByPackageIdQuery, IReadOnlyList<SoftwarePackageVersionDto>>
{
    public async Task<IReadOnlyList<SoftwarePackageVersionDto>> Handle(GetVersionsByPackageIdQuery request,
        CancellationToken cancellationToken)
    {
        var repo = unitOfWork.GetReadRepository<Domain.Entities.SoftwarePackageVersion>();

        return await repo.ListProjectedAsync(
            x => new SoftwarePackageVersionDto(
                x.Id,
                x.SoftwarePackageId,
                x.Version,
                x.PackageType.ToString(),
                x.OperatingSystemFamily.ToString(),
                x.OperatingSystemVersion,
                x.Architecture.ToString(),
                x.IsActive),
            x => x.SoftwarePackage != null && x.SoftwarePackageId == request.SoftwarePackageId,
            q => q.OrderByDescending(x => x.CreatedAtUtc),
            cancellationToken: cancellationToken);
    }
}