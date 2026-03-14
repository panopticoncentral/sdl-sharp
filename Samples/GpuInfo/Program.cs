// GpuInfo sample — enumerates GPU drivers, creates a device, and queries
// capabilities: shader formats, texture format support, sample counts.
// Exercises: GpuDevice.Create, NumDrivers, GetDriver, Driver, ShaderFormats,
//            SupportsTextureFormat, SupportsSampleCount, GetSwapchainTextureFormat,
//            ClaimWindow/ReleaseWindow, WaitForIdle, Dispose.

using SdlSharp;
using SdlSharp.Graphics;
using SdlSharp.Graphics.Gpu;

using var app = new Application(InitFlags.Video);

// --- GPU drivers ---
Console.WriteLine("=== GPU Drivers ===");
var numDrivers = GpuDevice.NumDrivers;
Console.WriteLine($"  Built-in drivers: {numDrivers}");
for (var i = 0; i < numDrivers; i++)
    Console.WriteLine($"    [{i}] {GpuDevice.GetDriver(i)}");

// --- Create device ---
Console.WriteLine();
Console.WriteLine("=== GPU Device ===");
using var device = GpuDevice.Create(
    GpuShaderFormat.Spirv | GpuShaderFormat.Msl | GpuShaderFormat.Dxil | GpuShaderFormat.MetalLib,
    debugMode: true);

Console.WriteLine($"  Driver: {device.Driver}");
Console.WriteLine($"  Shader formats: {device.ShaderFormats}");

// --- Texture format support ---
Console.WriteLine();
Console.WriteLine("=== Texture Format Support (2D, Sampler) ===");
GpuTextureFormat[] commonFormats =
[
    GpuTextureFormat.R8G8B8A8Unorm,
    GpuTextureFormat.B8G8R8A8Unorm,
    GpuTextureFormat.R8G8B8A8UnormSrgb,
    GpuTextureFormat.R16G16B16A16Float,
    GpuTextureFormat.R32G32B32A32Float,
    GpuTextureFormat.D16Unorm,
    GpuTextureFormat.D24Unorm,
    GpuTextureFormat.D32Float,
    GpuTextureFormat.D24UnormS8Uint,
    GpuTextureFormat.D32FloatS8Uint,
    GpuTextureFormat.Bc1RgbaUnorm,
    GpuTextureFormat.Bc3RgbaUnorm,
    GpuTextureFormat.Bc7RgbaUnorm,
];

foreach (var fmt in commonFormats)
{
    var sampler = device.SupportsTextureFormat(fmt, GpuTextureType.Texture2D, GpuTextureUsage.Sampler);
    var colorTarget = device.SupportsTextureFormat(fmt, GpuTextureType.Texture2D, GpuTextureUsage.ColorTarget);
    var depthTarget = device.SupportsTextureFormat(fmt, GpuTextureType.Texture2D, GpuTextureUsage.DepthStencilTarget);
    Console.WriteLine($"  {fmt,-30} Sampler={sampler,-5} Color={colorTarget,-5} Depth={depthTarget}");
}

// --- Sample count support ---
Console.WriteLine();
Console.WriteLine("=== MSAA Support (R8G8B8A8_UNORM) ===");
GpuSampleCount[] sampleCounts = [GpuSampleCount.One, GpuSampleCount.Two, GpuSampleCount.Four, GpuSampleCount.Eight];
foreach (var sc in sampleCounts)
{
    var supported = device.SupportsSampleCount(GpuTextureFormat.R8G8B8A8Unorm, sc);
    Console.WriteLine($"  {sc}: {supported}");
}

// --- Swapchain ---
Console.WriteLine();
Console.WriteLine("=== Swapchain ===");
using var window = Window.Create("GpuInfo (hidden)", 320, 240, WindowFlags.Hidden);
device.ClaimWindow(window);

var swapFormat = device.GetSwapchainTextureFormat(window);
Console.WriteLine($"  Swapchain format: {swapFormat}");

device.ReleaseWindow(window);
device.WaitForIdle();

Console.WriteLine();
Console.WriteLine("Done.");
