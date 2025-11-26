using static Sdl3Sharp.Native.Events;

namespace Sdl3Sharp.Input;

/// <summary>
/// Data for pen button events.
/// </summary>
public readonly struct PenButtonEventData
{
    private readonly SDL_PenButtonEvent _event;

    internal PenButtonEventData(SDL_PenButtonEvent e) => _event = e;

    /// <summary>Gets the window ID with pen focus.</summary>
    public uint WindowId => _event.windowID;

    /// <summary>Gets the pen instance ID.</summary>
    public uint PenId => _event.which;

    /// <summary>Gets the pen input state flags.</summary>
    public uint PenState => (uint)_event.pen_state;

    /// <summary>Gets the X coordinate relative to the window.</summary>
    public float X => _event.x;

    /// <summary>Gets the Y coordinate relative to the window.</summary>
    public float Y => _event.y;

    /// <summary>Gets the pen button index (first button is 1).</summary>
    public byte Button => _event.button;

    /// <summary>Gets a value indicating whether the button is pressed.</summary>
    public bool IsPressed => _event.down;
}
