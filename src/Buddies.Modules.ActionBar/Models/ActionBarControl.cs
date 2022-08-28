namespace Buddies.Modules.ActionBar.Models;

public record class ActionBarControl
{
	public ActionBarControl(string title, string icon, string path)
	{
		Title = title;
		Icon = icon;
		Path = path;
	}

	public string Title { get; set; }

	public string Icon { get; set; }

	public string Path { get; set; }

	public IEnumerable<string> Styles { get; set; } = Enumerable.Empty<string>();
}
