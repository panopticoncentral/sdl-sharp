using static Sdl3Sharp.ImGui.Native.ImGui;

namespace Sdl3Sharp.ImGui;

/// <summary>
/// A scope that automatically pops the font when disposed.
/// </summary>
public readonly unsafe ref struct FontScope : IDisposable
{
    /// <summary>
    /// Pushes a font and/or font size onto the stack.
    /// </summary>
    /// <param name="font">The font to use, or null to keep the current font.</param>
    /// <param name="fontSizeBaseUnscaled">The base font size before global scaling, or 0.0f to keep the current size.</param>
    public static FontScope Push(Font? font, float fontSizeBaseUnscaled = 0.0f)
    {
        ImGui_PushFontFloat(font == null ? null : font.Value.Native, fontSizeBaseUnscaled);
        return new();
    }

    /// <inheritdoc/>
    public void Dispose()
    {
        ImGui_PopFont();
    }
}
