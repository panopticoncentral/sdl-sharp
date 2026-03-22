// src/SdlSharp/SdlDateTime.cs
using static SdlSharp.Native.Common;
using static SdlSharp.Native.Time;

namespace SdlSharp;

/// <summary>
/// SDL date/time utilities bridging to .NET DateTime.
/// </summary>
public static unsafe class SdlDateTime
{
    /// <summary>
    /// Gets the user's preferred date format.
    /// </summary>
    public static DateFormat GetPreferredDateFormat()
    {
        Native.SDL_DateFormat fmt;
        Check(SDL_GetDateTimeLocalePreferences(&fmt, null));
        return (DateFormat)fmt;
    }

    /// <summary>
    /// Gets the user's preferred time format.
    /// </summary>
    public static TimeFormat GetPreferredTimeFormat()
    {
        Native.SDL_TimeFormat fmt;
        Check(SDL_GetDateTimeLocalePreferences(null, &fmt));
        return (TimeFormat)fmt;
    }

    /// <summary>
    /// Gets the current time as SDL nanosecond ticks since Unix epoch.
    /// </summary>
    public static long GetCurrentTime()
    {
        long ticks;
        Check(SDL_GetCurrentTime(&ticks));
        return ticks;
    }

    /// <summary>
    /// Converts an SDL time value (nanoseconds since Unix epoch) to a .NET DateTime.
    /// </summary>
    /// <param name="sdlTime">SDL time in nanoseconds.</param>
    /// <param name="localTime">If true, convert to local time; otherwise UTC.</param>
    public static DateTime ToDateTime(long sdlTime, bool localTime = false)
    {
        Native.SDL_DateTime dt;
        Check(SDL_TimeToDateTime(sdlTime, &dt, localTime));
        var result = new DateTime(dt.year, dt.month, dt.day, dt.hour, dt.minute, dt.second,
            dt.nanosecond / 1_000_000, localTime ? DateTimeKind.Local : DateTimeKind.Utc);
        return result;
    }

    /// <summary>
    /// Converts a .NET DateTime to an SDL time value (nanoseconds since Unix epoch).
    /// </summary>
    public static long FromDateTime(DateTime dateTime)
    {
        var dt = new Native.SDL_DateTime
        {
            year = dateTime.Year,
            month = dateTime.Month,
            day = dateTime.Day,
            hour = dateTime.Hour,
            minute = dateTime.Minute,
            second = dateTime.Second,
            nanosecond = (int)(dateTime.Ticks % TimeSpan.TicksPerSecond) * 100,
            day_of_week = (int)dateTime.DayOfWeek,
            utc_offset = dateTime.Kind == DateTimeKind.Local
                ? (int)TimeZoneInfo.Local.GetUtcOffset(dateTime).TotalSeconds
                : 0,
        };
        long ticks;
        Check(SDL_DateTimeToTime(&dt, &ticks));
        return ticks;
    }
}
