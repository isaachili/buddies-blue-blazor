using Microsoft.AspNetCore.Components;

namespace Buddies.App;

[EventHandler("oncarouselpagescroll", typeof(CarouselPageScrollEventArgs), enableStopPropagation: true, enablePreventDefault: true)]
public static class EventHandlers
{

}

public class CarouselPageScrollEventArgs : EventArgs
{

}

