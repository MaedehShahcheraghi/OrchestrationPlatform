using OrchestrationPlatform.Application.Abstractions.Clock;

namespace OrchestrationPlatform.Infrastructure.Persistence.Clock;

internal sealed class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}