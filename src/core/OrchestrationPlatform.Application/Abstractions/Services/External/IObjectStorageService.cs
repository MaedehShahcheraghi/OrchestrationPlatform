using OrchestrationPlatform.Application.Abstractions.Models.ServiceModels;

namespace OrchestrationPlatform.Application.Abstractions.Services.External;

public interface IObjectStorageService
{
    Task<UploadResult> UploadPackageAsync(
        Stream fileStream,
        string fileName,
        string contentType,
        CancellationToken cancellationToken = default);

    Task<bool> DeletePackageAsync(
        string bucketName,
        string objectKey,
        CancellationToken cancellationToken = default);

    Task<string> GetDownloadUrlAsync(
        string bucketName,
        string objectKey,
        TimeSpan expiration,
        CancellationToken cancellationToken = default);
}