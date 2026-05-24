using System.Security.Cryptography;
using Microsoft.Extensions.Logging;
using Minio;
using Minio.DataModel.Args;
using Minio.Exceptions;
using OrchestrationPlatform.Application.Abstractions.Models.ServiceModels;
using OrchestrationPlatform.Application.Abstractions.Services.External;

namespace OrchestrationPlatform.Infrastructure.External.Services;

public sealed class MinIoObjectStorageService : IObjectStorageService
{
    private const string DefaultBucketName = "software-packages";
    private readonly ILogger<MinIoObjectStorageService> _logger;
    private readonly IMinioClient _minioClient;

    public MinIoObjectStorageService(IMinioClient minioClient, ILogger<MinIoObjectStorageService> logger)
    {
        _minioClient = minioClient;
        _logger = logger;
    }

    public async Task<UploadResult> UploadPackageAsync(
        Stream fileStream,
        string fileName,
        string contentType,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var bucketExistsArgs = new BucketExistsArgs().WithBucket(DefaultBucketName);
            var found = await _minioClient.BucketExistsAsync(bucketExistsArgs, cancellationToken);
            if (!found)
            {
                var makeBucketArgs = new MakeBucketArgs().WithBucket(DefaultBucketName);
                await _minioClient.MakeBucketAsync(makeBucketArgs, cancellationToken);
                _logger.LogInformation("Bucket {BucketName} created successfully.", DefaultBucketName);
            }

            var objectKey = $"{Guid.NewGuid():N}/{fileName}";
            var fileSize = fileStream.Length;

            string sha256Hash;
            using (var sha256 = SHA256.Create())
            {
                fileStream.Position = 0;
                var hashBytes = await sha256.ComputeHashAsync(fileStream, cancellationToken);
                sha256Hash = BitConverter.ToString(hashBytes).Replace("-", "").ToLowerInvariant();
                fileStream.Position = 0;
            }

            var putObjectArgs = new PutObjectArgs()
                .WithBucket(DefaultBucketName)
                .WithObject(objectKey)
                .WithStreamData(fileStream)
                .WithObjectSize(fileSize)
                .WithContentType(contentType);

            await _minioClient.PutObjectAsync(putObjectArgs, cancellationToken);

            _logger.LogInformation("File {FileName} uploaded successfully to {BucketName}/{ObjectKey}.",
                fileName, DefaultBucketName, objectKey);

            return new UploadResult(DefaultBucketName, objectKey, fileSize, sha256Hash);
        }
        catch (MinioException ex)
        {
            _logger.LogError(ex, "An error occurred while uploading {FileName} to MinIO.", fileName);
            throw new ApplicationException($"Failed to upload package to storage: {ex.Message}", ex);
        }
    }

    public async Task<bool> DeletePackageAsync(
        string bucketName,
        string objectKey,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var removeObjectArgs = new RemoveObjectArgs()
                .WithBucket(bucketName)
                .WithObject(objectKey);

            await _minioClient.RemoveObjectAsync(removeObjectArgs, cancellationToken);
            _logger.LogInformation("Object {ObjectKey} deleted from {BucketName}.", objectKey, bucketName);
            return true;
        }
        catch (MinioException ex)
        {
            _logger.LogError(ex, "Failed to delete object {ObjectKey} from {BucketName}.", objectKey, bucketName);
            return false;
        }
    }

    public async Task<string> GetDownloadUrlAsync(
        string bucketName,
        string objectKey,
        TimeSpan expiration,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var presignedGetObjectArgs = new PresignedGetObjectArgs()
                .WithBucket(bucketName)
                .WithObject(objectKey)
                .WithExpiry((int)expiration.TotalSeconds);

            var url = await _minioClient.PresignedGetObjectAsync(presignedGetObjectArgs);
            return url;
        }
        catch (MinioException ex)
        {
            _logger.LogError(ex, "Failed to generate presigned URL for {ObjectKey}.", objectKey);
            throw new ApplicationException("Could not generate download URL.", ex);
        }
    }
}