using static Sdl3Sharp.Native.Events;

namespace Sdl3Sharp.Graphics;

/// <summary>
/// Event arguments for display events.
/// </summary>
public class DisplayEventArgs : SdlEventArgs
{
    /// <summary>
    /// Gets the display ID.
    /// </summary>
    public uint DisplayId { get; }

    /// <summary>
    /// Gets event-dependent data.
    /// </summary>
    public int Data { get; }

    internal DisplayEventArgs(SDL_DisplayEvent e) : base(e.timestamp)
    {
        DisplayId = e.displayID;
        Data = e.data1;
    }
}

/// <summary>
/// Event arguments for display orientation events.
/// </summary>
public sealed class DisplayOrientationEventArgs : DisplayEventArgs
{
    /// <summary>
    /// Gets the new display orientation.
    /// </summary>
    public DisplayOrientation Orientation => (DisplayOrientation)Data;

    internal DisplayOrientationEventArgs(SDL_DisplayEvent e) : base(e) { }
}
