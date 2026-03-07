using SQLite;
using SmartPole.Inventory.MobileCore.Domain;
using SmartPole.Inventory.MobileCore.Models;

namespace SmartPole.Inventory.MobileCore.Persistence;

public class LocalDbService : ILocalDbService, IDisposable {
  private SQLiteAsyncConnection? _database;
  private readonly string _dbPath;

  public LocalDbService(string dbPath) {
    _dbPath = dbPath;
  }

  public async Task InitAsync() {
    if (_database is not null)
      return;

    _database = new SQLiteAsyncConnection(_dbPath);
    await _database.CreateTableAsync<LocalPoste>();
    await _database.CreateTableAsync<LocalInspeccion>();
    await _database.CreateTableAsync<LocalFraude>();
    await _database.CreateTableAsync<InventoryItemModel>();
  }

  public async Task<int> SavePosteAsync(LocalPoste pole) {
    await InitAsync();
    return await _database!.InsertOrReplaceAsync(pole);
  }

  public async Task<List<LocalPoste>> GetPostesAsync() {
    await InitAsync();
    return await _database!.Table<LocalPoste>().ToListAsync();
  }

  public async Task<int> SaveInspeccionAsync(LocalInspeccion inspection) {
    await InitAsync();
    if (inspection.LocalId != 0)
      return await _database!.UpdateAsync(inspection);
    else
      return await _database!.InsertAsync(inspection);
  }

  public async Task<List<LocalInspeccion>> GetPendingInspeccionesAsync() {
    await InitAsync();
    return await _database!.Table<LocalInspeccion>()
      .Where(i => i.SyncStatus == SyncStatus.Pending || i.SyncStatus == SyncStatus.New)
      .ToListAsync();
  }

  public async Task<int> SaveFraudeAsync(LocalFraude fraud) {
    await InitAsync();
    if (fraud.LocalId != 0)
      return await _database!.UpdateAsync(fraud);
    else
      return await _database!.InsertAsync(fraud);
  }

  public async Task<int> SaveInventoryItemAsync(InventoryItemModel item) {
    await InitAsync();
    return await _database!.InsertOrReplaceAsync(item);
  }

  public async Task<List<InventoryItemModel>> GetPendingInventoryItemsAsync() {
    await InitAsync();
    return await _database!.Table<InventoryItemModel>()
      .Where(i => !i.IsSynced)
      .ToListAsync();
  }

  public void Dispose() {
    _database?.CloseAsync().Wait();
  }
}
