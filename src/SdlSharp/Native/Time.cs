using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
// ReSharper disable InconsistentNaming

namespace SdlSharp.Native;

/// <summary>
/// SDL date format preference.
/// </summary>
public enum SDL_DateFormat
{
    /// <summary>Year/Month/Day.</summary>
    SDL_DATE_FORMAT_YYYYMMDD = 0,
    /// <summary>Day/Month/Year.</summary>
    SDL_DATE_FORMAT_DDMMYYYY = 1,
    /// <summary>Month/Day/Year.</summary>
    SDL_DATE_FORMAT_MMDDYYYY = 2,
}

/// <summary>
/// SDL time format preference.
/// </summary>
public enum SDL_TimeFormat
{
    /// <summary>24-hour time.</summary>
    SDL_TIME_FORMAT_24HR = 0,
    /// <summary>12-hour time.</summary>
    SDL_TIME_FORMAT_12HR = 1,
}

/// <summary>
/// A struct representing a date/time.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public struct SDL_DateTime
{
    /// <summary>Year.</summary>
    public int year;
    /// <summary>Month (1-12).</summary>
    public int month;
    /// <summary>Day of the month (1-31).</summary>
    public int day;
    /// <summary>Hour (0-23).</summary>
    public int hour;
    /// <summary>Minute (0-59).</summary>
    public int minute;
    /// <summary>Second (0-59, may be 60 for leap seconds).</summary>
    public int second;
    /// <summary>Nanoseconds.</summary>
    public int nanosecond;
    /// <summary>Day of the week (0=Sunday).</summary>
    public int day_of_week;
    /// <summary>Seconds east of UTC.</summary>
    public int utc_offset;
}

/// <summary>
/// Native bindings for SDL_time.h — time and date utilities.
/// </summary>
[SuppressMessage("Interoperability", "CA1401:P/Invokes should not be visible")]
public static partial class Time
{
    // Deferred: SDL_TimeToWindows, SDL_TimeFromWindows (Windows FILETIME interop —
    // C# has its own DateTime/DateTimeOffset conversion). SDL_GetDaysInMonth,
    // SDL_GetDayOfYear, SDL_GetDayOfWeek (calendar utilities — C# has DateTime
    // and System.Globalization which are more capable).

    /// <summary>Gets the user's preferred date format.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetDateTimeLocalePreferences")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_GetDateTimeLocalePreferences(SDL_DateFormat* dateFormat, SDL_TimeFormat* timeFormat);

    /// <summary>Gets the current time as nanoseconds since the Unix epoch.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetCurrentTime")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_GetCurrentTime(long* ticks);

    /// <summary>Converts an SDL_Time value to a broken-down calendar representation.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_TimeToDateTime")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_TimeToDateTime(long ticks, SDL_DateTime* dt, [MarshalAs(UnmanagedType.U1)] bool localTime);

    /// <summary>Converts a broken-down calendar representation to an SDL_Time value.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_DateTimeToTime")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_DateTimeToTime(SDL_DateTime* dt, long* ticks);
}
