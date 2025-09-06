using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TaskManager.Application.Interfaces.Repositories;
using TaskManager.Application.Interfaces.Services;
using TaskManager.Infrastructure.Context;
using TaskManager.Infrastructure.Repositories;
using TaskManager.Infrastructure.Services;

namespace TaskManager.Infrastructure;

public static class DependencyContainer
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<TaskManagerContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
        });

        services.AddScoped<ITaskRepository, TaskRepository>();
        services.AddScoped<IProjectRepository, ProjectRepository>();
        services.AddScoped<ITeamMemberRepository, TeamMemberRepository>();

        services.AddScoped<ITaskService, TaskService>();
        services.AddScoped<IProjectService, ProjectService>();
        services.AddScoped<ITeamMemberService, TeamMemberService>();

        return services;
    }
}

