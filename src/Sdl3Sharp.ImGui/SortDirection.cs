using Sdl3Sharp.ImGui.Native;

namespace Sdl3Sharp.ImGui;

/// <summary>
/// A sorting direction.
/// </summary>
public enum SortDirection : byte
{
    /// <summary>
    /// No sorting direction.
    /// </summary>
    None = ImGuiSortDirection.None,

    /// <summary>
    /// Ascending order (0->9, A->Z etc.).
    /// </summary>
    Ascending = ImGuiSortDirection.Ascending,

    /// <summary>
    /// Descending order (9->0, Z->A etc.).
    /// </summary>
    Descending = ImGuiSortDirection.Descending
}
