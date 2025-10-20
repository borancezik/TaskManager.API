using MediatR;
using TaskManager.Application.Interfaces.Helpers;
using TaskManager.Application.Interfaces.Services;
using TaskManager.Application.Utilities.Errors.Base;
using TaskManager.Application.Utilities.Errors.ServiceErrors;
using TaskManager.Application.Utilities.Result;

namespace TaskManager.Application.Features.Authentication.Commands.ChangePassword;

internal sealed class ChangePasswordCommandHandler(IUserService userService, ISessionService sessionService, IPasswordHashHelper passwordHashHelper) : IRequestHandler<ChangePasswordCommand, Result<ChangePasswordCommandResponse>>
{
    private readonly IUserService _userService = userService;
    private readonly ISessionService _sessionService = sessionService;
    private readonly IPasswordHashHelper _passwordHashHelper = passwordHashHelper;

    public async Task<Result<ChangePasswordCommandResponse>> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
    {
        var user = await _userService.FindByIdAsync(request.UserId);

        if (user.IsSuccess is false)
            return UserErrors.UserNotFound;

        var userDto = user.Data;

        if (_passwordHashHelper.VerifyPassword(request.CurrentPassword, userDto.PasswordHash, userDto.PasswordSalt, userDto.Iterations) is false)
            return UserErrors.UsernameOrPasswordIsIncorrect;

        if (request.CurrentPassword == request.NewPassword)
            return UserErrors.NewPasswordIsEqualToCurrentPassword;

        var (newPasswordHash, newPasswordSalt, iterations) = _passwordHashHelper.CreateHash(request.NewPassword);

        userDto.Iterations = iterations;
        userDto.PasswordHash = newPasswordHash;
        userDto.PasswordSalt = newPasswordSalt;

        var updatedUser = await _userService.UpdateAsync(userDto);

        if (updatedUser.IsSuccess is false)
            return Error.UpdatedError;

        var currentSessionList = await _sessionService.GetListAsync(x => x.UserId == userDto.Id);

        foreach (var currentSession in currentSessionList)
        {
            await _sessionService.DeleteByIdAsync(currentSession.Id.Value);
        }

        return new ChangePasswordCommandResponse();
    }
}
