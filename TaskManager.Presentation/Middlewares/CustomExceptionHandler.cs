using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Diagnostics;
using TaskManager.Application.Utilities.Exceptions;

namespace TaskManager.Presentation.Middlewares;

internal sealed class CustomExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        var (statusCode, responseMessage) = GetExceptionDetails(exception);

        httpContext.Response.ContentType = "application/json";
        httpContext.Response.StatusCode = (int)statusCode;

        var result = new { IsSuccess = false, Message = responseMessage };

        var jsonOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        await httpContext.Response.WriteAsync(
            JsonSerializer.Serialize(result, jsonOptions),
            cancellationToken);

        return true;
    }

    private (HttpStatusCode statusCode, string message) GetExceptionDetails(Exception exception)
    {
        return exception switch
        {
            NotFoundException notFoundEx => (HttpStatusCode.NotFound, notFoundEx.Message),
            UnauthorizedException unauthorizedEx => (HttpStatusCode.Unauthorized, unauthorizedEx.Message),
            LogException logEx => (HttpStatusCode.BadRequest, $"Log error occurred : {logEx.Message}"),
            _ => (HttpStatusCode.InternalServerError, "Beklenmeyen bir sunucu hatası oluştu.")
        };
    }
}
