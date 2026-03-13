using System.Collections.Generic;

namespace SmartPole.Inventory.Domain.ML;

public interface IYoloModelService
{
    // Detect smart poles in an image
    IEnumerable<YoloDetectionResult> DetectPoles(string imagePath);
}
