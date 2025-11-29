using NativeAudio = Sdl3Sharp.Native.Audio;

using static Sdl3Sharp.Native.Common;
using static Sdl3Sharp.Native.StdInc;

namespace Sdl3Sharp.Audio;

/// <summary>
/// Provides functionality for loading WAV audio files.
/// </summary>
public static class WavFile
{
    /// <summary>
    /// Loads a WAV file from disk.
    /// </summary>
    /// <param name="path">The path to the WAV file.</param>
    /// <returns>A tuple containing the audio specification and the audio data.</returns>
    public static unsafe (AudioSpec Spec, byte[] Data) Load(string path)
    {
        NativeAudio.SDL_AudioSpec spec;
        byte* audioData;
        uint audioLen;

        _ = CheckErrorBool(NativeAudio.SDL_LoadWAV(path, &spec, &audioData, &audioLen));

        try
        {
            var data = new byte[audioLen];
            new Span<byte>(audioData, (int)audioLen).CopyTo(data);
            var audioSpec = new AudioSpec((AudioFormat)spec.format, spec.channels, spec.freq);
            return (audioSpec, data);
        }
        finally
        {
            SDL_free(audioData);
        }
    }

    /// <summary>
    /// Loads a WAV file from an IOStream.
    /// </summary>
    /// <param name="stream">The IOStream containing the WAV data.</param>
    /// <param name="closeStream">Whether to close the stream after loading.</param>
    /// <returns>A tuple containing the audio specification and the audio data.</returns>
    public static unsafe (AudioSpec Spec, byte[] Data) Load(IOStream stream, bool closeStream = false)
    {
        NativeAudio.SDL_AudioSpec spec;
        byte* audioData;
        uint audioLen;

        _ = CheckErrorBool(NativeAudio.SDL_LoadWAV_IO(stream.Handle, closeStream, &spec, &audioData, &audioLen));

        try
        {
            var data = new byte[audioLen];
            new Span<byte>(audioData, (int)audioLen).CopyTo(data);
            var audioSpec = new AudioSpec((AudioFormat)spec.format, spec.channels, spec.freq);
            return (audioSpec, data);
        }
        finally
        {
            SDL_free(audioData);
        }
    }

    /// <summary>
    /// Loads a WAV file from a byte array.
    /// </summary>
    /// <param name="data">The byte array containing the WAV data.</param>
    /// <returns>A tuple containing the audio specification and the audio data.</returns>
    public static (AudioSpec Spec, byte[] Data) Load(ReadOnlySpan<byte> data)
    {
        using var stream = IOStream.FromReadOnlyMemory(data);
        return Load(stream, closeStream: false);
    }
}
