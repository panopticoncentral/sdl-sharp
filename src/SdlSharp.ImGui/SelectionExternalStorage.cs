using System.Collections.Concurrent;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using static SdlSharp.ImGui.Native;

namespace SdlSharp.ImGui;

/// <summary>
/// Delegate invoked by <see cref="SelectionExternalStorage.ApplyRequests"/> to write
/// selection state into your own data structure: set the item at <paramref name="index"/>
/// to <paramref name="selected"/>.
/// </summary>
public delegate void SelectionSetItemSelected(int index, bool selected);

/// <summary>
/// Optional helper to apply multi-selection requests to your own external selection
/// storage (e.g. a bool per item). Set an adapter with <see cref="SetItemSelectedAdapter"/>
/// before calling <see cref="ApplyRequests"/>.
/// </summary>
public sealed unsafe class SelectionExternalStorage : IDisposable
{
    private static readonly ConcurrentDictionary<nint, AdapterState> _adapters = new();

    private IGSharp_SelectionExternalStorage* _handle;

    /// <summary>Creates a new external selection storage helper.</summary>
    public SelectionExternalStorage()
    {
        _handle = IGSharp_SelectionExternalStorage_Create();
    }

    /// <summary>
    /// Applies the selection requests coming from BeginMultiSelect/EndMultiSelect by
    /// invoking the adapter set via <see cref="SetItemSelectedAdapter"/> for each affected item.
    /// An adapter must be set before calling this.
    /// </summary>
    public void ApplyRequests(MultiSelectIO io)
    {
        ThrowIfDisposed();
        IGSharp_SelectionExternalStorage_ApplyRequests(_handle, io.Handle);
        if (_adapters.TryGetValue((nint)_handle, out var state))
            state.ThrowIfFailed();
    }

    /// <summary>Opaque user data pointer, available for your own use.</summary>
    public nint UserData
    {
        get
        {
            ThrowIfDisposed();
            return (nint)IGSharp_SelectionExternalStorage_GetUserData(_handle);
        }
        set
        {
            ThrowIfDisposed();
            IGSharp_SelectionExternalStorage_SetUserData(_handle, (void*)value);
        }
    }

    /// <summary>
    /// Sets the adapter that writes selection state to your own storage.
    /// Pass null to remove the adapter (calling <see cref="ApplyRequests"/> without
    /// an adapter is invalid).
    /// </summary>
    public void SetItemSelectedAdapter(SelectionSetItemSelected? adapter)
    {
        ThrowIfDisposed();
        if (adapter == null)
        {
            _adapters.TryRemove((nint)_handle, out _);
            IGSharp_SelectionExternalStorage_SetAdapterSetItemSelected(_handle, null);
        }
        else
        {
            _adapters[(nint)_handle] = new AdapterState(adapter);
            IGSharp_SelectionExternalStorage_SetAdapterSetItemSelected(_handle, &SetItemSelectedThunk);
        }
    }

    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
    private static void SetItemSelectedThunk(IGSharp_SelectionExternalStorage* self, int index, byte selected)
    {
        if (!_adapters.TryGetValue((nint)self, out var state)) return;
        try { state.Adapter(index, selected != 0); }
        catch (Exception ex) { state.Capture(ex); }
    }

    /// <summary>Releases the unmanaged selection storage.</summary>
    public void Dispose()
    {
        if (_handle != null)
        {
            _adapters.TryRemove((nint)_handle, out _);
            IGSharp_SelectionExternalStorage_Destroy(_handle);
            _handle = null;
        }
    }

    private void ThrowIfDisposed()
    {
        if (_handle == null) throw new ObjectDisposedException(nameof(SelectionExternalStorage));
    }

    private sealed class AdapterState(SelectionSetItemSelected adapter)
    {
        private ExceptionDispatchInfo? _exception;
        public SelectionSetItemSelected Adapter { get; } = adapter;
        public void Capture(Exception exception) => _exception ??= ExceptionDispatchInfo.Capture(exception);
        public void ThrowIfFailed()
        {
            var exception = Interlocked.Exchange(ref _exception, null);
            exception?.Throw();
        }
    }
}
