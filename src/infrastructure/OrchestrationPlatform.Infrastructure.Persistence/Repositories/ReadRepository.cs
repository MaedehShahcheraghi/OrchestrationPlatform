using OrchestrationPlatform.Application.Abstractions.Persistence;
using OrchestrationPlatform.Infrastructure.Persistence.Contexts;

namespace OrchestrationPlatform.Infrastructure.Persistence.Repositories;

public class ReadRepository<TEntity>(OrchestrationReadDbContext orchestrationReadDbContext)
    : IReadRepository<TEntity> where TEntity : class
{
    public Task<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<IReadOnlyList<TEntity>> ListAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}