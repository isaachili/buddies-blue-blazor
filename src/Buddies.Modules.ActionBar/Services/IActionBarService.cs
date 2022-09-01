using Buddies.Modules.ActionBar.Models;

namespace Buddies.Modules.ActionBar.Services;

public interface IActionBarService
{
	bool ShowBackButton { get; }

	IEnumerable<ActionBarControl> GetControls();
}
