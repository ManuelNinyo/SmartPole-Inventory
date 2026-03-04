using SmartPole.Inventory.Domain.Entities;
using NetTopologySuite.Geometries;
using Xunit;

namespace SmartPole.Inventory.UnitTests.Domain.Entities;

public class EntityScaffoldingTests {
  private readonly GeometryFactory _geometryFactory = new GeometryFactory(new PrecisionModel(), 3857);

  [Fact]
  public void SmartPoleShouldBeCreatedWithCorrectProperties() {
    var id = Guid.NewGuid();
    var location = _geometryFactory.CreatePoint(new Coordinate(-74.0060, 40.7128));
    var type = "Smart";
    var status = "Active";

    var pole = new SmartPole.Inventory.Domain.Entities.SmartPole(id, location, type, status);

    Assert.Equal(id, pole.Id);
    Assert.Equal(location, pole.Location);
    Assert.Equal(type, pole.Type);
    Assert.Equal(status, pole.Status);
  }

  [Fact]
  public void UserShouldBeCreatedWithCorrectProperties() {
    var id = Guid.NewGuid();
    var name = "Test User";
    var role = "Admin";

    var user = new User(id, name, role);

    Assert.Equal(id, user.Id);
    Assert.Equal(name, user.Name);
    Assert.Equal(role, user.Role);
  }
}
