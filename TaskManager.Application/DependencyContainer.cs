using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using TaskManager.Application.Utilities.Validation;

namespace TaskManager.Application;

public static class DependencyContainer
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        return services;
    }
}
