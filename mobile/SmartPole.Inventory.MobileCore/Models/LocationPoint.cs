namespace SmartPole.Inventory.MobileCore.Models;

public class LocationPoint
{
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}
