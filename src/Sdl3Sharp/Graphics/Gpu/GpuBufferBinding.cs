using static Sdl3Sharp.Native.Gpu;

namespace Sdl3Sharp.Graphics.Gpu;

/// <summary>
/// Describes a buffer binding.
/// </summary>
public struct GpuBufferBinding
{
    /// <summary>
    /// The buffer to bind.
    /// </summary>
    public GpuBuffer? Buffer { get; set; }

    /// <summary>
    /// The starting byte of the data to bind in the buffer.
    /// </summary>
    public uint Offset { get; set; }

    internal unsafe SDL_GPUBufferBinding ToNative()
    {
        return new SDL_GPUBufferBinding
        {
            buffer = Buffer != null ? Buffer.Handle : null,
            offset = Offset
        };
    }
}
