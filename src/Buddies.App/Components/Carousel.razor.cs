using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Buddies.App.Components;

public partial class Carousel : IAsyncDisposable
{
	private bool _disposed;

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

		// Start observing carousel pages
		for (int i = 0; i < Pages.Count(); i++)
		{
			await Runtime.InvokeVoidAsync(JSRuntimeFunctions.ObserveInteraction, CreatePageId(i));
		}
	}

	public async ValueTask DisposeAsync(bool disposing)
	{
		// Object has already been disposed
		if (_disposed)
		{
			return;
		}

		if (!disposing)
		{
			_disposed = true;
			return;
		}

		// Stop observing carousel pages
		for (int i = 0; i < Pages.Count(); i++)
		{
			await Runtime.InvokeVoidAsync(JSRuntimeFunctions.UnobserveInteraction, CreatePageId(i));
		}

		_disposed = true;
	}

	public async ValueTask DisposeAsync()
	{
		// Dispose
		await DisposeAsync(true);

		// Suppress finalizer
		GC.SuppressFinalize(this);
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
