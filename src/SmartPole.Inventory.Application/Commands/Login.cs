using MediatR;
using SmartPole.Inventory.Application.Common.Interfaces;

namespace SmartPole.Inventory.Application.Commands;

public record LoginCommand(string UserName, string Password) : IRequest<string>;

public class LoginCommandHandler : IRequestHandler<LoginCommand, string> {
  private readonly IJwtService _jwtService;

  public LoginCommandHandler(IJwtService jwtService) {
    _jwtService = jwtService;
  }

  public Task<string> Handle(LoginCommand request, CancellationToken cancellationToken) {
    // For now, implement a basic mock authentication
    // In a real scenario, you would verify against a database
    if (request.UserName == "testuser" && request.Password == "password123") {
      var roles = new[] { "Admin", "Technician" };
      var token = _jwtService.GenerateToken("1", request.UserName, roles);
      return Task.FromResult(token);
    }

    throw new UnauthorizedAccessException("Invalid credentials");
  }
}
