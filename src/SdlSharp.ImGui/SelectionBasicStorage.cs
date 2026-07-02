using System.Collections.Concurrent;
using System.Runtime.CompilerServices;
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
    private static readonly ConcurrentDictionary<nint, SelectionIndexToStorageId> _adapters = new();

    private IGSharp_SelectionBasicStorage* _handle;

    /// <summary>Creates a new, empty selection storage.</summary>
    public SelectionBasicStorage()
    {
        _handle = IGSharp_SelectionBasicStorage_Create();
    }

    /// <summary>Applies the selection requests coming from BeginMultiSelect/EndMultiSelect.</summary>
    public void ApplyRequests(MultiSelectIO io)
    {
        ThrowIfDisposed();
        IGSharp_SelectionBasicStorage_ApplyRequests(_handle, io.Handle);
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
    /// Enumerates the storage IDs of all selected items (in selection-storage order;
    /// see <see cref="PreserveOrder"/>). Do not modify the selection while enumerating.
    /// </summary>
    public IEnumerable<uint> SelectedItems
    {
        get
        {
            ThrowIfDisposed();
            return EnumerateSelectedItems();
        }
    }

    private IEnumerable<uint> EnumerateSelectedItems()
    {
        nint iterator = 0;
        while (TryGetNextSelectedItem(ref iterator, out var id))
            yield return id;
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
        return IGSharp_SelectionBasicStorage_GetStorageIdFromIndex(_handle, index);
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
            _adapters[(nint)_handle] = adapter;
            IGSharp_SelectionBasicStorage_SetAdapterIndexToStorageId(_handle, &IndexToStorageIdThunk);
        }
    }

    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
    private static uint IndexToStorageIdThunk(IGSharp_SelectionBasicStorage* self, int index)
    {
        // Fall back to the default id == index mapping if the registration vanished
        // (never throw across the native boundary).
        return _adapters.TryGetValue((nint)self, out var adapter) ? adapter(index) : (uint)index;
    }

    /// <summary>Releases the unmanaged selection storage.</summary>
    public void Dispose()
    {
        if (_handle != null)
        {
            _adapters.TryRemove((nint)_handle, out _);
            IGSharp_SelectionBasicStorage_Destroy(_handle);
            _handle = null;
        }
    }

    private void ThrowIfDisposed()
    {
        if (_handle == null) throw new ObjectDisposedException(nameof(SelectionBasicStorage));
    }
}
