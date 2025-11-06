using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using TaskManager.Application.Interfaces.Helpers;
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

        var path = httpContext.Request.Path.Value;

        if (endpoint != null)
        {
            var hasAuthIgnore = endpoint.Metadata.OfType<AllowAnonymousAttribute>().ToList();

            if (!hasAuthIgnore.Any())
            {
                var authHeader = httpContext.Request.Headers["Authorization"].ToString();

                if (string.IsNullOrWhiteSpace(authHeader))
                    throw new UnauthorizedException(TokenConstant.NOT_FOUND_TOKEN);

                //var token = authHeader.Split(" ").Skip(1).FirstOrDefault();

                //if (string.IsNullOrWhiteSpace(token))
                //    throw new UnauthorizedException(TokenConstant.NOT_FOUND_TOKEN);

                try
                {
                    var tokenModel = _tokenHelper.ValidateToken(authHeader);

                    if (tokenModel is null)
                        throw new UnauthorizedException(TokenConstant.INVALID_TOKEN);

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
                    throw new UnauthorizedException(TokenConstant.NOT_FOUND_TOKEN);
                }
            }
        }

        await next(httpContext);
    }
}

