using MediatR;
using OrchestrationPlatform.Application.Abstractions.Persistence.Common;

namespace OrchestrationPlatform.Application.Features.SoftwarePackageVersion.Commands.UpdateVersion;

internal sealed class UpdatePackageVersionCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<UpdatePackageVersionCommand>
{
    public async Task Handle(UpdatePackageVersionCommand request, CancellationToken cancellationToken)
    {
        var repo = unitOfWork.GetWriteRepository<Domain.Entities.SoftwarePackageVersion>();
        var version = await repo.GetForUpdateAsync(request.Id);

        if (version == null) throw new ApplicationException("Version not found.");

        version.Update(
            request.Version,
            request.PackageType,
            request.OperatingSystemFamily,
            request.OperatingSystemVersion,
            request.Architecture);

        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}