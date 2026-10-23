using static SdlSharp.ImGui.Native;

namespace SdlSharp.ImGui;

/// <summary>Temporarily replaces a window's state storage. Dispose in nesting order, before ending the window or frame.</summary>
public sealed unsafe class StateStorageScope : IDisposable
{
    private readonly Context _context;
    private readonly nint _window;
    private readonly IGSharp_Storage* _previous;
    private readonly IDisposable _contextLease;
    private readonly IDisposable _previousLease;
    private readonly IDisposable _storageLease;
    private bool _disposed;

    internal StateStorageScope(Storage storage)
    {
        ArgumentNullException.ThrowIfNull(storage);
        _context = Context.Current;
        _context.CheckCurrent();
        var handle = storage.Handle;
        _window = (nint)IGSharp_GetWindowDrawList();
        _previous = IGSharp_GetStateStorage();
        _contextLease = _context.Lifetime.Retain();
        try
        {
            _previousLease = Lifetime.Capture((nint)_previous).Retain();
            try { _storageLease = storage.Lifetime.Retain(); }
            catch { _previousLease.Dispose(); throw; }
        }
        catch { _contextLease.Dispose(); throw; }
        if (!_context.StorageScopes.TryGetValue(_window, out var stack)) _context.StorageScopes[_window] = stack = [];
        stack.Push(this);
        IGSharp_SetStateStorage(handle);
    }

    /// <summary>Restores the previous storage and releases both storage leases exactly once.</summary>
    public void Dispose()
    {
        if (_disposed) return;
        _context.CheckCurrent();
        if ((nint)IGSharp_GetWindowDrawList() != _window || !ReferenceEquals(_context.StorageScopes[_window].Peek(), this))
            throw new InvalidOperationException("Restore state storage in nesting order within the window that acquired it.");
        IGSharp_SetStateStorage(_previous);
        var stack = _context.StorageScopes[_window];
        stack.Pop();
        if (stack.Count == 0) _context.StorageScopes.Remove(_window);
        _storageLease.Dispose(); _previousLease.Dispose(); _contextLease.Dispose();
        _disposed = true;
    }
}
