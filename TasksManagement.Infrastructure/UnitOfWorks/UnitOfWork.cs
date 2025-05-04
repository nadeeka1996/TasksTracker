using TasksManagement.Application.Interfaces.Repositories;
using TasksManagement.Application.Interfaces.Repositories.Base;
using TasksManagement.Application.Interfaces.UnitOfWorks;
using TasksManagement.Domain.Entities.Base;
using TasksManagement.Infrastructure.Data;
using TasksManagement.Infrastructure.Repositories;
using TasksManagement.Infrastructure.Repositories.Base;

namespace TasksManagement.Infrastructure.UnitOfWorks;

internal sealed class UnitOfWork(
    ApplicationDbContext context
) : IUnitOfWork
{
    private readonly ApplicationDbContext _context = context;
    private readonly Dictionary<Type, object> _repositories = [];

    private ITaskItemHistoryRepository? _taskItemHistoryRepository;
    private ITaskItemRepository? _taskItemRepository;

    public ITaskItemHistoryRepository TaskItemHistoryRepository => 
        _taskItemHistoryRepository ??= new TaskItemHistoryRepository(_context);

    public ITaskItemRepository TaskItemRepository => 
        _taskItemRepository ??= new TaskItemRepository(_context);

    public IRepository<TEntity> Repository<TEntity>() where TEntity : class, IEntity
    {
        if (!_repositories.ContainsKey(typeof(TEntity)))
            _repositories[typeof(TEntity)] = new Repository<TEntity>(_context);

        return (IRepository<TEntity>)_repositories[typeof(TEntity)];
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) =>
        await _context.SaveChangesAsync(cancellationToken);
}