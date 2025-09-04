using MediatR;
using TaskManager.Application.Utilities.Result;

namespace TaskManager.Application.Features.Project.Queries.FindById;

public sealed class FindByIdProjectQuery : IRequest<Result<FindByIdProjectQueryResponse>>
{
    public Guid Id { get; set; }
}
