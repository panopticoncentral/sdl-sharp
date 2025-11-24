using static Sdl3Sharp.Native.Mouse;
using static Sdl3Sharp.Native.Touch;

// We are intentionally exposing the P/Invoke calls so people can do low-level calls if needed
#pragma warning disable CA1401 // P/Invokes should not be visible

namespace Sdl3Sharp.Native;

/// <summary>
/// P/Invoke bindings for SDL_pen.h - Pen (stylus) input handling.
/// SDL provides an API for pressure-sensitive pen (stylus and/or eraser)
/// handling, e.g., for input and drawing tablets or suitably equipped mobile/tablet devices.
/// </summary>
public static partial class Pen
{
    /// <summary>
    /// SDL pen instance IDs.
    /// Zero is used to signify an invalid/null device.
    /// These show up in pen events when SDL sees input from them. They remain
    /// consistent as long as SDL can recognize a tool to be the same pen; but if a
    /// pen physically leaves the area and returns, it might get a new ID.
    /// </summary>
    /// <param name="value">The pen ID value.</param>
    public readonly struct SDL_PenID(uint value)
    {
        /// <summary>The underlying pen ID value.</summary>
        public readonly uint Value = value;

        /// <summary>Implicitly converts an SDL_PenID to uint.</summary>
        /// <param name="id">The pen ID to convert.</param>
        public static implicit operator uint(SDL_PenID id)
        {
            return id.Value;
        }

        /// <summary>Implicitly converts a uint to SDL_PenID.</summary>
        /// <param name="value">The value to convert.</param>
        public static implicit operator SDL_PenID(uint value)
        {
            return new(value);
        }
    }

    /// <summary>
    /// The SDL_MouseID for mouse events simulated with pen input.
    /// </summary>
    public static readonly SDL_MouseID SDL_PEN_MOUSEID = unchecked((uint)-2);

    /// <summary>
    /// The SDL_TouchID for touch events simulated with pen input.
    /// </summary>
    public static readonly SDL_TouchID SDL_PEN_TOUCHID = unchecked((ulong)-2);

    /// <summary>
    /// Pen input flags, as reported by various pen events' pen_state field.
    /// </summary>
    [Flags]
    public enum SDL_PenInputFlags : uint
    {
        /// <summary>No pen input flags set.</summary>
        None = 0,
        /// <summary>Pen is pressed down.</summary>
        SDL_PEN_INPUT_DOWN = 1u << 0,
        /// <summary>Button 1 is pressed.</summary>
        SDL_PEN_INPUT_BUTTON_1 = 1u << 1,
        /// <summary>Button 2 is pressed.</summary>
        SDL_PEN_INPUT_BUTTON_2 = 1u << 2,
        /// <summary>Button 3 is pressed.</summary>
        SDL_PEN_INPUT_BUTTON_3 = 1u << 3,
        /// <summary>Button 4 is pressed.</summary>
        SDL_PEN_INPUT_BUTTON_4 = 1u << 4,
        /// <summary>Button 5 is pressed.</summary>
        SDL_PEN_INPUT_BUTTON_5 = 1u << 5,
        /// <summary>Eraser tip is used.</summary>
        SDL_PEN_INPUT_ERASER_TIP = 1u << 30
    }

    /// <summary>
    /// Pen axis indices.
    /// These are the valid values for the axis field in SDL_PenAxisEvent. All
    /// axes are either normalised to 0..1 or report a (positive or negative) angle
    /// in degrees, with 0.0 representing the centre. Not all pens/backends support
    /// all axes: unsupported axes are always zero.
    /// To convert angles for tilt and rotation into vector representation, use
    /// SDL_sinf on the XTILT, YTILT, or ROTATION component.
    /// </summary>
    public enum SDL_PenAxis
    {
        /// <summary>Pen pressure. Unidirectional: 0 to 1.0.</summary>
        SDL_PEN_AXIS_PRESSURE,
        /// <summary>Pen horizontal tilt angle. Bidirectional: -90.0 to 90.0 (left-to-right).</summary>
        SDL_PEN_AXIS_XTILT,
        /// <summary>Pen vertical tilt angle. Bidirectional: -90.0 to 90.0 (top-to-down).</summary>
        SDL_PEN_AXIS_YTILT,
        /// <summary>Pen distance to drawing surface. Unidirectional: 0.0 to 1.0.</summary>
        SDL_PEN_AXIS_DISTANCE,
        /// <summary>Pen barrel rotation. Bidirectional: -180 to 179.9 (clockwise, 0 is facing up, -180.0 is facing down).</summary>
        SDL_PEN_AXIS_ROTATION,
        /// <summary>Pen finger wheel or slider (e.g., Airbrush Pen). Unidirectional: 0 to 1.0.</summary>
        SDL_PEN_AXIS_SLIDER,
        /// <summary>Pressure from squeezing the pen ("barrel pressure").</summary>
        SDL_PEN_AXIS_TANGENTIAL_PRESSURE,
        /// <summary>Total known pen axis types in this version of SDL. This number may grow in future releases!</summary>
        SDL_PEN_AXIS_COUNT
    }

    // Note: SDL_pen.h does not expose any functions, only types and constants.
    // Pen input is handled through SDL events (SDL_EVENT_PEN_*).
}
