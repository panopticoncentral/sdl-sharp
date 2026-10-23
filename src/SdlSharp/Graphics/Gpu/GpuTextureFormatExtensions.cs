using SdlSharp.Native;
using static SdlSharp.Native.Gpu;

namespace SdlSharp.Graphics.Gpu;

/// <summary>
/// Size queries for GPU texture formats.
/// </summary>
public static class GpuTextureFormatExtensions
{
    /// <summary>
    /// Gets the texel block size of a format in bytes.
    /// </summary>
    /// <param name="format">The texture format.</param>
    /// <returns>The block size in bytes.</returns>
    public static uint TexelBlockSize(this GpuTextureFormat format) =>
        SDL_GPUTextureFormatTexelBlockSize((SDL_GPUTextureFormat)format);

    /// <summary>
    /// Calculates the total size in bytes of a texture with the given format and dimensions.
    /// </summary>
    /// <param name="format">The texture format.</param>
    /// <param name="width">The width in pixels.</param>
    /// <param name="height">The height in pixels.</param>
    /// <param name="depthOrLayerCount">The depth or layer count.</param>
    /// <returns>The size in bytes.</returns>
    public static uint CalculateSize(this GpuTextureFormat format, uint width, uint height, uint depthOrLayerCount = 1) =>
        SDL_CalculateGPUTextureFormatSize((SDL_GPUTextureFormat)format, width, height, depthOrLayerCount);
    /// <summary>Gets the corresponding SDL pixel format, or Unknown when no conversion exists.</summary>
    public static PixelFormat ToPixelFormat(this GpuTextureFormat format) => (PixelFormat)SDL_GetPixelFormatFromGPUTextureFormat((SDL_GPUTextureFormat)format);
    /// <summary>Gets the corresponding GPU texture format, or Invalid when no conversion exists.</summary>
    public static GpuTextureFormat ToGpuTextureFormat(this PixelFormat format) => (GpuTextureFormat)SDL_GetGPUTextureFormatFromPixelFormat((SDL_PixelFormat)format);
}
