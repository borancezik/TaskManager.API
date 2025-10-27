using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using TaskManager.Application.Utilities.AppSettings;
using TaskManager.Application.Utilities.Authorization.Helper;
using TaskManager.Application.Utilities.Authorization.Model;
using TaskManager.Application.Utilities.Authorization.Session;
using TaskManager.Application.Utilities.Constants;
using TaskManager.Application.Utilities.Exceptions;

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

        if (endpoint != null)
        {
            var hasAuhtIgnore = endpoint.Metadata.OfType<AllowAnonymousAttribute>().ToList();

            if (!hasAuhtIgnore.Any())
            {
                var checkAuthHeaders = httpContext.Request.Headers["Authorization"].ToString();

                if (string.IsNullOrWhiteSpace(checkAuthHeaders))
                    throw new UnauthorizedException(TokenConstant.NOT_FOUND_TOKEN);

                var token = httpContext.Request.Headers["Authorization"].ToString().Split(" ").Skip(1).First();

                if (string.IsNullOrWhiteSpace(token))
                    throw new UnauthorizedException(TokenConstant.NOT_FOUND_TOKEN);

                try
                {
                    var tokenHelper = new TokenHelper(_appSettings);

                    var tokenModel = tokenHelper.ValidateToken(token);

                    if (tokenModel is null)
                        throw new UnauthorizedException(TokenConstant.INVALID_TOKEN);

                    new SessionManager(httpContext)
                    {
                        LoginResult = new UserSessionModel
                        {
                            UserId = tokenModel.UserId,
                            Username = tokenModel.Username,
                            ValidTo = tokenModel.ValidTo,
                            RefreshTokenEndDate = tokenModel.RefreshTokenEndDate,
                            RefreshToken = tokenModel.RefreshToken
                        },
                        UserId = (int)tokenModel.UserId,
                        UserName = tokenModel.Username
                    };
                }
                catch (Exception)
                {
                    throw new UnauthorizedException(TokenConstant.NOT_FOUND_TOKEN);
                }
            }
        }

        await _next(httpContext);
    }
}

public static class CustomAuthorizationMiddlewareExtension
{
    public static IApplicationBuilder UseCustomAuthorizationMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<CustomAuthorizationMiddleware>();
    }
}
