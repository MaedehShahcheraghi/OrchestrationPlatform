using MediatR;
using OrchestrationPlatform.Application.Features.Packages.Queries.DTOs;

namespace OrchestrationPlatform.Application.Features.Packages.Queries.GetAllPackages;

public sealed record GetAllPackagesQuery : IRequest<IReadOnlyList<SoftwarePackageDto>>;