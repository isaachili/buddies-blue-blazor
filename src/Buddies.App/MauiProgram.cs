namespace Buddies.App;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		// Create MAUI application builder
		var builder = MauiApp.CreateBuilder();
		builder.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
			});

		// Add services
		_ = builder.Services.AddMauiBlazorWebView();
		_ = AddDebugServices(builder.Services);

		return builder.Build();
	}

	private static IServiceCollection AddDebugServices(IServiceCollection services)
	{
#if DEBUG
		services.AddBlazorWebViewDeveloperTools();
#endif

		return services;
	}
}
