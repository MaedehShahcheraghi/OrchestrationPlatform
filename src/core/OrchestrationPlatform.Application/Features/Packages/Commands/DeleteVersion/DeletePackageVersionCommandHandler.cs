using MediatR;
using OrchestrationPlatform.Application.Abstractions.Persistence.Common;
using OrchestrationPlatform.Domain.Entities;

namespace OrchestrationPlatform.Application.Features.Packages.Commands.DeleteVersion;

internal sealed class DeletePackageVersionCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<DeletePackageVersionCommand>
{
    public async Task Handle(DeletePackageVersionCommand request, CancellationToken cancellationToken)
    {
        var repo = unitOfWork.GetWriteRepository<SoftwarePackageVersion>();
        var version = await repo.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (version == null) throw new ApplicationException("Software Package Version not found.");

        repo.SoftDelete(version, DateTime.UtcNow);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}