using MediatR;
using OrchestrationPlatform.Application.Abstractions.Persistence.Common;
using OrchestrationPlatform.Application.Features.Operations.Queries.DTOs;
using OrchestrationPlatform.Domain.Entities;

namespace OrchestrationPlatform.Application.Features.Operations.Queries.GetInstalledSoftwares;

internal sealed class GetInstalledSoftwaresQueryHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<GetInstalledSoftwaresQuery, IReadOnlyList<InstalledSoftwareDto>>
{
    public async Task<IReadOnlyList<InstalledSoftwareDto>> Handle(GetInstalledSoftwaresQuery request,
        CancellationToken cancellationToken)
    {
        var repo = unitOfWork.GetReadRepository<InstalledSoftware>();
        var installedSoftwares = await repo.ListProjectedAsync(
            x => new InstalledSoftwareDto(
                x.Id,
                x.SoftwarePackageVersion.SoftwarePackage.Name,
                x.SoftwarePackageVersion.Version,
                x.InstalledAtUtc),
            x => x.OperatingSystemHostId == request.HostId && x.RemovedAtUtc == null,
            cancellationToken: cancellationToken);

        return installedSoftwares;
    }
}