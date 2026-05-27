using MediatR;

namespace OrchestrationPlatform.Application.Features.Packages.Queries.GetPackagesForSelect;

public sealed record GetPackagesForSelectQuery : IRequest<IReadOnlyList<PackageSelectItemDto>>;