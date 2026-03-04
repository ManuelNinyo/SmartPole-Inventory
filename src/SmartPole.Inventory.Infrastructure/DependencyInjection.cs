using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SmartPole.Inventory.Infrastructure.Persistence;

namespace SmartPole.Inventory.Infrastructure;

public static class DependencyInjection {
  public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration) {
    var connectionString = configuration.GetConnectionString("DefaultConnection");

    services.AddDbContext<SmartPoleDbContext>(options =>
        options.UseNpgsql(connectionString, x => x.UseNetTopologySuite()));

    return services;
  }
}
