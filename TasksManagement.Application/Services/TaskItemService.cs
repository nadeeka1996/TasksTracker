using TasksManagement.Application.Common;
using TasksManagement.Application.Interfaces.Services;
using TasksManagement.Application.Interfaces.UnitOfWorks;
using TasksManagement.Application.Models;
using TasksManagement.Application.Models.Requests;
using TasksManagement.Application.Models.Responses;
using TasksManagement.Domain.Entities;
using TasksManagement.Domain.Enums;

namespace TasksManagement.Application.Services;

internal sealed class TaskItemService(
    IUnitOfWork unitOfWork, 
    ICurrentUser currentUser
) : ITaskItemService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ICurrentUser _currentUser = currentUser;

    public async Task<Result<IEnumerable<TaskItemGetResponse>>> GetAsync(int pageNumber)
    {
        var taskItems = await _unitOfWork.TaskItemRepository.GetAsync(
            filter: task => task.UserId == _currentUser.Id && task.Status != TaskItemStatus.Completed,
            selector: task => TaskItemGetResponse.Map(task),
            skip:((pageNumber -1)*5),
            take: 5
        );

        return Result<IEnumerable<TaskItemGetResponse>>.Success(taskItems);
    }

    public async Task<Result<TaskItemGetResponse>> GetAsync(Guid id)
    {
        var taskItem = await _unitOfWork.TaskItemRepository
            .FirstOrDefaultAsync(
                filter: t => t.Id == id && t.UserId == _currentUser.Id,
                includeProperties: "Histories",
                selector: task => TaskItemGetResponse.Map(task)
            );

        if (taskItem is null)
            return Result<TaskItemGetResponse>.Failure("Task item not found");

        return Result<TaskItemGetResponse>.Success(taskItem);
    }

    public async Task<Result<Guid>> CreateAsync(TaskItemCreateRequest request)
    {
        var taskItem = TaskItem.Create(request.Title, request.Description, request.Status, _currentUser.Id);
        await _unitOfWork.TaskItemRepository.AddAsync(taskItem);

        await _unitOfWork.SaveChangesAsync();
        return Result<Guid>.Success(taskItem.Id);
    }

    public async Task<Result> UpdateAsync(Guid id, TaskItemUpdateRequest request)
    {
        var taskItem = await _unitOfWork.TaskItemRepository.FindAsync(id);
        if (taskItem is null)
            return Result.Failure("Task item not found");

        var history = TaskItemHistory.Create(taskItem.Id, taskItem, taskItem.Title, taskItem.Description, taskItem.Status, _currentUser.Id);
        await _unitOfWork.TaskItemHistoryRepository.AddAsync(history);

        taskItem.Update(request.Title, request.Description, request.Status);

        await _unitOfWork.SaveChangesAsync();
        return Result.Success();
    }

    public async Task<Result> DeleteAsync(Guid id)
    {
        var taskItem = await _unitOfWork.TaskItemRepository.FindAsync(id);
        if (taskItem is null)
            return Result.Failure("Task item not found");

        var history = TaskItemHistory.Create(taskItem.Id, taskItem, taskItem.Title, taskItem.Description, TaskItemStatus.Deleted, _currentUser.Id);
        await _unitOfWork.TaskItemHistoryRepository.AddAsync(history);

        _unitOfWork.TaskItemRepository.Remove(taskItem);

        await _unitOfWork.SaveChangesAsync();
        return Result.Success();
    }
}
