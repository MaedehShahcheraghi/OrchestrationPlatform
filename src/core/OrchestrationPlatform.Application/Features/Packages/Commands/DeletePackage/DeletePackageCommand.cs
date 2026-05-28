using MediatR;

namespace OrchestrationPlatform.Application.Features.Packages.Commands.DeletePackage;

public sealed record DeletePackageCommand(Guid Id) : IRequest;