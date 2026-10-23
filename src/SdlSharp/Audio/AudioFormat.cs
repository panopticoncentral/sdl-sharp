namespace SdlSharp.Audio;

/// <summary>
/// Audio data format.
/// </summary>
public enum AudioFormat : uint
{
    /// <summary>Unspecified audio format.</summary>
    Unknown = (uint)Native.SDL_AudioFormat.SDL_AUDIO_UNKNOWN,
    /// <summary>Unsigned 8-bit samples.</summary>
    U8 = (uint)Native.SDL_AudioFormat.SDL_AUDIO_U8,
    /// <summary>Signed 8-bit samples.</summary>
    S8 = (uint)Native.SDL_AudioFormat.SDL_AUDIO_S8,
    /// <summary>Signed 16-bit samples, little-endian.</summary>
    S16LE = (uint)Native.SDL_AudioFormat.SDL_AUDIO_S16LE,
    /// <summary>Signed 16-bit samples, big-endian.</summary>
    S16BE = (uint)Native.SDL_AudioFormat.SDL_AUDIO_S16BE,
    /// <summary>Signed 32-bit integer samples, little-endian.</summary>
    S32LE = (uint)Native.SDL_AudioFormat.SDL_AUDIO_S32LE,
    /// <summary>Signed 32-bit integer samples, big-endian.</summary>
    S32BE = (uint)Native.SDL_AudioFormat.SDL_AUDIO_S32BE,
    /// <summary>32-bit floating point samples, little-endian.</summary>
    F32LE = (uint)Native.SDL_AudioFormat.SDL_AUDIO_F32LE,
    /// <summary>32-bit floating point samples, big-endian.</summary>
    F32BE = (uint)Native.SDL_AudioFormat.SDL_AUDIO_F32BE,
    /// <summary>Signed 16-bit samples in native byte order (little-endian on all supported platforms).</summary>
    S16 = (uint)Native.SDL_AudioFormat.SDL_AUDIO_S16,
    /// <summary>Signed 32-bit integer samples in native byte order (little-endian on all supported platforms).</summary>
    S32 = (uint)Native.SDL_AudioFormat.SDL_AUDIO_S32,
    /// <summary>32-bit floating point samples in native byte order (little-endian on all supported platforms).</summary>
    F32 = (uint)Native.SDL_AudioFormat.SDL_AUDIO_F32,
}
