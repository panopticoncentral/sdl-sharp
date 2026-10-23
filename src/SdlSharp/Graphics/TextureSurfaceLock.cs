namespace SdlSharp.Graphics;
/// <summary>A texture mapped as a surface. The surface is invalid after this lease is disposed.</summary>
public sealed class TextureSurfaceLock : IDisposable
{
    private readonly SdlScope _release;
    /// <summary>The borrowed surface.</summary>
    public Surface Surface { get; }
    internal TextureSurfaceLock(Surface surface, Action release)
    {
        Surface = surface;
        _release = new SdlScope(() => { surface.Lifetime.EnsureCanDestroy(); release(); surface.Lifetime.Invalidate(); });
    }
    /// <summary>Unlocks the texture exactly once.</summary>
    public void Dispose() => _release.Dispose();
}
