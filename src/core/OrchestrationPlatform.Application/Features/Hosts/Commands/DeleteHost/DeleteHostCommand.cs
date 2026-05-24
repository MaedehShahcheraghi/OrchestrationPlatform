using MediatR;

namespace OrchestrationPlatform.Application.Features.Hosts.Commands.DeleteHost;

public sealed record DeleteHostCommand(Guid Id) : IRequest;