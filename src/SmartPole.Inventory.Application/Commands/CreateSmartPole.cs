using MediatR;

namespace SmartPole.Inventory.Application.Commands;

public record CreateSmartPoleCommand(string Location, string Type, string Status) : IRequest<Guid>;

public class CreateSmartPoleCommandHandler : IRequestHandler<CreateSmartPoleCommand, Guid> {
  public Task<Guid> Handle(CreateSmartPoleCommand request, CancellationToken cancellationToken) {
    // Skeleton implementation
    return Task.FromResult(Guid.NewGuid());
  }
}
