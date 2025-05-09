using TasksManagement.Application.Models.Requests.Validators;

namespace TasksManagement.Application.Models.Requests.Auth;

public record RegisterRequest(
    string Name,
    string Email,
    string Password
)
{
    public Result Validate()
    {
        var validationResult = new RegisterRequestValidator().Validate(this);
        if (validationResult is { IsValid: true })
            return Result.Success();

        return Result.Failure(string.Join(", ", validationResult.Errors));
    }
}
