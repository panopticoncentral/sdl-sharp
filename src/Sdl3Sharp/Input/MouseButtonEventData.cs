using static Sdl3Sharp.Native.Events;

namespace Sdl3Sharp.Input;

/// <summary>
/// Data for mouse button events.
/// </summary>
public readonly struct MouseButtonEventData
{
    private readonly SDL_MouseButtonEvent _event;

    internal MouseButtonEventData(SDL_MouseButtonEvent e) => _event = e;

    /// <summary>Gets the window ID with mouse focus.</summary>
    public uint WindowId => _event.windowID;

    /// <summary>Gets the mouse instance ID.</summary>
    public uint MouseId => _event.which;

    /// <summary>Gets the mouse button index (1=left, 2=middle, 3=right, 4=X1, 5=X2).</summary>
    public byte Button => _event.button;

    /// <summary>Gets a value indicating whether the button is pressed.</summary>
    public bool IsPressed => _event.down;

    /// <summary>Gets the click count (1 for single-click, 2 for double-click, etc.).</summary>
    public byte Clicks => _event.clicks;

    /// <summary>Gets the X coordinate relative to the window.</summary>
    public float X => _event.x;

    /// <summary>Gets the Y coordinate relative to the window.</summary>
    public float Y => _event.y;
}
