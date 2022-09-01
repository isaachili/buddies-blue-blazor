using Buddies.Modules.ActionBar.Models;
using Buddies.Modules.ActionBar.Services;

namespace Buddies.App.Services.ActionBar;

internal class IndexActionBarService : IActionBarService
{
	public bool ShowBackButton => false;

	public IEnumerable<ActionBarControl> GetControls()
	{
		return new[]
		{
			new ActionBarControl("Add Log", "add", "/log/"),
			new ActionBarControl("Settings", "settings", "_blank"),
		};
	}
}
