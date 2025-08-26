using FluentValidation;
using MediatR;
using TaskManager.Application.Utilities.Errors.Base;
using TaskManager.Domain.Enums;

namespace TaskManager.Application.Utilities.Validation;

public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
{
    private IValidator<TRequest> _validator;

    public ValidationBehavior(IValidator<TRequest> validator = null!)
    {
        _validator = validator;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (_validator is null)
        {
            return await next();
        }

        var validationResult = await _validator.ValidateAsync(request, cancellationToken);

        if (validationResult.IsValid)
        {
            return await next();
        }

        var errorKeys = validationResult.Errors.Where(x => x.ErrorCode != string.Empty).Select(x => x.ErrorCode).ToList();

        var errors = new List<Error>();

        foreach (var errorKey in errorKeys)
        {
            errors.Add(new Error(Key: errorKey, Type: ErrorType.VALIDATON_ERROR));
        }

        return (dynamic)errors;
    }
}
