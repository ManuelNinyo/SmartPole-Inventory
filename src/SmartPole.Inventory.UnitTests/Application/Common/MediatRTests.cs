using FluentAssertions;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using Xunit;

namespace SmartPole.Inventory.UnitTests.Application.Common;

public class MediatRTests {
  public record TestCommand : IRequest<string>;

  public class TestCommandHandler : IRequestHandler<TestCommand, string> {
    public Task<string> Handle(TestCommand request, CancellationToken cancellationToken) {
      return Task.FromResult("Handled");
    }
  }

  [Fact]
  public async Task ShouldDispatchAndHandleCommand() {
    // Arrange
    var services = new ServiceCollection();
    services.AddLogging();
    services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(MediatRTests).Assembly));
    var serviceProvider = services.BuildServiceProvider();
    var mediator = serviceProvider.GetRequiredService<IMediator>();

    // Act
    var result = await mediator.Send(new TestCommand());

    // Assert
    result.Should().Be("Handled");
  }
}
