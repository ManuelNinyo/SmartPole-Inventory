namespace SmartPole.Inventory.Domain.ML;

public class YoloModel
{
    public string ModelPath { get; private set; }
    public string Version { get; private set; }
    public float ConfidenceThreshold { get; private set; }

    public YoloModel(string modelPath, string version, float confidenceThreshold = 0.5f)
    {
        ModelPath = modelPath;
        Version = version;
        ConfidenceThreshold = confidenceThreshold;
    }

    // Placeholder for detection logic or configuration related to YOLO
}
