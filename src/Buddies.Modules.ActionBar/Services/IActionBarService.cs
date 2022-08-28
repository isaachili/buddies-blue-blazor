using Buddies.Modules.ActionBar.Models;

namespace Buddies.Modules.ActionBar.Services;

public interface IActionBarService
{
	IEnumerable<ActionBarControl> GetControls();
}
