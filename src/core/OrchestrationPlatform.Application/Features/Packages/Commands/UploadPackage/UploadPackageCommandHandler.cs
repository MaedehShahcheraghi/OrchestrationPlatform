using MediatR;
using OrchestrationPlatform.Application.Abstractions.Persistence.Common;
using OrchestrationPlatform.Application.Abstractions.Services.External;
using OrchestrationPlatform.Domain.Entities;

namespace OrchestrationPlatform.Application.Features.Packages.Commands.UploadPackage;

internal sealed class UploadPackageCommandHandler : IRequestHandler<UploadPackageCommand, Guid>
{
    private readonly IObjectStorageService _storageService;
    private readonly IUnitOfWork _unitOfWork;

    public UploadPackageCommandHandler(
        IObjectStorageService storageService,
        IUnitOfWork unitOfWork)
    {
        _storageService = storageService;
        _unitOfWork = unitOfWork;
    }

    public async Task<Guid> Handle(UploadPackageCommand request, CancellationToken cancellationToken)
    {
        // فایل استریم مستقیماً پاس داده می‌شود
        var uploadResult = await _storageService.UploadPackageAsync(
            request.FileStream,
            request.FileName,
            request.ContentType,
            cancellationToken);

        var packageArtifact = new PackageArtifact(
            request.SoftwarePackageVersionId,
            uploadResult.BucketName,
            uploadResult.ObjectKey,
            request.FileName,
            request.FileLength,
            request.ContentType,
            uploadResult.Sha256Hash,
            DateTime.UtcNow
        );

        var repository = _unitOfWork.GetWriteRepository<PackageArtifact>();
        await repository.AddAsync(packageArtifact, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return packageArtifact.Id;
    }
}