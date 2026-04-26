using static SdlSharp.ImGui.Native;

namespace SdlSharp.ImGui;

/// <summary>
/// Multi-select I/O context returned by <see cref="ImGui.BeginMultiSelect"/> and
/// <see cref="ImGui.EndMultiSelect"/>. Iterate <see cref="GetRequest"/> from 0 to
/// <see cref="RequestsCount"/> and apply each to your selection state.
/// </summary>
public readonly unsafe struct MultiSelectIO
{
    internal readonly void* Handle;

    internal MultiSelectIO(void* handle) => Handle = handle;

    /// <summary>True if this handle refers to a valid multi-select context.</summary>
    public bool IsValid => Handle != null;

    /// <summary>Number of selection requests to apply.</summary>
    public int RequestsCount => IGSharp_MultiSelectIO_GetRequestsCount(Handle);

    /// <summary>Gets the request at the given index.</summary>
    public SelectionRequest GetRequest(int index) => new(IGSharp_MultiSelectIO_GetRequest(Handle, index));

    /// <summary>(Clipper) Range source item — must be submitted even if clipped.</summary>
    public long RangeSrcItem => IGSharp_MultiSelectIO_GetRangeSrcItem(Handle);

    /// <summary>(Deletion) Last known selection user-data value for NavId.</summary>
    public long NavIdItem => IGSharp_MultiSelectIO_GetNavIdItem(Handle);

    /// <summary>(Deletion) Last known selection state for NavId.</summary>
    public bool NavIdSelected => IGSharp_MultiSelectIO_GetNavIdSelected(Handle);

    /// <summary>(Deletion) Set before EndMultiSelect to reset RangeSrcItem (e.g. after deleting selection).</summary>
    public bool RangeSrcReset
    {
        get => IGSharp_MultiSelectIO_GetRangeSrcReset(Handle);
        set => IGSharp_MultiSelectIO_SetRangeSrcReset(Handle, value);
    }

    /// <summary>Items count parameter passed to <see cref="ImGui.BeginMultiSelect"/>.</summary>
    public int ItemsCount => IGSharp_MultiSelectIO_GetItemsCount(Handle);
}
