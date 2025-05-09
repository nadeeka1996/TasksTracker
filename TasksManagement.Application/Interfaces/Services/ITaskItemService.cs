using TasksManagement.Application.Models;
using TasksManagement.Application.Models.Requests;
using TasksManagement.Application.Models.Responses;

namespace TasksManagement.Application.Interfaces.Services;

public interface ITaskItemService
{
    Task<Result<IEnumerable<TaskItemGetResponse>>> GetAsync(int pageNumber);
    Task<Result<TaskItemGetResponse>> GetAsync(Guid id);
    Task<Result<Guid>> CreateAsync(TaskItemCreateRequest request);
    Task<Result> UpdateAsync(Guid id, TaskItemUpdateRequest request);
    Task<Result> DeleteAsync(Guid id);
}
