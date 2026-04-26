namespace SdlSharp.Gui;

/// <summary>Flags for <see cref="Gui.BeginTable"/>.</summary>
[Flags]
public enum TableFlags
{
    /// <summary>No flags.</summary>
    None = 0,
    /// <summary>Enable resizing columns.</summary>
    Resizable = 1 << 0,
    /// <summary>Enable reordering columns in header row.</summary>
    Reorderable = 1 << 1,
    /// <summary>Enable hiding/disabling columns in context menu.</summary>
    Hideable = 1 << 2,
    /// <summary>Enable sorting (call TableGetSortSpecs to obtain sort specs).</summary>
    Sortable = 1 << 3,
    /// <summary>Disable persisting columns order, width, visibility and sort settings in the .ini file.</summary>
    NoSavedSettings = 1 << 4,
    /// <summary>Right-click on columns body/contents will also display table context menu.</summary>
    ContextMenuInBody = 1 << 5,
    /// <summary>Set each RowBg color with ImGuiCol_TableRowBg or ImGuiCol_TableRowBgAlt.</summary>
    RowBg = 1 << 6,
    /// <summary>Draw horizontal borders between rows.</summary>
    BordersInnerH = 1 << 7,
    /// <summary>Draw horizontal borders at the top and bottom.</summary>
    BordersOuterH = 1 << 8,
    /// <summary>Draw vertical borders between columns.</summary>
    BordersInnerV = 1 << 9,
    /// <summary>Draw vertical borders on the left and right sides.</summary>
    BordersOuterV = 1 << 10,
    /// <summary>Draw horizontal borders (combination).</summary>
    BordersH = BordersInnerH | BordersOuterH,
    /// <summary>Draw vertical borders (combination).</summary>
    BordersV = BordersInnerV | BordersOuterV,
    /// <summary>Draw inner borders (combination).</summary>
    BordersInner = BordersInnerV | BordersInnerH,
    /// <summary>Draw outer borders (combination).</summary>
    BordersOuter = BordersOuterV | BordersOuterH,
    /// <summary>Draw all borders (combination).</summary>
    Borders = BordersInner | BordersOuter,
    /// <summary>Disable vertical borders in columns body.</summary>
    NoBordersInBody = 1 << 11,
    /// <summary>Disable vertical borders in columns body until hovered for resize.</summary>
    NoBordersInBodyUntilResize = 1 << 12,
    /// <summary>Columns default to WidthFixed or WidthAuto, matching contents width.</summary>
    SizingFixedFit = 1 << 13,
    /// <summary>Columns default to WidthFixed or WidthAuto, matching max contents width of all columns.</summary>
    SizingFixedSame = 2 << 13,
    /// <summary>Columns default to WidthStretch with weights proportional to contents widths.</summary>
    SizingStretchProp = 3 << 13,
    /// <summary>Columns default to WidthStretch with equal weights.</summary>
    SizingStretchSame = 4 << 13,
    /// <summary>Make outer width auto-fit to columns.</summary>
    NoHostExtendX = 1 << 16,
    /// <summary>Make outer height stop exactly at outer_size.y.</summary>
    NoHostExtendY = 1 << 17,
    /// <summary>Disable keeping column always minimally visible.</summary>
    NoKeepColumnsVisible = 1 << 18,
    /// <summary>Disable distributing remainder width to stretched columns.</summary>
    PreciseWidths = 1 << 19,
    /// <summary>Disable clipping rectangle for every individual column.</summary>
    NoClip = 1 << 20,
    /// <summary>Enable outermost padding (default if BordersOuterV is on).</summary>
    PadOuterX = 1 << 21,
    /// <summary>Disable outermost padding (default if BordersOuterV is off).</summary>
    NoPadOuterX = 1 << 22,
    /// <summary>Disable inner padding between columns.</summary>
    NoPadInnerX = 1 << 23,
    /// <summary>Enable horizontal scrolling.</summary>
    ScrollX = 1 << 24,
    /// <summary>Enable vertical scrolling.</summary>
    ScrollY = 1 << 25,
    /// <summary>Hold shift when clicking headers to sort on multiple column.</summary>
    SortMulti = 1 << 26,
    /// <summary>Allow no sorting, disable default sorting.</summary>
    SortTristate = 1 << 27,
    /// <summary>Highlight column headers when hovered.</summary>
    HighlightHoveredColumn = 1 << 28,
}
