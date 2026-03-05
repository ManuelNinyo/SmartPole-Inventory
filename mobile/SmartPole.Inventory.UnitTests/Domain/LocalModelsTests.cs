using FluentAssertions;
using SmartPole.Inventory.MobileCore.Domain;
using Xunit;

namespace SmartPole.Inventory.UnitTests.Domain;

public class LocalModelsTests {
  [Fact]
  public void LocalPoste_ShouldInitializeCorrectly() {
    var id = Guid.NewGuid();
    var poste = new LocalPoste {
      Id = id,
      Location = "Location",
      Type = "Type",
      Status = "Status",
      SyncStatus = SyncStatus.New
    };

    poste.Id.Should().Be(id);
    poste.SyncStatus.Should().Be(SyncStatus.New);
  }
}
