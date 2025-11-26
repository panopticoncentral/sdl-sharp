using static Sdl3Sharp.Native.Events;

namespace Sdl3Sharp.Audio;

/// <summary>
/// Data for audio device events.
/// </summary>
public readonly struct AudioDeviceEventData
{
    private readonly SDL_AudioDeviceEvent _event;

    internal AudioDeviceEventData(SDL_AudioDeviceEvent e) => _event = e;

    /// <summary>Gets the audio device ID.</summary>
    public uint DeviceId => _event.which;

    /// <summary>Gets a value indicating whether this is a recording device (true) or playback device (false).</summary>
    public bool IsRecording => _event.recording;
}
