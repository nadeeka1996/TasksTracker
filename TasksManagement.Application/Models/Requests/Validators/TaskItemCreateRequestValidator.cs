using FluentValidation;

namespace TasksManagement.Application.Models.Requests.Validators;
public class TaskItemCreateRequestValidator : AbstractValidator<TaskItemCreateRequest>
{
    public TaskItemCreateRequestValidator()
    {
        RuleFor(request => request.Title)
            .NotEmpty()
            .WithMessage("Title is required.")
            .MaximumLength(200)
            .WithMessage("Title must not exceed 200 characters.");

        RuleFor(request => request.Status)
            .IsInEnum()
            .WithMessage("Provided status is not valid.");

    }
}
