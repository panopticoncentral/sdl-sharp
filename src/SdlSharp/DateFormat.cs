// src/SdlSharp/DateFormat.cs
namespace SdlSharp;

/// <summary>
/// User's preferred date format.
/// </summary>
public enum DateFormat
{
    YearMonthDay = (int)Native.SDL_DateFormat.SDL_DATE_FORMAT_YYYYMMDD,
    DayMonthYear = (int)Native.SDL_DateFormat.SDL_DATE_FORMAT_DDMMYYYY,
    MonthDayYear = (int)Native.SDL_DateFormat.SDL_DATE_FORMAT_MMDDYYYY,
}
