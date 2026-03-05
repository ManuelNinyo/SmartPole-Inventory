using System.Collections.ObjectModel;
using SmartPole.Inventory.MobileCore.Domain;
using SmartPole.Inventory.MobileCore.Persistence;

namespace SmartPole.Inventory.App.ViewModels;

public partial class InspectionsViewModel : BaseViewModel {
  private readonly ILocalDbService _dbService;

  public ObservableCollection<LocalInspeccion> Inspections { get; } = new();

  public InspectionsViewModel(ILocalDbService dbService) {
    Title = "Inspections";
    _dbService = dbService;
  }

  public async Task LoadInspectionsAsync() {
    if (IsBusy) return;

    IsBusy = true;
    try {
      var list = await _dbService.GetPendingInspeccionesAsync();
      Inspections.Clear();
      foreach (var item in list) {
        Inspections.Add(item);
      }
    } finally {
      IsBusy = false;
    }
  }
}
