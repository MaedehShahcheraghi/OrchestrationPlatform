using System.Reflection;
using Microsoft.EntityFrameworkCore;
using OrchestrationPlatform.Domain.Common;

namespace OrchestrationPlatform.Infrastructure.Persistence.Extensions;

internal static class ModelBuilderExtensions
{
    private static readonly MethodInfo SetSoftDeleteQueryFilterMethod =
        typeof(ModelBuilderExtensions)
            .GetMethod(
                nameof(SetSoftDeleteQueryFilter),
                BindingFlags.NonPublic | BindingFlags.Static)!;

    public static ModelBuilder ApplySoftDeleteQueryFilters(this ModelBuilder modelBuilder)
    {
        var softDeletableEntityTypes = modelBuilder.Model
            .GetEntityTypes()
            .Where(entityType =>
                entityType.BaseType is null &&
                !entityType.IsOwned() &&
                typeof(ISoftDeletableEntity).IsAssignableFrom(entityType.ClrType))
            .Select(entityType => entityType.ClrType);

        foreach (var entityType in softDeletableEntityTypes)
            SetSoftDeleteQueryFilterMethod
                .MakeGenericMethod(entityType)
                .Invoke(null, [modelBuilder]);

        return modelBuilder;
    }

    private static void SetSoftDeleteQueryFilter<TEntity>(ModelBuilder modelBuilder)
        where TEntity : class, ISoftDeletableEntity
    {
        modelBuilder.Entity<TEntity>()
            .HasQueryFilter(entity => !entity.IsDeleted);

        modelBuilder.Entity<TEntity>()
            .HasIndex(entity => entity.IsDeleted);
    }
}