using MediatR;
using OrchestrationPlatform.Application.Abstractions.Models.ServiceModels;
using OrchestrationPlatform.Application.Abstractions.Persistence.Common;
using OrchestrationPlatform.Application.Abstractions.Services.External;
using OrchestrationPlatform.Domain.Entities;
using OrchestrationPlatform.Domain.Enums;

namespace OrchestrationPlatform.Application.Features.Operations.Commands.TriggerInstall;

internal sealed class TriggerInstallCommandHandler(
    IUnitOfWork unitOfWork,
    IObjectStorageService storageService,
    IOrchestrationService orchestrationService)
    : IRequestHandler<TriggerInstallCommand, List<Guid>>
{
    public async Task<List<Guid>> Handle(TriggerInstallCommand request, CancellationToken cancellationToken)
    {
        var hostRepo = unitOfWork.GetReadRepository<OperatingSystemHost>();
        var artifactRepo = unitOfWork.GetReadRepository<PackageArtifact>();
        var operationRepo = unitOfWork.GetWriteRepository<InstallOperation>();

        var hosts = await hostRepo.ListAsync(h => request.OperatingSystemHostIds.Contains(h.Id),
            cancellationToken: cancellationToken);
        if (!hosts.Any()) throw new ApplicationException("No valid hosts found.");

        var artifact = await artifactRepo.FirstOrDefaultAsync(
            a => a.SoftwarePackageVersionId == request.SoftwarePackageVersionId && a.IsActive, cancellationToken);
        if (artifact == null) throw new ApplicationException("Package artifact not found.");

        var downloadUrl = await storageService.GetDownloadUrlAsync(
            artifact.BucketName, artifact.ObjectKey, TimeSpan.FromHours(1), cancellationToken);

        var operations = new List<InstallOperation>();
        var targetNodes = new List<BulkTargetModel>();

        foreach (var host in hosts)
        {
            var operation = new InstallOperation(
                request.SoftwarePackageVersionId,
                host.Id,
                InstallOperationType.Install,
                DateTime.UtcNow);

            operations.Add(operation);

            targetNodes.Add(new BulkTargetModel(operation.Id, host.IpAddress, host.Username));
        }

        var workflowExecutionId = await orchestrationService.TriggerInstallWorkflowAsync(
            targetNodes, downloadUrl, cancellationToken);

        foreach (var op in operations) op.SetExternalWorkflowId(workflowExecutionId);

        await operationRepo.AddRangeAsync(operations, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return operations.Select(o => o.Id).ToList();
    }
}