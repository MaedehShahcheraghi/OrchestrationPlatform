using System.Linq.Expressions;
using OrchestrationPlatform.Application.Abstractions.Models.Base;
using OrchestrationPlatform.Domain.Common;

namespace OrchestrationPlatform.Application.Abstractions.Persistence;

public interface IReadRepository<TEntity> : IRepository<TEntity>
    where TEntity : class, IEntity
{
    Task<TEntity?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default,
        Func<IQueryable<TEntity>, IQueryable<TEntity>>? includeAction = null);

    Task<TEntity?> FirstOrDefaultAsync(
        Expression<Func<TEntity, bool>> predicate,
        CancellationToken cancellationToken = default,
        Func<IQueryable<TEntity>, IQueryable<TEntity>>? includeAction = null);

    Task<IReadOnlyList<TEntity>> ListAsync(
        Expression<Func<TEntity, bool>>? predicate = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        int? skip = null,
        int? take = null,
        bool asSplitQuery = false,
        CancellationToken cancellationToken = default,
        Func<IQueryable<TEntity>, IQueryable<TEntity>>? includeAction = null);

    Task<PagedResult<TEntity>> PageAsync(
        int pageNumber,
        int pageSize,
        Expression<Func<TEntity, bool>>? predicate = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        bool asSplitQuery = false,
        CancellationToken cancellationToken = default,
        Func<IQueryable<TEntity>, IQueryable<TEntity>>? includeAction = null);

    Task<IReadOnlyList<TResult>> ListProjectedAsync<TResult>(
        Expression<Func<TEntity, TResult>> selector,
        Expression<Func<TEntity, bool>>? predicate = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        int? skip = null,
        int? take = null,
        CancellationToken cancellationToken = default);

    Task<int> CountAsync(
        Expression<Func<TEntity, bool>>? predicate = null,
        CancellationToken cancellationToken = default);

    Task<bool> ExistsAsync(
        Expression<Func<TEntity, bool>> predicate,
        CancellationToken cancellationToken = default);
}