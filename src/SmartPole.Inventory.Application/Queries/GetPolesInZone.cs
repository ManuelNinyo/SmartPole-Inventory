using MediatR;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;
using SmartPole.Inventory.Application.Common.Interfaces;
using SmartPole.Inventory.Application.Queries.Models;

namespace SmartPole.Inventory.Application.Queries;

public record GetPolesInZoneQuery(Geometry Zone) : IRequest<IEnumerable<SmartPoleDto>>;

public class GetPolesInZoneQueryHandler : IRequestHandler<GetPolesInZoneQuery, IEnumerable<SmartPoleDto>> {
  private readonly IApplicationDbContext _context;

  public GetPolesInZoneQueryHandler(IApplicationDbContext context) {
    _context = context;
  }

  public async Task<IEnumerable<SmartPoleDto>> Handle(GetPolesInZoneQuery request, CancellationToken cancellationToken) {
    var poles = await _context.SmartPoles
      .Where(p => p.Location.Within(request.Zone))
      .Select(p => new SmartPoleDto(
        p.Id,
        p.Location.Y, // Latitude
        p.Location.X, // Longitude
        p.Type,
        p.Status,
        null // TODO: Map GeoJSON if needed
      ))
      .ToListAsync(cancellationToken);

    return poles;
  }
}
