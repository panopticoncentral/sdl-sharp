using Sdl3Sharp.ImGui.Native;

namespace Sdl3Sharp.ImGui;

/// <summary>
/// Flags stored in Viewport.Flags, giving indications to the platform backends.
/// </summary>
[Flags]
public enum ViewportFlags
{
    /// <summary>
    /// No flags set.
    /// </summary>
    None = ImGuiViewportFlags.None,

    /// <summary>
    /// Represent a Platform Window.
    /// </summary>
    IsPlatformWindow = ImGuiViewportFlags.IsPlatformWindow,

    /// <summary>
    /// Represent a Platform Monitor (unused yet).
    /// </summary>
    IsPlatformMonitor = ImGuiViewportFlags.IsPlatformMonitor,

    /// <summary>
    /// Platform Window: Is created/managed by the application (rather than a dear imgui backend).
    /// </summary>
    OwnedByApp = ImGuiViewportFlags.OwnedByApp
}
