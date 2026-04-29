using OrchestrationPlatform.Application.Abstractions.Persistence;
using OrchestrationPlatform.Infrastructure.Persistence.Contexts;

namespace OrchestrationPlatform.Infrastructure.Persistence.Repositories;

public class WriteRepository<TEntity>(OrchestrationWriteDbContext orchestrationWriteDbContext)
    : IWriteRepository<TEntity> where TEntity : class
{
    public async Task AddAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        await orchestrationWriteDbContext.Set<TEntity>().AddAsync(entity, cancellationToken);
    }

    public void Update(TEntity entity)
    {
        orchestrationWriteDbContext.Set<TEntity>().Update(entity);
    }

    public void Remove(TEntity entity)
    {
        orchestrationWriteDbContext.Set<TEntity>().Remove(entity);
    }
}