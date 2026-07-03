using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

using static SdlSharp.Native.Audio;
using static SdlSharp.Native.Common;

namespace SdlSharp.Audio;

/// <summary>
/// A callback invoked with the final mixed audio just before it is sent to a device.
/// It runs on SDL's audio thread — keep it fast and avoid blocking. The buffer is the
/// device's mix in 32-bit float and may be modified in place. Exceptions that escape
/// the callback are swallowed.
/// </summary>
/// <param name="spec">The format of the buffer (always 32-bit float samples).</param>
/// <param name="buffer">The audio samples, modifiable in place.</param>
public delegate void AudioPostmixCallback(in AudioSpec spec, Span<float> buffer);

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

    private GCHandle _postmixHandle;

    private sealed record PostmixHolder(AudioPostmixCallback Callback);

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

    /// <summary>
    /// Binds one or more audio streams to this device in a single call.
    /// </summary>
    /// <param name="streams">The streams to bind.</param>
    public void Bind(params AudioStream[] streams)
    {
        if (streams.Length == 0) return;

        var pointers = stackalloc Native.SDL_AudioStream*[streams.Length];
        for (var i = 0; i < streams.Length; i++)
        {
            pointers[i] = streams[i].Handle;
        }

        Check(SDL_BindAudioStreams(Id, pointers, streams.Length));
    }

    /// <summary>
    /// Unbinds one or more audio streams from whatever devices they are bound to.
    /// </summary>
    /// <param name="streams">The streams to unbind.</param>
    public static void Unbind(params AudioStream[] streams)
    {
        if (streams.Length == 0) return;

        var pointers = stackalloc Native.SDL_AudioStream*[streams.Length];
        for (var i = 0; i < streams.Length; i++)
        {
            pointers[i] = streams[i].Handle;
        }

        SDL_UnbindAudioStreams(pointers, streams.Length);
    }

    /// <summary>Gets whether this device ID refers to a physical (not logical) device.</summary>
    public bool IsPhysical => SDL_IsAudioDevicePhysical(Id);

    /// <summary>Gets whether this device is a playback device (<c>false</c> for a recording device).</summary>
    public bool IsPlayback => SDL_IsAudioDevicePlayback(Id);

    /// <summary>
    /// Gets this device's channel map, or <c>null</c> for the default order.
    /// </summary>
    /// <returns>The channel map array, or <c>null</c>.</returns>
    public int[]? GetChannelMap()
    {
        var map = SDL_GetAudioDeviceChannelMap(Id, out var count);
        if (map == null) return null;
        try
        {
            var result = new int[count];
            new ReadOnlySpan<int>(map, count).CopyTo(result);
            return result;
        }
        finally
        {
            SDL_free(map);
        }
    }

    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
    private static void PostmixCallback(void* userdata, Native.SDL_AudioSpec* spec, float* buffer, int buflen)
    {
        try
        {
            var holder = (PostmixHolder)GCHandle.FromIntPtr((nint)userdata).Target!;
            var managedSpec = new AudioSpec((AudioFormat)spec->format, spec->channels, spec->freq);
            var samples = new Span<float>(buffer, buflen / sizeof(float));
            holder.Callback(in managedSpec, samples);
        }
        catch
        {
            // Must not cross the native boundary on the audio thread.
        }
    }

    /// <summary>
    /// Sets or clears (<c>null</c>) a callback that runs with the device's final mixed
    /// audio just before playback. The callback runs on SDL's audio thread. Setting a
    /// new callback replaces any previous one. See <see cref="AudioPostmixCallback"/>.
    /// </summary>
    /// <param name="callback">The callback, or <c>null</c> to clear.</param>
    public void SetPostmixCallback(AudioPostmixCallback? callback)
    {
        var previous = _postmixHandle;

        if (callback == null)
        {
            Check(SDL_SetAudioPostmixCallback(Id, null, null));
            _postmixHandle = default;
        }
        else
        {
            var newHandle = GCHandle.Alloc(new PostmixHolder(callback));
            if (!SDL_SetAudioPostmixCallback(Id, &PostmixCallback, (void*)GCHandle.ToIntPtr(newHandle)))
            {
                newHandle.Free();
                throw new SdlException();
            }

            _postmixHandle = newHandle;
        }

        if (previous.IsAllocated)
        {
            previous.Free();
        }
    }

    /// <inheritdoc/>
    public void Dispose()
    {
        if (!_ownsHandle && _id.Value != 0 && _postmixHandle.IsAllocated)
        {
            // This wrapper doesn't own the device, so the underlying device stays
            // alive after this call. If we freed the GCHandle without clearing the
            // native postmix callback first, SDL would keep invoking it on the audio
            // thread with a dangling userdata pointer. Ignore the result — Dispose
            // must not throw.
            _ = SDL_SetAudioPostmixCallback(_id, null, null);
        }

        if (_ownsHandle && _id.Value != 0)
        {
            // SDL_CloseAudioDevice stops any callbacks before returning, so it's
            // safe to free the postmix GCHandle afterward in this branch.
            SDL_CloseAudioDevice(_id);
        }

        _id = default;

        if (_postmixHandle.IsAllocated)
        {
            _postmixHandle.Free();
        }
    }
}
