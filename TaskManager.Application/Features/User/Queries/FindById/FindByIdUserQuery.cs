using MediatR;
using TaskManager.Application.Utilities.Result;

namespace TaskManager.Application.Features.User.Queries.FindById;

public sealed class FindByIdUserQuery : IRequest<Result<FindByIdUserQueryResponse>> 
{
    public Guid Id { get; set; }
}
