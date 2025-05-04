using Microsoft.Extensions.DependencyInjection;
using TasksManagement.Application.Common;
using TasksManagement.Application.Interfaces.Services;
using TasksManagement.Application.Services;

namespace TasksManagement.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services
            .AddScoped<ICurrentUser, CurrentUser>()
            .AddScoped<IUserService, UserService>()
            .AddScoped<ITaskItemService, TaskItemService>();

        return services;
    }
}