using MediatR;
using OrchestrationPlatform.Application.Abstractions.Persistence.Common;
using OrchestrationPlatform.Domain.Entities;

namespace OrchestrationPlatform.Application.Features.Hosts.Commands.CreateHost;

internal sealed class CreateHostCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<CreateHostCommand, Guid>
{
    public async Task<Guid> Handle(CreateHostCommand request, CancellationToken cancellationToken)
    {
        var hostRepository = unitOfWork.GetWriteRepository<OperatingSystemHost>();

        var host = new OperatingSystemHost(
            request.Name,
            request.IpAddress,
            request.SshPort,
            request.Username,
            request.OperatingSystemFamily,
            request.OperatingSystemVersion,
            request.Architecture);

        await hostRepository.AddAsync(host, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return host.Id;
    }
}