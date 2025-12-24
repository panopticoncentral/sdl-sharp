using Sdl3Sharp.ImGui.Native;

namespace Sdl3Sharp.ImGui;

/// <summary>
/// Enumeration for SetNextWindow***(), SetWindow***(), SetNextItem***() functions.
/// </summary>
/// <remarks>
/// Important: Treat as a regular enum! Do NOT combine multiple values using binary operators! All the functions above treat 0 as a shortcut to Always.
/// </remarks>
public enum Condition
{
    /// <summary>
    /// No condition (always set the variable), same as Always.
    /// </summary>
    None = ImGuiCond.None,

    /// <summary>
    /// No condition (always set the variable), same as None.
    /// </summary>
    Always = ImGuiCond.Always,

    /// <summary>
    /// Set the variable once per runtime session (only the first call will succeed).
    /// </summary>
    Once = ImGuiCond.Once,

    /// <summary>
    /// Set the variable if the object/window has no persistently saved data (no entry in .ini file).
    /// </summary>
    FirstUseEver = ImGuiCond.FirstUseEver,

    /// <summary>
    /// Set the variable if the object/window is appearing after being hidden/inactive (or the first time).
    /// </summary>
    Appearing = ImGuiCond.Appearing
}
