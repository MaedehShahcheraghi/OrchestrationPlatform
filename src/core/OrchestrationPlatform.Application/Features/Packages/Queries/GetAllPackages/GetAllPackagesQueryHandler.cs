using MediatR;
using OrchestrationPlatform.Application.Abstractions.Persistence.Common;
using OrchestrationPlatform.Application.Features.Packages.Queries.DTOs;
using OrchestrationPlatform.Domain.Entities;

namespace OrchestrationPlatform.Application.Features.Packages.Queries.GetAllPackages;

internal sealed class GetAllPackagesQueryHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<GetAllPackagesQuery, IReadOnlyList<SoftwarePackageDto>>
{
    public async Task<IReadOnlyList<SoftwarePackageDto>> Handle(GetAllPackagesQuery request,
        CancellationToken cancellationToken)
    {
        var repo = unitOfWork.GetReadRepository<SoftwarePackage>();

        return await repo.ListProjectedAsync(
            x => new SoftwarePackageDto(x.Id, x.Name, x.Description, x.IsActive, x.CreatedAtUtc),
            orderBy: q => q.OrderByDescending(x => x.CreatedAtUtc),
            cancellationToken: cancellationToken);
    }
}