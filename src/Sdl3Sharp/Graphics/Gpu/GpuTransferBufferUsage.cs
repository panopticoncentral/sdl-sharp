namespace Sdl3Sharp.Graphics.Gpu;

/// <summary>
/// Specifies how a transfer buffer is intended to be used by the client.
/// </summary>
public enum GpuTransferBufferUsage
{
    /// <summary>Transfer buffer is used for uploading data to the GPU.</summary>
    Upload,

    /// <summary>Transfer buffer is used for downloading data from the GPU.</summary>
    Download
}
