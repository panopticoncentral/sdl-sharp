using static SdlSharp.Native.Audio;

namespace SdlSharp.Audio;

/// <summary>
/// A scope that keeps an <see cref="AudioStream"/> locked against its audio-thread
/// callbacks. Obtain one with <see cref="AudioStream.Lock"/> and dispose it (for
/// example with <c>using</c>) to unlock. While held, the stream's get/put callbacks
/// will not run, so shared state can be updated safely.
/// </summary>
public readonly ref struct AudioStreamLock
{
    private readonly unsafe Native.SDL_AudioStream* _stream;

    internal unsafe AudioStreamLock(Native.SDL_AudioStream* stream) => _stream = stream;

    /// <summary>Unlocks the stream.</summary>
    public unsafe void Dispose() => SDL_UnlockAudioStream(_stream);
}
