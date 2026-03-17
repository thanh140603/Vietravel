using Microsoft.AspNetCore.Mvc;
using Vietravel.Tours.Application.Abstractions;

namespace Vietravel.Tours.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class AuthController(IAuthService authService) : ControllerBase
{
    [HttpPost("register")]
    public async Task<ActionResult> Register([FromBody] RegisterRequest request, CancellationToken cancellationToken)
    {
        try
        {
            await authService.RegisterAsync(request.Username, request.Password, cancellationToken);
            return NoContent();
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(new { message = ex.Message });
        }
    }

    [HttpPost("login")]
    public async Task<ActionResult<object>> Login([FromBody] LoginRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var token = await authService.LoginAsync(request.Username, request.Password, cancellationToken);
            return Ok(new { access_token = token });
        }
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized(new { message = ex.Message });
        }
    }
}

public sealed record RegisterRequest(string Username, string Password);
public sealed record LoginRequest(string Username, string Password);

