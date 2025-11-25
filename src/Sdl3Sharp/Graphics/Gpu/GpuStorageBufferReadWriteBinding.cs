using static Sdl3Sharp.Native.Gpu;

namespace Sdl3Sharp.Graphics.Gpu;

/// <summary>
/// Describes storage buffer binding for compute passes.
/// </summary>
public struct GpuStorageBufferReadWriteBinding
{
    /// <summary>
    /// The buffer to bind.
    /// </summary>
    public GpuBuffer? Buffer { get; set; }

    /// <summary>
    /// True cycles the buffer if it is already bound.
    /// </summary>
    public bool Cycle { get; set; }

    internal unsafe SDL_GPUStorageBufferReadWriteBinding ToNative()
    {
        return new SDL_GPUStorageBufferReadWriteBinding
        {
            buffer = Buffer != null ? Buffer.Handle : null,
            cycle = Cycle
        };
    }
}
