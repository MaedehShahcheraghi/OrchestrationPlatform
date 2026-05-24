namespace OrchestrationPlatform.Application.Abstractions.Models.ServiceModels;

public record UploadResult(
    string BucketName,
    string ObjectKey,
    long FileSize,
    string Sha256Hash
);