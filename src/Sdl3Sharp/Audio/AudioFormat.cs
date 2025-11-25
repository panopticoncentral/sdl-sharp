using NativeAudio = Sdl3Sharp.Native.Audio;

namespace Sdl3Sharp.Audio;

/// <summary>
/// Specifies the format of audio data.
/// </summary>
public enum AudioFormat : ushort
{
    /// <summary>Unspecified audio format.</summary>
    Unknown = 0x0000,

    /// <summary>Unsigned 8-bit samples.</summary>
    U8 = 0x0008,

    /// <summary>Signed 8-bit samples.</summary>
    S8 = 0x8008,

    /// <summary>Signed 16-bit samples (little-endian).</summary>
    S16LE = 0x8010,

    /// <summary>Signed 16-bit samples (big-endian).</summary>
    S16BE = 0x9010,

    /// <summary>32-bit integer samples (little-endian).</summary>
    S32LE = 0x8020,

    /// <summary>32-bit integer samples (big-endian).</summary>
    S32BE = 0x9020,

    /// <summary>32-bit floating point samples (little-endian).</summary>
    F32LE = 0x8120,

    /// <summary>32-bit floating point samples (big-endian).</summary>
    F32BE = 0x9120,

    /// <summary>Signed 16-bit samples in native byte order (little-endian on .NET).</summary>
    S16 = S16LE,

    /// <summary>32-bit integer samples in native byte order (little-endian on .NET).</summary>
    S32 = S32LE,

    /// <summary>32-bit floating point samples in native byte order (little-endian on .NET).</summary>
    F32 = F32LE
}

/// <summary>
/// Extension methods for <see cref="AudioFormat"/>.
/// </summary>
public static class AudioFormatExtensions
{
    private const int MaskBitSize = 0xFF;
    private const int MaskFloat = 1 << 8;
    private const int MaskBigEndian = 1 << 12;
    private const int MaskSigned = 1 << 15;

    /// <summary>
    /// Gets the size in bits of audio samples in this format.
    /// </summary>
    /// <param name="format">The audio format.</param>
    /// <returns>The number of bits per sample.</returns>
    public static int BitSize(this AudioFormat format) => (int)format & MaskBitSize;

    /// <summary>
    /// Gets the size in bytes of audio samples in this format.
    /// </summary>
    /// <param name="format">The audio format.</param>
    /// <returns>The number of bytes per sample.</returns>
    public static int ByteSize(this AudioFormat format) => format.BitSize() / 8;

    /// <summary>
    /// Gets whether this format uses floating point samples.
    /// </summary>
    /// <param name="format">The audio format.</param>
    /// <returns>True if the format uses floating point samples.</returns>
    public static bool IsFloat(this AudioFormat format) => ((int)format & MaskFloat) != 0;

    /// <summary>
    /// Gets whether this format uses integer samples.
    /// </summary>
    /// <param name="format">The audio format.</param>
    /// <returns>True if the format uses integer samples.</returns>
    public static bool IsInt(this AudioFormat format) => !format.IsFloat();

    /// <summary>
    /// Gets whether this format uses big-endian byte order.
    /// </summary>
    /// <param name="format">The audio format.</param>
    /// <returns>True if the format uses big-endian byte order.</returns>
    public static bool IsBigEndian(this AudioFormat format) => ((int)format & MaskBigEndian) != 0;

    /// <summary>
    /// Gets whether this format uses little-endian byte order.
    /// </summary>
    /// <param name="format">The audio format.</param>
    /// <returns>True if the format uses little-endian byte order.</returns>
    public static bool IsLittleEndian(this AudioFormat format) => !format.IsBigEndian();

    /// <summary>
    /// Gets whether this format uses signed samples.
    /// </summary>
    /// <param name="format">The audio format.</param>
    /// <returns>True if the format uses signed samples.</returns>
    public static bool IsSigned(this AudioFormat format) => ((int)format & MaskSigned) != 0;

    /// <summary>
    /// Gets whether this format uses unsigned samples.
    /// </summary>
    /// <param name="format">The audio format.</param>
    /// <returns>True if the format uses unsigned samples.</returns>
    public static bool IsUnsigned(this AudioFormat format) => !format.IsSigned();

    /// <summary>
    /// Gets the human-readable name of this audio format.
    /// </summary>
    /// <param name="format">The audio format.</param>
    /// <returns>A string describing the audio format.</returns>
    public static string GetName(this AudioFormat format) =>
        NativeAudio.SDL_GetAudioFormatName((NativeAudio.SDL_AudioFormat)format);

    /// <summary>
    /// Gets the silence value for this audio format.
    /// </summary>
    /// <param name="format">The audio format.</param>
    /// <returns>A byte value that represents silence in this format.</returns>
    public static int GetSilenceValue(this AudioFormat format) =>
        NativeAudio.SDL_GetSilenceValueForFormat((NativeAudio.SDL_AudioFormat)format);
}
