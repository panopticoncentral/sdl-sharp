using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

// We are intentionally exposing the P/Invoke calls so people can do low-level calls if needed
#pragma warning disable CA1401 // P/Invokes should not be visible

namespace Sdl3Sharp.Native;

/// <summary>
/// P/Invoke bindings for SDL_power.h - Power management routines.
/// </summary>
public static partial class Power
{
    /// <summary>
    /// The basic state for the system's power supply.
    /// These are results returned by SDL_GetPowerInfo().
    /// </summary>
    public enum SDL_PowerState
    {
        /// <summary>Error determining power status.</summary>
        SDL_POWERSTATE_ERROR = -1,
        /// <summary>Cannot determine power status.</summary>
        SDL_POWERSTATE_UNKNOWN,
        /// <summary>Not plugged in, running on the battery.</summary>
        SDL_POWERSTATE_ON_BATTERY,
        /// <summary>Plugged in, no battery available.</summary>
        SDL_POWERSTATE_NO_BATTERY,
        /// <summary>Plugged in, charging battery.</summary>
        SDL_POWERSTATE_CHARGING,
        /// <summary>Plugged in, battery charged.</summary>
        SDL_POWERSTATE_CHARGED
    }

    /// <summary>
    /// Get the current power supply details.
    /// </summary>
    /// <param name="seconds">A pointer filled in with the seconds of battery life left,
    /// or NULL to ignore. This will be filled in with -1 if we can't determine a value
    /// or there is no battery.</param>
    /// <param name="percent">A pointer filled in with the percentage of battery life
    /// left, between 0 and 100, or NULL to ignore. This will be filled in with -1 if
    /// we can't determine a value or there is no battery.</param>
    /// <returns>The current battery state or SDL_POWERSTATE_ERROR on failure;
    /// call SDL_GetError() for more information.</returns>
    [LibraryImport(Common.Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial SDL_PowerState SDL_GetPowerInfo(int* seconds, int* percent);
}
