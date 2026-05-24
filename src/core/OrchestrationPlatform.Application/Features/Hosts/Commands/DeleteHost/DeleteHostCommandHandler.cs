using MediatR;
using OrchestrationPlatform.Application.Abstractions.Persistence.Common;
using OrchestrationPlatform.Domain.Entities;

namespace OrchestrationPlatform.Application.Features.Hosts.Commands.DeleteHost;

internal sealed class DeleteHostCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<DeleteHostCommand>
{
    public async Task Handle(DeleteHostCommand request, CancellationToken cancellationToken)
    {
        var hostWriteRepo = unitOfWork.GetWriteRepository<OperatingSystemHost>();
        var hostReadRepo = unitOfWork.GetReadRepository<OperatingSystemHost>();

        var host = await hostReadRepo.GetByIdAsync(request.Id, cancellationToken);
        if (host == null) throw new ApplicationException("Host not found.");

        hostWriteRepo.SoftDelete(host, DateTime.UtcNow);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}