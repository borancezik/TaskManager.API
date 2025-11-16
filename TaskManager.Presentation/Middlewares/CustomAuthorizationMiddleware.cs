using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using TaskManager.Application.Interfaces.Helpers;
using TaskManager.Application.Utilities.Constants;
using TaskManager.Application.Utilities.Errors.ServiceErrors;
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

        var path = httpContext.Request.Path.Value;

        if (endpoint != null)
        {
            var hasAuthIgnore = endpoint.Metadata.OfType<AllowAnonymousAttribute>().ToList();

            if (!hasAuthIgnore.Any())
            {
                var token = httpContext.Request.Headers["Authorization"].ToString();

                if (string.IsNullOrWhiteSpace(token))
                    throw new UnauthorizedException(AuthenticationErrors.NotFoundToken.ToString());

                try
                {
                    var tokenModel = _tokenHelper.ValidateToken(token);

                    if (tokenModel is null)
                        throw new UnauthorizedException(AuthenticationErrors.InvalidToken.ToString());

                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.NameIdentifier, tokenModel.UserId.ToString()),
                        new Claim(ClaimTypes.Name, tokenModel.Username ?? string.Empty),
                        new Claim("ValidTo", tokenModel.ValidTo.ToString("O")),
                        new Claim("RefreshTokenEndDate", tokenModel.RefreshTokenEndDate.ToString("O")),
                        new Claim("RefreshToken", tokenModel.RefreshToken ?? string.Empty)
                    };

                    var identity = new ClaimsIdentity(claims, "CustomAuth");
                    var principal = new ClaimsPrincipal(identity);

                    httpContext.User = principal;
                }
                catch (Exception)
                {
                    throw new UnauthorizedException(AuthenticationErrors.NotFoundToken.ToString());
                }
            }
        }

        await next(httpContext);
    }
}

