using SmartPole.Inventory.Domain.Common;
using FluentAssertions;
using Xunit;

namespace SmartPole.Inventory.UnitTests.Domain.Common;

public class AuditableEntityTests {
  private class TestAuditableEntity : AuditableEntity<Guid> {
    public TestAuditableEntity(Guid id) : base(id) { }
  }

  [Fact]
  public void ShouldHaveAuditProperties() {
    // Arrange
    var id = Guid.NewGuid();
    var entity = new TestAuditableEntity(id);
    var now = DateTime.UtcNow;
    var user = "test-user";

    // Act
    entity.CreatedAt = now;
    entity.CreatedBy = user;
    entity.UpdatedAt = now;
    entity.UpdatedBy = user;

    // Assert
    entity.Id.Should().Be(id);
    entity.CreatedAt.Should().Be(now);
    entity.CreatedBy.Should().Be(user);
    entity.UpdatedAt.Should().Be(now);
    entity.UpdatedBy.Should().Be(user);
  }
}
