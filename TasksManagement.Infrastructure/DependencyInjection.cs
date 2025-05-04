using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TasksManagement.Application.Interfaces.UnitOfWorks;
using TasksManagement.Infrastructure.Authentication;
using TasksManagement.Infrastructure.Data;
using TasksManagement.Infrastructure.UnitOfWorks;

namespace TasksManagement.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(
                configuration.GetConnectionString("DefaultConnection"))
        );

        services.AddMemoryCache();

        services.AddAuthentication()
            .AddAuthenticationSchemes();

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}