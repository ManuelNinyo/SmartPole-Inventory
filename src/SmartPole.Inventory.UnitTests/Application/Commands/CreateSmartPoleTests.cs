using FluentAssertions;
using MediatR;
using SmartPole.Inventory.Application.Commands;
using SmartPole.Inventory.Domain.Entities;
using Xunit;

namespace SmartPole.Inventory.UnitTests.Application.Commands;

public class CreateSmartPoleTests {
  [Fact]
  public async Task ShouldReturnIdWhenSmartPoleIsCreated() {
    // Arrange
    var command = new CreateSmartPoleCommand("Location", "Type", "Status");
    var handler = new CreateSmartPoleCommandHandler();

    // Act
    var result = await handler.Handle(command, CancellationToken.None);

    // Assert
    result.Should().NotBeEmpty();
  }
}
