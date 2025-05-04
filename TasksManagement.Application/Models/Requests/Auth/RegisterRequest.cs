namespace TasksManagement.Application.Models.Requests.Auth;

public record RegisterRequest(
    string Name,
    string Email,
    string Password
);