using TasksManagement.Application.Interfaces.Repositories;
using TasksManagement.Application.Interfaces.Repositories.Base;
using TasksManagement.Domain.Entities.Base;

namespace TasksManagement.Application.Interfaces.UnitOfWorks;

public interface IUnitOfWork
{
    IRepository<TEntity> Repository<TEntity>() where TEntity : class, IEntity;
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

    ITaskItemHistoryRepository TaskItemHistoryRepository { get; }
    ITaskItemRepository TaskItemRepository { get; }
}
