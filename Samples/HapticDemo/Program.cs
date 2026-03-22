// HapticDemo sample — enumerates haptic (force feedback) devices and tests rumble.
// Exercises: Haptic.GetDevices, GetName, Haptic.Open, RumbleSupported,
//            InitRumble, PlayRumble, StopRumble, Dispose.
// If a haptic device is available, opens it and plays a short 500 ms rumble at 75% strength.

using SdlSharp;
using SdlSharp.Input;

using var app = new Application(InitFlags.Haptic | InitFlags.Joystick);

// --- Haptic device enumeration ---
Console.WriteLine("=== Haptic Devices ===");
var hapticIds = Haptic.GetDevices();
if (hapticIds.Length == 0)
{
    Console.WriteLine("  (no haptic devices found)");
}
else
{
    Console.WriteLine($"  Haptic devices found: {hapticIds.Length}");
    foreach (var id in hapticIds)
    {
        var name = Haptic.GetName(id) ?? "(unknown)";
        Console.WriteLine($"  [{id}] {name}");
    }
}

// --- Open first available haptic device and test rumble ---
Console.WriteLine();
Console.WriteLine("=== Rumble Test ===");
if (hapticIds.Length == 0)
{
    Console.WriteLine("  (no haptic device to open)");
}
else
{
    using var haptic = Haptic.Open(hapticIds[0]);
    Console.WriteLine($"  Opened: {haptic.Name}");
    Console.WriteLine($"  Rumble supported: {haptic.RumbleSupported}");

    if (haptic.RumbleSupported)
    {
        haptic.InitRumble();
        Console.WriteLine("  Rumble initialized.");

        const float strength = 0.75f;
        const uint durationMs = 500u;
        Console.WriteLine($"  Playing rumble: strength={strength}, duration={durationMs} ms ...");
        haptic.PlayRumble(strength, durationMs);

        // Wait for the rumble to finish, then stop explicitly.
        System.Threading.Thread.Sleep((int)durationMs);
        haptic.StopRumble();
        Console.WriteLine("  Rumble stopped.");
    }
    else
    {
        Console.WriteLine("  Skipping rumble test (not supported on this device).");
    }
}

Console.WriteLine();
Console.WriteLine("Done.");
