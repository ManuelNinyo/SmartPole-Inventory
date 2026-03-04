using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NetTopologySuite.Geometries;
using SmartPole.Inventory.Application.Commands;
using SmartPole.Inventory.Application.Queries;

namespace SmartPole.Inventory.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SmartPoleController : ControllerBase {
  private readonly IMediator _mediator;
  private readonly GeometryFactory _geometryFactory = new GeometryFactory(new PrecisionModel(), 3857);

  public SmartPoleController(IMediator mediator) {
    _mediator = mediator;
  }

  [HttpPost]
  public async Task<IActionResult> Create([FromBody] CreateSmartPoleCommand command) {
    var result = await _mediator.Send(command);
    return Ok(result);
  }

  [Authorize]
  [HttpGet("zona")]
  public async Task<IActionResult> GetInZone([FromQuery] double w, [FromQuery] double s, [FromQuery] double e, [FromQuery] double n) {
    var zone = _geometryFactory.CreatePolygon(new[] {
      new Coordinate(w, s),
      new Coordinate(e, s),
      new Coordinate(e, n),
      new Coordinate(w, n),
      new Coordinate(w, s)
    });

    var query = new GetPolesInZoneQuery(zone);
    var result = await _mediator.Send(query);
    return Ok(result);
  }
}
