using MediatR;

namespace OrchestrationPlatform.Application.Features.Operations.Commands.TriggerUninstall;

public sealed record TriggerUninstallCommand(
    List<Guid> OperatingSystemHostIds,
    Guid SoftwarePackageVersionId) : IRequest<Dictionary<Guid, Guid>>;