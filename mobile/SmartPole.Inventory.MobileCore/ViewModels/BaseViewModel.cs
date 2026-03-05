using CommunityToolkit.Mvvm.ComponentModel;

namespace SmartPole.Inventory.MobileCore.ViewModels;

public partial class BaseViewModel : ObservableObject {
  [ObservableProperty]
  [NotifyPropertyChangedFor(nameof(IsNotBusy))]
  private bool _isBusy;

  [ObservableProperty]
  private string? _title;

  public bool IsNotBusy => !IsBusy;
}
