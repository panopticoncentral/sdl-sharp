namespace SdlSharp.ImGui;

/// <summary>Flags for <see cref="ImGui.IsItemHovered"/> and <see cref="ImGui.IsWindowHovered"/>.</summary>
[Flags]
public enum HoveredFlags
{
    /// <summary>No flags — return true if directly over the item/window, not obstructed.</summary>
    None = 0,
    /// <summary>IsWindowHovered only: return true if any children of the window is hovered.</summary>
    ChildWindows = 1 << 0,
    /// <summary>IsWindowHovered only: test from root window.</summary>
    RootWindow = 1 << 1,
    /// <summary>IsWindowHovered only: return true if any window is hovered.</summary>
    AnyWindow = 1 << 2,
    /// <summary>IsWindowHovered only: do not consider popup hierarchy.</summary>
    NoPopupHierarchy = 1 << 3,
    /// <summary>Return true even if a popup window is normally blocking access to this item/window.</summary>
    AllowWhenBlockedByPopup = 1 << 5,
    /// <summary>Return true even if an active item is blocking access.</summary>
    AllowWhenBlockedByActiveItem = 1 << 7,
    /// <summary>IsItemHovered only: return true even if the item uses AllowOverlap mode and is overlapped by another hoverable item.</summary>
    AllowWhenOverlappedByItem = 1 << 8,
    /// <summary>IsItemHovered only: return true even if obstructed or overlapped by another window.</summary>
    AllowWhenOverlappedByWindow = 1 << 9,
    /// <summary>IsItemHovered only: return true even if the item is disabled.</summary>
    AllowWhenDisabled = 1 << 10,
    /// <summary>IsItemHovered only: disable using keyboard/gamepad navigation state when active.</summary>
    NoNavOverride = 1 << 11,
    /// <summary>Combination: AllowWhenOverlappedByItem | AllowWhenOverlappedByWindow.</summary>
    AllowWhenOverlapped = AllowWhenOverlappedByItem | AllowWhenOverlappedByWindow,
    /// <summary>Combination for detecting "rect only" visibility.</summary>
    RectOnly = AllowWhenBlockedByPopup | AllowWhenBlockedByActiveItem | AllowWhenOverlapped,
    /// <summary>Combination: RootWindow | ChildWindows.</summary>
    RootAndChildWindows = RootWindow | ChildWindows,
    /// <summary>Shortcut for standard flags when using IsItemHovered + SetTooltip sequence.</summary>
    ForTooltip = 1 << 12,
    /// <summary>Require mouse to be stationary for style.HoverStationaryDelay before returning true.</summary>
    Stationary = 1 << 13,
    /// <summary>IsItemHovered only: return true immediately (default).</summary>
    DelayNone = 1 << 14,
    /// <summary>IsItemHovered only: return true after style.HoverDelayShort elapsed.</summary>
    DelayShort = 1 << 15,
    /// <summary>IsItemHovered only: return true after style.HoverDelayNormal elapsed.</summary>
    DelayNormal = 1 << 16,
    /// <summary>IsItemHovered only: disable shared delay system.</summary>
    NoSharedDelay = 1 << 17,
}
