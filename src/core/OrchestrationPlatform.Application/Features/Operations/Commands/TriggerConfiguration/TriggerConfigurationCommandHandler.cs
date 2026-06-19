using System.Text.Json;
using MediatR;
using OrchestrationPlatform.Application.Abstractions.Models.ServiceModels;
using OrchestrationPlatform.Application.Abstractions.Persistence.Common;
using OrchestrationPlatform.Application.Abstractions.Services.External;
using OrchestrationPlatform.Domain.Entities;

namespace OrchestrationPlatform.Application.Features.Operations.Commands.TriggerConfiguration;

internal sealed class TriggerConfigurationCommandHandler(
    IUnitOfWork unitOfWork,
    IOrchestrationService orchestrationService)
    : IRequestHandler<TriggerConfigurationCommand, TriggerConfigurationCommandrResult>
{
    public async Task<TriggerConfigurationCommandrResult> Handle(TriggerConfigurationCommand request,
        CancellationToken cancellationToken)
    {
        var hostRepo = unitOfWork.GetReadRepository<OperatingSystemHost>();
        var operationRepo = unitOfWork.GetWriteRepository<OrchestrationOperation>();

        var hosts = await hostRepo.ListAsync(
            h => request.OperatingSystemHostIds.Contains(h.Id),
            cancellationToken: cancellationToken);

        if (!hosts.Any()) throw new ApplicationException("No valid hosts found.");

        var payloadJson = JsonSerializer.Serialize(new
        {
            Action = request.ConfigAction,
            Value = request.ConfigValue
        });

        var operations = new List<OrchestrationOperation>();
        var targetNodes = new List<BulkTargetModel>();
        var operationMap = new Dictionary<Guid, Guid>();

        foreach (var host in hosts)
        {
            var operation = OrchestrationOperation.CreateConfigurationOperation(
                host.Id,
                request.OperationType,
                payloadJson);

            operations.Add(operation);
            targetNodes.Add(new BulkTargetModel(operation.Id, host.IpAddress, host.Username));
            operationMap.Add(operation.Id, host.Id);
        }

        var payload = new OrchestrationPayload(
            "Configure",
            targetNodes,
            ConfigAction: request.ConfigAction,
            ConfigValue: request.ConfigValue
        );

        var workflowExecutionId = await orchestrationService.TriggerWorkflowAsync(payload, cancellationToken);

        foreach (var op in operations) op.SetExternalWorkflowId(workflowExecutionId);

        await operationRepo.AddRangeAsync(operations, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return new TriggerConfigurationCommandrResult { OperationHostMapping = operationMap };
    }
}