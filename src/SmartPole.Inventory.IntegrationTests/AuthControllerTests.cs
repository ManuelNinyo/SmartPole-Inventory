using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace SmartPole.Inventory.IntegrationTests;

public class AuthControllerTests : IClassFixture<WebApplicationFactory<Program>> {
  private readonly WebApplicationFactory<Program> _factory;

  public AuthControllerTests(WebApplicationFactory<Program> factory) {
    _factory = factory;
  }

  [Fact]
  public async Task Login_WithValidCredentials_ReturnsOkWithToken() {
    // Arrange
    var client = _factory.CreateClient();
    var loginRequest = new {
      UserName = "testuser",
      Password = "password123"
    };

    // Act
    var response = await client.PostAsJsonAsync("/api/auth/login", loginRequest);

    // Assert
    response.StatusCode.Should().Be(HttpStatusCode.OK);
    var result = await response.Content.ReadFromJsonAsync<LoginResponse>();
    result.Should().NotBeNull();
    result!.Token.Should().NotBeNullOrEmpty();
  }

  public record LoginResponse(string Token);
}
