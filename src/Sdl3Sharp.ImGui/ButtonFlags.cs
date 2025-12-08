using Sdl3Sharp.ImGui.Native;

namespace Sdl3Sharp.ImGui;

/// <summary>
/// Flags for InvisibleButton().
/// </summary>
[Flags]
public enum ButtonFlags
{
    /// <summary>
    /// No flags set.
    /// </summary>
    None = ImGuiButtonFlags.None,

    /// <summary>
    /// React on left mouse button (default).
    /// </summary>
    MouseButtonLeft = ImGuiButtonFlags.MouseButtonLeft,

    /// <summary>
    /// React on right mouse button.
    /// </summary>
    MouseButtonRight = ImGuiButtonFlags.MouseButtonRight,

    /// <summary>
    /// React on center mouse button.
    /// </summary>
    MouseButtonMiddle = ImGuiButtonFlags.MouseButtonMiddle,

    /// <summary>
    /// InvisibleButton(): do not disable navigation/tabbing. Otherwise disabled by default.
    /// </summary>
    EnableNav = ImGuiButtonFlags.EnableNav
}
