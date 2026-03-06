namespace SmartPole.Inventory.Domain.ML;

public class YoloDetectionResult
{
    public string Label { get; set; } = string.Empty;
    public float Confidence { get; set; }
    public float X { get; set; }
    public float Y { get; set; }
    public float Width { get; set; }
    public float Height { get; set; }
}
