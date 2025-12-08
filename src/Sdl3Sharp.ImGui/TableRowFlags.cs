using Sdl3Sharp.ImGui.Native;

namespace Sdl3Sharp.ImGui;

/// <summary>
/// Flags for TableNextRow().
/// </summary>
[Flags]
public enum TableRowFlags
{
    /// <summary>
    /// No flags set.
    /// </summary>
    None = ImGuiTableRowFlags.None,

    /// <summary>
    /// Identify header row (set default background color + width of its contents accounted differently for auto column width).
    /// </summary>
    Headers = ImGuiTableRowFlags.Headers
}
