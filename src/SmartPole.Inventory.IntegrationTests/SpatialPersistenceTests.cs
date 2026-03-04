using Microsoft.EntityFrameworkCore;
using SmartPole.Inventory.Infrastructure.Persistence;
using SmartPole.Inventory.Domain.Entities;
using NetTopologySuite.Geometries;
using FluentAssertions;
using Xunit;
using Microsoft.Extensions.Configuration;

namespace SmartPole.Inventory.IntegrationTests;

public class SpatialPersistenceTests {
  private readonly string _connectionString;

  public SpatialPersistenceTests() {
    var configuration = new ConfigurationBuilder()
      .SetBasePath(AppContext.BaseDirectory)
      .AddJsonFile("appsettings.json")
      .Build();

    _connectionString = configuration.GetConnectionString("DefaultConnection") 
      ?? "Host=localhost;Port=5432;Database=smartpole_db;Username=postgres;Password=password";
  }

  [Fact]
  public async Task ShouldPersistAndRetrieveSpatialPoint() {
    // Arrange
    var options = new DbContextOptionsBuilder<SmartPoleDbContext>()
      .UseNpgsql(_connectionString, x => x.UseNetTopologySuite())
      .Options;

    var geometryFactory = new GeometryFactory(new PrecisionModel(), 3857);
    var expectedLocation = geometryFactory.CreatePoint(new Coordinate(-74.0060, 40.7128));
    var poleId = Guid.NewGuid();
    var pole = new SmartPole.Inventory.Domain.Entities.SmartPole(poleId, expectedLocation, "Standard", "Active") {
      CreatedAt = DateTime.UtcNow
    };

    // Act
    using (var context = new SmartPoleDbContext(options)) {
      context.SmartPoles.Add(pole);
      await context.SaveChangesAsync();
    }

    // Assert
    using (var context = new SmartPoleDbContext(options)) {
      var retrievedPole = await context.SmartPoles.FindAsync(poleId);
      retrievedPole.Should().NotBeNull();
      retrievedPole!.Location.Should().NotBeNull();
      retrievedPole.Location.X.Should().BeApproximately(expectedLocation.X, 0.0001);
      retrievedPole.Location.Y.Should().BeApproximately(expectedLocation.Y, 0.0001);
      retrievedPole.Location.SRID.Should().Be(3857);
      
      // Cleanup
      context.SmartPoles.Remove(retrievedPole);
      await context.SaveChangesAsync();
    }
  }
}
