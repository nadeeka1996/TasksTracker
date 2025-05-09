using TasksManagement.Domain.Entities.Base;
using TasksManagement.Domain.Enums;

namespace TasksManagement.Domain.Entities;

public sealed class TaskItemHistory : IEntity, ISoftDeletable
{
    public Guid Id { get; private init; } = Guid.NewGuid();
    public Guid TaskItemId { get; private set; }
    public TaskItem TaskItem { get; private set; } = null!;
    public string Title { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public TaskItemStatus Status { get; private set; }
    public DateTime ChangedAt { get; private set; } = DateTime.UtcNow;
    public Guid ChangedBy { get; private set; }
    public bool IsDeleted { get; private set; }

    private TaskItemHistory() { }

    public static TaskItemHistory Create(
        Guid taskItemId, 
        TaskItem taskItem, 
        string title, 
        string description, 
        TaskItemStatus status, 
        Guid changedBy) =>
        new()
        {
            TaskItemId = taskItemId,
            TaskItem = taskItem,
            Title = title,
            Description = description,
            Status = status,
            ChangedBy = changedBy,
            IsDeleted = false
        };
}
