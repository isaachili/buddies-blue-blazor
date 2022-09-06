using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Buddies.App.Components;

public partial class Carousel : IDisposable
{
	#region Carousel Page

	public class Page
	{
		public string Title { get; set; }

		public Type ComponentType { get; init; }
	}

	public class Page<TComponent> : Page
	{
		public Page()
		{
			ComponentType = typeof(TComponent);
		}
	}

	#endregion

	#region Properties

	private string CarouselId { get; } = Guid.NewGuid().ToString("N");

	[Inject]
	public IJSRuntime Runtime { get; set; }

	[Parameter]
	public IEnumerable<Page> Pages { get; set; }

	[Parameter]
	public int CurrentIndex { get; set; }

	#endregion

	#region Lifetime

	protected override async Task OnAfterRenderAsync(bool firstRender)
	{
		await base.OnAfterRenderAsync(firstRender);

		if (!firstRender)
		{
			return;
		}

		for (int i = 0; i < Pages.Count(); i++)
		{
			// Start observing carousel pages
			await Runtime.InvokeVoidAsync(JSRuntimeFunctions.ObserveInteraction, CreatePageId(i));
		}
	}

	public void Dispose()
	{
		for (int i = 0; i < Pages.Count(); i++)
		{
			// Stop observing carousel pages
			_ = Runtime.InvokeVoidAsync(JSRuntimeFunctions.UnobserveInteraction, CreatePageId(i));
		}
	}

	#endregion

	private string CreatePageId(int index)
	{
		return $"{CarouselId}-p{index}";
	}

	private async Task ScrollToPage(int index, bool shouldScroll = true)
	{
		// Update page index
		CurrentIndex = index;

		if (!shouldScroll)
		{
			return;
		}

		// Scroll to page
		await Runtime.InvokeVoidAsync(JSRuntimeFunctions.ScrollIntoView, CreatePageId(index));
	}
}
