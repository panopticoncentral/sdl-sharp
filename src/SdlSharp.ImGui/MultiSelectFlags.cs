namespace SdlSharp.Gui;

/// <summary>Flags for <see cref="Gui.BeginMultiSelect"/>.</summary>
[Flags]
public enum MultiSelectFlags
{
    /// <summary>No flags.</summary>
    None = 0,
    /// <summary>Disable selecting more than one item.</summary>
    SingleSelect = 1 << 0,
    /// <summary>Disable Ctrl+A shortcut to select all.</summary>
    NoSelectAll = 1 << 1,
    /// <summary>Disable Shift+selection mouse/keyboard range support.</summary>
    NoRangeSelect = 1 << 2,
    /// <summary>Disable selecting items when navigating (useful for e.g. checkbox range select).</summary>
    NoAutoSelect = 1 << 3,
    /// <summary>Disable clearing selection when navigating or selecting another item.</summary>
    NoAutoClear = 1 << 4,
    /// <summary>Disable clearing selection when clicking an already-selected item.</summary>
    NoAutoClearOnReselect = 1 << 5,
    /// <summary>Enable box-selection for items with same width / x-pos (e.g. full-row Selectable).</summary>
    BoxSelect1d = 1 << 6,
    /// <summary>Enable box-selection for varying-width / 2D layouts.</summary>
    BoxSelect2d = 1 << 7,
    /// <summary>Disable scrolling when box-selecting near edges.</summary>
    BoxSelectNoScroll = 1 << 8,
    /// <summary>Clear selection when pressing Escape while scope is focused.</summary>
    ClearOnEscape = 1 << 9,
    /// <summary>Clear selection when clicking on empty space within scope.</summary>
    ClearOnClickVoid = 1 << 10,
    /// <summary>Scope is the whole window (default).</summary>
    ScopeWindow = 1 << 11,
    /// <summary>Scope is the rectangle enclosing BeginMultiSelect..EndMultiSelect.</summary>
    ScopeRect = 1 << 12,
    /// <summary>Apply selection on mouse down (unselected) / up (already selected) — default.</summary>
    SelectOnAuto = 1 << 13,
    /// <summary>Apply selection on mouse down always (Excel-style).</summary>
    SelectOnClickAlways = 1 << 14,
    /// <summary>Apply selection on mouse release when clicking an unselected item.</summary>
    SelectOnClickRelease = 1 << 15,
    /// <summary>Enable navigation wrapping on X axis.</summary>
    NavWrapX = 1 << 16,
    /// <summary>Disable default right-click selection (for context menus).</summary>
    NoSelectOnRightClick = 1 << 17,
}
