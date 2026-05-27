using MediatR;
using OrchestrationPlatform.Application.Abstractions.Persistence.Common;
using OrchestrationPlatform.Domain.Entities;

namespace OrchestrationPlatform.Application.Features.Packages.Queries.GetPackageVersionsForSelect;

internal sealed class GetPackageVersionsForSelectQueryHandler
    : IRequestHandler<GetPackageVersionsForSelectQuery, IReadOnlyList<PackageVersionSelectItemDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetPackageVersionsForSelectQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IReadOnlyList<PackageVersionSelectItemDto>> Handle(
        GetPackageVersionsForSelectQuery request,
        CancellationToken cancellationToken)
    {
        // دسترسی یکپارچه به ریپازیتوری خواندن ورژن‌ها
        var readRepository = _unitOfWork.GetReadRepository<SoftwarePackageVersion>();

        var versions = await readRepository.ListProjectedAsync(
            v => new PackageVersionSelectItemDto(
                v.Id,
                v.Version,
                v.PackageType.ToString(),
                v.OperatingSystemFamily.ToString(),
                v.OperatingSystemVersion,
                v.Architecture.ToString()
            ),
            v => v.SoftwarePackageId == request.SoftwarePackageId && v.IsActive,
            q => q.OrderByDescending(v => v.CreatedAtUtc), // نسخه‌های جدیدتر در بالای لیست
            cancellationToken: cancellationToken
        );

        return versions;
    }
}