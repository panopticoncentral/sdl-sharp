using Sdl3Sharp.ImGui.Native;

namespace Sdl3Sharp.ImGui;

/// <summary>
/// Flags for ListClipper (currently not fully exposed in function calls: a future refactor will likely add this to ListClipper.Begin function equivalent).
/// </summary>
[Flags]
public enum ListClipperFlags
{
    /// <summary>
    /// No flags set.
    /// </summary>
    None = ImGuiListClipperFlags.None
}
