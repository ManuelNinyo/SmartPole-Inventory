using System;
using System.Collections.Generic;
using Mapsui;
using Mapsui.Layers;
using Mapsui.Styles;
using Mapsui.Tiling;
using Mapsui.Tiling.Layers;

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
}
