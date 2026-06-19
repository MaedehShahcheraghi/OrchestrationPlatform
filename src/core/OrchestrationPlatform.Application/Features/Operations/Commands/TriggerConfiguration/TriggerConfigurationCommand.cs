using MediatR;
using OrchestrationPlatform.Domain.Enums;

namespace OrchestrationPlatform.Application.Features.Operations.Commands.TriggerConfiguration;

public sealed record TriggerConfigurationCommand(
    List<Guid> OperatingSystemHostIds,
    OrchestrationOperationType OperationType,
    string ConfigAction,
    string ConfigValue) : IRequest<TriggerConfigurationCommandrResult>;