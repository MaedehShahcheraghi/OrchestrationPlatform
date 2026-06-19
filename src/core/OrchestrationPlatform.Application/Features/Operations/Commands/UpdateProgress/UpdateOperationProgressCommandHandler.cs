using MediatR;
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
        var operationRepo = unitOfWork.GetWriteRepository<OrchestrationOperation>();
        var installedSoftwareRepo = unitOfWork.GetWriteRepository<InstalledSoftware>();

        var operation = await operationRepo.FirstOrDefaultAsync(x => x.Id == request.OperationId, cancellationToken);
        if (operation == null) return;

        Enum.TryParse<OrchestrationOperationStatus>(request.Status, true, out var parsedStatus);
        Enum.TryParse<OperationLogLevel>(request.LogLevel, true, out var parsedLogLevel);

        switch (parsedStatus)
        {
            case OrchestrationOperationStatus.Downloading:
                operation.MarkDownloading();
                break;
            case OrchestrationOperationStatus.Installing:
                operation.MarkInstalling();
                break;
            case OrchestrationOperationStatus.Configuring:
                operation.MarkConfiguring();
                break;
            case OrchestrationOperationStatus.Verifying:
                operation.MarkVerifying();
                break;
            case OrchestrationOperationStatus.Succeeded:
                operation.Succeed(DateTime.UtcNow);

                if (operation.SoftwarePackageVersionId.HasValue)
                {
                    if (operation.OperationType == OrchestrationOperationType.Install)
                    {
                        var alreadyInstalled = await installedSoftwareRepo.FirstOrDefaultAsync(
                            x => x.OperatingSystemHostId == operation.OperatingSystemHostId &&
                                 x.SoftwarePackageVersionId == operation.SoftwarePackageVersionId.Value &&
                                 x.IsActive, cancellationToken);

                        if (alreadyInstalled == null)
                        {
                            var inventoryRecord = new InstalledSoftware(
                                operation.SoftwarePackageVersionId.Value,
                                operation.OperatingSystemHostId,
                                operation.Id,
                                operation.PackageNameSnapshot,
                                operation.VersionSnapshot,
                                DateTime.UtcNow);

                            await installedSoftwareRepo.AddAsync(inventoryRecord, cancellationToken);
                        }
                    }
                    else if (operation.OperationType == OrchestrationOperationType.Uninstall)
                    {
                        var installedRecord = await installedSoftwareRepo.FirstOrDefaultAsync(
                            x => x.OperatingSystemHostId == operation.OperatingSystemHostId &&
                                 x.SoftwarePackageVersionId == operation.SoftwarePackageVersionId.Value &&
                                 x.IsActive, cancellationToken);

                        if (installedRecord != null) installedRecord.MarkRemoved(DateTime.UtcNow);
                    }
                }

                break;

            case OrchestrationOperationStatus.Failed:
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