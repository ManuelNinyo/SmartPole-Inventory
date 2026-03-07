using System.Collections.Generic;
using System.Threading.Tasks;
using SmartPole.Inventory.MobileCore.Domain;
using SmartPole.Inventory.MobileCore.Models;

namespace SmartPole.Inventory.MobileCore.Persistence;

public interface ILocalDbService {
  Task InitAsync();
  Task<int> SavePosteAsync(LocalPoste pole);
  Task<List<LocalPoste>> GetPostesAsync();
  Task<int> SaveInspeccionAsync(LocalInspeccion inspection);
  Task<List<LocalInspeccion>> GetPendingInspeccionesAsync();
  Task<int> SaveFraudeAsync(LocalFraude fraud);

  // New methods
  Task<int> SaveInventoryItemAsync(InventoryItemModel item);
  Task<List<InventoryItemModel>> GetPendingInventoryItemsAsync();
}
