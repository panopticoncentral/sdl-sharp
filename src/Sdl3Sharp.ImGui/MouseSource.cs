using Sdl3Sharp.ImGui.Native;

namespace Sdl3Sharp.ImGui;

/// <summary>
/// Enumeration for the actual source of mouse input data.
/// </summary>
/// <remarks>
/// <para>
/// Historically, "Mouse" terminology is used everywhere to indicate pointer data,
/// e.g., MousePos, IsMousePressed(), io.AddMousePosEvent().
/// But that "Mouse" data can come from different sources which may occasionally be useful
/// for applications to know about.
/// </para>
/// <para>
/// You can submit a change of pointer type using <see cref="IO.AddMouseSourceEvent"/>.
/// </para>
/// </remarks>
public enum MouseSource
{
    /// <summary>
    /// Input is coming from an actual mouse.
    /// </summary>
    Mouse = ImGuiMouseSource.Mouse,

    /// <summary>
    /// Input is coming from a touch screen (no hovering prior to initial press,
    /// less precise initial press aiming, dual-axis wheeling possible).
    /// </summary>
    TouchScreen = ImGuiMouseSource.TouchScreen,

    /// <summary>
    /// Input is coming from a pressure/magnetic pen (often used in conjunction with high-sampling rates).
    /// </summary>
    Pen = ImGuiMouseSource.Pen
}
