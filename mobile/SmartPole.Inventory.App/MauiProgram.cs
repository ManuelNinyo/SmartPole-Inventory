using Microsoft.Extensions.Logging;
using SmartPole.Inventory.App.ViewModels;
using SmartPole.Inventory.App.Views;
using SmartPole.Inventory.App.Services;
using SmartPole.Inventory.MobileCore.ViewModels;
using SmartPole.Inventory.MobileCore.Services;
using SmartPole.Inventory.MobileCore.Persistence;
using SkiaSharp.Views.Maui.Controls.Hosting;
using System.Net.Http;

namespace SmartPole.Inventory.App;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.UseSkiaSharp()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

#if DEBUG
		builder.Logging.AddDebug();
#endif

		// Registration
		string dbPath = Path.Combine(Microsoft.Maui.Storage.FileSystem.AppDataDirectory, "smartpole.db3");
		builder.Services.AddSingleton<ILocalDbService>(s => new LocalDbService(dbPath));
		builder.Services.AddSingleton<ILocationService, LocationService>();

		builder.Services.AddSingleton<IYoloDetectionService, YoloDetectionService>();
		builder.Services.AddSingleton<IMediaService, MediaService>();

        builder.Services.AddSingleton(new HttpClient { BaseAddress = new System.Uri("http://10.0.2.2:5000/") });
        builder.Services.AddTransient<SyncService>();

		builder.Services.AddSingleton<MainViewModel>();
		builder.Services.AddTransient<InspectionsViewModel>();
		builder.Services.AddTransient<MapViewModel>();
		builder.Services.AddTransient<InventoryInspectionViewModel>();

		builder.Services.AddSingleton<MainPage>();
		builder.Services.AddTransient<MapPage>();
		builder.Services.AddTransient<InventoryInspectionPage>();

		return builder.Build();
	}
}
