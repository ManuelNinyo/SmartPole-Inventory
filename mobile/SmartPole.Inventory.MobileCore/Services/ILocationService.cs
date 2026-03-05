using System;
using System.Threading.Tasks;

namespace SmartPole.Inventory.MobileCore.Services;

public interface ILocationService
{
    Task<(double Latitude, double Longitude)?> GetCurrentLocationAsync();
    Task<bool> CheckPermissionsAsync();
    Task<bool> RequestPermissionsAsync();
}
