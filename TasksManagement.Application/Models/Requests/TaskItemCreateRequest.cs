using TasksManagement.Application.Models.Requests.Validators;
using TasksManagement.Domain.Enums;

namespace TasksManagement.Application.Models.Requests;

public record TaskItemCreateRequest(
    string Title,
    string Description,
    TaskItemStatus Status
)
{
    public Result Validate()
    {       
        var validationResult = new TaskItemCreateRequestValidator().Validate(this);
        if (validationResult is { IsValid:true}) 
            return Result.Success();

        return Result.Failure(string.Join(", ", validationResult.Errors));
    }
}
