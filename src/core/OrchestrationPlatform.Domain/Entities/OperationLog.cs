using OrchestrationPlatform.Domain.Common;
using OrchestrationPlatform.Domain.Enums;

namespace OrchestrationPlatform.Domain.Entities;

public sealed class OperationLog : AuditableEntity
{
    #region Foreign Keys

    public Guid InstallOperationId { get; private set; }

    #endregion

    #region Navigation Properties

    public InstallOperation InstallOperation { get; private set; } = null!;

    #endregion

    #region Constructors

    private OperationLog()
    {
    }

    public OperationLog(
        Guid installOperationId,
        OperationLogLevel level,
        string message,
        string? details,
        DateTime loggedAtUtc)
    {
        InstallOperationId = installOperationId;
        Level = level;
        Message = message;
        Details = details;
        LoggedAtUtc = loggedAtUtc;
    }

    #endregion

    #region Properties

    public OperationLogLevel Level { get; private set; }

    public string Message { get; private set; } = null!;

    public string? Details { get; private set; }

    public DateTime LoggedAtUtc { get; private set; }

    #endregion
}