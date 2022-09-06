using Microsoft.AspNetCore.Components;

namespace Buddies.App.Events;

[EventHandler("oncarouselpagescroll", typeof(CarouselPageScrollEventArgs), enableStopPropagation: true, enablePreventDefault: true)]
public static class EventHandlers
{

}
