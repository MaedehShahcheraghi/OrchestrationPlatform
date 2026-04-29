namespace OrchestrationPlatform.Application.Abstractions.Persistence;

public interface IWriteRepository<TEntity> : IRepository<TEntity>
    where TEntity : class
{
    Task AddAsync(
        TEntity entity,
        CancellationToken cancellationToken = default);

    void Update(TEntity entity);

    void Remove(TEntity entity);
}