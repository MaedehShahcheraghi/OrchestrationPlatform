using MediatR;
using OrchestrationPlatform.Application.Abstractions.Persistence.Common;
using OrchestrationPlatform.Domain.Entities;

namespace OrchestrationPlatform.Application.Features.Packages.Commands.UpdatePackage;

internal sealed class UpdatePackageCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<UpdatePackageCommand>
{
    public async Task Handle(UpdatePackageCommand request, CancellationToken cancellationToken)
    {
        var repo = unitOfWork.GetWriteRepository<SoftwarePackage>();
        var package = await repo.GetForUpdateAsync(request.Id);

        if (package == null) throw new ApplicationException("Package not found.");

        package.Update(request.Name, request.Description);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}