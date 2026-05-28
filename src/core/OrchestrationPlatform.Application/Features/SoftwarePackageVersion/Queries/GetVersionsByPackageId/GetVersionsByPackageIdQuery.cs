using MediatR;
using OrchestrationPlatform.Application.Features.SoftwarePackageVersion.Queries.DTOs;

namespace OrchestrationPlatform.Application.Features.SoftwarePackageVersion.Queries.GetVersionsByPackageId;

public sealed record GetVersionsByPackageIdQuery(Guid SoftwarePackageId)
    : IRequest<IReadOnlyList<SoftwarePackageVersionDto>>;