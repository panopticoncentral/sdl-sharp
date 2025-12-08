using Sdl3Sharp.ImGui.Native;

namespace Sdl3Sharp.ImGui;

/// <summary>
/// Flags for TableSetupColumn().
/// </summary>
[Flags]
public enum TableColumnFlags
{
    /// <summary>
    /// No flags set.
    /// </summary>
    None = ImGuiTableColumnFlags.None,

    /// <summary>
    /// Overriding/master disable flag: hide column, won't show in context menu (unlike calling TableSetColumnEnabled() which manipulates the user accessible state).
    /// </summary>
    Disabled = ImGuiTableColumnFlags.Disabled,

    /// <summary>
    /// Default as a hidden/disabled column.
    /// </summary>
    DefaultHide = ImGuiTableColumnFlags.DefaultHide,

    /// <summary>
    /// Default as a sorting column.
    /// </summary>
    DefaultSort = ImGuiTableColumnFlags.DefaultSort,

    /// <summary>
    /// Column will stretch. Preferable with horizontal scrolling disabled (default if table sizing policy is SizingStretchSame or SizingStretchProp).
    /// </summary>
    WidthStretch = ImGuiTableColumnFlags.WidthStretch,

    /// <summary>
    /// Column will not stretch. Preferable with horizontal scrolling enabled (default if table sizing policy is SizingFixedFit and table is resizable).
    /// </summary>
    WidthFixed = ImGuiTableColumnFlags.WidthFixed,

    /// <summary>
    /// Disable manual resizing.
    /// </summary>
    NoResize = ImGuiTableColumnFlags.NoResize,

    /// <summary>
    /// Disable manual reordering this column, this will also prevent other columns from crossing over this column.
    /// </summary>
    NoReorder = ImGuiTableColumnFlags.NoReorder,

    /// <summary>
    /// Disable ability to hide/disable this column.
    /// </summary>
    NoHide = ImGuiTableColumnFlags.NoHide,

    /// <summary>
    /// Disable clipping for this column (all NoClip columns will render in a same draw command).
    /// </summary>
    NoClip = ImGuiTableColumnFlags.NoClip,

    /// <summary>
    /// Disable ability to sort on this field (even if TableFlags.Sortable is set on the table).
    /// </summary>
    NoSort = ImGuiTableColumnFlags.NoSort,

    /// <summary>
    /// Disable ability to sort in the ascending direction.
    /// </summary>
    NoSortAscending = ImGuiTableColumnFlags.NoSortAscending,

    /// <summary>
    /// Disable ability to sort in the descending direction.
    /// </summary>
    NoSortDescending = ImGuiTableColumnFlags.NoSortDescending,

    /// <summary>
    /// TableHeadersRow() will submit an empty label for this column. Convenient for some small columns. Name will still appear in context menu or in angled headers.
    /// </summary>
    NoHeaderLabel = ImGuiTableColumnFlags.NoHeaderLabel,

    /// <summary>
    /// Disable header text width contribution to automatic column width.
    /// </summary>
    NoHeaderWidth = ImGuiTableColumnFlags.NoHeaderWidth,

    /// <summary>
    /// Make the initial sort direction Ascending when first sorting on this column (default).
    /// </summary>
    PreferSortAscending = ImGuiTableColumnFlags.PreferSortAscending,

    /// <summary>
    /// Make the initial sort direction Descending when first sorting on this column.
    /// </summary>
    PreferSortDescending = ImGuiTableColumnFlags.PreferSortDescending,

    /// <summary>
    /// Use current Indent value when entering cell (default for column 0).
    /// </summary>
    IndentEnable = ImGuiTableColumnFlags.IndentEnable,

    /// <summary>
    /// Ignore current Indent value when entering cell (default for columns > 0). Indentation changes within the cell will still be honored.
    /// </summary>
    IndentDisable = ImGuiTableColumnFlags.IndentDisable,

    /// <summary>
    /// TableHeadersRow() will submit an angled header row for this column. Note this will add an extra row.
    /// </summary>
    AngledHeader = ImGuiTableColumnFlags.AngledHeader,

    /// <summary>
    /// Status: is enabled == not hidden by user/api (referred to as "Hide" in DefaultHide and NoHide) flags.
    /// </summary>
    IsEnabled = ImGuiTableColumnFlags.IsEnabled,

    /// <summary>
    /// Status: is visible == is enabled AND not clipped by scrolling.
    /// </summary>
    IsVisible = ImGuiTableColumnFlags.IsVisible,

    /// <summary>
    /// Status: is currently part of the sort specs.
    /// </summary>
    IsSorted = ImGuiTableColumnFlags.IsSorted,

    /// <summary>
    /// Status: is hovered by mouse.
    /// </summary>
    IsHovered = ImGuiTableColumnFlags.IsHovered
}
