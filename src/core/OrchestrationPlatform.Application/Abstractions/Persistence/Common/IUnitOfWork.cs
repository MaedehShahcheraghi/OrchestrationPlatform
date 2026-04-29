using OrchestrationPlatform.Domain.Common;

namespace OrchestrationPlatform.Application.Abstractions.Persistence.Common;

public interface IUnitOfWork
{
    Task<int> SaveChangesAsync(
        CancellationToken cancellationToken = default);

    IReadRepository<TEntity> GetReadRepository<TEntity>()
        where TEntity : class, IEntity;

    IWriteRepository<TEntity> GetWriteRepository<TEntity>()
        where TEntity : class, IEntity;
}