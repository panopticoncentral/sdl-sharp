using NativeAudio = Sdl3Sharp.Native.Audio;

using static Sdl3Sharp.Native.Common;

namespace Sdl3Sharp.Audio;

/// <summary>
/// Provides audio subsystem functionality for SDL.
/// </summary>
public static class Audio
{
    /// <summary>
    /// Gets a read-only collection of audio drivers compiled into SDL.
    /// </summary>
    public static AudioDriverCollection Drivers => AudioDriverCollection.Instance;

    /// <summary>
    /// Gets the name of the currently initialized audio driver.
    /// </summary>
    public static string? CurrentDriver => NativeAudio.SDL_GetCurrentAudioDriver();

    /// <summary>
    /// Mixes audio data from a source buffer into a destination buffer.
    /// </summary>
    /// <param name="destination">The destination buffer to mix into.</param>
    /// <param name="source">The source buffer to mix from.</param>
    /// <param name="format">The audio format of both buffers.</param>
    /// <param name="volume">The volume multiplier (0.0 to 1.0).</param>
    public static unsafe void Mix(Span<byte> destination, ReadOnlySpan<byte> source, AudioFormat format, float volume = 1.0f)
    {
        if (source.Length > destination.Length)
        {
            throw new ArgumentException("Source buffer is larger than destination buffer.", nameof(source));
        }

        fixed (byte* dst = destination)
        fixed (byte* src = source)
        {
            _ = CheckErrorBool(NativeAudio.SDL_MixAudio(dst, src, (NativeAudio.SDL_AudioFormat)format, (uint)source.Length, volume));
        }
    }

    /// <summary>
    /// Converts audio data from one format to another.
    /// </summary>
    /// <param name="sourceSpec">The format of the source audio data.</param>
    /// <param name="sourceData">The source audio data.</param>
    /// <param name="destinationSpec">The desired output format.</param>
    /// <returns>The converted audio data.</returns>
    public static unsafe byte[] Convert(AudioSpec sourceSpec, ReadOnlySpan<byte> sourceData, AudioSpec destinationSpec)
    {
        var srcSpec = new NativeAudio.SDL_AudioSpec
        {
            format = (NativeAudio.SDL_AudioFormat)sourceSpec.Format,
            channels = sourceSpec.Channels,
            freq = sourceSpec.Frequency
        };

        var dstSpec = new NativeAudio.SDL_AudioSpec
        {
            format = (NativeAudio.SDL_AudioFormat)destinationSpec.Format,
            channels = destinationSpec.Channels,
            freq = destinationSpec.Frequency
        };

        byte* dstData;
        int dstLen;

        fixed (byte* srcData = sourceData)
        {
            _ = CheckErrorBool(NativeAudio.SDL_ConvertAudioSamples(&srcSpec, srcData, sourceData.Length, &dstSpec, &dstData, &dstLen));
        }

        try
        {
            var result = new byte[dstLen];
            new Span<byte>(dstData, dstLen).CopyTo(result);
            return result;
        }
        finally
        {
            Native.StdInc.SDL_free(dstData);
        }
    }
}
