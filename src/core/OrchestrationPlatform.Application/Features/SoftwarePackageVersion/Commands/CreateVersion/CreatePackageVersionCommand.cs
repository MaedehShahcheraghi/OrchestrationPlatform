using MediatR;
using OrchestrationPlatform.Domain.Enums;

namespace OrchestrationPlatform.Application.Features.SoftwarePackageVersion.Commands.CreateVersion;

public sealed record CreatePackageVersionCommand(
    Guid SoftwarePackageId,
    string Version,
    PackageType PackageType,
    OperatingSystemFamily OperatingSystemFamily,
    string OperatingSystemVersion,
    CpuArchitecture Architecture) : IRequest<Guid>;