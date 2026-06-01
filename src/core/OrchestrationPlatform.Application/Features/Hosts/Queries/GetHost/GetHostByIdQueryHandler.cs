using MediatR;
using OrchestrationPlatform.Application.Abstractions.Persistence.Common;
using OrchestrationPlatform.Application.Features.Hosts.Queries.DTOs;
using OrchestrationPlatform.Domain.Entities;

namespace OrchestrationPlatform.Application.Features.Hosts.Queries.GetHost;

internal sealed class GetHostByIdQueryHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<GetHostByIdQuery, HostDetailsDto>
{
    public async Task<HostDetailsDto> Handle(GetHostByIdQuery request, CancellationToken cancellationToken)
    {
        var repo = unitOfWork.GetReadRepository<OperatingSystemHost>();
        var host = await repo.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (host == null)
            throw new ApplicationException("Host not found.");

        return new HostDetailsDto(
            host.Id,
            host.Name,
            host.IpAddress,
            host.SshPort,
            host.Username,
            host.OperatingSystemFamily,
            host.OperatingSystemVersion,
            host.Architecture,
            host.Description,
            host.IsActive,
            host.CreatedAtUtc
        );
    }
}