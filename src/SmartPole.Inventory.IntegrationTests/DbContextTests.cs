using Microsoft.EntityFrameworkCore;
using SmartPole.Inventory.Infrastructure.Persistence;
using FluentAssertions;
using Xunit;

namespace SmartPole.Inventory.IntegrationTests;

public class DbContextTests {
  [Fact]
  public void DbContext_ShouldLoadModelSuccessfully() {
    // Arrange
    var options = new DbContextOptionsBuilder<SmartPoleDbContext>()
      .UseInMemoryDatabase(databaseName: "TestDb")
      .Options;

    // Act
    using var context = new SmartPoleDbContext(options);
    var model = context.Model;

    // Assert
    model.Should().NotBeNull();
    model.FindEntityType(typeof(SmartPole.Inventory.Domain.Entities.SmartPole)).Should().NotBeNull();
    model.FindEntityType(typeof(SmartPole.Inventory.Domain.Entities.Inspection)).Should().NotBeNull();
    model.FindEntityType(typeof(SmartPole.Inventory.Domain.Entities.FraudFinding)).Should().NotBeNull();
    model.FindEntityType(typeof(SmartPole.Inventory.Domain.Entities.TelcoOperator)).Should().NotBeNull();
  }
}
