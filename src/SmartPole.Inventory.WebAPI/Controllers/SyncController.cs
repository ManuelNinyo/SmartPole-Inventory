using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartPole.Inventory.Application.Commands;
using SmartPole.Inventory.Application.Commands.Models;

namespace SmartPole.Inventory.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SyncController : ControllerBase {
  private readonly IMediator _mediator;

  public SyncController(IMediator mediator) {
    _mediator = mediator;
  }

  [Authorize(Roles = "Technician")]
  [HttpPost]
  public async Task<IActionResult> Sync([FromBody] IEnumerable<InspectionDto> inspections) {
    var command = new SyncInspectionsCommand(inspections);
    var result = await _mediator.Send(command);
    return Ok(result);
  }

  [Authorize]
  [HttpPost("inventory")]
  public async Task<IActionResult> SyncInventory([FromBody] List<SyncInventoryItemDto> items) {
    if (items == null || !items.Any()) return BadRequest("No items provided");

    var command = new SyncInventoryCommand { Items = items };
    var result = await _mediator.Send(command);
    return Ok(result);
  }
}
