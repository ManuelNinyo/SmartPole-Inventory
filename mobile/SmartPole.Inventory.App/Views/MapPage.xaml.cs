using System.IO;
using System.Threading.Tasks;
using Microsoft.Maui.Storage;
using SmartPole.Inventory.MobileCore.Helpers;
using SmartPole.Inventory.MobileCore.ViewModels;
using Mapsui.Projections;
using Mapsui;
using Mapsui.Extensions;

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

  private async void InitializeMap()
  {
    mapView.Map = MapHelper.CreateMap();
    
    // Load MBTiles from bundled assets
    try
    {
      string mbTilesPath = Path.Combine(FileSystem.AppDataDirectory, "sample_map.mbtiles");
      if (!File.Exists(mbTilesPath))
      {
        using var stream = await FileSystem.OpenAppPackageFileAsync("sample_map.mbtiles");
        using var destStream = File.Create(mbTilesPath);
        await stream.CopyToAsync(destStream);
      }

      mapView.Map.Layers.Add(MapHelper.CreateMbTilesLayer(mbTilesPath));
    }
    catch (Exception ex)
    {
      // Fallback to OSM if MBTiles fails
      System.Diagnostics.Debug.WriteLine($"MBTiles loading failed: {ex.Message}. Falling back to OSM.");
      mapView.Map.Layers.Add(MapHelper.CreateOsmLayer());
    }
    
    _viewModel.PropertyChanged += ViewModel_PropertyChanged;

    mapView.Info += MapView_Info;
  }

  private void MapView_Info(object? sender, MapInfoEventArgs e)
  {
    var mapInfo = e.GetMapInfo(mapView.Map.Layers);
    if (mapInfo?.Feature != null)
    {
      var name = mapInfo.Feature["name"]?.ToString();
      var description = mapInfo.Feature["description"]?.ToString();

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

  private void ViewModel_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
  {
    if (e.PropertyName == nameof(MapViewModel.CurrentLocation) && _viewModel.CurrentLocation != null)
    {
      UpdateMapLocation();
    }
    else if (e.PropertyName == nameof(MapViewModel.Poles))
    {
      UpdatePolesLayer();
    }
  }

  private void UpdateMapLocation()
  {
    if (_viewModel.CurrentLocation == null) return;

    var layer = MapHelper.CreateLocationLayer(_viewModel.CurrentLocation);
    
    // Remove old location layer if exists
    var oldLayer = mapView.Map.Layers.FirstOrDefault(l => l.Name == "My Location");
    if (oldLayer != null) mapView.Map.Layers.Remove(oldLayer);

    mapView.Map.Layers.Add(layer);

    var point = SphericalMercator.FromLonLat(_viewModel.CurrentLocation.Longitude, _viewModel.CurrentLocation.Latitude);
    mapView.Map.Navigator.CenterOn(new MPoint(point.x, point.y));
    mapView.Map.Navigator.ZoomTo(mapView.Map.Navigator.Resolutions[15]);
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

