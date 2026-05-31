using MediatR;
using OrchestrationPlatform.Application.Abstractions.Persistence.Common;
using OrchestrationPlatform.Application.Abstractions.Services.External;
using OrchestrationPlatform.Domain.Entities;

namespace OrchestrationPlatform.Application.Features.Packages.Commands.UploadPackageVersion;

internal sealed class UploadPackageVersionCommandHandler(
    IObjectStorageService storageService,
    IUnitOfWork unitOfWork)
    : IRequestHandler<UploadPackageVersionCommand, Guid>
{
    public async Task<Guid> Handle(UploadPackageVersionCommand request, CancellationToken cancellationToken)
    {
        var packageRepo = unitOfWork.GetReadRepository<SoftwarePackage>();
        var versionRepo = unitOfWork.GetWriteRepository<SoftwarePackageVersion>();
        var artifactRepo = unitOfWork.GetWriteRepository<PackageArtifact>();

        var packageExists = await packageRepo.ExistsAsync(x => x.Id == request.SoftwarePackageId, cancellationToken);
        if (!packageExists) throw new ApplicationException("Software Package does not exist.");

        var uploadResult = await storageService.UploadPackageAsync(
            request.FileStream,
            request.FileName,
            request.ContentType,
            cancellationToken);

        var version = new SoftwarePackageVersion(
            request.SoftwarePackageId,
            request.Version,
            request.PackageType,
            request.OperatingSystemFamily,
            request.OperatingSystemVersion,
            request.Architecture);

        var packageArtifact = new PackageArtifact(
            version.Id,
            uploadResult.BucketName,
            uploadResult.ObjectKey,
            request.FileName,
            request.FileLength,
            request.ContentType,
            uploadResult.Sha256Hash,
            DateTime.UtcNow);

        await versionRepo.AddAsync(version, cancellationToken);
        await artifactRepo.AddAsync(packageArtifact, cancellationToken);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return version.Id;
    }
}