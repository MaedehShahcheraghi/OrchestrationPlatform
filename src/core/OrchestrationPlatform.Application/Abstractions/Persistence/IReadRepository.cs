namespace OrchestrationPlatform.Application.Abstractions.Persistence;

public interface IReadRepository<TEntity> : IRepository<TEntity>
    where TEntity : class
{
    Task<TEntity?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default);

    Task<IReadOnlyList<TEntity>> ListAsync(
        CancellationToken cancellationToken = default);
}