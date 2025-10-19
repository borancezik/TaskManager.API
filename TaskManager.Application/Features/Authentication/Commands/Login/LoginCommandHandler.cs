using MediatR;
using TaskManager.Application.Interfaces.Helpers;
using TaskManager.Application.Interfaces.Services;
using TaskManager.Application.Utilities.Errors.ServiceErrors;
using TaskManager.Application.Utilities.Result;

namespace TaskManager.Application.Features.Authentication.Commands.Login;

internal sealed class LoginCommandHandler(IUserService userService, ITokenHelper tokenHelper, IPasswordHashHelper passwordHashHelper) : IRequestHandler<LoginCommand, Result<LoginCommandResponse>>
{
    private readonly IUserService _userService = userService;
    private readonly ITokenHelper _tokenHelper = tokenHelper;
    private readonly IPasswordHashHelper _passwordHashHelper = passwordHashHelper;

    public async Task<Result<LoginCommandResponse>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await _userService.GetByExpressionAsync(x => x.Username == request.Username);

        if (user.IsSuccess is false)
            return UserErrors.UserNotFound;

        if (_passwordHashHelper.VerifyPassword(request.Password, user.Data.PasswordHash, user.Data.PasswordSalt, user.Data.Iterations) is false)
            return UserErrors.UsernameOrPasswordIsIncorrect;

        var token = _tokenHelper.GenerateToken((Guid)user.Data.Id, user.Data.Username, user.Data.Email);

        return new LoginCommandResponse
        {
            AccessToken = token
        };
    }
}
