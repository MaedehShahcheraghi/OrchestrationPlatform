namespace OrchestrationPlatform.Application.Features.Operations.Commands.TriggerConfiguration;

public record TriggerConfigurationCommandrResult
{
    public Dictionary<Guid, Guid> OperationHostMapping { get; set; } = new();
}