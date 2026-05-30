using MediatR;
using Microsoft.EntityFrameworkCore;
using OrchestrationPlatform.Application.Abstractions.Models.ServiceModels;
using OrchestrationPlatform.Application.Abstractions.Persistence.Common;
using OrchestrationPlatform.Application.Abstractions.Services.External;
using OrchestrationPlatform.Domain.Entities;
using OrchestrationPlatform.Domain.Enums;

namespace OrchestrationPlatform.Application.Features.Operations.Commands.TriggerUninstall;

internal sealed class TriggerUninstallCommandHandler(
    IUnitOfWork unitOfWork,
    IOrchestrationService orchestrationService)
    : IRequestHandler<TriggerUninstallCommand, List<Guid>>
{
    public async Task<List<Guid>> Handle(TriggerUninstallCommand request, CancellationToken cancellationToken)
    {
        var hostRepo = unitOfWork.GetReadRepository<OperatingSystemHost>();
        var versionRepo = unitOfWork.GetReadRepository<Domain.Entities.SoftwarePackageVersion>();
        var installedSoftwareRepo = unitOfWork.GetReadRepository<InstalledSoftware>(); // ریپازیتوری جدید
        var operationRepo = unitOfWork.GetWriteRepository<InstallOperation>();

        var requestedHosts = await hostRepo.ListAsync(
            h => request.OperatingSystemHostIds.Contains(h.Id),
            cancellationToken: cancellationToken);

        if (!requestedHosts.Any()) throw new ApplicationException("No valid hosts found.");

        var packageVersion = await versionRepo.FirstOrDefaultAsync(
            v => v.Id == request.SoftwarePackageVersionId,
            includeAction: q => q.Include(v => v.SoftwarePackage),
            cancellationToken: cancellationToken);

        if (packageVersion == null) throw new ApplicationException("Software version not found.");


        var installedRecords = await installedSoftwareRepo.ListAsync(
            i => request.OperatingSystemHostIds.Contains(i.OperatingSystemHostId) &&
                 i.SoftwarePackageVersionId == request.SoftwarePackageVersionId &&
                 i.IsActive,
            cancellationToken: cancellationToken);

        var validHostIds = installedRecords.Select(i => i.OperatingSystemHostId).ToList();

        var targetHosts = requestedHosts.Where(h => validHostIds.Contains(h.Id)).ToList();

        if (!targetHosts.Any())
            throw new ApplicationException(
                $"The software '{packageVersion.SoftwarePackage.Name}' is not currently installed on any of the selected hosts.");


        var operations = new List<InstallOperation>();
        var targetNodes = new List<BulkTargetModel>();

        foreach (var host in targetHosts)
        {
            var operation = new InstallOperation(
                request.SoftwarePackageVersionId,
                host.Id,
                InstallOperationType.Uninstall,
                DateTime.UtcNow);

            operations.Add(operation);
            targetNodes.Add(new BulkTargetModel(operation.Id, host.IpAddress, host.Username));
        }

        var workflowExecutionId = await orchestrationService.TriggerUninstallWorkflowAsync(
            targetNodes,
            packageVersion.SoftwarePackage.Name,
            cancellationToken);

        foreach (var op in operations) op.SetExternalWorkflowId(workflowExecutionId);

        await operationRepo.AddRangeAsync(operations, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return operations.Select(o => o.Id).ToList();
    }
}