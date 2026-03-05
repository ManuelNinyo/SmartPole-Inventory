using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using SmartPole.Inventory.MobileCore.Models;
using SmartPole.Inventory.MobileCore.Services;
using SmartPole.Inventory.MobileCore.Persistence;

using CommunityToolkit.Mvvm.Input;

namespace SmartPole.Inventory.MobileCore.ViewModels;

public partial class MapViewModel : BaseViewModel
{
  private readonly ILocationService _locationService;
  private readonly ILocalDbService _localDbService;

  [ObservableProperty]
  private LocationPoint? _currentLocation;

  public ObservableCollection<LocationPoint> Poles { get; } = new();

  public MapViewModel(ILocationService locationService, ILocalDbService localDbService)
  {
    _locationService = locationService;
    _localDbService = localDbService;
    Title = "Map";
  }

  [RelayCommand]
  public async Task UpdateCurrentLocationAsync()
  {
    if (IsBusy) return;

    IsBusy = true;
    try
    {
      var hasPermission = await _locationService.CheckPermissionsAsync();
      if (!hasPermission)
      {
        hasPermission = await _locationService.RequestPermissionsAsync();
      }

      if (hasPermission)
      {
        var location = await _locationService.GetCurrentLocationAsync();
        if (location.HasValue)
        {
          CurrentLocation = new LocationPoint
          {
            Latitude = location.Value.Latitude,
            Longitude = location.Value.Longitude,
            Name = "You are here"
          };
        }
      }
    }
    finally
    {
      IsBusy = false;
    }
  }

  [RelayCommand]
  public async Task StartInspectionAsync(LocationPoint pole)
  {
    if (pole == null) return;
    
    // Navigation logic would go here, e.g., Shell.Current.GoToAsync
    // For now, we'll just log it
    System.Diagnostics.Debug.WriteLine($"Starting inspection for pole: {pole.Name}");
    
    await Task.CompletedTask;
  }

  [RelayCommand]
  public async Task LoadPolesAsync()
  {
    if (IsBusy) return;

    IsBusy = true;
    try
    {
      var localPoles = await _localDbService.GetPostesAsync();
      Poles.Clear();
      foreach (var pole in localPoles)
      {
        Poles.Add(new LocationPoint
        {
          Latitude = pole.Latitude,
          Longitude = pole.Longitude,
          Name = pole.Location,
          Description = $"{pole.Type} - {pole.Status}"
        });
      }
    }
    finally
    {
      IsBusy = false;
    }
  }
}

