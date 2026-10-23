using static SdlSharp.ImGui.Native;

namespace SdlSharp.ImGui;

/// <summary>
/// A single selection request emitted by multi-select. Obtain via
/// <see cref="MultiSelectIO.GetRequest"/>. The metadata is copied and can be retained.
/// </summary>
public readonly unsafe struct SelectionRequest
{

    internal SelectionRequest(IGSharp_SelectionRequest* handle)
    {
        this = default;
        if (handle == null) return;
        IsValid = true;
        RangeLastItem = IGSharp_SelectionRequest_GetRangeLastItem(handle);
        RangeFirstItem = IGSharp_SelectionRequest_GetRangeFirstItem(handle);
        Type = (SelectionRequestType)IGSharp_SelectionRequest_GetType(handle);
        Selected = IGSharp_SelectionRequest_GetSelected(handle);
        RangeDirection = IGSharp_SelectionRequest_GetRangeDirection(handle);
    }

    /// <summary>True if this handle refers to a valid request.</summary>
    public bool IsValid { get; }

    /// <summary>What kind of request this is.</summary>
    public SelectionRequestType Type { get; }

    /// <summary>For SetAll / SetRange: true to select, false to unselect.</summary>
    public bool Selected { get; }

    /// <summary>For SetRange: +1 when RangeFirstItem precedes RangeLastItem, -1 otherwise.</summary>
    public int RangeDirection { get; }

    /// <summary>For SetRange: first item in the range (inclusive). User-data value set via <see cref="ImGui.SetNextItemSelectionUserData"/>.</summary>
    public long RangeFirstItem { get; }

    /// <summary>For SetRange: last item in the range (inclusive). User-data value set via <see cref="ImGui.SetNextItemSelectionUserData"/>.</summary>
    public long RangeLastItem { get; }
}
