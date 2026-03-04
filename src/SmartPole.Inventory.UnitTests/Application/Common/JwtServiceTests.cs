using SmartPole.Inventory.Application.Common.Interfaces;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using NSubstitute;
using Xunit;
using System.Collections.Generic;
using System;

namespace SmartPole.Inventory.UnitTests.Application.Common;

public class JwtServiceTests {
  private readonly IConfiguration _configuration;

  public JwtServiceTests() {
    var settings = new Dictionary<string, string?> {
      {"Jwt:Key", "SuperSecretKeyForTestingThatIsLongEnough"},
      {"Jwt:Issuer", "TestIssuer"},
      {"Jwt:Audience", "TestAudience"},
      {"Jwt:DurationInMinutes", "60"}
    };

    _configuration = new ConfigurationBuilder()
      .AddInMemoryCollection(settings)
      .Build();
  }

  [Fact]
  public void ShouldGenerateValidToken() {
    // Arrange
    var jwtService = new SmartPole.Inventory.Infrastructure.Identity.JwtService(_configuration);
    var userId = Guid.NewGuid().ToString();
    var userName = "test-user";
    var roles = new[] { "Admin", "Technician" };

    // Act
    var token = jwtService.GenerateToken(userId, userName, roles);

    // Assert
    token.Should().NotBeNullOrEmpty();
  }
}
