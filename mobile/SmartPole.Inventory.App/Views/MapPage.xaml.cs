using System.IO;
using System.Threading.Tasks;
using Microsoft.Maui.Storage;
using SmartPole.Inventory.MobileCore.Helpers;
using SmartPole.Inventory.MobileCore.ViewModels;
using Mapsui.Projections;

namespace SmartPole.Inventory.App.Views;

public partial class MapPage : ContentPage
{
    private readonly MapViewModel _viewModel;

    public MapPage(MapViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = _viewModel = viewModel;

        InitializeMap();
    }

    private void InitializeMap()
    {
        mapView.Map = MapHelper.CreateMap();
        
        // Add OSM for testing, can be replaced by MBTiles
        mapView.Map.Layers.Add(MapHelper.CreateOsmLayer());
        
        _viewModel.PropertyChanged += ViewModel_PropertyChanged;

        mapView.Info += MapView_Info;
    }

    private void MapView_Info(object? sender, Mapsui.MapInfoEventArgs e)
    {
        if (e.MapInfo?.Feature != null)
        {
            var name = e.MapInfo.Feature["name"]?.ToString();
            var description = e.MapInfo.Feature["description"]?.ToString();

            if (!string.IsNullOrEmpty(description))
            {
                var pole = _viewModel.Poles.FirstOrDefault(p => p.Name == name);
                if (pole != null)
                {
                    _viewModel.StartInspectionCommand.Execute(pole);
                }
            }
        }
    }

    private async void ViewModel_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(MapViewModel.CurrentLocation) && _viewModel.CurrentLocation != null)
        {
            await UpdateMapLocation();
        }
        else if (e.PropertyName == nameof(MapViewModel.Poles))
        {
            UpdatePolesLayer();
        }
    }

    private async Task UpdateMapLocation()
    {
        if (_viewModel.CurrentLocation == null) return;

        var layer = MapHelper.CreateLocationLayer(_viewModel.CurrentLocation);
        
        // Remove old location layer if exists
        var oldLayer = mapView.Map.Layers.FirstOrDefault(l => l.Name == "My Location");
        if (oldLayer != null) mapView.Map.Layers.Remove(oldLayer);

        mapView.Map.Layers.Add(layer);

        var point = SphericalMercator.FromLonLat(_viewModel.CurrentLocation.Longitude, _viewModel.CurrentLocation.Latitude);
        mapView.Navigator.CenterOn(point);
        mapView.Navigator.ZoomTo(mapView.Map.Resolutions[15]);
    }

    private void UpdatePolesLayer()
    {
        var layer = MapHelper.CreatePinsLayer(_viewModel.Poles);
        
        var oldLayer = mapView.Map.Layers.FirstOrDefault(l => l.Name == "Poles");
        if (oldLayer != null) mapView.Map.Layers.Remove(oldLayer);

        mapView.Map.Layers.Add(layer);
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _viewModel.LoadPolesAsync();
        await _viewModel.UpdateCurrentLocationAsync();
    }
}
