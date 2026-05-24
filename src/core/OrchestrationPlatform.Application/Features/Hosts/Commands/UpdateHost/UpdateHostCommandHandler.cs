using MediatR;
using OrchestrationPlatform.Application.Abstractions.Persistence.Common;
using OrchestrationPlatform.Domain.Entities;

namespace OrchestrationPlatform.Application.Features.Hosts.Commands.UpdateHost;

internal sealed class UpdateHostCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<UpdateHostCommand>
{
    public async Task Handle(UpdateHostCommand request, CancellationToken cancellationToken)
    {
        var hostRepo = unitOfWork.GetWriteRepository<OperatingSystemHost>();

        var host = await unitOfWork.GetReadRepository<OperatingSystemHost>()
            .GetByIdAsync(request.Id, cancellationToken);

        if (host == null) throw new ApplicationException("Host not found.");

        host.Update(
            request.Name,
            request.IpAddress,
            request.SshPort,
            request.Username,
            request.OperatingSystemFamily,
            request.OperatingSystemVersion,
            request.Architecture,
            request.Description);

        hostRepo.Update(host);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}