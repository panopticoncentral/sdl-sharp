using Sdl3Sharp.ImGui.Native;

namespace Sdl3Sharp.ImGui;

/// <summary>
/// Flags for Selectable().
/// </summary>
[Flags]
public enum SelectableFlags
{
    /// <summary>
    /// No flags set.
    /// </summary>
    None = ImGuiSelectableFlags.None,

    /// <summary>
    /// Clicking this doesn't close parent popup window (overrides ItemFlags.AutoClosePopups).
    /// </summary>
    NoAutoClosePopups = ImGuiSelectableFlags.NoAutoClosePopups,

    /// <summary>
    /// Frame will span all columns of its container table (text will still fit in current column).
    /// </summary>
    SpanAllColumns = ImGuiSelectableFlags.SpanAllColumns,

    /// <summary>
    /// Generate press events on double clicks too.
    /// </summary>
    AllowDoubleClick = ImGuiSelectableFlags.AllowDoubleClick,

    /// <summary>
    /// Cannot be selected, display grayed out text.
    /// </summary>
    Disabled = ImGuiSelectableFlags.Disabled,

    /// <summary>
    /// (WIP) Hit testing to allow subsequent widgets to overlap this one.
    /// </summary>
    AllowOverlap = ImGuiSelectableFlags.AllowOverlap,

    /// <summary>
    /// Make the item be displayed as if it is hovered.
    /// </summary>
    Highlight = ImGuiSelectableFlags.Highlight,

    /// <summary>
    /// Auto-select when moved into, unless Ctrl is held. Automatic when in a BeginMultiSelect() block.
    /// </summary>
    SelectOnNav = ImGuiSelectableFlags.SelectOnNav
}
