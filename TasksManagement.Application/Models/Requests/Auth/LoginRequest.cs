namespace TasksManagement.Application.Models.Requests.Auth;

public record LoginRequest(
    string Email, 
    string Password
);
