using static Sdl3Sharp.ImGui.Native.ImGui;

namespace Sdl3Sharp.ImGui;

/// <summary>
/// A scope that pushes a text wrap position onto the stack and pops it when disposed.
/// </summary>
public readonly ref struct TextWrapPositionScope : IDisposable
{
    /// <summary>
    /// Pushes a text wrap position modification onto the stack.
    /// </summary>
    /// <param name="wrapLocalPositionX">
    /// The wrap position in window-local coordinates. Use 0.0f to wrap to end of window,
    /// or a positive value to wrap at a specific X coordinate.
    /// </param>
    public static TextWrapPositionScope Push(float wrapLocalPositionX = 0.0f)
    {
        ImGui_PushTextWrapPos(wrapLocalPositionX);
        return new();
    }

    /// <inheritdoc/>
    public void Dispose()
    {
        ImGui_PopTextWrapPos();
    }
}
