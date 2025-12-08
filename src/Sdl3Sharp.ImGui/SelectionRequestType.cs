using Sdl3Sharp.ImGui.Native;

namespace Sdl3Sharp.ImGui;

/// <summary>
/// Selection request type.
/// </summary>
public enum SelectionRequestType
{
    /// <summary>
    /// No request.
    /// </summary>
    None = ImGuiSelectionRequestType.None,

    /// <summary>
    /// Request app to clear selection (if Selected==false) or select all items (if Selected==true). We cannot set RangeFirstItem/RangeLastItem as its contents is entirely up to user (not necessarily an index).
    /// </summary>
    SetAll = ImGuiSelectionRequestType.SetAll,

    /// <summary>
    /// Request app to select/unselect [RangeFirstItem..RangeLastItem] items (inclusive) based on value of Selected. Only EndMultiSelect() request this, app code can read after BeginMultiSelect() and it will always be false.
    /// </summary>
    SetRange = ImGuiSelectionRequestType.SetRange
}
