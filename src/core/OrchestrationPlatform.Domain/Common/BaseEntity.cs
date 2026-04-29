namespace OrchestrationPlatform.Domain.Common;

public class BaseEntity : IEntity
{
    public DateTime? DeletedDate { get; set; }

    public DateTime? CreatedTime { get; set; }

    public DateTime? ModifiedDate { get; set; }
    public Guid Id { get; }

    public override bool Equals(object obj)
    {
        if (!(obj is BaseEntity baseEntity))
            return false;
        if (this == baseEntity)
            return true;
        return GetType() == baseEntity.GetType() && Id.Equals((object)baseEntity.Id);
    }

    public override int GetHashCode()
    {
        return (GetType() + Id.ToString()).GetHashCode();
    }
}