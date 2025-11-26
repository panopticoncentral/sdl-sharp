using static Sdl3Sharp.Native.Events;

namespace Sdl3Sharp.Graphics;

/// <summary>
/// Data for display events.
/// </summary>
public readonly struct DisplayEventData
{
    private readonly SDL_DisplayEvent _event;

    internal DisplayEventData(SDL_DisplayEvent e) => _event = e;

    /// <summary>Gets the display ID.</summary>
    public uint DisplayId => _event.displayID;

    /// <summary>Gets event-dependent data.</summary>
    public int Data1 => _event.data1;

    /// <summary>Gets event-dependent data.</summary>
    public int Data2 => _event.data2;
}
