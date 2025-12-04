using System.Runtime.InteropServices;
using Sdl3Sharp.ImGui.Native;

namespace Sdl3Sharp.ImGui;

/// <summary>
/// Represents font runtime data and rendering information.
/// </summary>
public readonly unsafe struct Font
{
    internal ImFont* Native { get; }

    internal Font(ImFont* native)
    {
        Native = native;
    }

    /// <summary>
    /// Checks whether a glyph exists in this font for the specified character.
    /// </summary>
    /// <param name="c">The Unicode codepoint to check.</param>
    /// <returns>True if the glyph exists; otherwise, false.</returns>
    public readonly bool IsGlyphInFont(ushort c)
    {
        return ImFont.IsGlyphInFont(Native, c);
    }

    /// <summary>
    /// Checks whether this font has been loaded.
    /// </summary>
    /// <returns>True if the font is loaded; otherwise, false.</returns>
    public readonly bool IsLoaded()
    {
        return ImFont.IsLoaded(Native);
    }

    /// <summary>
    /// Gets the debug name of this font (from FontConfig.Name).
    /// </summary>
    /// <returns>The debug name string.</returns>
    public readonly string GetDebugName()
    {
        var namePtr = ImFont.GetDebugName(Native);
        return Marshal.PtrToStringUTF8((nint)namePtr) ?? string.Empty;
    }
}
