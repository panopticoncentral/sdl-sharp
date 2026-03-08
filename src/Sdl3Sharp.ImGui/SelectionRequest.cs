using Sdl3Sharp.ImGui.Native;

namespace Sdl3Sharp.ImGui;

/// <summary>
/// Represents a selection request from the multi-select system.
/// </summary>
public unsafe readonly struct SelectionRequest
{
    internal readonly ImGuiSelectionRequest* Native;

    internal SelectionRequest(ImGuiSelectionRequest* native)
    {
        Native = native;
    }

    /// <summary>
    /// Gets the request type.
    /// </summary>
    public SelectionRequestType Type => (SelectionRequestType)Native->Type;

    /// <summary>
    /// Gets whether the items should be selected (true) or unselected (false).
    /// </summary>
    public bool Selected => Native->Selected;

    /// <summary>
    /// Gets the range direction: +1 when RangeFirstItem comes before RangeLastItem, -1 otherwise.
    /// Useful if you want to preserve selection order on a backward Shift+Click.
    /// </summary>
    public sbyte RangeDirection => Native->RangeDirection;

    /// <summary>
    /// Gets the first item in the range (for SetRange requests).
    /// This is generally == RangeSrcItem when shift selecting from top to bottom.
    /// </summary>
    public long RangeFirstItem => Native->RangeFirstItem.Value;

    /// <summary>
    /// Gets the last item in the range (for SetRange requests). Inclusive!
    /// This is generally == RangeSrcItem when shift selecting from bottom to top.
    /// </summary>
    public long RangeLastItem => Native->RangeLastItem.Value;
}
