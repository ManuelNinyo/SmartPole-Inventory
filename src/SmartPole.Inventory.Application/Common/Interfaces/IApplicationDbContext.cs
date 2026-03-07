using Microsoft.EntityFrameworkCore;
using SmartPole.Inventory.Domain.Entities;

namespace SmartPole.Inventory.Application.Common.Interfaces;

public interface IApplicationDbContext {
  DbSet<SmartPole.Inventory.Domain.Entities.SmartPole> SmartPoles { get; }
  DbSet<InventoryItem> InventoryItems { get; }
  DbSet<Inspection> Inspections { get; }
  DbSet<FraudFinding> FraudFindings { get; }
  DbSet<TelcoOperator> TelcoOperators { get; }

  Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
