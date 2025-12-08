using Sdl3Sharp.ImGui.Native;

namespace Sdl3Sharp.ImGui;

/// <summary>
/// Flags for IsWindowFocused().
/// </summary>
[Flags]
public enum FocusedFlags
{
    /// <summary>
    /// No flags set.
    /// </summary>
    None = ImGuiFocusedFlags.None,

    /// <summary>
    /// Return true if any children of the window is focused.
    /// </summary>
    ChildWindows = ImGuiFocusedFlags.ChildWindows,

    /// <summary>
    /// Test from root window (top most parent of the current hierarchy).
    /// </summary>
    RootWindow = ImGuiFocusedFlags.RootWindow,

    /// <summary>
    /// Return true if any window is focused. Important: If you are trying to tell how to dispatch your low-level inputs, do NOT use this. Use 'io.WantCaptureMouse' instead! Please read the FAQ!
    /// </summary>
    AnyWindow = ImGuiFocusedFlags.AnyWindow,

    /// <summary>
    /// Do not consider popup hierarchy (do not treat popup emitter as parent of popup) (when used with ChildWindows or RootWindow).
    /// </summary>
    NoPopupHierarchy = ImGuiFocusedFlags.NoPopupHierarchy,

    /// <summary>
    /// Combination of RootWindow | ChildWindows.
    /// </summary>
    RootAndChildWindows = ImGuiFocusedFlags.RootAndChildWindows
}
