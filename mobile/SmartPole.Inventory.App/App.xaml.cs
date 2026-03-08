using SmartPole.Inventory.MobileCore.Services;

namespace SmartPole.Inventory.App;

public partial class App : Application
{
	private readonly IYoloDetectionService _yoloService;

	public App(IYoloDetectionService yoloService)
	{
		InitializeComponent();
		_yoloService = yoloService;
	}

	protected override Window CreateWindow(IActivationState? activationState)
	{
		return new Window(new AppShell());
	}

	protected override async void OnStart()
	{
		base.OnStart();

		try
		{
			using var stream = await FileSystem.OpenAppPackageFileAsync("yolov8n.onnx");
			await _yoloService.InitializeAsync(stream);
		}
		catch (Exception ex)
		{
			Console.WriteLine($"Error loading YOLO model: {ex.Message}");
			if (Current?.MainPage != null)
			{
				Current.MainPage.Dispatcher.DispatchAsync(async () =>
				{
					await Current.MainPage.DisplayAlert("Error", $"No se pudo cargar el modelo YOLO. Las detecciones no funcionarán. {ex.Message}", "Aceptar");
				});
			}
		}
	}
}
