using MediatR;
using Microsoft.EntityFrameworkCore;
using OrchestrationPlatform.Application.Abstractions.Persistence.Common;
using OrchestrationPlatform.Application.Abstractions.Services.Api;
using OrchestrationPlatform.Domain.Entities;
using OrchestrationPlatform.Domain.Enums;

namespace OrchestrationPlatform.Application.Features.Operations.Commands.UpdateProgress;

public sealed class UpdateOperationProgressCommandHandler(
    IUnitOfWork unitOfWork,
    IOperationNotifierService notifier)
    : IRequestHandler<UpdateOperationProgressCommand>
{
    public async Task Handle(UpdateOperationProgressCommand request, CancellationToken cancellationToken)
    {
        var operationRepo = unitOfWork.GetWriteRepository<InstallOperation>();
        var installedSoftwareRepo = unitOfWork.GetWriteRepository<InstalledSoftware>();

        var operation = await operationRepo.FirstOrDefaultAsync(x => x.Id == request.OperationId,
            includeAction: x => x
                .Include(y => y.SoftwarePackageVersion)
                .ThenInclude(z => z.SoftwarePackage));
        if (operation == null) return;

        Enum.TryParse<InstallOperationStatus>(request.Status, true, out var parsedStatus);
        Enum.TryParse<OperationLogLevel>(request.LogLevel, true, out var parsedLogLevel);

        switch (parsedStatus)
        {
            case InstallOperationStatus.Downloading:
                operation.MarkDownloading();
                break;
            case InstallOperationStatus.Installing:
                operation.MarkInstalling();
                break;
            case InstallOperationStatus.Verifying:
                operation.MarkVerifying();
                break;
            case InstallOperationStatus.Succeeded:
                operation.Succeed(DateTime.UtcNow);

                var alreadyInstalled = await installedSoftwareRepo.FirstOrDefaultAsync(
                    x => x.OperatingSystemHostId == operation.OperatingSystemHostId &&
                         x.SoftwarePackageVersionId == operation.SoftwarePackageVersionId, cancellationToken);

                if (alreadyInstalled == null)
                {
                    var inventoryRecord = new InstalledSoftware(
                        operation.SoftwarePackageVersionId,
                        operation.OperatingSystemHostId,
                        operation.Id,
                        operation.SoftwarePackageVersion.SoftwarePackage.Name,
                        operation.SoftwarePackageVersion.Version,
                        DateTime.UtcNow);

                    await installedSoftwareRepo.AddAsync(inventoryRecord, cancellationToken);
                }

                break;

            case InstallOperationStatus.Failed:
                operation.Fail(request.Message, DateTime.UtcNow);
                break;
        }

        if (request.ProgressPercent > 0) operation.SetProgress(request.ProgressPercent);

        operation.AddLog(parsedLogLevel, request.Message, null, DateTime.UtcNow);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        await notifier.NotifyProgressAsync(
            operation.Id,
            parsedStatus.ToString(),
            operation.ProgressPercent,
            request.Message,
            cancellationToken);
    }
}