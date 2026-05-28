using MediatR;
using OrchestrationPlatform.Application.Abstractions.Persistence.Common;
using OrchestrationPlatform.Domain.Entities;

namespace OrchestrationPlatform.Application.Features.SoftwarePackageVersion.Commands.CreateVersion;

internal sealed class CreatePackageVersionCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<CreatePackageVersionCommand, Guid>
{
    public async Task<Guid> Handle(CreatePackageVersionCommand request, CancellationToken cancellationToken)
    {
        var repo = unitOfWork.GetWriteRepository<Domain.Entities.SoftwarePackageVersion>();
        var packageRepo = unitOfWork.GetReadRepository<SoftwarePackage>();

        var packageExists = await packageRepo.ExistsAsync(x => x.Id == request.SoftwarePackageId, cancellationToken);
        if (!packageExists) throw new ApplicationException("Software Package does not exist.");

        var version = new Domain.Entities.SoftwarePackageVersion(
            request.SoftwarePackageId,
            request.Version,
            request.PackageType,
            request.OperatingSystemFamily,
            request.OperatingSystemVersion,
            request.Architecture);

        await repo.AddAsync(version, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return version.Id;
    }
}