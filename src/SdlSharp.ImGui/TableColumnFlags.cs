namespace SdlSharp.ImGui;

/// <summary>Flags for <see cref="ImGui.TableSetupColumn"/>.</summary>
[Flags]
public enum TableColumnFlags
{
    /// <summary>No flags.</summary>
    None = 0,
    /// <summary>Overriding/master disable flag: hide column, won't show in context menu.</summary>
    Disabled = 1 << 0,
    /// <summary>Default as a hidden/disabled column.</summary>
    DefaultHide = 1 << 1,
    /// <summary>Default as a sorting column.</summary>
    DefaultSort = 1 << 2,
    /// <summary>Column will stretch.</summary>
    WidthStretch = 1 << 3,
    /// <summary>Column will not stretch.</summary>
    WidthFixed = 1 << 4,
    /// <summary>Disable manual resizing.</summary>
    NoResize = 1 << 5,
    /// <summary>Disable manual reordering this column.</summary>
    NoReorder = 1 << 6,
    /// <summary>Disable ability to hide/disable this column.</summary>
    NoHide = 1 << 7,
    /// <summary>Disable clipping for this column.</summary>
    NoClip = 1 << 8,
    /// <summary>Disable ability to sort on this field.</summary>
    NoSort = 1 << 9,
    /// <summary>Disable ability to sort in the ascending direction.</summary>
    NoSortAscending = 1 << 10,
    /// <summary>Disable ability to sort in the descending direction.</summary>
    NoSortDescending = 1 << 11,
    /// <summary>TableHeadersRow will submit an empty label for this column.</summary>
    NoHeaderLabel = 1 << 12,
    /// <summary>Disable header text width contribution to automatic column width.</summary>
    NoHeaderWidth = 1 << 13,
    /// <summary>Make the initial sort direction Ascending when first sorting on this column (default).</summary>
    PreferSortAscending = 1 << 14,
    /// <summary>Make the initial sort direction Descending when first sorting on this column.</summary>
    PreferSortDescending = 1 << 15,
    /// <summary>Use current Indent value when entering cell (default for column 0).</summary>
    IndentEnable = 1 << 16,
    /// <summary>Ignore current Indent value when entering cell (default for columns > 0).</summary>
    IndentDisable = 1 << 17,
    /// <summary>TableHeadersRow will submit an angled header row for this column.</summary>
    AngledHeader = 1 << 18,
    /// <summary>Status: is enabled == not hidden by user/api.</summary>
    IsEnabled = 1 << 24,
    /// <summary>Status: is visible == is enabled AND not clipped by scrolling.</summary>
    IsVisible = 1 << 25,
    /// <summary>Status: is currently part of the sort specs.</summary>
    IsSorted = 1 << 26,
    /// <summary>Status: is hovered by mouse.</summary>
    IsHovered = 1 << 27,
}
