// src/SdlSharp/TimeFormat.cs
namespace SdlSharp;

/// <summary>
/// User's preferred time format.
/// </summary>
public enum TimeFormat
{
    TwentyFourHour = (int)Native.SDL_TimeFormat.SDL_TIME_FORMAT_24HR,
    TwelveHour = (int)Native.SDL_TimeFormat.SDL_TIME_FORMAT_12HR,
}
