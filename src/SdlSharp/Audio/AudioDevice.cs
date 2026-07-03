using System.Runtime.InteropServices;

using static SdlSharp.Native.Audio;
using static SdlSharp.Native.Common;

namespace SdlSharp.Audio;

/// <summary>
/// Represents an opened SDL audio device (logical device).
/// </summary>
public sealed unsafe class AudioDevice : IDisposable
{
    private readonly bool _ownsHandle;

    internal Native.SDL_AudioDeviceID Id
    {
        get
        {
            ObjectDisposedException.ThrowIf(_id.Value == 0, this);
            return _id;
        }
    }

    private Native.SDL_AudioDeviceID _id;

    internal AudioDevice(Native.SDL_AudioDeviceID id, bool ownsHandle = true)
    {
        _id = id;
        _ownsHandle = ownsHandle;
    }

    /// <summary>
    /// Opens the default playback device with optional format hint.
    /// </summary>
    /// <param name="spec">Desired audio format, or null for system default.</param>
    /// <returns>A new audio device.</returns>
    public static AudioDevice OpenPlayback(AudioSpec? spec = null) =>
        Open(SDL_AUDIO_DEVICE_DEFAULT_PLAYBACK, spec);

    /// <summary>
    /// Opens the default recording device with optional format hint.
    /// </summary>
    /// <param name="spec">Desired audio format, or null for system default.</param>
    /// <returns>A new audio device.</returns>
    public static AudioDevice OpenRecording(AudioSpec? spec = null) =>
        Open(SDL_AUDIO_DEVICE_DEFAULT_RECORDING, spec);

    /// <summary>
    /// Opens a specific audio device.
    /// </summary>
    /// <param name="deviceId">The device ID to open (physical or logical).</param>
    /// <param name="spec">Desired audio format, or null for system default.</param>
    /// <returns>A new audio device.</returns>
    public static AudioDevice Open(uint deviceId, AudioSpec? spec = null) =>
        Open(new Native.SDL_AudioDeviceID(deviceId), spec);

    private static AudioDevice Open(Native.SDL_AudioDeviceID devid, AudioSpec? spec)
    {
        Native.SDL_AudioSpec nativeSpec;
        Native.SDL_AudioSpec* specPtr = null;
        if (spec is { } s)
        {
            nativeSpec = new Native.SDL_AudioSpec
            {
                format = (Native.SDL_AudioFormat)s.Format,
                channels = s.Channels,
                freq = s.Frequency
            };
            specPtr = &nativeSpec;
        }

        var id = SDL_OpenAudioDevice(devid, specPtr);
        if (id.Value == 0) throw new SdlException();
        return new AudioDevice(id);
    }

    /// <summary>
    /// Gets the names and IDs of currently connected playback devices.
    /// </summary>
    public static (uint Id, string? Name)[] GetPlaybackDevices()
    {
        var ids = SDL_GetAudioPlaybackDevices(out var count);
        if (ids == null) return [];
        try
        {
            return GetDeviceInfos(ids, count);
        }
        finally
        {
            SDL_free(ids);
        }
    }

    /// <summary>
    /// Gets the names and IDs of currently connected recording devices.
    /// </summary>
    public static (uint Id, string? Name)[] GetRecordingDevices()
    {
        var ids = SDL_GetAudioRecordingDevices(out var count);
        if (ids == null) return [];
        try
        {
            return GetDeviceInfos(ids, count);
        }
        finally
        {
            SDL_free(ids);
        }
    }

    private static (uint Id, string? Name)[] GetDeviceInfos(Native.SDL_AudioDeviceID* ids, int count)
    {
        var result = new (uint, string?)[count];
        for (var i = 0; i < count; i++)
        {
            var id = ids[i];
            result[i] = (id.Value, Marshal.PtrToStringUTF8((nint)SDL_GetAudioDeviceName(id)));
        }
        return result;
    }

    /// <summary>
    /// Gets the number of built-in audio drivers.
    /// </summary>
    public static int NumAudioDrivers => SDL_GetNumAudioDrivers();

    /// <summary>
    /// Gets the name of a built-in audio driver.
    /// </summary>
    /// <param name="index">The index of the audio driver.</param>
    public static string? GetAudioDriver(int index) =>
        Marshal.PtrToStringUTF8((nint)SDL_GetAudioDriver(index));

    /// <summary>
    /// Gets the name of the currently initialized audio driver.
    /// </summary>
    public static string? CurrentAudioDriver =>
        Marshal.PtrToStringUTF8((nint)SDL_GetCurrentAudioDriver());

    /// <summary>
    /// Gets the current audio format of this device.
    /// </summary>
    /// <param name="sampleFrames">Receives the device buffer size in sample frames, if not null.</param>
    /// <returns>The current audio spec of this device.</returns>
    public AudioSpec GetFormat(out int sampleFrames)
    {
        Native.SDL_AudioSpec spec;
        int frames;
        Check(SDL_GetAudioDeviceFormat(Id, &spec, &frames));
        sampleFrames = frames;
        return new AudioSpec((AudioFormat)spec.format, spec.channels, spec.freq);
    }

    /// <summary>
    /// Pauses audio playback on this device.
    /// </summary>
    public void Pause() => Check(SDL_PauseAudioDevice(Id));

    /// <summary>
    /// Resumes audio playback on this device.
    /// </summary>
    public void Resume() => Check(SDL_ResumeAudioDevice(Id));

    /// <summary>
    /// Gets whether this device is currently paused.
    /// </summary>
    public bool IsPaused => SDL_AudioDevicePaused(Id);

    /// <summary>
    /// Gets or sets the gain (volume) of this device.
    /// 1.0 is no change, 0.0 is silence.
    /// </summary>
    public float Gain
    {
        get => SDL_GetAudioDeviceGain(Id);
        set => Check(SDL_SetAudioDeviceGain(Id, value));
    }

    /// <summary>
    /// Binds an audio stream to this device.
    /// </summary>
    /// <param name="stream">The audio stream to bind.</param>
    public void Bind(AudioStream stream) =>
        Check(SDL_BindAudioStream(Id, stream.Handle));

    /// <inheritdoc/>
    public void Dispose()
    {
        if (_ownsHandle && _id.Value != 0)
        {
            SDL_CloseAudioDevice(_id);
        }
        _id = default;
    }
}
