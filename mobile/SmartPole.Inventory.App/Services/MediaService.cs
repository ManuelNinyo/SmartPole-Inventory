using System.IO;
using System.Threading.Tasks;
using Microsoft.Maui.Media;
using Microsoft.Maui.Storage;
using SmartPole.Inventory.MobileCore.ViewModels;

namespace SmartPole.Inventory.App.Services;

public class MediaService : IMediaService
{
    public async Task<string?> TakePhotoAsync()
    {
        var photo = await MediaPicker.Default.CapturePhotoAsync();
        if (photo != null)
        {
            var imagePath = Path.Combine(FileSystem.CacheDirectory, photo.FileName);
            using var stream = await photo.OpenReadAsync();
            using var newStream = File.OpenWrite(imagePath);
            await stream.CopyToAsync(newStream);
            return imagePath;
        }
        return null;
    }
}
