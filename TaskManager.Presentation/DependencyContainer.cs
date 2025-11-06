using TaskManager.Presentation.Middlewares;

namespace TaskManager.Presentation;

public static class DependencyContainer
{
    public static IServiceCollection AddMiddlewareServices(this IServiceCollection services)
    {
        services.AddExceptionHandler<CustomExceptionHandler>();
        services.AddTransient<CustomAuthorizationMiddleware>();
        return services;
    }
    public static IApplicationBuilder UseCustomAuthorizationMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<CustomAuthorizationMiddleware>();
    }
}
