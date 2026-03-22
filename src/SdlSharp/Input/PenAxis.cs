namespace SdlSharp.Input;

/// <summary>
/// Pen axis indices.
/// </summary>
public enum PenAxis
{
    /// <summary>Pen tip pressure (0.0 to 1.0).</summary>
    Pressure = (int)Native.SDL_PenAxis.SDL_PEN_AXIS_PRESSURE,
    /// <summary>Pen horizontal tilt angle in degrees (-90 to 90).</summary>
    XTilt = (int)Native.SDL_PenAxis.SDL_PEN_AXIS_XTILT,
    /// <summary>Pen vertical tilt angle in degrees (-90 to 90).</summary>
    YTilt = (int)Native.SDL_PenAxis.SDL_PEN_AXIS_YTILT,
    /// <summary>Pen distance above the tablet surface.</summary>
    Distance = (int)Native.SDL_PenAxis.SDL_PEN_AXIS_DISTANCE,
    /// <summary>Pen barrel rotation angle in degrees (0 to 359).</summary>
    Rotation = (int)Native.SDL_PenAxis.SDL_PEN_AXIS_ROTATION,
    /// <summary>Pen finger wheel or slider value (0.0 to 1.0).</summary>
    Slider = (int)Native.SDL_PenAxis.SDL_PEN_AXIS_SLIDER,
    /// <summary>Tangential (barrel) pressure on the pen (-1.0 to 1.0).</summary>
    TangentialPressure = (int)Native.SDL_PenAxis.SDL_PEN_AXIS_TANGENTIAL_PRESSURE,
}
