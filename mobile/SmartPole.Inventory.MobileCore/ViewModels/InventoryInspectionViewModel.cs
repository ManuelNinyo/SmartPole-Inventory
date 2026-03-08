using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SmartPole.Inventory.MobileCore.Models;
using SmartPole.Inventory.MobileCore.Services;
using SmartPole.Inventory.MobileCore.Persistence;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;
using System;

namespace SmartPole.Inventory.MobileCore.ViewModels;

// Creamos un interfaz para abstraer MediaPicker y FileSystem
public interface IMediaService
{
    Task<string?> TakePhotoAsync();
}

public interface IDialogService
{
    Task ShowErrorAsync(string title, string message, string buttonText);
}

public partial class InventoryInspectionViewModel : ObservableObject
{
    private readonly IYoloDetectionService _yoloService;
    private readonly IMediaService _mediaService;
    private readonly ILocalDbService _dbService;
    private readonly IDialogService _dialogService;

    public InventoryInspectionViewModel(IYoloDetectionService yoloService, IMediaService mediaService, ILocalDbService dbService, IDialogService dialogService)
    {
        _yoloService = yoloService;
        _mediaService = mediaService;
        _dbService = dbService;
        _dialogService = dialogService;
        Detections = new ObservableCollection<YoloDetection>();
    }

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(HasNoImage))]
    private string _imageSource = string.Empty;

    public bool HasNoImage => string.IsNullOrEmpty(ImageSource);

    [ObservableProperty]
    private bool _isProcessing;

    public ObservableCollection<YoloDetection> Detections { get; }

    [RelayCommand]
    public async Task TakePhotoAsync()
    {
        try
        {
            var imagePath = await _mediaService.TakePhotoAsync();
            if (!string.IsNullOrEmpty(imagePath))
            {
                ImageSource = imagePath;
                await ProcessImageAsync(imagePath);
            }
        }
        catch (System.Exception ex)
        {
            System.Console.WriteLine(ex);
        }
    }

    private async Task ProcessImageAsync(string imagePath)
    {
        IsProcessing = true;
        Detections.Clear();

        try
        {
            using var stream = File.OpenRead(imagePath);
            var results = await _yoloService.DetectAsync(stream);

            foreach(var result in results)
            {
                Detections.Add(result);
            }
        }
        catch(System.Exception ex)
        {
            System.Console.WriteLine($"Error processing image: {ex.Message}");
            await _dialogService.ShowErrorAsync("Error", $"No se pudo analizar el inventario: {ex.Message}", "Aceptar");
        }
        finally
        {
            IsProcessing = false;
        }
    }

    [RelayCommand]
    public async Task SaveInspectionAsync()
    {
        if (Detections.Count == 0) return;

        Guid currentSmartPoleId = Guid.NewGuid(); // ToDo: Obtener el SmartPole seleccionado

        foreach(var detection in Detections)
        {
            var item = new InventoryItemModel
            {
                Id = Guid.NewGuid(),
                SmartPoleId = currentSmartPoleId,
                ItemType = detection.Label,
                Confidence = detection.Confidence,
                DetectedAt = DateTime.UtcNow,
                ImagePath = ImageSource,
                IsSynced = false
            };

            await _dbService.SaveInventoryItemAsync(item);
        }

        // Simular un trigger para sincronización (en un escenario real podría lanzarse en background)
        // var syncService = App.ServiceProvider.GetService<SyncService>();
        // await syncService.SyncInventoryItemsAsync();

        Detections.Clear();
        ImageSource = string.Empty;
    }
}
