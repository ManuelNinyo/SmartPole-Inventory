using System;
using System.IO;
using System.Linq;
using FluentAssertions;
using Mapsui.Layers;
using Mapsui.Tiling.Layers;
using SmartPole.Inventory.MobileCore.Helpers;
using Xunit;

namespace SmartPole.Inventory.UnitTests.Helpers;

public class MapHelperTests
{
    [Fact]
    public void CreateMap_ShouldReturnMapInstance()
    {
        // Act
        var map = MapHelper.CreateMap();

        // Assert
        map.Should().NotBeNull();
    }

    [Fact]
    public void CreateOsmLayer_ShouldReturnTileLayer()
    {
        // Act
        var layer = MapHelper.CreateOsmLayer();

        // Assert
        layer.Should().NotBeNull();
        layer.Should().BeOfType<TileLayer>();
    }

    [Fact]
    public void CreateMbTilesLayer_ShouldThrowFileNotFound_WhenPathIsInvalid()
    {
        // Arrange
        var invalidPath = "non_existent.mbtiles";

        // Act
        Action act = () => MapHelper.CreateMbTilesLayer(invalidPath);

        // Assert
        act.Should().Throw<FileNotFoundException>();
    }

    // Note: Testing actual MBTiles loading requires a valid .mbtiles file 
    // and potentially more complex mocking of the SQLite connection.
}
