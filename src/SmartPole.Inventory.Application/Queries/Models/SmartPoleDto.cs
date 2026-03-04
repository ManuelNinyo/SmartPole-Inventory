namespace SmartPole.Inventory.Application.Queries.Models;

public record SmartPoleDto(
  Guid Id,
  double Latitude,
  double Longitude,
  string Type,
  string Status,
  object? GeoJson = null
);
