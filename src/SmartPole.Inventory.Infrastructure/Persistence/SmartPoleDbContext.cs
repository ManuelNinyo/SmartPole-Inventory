using Microsoft.EntityFrameworkCore;
using SmartPole.Inventory.Domain.Entities;

namespace SmartPole.Inventory.Infrastructure.Persistence;

public class SmartPoleDbContext : DbContext {
  public DbSet<SmartPole.Inventory.Domain.Entities.SmartPole> SmartPoles => Set<SmartPole.Inventory.Domain.Entities.SmartPole>();
  public DbSet<Inspection> Inspections => Set<Inspection>();
  public DbSet<FraudFinding> FraudFindings => Set<FraudFinding>();
  public DbSet<TelcoOperator> TelcoOperators => Set<TelcoOperator>();

  public SmartPoleDbContext(DbContextOptions<SmartPoleDbContext> options) : base(options) { }

  protected override void OnModelCreating(ModelBuilder modelBuilder) {
    modelBuilder.HasPostgresExtension("postgis");
    modelBuilder.ApplyConfigurationsFromAssembly(typeof(SmartPoleDbContext).Assembly);
    base.OnModelCreating(modelBuilder);
  }
}
