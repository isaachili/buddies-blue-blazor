using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Buddies.App.Components;

public partial class Carousel : IAsyncDisposable
{
	private bool _disposed;

	#region Carousel Page

	/// <summary>
	/// Carousel page
	/// </summary>
	public class Page
	{
		/// <summary>
		/// Title of the carousel page
		/// </summary>
		public string Title { get; set; }

		/// <summary>
		/// Component/view type
		/// </summary>
		public Type ComponentType { get; init; }
	}

	/// <summary>
	/// Carousel page
	/// </summary>
	/// <typeparam name="TComponent">Component/view type</typeparam>
	public class Page<TComponent> : Page
	{
		public Page()
		{
			ComponentType = typeof(TComponent);
		}
	}

	#endregion

	#region Properties

	/// <summary>
	/// Identifier
	/// </summary>
	private string CarouselId { get; } = Guid.NewGuid().ToString("N");

	/// <summary>
	/// JavaScript runtime service
	/// </summary>
	[Inject]
	public IJSRuntime Runtime { get; set; }

	/// <summary>
	/// Pages parameter
	/// </summary>
	[Parameter]
	public IEnumerable<Page> Pages { get; set; }

	/// <summary>
	/// Current page index parameter
	/// </summary>
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

	/// <summary>
	/// Unsubscribes to any interaction observers and disposes of the component.
	/// </summary>
	/// <param name="disposing">Disposes of the component when <see langword="true"/></param>
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

	/// <summary>
	/// Generates a page identifier based upon the page's index.
	/// </summary>
	/// <param name="index">Page index</param>
	/// <returns>Page identifier</returns>
	private string CreatePageId(int index)
	{
		return $"{CarouselId}-p{index}";
	}

	/// <summary>
	/// Scrolls to the selected page.
	/// </summary>
	/// <param name="index">Page index</param>
	/// <param name="shouldScroll">Scrolls the selected page into the view when <see langword="true"/></param>
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
