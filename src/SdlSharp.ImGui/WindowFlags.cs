namespace SdlSharp.Gui;

/// <summary>Flags for <see cref="Gui.Begin(string, WindowFlags)"/>.</summary>
[Flags]
public enum WindowFlags
{
    /// <summary>No flags.</summary>
    None = 0,
    /// <summary>Disable title-bar.</summary>
    NoTitleBar = 1 << 0,
    /// <summary>Disable user resizing with the lower-right grip.</summary>
    NoResize = 1 << 1,
    /// <summary>Disable user moving the window.</summary>
    NoMove = 1 << 2,
    /// <summary>Disable scrollbars.</summary>
    NoScrollbar = 1 << 3,
    /// <summary>Disable user vertically scrolling with mouse wheel.</summary>
    NoScrollWithMouse = 1 << 4,
    /// <summary>Disable user collapsing window by double-clicking on it.</summary>
    NoCollapse = 1 << 5,
    /// <summary>Resize every window to its content every frame.</summary>
    AlwaysAutoResize = 1 << 6,
    /// <summary>Disable drawing background color.</summary>
    NoBackground = 1 << 7,
    /// <summary>Never load/save settings in .ini file.</summary>
    NoSavedSettings = 1 << 8,
    /// <summary>Disable catching mouse, hovering test with pass through.</summary>
    NoMouseInputs = 1 << 9,
    /// <summary>Has a menu-bar.</summary>
    MenuBar = 1 << 10,
    /// <summary>Allow horizontal scrollbar to appear.</summary>
    HorizontalScrollbar = 1 << 11,
    /// <summary>Disable taking focus when transitioning from hidden to visible state.</summary>
    NoFocusOnAppearing = 1 << 12,
    /// <summary>Disable bringing window to front when taking focus.</summary>
    NoBringToFrontOnFocus = 1 << 13,
    /// <summary>Always show vertical scrollbar.</summary>
    AlwaysVerticalScrollbar = 1 << 14,
    /// <summary>Always show horizontal scrollbar.</summary>
    AlwaysHorizontalScrollbar = 1 << 15,
    /// <summary>No keyboard/gamepad navigation within the window.</summary>
    NoNavInputs = 1 << 16,
    /// <summary>No focusing toward this window with keyboard/gamepad navigation.</summary>
    NoNavFocus = 1 << 17,
    /// <summary>Display a dot next to the title (used for unsaved documents).</summary>
    UnsavedDocument = 1 << 18,
    /// <summary>Combination: NoNavInputs | NoNavFocus.</summary>
    NoNav = NoNavInputs | NoNavFocus,
    /// <summary>Combination: NoTitleBar | NoResize | NoScrollbar | NoCollapse.</summary>
    NoDecoration = NoTitleBar | NoResize | NoScrollbar | NoCollapse,
    /// <summary>Combination: NoMouseInputs | NoNavInputs | NoNavFocus.</summary>
    NoInputs = NoMouseInputs | NoNavInputs | NoNavFocus,
}
