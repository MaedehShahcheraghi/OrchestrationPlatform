using MediatR;
using OrchestrationPlatform.Domain.Enums;

namespace OrchestrationPlatform.Application.Features.SoftwarePackageVersion.Commands.UpdateVersion;

public sealed record UpdatePackageVersionCommand(
    Guid Id,
    string Version,
    PackageType PackageType,
    OperatingSystemFamily OperatingSystemFamily,
    string OperatingSystemVersion,
    CpuArchitecture Architecture) : IRequest;