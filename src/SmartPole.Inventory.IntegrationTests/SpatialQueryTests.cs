using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using NetTopologySuite.Geometries;
using SmartPole.Inventory.Application.Common.Interfaces;
using SmartPole.Inventory.Application.Queries.Models;
using Xunit;

namespace SmartPole.Inventory.IntegrationTests;

public class SpatialQueryTests : IClassFixture<WebApplicationFactory<Program>> {
  private readonly WebApplicationFactory<Program> _factory;
  private readonly GeometryFactory _geometryFactory = new GeometryFactory(new PrecisionModel(), 3857);

  public SpatialQueryTests(WebApplicationFactory<Program> factory) {
    _factory = factory;
  }

  [Fact]
  public async Task GetInZone_ReturnsPolesInArea() {
    // Arrange
    var client = _factory.CreateClient();
    
    // Generate token
    using var scope = _factory.Services.CreateScope();
    var jwtService = scope.ServiceProvider.GetRequiredService<IJwtService>();
    var token = jwtService.GenerateToken("1", "testuser", new[] { "Admin" });
    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

    // Act
    // Using a simple query string or body depending on implementation
    // The spec says "receiba un Bounding Box (polígono)"
    var response = await client.GetAsync("/api/smartpole/zona?w=0&s=0&e=10&n=10");

    // Assert
    // This will fail with 404 until implemented
    response.StatusCode.Should().Be(HttpStatusCode.OK);
  }
}
