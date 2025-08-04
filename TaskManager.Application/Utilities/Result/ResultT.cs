using TaskManager.Application.Utilities.Errors.Base;

namespace TaskManager.Application.Utilities.Result;

public class Result<TData> : Result
{
    public TData Data { get; }

    public Result(TData data) : base()
    {
        Data = data;
    }

    public Result(Error error) : base(error: error)
    {
    }

    public Result(List<Error> errors) : base(errors: errors)
    {
    }

    public static implicit operator Result<TData>(TData data) => new(data);
    public static implicit operator Result<TData>(Error error) => new(error);
    public static implicit operator Result<TData>(List<Error> errors) => new(errors);
}
