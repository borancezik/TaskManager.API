using FluentValidation;
using TaskManager.Application.Utilities.Errors.ValidationErrors;

namespace TaskManager.Application.Features.Task.Commands.Update;

internal sealed class UpdateTaskCommandValidator : AbstractValidator<UpdateTaskCommand>
{
    public UpdateTaskCommandValidator()
    {
        RuleFor(x => x.Id)
          .NotEmpty()
          .WithErrorCode(TaskValidationError.IdRequired);

        RuleFor(x => x.ProjectId)
            .NotEmpty()
            .WithErrorCode(TaskValidationError.ProjectIdRequired);

        RuleFor(x => x.Title)
            .NotEmpty()
            .WithErrorCode(TaskValidationError.TitleRequired)
            .MaximumLength(200)
            .WithErrorCode(TaskValidationError.TitleTooLong);

        RuleFor(x => x.Description)
            .MaximumLength(1000)
            .When(x => !string.IsNullOrWhiteSpace(x.Description))
            .WithErrorCode(TaskValidationError.DescriptionTooLong);

        RuleFor(x => x.Status)
            .NotEmpty()
            .WithErrorCode(TaskValidationError.StatusRequired);

        RuleFor(x => x.DueDate)
            .GreaterThan(DateTime.UtcNow)
            .When(x => x.DueDate.HasValue)
            .WithErrorCode(TaskValidationError.DueDateInvalid);

        RuleFor(x => x.Priority)
            .InclusiveBetween(1, 5)
            .WithErrorCode(TaskValidationError.PriorityOutOfRange);
    }
}
