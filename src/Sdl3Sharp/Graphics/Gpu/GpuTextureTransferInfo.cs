using static Sdl3Sharp.Native.Gpu;

namespace Sdl3Sharp.Graphics.Gpu;

/// <summary>
/// Describes parameters related to transferring data to or from a texture.
/// </summary>
public struct GpuTextureTransferInfo
{
    /// <summary>
    /// The transfer buffer used in the transfer operation.
    /// </summary>
    public GpuTransferBuffer? TransferBuffer { get; set; }

    /// <summary>
    /// The starting byte of the image data in the transfer buffer.
    /// </summary>
    public uint Offset { get; set; }

    /// <summary>
    /// The number of pixels from one row to the next.
    /// </summary>
    public uint PixelsPerRow { get; set; }

    /// <summary>
    /// The number of rows from one layer/depth-slice to the next.
    /// </summary>
    public uint RowsPerLayer { get; set; }

    internal unsafe SDL_GPUTextureTransferInfo ToNative()
    {
        return new SDL_GPUTextureTransferInfo
        {
            transfer_buffer = TransferBuffer != null ? TransferBuffer.Handle : null,
            offset = Offset,
            pixels_per_row = PixelsPerRow,
            rows_per_layer = RowsPerLayer
        };
    }
}