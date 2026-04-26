namespace SdlSharp.Gui;

/// <summary>Flags for <see cref="Gui.Selectable(string, bool, SelectableFlags, float, float)"/>.</summary>
[Flags]
public enum SelectableFlags
{
    /// <summary>No flags.</summary>
    None = 0,
    /// <summary>Clicking this doesn't close parent popup window.</summary>
    NoAutoClosePopups = 1 << 0,
    /// <summary>Frame will span all columns of its container table.</summary>
    SpanAllColumns = 1 << 1,
    /// <summary>Generate press events on double clicks too.</summary>
    AllowDoubleClick = 1 << 2,
    /// <summary>Cannot be selected, display grayed out text.</summary>
    Disabled = 1 << 3,
    /// <summary>Hit testing will allow subsequent widgets to overlap this one.</summary>
    AllowOverlap = 1 << 4,
    /// <summary>Make the item be displayed as if it is hovered.</summary>
    Highlight = 1 << 5,
    /// <summary>Auto-select when moved into, unless Ctrl is held.</summary>
    SelectOnNav = 1 << 6,
}
