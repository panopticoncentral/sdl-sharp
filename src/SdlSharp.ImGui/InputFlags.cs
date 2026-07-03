namespace SdlSharp.ImGui;

/// <summary>
/// Flags for <see cref="ImGui.Shortcut"/> and <see cref="ImGui.SetNextItemShortcut"/>.
/// Mirrors ImGuiInputFlags.
/// </summary>
[Flags]
public enum InputFlags
{
    /// <summary>No flags.</summary>
    None = 0,

    /// <summary>Enable repeat. Return true on successive repeats.</summary>
    Repeat = 1 << 0,

    // Routing policies for Shortcut(), SetNextItemShortcut() — can select only one.

    /// <summary>Route to active item only.</summary>
    RouteActive = 1 << 10,

    /// <summary>Route to windows in the focus stack (default).</summary>
    RouteFocused = 1 << 11,

    /// <summary>Global route (unless a focused window or active item registered the route).</summary>
    RouteGlobal = 1 << 12,

    /// <summary>Do not register route, poll keys directly.</summary>
    RouteAlways = 1 << 13,

    // Routing options

    /// <summary>Global route: higher priority than focused route.</summary>
    RouteOverFocused = 1 << 14,

    /// <summary>Global route: higher priority than active item.</summary>
    RouteOverActive = 1 << 15,

    /// <summary>Global route: not applied if underlying background/void is focused.</summary>
    RouteUnlessBgFocused = 1 << 16,

    /// <summary>Route evaluated from the point of view of the root window rather than current window.</summary>
    RouteFromRootWindow = 1 << 17,

    /// <summary>Automatically display a tooltip when hovering item. [BETA]</summary>
    Tooltip = 1 << 18,
}
