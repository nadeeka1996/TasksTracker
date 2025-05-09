using TasksManagement.Application.Models.Requests.Validators;

namespace TasksManagement.Application.Models.Requests.Auth;

public record LoginRequest(
    string Email,
    string Password
)
{
    public Result Validate()
    {
        var validationResult = new LoginRequestValidator().Validate(this);
        if (validationResult is { IsValid: true })
            return Result.Success();

        return Result.Failure(string.Join(", ", validationResult.Errors));
    }
}
