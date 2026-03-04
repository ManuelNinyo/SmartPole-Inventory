using SmartPole.Inventory.Domain.Entities;
using Xunit;

namespace SmartPole.Inventory.UnitTests.Domain.Entities;

public class EntityScaffoldingTests {
  [Fact]
  public void SmartPoleShouldBeCreatedWithCorrectProperties() {
    var id = Guid.NewGuid();
    var location = "Test Location";
    var type = "Smart";
    var status = "Active";

    var pole = new SmartPole.Inventory.Domain.Entities.SmartPole(id, location, type, status);

    Assert.Equal(id, pole.Id);
    Assert.Equal(location, pole.Location);
    Assert.Equal(type, pole.Type);
    Assert.Equal(status, pole.Status);
  }

  [Fact]
  public void MaintenanceRecordShouldBeCreatedWithCorrectProperties() {
    var id = Guid.NewGuid();
    var timestamp = DateTime.UtcNow;
    var technician = "Test Tech";
    var action = "Repair";

    var record = new MaintenanceRecord(id, timestamp, technician, action);

    Assert.Equal(id, record.Id);
    Assert.Equal(timestamp, record.Timestamp);
    Assert.Equal(technician, record.Technician);
    Assert.Equal(action, record.Action);
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
