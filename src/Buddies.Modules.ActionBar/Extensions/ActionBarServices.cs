using Microsoft.Extensions.DependencyInjection;

namespace Buddies.Modules.ActionBar.Extensions;

public static class ActionBarServices
{
	public static IServiceCollection AddActionBarServices(this IServiceCollection services, Action<IActionBarServiceHandler>? handlerAction = null)
	{
		return services.AddSingleton(p =>
		{
			var handler = (IActionBarServiceHandler)ActivatorUtilities.CreateInstance(p, typeof(ActionBarServiceHandler));
			handlerAction?.Invoke(handler);

			return handler;
		});
	}
}
