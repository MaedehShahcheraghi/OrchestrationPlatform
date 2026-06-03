using OrchestrationPlatform.Domain.Common;

namespace OrchestrationPlatform.Domain.Entities;

public sealed class SoftwarePackage : AuditableEntity
{
    #region Navigation Properties

    public ICollection<SoftwarePackageVersion> Versions { get; private set; } = [];

    #endregion

    #region Constructors

    private SoftwarePackage()
    {
    }

    public SoftwarePackage(string name, string? description = null)
    {
        Name = name;
        Description = description;
        IsActive = true;
    }

    #endregion

    #region Properties

    public string Name { get; private set; } = null!;

    public string? Description { get; private set; }

    public bool IsActive { get; private set; }

    #endregion

    #region Behaviors

    public void Update(string name, string? description)
    {
        Name = name;
        Description = description;
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