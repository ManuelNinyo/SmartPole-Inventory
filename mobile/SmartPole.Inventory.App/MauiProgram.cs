using Microsoft.Extensions.Logging;
using SmartPole.Inventory.App.ViewModels;
using SmartPole.Inventory.App.Views;
using SmartPole.Inventory.MobileCore.Persistence;

namespace SmartPole.Inventory.App;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

#if DEBUG
		builder.Logging.AddDebug();
#endif

		// Registration
		string dbPath = Path.Combine(FileSystem.AppDataDirectory, "smartpole.db3");
		builder.Services.AddSingleton<ILocalDbService>(s => new LocalDbService(dbPath));

		builder.Services.AddSingleton<MainViewModel>();
		builder.Services.AddTransient<InspectionsViewModel>();

		builder.Services.AddSingleton<MainPage>();

		return builder.Build();
	}
}
