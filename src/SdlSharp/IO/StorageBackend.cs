namespace SdlSharp.IO;

/// <summary>A synchronous managed implementation of an SDL storage container.</summary>
/// <remarks>Pass an instance to <see cref="StorageContainer.Open(StorageBackend, bool)"/>. Calls are serialized, may occur on any calling thread, and must complete before returning. Do not retain supplied spans or reenter the owning container. Unsupported operations throw <see cref="NotSupportedException"/>. A backend can belong to only one open container at a time.</remarks>
public abstract class StorageBackend : IDisposable
{
    private readonly object _lifetimeGate = new();
    private bool _disposed;
    private bool _attached;
    internal void Attach()
    {
        lock (_lifetimeGate)
        {
            ObjectDisposedException.ThrowIf(_disposed, this);
            if (_attached) throw new InvalidOperationException("This backend already belongs to an open container.");
            _attached = true;
        }
    }
    internal void Detach() { lock (_lifetimeGate) _attached = false; }
    internal void CloseFromContainer() => DisposeCore(fromContainer: true);
    private void DisposeCore(bool fromContainer)
    {
        lock (_lifetimeGate)
        {
            if (_disposed) return;
            if (_attached && !fromContainer) throw new InvalidOperationException("Close the storage container before disposing its backend.");
            _disposed = true;
        }
        try { Dispose(true); }
        finally { GC.SuppressFinalize(this); }
    }

    /// <summary>Gets whether this backend is ready; poll through the container while processing application events.</summary>
    public virtual bool IsReady => true;
    /// <summary>Gets available bytes. Implementations that cannot determine capacity may return <see cref="ulong.MaxValue"/>.</summary>
    public virtual ulong SpaceRemaining => throw new NotSupportedException();
    /// <summary>Enumerates immediate child names, without path separators. Enumeration must stop when its iterator is disposed.</summary>
    public virtual IEnumerable<string> EnumerateDirectory(string path) => throw new NotSupportedException();
    /// <summary>Returns path metadata or throws when unavailable. Missing paths may return a value whose type is None.</summary>
    public virtual StoragePathInfo GetPathInfo(string path) => throw new NotSupportedException();
    /// <summary>Reads the entire file into the exact-sized destination or throws; the span expires on return.</summary>
    public virtual void ReadFile(string path, Span<byte> destination) => throw new NotSupportedException();
    /// <summary>Writes the complete file or throws; the span expires on return.</summary>
    public virtual void WriteFile(string path, ReadOnlySpan<byte> source) => throw new NotSupportedException();
    /// <summary>Creates a directory.</summary>
    public virtual void CreateDirectory(string path) => throw new NotSupportedException();
    /// <summary>Removes a file or empty directory.</summary>
    public virtual void Remove(string path) => throw new NotSupportedException();
    /// <summary>Renames a path, replacing the destination when supported.</summary>
    public virtual void Rename(string oldPath, string newPath) => throw new NotSupportedException();
    /// <summary>Copies a file, replacing the destination when supported.</summary>
    public virtual void CopyFile(string oldPath, string newPath) => throw new NotSupportedException();
    /// <summary>Flushes and releases owned backend resources. Disposal is idempotent, including when cleanup throws. The container calls this once unless opened with leaveOpen; direct disposal while attached is rejected.</summary>
    public void Dispose() => DisposeCore(fromContainer: false);
    /// <summary>Override to flush and release backend resources.</summary>
    protected virtual void Dispose(bool disposing) { }
}
