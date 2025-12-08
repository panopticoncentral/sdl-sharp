using Sdl3Sharp.ImGui.Native;

namespace Sdl3Sharp.ImGui;

/// <summary>
/// Identify a mouse button.
/// </summary>
/// <remarks>
/// Those values are guaranteed to be stable and we frequently use 0/1 directly. Named enums provided for convenience.
/// </remarks>
public enum MouseButton
{
    /// <summary>
    /// Left mouse button.
    /// </summary>
    Left = ImGuiMouseButton.Left,

    /// <summary>
    /// Right mouse button.
    /// </summary>
    Right = ImGuiMouseButton.Right,

    /// <summary>
    /// Middle mouse button (scroll wheel button).
    /// </summary>
    Middle = ImGuiMouseButton.Middle
}
