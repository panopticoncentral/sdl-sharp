namespace SdlSharp;

/// <summary>
/// User's preferred date format.
/// </summary>
public enum DateFormat
{
    /// <summary>Year, month, day order (YYYY-MM-DD).</summary>
    YearMonthDay = (int)Native.SDL_DateFormat.SDL_DATE_FORMAT_YYYYMMDD,
    /// <summary>Day, month, year order (DD-MM-YYYY).</summary>
    DayMonthYear = (int)Native.SDL_DateFormat.SDL_DATE_FORMAT_DDMMYYYY,
    /// <summary>Month, day, year order (MM-DD-YYYY).</summary>
    MonthDayYear = (int)Native.SDL_DateFormat.SDL_DATE_FORMAT_MMDDYYYY,
}
