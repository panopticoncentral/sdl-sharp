using static Sdl3Sharp.Native.Events;

namespace Sdl3Sharp.Input;

/// <summary>
/// Data for pen proximity events.
/// </summary>
public readonly struct PenProximityEventData
{
    private readonly SDL_PenProximityEvent _event;

    internal PenProximityEventData(SDL_PenProximityEvent e) => _event = e;

    /// <summary>Gets the window ID with pen focus.</summary>
    public uint WindowId => _event.windowID;

    /// <summary>Gets the pen instance ID.</summary>
    public uint PenId => _event.which;
}
