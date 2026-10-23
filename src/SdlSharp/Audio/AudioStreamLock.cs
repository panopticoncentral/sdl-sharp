using static SdlSharp.Native.Audio;

namespace SdlSharp.Audio;

/// <summary>Locks an audio stream until disposed once on the acquiring thread.</summary>
public sealed unsafe class AudioStreamLock : IDisposable
{
    private readonly SdlScope _scope;
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Reliability", "CA2000", Justification = "The lease is transferred to the returned scope or lifetime cleanup, which releases it exactly once.")]
    internal AudioStreamLock(AudioStream stream)
    {
        var handle = stream.Handle;
        var lease = stream.Lifetime.Retain();
        try
        {
            Native.Common.Check(SDL_LockAudioStream(handle));
            _scope = new SdlScope(() => { SDL_UnlockAudioStream(handle); lease.Dispose(); });
        }
        catch { lease.Dispose(); throw; }
    }
    /// <summary>Unlocks the stream once. Repeated disposal is harmless.</summary>
    public void Dispose() => _scope.Dispose();
}
