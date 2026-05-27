using MediatR;

namespace OrchestrationPlatform.Application.Features.Operations.Commands.TriggerInstall;

public sealed record TriggerInstallCommand(
    List<Guid> OperatingSystemHostIds,
    Guid SoftwarePackageVersionId) : IRequest<List<Guid>>;