using Microsoft.AspNetCore.Http;
using TaskManager.Application.Extensions;
using TaskManager.Application.Utilities.Authorization.Model;

namespace TaskManager.Application.Utilities.Authorization.Session;

public class SessionManager
{
    private readonly ISession _session;
    private readonly IHttpContextAccessor _httpContextAccessor;

    private readonly string _userInfoKey = "TaskManager.UserInfo";
    private readonly string _userIdKey = "TaskManager.Userd";
    private readonly string _userNameKey = "TaskManager.UserName";

    public SessionManager(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
        _session = httpContextAccessor?.HttpContext?.Session;
    }

    public SessionManager(HttpContext context)
    {
        _session = context?.Session;
    }

    public virtual UserSessionModel LoginResult
    {
        get
        {
            var _userInfo = _session.GetObjectFromJson<UserSessionModel>(_userInfoKey);
            if (_userInfo != null)
                return _userInfo;
            else
                return null;
        }
        set
        {
            _session.SetObjectAsJson(_userInfoKey, value);
        }
    }

    public int? UserId
    {
        get
        {
            var v = _session.GetInt32(_userIdKey);
            if (v.HasValue)
                return v.Value;
            else
                return null;
        }
        set
        {
            _session.SetInt32(_userIdKey, value.Value);
        }
    }

    public string UserName
    {
        get
        {
            return _session.GetString(_userNameKey);
        }
        set
        {
            _session.SetString(_userNameKey, value);
        }
    }
}
