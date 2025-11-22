using static Sdl3Sharp.Native.Pen;

namespace Sdl3Sharp.Input;

/// <summary>
/// Pen axis indices.
/// These are the valid values for the axis field in pen axis events. All
/// axes are either normalised to 0..1 or report a (positive or negative) angle
/// in degrees, with 0.0 representing the centre. Not all pens/backends support
/// all axes: unsupported axes are always zero.
/// </summary>
/// <remarks>
/// To convert angles for tilt and rotation into vector representation, use
/// MathF.Sin on the XTilt, YTilt, or Rotation component, for example:
/// <code>MathF.Sin(xtilt * MathF.PI / 180.0f)</code>
/// </remarks>
public enum PenAxis
{
    /// <summary>Pen pressure. Unidirectional: 0 to 1.0.</summary>
    Pressure = SDL_PenAxis.SDL_PEN_AXIS_PRESSURE,
    /// <summary>Pen horizontal tilt angle. Bidirectional: -90.0 to 90.0 (left-to-right).</summary>
    XTilt = SDL_PenAxis.SDL_PEN_AXIS_XTILT,
    /// <summary>Pen vertical tilt angle. Bidirectional: -90.0 to 90.0 (top-to-down).</summary>
    YTilt = SDL_PenAxis.SDL_PEN_AXIS_YTILT,
    /// <summary>Pen distance to drawing surface. Unidirectional: 0.0 to 1.0.</summary>
    Distance = SDL_PenAxis.SDL_PEN_AXIS_DISTANCE,
    /// <summary>Pen barrel rotation. Bidirectional: -180 to 179.9 (clockwise, 0 is facing up, -180.0 is facing down).</summary>
    Rotation = SDL_PenAxis.SDL_PEN_AXIS_ROTATION,
    /// <summary>Pen finger wheel or slider (e.g., Airbrush Pen). Unidirectional: 0 to 1.0.</summary>
    Slider = SDL_PenAxis.SDL_PEN_AXIS_SLIDER,
    /// <summary>Pressure from squeezing the pen ("barrel pressure").</summary>
    TangentialPressure = SDL_PenAxis.SDL_PEN_AXIS_TANGENTIAL_PRESSURE,
    /// <summary>Total known pen axis types. This number may grow in future releases!</summary>
    Count = SDL_PenAxis.SDL_PEN_AXIS_COUNT
}
