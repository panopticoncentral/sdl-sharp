namespace SdlSharp.Gui;

/// <summary>Flags for <see cref="Gui.BeginTabBar"/>.</summary>
[Flags]
public enum TabBarFlags
{
    /// <summary>No flags.</summary>
    None = 0,
    /// <summary>Allow manually dragging tabs to re-order them + new tabs are appended at the end of list.</summary>
    Reorderable = 1 << 0,
    /// <summary>Automatically select new tabs when they appear.</summary>
    AutoSelectNewTabs = 1 << 1,
    /// <summary>Disable buttons to open the tab list popup.</summary>
    TabListPopupButton = 1 << 2,
    /// <summary>Disable behavior of closing tabs with middle mouse button.</summary>
    NoCloseWithMiddleMouseButton = 1 << 3,
    /// <summary>Disable scrolling buttons.</summary>
    NoTabListScrollingButtons = 1 << 4,
    /// <summary>Disable tooltips when hovering a tab.</summary>
    NoTooltip = 1 << 5,
    /// <summary>Draw selected overline markers over selected tab.</summary>
    DrawSelectedOverline = 1 << 6,
    /// <summary>Shrink tabs when they don't fit, then enable scrolling buttons.</summary>
    FittingPolicyMixed = 1 << 7,
    /// <summary>Shrink down tabs when they don't fit.</summary>
    FittingPolicyShrink = 1 << 8,
    /// <summary>Enable scrolling buttons when tabs don't fit.</summary>
    FittingPolicyScroll = 1 << 9,
}
