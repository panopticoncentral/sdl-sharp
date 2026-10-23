using System.Collections.Concurrent;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using static SdlSharp.ImGui.Native;

namespace SdlSharp.ImGui;

/// <summary>
/// Delegate mapping an item index to its stable storage ID for
/// <see cref="SelectionBasicStorage"/> (used when items can be reordered).
/// </summary>
public delegate uint SelectionIndexToStorageId(int index);

/// <summary>
/// Optional helper to store multi-selection state as a set of item IDs.
/// Feed it the <see cref="MultiSelectIO"/> from <see cref="ImGui.BeginMultiSelect"/> /
/// <see cref="ImGui.EndMultiSelect"/> via <see cref="ApplyRequests"/>.
/// </summary>
public sealed unsafe class SelectionBasicStorage : IDisposable
{
    private static readonly ConcurrentDictionary<nint, AdapterState> _adapters = new();

    private IGSharp_SelectionBasicStorage* _handle;
    private readonly Lifetime _lifetime;

    /// <summary>Creates a new, empty selection storage.</summary>
    public SelectionBasicStorage()
    {
        _handle = IGSharp_SelectionBasicStorage_Create(); var pointer = (nint)_handle;
        _lifetime = Lifetime.Own(pointer, () => IGSharp_SelectionBasicStorage_Destroy((IGSharp_SelectionBasicStorage*)pointer));
        _lifetime.AddCleanup(() => _adapters.TryRemove(pointer, out _));
    }

    /// <summary>Applies the selection requests coming from BeginMultiSelect/EndMultiSelect.</summary>
    public void ApplyRequests(MultiSelectIO io)
    {
        ThrowIfDisposed();
        using var lease = _lifetime.Retain();
        IGSharp_SelectionBasicStorage_ApplyRequests(_handle, io.Handle);
        ThrowAdapterException();
    }

    /// <summary>Returns true if the item with the given storage ID is selected.</summary>
    public bool Contains(uint id)
    {
        ThrowIfDisposed();
        return IGSharp_SelectionBasicStorage_Contains(_handle, id);
    }

    /// <summary>Clears the selection.</summary>
    public void Clear()
    {
        ThrowIfDisposed();
        IGSharp_SelectionBasicStorage_Clear(_handle);
    }

    /// <summary>Adds or removes a single item from the selection.</summary>
    public void SetItemSelected(uint id, bool selected)
    {
        ThrowIfDisposed();
        IGSharp_SelectionBasicStorage_SetItemSelected(_handle, id, selected);
    }

    /// <summary>
    /// Copies the selected IDs in selection-storage order. The returned snapshot is independent
    /// of subsequent mutations or disposal; see <see cref="PreserveOrder"/>.
    /// </summary>
    public IReadOnlyList<uint> SelectedItems
    {
        get
        {
            ThrowIfDisposed();
            return EnumerateSelectedItems();
        }
    }

    private uint[] EnumerateSelectedItems()
    {
        nint iterator = 0; var selected = new List<uint>(Size);
        while (TryGetNextSelectedItem(ref iterator, out var id)) selected.Add(id);
        return selected.ToArray();
    }

    private bool TryGetNextSelectedItem(ref nint iterator, out uint id)
    {
        ThrowIfDisposed();
        void* opaque = (void*)iterator;
        uint value;
        var result = IGSharp_SelectionBasicStorage_GetNextSelectedItem(_handle, &opaque, &value);
        iterator = (nint)opaque;
        id = value;
        return result;
    }

    /// <summary>Converts an item index to its storage ID using the current adapter (default: id == index).</summary>
    public uint GetStorageIdFromIndex(int index)
    {
        ThrowIfDisposed();
        using var lease = _lifetime.Retain();
        var result = IGSharp_SelectionBasicStorage_GetStorageIdFromIndex(_handle, index);
        ThrowAdapterException();
        return result;
    }

    /// <summary>Number of selected items.</summary>
    public int Size
    {
        get
        {
            ThrowIfDisposed();
            return IGSharp_SelectionBasicStorage_GetSize(_handle);
        }
    }

    /// <summary>When true, <see cref="SelectedItems"/> yields items in order of selection (slightly slower).</summary>
    public bool PreserveOrder
    {
        get
        {
            ThrowIfDisposed();
            return IGSharp_SelectionBasicStorage_GetPreserveOrder(_handle);
        }
        set
        {
            ThrowIfDisposed();
            IGSharp_SelectionBasicStorage_SetPreserveOrder(_handle, value);
        }
    }

    /// <summary>Opaque user data pointer, available for your own use (e.g. from an adapter).</summary>
    public nint UserData
    {
        get
        {
            ThrowIfDisposed();
            return (nint)IGSharp_SelectionBasicStorage_GetUserData(_handle);
        }
        set
        {
            ThrowIfDisposed();
            IGSharp_SelectionBasicStorage_SetUserData(_handle, (void*)value);
        }
    }

    /// <summary>
    /// Sets the adapter used by <see cref="ApplyRequests"/> to convert item indices to
    /// storage IDs. Pass null to restore the default (id == index) adapter.
    /// </summary>
    public void SetIndexToStorageIdAdapter(SelectionIndexToStorageId? adapter)
    {
        ThrowIfDisposed();
        if (adapter == null)
        {
            _adapters.TryRemove((nint)_handle, out _);
            IGSharp_SelectionBasicStorage_SetAdapterIndexToStorageId(_handle, null);
        }
        else
        {
            _adapters[(nint)_handle] = new AdapterState(adapter);
            IGSharp_SelectionBasicStorage_SetAdapterIndexToStorageId(_handle, &IndexToStorageIdThunk);
        }
    }

    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
    private static uint IndexToStorageIdThunk(IGSharp_SelectionBasicStorage* self, int index)
    {
        if (!_adapters.TryGetValue((nint)self, out var state))
            return (uint)index;
        try { using var guard = Context.TryGetCurrent()?.EnterCallback(); return state.Adapter(index); }
        catch (Exception ex)
        {
            state.Capture(ex);
            return (uint)index;
        }
    }

    /// <summary>Swaps selection contents and order with another storage, preserving each owner's configuration and adapter.</summary>
    public void Swap(SelectionBasicStorage other)
    {
        ArgumentNullException.ThrowIfNull(other); ThrowIfDisposed(); other.ThrowIfDisposed();
        if (ReferenceEquals(this, other)) return;
        IGSharp_SelectionBasicStorage_Swap(_handle, other._handle);

    }

    /// <summary>Releases the unmanaged selection storage.</summary>
    public void Dispose()
    {
        if (_handle != null)
        {
            _lifetime.Dispose();
            _handle = null;
        }
    }

    private void ThrowIfDisposed()
    {
        _lifetime.Check();
    }

    private void ThrowAdapterException()
    {
        if (_adapters.TryGetValue((nint)_handle, out var state))
            state.ThrowIfFailed();
    }

    private sealed class AdapterState(SelectionIndexToStorageId adapter)
    {
        private ExceptionDispatchInfo? _exception;
        public SelectionIndexToStorageId Adapter { get; } = adapter;
        public void Capture(Exception exception) => _exception ??= ExceptionDispatchInfo.Capture(exception);
        public void ThrowIfFailed()
        {
            var exception = Interlocked.Exchange(ref _exception, null);
            exception?.Throw();
        }
    }
}
