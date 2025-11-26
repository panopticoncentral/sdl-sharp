using static Sdl3Sharp.Native.Events;

namespace Sdl3Sharp.Graphics;

/// <summary>
/// Data for window events.
/// </summary>
public readonly struct WindowEventData
{
    private readonly SDL_WindowEvent _event;

    internal WindowEventData(SDL_WindowEvent e) => _event = e;

    /// <summary>Gets the window ID.</summary>
    public uint WindowId => _event.windowID;

    /// <summary>Gets event-dependent data (e.g., new X position for move events, new width for resize events).</summary>
    public int Data1 => _event.data1;

    /// <summary>Gets event-dependent data (e.g., new Y position for move events, new height for resize events).</summary>
    public int Data2 => _event.data2;
}
