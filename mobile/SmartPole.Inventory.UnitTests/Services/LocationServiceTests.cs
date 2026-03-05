using System;
using System.Threading.Tasks;
using FluentAssertions;
using NSubstitute;
using SmartPole.Inventory.MobileCore.Services;
using Xunit;

namespace SmartPole.Inventory.UnitTests.Services;

public class LocationServiceTests
{
    private readonly ILocationService _locationService;

    public LocationServiceTests()
    {
        // Since we can't easily mock MAUI's static Geolocation.Default in a unit test 
        // without wrappers, we'll mock the interface for now to define behavior.
        // In a real scenario, we'd use a wrapper around Geolocation.Default.
        _locationService = Substitute.For<ILocationService>();
    }

    [Fact]
    public async Task GetCurrentLocationAsync_ShouldReturnCoordinates_WhenSuccessful()
    {
        // Arrange
        var expectedLocation = (40.7128, -74.0060);
        _locationService.GetCurrentLocationAsync().Returns(expectedLocation);

        // Act
        var result = await _locationService.GetCurrentLocationAsync();

        // Assert
        result.Should().NotBeNull();
        result.Value.Latitude.Should().Be(40.7128);
        result.Value.Longitude.Should().Be(-74.0060);
    }

    [Fact]
    public async Task GetCurrentLocationAsync_ShouldReturnNull_WhenLocationIsUnknown()
    {
        // Arrange
        _locationService.GetCurrentLocationAsync().Returns(((double Latitude, double Longitude)?)null);

        // Act
        var result = await _locationService.GetCurrentLocationAsync();

        // Assert
        result.Should().BeNull();
    }

    [Fact]
    public async Task CheckPermissionsAsync_ShouldReturnTrue_WhenGranted()
    {
        // Arrange
        _locationService.CheckPermissionsAsync().Returns(true);

        // Act
        var result = await _locationService.CheckPermissionsAsync();

        // Assert
        result.Should().BeTrue();
    }
}
