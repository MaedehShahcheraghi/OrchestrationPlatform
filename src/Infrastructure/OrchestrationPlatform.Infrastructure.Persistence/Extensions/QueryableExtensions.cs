using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace OrchestrationPlatform.Infrastructure.Persistence.Extensions;

public static class QueryableExtensions
{
    public static IQueryable<TEntity> ApplySpecification<TEntity>(
        this IQueryable<TEntity> query,
        Expression<Func<TEntity, bool>>? predicate,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy,
        bool asSplitQuery,
        Func<IQueryable<TEntity>, IQueryable<TEntity>>? includeAction = null)
        where TEntity : class
    {
        if (asSplitQuery)
            query = query.AsSplitQuery();

        if (includeAction is not null)
            query = includeAction(query);

        if (predicate is not null)
            query = query.Where(predicate);

        if (orderBy is not null)
            query = orderBy(query);

        return query;
    }

    public static IQueryable<TEntity> ApplyPaging<TEntity>(
        this IQueryable<TEntity> query,
        int? skip,
        int? take)
        where TEntity : class
    {
        if (skip.HasValue) query = query.Skip(skip.Value);
        if (take.HasValue) query = query.Take(take.Value);

        return query;
    }
}