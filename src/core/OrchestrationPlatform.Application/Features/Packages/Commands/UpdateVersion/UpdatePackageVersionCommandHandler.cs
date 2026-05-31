using MediatR;
using Microsoft.EntityFrameworkCore;
using OrchestrationPlatform.Application.Abstractions.Persistence.Common;
using OrchestrationPlatform.Application.Abstractions.Services.External;
using OrchestrationPlatform.Domain.Entities;

namespace OrchestrationPlatform.Application.Features.Packages.Commands.UpdateVersion;

internal sealed class UpdatePackageVersionCommandHandler(
    IUnitOfWork unitOfWork,
    IObjectStorageService storageService)
    : IRequestHandler<UpdatePackageVersionCommand>
{
    public async Task Handle(UpdatePackageVersionCommand request, CancellationToken cancellationToken)
    {
        var versionRepo = unitOfWork.GetWriteRepository<SoftwarePackageVersion>();
        var artifactRepo = unitOfWork.GetWriteRepository<PackageArtifact>();

        var version = await versionRepo.FirstOrDefaultAsync(
            x => x.Id == request.Id,
            includeAction: x => x.Include(v => v.Artifact),
            cancellationToken: cancellationToken);

        if (version == null) throw new ApplicationException("Software Package Version not found.");

        version.Update(
            request.Version,
            request.PackageType,
            request.OperatingSystemFamily,
            request.OperatingSystemVersion,
            request.Architecture);

        if (request is { FileStream: not null, FileLength: > 0 } && !string.IsNullOrEmpty(request.FileName))
        {
            var uploadResult = await storageService.UploadPackageAsync(
                request.FileStream,
                request.FileName,
                request.ContentType ?? "application/octet-stream",
                cancellationToken);

            if (version.Artifact != null) artifactRepo.SoftDelete(version.Artifact, DateTime.UtcNow);

            var newArtifact = new PackageArtifact(
                version.Id,
                uploadResult.BucketName,
                uploadResult.ObjectKey,
                request.FileName,
                request.FileLength.Value,
                request.ContentType ?? "application/octet-stream",
                uploadResult.Sha256Hash,
                DateTime.UtcNow);

            await artifactRepo.AddAsync(newArtifact, cancellationToken);
        }

        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}