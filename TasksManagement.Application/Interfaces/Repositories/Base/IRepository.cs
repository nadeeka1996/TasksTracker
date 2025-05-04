using System.Linq.Expressions;
using TasksManagement.Domain.Entities.Base;

namespace TasksManagement.Application.Interfaces.Repositories.Base;

public interface IRepository<TEntity> where TEntity : class, IEntity
{
    Task<TEntity?> FindAsync(
        params object?[]? keyValues);

    Task<IEnumerable<TEntity>> GetAsync(
        IQueryable<TEntity>? query = null,
        Expression<Func<TEntity, bool>>? filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        string? includeProperties = null,
        int? skip = null,
        int? take = null,
        bool disableTracking = false,
        CancellationToken cancellationToken = default);

    Task<IEnumerable<TResult>> GetAsync<TResult>(
        Expression<Func<TEntity, TResult>> selector,
        IQueryable<TEntity>? query = null,
        Expression<Func<TEntity, bool>>? filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        string? includeProperties = null,
        int? skip = null,
        int? take = null,
        CancellationToken cancellationToken = default);

    Task<TEntity?> FirstOrDefaultAsync(
        IQueryable<TEntity>? query = null,
        Expression<Func<TEntity, bool>>? filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        string? includeProperties = null,
        bool disableTracking = false,
        CancellationToken cancellationToken = default);

    Task<TResult?> FirstOrDefaultAsync<TResult>(
        Expression<Func<TEntity, TResult>> selector,
        IQueryable<TEntity>? query = null,
        Expression<Func<TEntity, bool>>? filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        string? includeProperties = null,
        CancellationToken cancellationToken = default);

    Task<bool> AnyAsync(
       IQueryable<TEntity>? query = null,
       Expression<Func<TEntity, bool>>? filter = null,
       CancellationToken cancellationToken = default);

    Task AddAsync(TEntity entity, CancellationToken cancellationToken = default);

    void Update(TEntity entity);

    void Remove(TEntity entity);
}