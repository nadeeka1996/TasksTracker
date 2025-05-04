using TasksManagement.Domain.Enums;

namespace TasksManagement.Application.Models.Requests;

public record TaskItemCreateRequest(
    string Title, 
    string Description,
    TaskItemStatus Status
);