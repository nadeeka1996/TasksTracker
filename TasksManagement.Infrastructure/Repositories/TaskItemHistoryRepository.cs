using TasksManagement.Application.Interfaces.Repositories;
using TasksManagement.Domain.Entities;
using TasksManagement.Infrastructure.Data;
using TasksManagement.Infrastructure.Repositories.Base;

namespace TasksManagement.Infrastructure.Repositories;

internal sealed class TaskItemHistoryRepository(
    ApplicationDbContext context
) : Repository<TaskItemHistory>(context), ITaskItemHistoryRepository;

