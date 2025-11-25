using static Sdl3Sharp.Native.Gpu;

namespace Sdl3Sharp.Graphics.Gpu;

/// <summary>
/// Represents a GPU copy pass for data transfer operations.
/// </summary>
public sealed unsafe class GpuCopyPass
{
    private readonly GpuCommandBuffer _commandBuffer;

    /// <summary>
    /// Gets the underlying SDL_GPUCopyPass pointer.
    /// </summary>
    public SDL_GPUCopyPass* Handle { get; private set; }

    /// <summary>
    /// Initializes a new instance wrapping an existing SDL_GPUCopyPass pointer.
    /// </summary>
    /// <param name="handle">The SDL_GPUCopyPass pointer.</param>
    /// <param name="commandBuffer">The command buffer that created this copy pass.</param>
    internal GpuCopyPass(SDL_GPUCopyPass* handle, GpuCommandBuffer commandBuffer)
    {
        Handle = handle;
        _commandBuffer = commandBuffer;
    }

    /// <summary>
    /// Uploads data from a transfer buffer to a texture.
    /// </summary>
    /// <param name="source">The source transfer buffer with image layout information.</param>
    /// <param name="destination">The destination texture region.</param>
    /// <param name="cycle">If true, cycles the texture if the texture is bound, otherwise overwrites the data.</param>
    public void UploadToTexture(GpuTextureTransferInfo source, GpuTextureRegion destination, bool cycle)
    {
        var nativeSource = source.ToNative();
        var nativeDestination = destination.ToNative();
        SDL_UploadToGPUTexture(Handle, &nativeSource, &nativeDestination, cycle);
    }

    /// <summary>
    /// Uploads data from a transfer buffer to a buffer.
    /// </summary>
    /// <param name="source">The source transfer buffer with offset.</param>
    /// <param name="destination">The destination buffer with offset and size.</param>
    /// <param name="cycle">If true, cycles the buffer if it is already bound, otherwise overwrites the data.</param>
    public void UploadToBuffer(GpuTransferBufferLocation source, GpuBufferRegion destination, bool cycle)
    {
        var nativeSource = source.ToNative();
        var nativeDestination = destination.ToNative();
        SDL_UploadToGPUBuffer(Handle, &nativeSource, &nativeDestination, cycle);
    }

    /// <summary>
    /// Performs a texture-to-texture copy.
    /// </summary>
    /// <param name="source">A source texture location.</param>
    /// <param name="destination">A destination texture location.</param>
    /// <param name="width">The width of the region to copy.</param>
    /// <param name="height">The height of the region to copy.</param>
    /// <param name="depth">The depth of the region to copy.</param>
    /// <param name="cycle">If true, cycles the destination texture if bound, otherwise overwrites the data.</param>
    public void CopyTextureToTexture(GpuTextureLocation source, GpuTextureLocation destination, uint width, uint height, uint depth, bool cycle)
    {
        var nativeSource = source.ToNative();
        var nativeDestination = destination.ToNative();
        SDL_CopyGPUTextureToTexture(Handle, &nativeSource, &nativeDestination, width, height, depth, cycle);
    }

    /// <summary>
    /// Performs a buffer-to-buffer copy.
    /// </summary>
    /// <param name="source">The buffer and offset to copy from.</param>
    /// <param name="destination">The buffer and offset to copy to.</param>
    /// <param name="size">The length of the buffer to copy.</param>
    /// <param name="cycle">If true, cycles the destination buffer if bound, otherwise overwrites the data.</param>
    public void CopyBufferToBuffer(GpuBufferLocation source, GpuBufferLocation destination, uint size, bool cycle)
    {
        var nativeSource = source.ToNative();
        var nativeDestination = destination.ToNative();
        SDL_CopyGPUBufferToBuffer(Handle, &nativeSource, &nativeDestination, size, cycle);
    }

    /// <summary>
    /// Copies data from a texture to a transfer buffer on the GPU timeline.
    /// </summary>
    /// <param name="source">The source texture region.</param>
    /// <param name="destination">The destination transfer buffer with image layout information.</param>
    public void DownloadFromTexture(GpuTextureRegion source, GpuTextureTransferInfo destination)
    {
        var nativeSource = source.ToNative();
        var nativeDestination = destination.ToNative();
        SDL_DownloadFromGPUTexture(Handle, &nativeSource, &nativeDestination);
    }

    /// <summary>
    /// Copies data from a buffer to a transfer buffer on the GPU timeline.
    /// </summary>
    /// <param name="source">The source buffer with offset and size.</param>
    /// <param name="destination">The destination transfer buffer with offset.</param>
    public void DownloadFromBuffer(GpuBufferRegion source, GpuTransferBufferLocation destination)
    {
        var nativeSource = source.ToNative();
        var nativeDestination = destination.ToNative();
        SDL_DownloadFromGPUBuffer(Handle, &nativeSource, &nativeDestination);
    }

    /// <summary>
    /// Ends the copy pass.
    /// </summary>
    public void End()
    {
        SDL_EndGPUCopyPass(Handle);
        Handle = null;
    }
}
