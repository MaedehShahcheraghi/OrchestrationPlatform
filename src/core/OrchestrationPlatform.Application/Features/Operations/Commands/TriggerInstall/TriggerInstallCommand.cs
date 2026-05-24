using MediatR;

namespace OrchestrationPlatform.Application.Features.Operations.Commands.TriggerInstall;

public sealed record TriggerInstallCommand(
    Guid OperatingSystemHostId,
    Guid SoftwarePackageVersionId) : IRequest<Guid>;