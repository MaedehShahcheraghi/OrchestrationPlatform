using MediatR;
using OrchestrationPlatform.Application.Abstractions.Persistence.Common;
using OrchestrationPlatform.Application.Abstractions.Services.External;
using OrchestrationPlatform.Domain.Entities;
using OrchestrationPlatform.Domain.Enums;

namespace OrchestrationPlatform.Application.Features.Operations.Commands.TriggerInstall;

internal sealed class TriggerInstallCommandHandler(
    IUnitOfWork unitOfWork,
    IObjectStorageService storageService,
    IOrchestrationService orchestrationService)
    : IRequestHandler<TriggerInstallCommand, Guid>
{
    public async Task<Guid> Handle(TriggerInstallCommand request, CancellationToken cancellationToken)
    {
        var hostRepo = unitOfWork.GetReadRepository<OperatingSystemHost>();
        var artifactRepo = unitOfWork.GetReadRepository<PackageArtifact>();
        var operationRepo = unitOfWork.GetWriteRepository<InstallOperation>();

        var host = await hostRepo.GetByIdAsync(request.OperatingSystemHostId, cancellationToken);
        if (host == null) throw new ApplicationException("Host not found.");

        var artifact = await artifactRepo.FirstOrDefaultAsync(
            a => a.SoftwarePackageVersionId == request.SoftwarePackageVersionId && a.IsActive,
            cancellationToken);
        if (artifact == null) throw new ApplicationException("Package artifact not found.");

        var downloadUrl = await storageService.GetDownloadUrlAsync(
            artifact.BucketName,
            artifact.ObjectKey,
            TimeSpan.FromHours(1),
            cancellationToken);

        var operation = new InstallOperation(
            request.SoftwarePackageVersionId,
            request.OperatingSystemHostId,
            InstallOperationType.Install,
            DateTime.UtcNow);

        var workflowExecutionId = await orchestrationService.TriggerInstallWorkflowAsync(
            operation.Id,
            host.IpAddress,
            host.Username,
            downloadUrl,
            cancellationToken);

        operation.SetExternalWorkflowId(workflowExecutionId);
        await operationRepo.AddAsync(operation, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return operation.Id;
    }
}