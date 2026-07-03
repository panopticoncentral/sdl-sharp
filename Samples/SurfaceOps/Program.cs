// SurfaceOps sample — headless exercise of the phase 9 surface API.
// Exercises: pixel read/write, color key, scale, flip, PNG save/reload round-trip.

using SdlSharp;
using SdlSharp.Graphics;

using var app = new Application(InitFlags.Video);

using var surface = Surface.Create(64, 64, PixelFormat.Rgba8888);
surface.WritePixel(10, 10, new Color(255, 0, 0, 255));
var read = surface.ReadPixel(10, 10);
Console.WriteLine($"Pixel round-trip: {read} (red: {read.R == 255})");

surface.SetColorKey(new Color(0, 0, 0, 255));
Console.WriteLine($"Has color key: {surface.HasColorKey}");
surface.ClearColorKey();
Console.WriteLine($"Cleared color key: {!surface.HasColorKey}");

using var scaled = surface.Scale(128, 128, ScaleMode.Nearest);
Console.WriteLine($"Scaled: 64x64 -> {scaled.Width}x{scaled.Height}");

surface.Flip(FlipMode.Horizontal);
var flipped = surface.ReadPixel(53, 10);   // 63 - 10
Console.WriteLine($"Flip moved pixel: {flipped.R == 255}");

var pngPath = Path.Combine(Path.GetTempPath(), $"sdlsharp-surfaceops-{Environment.ProcessId}.png");
surface.SavePng(pngPath);
using var reloaded = Surface.LoadPng(pngPath);
var probe = reloaded.ReadPixel(53, 10);
Console.WriteLine($"PNG round-trip: {probe.R == 255}");
File.Delete(pngPath);

Console.WriteLine($"MustLock: {surface.MustLock}, Flags: {surface.Flags}");
Console.WriteLine("Done.");
