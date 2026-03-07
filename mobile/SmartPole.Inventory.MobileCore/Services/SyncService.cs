using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using SmartPole.Inventory.MobileCore.Models;
using SmartPole.Inventory.MobileCore.Persistence;

namespace SmartPole.Inventory.MobileCore.Services;

public class SyncService {
    private readonly ILocalDbService _dbService;
    private readonly HttpClient _httpClient;

    public SyncService(ILocalDbService dbService, HttpClient httpClient) {
        _dbService = dbService;
        _httpClient = httpClient;
    }

    public async Task SyncInventoryItemsAsync() {
        var pendingItems = await _dbService.GetPendingInventoryItemsAsync();
        if (!pendingItems.Any()) return;

        try {
            var response = await _httpClient.PostAsJsonAsync("api/sync/inventory", pendingItems);

            if (response.IsSuccessStatusCode) {
                foreach (var item in pendingItems) {
                    item.IsSynced = true;
                    await _dbService.SaveInventoryItemAsync(item);
                }
            }
        } catch (Exception ex) {
            Console.WriteLine($"Sync Error: {ex.Message}");
        }
    }
}
