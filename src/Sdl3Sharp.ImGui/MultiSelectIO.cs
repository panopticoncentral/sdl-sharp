using Sdl3Sharp.ImGui.Native;

namespace Sdl3Sharp.ImGui;

/// <summary>
/// Main IO structure returned by BeginMultiSelect()/EndMultiSelect().
/// This mainly contains a list of selection requests.
/// </summary>
public unsafe readonly struct MultiSelectIO
{
    internal readonly ImGuiMultiSelectIO* Native;

    internal MultiSelectIO(ImGuiMultiSelectIO* native)
    {
        Native = native;
    }

    /// <summary>
    /// Gets whether this MultiSelectIO is valid (non-null).
    /// </summary>
    public bool IsValid => Native != null;

    /// <summary>
    /// Gets the number of selection requests.
    /// </summary>
    public int RequestCount => Native->Requests.Size;

    /// <summary>
    /// Gets a selection request by index.
    /// </summary>
    /// <param name="index">The index of the request.</param>
    /// <returns>The selection request at the specified index.</returns>
    public SelectionRequest GetRequest(int index)
    {
        return new SelectionRequest(&Native->Requests.Data[index]);
    }

    /// <summary>
    /// Gets the source item for range selection (often the first selected item).
    /// When using a clipper, this item must never be clipped.
    /// </summary>
    public long RangeSrcItem => Native->RangeSrcItem.Value;

    /// <summary>
    /// Gets the last known SetNextItemSelectionUserData() value for NavId (if part of submitted items).
    /// Useful when using deletion.
    /// </summary>
    public long NavIdItem => Native->NavIdItem.Value;

    /// <summary>
    /// Gets the last known selection state for NavId (if part of submitted items).
    /// Useful when using deletion.
    /// </summary>
    public bool NavIdSelected => Native->NavIdSelected;

    /// <summary>
    /// Gets or sets whether to reset the RangeSrcItem.
    /// Set this to true before EndMultiSelect() if you deleted the selection.
    /// </summary>
    public ref bool RangeSrcReset => ref Native->RangeSrcReset;

    /// <summary>
    /// Gets the items_count parameter passed to BeginMultiSelect().
    /// Copied here for convenience, allowing simpler calls to your ApplyRequests handler.
    /// </summary>
    public int ItemsCount => Native->ItemsCount;
}
