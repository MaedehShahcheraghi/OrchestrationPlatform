// 1. UploadPackageVersionCommand.cs

using MediatR;
using OrchestrationPlatform.Domain.Enums;

namespace OrchestrationPlatform.Application.Features.Packages.Commands.UploadPackageVersion;

public sealed record UploadPackageVersionCommand(
    Guid SoftwarePackageId,
    string Version,
    PackageType PackageType,
    OperatingSystemFamily OperatingSystemFamily,
    string OperatingSystemVersion,
    CpuArchitecture Architecture,
    Stream FileStream,
    string FileName,
    string ContentType,
    long FileLength) : IRequest<Guid>;