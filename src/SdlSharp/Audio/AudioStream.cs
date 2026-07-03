using static SdlSharp.Native.Audio;
using static SdlSharp.Native.Common;

namespace SdlSharp.Audio;

/// <summary>
/// A managed wrapper around an SDL audio stream (SDL_AudioStream).
/// Audio streams handle format conversion, resampling, and buffering.
/// </summary>
public sealed unsafe class AudioStream : IDisposable
{
    private readonly bool _ownsHandle;

    internal Native.SDL_AudioStream* Handle
    {
        get
        {
            ObjectDisposedException.ThrowIf(_handle == null, this);
            return _handle;
        }
    }

    private Native.SDL_AudioStream* _handle;

    internal AudioStream(Native.SDL_AudioStream* handle, bool ownsHandle = true)
    {
        _handle = handle;
        _ownsHandle = ownsHandle;
    }

    /// <summary>
    /// Creates a new audio stream that converts between two formats.
    /// </summary>
    /// <param name="srcSpec">The input audio format.</param>
    /// <param name="dstSpec">The output audio format.</param>
    /// <returns>A new audio stream.</returns>
    public static AudioStream Create(AudioSpec srcSpec, AudioSpec dstSpec)
    {
        var src = ToNativeSpec(srcSpec);
        var dst = ToNativeSpec(dstSpec);
        return new AudioStream(Check(SDL_CreateAudioStream(&src, &dst)));
    }

    /// <summary>
    /// Convenience: opens a device, creates a stream, and binds them together.
    /// The device starts paused — call <see cref="ResumeDevice"/> to begin playback.
    /// Destroying this stream also closes the associated device.
    /// </summary>
    /// <param name="playback">True for playback, false for recording.</param>
    /// <param name="spec">Desired audio format, or null for system default.</param>
    /// <returns>A new audio stream bound to a new device.</returns>
    public static AudioStream OpenDevice(bool playback = true, AudioSpec? spec = null)
    {
        var devid = playback ? SDL_AUDIO_DEVICE_DEFAULT_PLAYBACK : SDL_AUDIO_DEVICE_DEFAULT_RECORDING;

        Native.SDL_AudioSpec nativeSpec;
        Native.SDL_AudioSpec* specPtr = null;
        if (spec is { } s)
        {
            nativeSpec = ToNativeSpec(s);
            specPtr = &nativeSpec;
        }

        return new AudioStream(Check(SDL_OpenAudioDeviceStream(devid, specPtr, null, null)));
    }

    /// <summary>
    /// Gets the properties associated with this stream.
    /// </summary>
    public PropertyGroup Properties =>
        new(CheckId(SDL_GetAudioStreamProperties(Handle)), ownsHandle: false);

    /// <summary>
    /// Gets the current input and output formats of this stream.
    /// </summary>
    /// <param name="srcSpec">Receives the input format.</param>
    /// <param name="dstSpec">Receives the output format.</param>
    public void GetFormat(out AudioSpec srcSpec, out AudioSpec dstSpec)
    {
        Native.SDL_AudioSpec src, dst;
        Check(SDL_GetAudioStreamFormat(Handle, &src, &dst));
        srcSpec = FromNativeSpec(src);
        dstSpec = FromNativeSpec(dst);
    }

    /// <summary>
    /// Changes the input and/or output format of this stream.
    /// </summary>
    /// <param name="srcSpec">New input format, or null to leave unchanged.</param>
    /// <param name="dstSpec">New output format, or null to leave unchanged.</param>
    public void SetFormat(AudioSpec? srcSpec = null, AudioSpec? dstSpec = null)
    {
        Native.SDL_AudioSpec src, dst;
        Native.SDL_AudioSpec* srcPtr = null, dstPtr = null;

        if (srcSpec is { } s)
        {
            src = ToNativeSpec(s);
            srcPtr = &src;
        }
        if (dstSpec is { } d)
        {
            dst = ToNativeSpec(d);
            dstPtr = &dst;
        }

        Check(SDL_SetAudioStreamFormat(Handle, srcPtr, dstPtr));
    }

    /// <summary>
    /// Gets or sets the frequency ratio of this stream.
    /// Values &gt; 1.0 play faster/higher pitch, &lt; 1.0 play slower/lower pitch.
    /// Must be between 0.01 and 100.
    /// </summary>
    public float FrequencyRatio
    {
        get => SDL_GetAudioStreamFrequencyRatio(Handle);
        set => Check(SDL_SetAudioStreamFrequencyRatio(Handle, value));
    }

    /// <summary>
    /// Gets or sets the gain (volume) of this stream.
    /// 1.0 is no change, 0.0 is silence.
    /// </summary>
    public float Gain
    {
        get => SDL_GetAudioStreamGain(Handle);
        set => Check(SDL_SetAudioStreamGain(Handle, value));
    }

    /// <summary>
    /// Adds audio data to the stream.
    /// </summary>
    /// <param name="data">The audio data to add.</param>
    public void PutData(ReadOnlySpan<byte> data)
    {
        fixed (byte* ptr = data)
        {
            Check(SDL_PutAudioStreamData(Handle, ptr, data.Length));
        }
    }

    /// <summary>
    /// Gets converted/resampled data from the stream.
    /// </summary>
    /// <param name="buffer">The buffer to fill with audio data.</param>
    /// <returns>The number of bytes actually read.</returns>
    public int GetData(Span<byte> buffer)
    {
        fixed (byte* ptr = buffer)
        {
            var result = SDL_GetAudioStreamData(Handle, ptr, buffer.Length);
            if (result < 0) throw new SdlException();
            return result;
        }
    }

    /// <summary>
    /// Gets the number of converted/resampled bytes available for reading.
    /// </summary>
    public int Available
    {
        get
        {
            var result = SDL_GetAudioStreamAvailable(Handle);
            if (result < 0) throw new SdlException();
            return result;
        }
    }

    /// <summary>
    /// Gets the number of bytes currently queued as input.
    /// </summary>
    public int Queued
    {
        get
        {
            var result = SDL_GetAudioStreamQueued(Handle);
            if (result < 0) throw new SdlException();
            return result;
        }
    }

    /// <summary>
    /// Flushes the stream — converts/resamples all buffered data immediately.
    /// </summary>
    public void Flush() => Check(SDL_FlushAudioStream(Handle));

    /// <summary>
    /// Clears any pending data in the stream.
    /// </summary>
    public void Clear() => Check(SDL_ClearAudioStream(Handle));

    /// <summary>
    /// Unbinds this stream from its audio device.
    /// </summary>
    public void Unbind() => SDL_UnbindAudioStream(Handle);

    /// <summary>
    /// Pauses the audio device associated with this stream.
    /// </summary>
    public void PauseDevice() => Check(SDL_PauseAudioStreamDevice(Handle));

    /// <summary>
    /// Resumes the audio device associated with this stream.
    /// </summary>
    public void ResumeDevice() => Check(SDL_ResumeAudioStreamDevice(Handle));

    /// <summary>
    /// Gets whether the audio device associated with this stream is paused.
    /// </summary>
    public bool IsDevicePaused => SDL_AudioStreamDevicePaused(Handle);

    private static Native.SDL_AudioSpec ToNativeSpec(AudioSpec spec) =>
        new()
        {
            format = (Native.SDL_AudioFormat)spec.Format,
            channels = spec.Channels,
            freq = spec.Frequency
        };

    private static AudioSpec FromNativeSpec(Native.SDL_AudioSpec spec) =>
        new((AudioFormat)spec.format, spec.channels, spec.freq);

    /// <inheritdoc/>
    public void Dispose()
    {
        if (_ownsHandle && _handle != null)
        {
            SDL_DestroyAudioStream(_handle);
        }
        _handle = null;
    }
}
