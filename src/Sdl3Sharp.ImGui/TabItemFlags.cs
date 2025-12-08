using Sdl3Sharp.ImGui.Native;

namespace Sdl3Sharp.ImGui;

/// <summary>
/// Flags for BeginTabItem().
/// </summary>
[Flags]
public enum TabItemFlags
{
    /// <summary>
    /// No flags set.
    /// </summary>
    None = ImGuiTabItemFlags.None,

    /// <summary>
    /// Display a dot next to the title + set NoAssumedClosure.
    /// </summary>
    UnsavedDocument = ImGuiTabItemFlags.UnsavedDocument,

    /// <summary>
    /// Trigger flag to programmatically make the tab selected when calling BeginTabItem().
    /// </summary>
    SetSelected = ImGuiTabItemFlags.SetSelected,

    /// <summary>
    /// Disable behavior of closing tabs (that are submitted with p_open != NULL) with middle mouse button. You may handle this behavior manually on user's side with if (IsItemHovered() &amp;&amp; IsMouseClicked(2)) *p_open = false.
    /// </summary>
    NoCloseWithMiddleMouseButton = ImGuiTabItemFlags.NoCloseWithMiddleMouseButton,

    /// <summary>
    /// Don't call PushID()/PopID() on BeginTabItem()/EndTabItem().
    /// </summary>
    NoPushId = ImGuiTabItemFlags.NoPushId,

    /// <summary>
    /// Disable tooltip for the given tab.
    /// </summary>
    NoTooltip = ImGuiTabItemFlags.NoTooltip,

    /// <summary>
    /// Disable reordering this tab or having another tab cross over this tab.
    /// </summary>
    NoReorder = ImGuiTabItemFlags.NoReorder,

    /// <summary>
    /// Enforce the tab position to the left of the tab bar (after the tab list popup button).
    /// </summary>
    Leading = ImGuiTabItemFlags.Leading,

    /// <summary>
    /// Enforce the tab position to the right of the tab bar (before the scrolling buttons).
    /// </summary>
    Trailing = ImGuiTabItemFlags.Trailing,

    /// <summary>
    /// Tab is selected when trying to close + closure is not immediately assumed (will wait for user to stop submitting the tab). Otherwise closure is assumed when pressing the X, so if you keep submitting the tab may reappear at end of tab bar.
    /// </summary>
    NoAssumedClosure = ImGuiTabItemFlags.NoAssumedClosure
}
