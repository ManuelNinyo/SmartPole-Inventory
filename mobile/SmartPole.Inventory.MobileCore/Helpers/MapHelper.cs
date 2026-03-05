using System.IO;
using Mapsui;
using Mapsui.Layers;
using Mapsui.Styles;
using Mapsui.Tiling;
using Mapsui.Tiling.Layers;
using Mapsui.Extensions;
using Mapsui.Providers;
using SQLite;
using BruTile.MbTiles;

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
}
