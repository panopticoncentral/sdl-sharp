using Sdl3Sharp.ImGui.Native;

namespace Sdl3Sharp.ImGui;

/// <summary>
/// Flags for IsItemHovered() and IsWindowHovered().
/// </summary>
/// <remarks>
/// <para>
/// Note: if you are trying to check whether your mouse should be dispatched to Dear ImGui or to your app,
/// you should use <see cref="IO.WantCaptureMouse"/> instead! Please read the FAQ!
/// </para>
/// <para>
/// Note: windows with the NoInputs window flag are ignored by IsWindowHovered() calls.
/// </para>
/// </remarks>
[Flags]
public enum HoveredFlags
{
    /// <summary>
    /// Return true if directly over the item/window, not obstructed by another window,
    /// not obstructed by an active popup or modal blocking inputs under them.
    /// </summary>
    None = ImGuiHoveredFlags.None,

    /// <summary>
    /// IsWindowHovered() only: Return true if any children of the window is hovered.
    /// </summary>
    ChildWindows = ImGuiHoveredFlags.ChildWindows,

    /// <summary>
    /// IsWindowHovered() only: Test from root window (top most parent of the current hierarchy).
    /// </summary>
    RootWindow = ImGuiHoveredFlags.RootWindow,

    /// <summary>
    /// IsWindowHovered() only: Return true if any window is hovered.
    /// </summary>
    AnyWindow = ImGuiHoveredFlags.AnyWindow,

    /// <summary>
    /// IsWindowHovered() only: Do not consider popup hierarchy (do not treat popup emitter as parent of popup)
    /// (when used with <see cref="ChildWindows"/> or <see cref="RootWindow"/>).
    /// </summary>
    NoPopupHierarchy = ImGuiHoveredFlags.NoPopupHierarchy,

    /// <summary>
    /// Return true even if a popup window is normally blocking access to this item/window.
    /// </summary>
    AllowWhenBlockedByPopup = ImGuiHoveredFlags.AllowWhenBlockedByPopup,

    /// <summary>
    /// Return true even if an active item is blocking access to this item/window.
    /// Useful for Drag and Drop patterns.
    /// </summary>
    AllowWhenBlockedByActiveItem = ImGuiHoveredFlags.AllowWhenBlockedByActiveItem,

    /// <summary>
    /// IsItemHovered() only: Return true even if the item uses AllowOverlap mode and is overlapped by another hoverable item.
    /// </summary>
    AllowWhenOverlappedByItem = ImGuiHoveredFlags.AllowWhenOverlappedByItem,

    /// <summary>
    /// IsItemHovered() only: Return true even if the position is obstructed or overlapped by another window.
    /// </summary>
    AllowWhenOverlappedByWindow = ImGuiHoveredFlags.AllowWhenOverlappedByWindow,

    /// <summary>
    /// IsItemHovered() only: Return true even if the item is disabled.
    /// </summary>
    AllowWhenDisabled = ImGuiHoveredFlags.AllowWhenDisabled,

    /// <summary>
    /// IsItemHovered() only: Disable using keyboard/gamepad navigation state when active, always query mouse.
    /// </summary>
    NoNavOverride = ImGuiHoveredFlags.NoNavOverride,

    /// <summary>
    /// Combination of <see cref="AllowWhenOverlappedByItem"/> and <see cref="AllowWhenOverlappedByWindow"/>.
    /// </summary>
    AllowWhenOverlapped = ImGuiHoveredFlags.AllowWhenOverlapped,

    /// <summary>
    /// Combination of <see cref="AllowWhenBlockedByPopup"/>, <see cref="AllowWhenBlockedByActiveItem"/>, and <see cref="AllowWhenOverlapped"/>.
    /// </summary>
    RectOnly = ImGuiHoveredFlags.RectOnly,

    /// <summary>
    /// Combination of <see cref="RootWindow"/> and <see cref="ChildWindows"/>.
    /// </summary>
    RootAndChildWindows = ImGuiHoveredFlags.RootAndChildWindows,

    /// <summary>
    /// Shortcut for standard flags when using IsItemHovered() + SetTooltip() sequence.
    /// </summary>
    ForTooltip = ImGuiHoveredFlags.ForTooltip,

    /// <summary>
    /// Require mouse to be stationary for style.HoverStationaryDelay (~0.15 sec) at least one time.
    /// After this, can move on same item/window. Using the stationary test tends to reduces the need for a long delay.
    /// </summary>
    Stationary = ImGuiHoveredFlags.Stationary,

    /// <summary>
    /// IsItemHovered() only: Return true immediately (default). As this is the default you generally ignore this.
    /// </summary>
    DelayNone = ImGuiHoveredFlags.DelayNone,

    /// <summary>
    /// IsItemHovered() only: Return true after style.HoverDelayShort elapsed (~0.15 sec) (shared between items)
    /// + requires mouse to be stationary for style.HoverStationaryDelay (once per item).
    /// </summary>
    DelayShort = ImGuiHoveredFlags.DelayShort,

    /// <summary>
    /// IsItemHovered() only: Return true after style.HoverDelayNormal elapsed (~0.40 sec) (shared between items)
    /// + requires mouse to be stationary for style.HoverStationaryDelay (once per item).
    /// </summary>
    DelayNormal = ImGuiHoveredFlags.DelayNormal,

    /// <summary>
    /// IsItemHovered() only: Disable shared delay system where moving from one item to the next
    /// keeps the previous timer for a short time (standard for tooltips with long delays).
    /// </summary>
    NoSharedDelay = ImGuiHoveredFlags.NoSharedDelay
}
