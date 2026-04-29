using OrchestrationPlatform.Application.Abstractions.Persistence;
using OrchestrationPlatform.Application.Abstractions.Persistence.Common;
using OrchestrationPlatform.Domain.Common;
using OrchestrationPlatform.Infrastructure.Persistence.Contexts;

namespace OrchestrationPlatform.Infrastructure.Persistence.Repositories.Common;

internal sealed class UnitOfWork(
    OrchestrationWriteDbContext writeDbContext,
    OrchestrationReadDbContext readDbContext) : IUnitOfWork
{
    private readonly Dictionary<Type, object> _readRepositories = [];
    private readonly Dictionary<Type, object> _writeRepositories = [];

    public IReadRepository<TEntity> GetReadRepository<TEntity>()
        where TEntity : class, IEntity
    {
        var entityType = typeof(TEntity);

        if (_readRepositories.TryGetValue(entityType, out var repository)) return (IReadRepository<TEntity>)repository;

        var newRepository = new ReadRepository<TEntity>(readDbContext);

        _readRepositories.Add(entityType, newRepository);

        return newRepository;
    }

    public IWriteRepository<TEntity> GetWriteRepository<TEntity>()
        where TEntity : class, IEntity
    {
        var entityType = typeof(TEntity);

        if (_writeRepositories.TryGetValue(entityType, out var repository))
            return (IWriteRepository<TEntity>)repository;

        var newRepository = new WriteRepository<TEntity>(writeDbContext);

        _writeRepositories.Add(entityType, newRepository);

        return newRepository;
    }

    public Task<int> SaveChangesAsync(
        CancellationToken cancellationToken = default)
    {
        return writeDbContext.SaveChangesAsync(cancellationToken);
    }
}