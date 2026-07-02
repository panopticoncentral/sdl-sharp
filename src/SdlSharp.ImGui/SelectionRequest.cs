using static SdlSharp.ImGui.Native;

namespace SdlSharp.ImGui;

/// <summary>
/// A single selection request emitted by multi-select. Obtain via
/// <see cref="MultiSelectIO.GetRequest"/>. Valid only for the current frame.
/// </summary>
public readonly unsafe struct SelectionRequest
{
    internal readonly IGSharp_SelectionRequest* Handle;

    internal SelectionRequest(IGSharp_SelectionRequest* handle) => Handle = handle;

    /// <summary>True if this handle refers to a valid request.</summary>
    public bool IsValid => Handle != null;

    /// <summary>What kind of request this is.</summary>
    public SelectionRequestType Type => (SelectionRequestType)IGSharp_SelectionRequest_GetType(Handle);

    /// <summary>For SetAll / SetRange: true to select, false to unselect.</summary>
    public bool Selected => IGSharp_SelectionRequest_GetSelected(Handle);

    /// <summary>For SetRange: +1 when RangeFirstItem precedes RangeLastItem, -1 otherwise.</summary>
    public int RangeDirection => IGSharp_SelectionRequest_GetRangeDirection(Handle);

    /// <summary>For SetRange: first item in the range (inclusive). User-data value set via <see cref="ImGui.SetNextItemSelectionUserData"/>.</summary>
    public long RangeFirstItem => IGSharp_SelectionRequest_GetRangeFirstItem(Handle);

    /// <summary>For SetRange: last item in the range (inclusive). User-data value set via <see cref="ImGui.SetNextItemSelectionUserData"/>.</summary>
    public long RangeLastItem => IGSharp_SelectionRequest_GetRangeLastItem(Handle);
}
