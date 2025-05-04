using TasksManagement.Domain.Entities;
using TasksManagement.Domain.Enums;

namespace TasksManagement.Application.Models.Responses;

public record TaskItemHistoryResponse(
    Guid Id,
    string Title,
    string Description,
    TaskItemStatus Status,
    DateTime ChangedAt,
    Guid ChangedBy
)
{
    public static TaskItemHistoryResponse Map(TaskItemHistory entity) =>
        new(
            entity.Id,
            entity.Title,
            entity.Description,
            entity.Status,
            entity.ChangedAt,
            entity.ChangedBy
        );
}