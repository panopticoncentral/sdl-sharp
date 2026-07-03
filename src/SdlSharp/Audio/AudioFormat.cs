namespace SdlSharp.Audio;

/// <summary>
/// Audio data format.
/// </summary>
public enum AudioFormat : ushort
{
    /// <summary>Unspecified audio format.</summary>
    Unknown = (ushort)Native.SDL_AudioFormat.SDL_AUDIO_UNKNOWN,
    /// <summary>Unsigned 8-bit samples.</summary>
    U8 = (ushort)Native.SDL_AudioFormat.SDL_AUDIO_U8,
    /// <summary>Signed 8-bit samples.</summary>
    S8 = (ushort)Native.SDL_AudioFormat.SDL_AUDIO_S8,
    /// <summary>Signed 16-bit samples, little-endian.</summary>
    S16LE = (ushort)Native.SDL_AudioFormat.SDL_AUDIO_S16LE,
    /// <summary>Signed 16-bit samples, big-endian.</summary>
    S16BE = (ushort)Native.SDL_AudioFormat.SDL_AUDIO_S16BE,
    /// <summary>Signed 32-bit integer samples, little-endian.</summary>
    S32LE = (ushort)Native.SDL_AudioFormat.SDL_AUDIO_S32LE,
    /// <summary>Signed 32-bit integer samples, big-endian.</summary>
    S32BE = (ushort)Native.SDL_AudioFormat.SDL_AUDIO_S32BE,
    /// <summary>32-bit floating point samples, little-endian.</summary>
    F32LE = (ushort)Native.SDL_AudioFormat.SDL_AUDIO_F32LE,
    /// <summary>32-bit floating point samples, big-endian.</summary>
    F32BE = (ushort)Native.SDL_AudioFormat.SDL_AUDIO_F32BE,
    /// <summary>Signed 16-bit samples in native byte order (little-endian on all supported platforms).</summary>
    S16 = (ushort)Native.SDL_AudioFormat.SDL_AUDIO_S16,
    /// <summary>Signed 32-bit integer samples in native byte order (little-endian on all supported platforms).</summary>
    S32 = (ushort)Native.SDL_AudioFormat.SDL_AUDIO_S32,
    /// <summary>32-bit floating point samples in native byte order (little-endian on all supported platforms).</summary>
    F32 = (ushort)Native.SDL_AudioFormat.SDL_AUDIO_F32,
}
