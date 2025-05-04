using TasksManagement.Application.Interfaces.Repositories.Base;
using TasksManagement.Domain.Entities;

namespace TasksManagement.Application.Interfaces.Repositories;

public interface ITaskItemRepository : IRepository<TaskItem>;
