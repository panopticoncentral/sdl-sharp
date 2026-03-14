namespace SdlSharp;

/// <summary>
/// Power supply state.
/// </summary>
public enum PowerState
{
    /// <summary>Error determining power status.</summary>
    Error = (int)Native.SDL_PowerState.SDL_POWERSTATE_ERROR,
    /// <summary>Cannot determine power status.</summary>
    Unknown = (int)Native.SDL_PowerState.SDL_POWERSTATE_UNKNOWN,
    /// <summary>Not plugged in, running on battery.</summary>
    OnBattery = (int)Native.SDL_PowerState.SDL_POWERSTATE_ON_BATTERY,
    /// <summary>Plugged in, no battery available.</summary>
    NoBattery = (int)Native.SDL_PowerState.SDL_POWERSTATE_NO_BATTERY,
    /// <summary>Plugged in, charging battery.</summary>
    Charging = (int)Native.SDL_PowerState.SDL_POWERSTATE_CHARGING,
    /// <summary>Plugged in, battery charged.</summary>
    Charged = (int)Native.SDL_PowerState.SDL_POWERSTATE_CHARGED,
}
