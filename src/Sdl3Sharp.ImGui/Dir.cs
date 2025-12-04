using Sdl3Sharp.ImGui.Native;

namespace Sdl3Sharp.ImGui;

/// <summary>
/// A cardinal direction.
/// </summary>
public enum Dir
{
    /// <summary>
    /// No direction.
    /// </summary>
    None = ImGuiDir.None,

    /// <summary>
    /// Left direction.
    /// </summary>
    Left = ImGuiDir.Left,

    /// <summary>
    /// Right direction.
    /// </summary>
    Right = ImGuiDir.Right,

    /// <summary>
    /// Up direction.
    /// </summary>
    Up = ImGuiDir.Up,

    /// <summary>
    /// Down direction.
    /// </summary>
    Down = ImGuiDir.Down
}
