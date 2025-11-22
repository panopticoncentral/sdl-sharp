namespace Sdl3Sharp;

/// <summary>
/// Information about the system's power supply.
/// </summary>
/// <param name="State">The current power state.</param>
/// <param name="SecondsRemaining">The seconds of battery life remaining, or null if unknown or not applicable.</param>
/// <param name="PercentRemaining">The percentage of battery life remaining (0-100), or null if unknown or not applicable.</param>
public readonly record struct PowerInfo(PowerState State, int? SecondsRemaining, int? PercentRemaining);
