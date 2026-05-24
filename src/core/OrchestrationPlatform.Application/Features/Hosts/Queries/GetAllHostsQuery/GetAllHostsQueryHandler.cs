using MediatR;
using OrchestrationPlatform.Application.Abstractions.Persistence.Common;
using OrchestrationPlatform.Domain.Entities;

namespace OrchestrationPlatform.Application.Features.Hosts.Queries.GetAllHostsQuery;

internal sealed class GetAllHostsQueryHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<GetAllHostsQuery, List<HostResponse>>
{
    public async Task<List<HostResponse>> Handle(GetAllHostsQuery request, CancellationToken cancellationToken)
    {
        var hostRepository = unitOfWork.GetReadRepository<OperatingSystemHost>();

        var hosts = await hostRepository.ListAsync(orderBy: o => o.OrderBy(q => q.Name),
            cancellationToken: cancellationToken);

        return hosts.Select(h => new HostResponse(
            h.Id,
            h.Name,
            h.IpAddress,
            h.SshPort,
            h.Username,
            h.OperatingSystemFamily,
            h.OperatingSystemVersion,
            h.Architecture,
            h.Status,
            h.IsActive)).ToList();
    }
}