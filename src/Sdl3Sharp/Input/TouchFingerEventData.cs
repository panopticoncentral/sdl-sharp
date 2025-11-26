using static Sdl3Sharp.Native.Events;

namespace Sdl3Sharp.Input;

/// <summary>
/// Data for touch finger events.
/// </summary>
public readonly struct TouchFingerEventData
{
    private readonly SDL_TouchFingerEvent _event;

    internal TouchFingerEventData(SDL_TouchFingerEvent e) => _event = e;

    /// <summary>Gets the touch device ID.</summary>
    public ulong TouchId => _event.touchID;

    /// <summary>Gets the finger ID.</summary>
    public ulong FingerId => _event.fingerID;

    /// <summary>Gets the X position (normalized 0-1).</summary>
    public float X => _event.x;

    /// <summary>Gets the Y position (normalized 0-1).</summary>
    public float Y => _event.y;

    /// <summary>Gets the X delta (normalized -1 to 1).</summary>
    public float DeltaX => _event.dx;

    /// <summary>Gets the Y delta (normalized -1 to 1).</summary>
    public float DeltaY => _event.dy;

    /// <summary>Gets the pressure (normalized 0-1).</summary>
    public float Pressure => _event.pressure;

    /// <summary>Gets the window ID underneath the finger, if any.</summary>
    public uint WindowId => _event.windowID;
}
