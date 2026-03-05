using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using FluentAssertions;
using Mapsui.Layers;
using Mapsui.Tiling.Layers;
using SmartPole.Inventory.MobileCore.Helpers;
using SmartPole.Inventory.MobileCore.Models;
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

    [Fact]
    public void CreatePinsLayer_ShouldReturnMemoryLayerWithFeatures()
    {
        // Arrange
        var points = new List<LocationPoint>
        {
            new LocationPoint { Latitude = 10, Longitude = 20, Name = "P1" },
            new LocationPoint { Latitude = 11, Longitude = 21, Name = "P2" }
        };

        // Act
        var layer = MapHelper.CreatePinsLayer(points) as MemoryLayer;

        // Assert
        layer.Should().NotBeNull();
        layer!.Features.Should().HaveCount(2);
    }

    [Fact]
    public void CreateLocationLayer_ShouldReturnMemoryLayerWithOneFeature()
    {
        // Arrange
        var point = new LocationPoint { Latitude = 10, Longitude = 20, Name = "Me" };

        // Act
        var layer = MapHelper.CreateLocationLayer(point) as MemoryLayer;

        // Assert
        layer.Should().NotBeNull();
        layer!.Features.Should().HaveCount(1);
    }
}
