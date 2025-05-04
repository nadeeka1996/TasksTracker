using TasksManagement.Application.Interfaces.Repositories;
using TasksManagement.Domain.Entities;
using TasksManagement.Infrastructure.Data;
using TasksManagement.Infrastructure.Repositories.Base;

namespace TasksManagement.Infrastructure.Repositories;

internal sealed class TaskItemRepository(
    ApplicationDbContext context
) : Repository<TaskItem>(context), ITaskItemRepository;
