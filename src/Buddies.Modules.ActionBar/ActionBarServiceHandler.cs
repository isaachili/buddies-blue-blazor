using Buddies.Modules.ActionBar.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.DependencyInjection;

namespace Buddies.Modules.ActionBar;

internal class ActionBarServiceHandler : IActionBarServiceHandler
{
	// Injected dependencies
	private readonly IServiceProvider _provider;

	// Action bar services
	private readonly Dictionary<Type, Type> _services = new();

	// Backing fields
	private Type? _currentPageType;

	public ActionBarServiceHandler(IServiceProvider provider)
	{
		_provider = provider;
	}

	public Type CurrentPageType
	{
		get => _currentPageType ?? throw new InvalidOperationException("Current page type hasn't been set yet.");
		set => _currentPageType = value;
	}

	public IActionBarService? Create()
	{
		return Create(CurrentPageType);
	}

	private IActionBarService? Create(Type pageType)
	{
		if (!_services.ContainsKey(pageType))
		{
			return null;
		}

		return ActivatorUtilities.CreateInstance(_provider, _services[pageType]) as IActionBarService;
	}

	public void Register<TPage, TActionBarService>()
		where TActionBarService : IActionBarService
	{
		if (_services.ContainsKey(typeof(TPage)))
		{
			throw new InvalidOperationException($"{typeof(TPage).Name} has already been registered.");
		}

		_services.Add(typeof(TPage), typeof(TActionBarService));
	}
}
