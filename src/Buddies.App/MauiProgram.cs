using Buddies.App.Services.ActionBar;
using Buddies.Modules.ActionBar.Extensions;
using IndexPage = Buddies.App.Pages.Index;

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
		_ = builder.Services.AddActionBarServices(h =>
		{
			h.Register<IndexPage, IndexActionBarService>();
		});
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
