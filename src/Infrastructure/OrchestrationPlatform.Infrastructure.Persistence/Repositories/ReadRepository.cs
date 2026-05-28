using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using OrchestrationPlatform.Application.Abstractions.Models.Base;
using OrchestrationPlatform.Application.Abstractions.Persistence;
using OrchestrationPlatform.Domain.Common;
using OrchestrationPlatform.Infrastructure.Persistence.Contexts;
using OrchestrationPlatform.Infrastructure.Persistence.Extensions;

namespace OrchestrationPlatform.Infrastructure.Persistence.Repositories;

internal sealed class ReadRepository<TEntity>(
    OrchestrationReadDbContext orchestrationReadDbContext)
    : IReadRepository<TEntity>
    where TEntity : class, IEntity
{
    public async Task<TEntity?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default,
        Func<IQueryable<TEntity>, IQueryable<TEntity>>? includeAction = null)
    {
        return await orchestrationReadDbContext.Set<TEntity>()
            .AsNoTracking()
            .ApplySpecification(entity => entity.Id == id, null, false, includeAction)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<TEntity?> FirstOrDefaultAsync(
        Expression<Func<TEntity, bool>> predicate,
        CancellationToken cancellationToken = default,
        Func<IQueryable<TEntity>, IQueryable<TEntity>>? includeAction = null)
    {
        return await orchestrationReadDbContext.Set<TEntity>()
            .AsNoTracking()
            .ApplySpecification(predicate, null, false, includeAction)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<TEntity>> ListAsync(
        Expression<Func<TEntity, bool>>? predicate = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        int? skip = null,
        int? take = null,
        bool asSplitQuery = false,
        CancellationToken cancellationToken = default,
        Func<IQueryable<TEntity>, IQueryable<TEntity>>? includeAction = null)
    {
        return await orchestrationReadDbContext.Set<TEntity>()
            .AsNoTracking()
            .ApplySpecification(predicate, orderBy, asSplitQuery, includeAction)
            .ApplyPaging(skip, take)
            .ToListAsync(cancellationToken);
    }

    public async Task<PagedResult<TEntity>> PageAsync(
        int pageNumber,
        int pageSize,
        Expression<Func<TEntity, bool>>? predicate = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        bool asSplitQuery = false,
        CancellationToken cancellationToken = default,
        Func<IQueryable<TEntity>, IQueryable<TEntity>>? includeAction = null)
    {
        if (pageNumber < 1) pageNumber = 1;
        if (pageSize < 1) pageSize = 10;

        var query = orchestrationReadDbContext.Set<TEntity>()
            .AsNoTracking()
            .ApplySpecification(predicate, orderBy, asSplitQuery, includeAction);

        var totalCount = await query.CountAsync(cancellationToken);

        var items = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return new PagedResult<TEntity>(
            items,
            totalCount,
            pageNumber,
            pageSize);
    }

    public async Task<IReadOnlyList<TResult>> ListProjectedAsync<TResult>(
        Expression<Func<TEntity, TResult>> selector,
        Expression<Func<TEntity, bool>>? predicate = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        int? skip = null,
        int? take = null,
        CancellationToken cancellationToken = default)
    {
        return await orchestrationReadDbContext.Set<TEntity>()
            .AsNoTracking()
            .ApplySpecification(predicate, orderBy, false)
            .ApplyPaging(skip, take)
            .Select(selector)
            .ToListAsync(cancellationToken);
    }

    public async Task<int> CountAsync(
        Expression<Func<TEntity, bool>>? predicate = null,
        CancellationToken cancellationToken = default)
    {
        var query = orchestrationReadDbContext
            .Set<TEntity>()
            .AsNoTracking();

        if (predicate is not null) query = query.Where(predicate);

        return await query.CountAsync(cancellationToken);
    }

    public async Task<bool> ExistsAsync(
        Expression<Func<TEntity, bool>> predicate,
        CancellationToken cancellationToken = default)
    {
        return await orchestrationReadDbContext
            .Set<TEntity>()
            .AsNoTracking()
            .AnyAsync(predicate, cancellationToken);
    }
}