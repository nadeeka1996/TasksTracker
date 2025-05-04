namespace TasksManagement.Application.Common;

public interface ICurrentUser
{
    Guid Id { get; }
}