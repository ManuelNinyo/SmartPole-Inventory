using MediatR;
using Microsoft.AspNetCore.Mvc;
using SmartPole.Inventory.Application.Commands;

namespace SmartPole.Inventory.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase {
  private readonly IMediator _mediator;

  public AuthController(IMediator mediator) {
    _mediator = mediator;
  }

  [HttpPost("login")]
  public async Task<IActionResult> Login([FromBody] LoginCommand command) {
    try {
      var token = await _mediator.Send(command);
      return Ok(new { Token = token });
    } catch (UnauthorizedAccessException) {
      return Unauthorized();
    }
  }
}
