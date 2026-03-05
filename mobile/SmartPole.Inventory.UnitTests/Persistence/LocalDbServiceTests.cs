using FluentAssertions;
using SmartPole.Inventory.MobileCore.Domain;
using SmartPole.Inventory.MobileCore.Persistence;
using Xunit;

namespace SmartPole.Inventory.UnitTests.Persistence;

public class LocalDbServiceTests {
  [Fact]
  public async Task InitAsync_ShouldCreateTables() {
    // Arrange
    var dbPath = Path.Combine(Path.GetTempPath(), $"{Guid.NewGuid()}.db3");
    using var service = new LocalDbService(dbPath);

    // Act
    await service.InitAsync();

    // Assert
    var result = await service.GetPostesAsync();
    result.Should().BeEmpty();

    // Cleanup attempt (may still fail if handle is not fully released)
    service.Dispose();
    try {
      if (File.Exists(dbPath)) File.Delete(dbPath);
    } catch {
      // Ignore cleanup failures in tests
    }
  }
}
