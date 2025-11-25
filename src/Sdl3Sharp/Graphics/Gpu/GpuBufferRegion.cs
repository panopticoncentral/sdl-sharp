using static Sdl3Sharp.Native.Gpu;

namespace Sdl3Sharp.Graphics.Gpu;

/// <summary>
/// Describes a region of a buffer.
/// </summary>
public struct GpuBufferRegion
{
    /// <summary>
    /// The buffer.
    /// </summary>
    public GpuBuffer? Buffer { get; set; }

    /// <summary>
    /// The starting byte within the buffer.
    /// </summary>
    public uint Offset { get; set; }

    /// <summary>
    /// The size in bytes of the region.
    /// </summary>
    public uint Size { get; set; }

    internal unsafe SDL_GPUBufferRegion ToNative()
    {
        return new SDL_GPUBufferRegion
        {
            buffer = Buffer != null ? Buffer.Handle : null,
            offset = Offset,
            size = Size
        };
    }
}