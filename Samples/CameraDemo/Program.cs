// CameraDemo sample — enumerates camera drivers and connected camera devices.
// Exercises: Camera.GetDrivers, GetCurrentDriver, GetDevices, GetName, GetPosition,
//            GetSupportedFormats, CameraSpec, CameraPosition.
// Note: Does not open any camera device or acquire frames, so no camera permission
// is requested and the sample works even when no camera is available.

using SdlSharp;
using SdlSharp.Graphics;

using var app = new Application(InitFlags.Camera);

// --- Camera drivers ---
Console.WriteLine("=== Camera Drivers ===");
var drivers = Camera.GetDrivers();
Console.WriteLine($"  Built-in drivers: {drivers.Length}");
for (var i = 0; i < drivers.Length; i++)
    Console.WriteLine($"    [{i}] {drivers[i]}");

var currentDriver = Camera.GetCurrentDriver();
Console.WriteLine($"  Current driver: {currentDriver ?? "(none)"}");

// --- Camera devices ---
Console.WriteLine();
Console.WriteLine("=== Camera Devices ===");
var devices = Camera.GetDevices();
if (devices.Length == 0)
{
    Console.WriteLine("  (no cameras found)");
}
else
{
    Console.WriteLine($"  Cameras found: {devices.Length}");
    foreach (var id in devices)
    {
        var name = Camera.GetName(id) ?? "(unknown)";
        var position = Camera.GetPosition(id);
        Console.WriteLine($"  [{id}] {name}  position={position}");

        // --- Supported formats ---
        var formats = Camera.GetSupportedFormats(id);
        if (formats.Length == 0)
        {
            Console.WriteLine($"         Supported formats: (none reported)");
        }
        else
        {
            Console.WriteLine($"         Supported formats ({formats.Length}):");
            foreach (var fmt in formats)
            {
                var fps = fmt.FramerateDenominator > 0
                    ? $"{(double)fmt.FramerateNumerator / fmt.FramerateDenominator:F2} fps"
                    : "? fps";
                Console.WriteLine($"           {fmt.Width}x{fmt.Height}  {fmt.Format}  {fps}");
            }
        }
    }
}

Console.WriteLine();
Console.WriteLine("Done.");
