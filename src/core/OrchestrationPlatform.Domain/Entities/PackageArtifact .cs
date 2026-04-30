using OrchestrationPlatform.Domain.Common;

namespace OrchestrationPlatform.Domain.Entities;

public sealed class PackageArtifact : AuditableEntity
{
    #region Constructors

    private PackageArtifact()
    {
    }

    public PackageArtifact(
        Guid softwarePackageVersionId,
        string bucketName,
        string objectKey,
        string originalFileName,
        long fileSize,
        string? contentType,
        string? sha256Hash,
        DateTime uploadedAtUtc)
    {
        SoftwarePackageVersionId = softwarePackageVersionId;
        BucketName = bucketName;
        ObjectKey = objectKey;
        OriginalFileName = originalFileName;
        FileSize = fileSize;
        ContentType = contentType;
        Sha256Hash = sha256Hash;
        UploadedAtUtc = uploadedAtUtc;
        IsActive = true;
    }

    #endregion

    #region Foreign Keys

    public Guid SoftwarePackageVersionId { get; private set; }

    #endregion

    #region Properties

    public string BucketName { get; private set; } = null!;

    public string ObjectKey { get; private set; } = null!;

    public string OriginalFileName { get; private set; } = null!;

    public long FileSize { get; private set; }

    public string? ContentType { get; private set; }

    public string? Sha256Hash { get; private set; }

    public DateTime UploadedAtUtc { get; private set; }

    public bool IsActive { get; private set; }

    #endregion

    #region Navigation Properties

    public SoftwarePackageVersion SoftwarePackageVersion { get; private set; } = null!;

    #endregion

    #region Behaviors

    public void ReplaceFile(
        string bucketName,
        string objectKey,
        string originalFileName,
        long fileSize,
        string? contentType,
        string? sha256Hash,
        DateTime uploadedAtUtc)
    {
        BucketName = bucketName;
        ObjectKey = objectKey;
        OriginalFileName = originalFileName;
        FileSize = fileSize;
        ContentType = contentType;
        Sha256Hash = sha256Hash;
        UploadedAtUtc = uploadedAtUtc;
    }

    public void Enable()
    {
        IsActive = true;
    }

    public void Disable()
    {
        IsActive = false;
    }

    #endregion
}