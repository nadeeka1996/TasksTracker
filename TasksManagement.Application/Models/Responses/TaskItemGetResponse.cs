using TasksManagement.Domain.Entities;
using TasksManagement.Domain.Enums;

namespace TasksManagement.Application.Models.Responses;

public record TaskItemGetResponse(
    Guid Id,
    string Title,
    string Description,
    TaskItemStatus Status,
    IEnumerable<TaskItemHistoryResponse> Histories
)
{
    public static TaskItemGetResponse Map(TaskItem entity) =>
        new(
            entity.Id,
            entity.Title,
            entity.Description,
            entity.Status,
            entity.Histories.Select(TaskItemHistoryResponse.Map)
        );
}