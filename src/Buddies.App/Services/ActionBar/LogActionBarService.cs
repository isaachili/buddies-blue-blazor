using Buddies.Modules.ActionBar.Models;
using Buddies.Modules.ActionBar.Services;

namespace Buddies.App.Services.ActionBar
{
	internal class LogActionBarService : IActionBarService
	{
		public bool ShowBackButton => true;

		public IEnumerable<ActionBarControl> GetControls()
		{
			return new[]
			{
				new ActionBarControl("Cancel", "close", "_blank")
				{
					Styles = new[]
					{
						"danger",
					},
				},
				new ActionBarControl("Save", "done", "_blank"),
			};
		}
	}
}
