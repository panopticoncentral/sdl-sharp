using static Sdl3Sharp.Native.Gpu;

namespace Sdl3Sharp.Graphics.Gpu;

/// <summary>
/// Describes a location in a buffer.
/// </summary>
public struct GpuBufferLocation
{
    /// <summary>
    /// The buffer.
    /// </summary>
    public GpuBuffer? Buffer { get; set; }

    /// <summary>
    /// The starting byte within the buffer.
    /// </summary>
    public uint Offset { get; set; }

    internal unsafe SDL_GPUBufferLocation ToNative()
    {
        return new SDL_GPUBufferLocation
        {
            buffer = Buffer != null ? Buffer.Handle : null,
            offset = Offset
        };
    }
}