using static Sdl3Sharp.Native.Events;

namespace Sdl3Sharp.Input;

/// <summary>
/// Data for camera device events.
/// </summary>
public readonly struct CameraDeviceEventData
{
    private readonly SDL_CameraDeviceEvent _event;

    internal CameraDeviceEventData(SDL_CameraDeviceEvent e) => _event = e;

    /// <summary>Gets the camera device ID.</summary>
    public uint DeviceId => _event.which;
}
