using Microsoft.EntityFrameworkCore;
using TasksManagement.Domain.Entities;

namespace TasksManagement.Infrastructure.Data;

public class ApplicationDbContext(
    DbContextOptions<ApplicationDbContext> options
) : DbContext(options)
{
    public DbSet<TaskItem> TaskItems { get; set; }
    public DbSet<TaskItemHistory> TaskItemHistories { get; set; }
    public DbSet<User> Users { get; set; }
}
