using static Sdl3Sharp.Native.Events;

namespace Sdl3Sharp.Graphics;

/// <summary>
/// Event arguments for window events.
/// </summary>
public class WindowEventArgs : SdlEventArgs
{
    /// <summary>
    /// Gets the window ID.
    /// </summary>
    public uint WindowId { get; }

    /// <summary>
    /// Gets event-dependent data (e.g., new X position for move events, new width for resize events).
    /// </summary>
    public int Data1 { get; }

    /// <summary>
    /// Gets event-dependent data (e.g., new Y position for move events, new height for resize events).
    /// </summary>
    public int Data2 { get; }

    internal WindowEventArgs(SDL_WindowEvent e) : base(e.timestamp)
    {
        WindowId = e.windowID;
        Data1 = e.data1;
        Data2 = e.data2;
    }
}

/// <summary>
/// Event arguments for window move events.
/// </summary>
public sealed class WindowMovedEventArgs : WindowEventArgs
{
    /// <summary>
    /// Gets the new X position of the window.
    /// </summary>
    public int X => Data1;

    /// <summary>
    /// Gets the new Y position of the window.
    /// </summary>
    public int Y => Data2;

    /// <summary>
    /// Gets the new position of the window.
    /// </summary>
    public Point Position => new(X, Y);

    internal WindowMovedEventArgs(SDL_WindowEvent e) : base(e) { }
}

/// <summary>
/// Event arguments for window resize events.
/// </summary>
public sealed class WindowResizedEventArgs : WindowEventArgs
{
    /// <summary>
    /// Gets the new width of the window.
    /// </summary>
    public int Width => Data1;

    /// <summary>
    /// Gets the new height of the window.
    /// </summary>
    public int Height => Data2;

    /// <summary>
    /// Gets the new size of the window.
    /// </summary>
    public Size Size => new(Width, Height);

    internal WindowResizedEventArgs(SDL_WindowEvent e) : base(e) { }
}

/// <summary>
/// Event arguments for window display changed events.
/// </summary>
public sealed class WindowDisplayChangedEventArgs : WindowEventArgs
{
    /// <summary>
    /// Gets the ID of the display the window moved to.
    /// </summary>
    public uint DisplayId => (uint)Data1;

    internal WindowDisplayChangedEventArgs(SDL_WindowEvent e) : base(e) { }
}
