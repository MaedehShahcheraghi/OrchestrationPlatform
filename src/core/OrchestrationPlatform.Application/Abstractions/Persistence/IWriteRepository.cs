using System.Linq.Expressions;
using OrchestrationPlatform.Domain.Common;

namespace OrchestrationPlatform.Application.Abstractions.Persistence;

public interface IWriteRepository<TEntity> : IRepository<TEntity>
    where TEntity : class, IEntity
{
    Task AddAsync(
        TEntity entity,
        CancellationToken cancellationToken = default);

    Task AddRangeAsync(
        IEnumerable<TEntity> entities,
        CancellationToken cancellationToken = default);

    void Update(TEntity entity);

    void UpdateRange(IEnumerable<TEntity> entities);

    void HardDelete(TEntity entity);

    void HardDeleteRange(IEnumerable<TEntity> entities);

    void SoftDelete(
        TEntity entity,
        DateTime deletedAtUtc);

    void SoftDeleteRange(
        IEnumerable<TEntity> entities,
        DateTime deletedAtUtc);

    void Restore(TEntity entity);

    void RestoreRange(IEnumerable<TEntity> entities);

    public Task<TEntity?> GetForUpdateAsync(
        Guid id,
        Func<IQueryable<TEntity>, IQueryable<TEntity>>? includeAction = null);


    Task<TEntity?> FirstOrDefaultAsync(
        Expression<Func<TEntity, bool>> predicate,
        CancellationToken cancellationToken = default,
        Func<IQueryable<TEntity>, IQueryable<TEntity>>? includeAction = null);
}