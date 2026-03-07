using System;

namespace SmartPole.Inventory.MobileCore.Models;

public class YoloDetection {
    public string Label { get; set; } = string.Empty;
    public float Confidence { get; set; }
    public float[] BoundingBox { get; set; } = Array.Empty<float>(); // [x, y, width, height]
}
