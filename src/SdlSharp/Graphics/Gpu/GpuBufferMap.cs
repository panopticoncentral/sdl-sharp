namespace SdlSharp.Graphics.Gpu;

/// <summary>A CPU mapping of a transfer buffer. Obtained spans must not outlive this lease.</summary>
public sealed unsafe class GpuBufferMap : IDisposable
{
    private nint _pointer;
    private readonly int _size;
    private readonly int _thread = Environment.CurrentManagedThreadId;
    private readonly SdlScope _release;
    internal GpuBufferMap(nint pointer, int size, Action release)
    {
        _pointer = pointer; _size = size;
        _release = new SdlScope(() => { release(); _pointer = 0; });
    }
    /// <summary>The mapped bytes. Access on the acquiring thread before disposal.</summary>
    public Span<byte> Span
    {
        get
        {
            ObjectDisposedException.ThrowIf(_pointer == 0, this);
            if (_thread != Environment.CurrentManagedThreadId) throw new InvalidOperationException("Use the mapping on its acquiring thread.");
            return new Span<byte>((void*)_pointer, _size);
        }
    }
    /// <summary>Unmaps the buffer exactly once.</summary>
    public void Dispose() => _release.Dispose();
}
