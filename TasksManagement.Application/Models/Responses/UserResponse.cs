namespace TasksManagement.Application.Models.Responses;

public record UserResponse(
    Guid Id,
    string Name,
    string Email
);