using MediatR;
using OrchestrationPlatform.Application.Abstractions.Persistence.Common;
using OrchestrationPlatform.Domain.Entities;

namespace OrchestrationPlatform.Application.Features.Packages.Commands.DeletePackage;

internal sealed class DeletePackageCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<DeletePackageCommand>
{
    public async Task Handle(DeletePackageCommand request, CancellationToken cancellationToken)
    {
        var repo = unitOfWork.GetWriteRepository<SoftwarePackage>();
        var package = await repo.GetForUpdateAsync(request.Id);

        if (package == null) throw new ApplicationException("Package not found.");

        repo.SoftDelete(package, DateTime.UtcNow);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}