using MediatR;

namespace OrchestrationPlatform.Application.Features.Packages.Commands.CreatePackage;

public sealed record CreatePackageCommand(string Name, string? Description) : IRequest<Guid>;