using Sdl3Sharp.ImGui.Native;

namespace Sdl3Sharp.ImGui;

/// <summary>
/// Flags for PushItemFlag().
/// </summary>
/// <remarks>
/// These are shared by all items.
/// </remarks>
[Flags]
public enum ItemFlags
{
    /// <summary>
    /// No flags set (default).
    /// </summary>
    None = ImGuiItemFlags.None,

    /// <summary>
    /// Disable keyboard tabbing. This is a "lighter" version of NoNav.
    /// </summary>
    NoTabStop = ImGuiItemFlags.NoTabStop,

    /// <summary>
    /// Disable any form of focusing (keyboard/gamepad directional navigation and SetKeyboardFocusHere() calls).
    /// </summary>
    NoNav = ImGuiItemFlags.NoNav,

    /// <summary>
    /// Disable item being a candidate for default focus (e.g. used by title bar items).
    /// </summary>
    NoNavDefaultFocus = ImGuiItemFlags.NoNavDefaultFocus,

    /// <summary>
    /// Any button-like behavior will have repeat mode enabled (based on io.KeyRepeatDelay and io.KeyRepeatRate values). Note that you can also call IsItemActive() after any button to tell if it is being held.
    /// </summary>
    ButtonRepeat = ImGuiItemFlags.ButtonRepeat,

    /// <summary>
    /// MenuItem()/Selectable() automatically close their parent popup window.
    /// </summary>
    AutoClosePopups = ImGuiItemFlags.AutoClosePopups,

    /// <summary>
    /// Allow submitting an item with the same identifier as an item already submitted this frame without triggering a warning tooltip if io.ConfigDebugHighlightIdConflicts is set.
    /// </summary>
    AllowDuplicateId = ImGuiItemFlags.AllowDuplicateId
}
