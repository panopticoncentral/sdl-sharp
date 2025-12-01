using NativeAudio = Sdl3Sharp.Native.Audio;

using static Sdl3Sharp.Native.Common;
using static Sdl3Sharp.Native.StdInc;

namespace Sdl3Sharp.Audio;

/// <summary>
/// Represents an SDL audio device for playback or recording.
/// </summary>
public sealed unsafe class AudioDevice : IDisposable
{
    /// <summary>
    /// Occurs when an audio device has been added to the system.
    /// </summary>
    public static event EventHandler<AudioDeviceEventArgs>? Added;

    /// <summary>
    /// Occurs when an audio device has been removed from the system.
    /// </summary>
    public static event EventHandler<AudioDeviceEventArgs>? Removed;

    /// <summary>
    /// Occurs when an audio device's format has been changed by the system.
    /// </summary>
    public static event EventHandler<AudioDeviceEventArgs>? FormatChanged;

    internal static void DispatchEvent(Event e)
    {
        switch (e.Type)
        {
            case EventType.AudioDeviceAdded:
                Added?.Invoke(null, (AudioDeviceEventArgs)e.TranslateEvent());
                break;
            case EventType.AudioDeviceRemoved:
                Removed?.Invoke(null, (AudioDeviceEventArgs)e.TranslateEvent());
                break;
            case EventType.AudioDeviceFormatChanged:
                FormatChanged?.Invoke(null, (AudioDeviceEventArgs)e.TranslateEvent());
                break;
        }
    }

    private bool _disposed;
    private readonly bool _ownsHandle;

    /// <summary>
    /// Gets the underlying SDL audio device ID.
    /// </summary>
    public NativeAudio.SDL_AudioDeviceID Id { get; private set; }

    /// <summary>
    /// Gets the name of this audio device.
    /// </summary>
    public string? Name => NativeAudio.SDL_GetAudioDeviceName(Id);

    /// <summary>
    /// Gets whether this is a physical device (as opposed to a logical device).
    /// </summary>
    public bool IsPhysical => NativeAudio.SDL_IsAudioDevicePhysical(Id);

    /// <summary>
    /// Gets whether this is a playback device (as opposed to a recording device).
    /// </summary>
    public bool IsPlayback => NativeAudio.SDL_IsAudioDevicePlayback(Id);

    /// <summary>
    /// Gets whether this is a recording device (as opposed to a playback device).
    /// </summary>
    public bool IsRecording => !IsPlayback;

    /// <summary>
    /// Gets or sets whether this audio device is paused.
    /// </summary>
    public bool IsPaused
    {
        get
        {
            ThrowIfDisposed();
            return NativeAudio.SDL_AudioDevicePaused(Id);
        }
        set
        {
            ThrowIfDisposed();
            _ = value ? CheckErrorBool(NativeAudio.SDL_PauseAudioDevice(Id)) : CheckErrorBool(NativeAudio.SDL_ResumeAudioDevice(Id));
        }
    }

    /// <summary>
    /// Gets or sets the gain (volume) of this audio device.
    /// </summary>
    /// <remarks>
    /// A gain of 1.0 means no change, 0.0 means silence.
    /// Only logical devices can have their gain changed.
    /// </remarks>
    public float Gain
    {
        get
        {
            ThrowIfDisposed();
            var gain = NativeAudio.SDL_GetAudioDeviceGain(Id);
            return gain < 0 ? throw new SdlException() : gain;
        }
        set
        {
            ThrowIfDisposed();
            _ = CheckErrorBool(NativeAudio.SDL_SetAudioDeviceGain(Id, value));
        }
    }

    /// <summary>
    /// Gets the current audio format of this device.
    /// </summary>
    public AudioSpec Format
    {
        get
        {
            ThrowIfDisposed();
            NativeAudio.SDL_AudioSpec spec;
            _ = CheckErrorBool(NativeAudio.SDL_GetAudioDeviceFormat(Id, &spec, null));
            return new AudioSpec((AudioFormat)spec.format, spec.channels, spec.freq);
        }
    }

    /// <summary>
    /// Gets the current buffer size in sample frames.
    /// </summary>
    public int BufferSampleFrames
    {
        get
        {
            ThrowIfDisposed();
            NativeAudio.SDL_AudioSpec spec;
            int sampleFrames;
            _ = CheckErrorBool(NativeAudio.SDL_GetAudioDeviceFormat(Id, &spec, &sampleFrames));
            return sampleFrames;
        }
    }

    /// <summary>
    /// Gets the channel map of this device, or null if using default channel layout.
    /// </summary>
    public int[]? ChannelMap
    {
        get
        {
            ThrowIfDisposed();
            int count;
            var map = NativeAudio.SDL_GetAudioDeviceChannelMap(Id, &count);
            if (map == null)
            {
                return null;
            }

            var result = new int[count];
            for (var i = 0; i < count; i++)
            {
                result[i] = map[i];
            }

            SDL_free(map);
            return result;
        }
    }

    /// <summary>
    /// Opens a specific audio device or the default device.
    /// </summary>
    /// <param name="deviceId">The device to open, or use DefaultPlayback/DefaultRecording for defaults.</param>
    /// <param name="spec">The requested audio format, or null to use device defaults.</param>
    /// <returns>A new AudioDevice instance.</returns>
    public static AudioDevice Open(NativeAudio.SDL_AudioDeviceID deviceId, AudioSpec? spec = null)
    {
        NativeAudio.SDL_AudioSpec nativeSpec;
        NativeAudio.SDL_AudioSpec* specPtr = null;

        if (spec.HasValue)
        {
            nativeSpec = new NativeAudio.SDL_AudioSpec
            {
                format = (NativeAudio.SDL_AudioFormat)spec.Value.Format,
                channels = spec.Value.Channels,
                freq = spec.Value.Frequency
            };
            specPtr = &nativeSpec;
        }

        NativeAudio.SDL_AudioDeviceID id = NativeAudio.SDL_OpenAudioDevice(deviceId, specPtr);
        return id.Value == 0 ? throw new SdlException() : new AudioDevice(id, ownsHandle: true);
    }

    /// <summary>
    /// Opens the default playback device.
    /// </summary>
    /// <param name="spec">The requested audio format, or null to use device defaults.</param>
    /// <returns>A new AudioDevice instance.</returns>
    public static AudioDevice OpenDefaultPlayback(AudioSpec? spec = null)
    {
        return Open(NativeAudio.SDL_AUDIO_DEVICE_DEFAULT_PLAYBACK, spec);
    }

    /// <summary>
    /// Opens the default recording device.
    /// </summary>
    /// <param name="spec">The requested audio format, or null to use device defaults.</param>
    /// <returns>A new AudioDevice instance.</returns>
    public static AudioDevice OpenDefaultRecording(AudioSpec? spec = null)
    {
        return Open(NativeAudio.SDL_AUDIO_DEVICE_DEFAULT_RECORDING, spec);
    }

    /// <summary>
    /// Gets all currently connected playback devices.
    /// </summary>
    public static AudioDevice[] GetPlaybackDevices()
    {
        int count;
        NativeAudio.SDL_AudioDeviceID* devices = NativeAudio.SDL_GetAudioPlaybackDevices(&count);
        if (devices == null)
        {
            return [];
        }

        var result = new AudioDevice[count];
        for (var i = 0; i < count; i++)
        {
            result[i] = new AudioDevice(devices[i], ownsHandle: false);
        }

        SDL_free(devices);
        return result;
    }

    /// <summary>
    /// Gets all currently connected recording devices.
    /// </summary>
    public static AudioDevice[] GetRecordingDevices()
    {
        int count;
        NativeAudio.SDL_AudioDeviceID* devices = NativeAudio.SDL_GetAudioRecordingDevices(&count);
        if (devices == null)
        {
            return [];
        }

        var result = new AudioDevice[count];
        for (var i = 0; i < count; i++)
        {
            result[i] = new AudioDevice(devices[i], ownsHandle: false);
        }

        SDL_free(devices);
        return result;
    }

    /// <summary>
    /// Creates an AudioDevice wrapper for an existing device ID.
    /// </summary>
    /// <param name="id">The SDL audio device ID.</param>
    /// <param name="ownsHandle">Whether this instance should close the device when disposed.</param>
    internal AudioDevice(NativeAudio.SDL_AudioDeviceID id, bool ownsHandle)
    {
        Id = id;
        _ownsHandle = ownsHandle;
    }

    /// <summary>
    /// Pauses audio playback on this device.
    /// </summary>
    public void Pause()
    {
        ThrowIfDisposed();
        _ = CheckErrorBool(NativeAudio.SDL_PauseAudioDevice(Id));
    }

    /// <summary>
    /// Resumes audio playback on this device.
    /// </summary>
    public void Resume()
    {
        ThrowIfDisposed();
        _ = CheckErrorBool(NativeAudio.SDL_ResumeAudioDevice(Id));
    }

    /// <summary>
    /// Binds an audio stream to this device.
    /// </summary>
    /// <param name="stream">The audio stream to bind.</param>
    public void BindStream(AudioStream stream)
    {
        ThrowIfDisposed();
        _ = CheckErrorBool(NativeAudio.SDL_BindAudioStream(Id, stream.Handle));
    }

    /// <summary>
    /// Binds multiple audio streams to this device atomically.
    /// </summary>
    /// <param name="streams">The audio streams to bind.</param>
    public void BindStreams(params AudioStream[] streams)
    {
        ThrowIfDisposed();

        if (streams.Length == 0)
        {
            return;
        }

        NativeAudio.SDL_AudioStream** handles = stackalloc NativeAudio.SDL_AudioStream*[streams.Length];
        for (var i = 0; i < streams.Length; i++)
        {
            handles[i] = streams[i].Handle;
        }

        _ = CheckErrorBool(NativeAudio.SDL_BindAudioStreams(Id, handles, streams.Length));
    }

    /// <inheritdoc/>
    public void Dispose()
    {
        if (_disposed)
        {
            return;
        }

        if (_ownsHandle && Id.Value != 0)
        {
            NativeAudio.SDL_CloseAudioDevice(Id);
            Id = default;
        }

        _disposed = true;
    }

    /// <inheritdoc/>
    public override string ToString()
    {
        return Name ?? $"AudioDevice {Id.Value}";
    }

    private void ThrowIfDisposed()
    {
        ObjectDisposedException.ThrowIf(_disposed, this);
    }
}
