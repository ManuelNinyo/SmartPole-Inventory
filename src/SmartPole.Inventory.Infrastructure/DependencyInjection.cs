using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SmartPole.Inventory.Infrastructure.Persistence;
using SmartPole.Inventory.Application.Common.Interfaces;
using SmartPole.Inventory.Infrastructure.Identity;

namespace SmartPole.Inventory.Infrastructure;

public static class DependencyInjection {
  public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration) {
    var connectionString = configuration.GetConnectionString("DefaultConnection");

    services.AddDbContext<SmartPoleDbContext>(options =>
        options.UseNpgsql(connectionString, x => x.UseNetTopologySuite()));

    services.AddScoped<IJwtService, JwtService>();

    services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<SmartPoleDbContext>());

    return services;
  }
}
