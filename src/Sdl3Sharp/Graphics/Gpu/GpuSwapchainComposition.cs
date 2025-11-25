namespace Sdl3Sharp.Graphics.Gpu;

/// <summary>
/// Specifies the texture format and colorspace of the swapchain textures.
/// </summary>
public enum GpuSwapchainComposition
{
    /// <summary>B8G8R8A8 or R8G8B8A8 swapchain. Pixel values are in sRGB encoding.</summary>
    Sdr,

    /// <summary>B8G8R8A8_SRGB or R8G8B8A8_SRGB swapchain. Pixel values are stored in sRGB encoding but accessed in linear sRGB.</summary>
    SdrLinear,

    /// <summary>R16G16B16A16_FLOAT swapchain. Pixel values are in extended linear sRGB encoding.</summary>
    HdrExtendedLinear,

    /// <summary>A2R10G10B10 or A2B10G10R10 swapchain. Pixel values are in BT.2020 ST2084 (PQ) encoding.</summary>
    Hdr10St2084
}
