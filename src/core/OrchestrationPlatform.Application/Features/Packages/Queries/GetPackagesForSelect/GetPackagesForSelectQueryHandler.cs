using MediatR;
using OrchestrationPlatform.Application.Abstractions.Persistence.Common;
using OrchestrationPlatform.Domain.Entities;

namespace OrchestrationPlatform.Application.Features.Packages.Queries.GetPackagesForSelect;

internal sealed class GetPackagesForSelectQueryHandler
    : IRequestHandler<GetPackagesForSelectQuery, IReadOnlyList<PackageSelectItemDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetPackagesForSelectQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IReadOnlyList<PackageSelectItemDto>> Handle(
        GetPackagesForSelectQuery request,
        CancellationToken cancellationToken)
    {
        var readRepository = _unitOfWork.GetReadRepository<SoftwarePackage>();

        var packages = await readRepository.ListProjectedAsync(
            p => new PackageSelectItemDto(p.Id, p.Name, p.Description ?? string.Empty),
            p => p.IsActive,
            q => q.OrderBy(p => p.Name), 
            cancellationToken: cancellationToken
        );

        return packages;
    }
}