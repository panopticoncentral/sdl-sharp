// src/SdlSharp/Input/PenAxis.cs
namespace SdlSharp.Input;

/// <summary>
/// Pen axis indices.
/// </summary>
public enum PenAxis
{
    Pressure = (int)Native.SDL_PenAxis.SDL_PEN_AXIS_PRESSURE,
    XTilt = (int)Native.SDL_PenAxis.SDL_PEN_AXIS_XTILT,
    YTilt = (int)Native.SDL_PenAxis.SDL_PEN_AXIS_YTILT,
    Distance = (int)Native.SDL_PenAxis.SDL_PEN_AXIS_DISTANCE,
    Rotation = (int)Native.SDL_PenAxis.SDL_PEN_AXIS_ROTATION,
    Slider = (int)Native.SDL_PenAxis.SDL_PEN_AXIS_SLIDER,
    TangentialPressure = (int)Native.SDL_PenAxis.SDL_PEN_AXIS_TANGENTIAL_PRESSURE,
}
