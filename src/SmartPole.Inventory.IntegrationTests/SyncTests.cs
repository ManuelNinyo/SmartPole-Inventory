using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using SmartPole.Inventory.Application.Common.Interfaces;
using SmartPole.Inventory.Application.Commands.Models;
using Xunit;

namespace SmartPole.Inventory.IntegrationTests;

public class SyncTests : IClassFixture<WebApplicationFactory<Program>> {
  private readonly WebApplicationFactory<Program> _factory;

  public SyncTests(WebApplicationFactory<Program> factory) {
    _factory = factory;
  }

  [Fact]
  public async Task Sync_WithValidBatch_ReturnsOk() {
    // Arrange
    var client = _factory.CreateClient();
    
    // Generate token with Technician role
    using var scope = _factory.Services.CreateScope();
    var jwtService = scope.ServiceProvider.GetRequiredService<IJwtService>();
    var context = scope.ServiceProvider.GetRequiredService<IApplicationDbContext>();
    
    var poleId = Guid.NewGuid();
    var geometryFactory = new NetTopologySuite.Geometries.GeometryFactory(new NetTopologySuite.Geometries.PrecisionModel(), 3857);
    var pole = new SmartPole.Inventory.Domain.Entities.SmartPole(poleId, geometryFactory.CreatePoint(new NetTopologySuite.Geometries.Coordinate(0, 0)), "Type", "Active") {
      CreatedAt = DateTime.UtcNow
    };
    context.SmartPoles.Add(pole);
    await context.SaveChangesAsync(CancellationToken.None);

    var token = jwtService.GenerateToken("1", "techuser", new[] { "Technician" });
    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

    var inspections = new List<InspectionDto> {
      new InspectionDto(Guid.NewGuid(), poleId, DateTime.UtcNow, "Normal", new List<FraudFindingDto>())
    };

    // Act
    var response = await client.PostAsJsonAsync("/api/sync", inspections);

    // Assert
    response.StatusCode.Should().Be(HttpStatusCode.OK);
    var result = await response.Content.ReadFromJsonAsync<SyncResultDto>();
    result.Should().NotBeNull();
    result!.Successful.Should().Be(1);
  }
}
