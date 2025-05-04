using TasksManagement.Domain.Enums;

namespace TasksManagement.Application.Models.Requests;

public record TaskItemUpdateRequest(
    string Title, 
    string Description,
    TaskItemStatus Status
);