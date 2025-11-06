using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TaskManager.Application.Interfaces.Helpers;
using TaskManager.Application.Interfaces.Persistence.Repositories;
using TaskManager.Application.Interfaces.Services;
using TaskManager.Application.Utilities.AppSettings;
using TaskManager.Infrastructure.Helper;
using TaskManager.Infrastructure.Persistence.Context;
using TaskManager.Infrastructure.Persistence.Repositories;
using TaskManager.Infrastructure.Services;

namespace TaskManager.Infrastructure;

public static class DependencyContainer
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<TaskManagerSettings>(configuration.GetSection("TaskManagerSettings"));

        services.AddDbContext<TaskManagerContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
        });

        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

        services.AddDistributedMemoryCache();
        services.AddHttpContextAccessor();

        services.AddScoped<ICurrentUserHelper, CurrentUserHelper>();
        services.AddSingleton<IPasswordHashHelper, PasswordHashHelper>();
        services.AddSingleton<ITokenHelper, TokenHelper>();

        services.AddScoped<ITaskRepository, TaskRepository>();
        services.AddScoped<IProjectRepository, ProjectRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ISessionRepository, SessionRepository>();

        services.AddScoped<ITaskService, TaskService>();
        services.AddScoped<IProjectService, ProjectService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<ISessionService, SessionService>();

        return services;
    }
}

