namespace SdlSharp.Graphics;

/// <summary>A camera-owned frame. Its surface is valid only until this lease is disposed.</summary>
public sealed unsafe class CameraFrame : IDisposable
{
    private readonly SdlScope _release;
    /// <summary>The frame's SDL timestamp in nanoseconds.</summary>
    public ulong TimestampNanoseconds { get; }
    /// <summary>The borrowed image, invalidated when this lease is released.</summary>
    public Surface Surface { get; }
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Reliability", "CA2000", Justification = "The lease is transferred to the returned scope or lifetime cleanup, which releases it exactly once.")]
    internal CameraFrame(NativeLifetime camera, Native.SDL_Surface* surface, ulong timestamp, Action releaseFrame)
    {
        var lease = camera.Retain();
        Surface = new Surface(surface, ownsHandle: false, parent: camera);
        TimestampNanoseconds = timestamp;
        _release = new SdlScope(() =>
        {
            Surface.Lifetime.EnsureCanDestroy();
            releaseFrame();
            Surface.Lifetime.Invalidate();
            lease.Dispose();
        });
    }
    /// <summary>Returns this frame to the camera exactly once.</summary>
    public void Dispose() => _release.Dispose();
}
