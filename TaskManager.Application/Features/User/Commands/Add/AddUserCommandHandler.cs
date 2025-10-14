using MediatR;
using TaskManager.Application.Dtos.User;
using TaskManager.Application.Interfaces.Services;
using TaskManager.Application.Utilities.Errors.Base;
using TaskManager.Application.Utilities.Result;

namespace TaskManager.Application.Features.User.Commands.Add;

internal sealed class AddUserCommandHandler(IUserService userService) : IRequestHandler<AddUserCommand, Result<AddUserCommandResponse>>
{
    private readonly IUserService _userService = userService;
    public async Task<Result<AddUserCommandResponse>> Handle(AddUserCommand request, CancellationToken cancellationToken)
    {
        var userDto = new UserDto
        {
            Name = request.Name,
            Email = request.Email,
            Role = request.Role,
            JoinedAt = request.JoinedAt
        };

        var addedUser = await _userService.AddAsync(userDto);

        if (!addedUser.IsSuccess)
            return Error.AddedError;

        return new AddUserCommandResponse
        {
            Id = addedUser.Data.Id.Value,
        };
    }
}
