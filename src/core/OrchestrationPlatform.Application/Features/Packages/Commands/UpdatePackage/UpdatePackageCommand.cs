using MediatR;

namespace OrchestrationPlatform.Application.Features.Packages.Commands.UpdatePackage;

public sealed record UpdatePackageCommand(Guid Id, string Name, string? Description) : IRequest;