using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using OrchestrationPlatform.Application.Abstractions.Clock;
using OrchestrationPlatform.Domain.Common;

namespace OrchestrationPlatform.Infrastructure.Persistence.Interceptors;

internal sealed class AuditableEntitySaveChangesInterceptor(
    IDateTimeProvider dateTimeProvider) : SaveChangesInterceptor
{
    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        if (eventData.Context is null)
            return base.SavingChangesAsync(eventData, result, cancellationToken);

        var now = dateTimeProvider.UtcNow;

        foreach (var entry in eventData.Context.ChangeTracker.Entries<IAuditableEntity>())
        {
            if (entry.State == EntityState.Added) entry.Entity.SetCreatedAt(now);

            if (entry.State == EntityState.Modified) entry.Entity.SetModifiedAt(now);
        }

        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }
}