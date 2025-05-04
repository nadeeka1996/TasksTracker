using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TasksManagement.Application.Interfaces.Repositories.Base;
using TasksManagement.Domain.Entities.Base;
using TasksManagement.Infrastructure.Data;

namespace TasksManagement.Infrastructure.Repositories.Base;

public class Repository<TEntity>(
    ApplicationDbContext context
) : IRepository<TEntity> where TEntity : class, IEntity
{
    private readonly ApplicationDbContext _context = context;

    public async Task<TEntity?> FindAsync(
        params object?[]? keyValues) =>
        await _context.Set<TEntity>().FindAsync(keyValues);

    public async Task<IEnumerable<TEntity>> GetAsync(
        IQueryable<TEntity>? query = null,
        Expression<Func<TEntity, bool>>? filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        string? includeProperties = null,
        int? skip = null,
        int? take = null,
        bool disableTracking = false,
        CancellationToken cancellationToken = default)
    {
        query = BuildQuery(
            filter,
            orderBy,
            includeProperties,
            query,
            skip,
            take,
            disableTracking
        );

        return await query.ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<TResult>> GetAsync<TResult>(
        Expression<Func<TEntity, TResult>> selector,
        IQueryable<TEntity>? query = null,
        Expression<Func<TEntity, bool>>? filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        string? includeProperties = null,
        int? skip = null,
        int? take = null,
        CancellationToken cancellationToken = default)
    {
        query = BuildQuery(
            filter,
            orderBy,
            includeProperties,
            query,
            skip,
            take,
            disableTracking: true
        );

        return await query.Select(selector).ToListAsync(cancellationToken);
    }

    public async Task<TEntity?> FirstOrDefaultAsync(
        IQueryable<TEntity>? query = null,
        Expression<Func<TEntity, bool>>? filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        string? includeProperties = null,
        bool disableTracking = false,
        CancellationToken cancellationToken = default)
    {
        query = BuildQuery(
            filter,
            orderBy,
            includeProperties,
            query,
            disableTracking: disableTracking
        );

        return await query.FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<TResult?> FirstOrDefaultAsync<TResult>(
        Expression<Func<TEntity, TResult>> selector,
        IQueryable<TEntity>? query = null,
        Expression<Func<TEntity, bool>>? filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        string? includeProperties = null,
        CancellationToken cancellationToken = default)
    {
        query = BuildQuery(
            filter,
            orderBy,
            includeProperties,
            query,
            disableTracking: true
        );

        return await query.Select(selector).FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<bool> AnyAsync(
        IQueryable<TEntity>? query = null,
        Expression<Func<TEntity, bool>>? filter = null,
        CancellationToken cancellationToken = default) =>
        await BuildQuery(
            query: query,
            filter: filter
        ).AnyAsync(cancellationToken);

    public async Task AddAsync(
        TEntity entity,
        CancellationToken cancellationToken = default) =>
        await _context.Set<TEntity>().AddAsync(entity, cancellationToken);

    public void Update(TEntity entity)
    {
        _context.Set<TEntity>().Attach(entity);
        _context.Entry(entity).State = EntityState.Modified;
    }

    public void Remove(TEntity entity) =>
        _context.Set<TEntity>().Remove(entity);

    private IQueryable<TEntity> BuildQuery(
        Expression<Func<TEntity, bool>>? filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        string? includeProperties = null,
        IQueryable<TEntity>? query = null,
        int? skip = null,
        int? take = null,
        bool disableTracking = false)
    {
        query ??= _context.Set<TEntity>();

        if (disableTracking)
            query = query.AsNoTracking();

        if (filter is not null)
            query = query.Where(filter);

        if (!string.IsNullOrWhiteSpace(includeProperties))
            query = includeProperties
                .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                .Aggregate(query, (current, includeProperty) =>
                    current.Include(includeProperty.Trim())
                );

        if (orderBy is not null)
            query = orderBy(query);

        if (skip.HasValue)
            query = query.Skip(skip.Value);

        if (take.HasValue)
            query = query.Take(take.Value);

        return query;
    }
}