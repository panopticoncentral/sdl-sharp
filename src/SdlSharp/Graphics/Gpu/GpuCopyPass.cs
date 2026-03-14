using SdlSharp.Native;
using static SdlSharp.Native.Gpu;

namespace SdlSharp.Graphics.Gpu;

/// <summary>
/// A managed wrapper around an SDL GPU copy pass (SDL_GPUCopyPass).
/// Copy passes record data transfer operations and are ended by calling <see cref="End"/>.
/// </summary>
public sealed unsafe class GpuCopyPass
{
    /// <summary>
    /// The underlying native SDL_GPUCopyPass pointer.
    /// </summary>
    internal SDL_GPUCopyPass* Handle { get; }

    internal GpuCopyPass(SDL_GPUCopyPass* handle) { Handle = handle; }

    /// <summary>
    /// Uploads data from a transfer buffer to a GPU texture.
    /// </summary>
    /// <param name="source">The transfer buffer source info.</param>
    /// <param name="destination">The texture destination region.</param>
    /// <param name="cycle">Whether to cycle the destination texture to avoid stalls.</param>
    public void UploadToTexture(in SDL_GPUTextureTransferInfo source, in SDL_GPUTextureRegion destination, bool cycle = false)
    {
        fixed (SDL_GPUTextureTransferInfo* s = &source)
        fixed (SDL_GPUTextureRegion* d = &destination)
            SDL_UploadToGPUTexture(Handle, s, d, cycle);
    }

    /// <summary>
    /// Uploads data from a transfer buffer to a GPU buffer.
    /// </summary>
    /// <param name="source">The transfer buffer source location.</param>
    /// <param name="destination">The buffer destination region.</param>
    /// <param name="cycle">Whether to cycle the destination buffer to avoid stalls.</param>
    public void UploadToBuffer(in SDL_GPUTransferBufferLocation source, in SDL_GPUBufferRegion destination, bool cycle = false)
    {
        fixed (SDL_GPUTransferBufferLocation* s = &source)
        fixed (SDL_GPUBufferRegion* d = &destination)
            SDL_UploadToGPUBuffer(Handle, s, d, cycle);
    }

    /// <summary>
    /// Copies data from one GPU texture to another.
    /// </summary>
    /// <param name="source">The source texture location.</param>
    /// <param name="destination">The destination texture location.</param>
    /// <param name="w">Width of the region to copy.</param>
    /// <param name="h">Height of the region to copy.</param>
    /// <param name="d">Depth of the region to copy.</param>
    /// <param name="cycle">Whether to cycle the destination texture to avoid stalls.</param>
    public void CopyTextureToTexture(in SDL_GPUTextureLocation source, in SDL_GPUTextureLocation destination, uint w, uint h, uint d, bool cycle = false)
    {
        fixed (SDL_GPUTextureLocation* s = &source)
        fixed (SDL_GPUTextureLocation* dst = &destination)
            SDL_CopyGPUTextureToTexture(Handle, s, dst, w, h, d, cycle);
    }

    /// <summary>
    /// Copies data from one GPU buffer to another.
    /// </summary>
    /// <param name="source">The source buffer location.</param>
    /// <param name="destination">The destination buffer location.</param>
    /// <param name="size">The number of bytes to copy.</param>
    /// <param name="cycle">Whether to cycle the destination buffer to avoid stalls.</param>
    public void CopyBufferToBuffer(in SDL_GPUBufferLocation source, in SDL_GPUBufferLocation destination, uint size, bool cycle = false)
    {
        fixed (SDL_GPUBufferLocation* s = &source)
        fixed (SDL_GPUBufferLocation* dst = &destination)
            SDL_CopyGPUBufferToBuffer(Handle, s, dst, size, cycle);
    }

    /// <summary>
    /// Downloads data from a GPU texture to a transfer buffer.
    /// </summary>
    /// <param name="source">The source texture region.</param>
    /// <param name="destination">The transfer buffer destination info.</param>
    public void DownloadFromTexture(in SDL_GPUTextureRegion source, in SDL_GPUTextureTransferInfo destination)
    {
        fixed (SDL_GPUTextureRegion* s = &source)
        fixed (SDL_GPUTextureTransferInfo* d = &destination)
            SDL_DownloadFromGPUTexture(Handle, s, d);
    }

    /// <summary>
    /// Downloads data from a GPU buffer to a transfer buffer.
    /// </summary>
    /// <param name="source">The source buffer region.</param>
    /// <param name="destination">The transfer buffer destination location.</param>
    public void DownloadFromBuffer(in SDL_GPUBufferRegion source, in SDL_GPUTransferBufferLocation destination)
    {
        fixed (SDL_GPUBufferRegion* s = &source)
        fixed (SDL_GPUTransferBufferLocation* d = &destination)
            SDL_DownloadFromGPUBuffer(Handle, s, d);
    }

    /// <summary>
    /// Ends the copy pass.
    /// </summary>
    public void End() => SDL_EndGPUCopyPass(Handle);
}
