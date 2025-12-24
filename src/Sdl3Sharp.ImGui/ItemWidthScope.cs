using static Sdl3Sharp.ImGui.Native.ImGui;

namespace Sdl3Sharp.ImGui;

/// <summary>
/// A scope that pushes an item width onto the stack and pops it when disposed.
/// </summary>
public readonly ref struct ItemWidthScope : IDisposable
{
    /// <summary>
    /// Pushes an item width modification onto the stack.
    /// </summary>
    /// <param name="itemWidth">
    /// The width to push. Use 0.0f for default width, greater than 0.0f for fixed width in pixels,
    /// or less than 0.0f for width relative to the right edge of the content region.
    /// </param>
    public static ItemWidthScope Push(float itemWidth)
    {
        ImGui_PushItemWidth(itemWidth);
        return new();
    }

    /// <inheritdoc/>
    public void Dispose()
    {
        ImGui_PopItemWidth();
    }
}
