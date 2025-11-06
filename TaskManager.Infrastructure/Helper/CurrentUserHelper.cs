using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using TaskManager.Application.Interfaces.Helpers;

namespace TaskManager.Infrastructure.Helper;

public class CurrentUserHelper(IHttpContextAccessor httpContextAccessor) : ICurrentUserHelper
{
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

    private ClaimsPrincipal User => _httpContextAccessor.HttpContext?.User;

    public Guid UserId
    {
        get
        {
            var claim = User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return Guid.TryParse(claim, out var userId) ? userId : Guid.Empty;
        }
        set {}
    }

    public string Username
    {
        get => User?.FindFirst(ClaimTypes.Name)?.Value;
        set {}
    }

    public DateTime ValidFrom
    {
        get
        {
            var claim = User?.FindFirst("ValidFrom")?.Value;
            return DateTime.TryParse(claim, out var date) ? date : DateTime.MinValue;
        }
        set {}
    }

    public DateTime ValidTo
    {
        get
        {
            var claim = User?.FindFirst("ValidTo")?.Value;
            return DateTime.TryParse(claim, out var date) ? date : DateTime.MinValue;
        }
        set {}
    }

    public DateTime RefreshTokenEndDate
    {
        get
        {
            var claim = User?.FindFirst("RefreshTokenEndDate")?.Value;
            return DateTime.TryParse(claim, out var date) ? date : DateTime.MinValue;
        }
        set {}
    }

    public string RefreshToken
    {
        get => User?.FindFirst("RefreshToken")?.Value;
        set {}
    }
}
