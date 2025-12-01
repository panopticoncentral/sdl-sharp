using static Sdl3Sharp.Native.Events;

namespace Sdl3Sharp.Input;

/// <summary>
/// Event arguments for camera device events.
/// </summary>
public sealed class CameraDeviceEventArgs : SdlEventArgs
{
    /// <summary>
    /// Gets the camera instance ID.
    /// </summary>
    public uint CameraId { get; }

    internal CameraDeviceEventArgs(SDL_CameraDeviceEvent e) : base(e.timestamp)
    {
        CameraId = e.which;
    }
}
