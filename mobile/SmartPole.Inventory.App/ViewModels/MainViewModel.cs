using CommunityToolkit.Mvvm.Input;
using SmartPole.Inventory.MobileCore.ViewModels;

namespace SmartPole.Inventory.App.ViewModels;

public partial class MainViewModel : BaseViewModel {
  public MainViewModel() {
    Title = "Smart Pole Inventory";
  }

  [RelayCommand]
  private async Task NavigateToInspections() {
    // Placeholder for navigation
    await Shell.Current.DisplayAlert("Info", "Navigation to Inspections", "OK");
  }
}
