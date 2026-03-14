using static SdlSharp.Native.Power;

namespace SdlSharp;

/// <summary>
/// Information about the system's power supply.
/// </summary>
/// <param name="State">The current power state.</param>
/// <param name="BatterySeconds">Seconds of battery life left, or -1 if unknown.</param>
/// <param name="BatteryPercent">Percentage of battery life left (0-100), or -1 if unknown.</param>
public readonly record struct PowerInfo(PowerState State, int BatterySeconds, int BatteryPercent)
{
    /// <summary>
    /// Gets the current power supply details.
    /// </summary>
    public static PowerInfo Get()
    {
        var state = SDL_GetPowerInfo(out var seconds, out var percent);
        return new PowerInfo((PowerState)state, seconds, percent);
    }
}
