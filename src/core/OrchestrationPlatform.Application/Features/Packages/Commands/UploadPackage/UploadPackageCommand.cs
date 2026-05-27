using MediatR;

namespace OrchestrationPlatform.Application.Features.Packages.Commands.UploadPackage;

public sealed record UploadPackageCommand(
    Guid SoftwarePackageVersionId,
    Stream FileStream,
    string FileName,
    string ContentType,
    long FileLength) : IRequest<Guid>;