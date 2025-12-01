using static Sdl3Sharp.Native.Events;

namespace Sdl3Sharp.Audio;

/// <summary>
/// Event arguments for audio device events.
/// </summary>
public sealed class AudioDeviceEventArgs : SdlEventArgs
{
    /// <summary>
    /// Gets the audio device ID.
    /// </summary>
    public uint DeviceId { get; }

    /// <summary>
    /// Gets a value indicating whether this is a recording device (false means playback).
    /// </summary>
    public bool IsRecording { get; }

    internal AudioDeviceEventArgs(SDL_AudioDeviceEvent e) : base(e.timestamp)
    {
        DeviceId = e.which;
        IsRecording = e.recording;
    }
}
