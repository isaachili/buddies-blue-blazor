namespace Buddies.Modules.ActionBar.Models;

public record class ActionBarControl
{
	public ActionBarControl(string title, string icon)
	{
		Title = title;
		Icon = icon;
	}

	public string Title { get; set; }

	public string Icon { get; set; }
}
