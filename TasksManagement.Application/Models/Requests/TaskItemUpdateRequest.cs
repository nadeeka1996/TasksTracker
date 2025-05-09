using TasksManagement.Application.Models.Requests.Validators;
using TasksManagement.Domain.Enums;

namespace TasksManagement.Application.Models.Requests;

public record TaskItemUpdateRequest(
    string Title,
    string Description,
    TaskItemStatus Status
)
{
    public Result Validate()
    {
        var validationResult = new TaskItemUpdateRequestValidator().Validate(this);
        if (validationResult is { IsValid: true })
            return Result.Success();

        return Result.Failure(string.Join(", ", validationResult.Errors));
    }
}
