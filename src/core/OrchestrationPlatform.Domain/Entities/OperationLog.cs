using OrchestrationPlatform.Domain.Common;
using OrchestrationPlatform.Domain.Enums;

namespace OrchestrationPlatform.Domain.Entities;

public sealed class OperationLog : AuditableEntity
{
    #region Foreign Keys

    public Guid OrchestrationOperationId { get; }

    #endregion

    #region Navigation Properties

    public OrchestrationOperation OrchestrationOperation { get; private set; } = null!;

    #endregion

    #region Constructors

    private OperationLog()
    {
    }

    public OperationLog(
        Guid OrchestrationOperationId,
        OperationLogLevel level,
        string message,
        string? details,
        DateTime loggedAtUtc)
    {
        OrchestrationOperationId = OrchestrationOperationId;
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