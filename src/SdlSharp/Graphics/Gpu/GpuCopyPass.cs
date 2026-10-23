using SdlSharp.Native;
using static SdlSharp.Native.Gpu;

namespace SdlSharp.Graphics.Gpu;

/// <summary>
/// A managed wrapper around an SDL GPU copy pass (SDL_GPUCopyPass).
/// Copy passes record data transfer operations and are ended by calling <see cref="End"/>.
/// </summary>
public sealed unsafe class GpuCopyPass : IDisposable
{
    /// <summary>
    /// The underlying native SDL_GPUCopyPass pointer.
    /// </summary>
    private readonly GpuCommandBuffer _command;

    internal SDL_GPUCopyPass* Handle
    {
        get
        {
            ObjectDisposedException.ThrowIf(_handle == null, this);
            _command.CheckPass(this);
            return _handle;
        }
    }

    private SDL_GPUCopyPass* _handle;

    internal GpuCopyPass(GpuCommandBuffer command, SDL_GPUCopyPass* handle) { _command = command; command.BeginPass(this); _handle = handle; }

    /// <summary>
    /// Uploads data from a transfer buffer to a GPU texture.
    /// </summary>
    /// <param name="source">The transfer buffer source info.</param>
    /// <param name="destination">The texture destination region.</param>
    /// <param name="cycle">Whether to cycle the destination texture to avoid stalls.</param>
    public void UploadToTexture(in GpuTextureTransferInfo source, in GpuTextureRegion destination, bool cycle = false)
    {
        var s = source.ToNative(_command);
        var d = destination.ToNative(_command);
        SDL_UploadToGPUTexture(Handle, &s, &d, cycle);
    }

    /// <summary>
    /// Uploads data from a transfer buffer to a GPU buffer.
    /// </summary>
    /// <param name="source">The transfer buffer source location.</param>
    /// <param name="destination">The buffer destination region.</param>
    /// <param name="cycle">Whether to cycle the destination buffer to avoid stalls.</param>
    public void UploadToBuffer(in GpuTransferBufferLocation source, in GpuBufferRegion destination, bool cycle = false)
    {
        var s = source.ToNative(_command);
        var d = destination.ToNative(_command);
        SDL_UploadToGPUBuffer(Handle, &s, &d, cycle);
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
    public void CopyTextureToTexture(in GpuTextureLocation source, in GpuTextureLocation destination, uint w, uint h, uint d, bool cycle = false)
    {
        var s = source.ToNative(_command);
        var dst = destination.ToNative(_command);
        SDL_CopyGPUTextureToTexture(Handle, &s, &dst, w, h, d, cycle);
    }

    /// <summary>
    /// Copies data from one GPU buffer to another.
    /// </summary>
    /// <param name="source">The source buffer location.</param>
    /// <param name="destination">The destination buffer location.</param>
    /// <param name="size">The number of bytes to copy.</param>
    /// <param name="cycle">Whether to cycle the destination buffer to avoid stalls.</param>
    public void CopyBufferToBuffer(in GpuBufferLocation source, in GpuBufferLocation destination, uint size, bool cycle = false)
    {
        var s = source.ToNative(_command);
        var dst = destination.ToNative(_command);
        SDL_CopyGPUBufferToBuffer(Handle, &s, &dst, size, cycle);
    }

    /// <summary>
    /// Downloads data from a GPU texture to a transfer buffer.
    /// </summary>
    /// <param name="source">The source texture region.</param>
    /// <param name="destination">The transfer buffer destination info.</param>
    public void DownloadFromTexture(in GpuTextureRegion source, in GpuTextureTransferInfo destination)
    {
        var s = source.ToNative(_command);
        var d = destination.ToNative(_command);
        SDL_DownloadFromGPUTexture(Handle, &s, &d);
    }

    /// <summary>
    /// Downloads data from a GPU buffer to a transfer buffer.
    /// </summary>
    /// <param name="source">The source buffer region.</param>
    /// <param name="destination">The transfer buffer destination location.</param>
    public void DownloadFromBuffer(in GpuBufferRegion source, in GpuTransferBufferLocation destination)
    {
        var s = source.ToNative(_command);
        var d = destination.ToNative(_command);
        SDL_DownloadFromGPUBuffer(Handle, &s, &d);
    }

    /// <summary>
    /// Ends the copy pass.
    /// </summary>
    public void End()
    {
        var handle = Handle;
        _command.EndPass(this);
        _handle = null;
        SDL_EndGPUCopyPass(handle);
    }
    /// <summary>Ends this pass once.</summary>
    public void Dispose() { if (_handle != null) End(); }
}
