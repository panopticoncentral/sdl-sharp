using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
// ReSharper disable InconsistentNaming

namespace SdlSharp.Native;

// SDL_PowerState enum is defined in Events.cs (needed by SDL_JoystickBatteryEvent).

/// <summary>
/// Native bindings for SDL_power.h — power management status.
/// </summary>
[SuppressMessage("Interoperability", "CA1401:P/Invokes should not be visible")]
public static partial class Power
{
    /// <summary>
    /// Get the current power supply details.
    /// </summary>
    /// <param name="seconds">Seconds of battery life left, or -1 if unknown/not on battery.</param>
    /// <param name="percent">Percentage of battery life left (0-100), or -1 if unknown/not on battery.</param>
    /// <returns>The current power state.</returns>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetPowerInfo")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL_PowerState SDL_GetPowerInfo(out int seconds, out int percent);
}
