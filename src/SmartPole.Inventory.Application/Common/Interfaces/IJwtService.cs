namespace SmartPole.Inventory.Application.Common.Interfaces;

public interface IJwtService {
  string GenerateToken(string userId, string userName, IEnumerable<string> roles);
}
