using MediatR;

namespace OrchestrationPlatform.Application.Features.Packages.Commands.DeleteVersion;

public sealed record DeletePackageVersionCommand(Guid Id) : IRequest;