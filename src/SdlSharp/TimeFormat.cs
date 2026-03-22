namespace SdlSharp;

/// <summary>
/// User's preferred time format.
/// </summary>
public enum TimeFormat
{
    /// <summary>24-hour clock format.</summary>
    TwentyFourHour = (int)Native.SDL_TimeFormat.SDL_TIME_FORMAT_24HR,
    /// <summary>12-hour clock format with AM/PM.</summary>
    TwelveHour = (int)Native.SDL_TimeFormat.SDL_TIME_FORMAT_12HR,
}
