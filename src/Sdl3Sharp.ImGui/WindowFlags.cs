using Sdl3Sharp.ImGui.Native;

namespace Sdl3Sharp.ImGui;

/// <summary>
/// Flags for window creation and behavior.
/// </summary>
/// <remarks>
/// These are per-window flags. There are shared flags in <see cref="IO"/>:
/// <c>ConfigWindowsResizeFromEdges</c> and <c>ConfigWindowsMoveFromTitleBarOnly</c>.
/// </remarks>
[Flags]
public enum WindowFlags
{
    /// <summary>
    /// No flags set.
    /// </summary>
    None = ImGuiWindowFlags.None,

    /// <summary>
    /// Disable title bar.
    /// </summary>
    NoTitleBar = ImGuiWindowFlags.NoTitleBar,

    /// <summary>
    /// Disable user resizing with the lower-right grip.
    /// </summary>
    NoResize = ImGuiWindowFlags.NoResize,

    /// <summary>
    /// Disable user moving the window.
    /// </summary>
    NoMove = ImGuiWindowFlags.NoMove,

    /// <summary>
    /// Disable scrollbars (window can still scroll with mouse or programmatically).
    /// </summary>
    NoScrollbar = ImGuiWindowFlags.NoScrollbar,

    /// <summary>
    /// Disable user vertically scrolling with mouse wheel. On child window, mouse wheel will be forwarded to the parent unless NoScrollbar is also set.
    /// </summary>
    NoScrollWithMouse = ImGuiWindowFlags.NoScrollWithMouse,

    /// <summary>
    /// Disable user collapsing window by double-clicking on it. Also referred to as Window Menu Button (e.g. within a docking node).
    /// </summary>
    NoCollapse = ImGuiWindowFlags.NoCollapse,

    /// <summary>
    /// Resize every window to its content every frame.
    /// </summary>
    AlwaysAutoResize = ImGuiWindowFlags.AlwaysAutoResize,

    /// <summary>
    /// Disable drawing background color (WindowBg, etc.) and outside border. Similar as using SetNextWindowBgAlpha(0.0f).
    /// </summary>
    NoBackground = ImGuiWindowFlags.NoBackground,

    /// <summary>
    /// Never load/save settings in .ini file.
    /// </summary>
    NoSavedSettings = ImGuiWindowFlags.NoSavedSettings,

    /// <summary>
    /// Disable catching mouse, hovering test with pass through.
    /// </summary>
    NoMouseInputs = ImGuiWindowFlags.NoMouseInputs,

    /// <summary>
    /// Has a menu-bar.
    /// </summary>
    MenuBar = ImGuiWindowFlags.MenuBar,

    /// <summary>
    /// Allow horizontal scrollbar to appear (off by default). You may use SetNextWindowContentSize(ImVec2(width,0.0f)); prior to calling Begin() to specify width.
    /// </summary>
    HorizontalScrollbar = ImGuiWindowFlags.HorizontalScrollbar,

    /// <summary>
    /// Disable taking focus when transitioning from hidden to visible state.
    /// </summary>
    NoFocusOnAppearing = ImGuiWindowFlags.NoFocusOnAppearing,

    /// <summary>
    /// Disable bringing window to front when taking focus (e.g. clicking on it or programmatically giving it focus).
    /// </summary>
    NoBringToFrontOnFocus = ImGuiWindowFlags.NoBringToFrontOnFocus,

    /// <summary>
    /// Always show vertical scrollbar (even if ContentSize.y &lt; Size.y).
    /// </summary>
    AlwaysVerticalScrollbar = ImGuiWindowFlags.AlwaysVerticalScrollbar,

    /// <summary>
    /// Always show horizontal scrollbar (even if ContentSize.x &lt; Size.x).
    /// </summary>
    AlwaysHorizontalScrollbar = ImGuiWindowFlags.AlwaysHorizontalScrollbar,

    /// <summary>
    /// No keyboard/gamepad navigation within the window.
    /// </summary>
    NoNavInputs = ImGuiWindowFlags.NoNavInputs,

    /// <summary>
    /// No focusing toward this window with keyboard/gamepad navigation (e.g. skipped by Ctrl+Tab).
    /// </summary>
    NoNavFocus = ImGuiWindowFlags.NoNavFocus,

    /// <summary>
    /// Display a dot next to the title. When used in a tab/docking context, tab is selected when clicking the X + closure is not assumed.
    /// </summary>
    UnsavedDocument = ImGuiWindowFlags.UnsavedDocument,

    /// <summary>
    /// Disable all navigation (NoNavInputs | NoNavFocus).
    /// </summary>
    NoNav = ImGuiWindowFlags.NoNav,

    /// <summary>
    /// Disable all window decorations (NoTitleBar | NoResize | NoScrollbar | NoCollapse).
    /// </summary>
    NoDecoration = ImGuiWindowFlags.NoDecoration,

    /// <summary>
    /// Disable all inputs (NoMouseInputs | NoNavInputs | NoNavFocus).
    /// </summary>
    NoInputs = ImGuiWindowFlags.NoInputs
}
