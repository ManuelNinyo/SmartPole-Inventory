using Microsoft.ML.OnnxRuntime;
using Microsoft.ML.OnnxRuntime.Tensors;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using SmartPole.Inventory.MobileCore.Models;
using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartPole.Inventory.MobileCore.Services;

public class YoloDetectionService : IYoloDetectionService {
    private InferenceSession? _session;
    private readonly string[] _labels = {
        "person", "bicycle", "car", "motorcycle", "airplane", "bus", "train", "truck", "boat", "traffic light",
        "fire hydrant", "stop sign", "parking meter", "bench", "bird", "cat", "dog", "horse", "sheep", "cow",
        "elephant", "bear", "zebra", "giraffe", "backpack", "umbrella", "handbag", "tie", "suitcase", "frisbee",
        "skis", "snowboard", "sports ball", "kite", "baseball bat", "baseball glove", "skateboard", "surfboard", "tennis racket", "bottle",
        "wine glass", "cup", "fork", "knife", "spoon", "bowl", "banana", "apple", "sandwich", "orange",
        "broccoli", "carrot", "hot dog", "pizza", "donut", "cake", "chair", "couch", "potted plant", "bed",
        "dining table", "toilet", "tv", "laptop", "mouse", "remote", "keyboard", "cell phone", "microwave", "oven",
        "toaster", "sink", "refrigerator", "book", "clock", "vase", "scissors", "teddy bear", "hair drier", "toothbrush"
    };

    public Task InitializeAsync() {
        return Task.CompletedTask;
    }

    public async Task InitializeAsync(Stream modelStream) {
        if (_session != null) return;

        using var memoryStream = new MemoryStream();
        await modelStream.CopyToAsync(memoryStream);
        var modelBytes = memoryStream.ToArray();
        _session = new InferenceSession(modelBytes);
    }

    public async Task<List<YoloDetection>> DetectAsync(Stream imageStream) {
        if (_session == null) {
            throw new InvalidOperationException("YoloDetectionService is not initialized with a model.");
        }

        // Image preprocessing
        using var image = await Image.LoadAsync<Rgb24>(imageStream);
        var originalWidth = image.Width;
        var originalHeight = image.Height;

        int targetSize = 640;
        image.Mutate(x => x.Resize(targetSize, targetSize));

        var tensor = new DenseTensor<float>(new[] { 1, 3, targetSize, targetSize });
        for (int y = 0; y < targetSize; y++) {
            for (int x = 0; x < targetSize; x++) {
                var pixel = image[x, y];
                tensor[0, 0, y, x] = pixel.R / 255.0f;
                tensor[0, 1, y, x] = pixel.G / 255.0f;
                tensor[0, 2, y, x] = pixel.B / 255.0f;
            }
        }

        var inputs = new List<NamedOnnxValue> {
            NamedOnnxValue.CreateFromTensor("images", tensor)
        };

        // Inference
        using var results = _session.Run(inputs);
        var output = results.First().AsTensor<float>();

        // Postprocessing (YOLOv8 specific output: [1, 84, 8400])
        var detections = new List<YoloDetection>();

        int numClasses = 80;
        int numBoxes = 8400;
        float confidenceThreshold = 0.25f;

        for (int i = 0; i < numBoxes; i++) {
            float maxClassScore = 0;
            int classIndex = -1;

            for (int j = 0; j < numClasses; j++) {
                float score = output[0, 4 + j, i];
                if (score > maxClassScore) {
                    maxClassScore = score;
                    classIndex = j;
                }
            }

            if (maxClassScore > confidenceThreshold) {
                float xc = output[0, 0, i];
                float yc = output[0, 1, i];
                float w = output[0, 2, i];
                float h = output[0, 3, i];

                float x = (xc - w / 2) * (originalWidth / (float)targetSize);
                float y = (yc - h / 2) * (originalHeight / (float)targetSize);
                float width = w * (originalWidth / (float)targetSize);
                float height = h * (originalHeight / (float)targetSize);

                detections.Add(new YoloDetection {
                    Label = _labels[classIndex],
                    Confidence = maxClassScore,
                    BoundingBox = new[] { x, y, width, height }
                });
            }
        }

        return ApplyNms(detections, 0.45f);
    }

    private List<YoloDetection> ApplyNms(List<YoloDetection> detections, float iouThreshold) {
        var result = new List<YoloDetection>();
        detections.Sort((a, b) => b.Confidence.CompareTo(a.Confidence));

        while (detections.Count > 0) {
            var best = detections[0];
            result.Add(best);
            detections.RemoveAt(0);

            for (int i = detections.Count - 1; i >= 0; i--) {
                var other = detections[i];
                if (best.Label == other.Label && CalculateIou(best.BoundingBox, other.BoundingBox) > iouThreshold) {
                    detections.RemoveAt(i);
                }
            }
        }

        return result;
    }

    private float CalculateIou(float[] boxA, float[] boxB) {
        float xA = Math.Max(boxA[0], boxB[0]);
        float yA = Math.Max(boxA[1], boxB[1]);
        float xB = Math.Min(boxA[0] + boxA[2], boxB[0] + boxB[2]);
        float yB = Math.Min(boxA[1] + boxA[3], boxB[1] + boxB[3]);

        float interArea = Math.Max(0, xB - xA) * Math.Max(0, yB - yA);
        float boxAArea = boxA[2] * boxA[3];
        float boxBArea = boxB[2] * boxB[3];

        return interArea / (float)(boxAArea + boxBArea - interArea);
    }
}
