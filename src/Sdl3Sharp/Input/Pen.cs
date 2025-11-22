using static Sdl3Sharp.Native.Pen;

namespace Sdl3Sharp.Input;

/// <summary>
/// Represents a pen (stylus) input device.
/// Pens may provide more than simple touch input; they might have other axes,
/// such as pressure, tilt, rotation, etc.
/// </summary>
/// <remarks>
/// To get started with pens, handle pen events from the event system. When a pen
/// starts providing input, SDL will assign it a unique ID, which will remain for
/// the life of the process, as long as the pen stays connected.
/// </remarks>
/// <param name="Id">The unique pen instance ID.</param>
public readonly record struct Pen(uint Id)
{
    /// <summary>
    /// The mouse for mouse events simulated with pen input.
    /// Use this to filter out simulated mouse events when processing pen input separately.
    /// </summary>
    public static Mouse PenMouse => new(SDL_PEN_MOUSEID);

    /// <summary>
    /// The touch device for touch events simulated with pen input.
    /// Use this to filter out simulated touch events when processing pen input separately.
    /// </summary>
    public static TouchDevice PenTouch => new(SDL_PEN_TOUCHID);
}
