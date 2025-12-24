using static Sdl3Sharp.ImGui.Native.ImGui;

namespace Sdl3Sharp.ImGui;

/// <summary>
/// Locks the horizontal starting position for a group of items.
/// </summary>
public readonly ref struct GroupScope : IDisposable
{
    /// <summary>
    /// Creates a new group scope, beginning the group.
    /// </summary>
    public GroupScope()
    {
        ImGui_BeginGroup();
    }

    /// <inheritdoc/>
    public void Dispose()
    {
        ImGui_EndGroup();
    }
}
