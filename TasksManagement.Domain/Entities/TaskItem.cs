using TasksManagement.Domain.Entities.Base;
using TasksManagement.Domain.Enums;

namespace TasksManagement.Domain.Entities;

public sealed class TaskItem : IEntity, ISoftDeletable
{
    public Guid Id { get; private init; } = Guid.NewGuid();
    public string Title { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public TaskItemStatus Status { get; private set; }
    public DateTime Created { get; private set; } = DateTime.UtcNow;
    public DateTime Updated { get; private set; }
    public bool IsDeleted { get; private set; }
    public Guid UserId { get; private set; }
    public ICollection<TaskItemHistory> Histories { get; private set; } = [];

    private TaskItem() { }

    public static TaskItem Create(
        string title, 
        string description, 
        TaskItemStatus status, 
        Guid userId) =>
        new()
        {
            Title = title,
            Description = description,
            Status = status,
            IsDeleted = false,
            UserId = userId
        };

    public void Update(
        string title, 
        string description, 
        TaskItemStatus status
    )
    {
        Title = title;
        Description = description;
        Status = status;
        Updated = DateTime.UtcNow;
    }
}
