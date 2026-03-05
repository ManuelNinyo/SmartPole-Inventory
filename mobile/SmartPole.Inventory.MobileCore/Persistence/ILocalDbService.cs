using SmartPole.Inventory.MobileCore.Domain;

namespace SmartPole.Inventory.MobileCore.Persistence;

public interface ILocalDbService {
  Task InitAsync();
  Task<int> SavePosteAsync(LocalPoste pole);
  Task<List<LocalPoste>> GetPostesAsync();
  Task<int> SaveInspeccionAsync(LocalInspeccion inspection);
  Task<List<LocalInspeccion>> GetPendingInspeccionesAsync();
  Task<int> SaveFraudeAsync(LocalFraude fraud);
}
