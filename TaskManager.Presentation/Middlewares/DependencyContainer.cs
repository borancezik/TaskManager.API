namespace TaskManager.Presentation.Middlewares;

public static class DependencyContainer
{
    public static IServiceCollection AddMiddlewareServices(this IServiceCollection services)
    {
        services.AddExceptionHandler<CustomExceptionHandler>();
        services.AddScoped<CustomAuthorizationMiddleware>();
        return services;
    }
    public static IApplicationBuilder UseCustomAuthorizationMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<CustomAuthorizationMiddleware>();
    }
}
