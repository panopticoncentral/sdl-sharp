using static SdlSharp.Native.Audio;
using static SdlSharp.Native.Common;

namespace SdlSharp.Audio;

/// <summary>
/// Holds audio data loaded from a WAV file.
/// </summary>
public readonly record struct WavData(AudioSpec Spec, byte[] Data)
{
    /// <summary>
    /// Loads audio data from a WAV file.
    /// </summary>
    /// <param name="path">Path to the WAV file.</param>
    /// <returns>The loaded audio spec and data.</returns>
    public static unsafe WavData Load(string path)
    {
        Native.SDL_AudioSpec spec;
        byte* audioBuf;
        uint audioLen;
        Check(SDL_LoadWAV(ToUtf8(path), &spec, &audioBuf, &audioLen));
        try
        {
            var data = new byte[checked((int)audioLen)];
            new Span<byte>(audioBuf, checked((int)audioLen)).CopyTo(data);
            return new WavData(
                new AudioSpec((AudioFormat)spec.format, spec.channels, spec.freq),
                data);
        }
        finally
        {
            SDL_free(audioBuf);
        }
    }
    /// <summary>Loads a WAV from a managed stream, leaving it open.</summary>
    public static unsafe WavData Load(Stream stream)
    {
        using var bridge = SdlStreamBridge.ForReading(stream);
        Native.SDL_AudioSpec spec;
        byte* data = null;
        uint length;
        var success = SDL_LoadWAV_IO(bridge.Handle, false, &spec, &data, &length);
        try
        {
            bridge.ThrowIfError(); Check(success);
            return new(new AudioSpec((AudioFormat)spec.format, spec.channels, spec.freq), new ReadOnlySpan<byte>(data, checked((int)length)).ToArray());
        }
        finally { SDL_free(data); }
    }
}
