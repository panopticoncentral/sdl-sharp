using System.Collections.Concurrent;
using System.Runtime.InteropServices;
using Sdl3Sharp.ImGui.Native;

namespace Sdl3Sharp.ImGui;

/// <summary>
/// Optional helper to store multi-selection state and apply multi-selection requests.
/// This is a convenience class that wraps ImGuiSelectionBasicStorage.
/// </summary>
/// <remarks>
/// <para>
/// This enables standard multi-selection/range-selection idioms using index-based selection.
/// </para>
/// <para>
/// Usage:
/// <list type="bullet">
/// <item>Create an instance and keep it alive while using multi-select.</item>
/// <item>Call <see cref="ApplyRequests"/> with the MultiSelectIO from BeginMultiSelect/EndMultiSelect.</item>
/// <item>Use <see cref="Contains"/> to check if an index is selected.</item>
/// <item>Iterate with <see cref="GetSelectedItems"/> to get all selected indices.</item>
/// </list>
/// </para>
/// </remarks>
public unsafe struct SelectionBasicStorage : IDisposable
{
    private static long s_nextId = 1;
    private static readonly ConcurrentDictionary<long, Func<int, Id>> s_adapters = new();

    private ImGuiSelectionBasicStorage _native;
    private readonly long _adapterId;

    /// <summary>
    /// Initializes a new instance of the <see cref="SelectionBasicStorage"/> struct
    /// with the default adapter that uses index + 1 as the storage ID.
    /// </summary>
    public SelectionBasicStorage()
    {
        _adapterId = 0;
        _native = new ImGuiSelectionBasicStorage
        {
            // Set up the default adapter that uses index as storage ID
            AdapterIndexToStorageId = &DefaultAdapterIndexToStorageId,
            SelectionOrder = 1
        };
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="SelectionBasicStorage"/> struct
    /// with a custom adapter function to convert item indices to storage IDs.
    /// </summary>
    /// <param name="adapterIndexToStorageId">
    /// A function that converts an item index to a unique storage ID.
    /// The function receives the item index and should return a unique ID for that item.
    /// </param>
    public SelectionBasicStorage(Func<int, Id> adapterIndexToStorageId)
    {
        _adapterId = Interlocked.Increment(ref s_nextId);
        s_adapters[_adapterId] = adapterIndexToStorageId;

        _native = new ImGuiSelectionBasicStorage
        {
            AdapterIndexToStorageId = &ManagedAdapterIndexToStorageId,
            UserData = (void*)_adapterId,
            SelectionOrder = 1
        };
    }

    [UnmanagedCallersOnly(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    private static ImGuiID DefaultAdapterIndexToStorageId(ImGuiSelectionBasicStorage* self, int idx)
    {
        return (uint)idx;
    }

    [UnmanagedCallersOnly(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    private static ImGuiID ManagedAdapterIndexToStorageId(ImGuiSelectionBasicStorage* self, int idx)
    {
        var adapterId = (long)(nuint)self->UserData;
        if (s_adapters.TryGetValue(adapterId, out Func<int, Id>? adapter))
        {
            return adapter(idx).Value;
        }

        // Fallback to default behavior if adapter not found
        return (uint)idx;
    }

    /// <summary>
    /// Gets the number of selected items.
    /// </summary>
    public readonly int Size => _native.Size;

    /// <summary>
    /// Gets or sets whether to preserve selection order when iterating.
    /// </summary>
    public bool PreserveOrder
    {
        readonly get => _native.PreserveOrder;
        set => _native.PreserveOrder = value;
    }

    /// <summary>
    /// Applies selection requests from BeginMultiSelect() and EndMultiSelect().
    /// </summary>
    /// <param name="msIo">The MultiSelectIO containing selection requests.</param>
    public void ApplyRequests(MultiSelectIO msIo)
    {
        fixed (ImGuiSelectionBasicStorage* ptr = &_native)
        {
            ImGuiSelectionBasicStorage.ApplyRequests(ptr, msIo.Native);
        }
    }

    /// <summary>
    /// Checks if an item index is in the selection.
    /// </summary>
    /// <param name="id">The item ID.</param>
    /// <returns>True if the item is selected.</returns>
    public bool Contains(Id id)
    {
        fixed (ImGuiSelectionBasicStorage* ptr = &_native)
        {
            return ImGuiSelectionBasicStorage.Contains(ptr, id.Value);
        }
    }

    /// <summary>
    /// Clears all selected items.
    /// </summary>
    public void Clear()
    {
        fixed (ImGuiSelectionBasicStorage* ptr = &_native)
        {
            ImGuiSelectionBasicStorage.Clear(ptr);
        }
    }

    /// <summary>
    /// Sets whether an item is selected.
    /// </summary>
    /// <param name="id">The item ID.</param>
    /// <param name="selected">True to select, false to deselect.</param>
    public void SetItemSelected(Id id, bool selected)
    {
        fixed (ImGuiSelectionBasicStorage* ptr = &_native)
        {
            ImGuiSelectionBasicStorage.SetItemSelected(ptr, id.Value, selected);
        }
    }

    /// <summary>
    /// Enumerates all selected item IDs.
    /// </summary>
    /// <returns>A list of selected item IDs.</returns>
    public List<Id> GetSelectedItems()
    {
        var result = new List<Id>();
        fixed (ImGuiSelectionBasicStorage* ptr = &_native)
        {
            void* it = null;
            ImGuiID id;
            while (ImGuiSelectionBasicStorage.GetNextSelectedItem(ptr, &it, &id))
            {
                result.Add(new(id));
            }
        }

        return result;
    }

    /// <inheritdoc/>
    public void Dispose()
    {
        fixed (ImGuiSelectionBasicStorage* ptr = &_native)
        {
            ImGuiSelectionBasicStorage.Clear(ptr);
        }

        if (_adapterId != 0)
        {
            _ = s_adapters.TryRemove(_adapterId, out _);
        }
    }
}
