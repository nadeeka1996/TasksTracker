namespace TasksManagement.Presentation;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddControllers();

        services
            .AddHttpContextAccessor()
            .AddProblemDetails()
            .AddExceptionHandler<GlobalExceptionHandler>()
            .AddCorsPolicies(configuration)
            .AddEndpointsApiExplorer()
            .AddSwaggerGen();

        return services;
    }

    private static IServiceCollection AddCorsPolicies(this IServiceCollection services, IConfiguration configuration)
    {
        string[] allowedOrigins = configuration.GetSection("AllowedOrigins").Get<string[]>() ?? [];
        services.AddCors(options =>
        {
            options.AddDefaultPolicy(policy =>
            {
                policy
                    .WithOrigins(allowedOrigins)
                    .AllowAnyHeader()
                    .AllowAnyMethod();
            });
        });

        return services;
    }
}