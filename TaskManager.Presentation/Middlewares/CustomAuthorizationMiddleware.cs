using Microsoft.AspNetCore.Authorization;
using TaskManager.Application.Interfaces.Helpers;
using TaskManager.Application.Utilities.Authorization.Model;
using TaskManager.Application.Utilities.Authorization.Session;
using TaskManager.Application.Utilities.Constants;
using TaskManager.Application.Utilities.Exceptions;

namespace TaskManager.Presentation.Middlewares;

public class CustomAuthorizationMiddleware : IMiddleware
{
    private readonly ITokenHelper _tokenHelper;

    public CustomAuthorizationMiddleware(ITokenHelper tokenHelper)
    {
        _tokenHelper = tokenHelper;
    }

    public async Task InvokeAsync(HttpContext httpContext, RequestDelegate next)
    {
        var endpoint = httpContext.GetEndpoint();

        if (endpoint != null)
        {
            var hasAuthIgnore = endpoint.Metadata.OfType<AllowAnonymousAttribute>().ToList();

            if (!hasAuthIgnore.Any())
            {
                var authHeader = httpContext.Request.Headers["Authorization"].ToString();

                if (string.IsNullOrWhiteSpace(authHeader))
                    throw new UnauthorizedException(TokenConstant.NOT_FOUND_TOKEN);

                var token = authHeader.Split(" ").Skip(1).FirstOrDefault();

                if (string.IsNullOrWhiteSpace(token))
                    throw new UnauthorizedException(TokenConstant.NOT_FOUND_TOKEN);

                try
                {
                    var tokenModel = _tokenHelper.ValidateToken(token);

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

        await next(httpContext);
    }
}

