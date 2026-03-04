using SmartPole.Inventory.Domain.Entities;
using NetTopologySuite.Geometries;
using FluentAssertions;
using Xunit;

namespace SmartPole.Inventory.UnitTests.Domain.Entities;

public class SmartPoleEntitiesTests {
  private readonly GeometryFactory _geometryFactory = new GeometryFactory(new PrecisionModel(), 3857);

  [Fact]
  public void SmartPole_ShouldBeCreatedWithLocation() {
    // Arrange
    var id = Guid.NewGuid();
    var location = _geometryFactory.CreatePoint(new Coordinate(-74.0060, 40.7128));
    var type = "Standard";
    var status = "Active";

    // Act
    var pole = new SmartPole.Inventory.Domain.Entities.SmartPole(id, location, type, status);

    // Assert
    pole.Id.Should().Be(id);
    pole.Location.Should().Be(location);
    pole.Location.SRID.Should().Be(3857);
    pole.Type.Should().Be(type);
    pole.Status.Should().Be(status);
  }

  [Fact]
  public void Inspection_ShouldBeLinkedToPole() {
    // Arrange
    var poleId = Guid.NewGuid();
    var inspectionId = Guid.NewGuid();
    var now = DateTime.UtcNow;

    // Act
    var inspection = new Inspection(inspectionId, poleId, now, "Normal");

    // Assert
    inspection.Id.Should().Be(inspectionId);
    inspection.SmartPoleId.Should().Be(poleId);
    inspection.Timestamp.Should().Be(now);
    inspection.Result.Should().Be("Normal");
  }

  [Fact]
  public void FraudFinding_ShouldBeLinkedToInspection() {
    // Arrange
    var inspectionId = Guid.NewGuid();
    var fraudId = Guid.NewGuid();

    // Act
    var fraud = new FraudFinding(fraudId, inspectionId, "Illegal Connection", "High");

    // Assert
    fraud.Id.Should().Be(fraudId);
    fraud.InspectionId.Should().Be(inspectionId);
    fraud.Description.Should().Be("Illegal Connection");
    fraud.Severity.Should().Be("High");
  }

  [Fact]
  public void TelcoOperator_ShouldBeCreatedWithDetails() {
    // Arrange
    var id = Guid.NewGuid();
    var name = "Operator A";

    // Act
    var op = new TelcoOperator(id, name, "Contact Info");

    // Assert
    op.Id.Should().Be(id);
    op.Name.Should().Be(name);
    op.ContactInfo.Should().Be("Contact Info");
  }
}
