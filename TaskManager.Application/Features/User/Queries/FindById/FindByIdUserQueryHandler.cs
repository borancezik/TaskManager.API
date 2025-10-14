using Mapster;
using MediatR;
using TaskManager.Application.Interfaces.Services;
using TaskManager.Application.Utilities.Errors.Base;
using TaskManager.Application.Utilities.Result;

namespace TaskManager.Application.Features.User.Queries.FindById;

internal sealed class FindByIdUserQueryHandler(IUserService userService) : IRequestHandler<FindByIdUserQuery, Result<FindByIdUserQueryResponse>>
{
    private readonly IUserService _userService = userService;
    public async Task<Result<FindByIdUserQueryResponse>> Handle(FindByIdUserQuery request, CancellationToken cancellationToken)
    {
        var user = await _userService.FindByIdAsync(request.Id);

        if (!user.IsSuccess)
            return Error.NotFound;

        return user.Data.Adapt<FindByIdUserQueryResponse>();
    }
}
