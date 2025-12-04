using Sdl3Sharp.ImGui.Native;

namespace Sdl3Sharp.ImGui;

/// <summary>
/// Configuration flags for Dear ImGui behavior.
/// </summary>
/// <remarks>
/// Set by user/application via <see cref="IO.ConfigFlags"/>.
/// </remarks>
[Flags]
public enum ConfigFlags
{
    /// <summary>
    /// No flags set.
    /// </summary>
    None = ImGuiConfigFlags.None,

    /// <summary>
    /// Master keyboard navigation enable flag. Enable full Tabbing + directional arrows + space/enter to activate.
    /// </summary>
    NavEnableKeyboard = ImGuiConfigFlags.NavEnableKeyboard,

    /// <summary>
    /// Master gamepad navigation enable flag. Backend also needs to set <see cref="BackendFlags.HasGamepad"/>.
    /// </summary>
    NavEnableGamepad = ImGuiConfigFlags.NavEnableGamepad,

    /// <summary>
    /// Instruct Dear ImGui to disable mouse inputs and interactions.
    /// </summary>
    NoMouse = ImGuiConfigFlags.NoMouse,

    /// <summary>
    /// Instruct backend to not alter mouse cursor shape and visibility.
    /// Use if the backend cursor changes are interfering with yours and you don't want to use SetMouseCursor() to change mouse cursor.
    /// </summary>
    NoMouseCursorChange = ImGuiConfigFlags.NoMouseCursorChange,

    /// <summary>
    /// Instruct Dear ImGui to disable keyboard inputs and interactions.
    /// This is done by ignoring keyboard events and clearing existing states.
    /// </summary>
    NoKeyboard = ImGuiConfigFlags.NoKeyboard,

    /// <summary>
    /// Application is SRGB-aware.
    /// </summary>
    IsSrgb = ImGuiConfigFlags.IsSRGB,

    /// <summary>
    /// Application is using a touch screen instead of a mouse.
    /// </summary>
    IsTouchScreen = ImGuiConfigFlags.IsTouchScreen
}
