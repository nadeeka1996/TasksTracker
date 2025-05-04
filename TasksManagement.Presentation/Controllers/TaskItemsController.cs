using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TasksManagement.Application.Interfaces.Services;
using TasksManagement.Application.Models.Requests;
using TasksManagement.Application.Models.Responses;

namespace TasksManagement.Presentation.Controllers;

[ApiController]
[Route("api/tasks")]
[Authorize]
public class TaskItemsController(ITaskItemService service) : ControllerBase
{
    private readonly ITaskItemService _service = service;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TaskItemGetResponse>>> GetAll()
    {
        var result = await _service.GetAsync();
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<TaskItemGetResponse>> GetById(Guid id)
    {
        var result = await _service.GetAsync(id);
        if (result.IsFailure) 
            return NotFound(result);

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create(TaskItemCreateRequest request)
    {
        var result = await _service.CreateAsync(request);
        if (result.IsFailure) 
            return BadRequest(result);

        return CreatedAtAction(nameof(GetById), new { id = result.Value }, null);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, TaskItemUpdateRequest request)
    {
        var result = await _service.UpdateAsync(id, request);
        if (result.IsFailure) 
            return NotFound(result);

        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await _service.DeleteAsync(id);
        if (result.IsFailure) 
            return NotFound(result);

        return NoContent();
    }
}
