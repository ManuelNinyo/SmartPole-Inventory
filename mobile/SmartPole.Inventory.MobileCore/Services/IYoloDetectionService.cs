using SmartPole.Inventory.MobileCore.Models;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace SmartPole.Inventory.MobileCore.Services;

public interface IYoloDetectionService {
    Task InitializeAsync();
    Task InitializeAsync(Stream modelStream);
    Task<List<YoloDetection>> DetectAsync(Stream imageStream);
}
