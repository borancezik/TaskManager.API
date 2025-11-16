using MediatR;
using Microsoft.AspNetCore.Http;
using TaskManager.Application.Dtos.Session;
using TaskManager.Application.Interfaces.Helpers;
using TaskManager.Application.Interfaces.Services;
using TaskManager.Application.Utilities.Authorization.Model;
using TaskManager.Application.Utilities.Constants;
using TaskManager.Application.Utilities.Errors.ServiceErrors;
using TaskManager.Application.Utilities.Result;

namespace TaskManager.Application.Features.Authentication.Commands.Login;

internal sealed class LoginCommandHandler(IUserService userService,ISessionService sessionService, ITokenHelper tokenHelper, IPasswordHashHelper passwordHashHelper, IHttpContextAccessor httpContextAccessor) : IRequestHandler<LoginCommand, Result<LoginCommandResponse>>
{
    private readonly IUserService _userService = userService;
    private readonly ISessionService _sessionService = sessionService;
    private readonly ITokenHelper _tokenHelper = tokenHelper;
    private readonly IPasswordHashHelper _passwordHashHelper = passwordHashHelper;
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

    public async Task<Result<LoginCommandResponse>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await _userService.GetByExpressionAsync(x => x.Username == request.Username);

        if (user.IsSuccess is false)
            return UserErrors.UserNotFound;

        if (_passwordHashHelper.VerifyPassword(request.Password, user.Data.PasswordHash, user.Data.PasswordSalt, user.Data.Iterations) is false)
            return UserErrors.UsernameOrPasswordIsIncorrect;

        var newTokenModel = new TokenModel
        {
            UserId = (Guid)user.Data.Id,
            Username = user.Data.Username,
            Email = user.Data.Email,
            ValidTo = DateTime.Now.AddMinutes(TokenConstant.VALID_TO_END_MINUTE),
            RefreshTokenEndDate = DateTime.Now.AddDays(TokenConstant.REFRESH_TOKEN_END_MINUTE),
            RefreshToken = Guid.NewGuid().ToString()
        };

        var token = _tokenHelper.GenerateToken(newTokenModel);

        if (string.IsNullOrWhiteSpace(token))
            return AuthenticationErrors.TokenNotCreated;

        var session = new SessionDto
        {
            UserId = (Guid)user.Data.Id,
            AccessToken = token,
            RefreshToken = token,
            ExpiresAt = newTokenModel.ValidTo,
            IpAddress = _httpContextAccessor.HttpContext?.Connection?.RemoteIpAddress?.ToString()
        };

        var sessionAdd =  await _sessionService.AddAsync(session);

        return new LoginCommandResponse
        {
            AccessToken = token
        };
    }
}
