using static SdlSharp.Native.Audio;
using static SdlSharp.Native.Common;

namespace SdlSharp.Audio;

/// <summary>
/// Stateless helpers for mixing and converting raw audio sample buffers.
/// </summary>
public static unsafe class AudioSamples
{
    /// <summary>
    /// Mixes <paramref name="source"/> into <paramref name="destination"/> at the given volume,
    /// interpreting both buffers as samples of <paramref name="format"/>. The two spans must be
    /// the same length. Mixing more than two streams into the same destination may clip;
    /// use an <see cref="AudioStream"/> for general-purpose mixing.
    /// </summary>
    /// <param name="destination">The buffer mixed into, in place.</param>
    /// <param name="source">The buffer to add.</param>
    /// <param name="format">The sample format of both buffers.</param>
    /// <param name="volume">The mix volume, 0.0 (silence) to 1.0 (full); may exceed 1.0 to amplify.</param>
    /// <exception cref="ArgumentException">The spans differ in length.</exception>
    public static void Mix(Span<byte> destination, ReadOnlySpan<byte> source, AudioFormat format, float volume)
    {
        if (destination.Length != source.Length)
        {
            throw new ArgumentException("Destination and source must be the same length.", nameof(source));
        }

        fixed (byte* dst = destination)
        fixed (byte* src = source)
        {
            Check(SDL_MixAudio(dst, src, (Native.SDL_AudioFormat)format, (uint)source.Length, volume));
        }
    }

    /// <summary>
    /// Converts a complete audio buffer from one format to another. Suitable for whole,
    /// in-memory clips; for streaming conversion use <see cref="AudioStream"/>.
    /// </summary>
    /// <param name="sourceSpec">The format of <paramref name="sourceData"/>.</param>
    /// <param name="sourceData">The input audio bytes.</param>
    /// <param name="destinationSpec">The desired output format.</param>
    /// <returns>A newly allocated buffer holding the converted audio.</returns>
    public static byte[] Convert(AudioSpec sourceSpec, ReadOnlySpan<byte> sourceData, AudioSpec destinationSpec)
    {
        var srcSpec = ToNativeSpec(sourceSpec);
        var dstSpec = ToNativeSpec(destinationSpec);
        byte* dstData;
        int dstLen;

        fixed (byte* src = sourceData)
        {
            Check(SDL_ConvertAudioSamples(&srcSpec, src, sourceData.Length, &dstSpec, &dstData, &dstLen));
        }

        try
        {
            var result = new byte[dstLen];
            new ReadOnlySpan<byte>(dstData, dstLen).CopyTo(result);
            return result;
        }
        finally
        {
            SDL_free(dstData);
        }
    }

    private static Native.SDL_AudioSpec ToNativeSpec(AudioSpec spec) =>
        new()
        {
            format = (Native.SDL_AudioFormat)spec.Format,
            channels = spec.Channels,
            freq = spec.Frequency
        };
}
