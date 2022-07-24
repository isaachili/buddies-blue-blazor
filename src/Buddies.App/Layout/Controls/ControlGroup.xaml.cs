namespace Buddies.App.Layout.Controls;

public partial class ControlGroup : ContentView
{
	#region Bindings

	public static readonly BindableProperty TitleProperty = BindableProperty.Create(nameof(Title), typeof(string), typeof(MultiSelect));
	public static readonly BindableProperty SubtitleProperty = BindableProperty.Create(nameof(Subtitle), typeof(string), typeof(MultiSelect));

	#endregion

	public ControlGroup()
	{
		InitializeComponent();
	}

	public string? Title
	{
		get => (string)GetValue(TitleProperty);
		set => SetValue(TitleProperty, value);
	}

	public string? Subtitle
	{
		get => (string)GetValue(SubtitleProperty);
		set => SetValue(SubtitleProperty, value);
	}
}
