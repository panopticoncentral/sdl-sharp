using static SdlSharp.Native.Timer;

namespace SdlSharp;

/// <summary>
/// Provides SDL timing and delay utilities.
/// </summary>
public static class SdlTimer
{
    /// <summary>Gets the number of milliseconds since SDL library initialization.</summary>
    public static ulong Ticks => SDL_GetTicks();

    /// <summary>Gets the number of nanoseconds since SDL library initialization.</summary>
    public static ulong TicksNS => SDL_GetTicksNS();

    /// <summary>Gets the current value of the high resolution counter.</summary>
    public static ulong PerformanceCounter => SDL_GetPerformanceCounter();

    /// <summary>Gets the count per second of the high resolution counter.</summary>
    public static ulong PerformanceFrequency => SDL_GetPerformanceFrequency();

    /// <summary>
    /// Waits a specified number of milliseconds before returning.
    /// </summary>
    /// <param name="ms">The number of milliseconds to wait.</param>
    public static void Delay(uint ms) => SDL_Delay(ms);

    /// <summary>
    /// Waits a specified number of nanoseconds before returning.
    /// </summary>
    /// <param name="ns">The number of nanoseconds to wait.</param>
    public static void DelayNS(ulong ns) => SDL_DelayNS(ns);

    /// <summary>
    /// Waits a specified number of nanoseconds with precise timing.
    /// </summary>
    /// <param name="ns">The number of nanoseconds to wait.</param>
    public static void DelayPrecise(ulong ns) => SDL_DelayPrecise(ns);
}
