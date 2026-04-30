using OrchestrationPlatform.Application.Abstractions.Persistence;
using OrchestrationPlatform.Domain.Common;
using OrchestrationPlatform.Infrastructure.Persistence.Contexts;

namespace OrchestrationPlatform.Infrastructure.Persistence.Repositories;

internal sealed class WriteRepository<TEntity>(
    OrchestrationWriteDbContext orchestrationWriteDbContext)
    : IWriteRepository<TEntity>
    where TEntity : class, IEntity
{
    public async Task AddAsync(
        TEntity entity,
        CancellationToken cancellationToken = default)
    {
        await orchestrationWriteDbContext
            .Set<TEntity>()
            .AddAsync(entity, cancellationToken);
    }

    public async Task AddRangeAsync(
        IEnumerable<TEntity> entities,
        CancellationToken cancellationToken = default)
    {
        await orchestrationWriteDbContext
            .Set<TEntity>()
            .AddRangeAsync(entities, cancellationToken);
    }

    public void Update(TEntity entity)
    {
        orchestrationWriteDbContext
            .Set<TEntity>()
            .Update(entity);
    }

    public void UpdateRange(IEnumerable<TEntity> entities)
    {
        orchestrationWriteDbContext
            .Set<TEntity>()
            .UpdateRange(entities);
    }

    public void HardDelete(TEntity entity)
    {
        orchestrationWriteDbContext
            .Set<TEntity>()
            .Remove(entity);
    }

    public void HardDeleteRange(IEnumerable<TEntity> entities)
    {
        orchestrationWriteDbContext
            .Set<TEntity>()
            .RemoveRange(entities);
    }

    public void SoftDelete(
        TEntity entity,
        DateTime deletedAtUtc)
    {
        if (entity is not ISoftDeletableEntity softDeletableEntity)
            throw new InvalidOperationException(
                $"{typeof(TEntity).Name} does not support soft delete.");

        softDeletableEntity.Delete(deletedAtUtc);

        orchestrationWriteDbContext
            .Set<TEntity>()
            .Update(entity);
    }

    public void SoftDeleteRange(
        IEnumerable<TEntity> entities,
        DateTime deletedAtUtc)
    {
        var entityList = entities.ToList();

        foreach (var entity in entityList)
        {
            if (entity is not ISoftDeletableEntity softDeletableEntity)
                throw new InvalidOperationException(
                    $"{typeof(TEntity).Name} does not support soft delete.");

            softDeletableEntity.Delete(deletedAtUtc);
        }

        orchestrationWriteDbContext
            .Set<TEntity>()
            .UpdateRange(entityList);
    }

    public void Restore(TEntity entity)
    {
        if (entity is not ISoftDeletableEntity softDeletableEntity)
            throw new InvalidOperationException(
                $"{typeof(TEntity).Name} does not support restore.");

        softDeletableEntity.Restore();

        orchestrationWriteDbContext
            .Set<TEntity>()
            .Update(entity);
    }

    public void RestoreRange(IEnumerable<TEntity> entities)
    {
        var entityList = entities.ToList();

        foreach (var entity in entityList)
        {
            if (entity is not ISoftDeletableEntity softDeletableEntity)
                throw new InvalidOperationException(
                    $"{typeof(TEntity).Name} does not support restore.");

            softDeletableEntity.Restore();
        }

        orchestrationWriteDbContext
            .Set<TEntity>()
            .UpdateRange(entityList);
    }
}