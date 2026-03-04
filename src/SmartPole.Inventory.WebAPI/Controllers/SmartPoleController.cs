using MediatR;
using Microsoft.AspNetCore.Mvc;
using SmartPole.Inventory.Application.Commands;

namespace SmartPole.Inventory.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SmartPoleController : ControllerBase {
  private readonly IMediator _mediator;

  public SmartPoleController(IMediator mediator) {
    _mediator = mediator;
  }

  [HttpPost]
  public async Task<IActionResult> Create([FromBody] CreateSmartPoleCommand command) {
    var result = await _mediator.Send(command);
    return Ok(result);
  }
}
