using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Buddies.App.Layout.Controls;

public partial class MultiSelect : ContentView
{
	public class Item
	{
		private string? _iconResourceName;

		public string? Name { get; set; }

		public string? Icon
		{
			get => _iconResourceName;
			set
			{
				_iconResourceName = value;
				IconSource = _iconResourceName is not null
					? ImageSource.FromFile(Icon)
					: null;
			}
		}

		public ImageSource? IconSource { get; private set; }
	}

	#region Bindings

	public static readonly BindableProperty TitleProperty = BindableProperty.Create(nameof(Title), typeof(string), typeof(MultiSelect));
	public static readonly BindableProperty SubtitleProperty = BindableProperty.Create(nameof(Subtitle), typeof(string), typeof(MultiSelect));
	public static readonly BindableProperty ItemsProperty = BindableProperty.Create(nameof(Items), typeof(IEnumerable<Item>), typeof(MultiSelect), Array.Empty<Item>());
	public static readonly BindableProperty SelectedItemsProperty = BindableProperty.Create(nameof(SelectedItems), typeof(ObservableCollection<Item>), typeof(MultiSelect), defaultBindingMode: BindingMode.TwoWay);
	public static readonly BindableProperty SelectionModeProperty = BindableProperty.Create(nameof(SelectionMode), typeof(SelectionMode), typeof(MultiSelect), SelectionMode.Multiple);

	#endregion

	public MultiSelect()
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

	public IEnumerable<Item> Items
	{
		get => (IEnumerable<Item>)GetValue(ItemsProperty);
		set => SetValue(ItemsProperty, value);
	}

	public ObservableCollection<Item> SelectedItems
	{
		get => (ObservableCollection<Item>)GetValue(SelectedItemsProperty);
		set => SetValue(SelectedItemsProperty, value);
	}

	public SelectionMode SelectionMode
	{
		get => (SelectionMode)GetValue(SelectionModeProperty);
		set
		{
			if (value == SelectionMode.None)
			{
				throw new NotSupportedException("'None' is not a valid selection mode for the multi-select component.");
			}

			SetValue(SelectionModeProperty, value);
		}
	}
}
