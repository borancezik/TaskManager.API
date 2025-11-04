using FluentValidation;
using Mapster;
using MapsterMapper;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using TaskManager.Application.Mapping.Mapster;
using TaskManager.Application.Utilities.AppSettings;
using TaskManager.Application.Utilities.Authorization.Session;
using TaskManager.Application.Utilities.Validation;

namespace TaskManager.Application;

public static class DependencyContainer
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<TaskManagerSettings>(options => configuration.GetSection("TaskManagerSettings"));

        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

        #region Mapster Configuration
        var typeAdapterConfig = TypeAdapterConfig.GlobalSettings;

        typeAdapterConfig.Scan(Assembly.GetAssembly(typeof(EntityToDtoAdapter))!);
        typeAdapterConfig.Scan(Assembly.GetAssembly(typeof(DtoToEntityAdapter))!);

        var mapperConfig = new Mapper(typeAdapterConfig);
        services.AddSingleton<IMapper>(mapperConfig);
        #endregion

        services.AddSession(options =>
        {
            options.IdleTimeout = TimeSpan.FromMinutes(20);
            options.Cookie.HttpOnly = true;
            options.Cookie.IsEssential = true;
        });

        services.AddScoped<SessionManager>();

        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        return services;
    }
}
