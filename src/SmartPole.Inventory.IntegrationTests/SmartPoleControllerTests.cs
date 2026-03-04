using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using SmartPole.Inventory.Application.Commands;
using Xunit;

namespace SmartPole.Inventory.IntegrationTests;

public class SmartPoleControllerTests : IClassFixture<WebApplicationFactory<Program>> {
  private readonly WebApplicationFactory<Program> _factory;

  public SmartPoleControllerTests(WebApplicationFactory<Program> factory) {
    _factory = factory;
  }

  [Fact]
  public async Task CreateSmartPole_ReturnsOkWithId() {
    // Arrange
    var client = _factory.CreateClient();
    var command = new CreateSmartPoleCommand("Location", "Type", "Status");

    // Act
    var response = await client.PostAsJsonAsync("/api/smartpole", command);

    // Assert
    Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    var result = await response.Content.ReadFromJsonAsync<Guid>();
    Assert.NotEqual(Guid.Empty, result);
  }
}
