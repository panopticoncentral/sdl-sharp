using Sdl3Sharp.ImGui.Native;

namespace Sdl3Sharp.ImGui;

/// <summary>
/// Flags for Shortcut(), SetNextItemShortcut().
/// </summary>
/// <remarks>
/// Don't mistake with <see cref="InputTextFlags"/>! (which is for InputText() function)
/// </remarks>
[Flags]
public enum InputFlags
{
    /// <summary>
    /// No flags set.
    /// </summary>
    None = ImGuiInputFlags.None,

    /// <summary>
    /// Enable repeat. Return true on successive repeats. Default for legacy IsKeyPressed(). NOT Default for legacy IsMouseClicked(). MUST BE == 1.
    /// </summary>
    Repeat = ImGuiInputFlags.Repeat,

    /// <summary>
    /// Route to active item only.
    /// </summary>
    RouteActive = ImGuiInputFlags.RouteActive,

    /// <summary>
    /// Route to windows in the focus stack (DEFAULT). Deep-most focused window takes inputs. Active item takes inputs over deep-most focused window.
    /// </summary>
    RouteFocused = ImGuiInputFlags.RouteFocused,

    /// <summary>
    /// Global route (unless a focused window or active item registered the route).
    /// </summary>
    RouteGlobal = ImGuiInputFlags.RouteGlobal,

    /// <summary>
    /// Do not register route, poll keys directly.
    /// </summary>
    RouteAlways = ImGuiInputFlags.RouteAlways,

    /// <summary>
    /// Option: global route: higher priority than focused route (unless active item in focused route).
    /// </summary>
    RouteOverFocused = ImGuiInputFlags.RouteOverFocused,

    /// <summary>
    /// Option: global route: higher priority than active item. Unlikely you need to use that: will interfere with every active items.
    /// </summary>
    RouteOverActive = ImGuiInputFlags.RouteOverActive,

    /// <summary>
    /// Option: global route: will not be applied if underlying background/void is focused (== no Dear ImGui windows are focused). Useful for overlay applications.
    /// </summary>
    RouteUnlessBgFocused = ImGuiInputFlags.RouteUnlessBgFocused,

    /// <summary>
    /// Option: route evaluated from the point of view of root window rather than current window.
    /// </summary>
    RouteFromRootWindow = ImGuiInputFlags.RouteFromRootWindow,

    /// <summary>
    /// Automatically display a tooltip when hovering item [BETA] Unsure of right api (opt-in/opt-out).
    /// </summary>
    Tooltip = ImGuiInputFlags.Tooltip
}
