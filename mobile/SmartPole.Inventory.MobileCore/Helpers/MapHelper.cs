using System.IO;
using System.Collections.Generic;
using System.Linq;
using Mapsui;
using Mapsui.Layers;
using Mapsui.Styles;
using Mapsui.Tiling;
using Mapsui.Tiling.Layers;
using Mapsui.Extensions;
using Mapsui.Providers;
using SQLite;
using BruTile.MbTiles;
using SmartPole.Inventory.MobileCore.Models;
using Mapsui.Projections;

namespace SmartPole.Inventory.MobileCore.Helpers;

public static class MapHelper
{
  public static Map CreateMap()
  {
    var map = new Map();
    return map;
  }

  public static ILayer CreateOsmLayer()
  {
    return OpenStreetMap.CreateTileLayer();
  }

  public static ILayer CreateMbTilesLayer(string mbTilesPath, string layerName = "Offline Map")
  {
    if (!File.Exists(mbTilesPath))
    {
      throw new FileNotFoundException("MBTiles file not found", mbTilesPath);
    }

    var connection = new SQLiteConnectionString(mbTilesPath, true);
    var mbTilesTileSource = new MbTilesTileSource(connection);
    
    return new TileLayer(mbTilesTileSource)
    {
      Name = layerName
    };
  }

  public static ILayer CreatePinsLayer(IEnumerable<LocationPoint> points, string layerName = "Poles")
  {
    var features = points?.Select(p =>
    {
      var feature = new PointFeature(SphericalMercator.FromLonLat(p.Longitude, p.Latitude));
      feature["name"] = p.Name;
      feature["description"] = p.Description;
      return feature;
    }).ToList() ?? new List<PointFeature>();

    return new MemoryLayer
    {
      Name = layerName,
      Features = features,
      Style = new SymbolStyle
      {
        SymbolType = SymbolType.Ellipse,
        Fill = new Brush(Color.FromString("Red")),
        SymbolScale = 0.5
      }
    };
  }

  public static ILayer CreateLocationLayer(LocationPoint point, string layerName = "My Location")
  {
    if (point == null) return new MemoryLayer { Name = layerName };

    var feature = new PointFeature(SphericalMercator.FromLonLat(point.Longitude, point.Latitude));
    feature["name"] = point.Name;

    return new MemoryLayer
    {
      Name = layerName,
      Features = new[] { feature },
      Style = new SymbolStyle
      {
        SymbolType = SymbolType.Ellipse,
        Fill = new Brush(Color.FromString("Blue")),
        SymbolScale = 0.7,
        Outline = new Pen(Color.FromString("White"), 2)
      }
    };
  }
}

