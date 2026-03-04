using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SmartPole.Inventory.Application.Common.Interfaces;

namespace SmartPole.Inventory.Infrastructure.Identity;

public class JwtService : IJwtService {
  private readonly IConfiguration _configuration;

  public JwtService(IConfiguration configuration) {
    _configuration = configuration;
  }

  public string GenerateToken(string userId, string userName, IEnumerable<string> roles) {
    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

    var claims = new List<Claim> {
      new Claim(JwtRegisteredClaimNames.Sub, userId),
      new Claim(JwtRegisteredClaimNames.UniqueName, userName),
      new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
    };

    foreach (var role in roles) {
      claims.Add(new Claim(ClaimTypes.Role, role));
    }

    var token = new JwtSecurityToken(
      issuer: _configuration["Jwt:Issuer"],
      audience: _configuration["Jwt:Audience"],
      claims: claims,
      expires: DateTime.Now.AddMinutes(double.Parse(_configuration["Jwt:DurationInMinutes"]!)),
      signingCredentials: creds
    );

    return new JwtSecurityTokenHandler().WriteToken(token);
  }
}
