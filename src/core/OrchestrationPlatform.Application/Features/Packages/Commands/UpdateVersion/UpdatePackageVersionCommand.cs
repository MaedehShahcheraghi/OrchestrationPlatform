using MediatR;
using OrchestrationPlatform.Domain.Enums;

namespace OrchestrationPlatform.Application.Features.Packages.Commands.UpdateVersion;

public sealed record UpdatePackageVersionCommand(
    Guid Id,
    string Version,
    PackageType PackageType,
    OperatingSystemFamily OperatingSystemFamily,
    string OperatingSystemVersion,
    CpuArchitecture Architecture,
    Stream? FileStream = null,
    string? FileName = null,
    string? ContentType = null,
    long? FileLength = null) : IRequest;