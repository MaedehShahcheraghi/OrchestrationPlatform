using MediatR;
using Microsoft.EntityFrameworkCore;
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
    : IRequestHandler<TriggerInstallCommand, Dictionary<Guid, Guid>>
{
    public async Task<Dictionary<Guid, Guid>> Handle(TriggerInstallCommand request, CancellationToken cancellationToken)
    {
        var hostRepo = unitOfWork.GetReadRepository<OperatingSystemHost>();
        var artifactRepo = unitOfWork.GetReadRepository<PackageArtifact>();
        var operationRepo = unitOfWork.GetWriteRepository<OrchestrationOperation>();
        var versionRepo = unitOfWork.GetReadRepository<SoftwarePackageVersion>();

        var hosts = await hostRepo.ListAsync(h => request.OperatingSystemHostIds.Contains(h.Id),
            cancellationToken: cancellationToken);
        var packageVersion = await versionRepo.FirstOrDefaultAsync(
            v => v.Id == request.SoftwarePackageVersionId,
            cancellationToken,
            q => q.Include(x => x.SoftwarePackage));

        if (packageVersion == null) throw new ApplicationException("Package version not found.");
        if (!hosts.Any()) throw new ApplicationException("No valid hosts found.");

        var artifact = await artifactRepo.FirstOrDefaultAsync(
            a => a.SoftwarePackageVersionId == request.SoftwarePackageVersionId && a.IsActive, cancellationToken);
        if (artifact == null) throw new ApplicationException("Package artifact not found.");

        var downloadUrl = await storageService.GetDownloadUrlAsync(
            artifact.BucketName, artifact.ObjectKey, TimeSpan.FromHours(1), cancellationToken);

        var operations = new List<OrchestrationOperation>();
        var targetNodes = new List<BulkTargetModel>();
        var operationMap = new Dictionary<Guid, Guid>();

        foreach (var host in hosts)
        {
            var operation = OrchestrationOperation.CreateSoftwareOperation(
                request.SoftwarePackageVersionId,
                host.Id,
                OrchestrationOperationType.Install,
                packageVersion.SoftwarePackage.Name,
                packageVersion.Version);

            operations.Add(operation);
            targetNodes.Add(new BulkTargetModel(operation.Id, host.IpAddress, host.Username));
            operationMap.Add(operation.Id, host.Id);
        }

        var payload = new OrchestrationPayload(
            nameof(OrchestrationOperationType.Install),
            targetNodes,
            downloadUrl
        );

        var workflowExecutionId = await orchestrationService.TriggerWorkflowAsync(payload, cancellationToken);

        foreach (var op in operations) op.SetExternalWorkflowId(workflowExecutionId);

        await operationRepo.AddRangeAsync(operations, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return operationMap;
    }
}