namespace SdlSharp.Graphics;

/// <summary>A pixel mapping valid until disposed. Spans obtained from it must not outlive the mapping.</summary>
public sealed unsafe class PixelLock : IDisposable
{
    private nint _pixels;
    private readonly SdlScope _release;
    private readonly int _thread = Environment.CurrentManagedThreadId;
    /// <summary>Bytes between row starts.</summary>
    public int Pitch { get; }
    /// <summary>Accessible bytes in each row, excluding padding.</summary>
    public int RowBytes { get; }
    /// <summary>The number of rows.</summary>
    public int Height { get; }
    internal PixelLock(nint pixels, int pitch, int rowBytes, int height, Action release)
    {
        _pixels = pixels; Pitch = pitch; RowBytes = rowBytes; Height = height;
        _release = new SdlScope(() => { release(); _pixels = 0; });
    }
    /// <summary>Returns one row without exposing padding beyond the locked rectangle.</summary>
    public Span<byte> GetRowSpan(int row)
    {
        ObjectDisposedException.ThrowIf(_pixels == 0, this);
        if (_thread != Environment.CurrentManagedThreadId) throw new InvalidOperationException("Use the mapping on its acquiring thread.");
        if ((uint)row >= (uint)Height) throw new ArgumentOutOfRangeException(nameof(row));
        return new Span<byte>((byte*)_pixels + checked(row * Pitch), RowBytes);
    }
    /// <summary>Releases this mapping exactly once.</summary>
    public void Dispose() => _release.Dispose();
}
