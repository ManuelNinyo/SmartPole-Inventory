using System.Threading.Tasks;
using FluentAssertions;
using NSubstitute;
using SmartPole.Inventory.MobileCore.Services;
using SmartPole.Inventory.MobileCore.ViewModels;
using Xunit;

namespace SmartPole.Inventory.UnitTests.ViewModels;

public class MapViewModelTests
{
    private readonly ILocationService _locationService;
    private readonly MapViewModel _viewModel;

    public MapViewModelTests()
    {
        _locationService = Substitute.For<ILocationService>();
        _viewModel = new MapViewModel(_locationService);
    }

    [Fact]
    public async Task UpdateCurrentLocationAsync_ShouldUpdateCurrentLocation_WhenPermissionGranted()
    {
        // Arrange
        _locationService.CheckPermissionsAsync().Returns(true);
        _locationService.GetCurrentLocationAsync().Returns((40.7128, -74.0060));

        // Act
        await _viewModel.UpdateCurrentLocationAsync();

        // Assert
        _viewModel.CurrentLocation.Should().NotBeNull();
        _viewModel.CurrentLocation!.Latitude.Should().Be(40.7128);
        _viewModel.CurrentLocation!.Longitude.Should().Be(-74.0060);
    }

    [Fact]
    public async Task UpdateCurrentLocationAsync_ShouldRequestPermission_WhenNotGranted()
    {
        // Arrange
        _locationService.CheckPermissionsAsync().Returns(false);
        _locationService.RequestPermissionsAsync().Returns(true);
        _locationService.GetCurrentLocationAsync().Returns((40.7128, -74.0060));

        // Act
        await _viewModel.UpdateCurrentLocationAsync();

        // Assert
        await _locationService.Received(1).RequestPermissionsAsync();
        _viewModel.CurrentLocation.Should().NotBeNull();
    }

    [Fact]
    public async Task UpdateCurrentLocationAsync_ShouldNotUpdate_WhenPermissionDenied()
    {
        // Arrange
        _locationService.CheckPermissionsAsync().Returns(false);
        _locationService.RequestPermissionsAsync().Returns(false);

        // Act
        await _viewModel.UpdateCurrentLocationAsync();

        // Assert
        _viewModel.CurrentLocation.Should().BeNull();
    }
}
