using System.Runtime.InteropServices;

using static SdlSharp.Native.Audio;

namespace SdlSharp.Audio;

/// <summary>
/// Extension methods that inspect <see cref="AudioFormat"/> values.
/// </summary>
public static unsafe class AudioFormatInfo
{
    /// <summary>
    /// Gets the human-readable name of an audio format (for example "SDL_AUDIO_S16LE").
    /// </summary>
    /// <param name="format">The audio format.</param>
    /// <returns>The format name, or null if unavailable.</returns>
    public static string? GetName(this AudioFormat format) =>
        Marshal.PtrToStringUTF8((nint)SDL_GetAudioFormatName((Native.SDL_AudioFormat)format));

    /// <summary>Gets the size of a single sample in bits.</summary>
    /// <param name="format">The audio format.</param>
    public static int GetBitSize(this AudioFormat format) => (ushort)format & 0xFF;

    /// <summary>Gets the size of a single sample in bytes.</summary>
    /// <param name="format">The audio format.</param>
    public static int GetByteSize(this AudioFormat format) => format.GetBitSize() / 8;

    /// <summary>Gets whether the format holds floating-point samples.</summary>
    /// <param name="format">The audio format.</param>
    public static bool IsFloat(this AudioFormat format) => ((ushort)format & 0x0100) != 0;

    /// <summary>Gets whether the format holds integer samples.</summary>
    /// <param name="format">The audio format.</param>
    public static bool IsInt(this AudioFormat format) => !format.IsFloat();

    /// <summary>Gets whether the format stores samples big-endian.</summary>
    /// <param name="format">The audio format.</param>
    public static bool IsBigEndian(this AudioFormat format) => ((ushort)format & 0x1000) != 0;

    /// <summary>Gets whether the format stores samples little-endian.</summary>
    /// <param name="format">The audio format.</param>
    public static bool IsLittleEndian(this AudioFormat format) => !format.IsBigEndian();

    /// <summary>Gets whether the format holds signed samples.</summary>
    /// <param name="format">The audio format.</param>
    public static bool IsSigned(this AudioFormat format) => ((ushort)format & 0x8000) != 0;

    /// <summary>Gets whether the format holds unsigned samples.</summary>
    /// <param name="format">The audio format.</param>
    public static bool IsUnsigned(this AudioFormat format) => !format.IsSigned();

    /// <summary>
    /// Gets the byte value that represents silence for this format.
    /// </summary>
    /// <param name="format">The audio format.</param>
    public static int GetSilenceValue(this AudioFormat format) =>
        SDL_GetSilenceValueForFormat((Native.SDL_AudioFormat)format);
}
