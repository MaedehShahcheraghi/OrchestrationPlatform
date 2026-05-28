using MediatR;
using OrchestrationPlatform.Application.Abstractions.Persistence.Common;
using OrchestrationPlatform.Domain.Entities;

namespace OrchestrationPlatform.Application.Features.Packages.Commands.CreatePackage;

internal sealed class CreatePackageCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<CreatePackageCommand, Guid>
{
    public async Task<Guid> Handle(CreatePackageCommand request, CancellationToken cancellationToken)
    {
        var repo = unitOfWork.GetWriteRepository<SoftwarePackage>();

        var package = new SoftwarePackage(request.Name, request.Description);

        await repo.AddAsync(package, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return package.Id;
    }
}