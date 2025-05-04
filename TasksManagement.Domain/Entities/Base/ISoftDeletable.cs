namespace TasksManagement.Domain.Entities.Base;

public interface ISoftDeletable
{
    bool IsDeleted { get; }
}