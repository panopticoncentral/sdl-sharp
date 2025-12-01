using NativeAudio = Sdl3Sharp.Native.Audio;

using static Sdl3Sharp.Native.Common;
using static Sdl3Sharp.Native.StdInc;
using Sdl3Sharp.Native;

namespace Sdl3Sharp.Audio;

/// <summary>
/// Represents an SDL audio stream for converting, buffering, and streaming audio data.
/// </summary>
public sealed unsafe class AudioStream : IDisposable
{
    private bool _disposed;
    private readonly bool _ownsHandle;

    /// <summary>
    /// Gets the underlying SDL_AudioStream pointer.
    /// </summary>
    public NativeAudio.SDL_AudioStream* Handle { get; private set; }

    /// <summary>
    /// Gets the properties associated with this audio stream.
    /// </summary>
    public PropertyGroup Properties
    {
        get
        {
            ThrowIfDisposed();
            Properties.SDL_PropertiesID id = NativeAudio.SDL_GetAudioStreamProperties(Handle);
            return id.Value == 0 ? throw new SdlException() : new PropertyGroup(id, ownsHandle: false);
        }
    }

    /// <summary>
    /// Gets or sets the input format of this audio stream.
    /// </summary>
    public AudioSpec InputFormat
    {
        get
        {
            ThrowIfDisposed();
            NativeAudio.SDL_AudioSpec srcSpec;
            _ = CheckErrorBool(NativeAudio.SDL_GetAudioStreamFormat(Handle, &srcSpec, null));
            return new AudioSpec((AudioFormat)srcSpec.format, srcSpec.channels, srcSpec.freq);
        }
        set
        {
            ThrowIfDisposed();
            var spec = new NativeAudio.SDL_AudioSpec
            {
                format = (NativeAudio.SDL_AudioFormat)value.Format,
                channels = value.Channels,
                freq = value.Frequency
            };
            _ = CheckErrorBool(NativeAudio.SDL_SetAudioStreamFormat(Handle, &spec, null));
        }
    }

    /// <summary>
    /// Gets or sets the output format of this audio stream.
    /// </summary>
    public AudioSpec OutputFormat
    {
        get
        {
            ThrowIfDisposed();
            NativeAudio.SDL_AudioSpec dstSpec;
            _ = CheckErrorBool(NativeAudio.SDL_GetAudioStreamFormat(Handle, null, &dstSpec));
            return new AudioSpec((AudioFormat)dstSpec.format, dstSpec.channels, dstSpec.freq);
        }
        set
        {
            ThrowIfDisposed();
            var spec = new NativeAudio.SDL_AudioSpec
            {
                format = (NativeAudio.SDL_AudioFormat)value.Format,
                channels = value.Channels,
                freq = value.Frequency
            };
            _ = CheckErrorBool(NativeAudio.SDL_SetAudioStreamFormat(Handle, null, &spec));
        }
    }

    /// <summary>
    /// Gets or sets the frequency ratio of this audio stream.
    /// </summary>
    /// <remarks>
    /// A ratio of 1.0 means normal speed, greater than 1.0 means faster playback and higher pitch,
    /// less than 1.0 means slower playback and lower pitch. Must be between 0.01 and 100.
    /// </remarks>
    public float FrequencyRatio
    {
        get
        {
            ThrowIfDisposed();
            var ratio = NativeAudio.SDL_GetAudioStreamFrequencyRatio(Handle);
            return ratio == 0.0f ? throw new SdlException() : ratio;
        }
        set
        {
            ThrowIfDisposed();
            _ = CheckErrorBool(NativeAudio.SDL_SetAudioStreamFrequencyRatio(Handle, value));
        }
    }

    /// <summary>
    /// Gets or sets the gain (volume) of this audio stream.
    /// </summary>
    /// <remarks>
    /// A gain of 1.0 means no change, 0.0 means silence.
    /// </remarks>
    public float Gain
    {
        get
        {
            ThrowIfDisposed();
            var gain = NativeAudio.SDL_GetAudioStreamGain(Handle);
            return gain < 0 ? throw new SdlException() : gain;
        }
        set
        {
            ThrowIfDisposed();
            _ = CheckErrorBool(NativeAudio.SDL_SetAudioStreamGain(Handle, value));
        }
    }

    /// <summary>
    /// Gets the number of bytes available to read from the stream.
    /// </summary>
    public int Available
    {
        get
        {
            ThrowIfDisposed();
            return CheckErrorNegativeOne(NativeAudio.SDL_GetAudioStreamAvailable(Handle));
        }
    }

    /// <summary>
    /// Gets the number of bytes currently queued in the stream.
    /// </summary>
    public int Queued
    {
        get
        {
            ThrowIfDisposed();
            return CheckErrorNegativeOne(NativeAudio.SDL_GetAudioStreamQueued(Handle));
        }
    }

    /// <summary>
    /// Gets the audio device this stream is bound to, or null if not bound.
    /// </summary>
    public AudioDevice? BoundDevice
    {
        get
        {
            ThrowIfDisposed();
            NativeAudio.SDL_AudioDeviceID id = NativeAudio.SDL_GetAudioStreamDevice(Handle);
            return id.Value == 0 ? null : new AudioDevice(id, ownsHandle: false);
        }
    }

    /// <summary>
    /// Gets or sets whether the associated audio device is paused.
    /// </summary>
    public bool IsDevicePaused
    {
        get
        {
            ThrowIfDisposed();
            return NativeAudio.SDL_AudioStreamDevicePaused(Handle);
        }
        set
        {
            ThrowIfDisposed();
            _ = value
                ? CheckErrorBool(NativeAudio.SDL_PauseAudioStreamDevice(Handle))
                : CheckErrorBool(NativeAudio.SDL_ResumeAudioStreamDevice(Handle));
        }
    }

    /// <summary>
    /// Creates a new audio stream for converting audio between formats.
    /// </summary>
    /// <param name="inputFormat">The format of the input audio data.</param>
    /// <param name="outputFormat">The format of the output audio data.</param>
    /// <returns>A new AudioStream instance.</returns>
    public static AudioStream Create(AudioSpec inputFormat, AudioSpec outputFormat)
    {
        var srcSpec = new NativeAudio.SDL_AudioSpec
        {
            format = (NativeAudio.SDL_AudioFormat)inputFormat.Format,
            channels = inputFormat.Channels,
            freq = inputFormat.Frequency
        };

        var dstSpec = new NativeAudio.SDL_AudioSpec
        {
            format = (NativeAudio.SDL_AudioFormat)outputFormat.Format,
            channels = outputFormat.Channels,
            freq = outputFormat.Frequency
        };

        NativeAudio.SDL_AudioStream* handle = NativeAudio.SDL_CreateAudioStream(&srcSpec, &dstSpec);
        return handle == null ? throw new SdlException() : new AudioStream(handle, ownsHandle: true);
    }

    /// <summary>
    /// Creates a new audio stream with the same input and output format (for queuing audio).
    /// </summary>
    /// <param name="format">The audio format.</param>
    /// <returns>A new AudioStream instance.</returns>
    public static AudioStream Create(AudioSpec format)
    {
        return Create(format, format);
    }

    /// <summary>
    /// Opens an audio device and creates a bound stream in one call.
    /// </summary>
    /// <param name="deviceId">The device to open, or use DefaultPlayback/DefaultRecording for defaults.</param>
    /// <param name="spec">The audio format, or null to use device defaults.</param>
    /// <returns>A new AudioStream instance bound to the opened device.</returns>
    /// <remarks>
    /// The device starts paused and must be resumed with <see cref="ResumeDevice"/>.
    /// Destroying this stream will also close the associated device.
    /// </remarks>
    public static AudioStream OpenDeviceStream(NativeAudio.SDL_AudioDeviceID deviceId, AudioSpec? spec = null)
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

        NativeAudio.SDL_AudioStream* handle = NativeAudio.SDL_OpenAudioDeviceStream(deviceId, specPtr, null, 0);
        return handle == null ? throw new SdlException() : new AudioStream(handle, ownsHandle: true);
    }

    /// <summary>
    /// Opens the default playback device and creates a bound stream.
    /// </summary>
    /// <param name="spec">The audio format, or null to use device defaults.</param>
    /// <returns>A new AudioStream instance bound to the default playback device.</returns>
    public static AudioStream OpenDefaultPlayback(AudioSpec? spec = null)
    {
        return OpenDeviceStream(NativeAudio.SDL_AUDIO_DEVICE_DEFAULT_PLAYBACK, spec);
    }

    /// <summary>
    /// Opens the default recording device and creates a bound stream.
    /// </summary>
    /// <param name="spec">The audio format, or null to use device defaults.</param>
    /// <returns>A new AudioStream instance bound to the default recording device.</returns>
    public static AudioStream OpenDefaultRecording(AudioSpec? spec = null)
    {
        return OpenDeviceStream(NativeAudio.SDL_AUDIO_DEVICE_DEFAULT_RECORDING, spec);
    }

    /// <summary>
    /// Creates an AudioStream wrapper for an existing SDL_AudioStream pointer.
    /// </summary>
    /// <param name="handle">The SDL_AudioStream pointer.</param>
    /// <param name="ownsHandle">Whether this instance should destroy the stream when disposed.</param>
    internal AudioStream(NativeAudio.SDL_AudioStream* handle, bool ownsHandle)
    {
        Handle = handle;
        _ownsHandle = ownsHandle;
    }

    /// <summary>
    /// Adds audio data to the stream.
    /// </summary>
    /// <param name="data">The audio data to add.</param>
    public void Put(ReadOnlySpan<byte> data)
    {
        ThrowIfDisposed();

        fixed (byte* ptr = data)
        {
            _ = CheckErrorBool(NativeAudio.SDL_PutAudioStreamData(Handle, ptr, data.Length));
        }
    }

    /// <summary>
    /// Gets converted/resampled audio data from the stream.
    /// </summary>
    /// <param name="buffer">The buffer to fill with audio data.</param>
    /// <returns>The number of bytes actually read.</returns>
    public int Get(Span<byte> buffer)
    {
        ThrowIfDisposed();

        fixed (byte* ptr = buffer)
        {
            return CheckErrorNegativeOne(NativeAudio.SDL_GetAudioStreamData(Handle, ptr, buffer.Length));
        }
    }

    /// <summary>
    /// Flushes the stream, making all buffered data available immediately.
    /// </summary>
    public void Flush()
    {
        ThrowIfDisposed();
        _ = CheckErrorBool(NativeAudio.SDL_FlushAudioStream(Handle));
    }

    /// <summary>
    /// Clears all pending data from the stream.
    /// </summary>
    public void Clear()
    {
        ThrowIfDisposed();
        _ = CheckErrorBool(NativeAudio.SDL_ClearAudioStream(Handle));
    }

    /// <summary>
    /// Locks the stream for thread-safe access.
    /// </summary>
    public void Lock()
    {
        ThrowIfDisposed();
        _ = CheckErrorBool(NativeAudio.SDL_LockAudioStream(Handle));
    }

    /// <summary>
    /// Unlocks the stream after a call to <see cref="Lock"/>.
    /// </summary>
    public void Unlock()
    {
        ThrowIfDisposed();
        _ = CheckErrorBool(NativeAudio.SDL_UnlockAudioStream(Handle));
    }

    /// <summary>
    /// Pauses the audio device associated with this stream.
    /// </summary>
    public void PauseDevice()
    {
        ThrowIfDisposed();
        _ = CheckErrorBool(NativeAudio.SDL_PauseAudioStreamDevice(Handle));
    }

    /// <summary>
    /// Resumes the audio device associated with this stream.
    /// </summary>
    public void ResumeDevice()
    {
        ThrowIfDisposed();
        _ = CheckErrorBool(NativeAudio.SDL_ResumeAudioStreamDevice(Handle));
    }

    /// <summary>
    /// Unbinds this stream from its audio device.
    /// </summary>
    public void Unbind()
    {
        ThrowIfDisposed();
        NativeAudio.SDL_UnbindAudioStream(Handle);
    }

    /// <summary>
    /// Gets the input channel map, or null if using default channel layout.
    /// </summary>
    public int[]? GetInputChannelMap()
    {
        ThrowIfDisposed();
        int count;
        var map = NativeAudio.SDL_GetAudioStreamInputChannelMap(Handle, &count);
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

    /// <summary>
    /// Gets the output channel map, or null if using default channel layout.
    /// </summary>
    public int[]? GetOutputChannelMap()
    {
        ThrowIfDisposed();
        int count;
        var map = NativeAudio.SDL_GetAudioStreamOutputChannelMap(Handle, &count);
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

    /// <summary>
    /// Sets the input channel map.
    /// </summary>
    /// <param name="channelMap">The channel map, or null to reset to default.</param>
    public void SetInputChannelMap(int[]? channelMap)
    {
        ThrowIfDisposed();

        if (channelMap == null)
        {
            _ = CheckErrorBool(NativeAudio.SDL_SetAudioStreamInputChannelMap(Handle, null, 0));
        }
        else
        {
            fixed (int* ptr = channelMap)
            {
                _ = CheckErrorBool(NativeAudio.SDL_SetAudioStreamInputChannelMap(Handle, ptr, channelMap.Length));
            }
        }
    }

    /// <summary>
    /// Sets the output channel map.
    /// </summary>
    /// <param name="channelMap">The channel map, or null to reset to default.</param>
    public void SetOutputChannelMap(int[]? channelMap)
    {
        ThrowIfDisposed();

        if (channelMap == null)
        {
            _ = CheckErrorBool(NativeAudio.SDL_SetAudioStreamOutputChannelMap(Handle, null, 0));
        }
        else
        {
            fixed (int* ptr = channelMap)
            {
                _ = CheckErrorBool(NativeAudio.SDL_SetAudioStreamOutputChannelMap(Handle, ptr, channelMap.Length));
            }
        }
    }

    /// <inheritdoc/>
    public void Dispose()
    {
        if (_disposed)
        {
            return;
        }

        if (_ownsHandle && Handle != null)
        {
            NativeAudio.SDL_DestroyAudioStream(Handle);
            Handle = null;
        }

        _disposed = true;
    }

    private void ThrowIfDisposed()
    {
        ObjectDisposedException.ThrowIf(_disposed, this);
    }
}
