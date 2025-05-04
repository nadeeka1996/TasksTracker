using TasksManagement.Application.Models;
using TasksManagement.Application.Models.Responses;

namespace TasksManagement.Application.Interfaces.Services;

public interface IUserService
{
    Task<Result<UserResponse>> AuthenticateAsync(string email, string password);
    Task<Result<UserResponse>> RegisterAsync(string name, string email, string password);
    Task<Result> IsValidIdAsync(Guid userId);
}
