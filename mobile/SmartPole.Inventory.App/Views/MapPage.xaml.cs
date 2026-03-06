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
  private Task? _initializeMapTask;

  public MapPage(MapViewModel viewModel)
  {
    InitializeComponent();
    BindingContext = _viewModel = viewModel;

    _initializeMapTask = InitializeMapAsync();
  }

  private async Task InitializeMapAsync()
  {
    try
    {
        mapView.Map = MapHelper.CreateMap();
        
        // Load MBTiles from bundled assets - ONLY if it's a valid size (at least 100 bytes for SQLite header)
        string mbTilesPath = Path.Combine(FileSystem.AppDataDirectory, "sample_map.mbtiles");
        bool mbTilesLoaded = false;

        try
        {
          if (!File.Exists(mbTilesPath) || new FileInfo(mbTilesPath).Length < 100)
          {
            using var stream = await FileSystem.OpenAppPackageFileAsync("sample_map.mbtiles");
            using var destStream = File.Create(mbTilesPath);
            await stream.CopyToAsync(destStream);
          }

          if (new FileInfo(mbTilesPath).Length > 100)
          {
            mapView.Map.Layers.Add(MapHelper.CreateMbTilesLayer(mbTilesPath));
            mbTilesLoaded = true;
          }
        }
        catch (Exception ex)
        {
          System.Diagnostics.Debug.WriteLine($"MBTiles loading failed: {ex.Message}");
        }

        if (!mbTilesLoaded)
        {
          mapView.Map.Layers.Add(MapHelper.CreateOsmLayer());
        }
        
        _viewModel.PropertyChanged += ViewModel_PropertyChanged;
        mapView.Info += MapView_Info;
    }
    catch (Exception ex)
    {
        System.Diagnostics.Debug.WriteLine($"Critical error in InitializeMapAsync: {ex}");
    }
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
    MainThread.BeginInvokeOnMainThread(() => 
    {
        if (e.PropertyName == nameof(MapViewModel.CurrentLocation) && _viewModel.CurrentLocation != null)
        {
          UpdateMapLocation();
        }
        else if (e.PropertyName == nameof(MapViewModel.Poles))
        {
          UpdatePolesLayer();
        }
    });
  }

  private void UpdateMapLocation()
  {
    if (_viewModel.CurrentLocation == null || mapView.Map == null) return;

    try
    {
        var layer = MapHelper.CreateLocationLayer(_viewModel.CurrentLocation);
        
        // Remove old location layer if exists
        var oldLayer = mapView.Map.Layers.FirstOrDefault(l => l.Name == "My Location");
        if (oldLayer != null) mapView.Map.Layers.Remove(oldLayer);

        mapView.Map.Layers.Add(layer);

        var point = SphericalMercator.FromLonLat(_viewModel.CurrentLocation.Longitude, _viewModel.CurrentLocation.Latitude);
        mapView.Map.Navigator.CenterOn(new MPoint(point.x, point.y));
        
        var resolutions = mapView.Map.Navigator.Resolutions;
        if (resolutions != null && resolutions.Count > 0)
        {
            if (resolutions.Count > 15)
                mapView.Map.Navigator.ZoomTo(resolutions[15]);
            else
                mapView.Map.Navigator.ZoomTo(resolutions.Last());
        }
    }
    catch (Exception ex)
    {
        System.Diagnostics.Debug.WriteLine($"Error in UpdateMapLocation: {ex.Message}");
    }
  }

  private void UpdatePolesLayer()
  {
    if (mapView.Map == null) return;

    try
    {
        var layer = MapHelper.CreatePinsLayer(_viewModel.Poles);
        
        var oldLayer = mapView.Map.Layers.FirstOrDefault(l => l.Name == "Poles");
        if (oldLayer != null) mapView.Map.Layers.Remove(oldLayer);

        mapView.Map.Layers.Add(layer);
    }
    catch (Exception ex)
    {
        System.Diagnostics.Debug.WriteLine($"Error in UpdatePolesLayer: {ex.Message}");
    }
  }

  protected override async void OnAppearing()
  {
    try
    {
        base.OnAppearing();
        
        if (_initializeMapTask != null)
        {
            await _initializeMapTask;
        }

        await _viewModel.LoadPolesAsync();
        await _viewModel.UpdateCurrentLocationAsync();
    }
    catch (Exception ex)
    {
        System.Diagnostics.Debug.WriteLine($"Error in OnAppearing: {ex.Message}");
    }
  }
}

