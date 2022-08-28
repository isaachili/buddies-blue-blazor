using Buddies.Modules.ActionBar.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Buddies.Modules.ActionBar;

public interface IActionBarServiceHandler
{
	Type CurrentPageType { get; set; }

	IActionBarService? Create();

	void Register<TPage, TActionBarService>()
		where TActionBarService : IActionBarService;
}
