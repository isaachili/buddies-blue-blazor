using Buddies.Modules.ActionBar.Models;
using Buddies.Modules.ActionBar.Services;

namespace Buddies.App.Services.ActionBar;

internal class IndexActionBarService : IActionBarService
{
	public IEnumerable<ActionBarControl> GetControls()
	{
		return new[]
		{
			new ActionBarControl("Add Log", "add"),
			new ActionBarControl("Settings", "settings"),
		};
	}
}
