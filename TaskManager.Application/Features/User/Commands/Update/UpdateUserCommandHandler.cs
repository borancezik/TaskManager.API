using MediatR;
using TaskManager.Application.Interfaces.Services;
using TaskManager.Application.Utilities.Errors.Base;
using TaskManager.Application.Utilities.Result;

namespace TaskManager.Application.Features.User.Commands.Update;

internal sealed class UpdateUserCommandHandler(IUserService userService) : IRequestHandler<UpdateUserCommand, Result<UpdateUserCommandResponse>>
{
    private readonly IUserService _userService = userService;
    public async Task<Result<UpdateUserCommandResponse>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var userDto = await _userService.FindByIdAsync(request.Id);

        if (!userDto.IsSuccess)
            return Error.NotFound;

        var user = userDto.Data;
        
        user.Name = request.Name;
        user.Email = request.Email;
        user.Username = request.Username;
        user.PasswordHash = request.PasswordHash;
        user.PasswordSalt = request.PasswordSalt;
        user.Iterations = request.Iterations;
        user.JoinedAt = request.JoinedAt;

        var updateduser = await _userService.UpdateAsync(user);

        if (!updateduser.IsSuccess)
            return Error.UpdatedError;

        return new UpdateUserCommandResponse();
    }
}
