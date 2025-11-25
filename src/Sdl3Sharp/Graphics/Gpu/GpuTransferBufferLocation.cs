using static Sdl3Sharp.Native.Gpu;

namespace Sdl3Sharp.Graphics.Gpu;

/// <summary>
/// Describes a location in a transfer buffer.
/// </summary>
public struct GpuTransferBufferLocation
{
    /// <summary>
    /// The transfer buffer used in the transfer operation.
    /// </summary>
    public GpuTransferBuffer? TransferBuffer { get; set; }

    /// <summary>
    /// The starting byte of the buffer data in the transfer buffer.
    /// </summary>
    public uint Offset { get; set; }

    internal unsafe SDL_GPUTransferBufferLocation ToNative()
    {
        return new SDL_GPUTransferBufferLocation
        {
            transfer_buffer = TransferBuffer != null ? TransferBuffer.Handle : null,
            offset = Offset
        };
    }
}