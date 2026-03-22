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
            var data = new byte[audioLen];
            new Span<byte>(audioBuf, (int)audioLen).CopyTo(data);
            return new WavData(
                new AudioSpec((AudioFormat)spec.format, spec.channels, spec.freq),
                data);
        }
        finally
        {
            SDL_free(audioBuf);
        }
    }
}
