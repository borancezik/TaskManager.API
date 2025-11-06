using MediatR;
using TaskManager.Application.Dtos.User;
using TaskManager.Application.Interfaces.Helpers;
using TaskManager.Application.Interfaces.Services;
using TaskManager.Application.Utilities.Errors.ServiceErrors;
using TaskManager.Application.Utilities.Result;

namespace TaskManager.Application.Features.Authentication.Commands.Register;

internal sealed class RegisterCommandHandler(IUserService userService, IPasswordHashHelper passwordHashHelper) : IRequestHandler<RegisterCommand, Result<RegisterCommandResponse>>
{
    private readonly IUserService _userService = userService;
    private readonly IPasswordHashHelper _passwordHashHelper = passwordHashHelper;
    public async Task<Result<RegisterCommandResponse>> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var checkUser = await _userService.GetByExpressionAsync(x => x.Email == request.Email || x.Username == request.Username);

        if (checkUser.IsSuccess)
            return UserErrors.SameUser;

        var (passwordHash, passwordSalt, iterations) = _passwordHashHelper.CreateHash(request.Password);

        var userDto = new UserDto
        {
            Name = request.Name,
            Username = request.Username,
            Email = request.Email,
            PasswordHash = passwordHash,
            PasswordSalt = passwordSalt,
            Iterations = iterations
        };

        var userAdd = await _userService.AddAsync(userDto);

        if (!userAdd.IsSuccess)
            return userAdd.Error;

        return new RegisterCommandResponse();
    }
}
