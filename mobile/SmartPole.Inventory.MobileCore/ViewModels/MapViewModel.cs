using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using SmartPole.Inventory.MobileCore.Models;
using SmartPole.Inventory.MobileCore.Services;

namespace SmartPole.Inventory.MobileCore.ViewModels;

public partial class MapViewModel : BaseViewModel
{
    private readonly ILocationService _locationService;

    [ObservableProperty]
    private LocationPoint? _currentLocation;

    public ObservableCollection<LocationPoint> Poles { get; } = new();

    public MapViewModel(ILocationService locationService)
    {
        _locationService = locationService;
        Title = "Map";
    }

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
}
