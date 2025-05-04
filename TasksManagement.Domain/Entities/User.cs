using TasksManagement.Domain.Entities.Base;

namespace TasksManagement.Domain.Entities;

public sealed class User : IEntity
{
    public Guid Id { get; private init; } = Guid.NewGuid();
    public string Name { get; private set; } = string.Empty;
    public string Email { get; private set; } = string.Empty;
    public string PasswordHash { get; private set; } = string.Empty;
    public string PasswordSalt { get; private set; } = string.Empty;
    public ICollection<TaskItem> Tasks { get; private set; } = [];

    private User() { }

    public static User Create(
        string name, 
        string email,
        string hash, 
        string salt) =>
        new()
        {
            Name = name,
            Email = email,
            PasswordHash = hash,
            PasswordSalt = salt
        };

    public void Update(
        string name,
        string email,
        string hash,
        string salt
    )
    {
        Name = name;
        Email = email;
        PasswordHash = hash;
        PasswordSalt = salt;
    }
}
