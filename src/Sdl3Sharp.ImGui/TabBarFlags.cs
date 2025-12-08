using Sdl3Sharp.ImGui.Native;

namespace Sdl3Sharp.ImGui;

/// <summary>
/// Flags for BeginTabBar().
/// </summary>
[Flags]
public enum TabBarFlags
{
    /// <summary>
    /// No flags set.
    /// </summary>
    None = ImGuiTabBarFlags.None,

    /// <summary>
    /// Allow manually dragging tabs to re-order them + New tabs are appended at the end of list.
    /// </summary>
    Reorderable = ImGuiTabBarFlags.Reorderable,

    /// <summary>
    /// Automatically select new tabs when they appear.
    /// </summary>
    AutoSelectNewTabs = ImGuiTabBarFlags.AutoSelectNewTabs,

    /// <summary>
    /// Disable buttons to open the tab list popup.
    /// </summary>
    TabListPopupButton = ImGuiTabBarFlags.TabListPopupButton,

    /// <summary>
    /// Disable behavior of closing tabs (that are submitted with p_open != NULL) with middle mouse button. You may handle this behavior manually on user's side with if (IsItemHovered() &amp;&amp; IsMouseClicked(2)) *p_open = false.
    /// </summary>
    NoCloseWithMiddleMouseButton = ImGuiTabBarFlags.NoCloseWithMiddleMouseButton,

    /// <summary>
    /// Disable scrolling buttons (apply when fitting policy is FittingPolicyScroll).
    /// </summary>
    NoTabListScrollingButtons = ImGuiTabBarFlags.NoTabListScrollingButtons,

    /// <summary>
    /// Disable tooltips when hovering a tab.
    /// </summary>
    NoTooltip = ImGuiTabBarFlags.NoTooltip,

    /// <summary>
    /// Draw selected overline markers over selected tab.
    /// </summary>
    DrawSelectedOverline = ImGuiTabBarFlags.DrawSelectedOverline,

    /// <summary>
    /// Shrink down tabs when they don't fit, until width is style.TabMinWidthShrink, then enable scrolling buttons.
    /// </summary>
    FittingPolicyMixed = ImGuiTabBarFlags.FittingPolicyMixed,

    /// <summary>
    /// Shrink down tabs when they don't fit.
    /// </summary>
    FittingPolicyShrink = ImGuiTabBarFlags.FittingPolicyShrink,

    /// <summary>
    /// Enable scrolling buttons when tabs don't fit.
    /// </summary>
    FittingPolicyScroll = ImGuiTabBarFlags.FittingPolicyScroll
}
