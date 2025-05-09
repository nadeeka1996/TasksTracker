using Microsoft.AspNetCore.Mvc;
using TasksManagement.Application.Interfaces.Services;
using TasksManagement.Application.Models.Requests.Auth;

namespace TasksManagement.Presentation.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController(IUserService userService) : ControllerBase
{
    private readonly IUserService _userService = userService;

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var modelState = request.Validate();
        if (!modelState.IsSuccess)
            return BadRequest(modelState);

        var result = await _userService.AuthenticateAsync(request.Email, request.Password);
        if (result.IsFailure)
            return Unauthorized(result);

        return Ok(result);
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
        var modelState = request.Validate();
        if (!modelState.IsSuccess)
            return BadRequest(modelState);

        var result = await _userService.RegisterAsync(request.Name, request.Email, request.Password);
        if (result.IsFailure)
            return BadRequest(result);

        return Ok(result);
    }
}
