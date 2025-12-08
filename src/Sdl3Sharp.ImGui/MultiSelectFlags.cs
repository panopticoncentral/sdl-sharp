using Sdl3Sharp.ImGui.Native;

namespace Sdl3Sharp.ImGui;

/// <summary>
/// Flags for BeginMultiSelect().
/// </summary>
[Flags]
public enum MultiSelectFlags
{
    /// <summary>
    /// No flags set.
    /// </summary>
    None = ImGuiMultiSelectFlags.None,

    /// <summary>
    /// Disable selecting more than one item. This is available to allow single-selection code to share same code/logic if desired. It essentially disables the main purpose of BeginMultiSelect() tho!
    /// </summary>
    SingleSelect = ImGuiMultiSelectFlags.SingleSelect,

    /// <summary>
    /// Disable Ctrl+A shortcut to select all.
    /// </summary>
    NoSelectAll = ImGuiMultiSelectFlags.NoSelectAll,

    /// <summary>
    /// Disable Shift+selection mouse/keyboard support (useful for unordered 2D selection). With BoxSelect is also ensure contiguous SetRange requests are not combined into one. This allows not handling interpolation in SetRange requests.
    /// </summary>
    NoRangeSelect = ImGuiMultiSelectFlags.NoRangeSelect,

    /// <summary>
    /// Disable selecting items when navigating (useful for e.g. supporting range-select in a list of checkboxes).
    /// </summary>
    NoAutoSelect = ImGuiMultiSelectFlags.NoAutoSelect,

    /// <summary>
    /// Disable clearing selection when navigating or selecting another one (generally used with NoAutoSelect. useful for e.g. supporting range-select in a list of checkboxes).
    /// </summary>
    NoAutoClear = ImGuiMultiSelectFlags.NoAutoClear,

    /// <summary>
    /// Disable clearing selection when clicking/selecting an already selected item.
    /// </summary>
    NoAutoClearOnReselect = ImGuiMultiSelectFlags.NoAutoClearOnReselect,

    /// <summary>
    /// Enable box-selection with same width and same x pos items (e.g. full row Selectable()). Box-selection works better with little bit of spacing between items hit-box in order to be able to aim at empty space.
    /// </summary>
    BoxSelect1d = ImGuiMultiSelectFlags.BoxSelect1d,

    /// <summary>
    /// Enable box-selection with varying width or varying x pos items support (e.g. different width labels, or 2D layout/grid). This is slower: alters clipping logic so that e.g. horizontal movements will update selection of normally clipped items.
    /// </summary>
    BoxSelect2d = ImGuiMultiSelectFlags.BoxSelect2d,

    /// <summary>
    /// Disable scrolling when box-selecting near edges of scope.
    /// </summary>
    BoxSelectNoScroll = ImGuiMultiSelectFlags.BoxSelectNoScroll,

    /// <summary>
    /// Clear selection when pressing Escape while scope is focused.
    /// </summary>
    ClearOnEscape = ImGuiMultiSelectFlags.ClearOnEscape,

    /// <summary>
    /// Clear selection when clicking on empty location within scope.
    /// </summary>
    ClearOnClickVoid = ImGuiMultiSelectFlags.ClearOnClickVoid,

    /// <summary>
    /// Scope for BoxSelect and ClearOnClickVoid is whole window (Default). Use if BeginMultiSelect() covers a whole window or used a single time in same window.
    /// </summary>
    ScopeWindow = ImGuiMultiSelectFlags.ScopeWindow,

    /// <summary>
    /// Scope for BoxSelect and ClearOnClickVoid is rectangle encompassing BeginMultiSelect()/EndMultiSelect(). Use if BeginMultiSelect() is called multiple times in same window.
    /// </summary>
    ScopeRect = ImGuiMultiSelectFlags.ScopeRect,

    /// <summary>
    /// Apply selection on mouse down when clicking on unselected item. (Default)
    /// </summary>
    SelectOnClick = ImGuiMultiSelectFlags.SelectOnClick,

    /// <summary>
    /// Apply selection on mouse release when clicking an unselected item. Allow dragging an unselected item without altering selection.
    /// </summary>
    SelectOnClickRelease = ImGuiMultiSelectFlags.SelectOnClickRelease,

    /// <summary>
    /// [Temporary] Enable navigation wrapping on X axis. Provided as a convenience because we don't have a design for the general Nav API for this yet.
    /// </summary>
    NavWrapX = ImGuiMultiSelectFlags.NavWrapX,

    /// <summary>
    /// Disable default right-click processing, which selects item on mouse down, and is designed for context-menus.
    /// </summary>
    NoSelectOnRightClick = ImGuiMultiSelectFlags.NoSelectOnRightClick
}
