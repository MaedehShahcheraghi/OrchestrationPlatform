namespace OrchestrationPlatform.Domain.Common;

public interface IAuditableEntity
{
    DateTime CreatedAtUtc { get; }
    DateTime? ModifiedAtUtc { get; }

    void SetCreatedAt(DateTime createdAtUtc);
    void SetModifiedAt(DateTime modifiedAtUtc);
}