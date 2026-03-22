using System.Runtime.InteropServices;

using static SdlSharp.Native.Audio;

namespace SdlSharp.Audio;

/// <summary>
/// Provides information about audio formats.
/// </summary>
public static unsafe class AudioFormatInfo
{
    /// <summary>
    /// Gets the human-readable name of an audio format.
    /// </summary>
    public static string? GetName(AudioFormat format) =>
        Marshal.PtrToStringUTF8((nint)SDL_GetAudioFormatName((Native.SDL_AudioFormat)format));
}
