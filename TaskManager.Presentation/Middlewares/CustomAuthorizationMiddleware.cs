using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using TaskManager.Application.Utilities.AppSettings;

namespace TaskManager.Presentation.Middlewares;

public class CustomAuthorizationMiddleware
{
    private readonly RequestDelegate _next;

    private readonly IOptions<TaskManagerSettings> _appSettings;

    public CustomAuthorizationMiddleware(IOptions<TaskManagerSettings> appSettings, RequestDelegate next)
    {
        _appSettings = appSettings;
        _next = next;
    }

    public async Task Invoke(HttpContext httpContext)
    {
        var endpoint = httpContext.GetEndpoint();

        var hasAuhtIgnore = endpoint.Metadata.OfType<AllowAnonymousAttribute>().ToList();

        if (!hasAuhtIgnore.Any())
        {

        }

        await _next(httpContext);
    }
}
