using Sdl3Sharp.Graphics;
using static Sdl3Sharp.Native.Events;

namespace Sdl3Sharp.Input;

/// <summary>
/// Event arguments for touch finger events.
/// </summary>
public sealed class TouchFingerEventArgs : SdlEventArgs
{
    /// <summary>
    /// Gets the touch device ID.
    /// </summary>
    public ulong TouchId { get; }

    /// <summary>
    /// Gets the finger ID.
    /// </summary>
    public ulong FingerId { get; }

    /// <summary>
    /// Gets the normalized X position in the range 0 to 1.
    /// </summary>
    public float X { get; }

    /// <summary>
    /// Gets the normalized Y position in the range 0 to 1.
    /// </summary>
    public float Y { get; }

    /// <summary>
    /// Gets the change in X position as a normalized value.
    /// </summary>
    public float DeltaX { get; }

    /// <summary>
    /// Gets the change in Y position as a normalized value.
    /// </summary>
    public float DeltaY { get; }

    /// <summary>
    /// Gets the pressure applied in the range 0 to 1.
    /// </summary>
    public float Pressure { get; }

    /// <summary>
    /// Gets the window under the touch, if any.
    /// </summary>
    public Window? Window { get; }

    internal TouchFingerEventArgs(SDL_TouchFingerEvent e) : base(e.timestamp)
    {
        TouchId = e.touchID;
        FingerId = e.fingerID;
        X = e.x;
        Y = e.y;
        DeltaX = e.dx;
        DeltaY = e.dy;
        Pressure = e.pressure;
        Window = e.windowID != 0 ? Window.FromID(e.windowID) : null;
    }
}
