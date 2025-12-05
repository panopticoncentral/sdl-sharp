using System.Runtime.InteropServices;
using Sdl3Sharp.ImGui.Native;

namespace Sdl3Sharp.ImGui;

/// <summary>
/// Represents font runtime data for a specific size.
/// </summary>
/// <remarks>
/// Pointers to FontBaked are only valid for the current frame.
/// </remarks>
[StructLayout(LayoutKind.Sequential)]
public unsafe sealed class FontBaked
{
    private readonly ImFontBaked* _native;

    /// <summary>
    /// Clears the output data for this baked font.
    /// </summary>
    public void ClearOutputData()
    {
        ImFontBaked.ClearOutputData(_native);
    }

    /// <summary>
    /// Finds a glyph for the specified character, returning the fallback glyph (U+FFFD) if not found.
    /// </summary>
    /// <param name="c">The Unicode codepoint to find.</param>
    /// <returns>The glyph data, or the fallback glyph if not found.</returns>
    public FontGlyph FindGlyph(char c)
    {
        return new(ImFontBaked.FindGlyph(_native, c));
    }

    /// <summary>
    /// Finds a glyph for the specified character, returning null if not found.
    /// </summary>
    /// <param name="c">The Unicode codepoint to find.</param>
    /// <returns>The glyph data, or null if not found.</returns>
    public FontGlyph FindGlyphNoFallback(char c)
    {
        return new(ImFontBaked.FindGlyphNoFallback(_native, c));
    }

    /// <summary>
    /// Gets the advance width for a character.
    /// </summary>
    /// <param name="c">The Unicode codepoint.</param>
    /// <returns>The horizontal advance distance for the character.</returns>
    public float GetCharAdvance(ushort c)
    {
        return ImFontBaked.GetCharAdvance(_native, c);
    }

    /// <summary>
    /// Checks whether a glyph has been loaded for the specified character.
    /// </summary>
    /// <param name="c">The Unicode codepoint.</param>
    /// <returns>True if the glyph is loaded; otherwise, false.</returns>
    public bool IsGlyphLoaded(ushort c)
    {
        return ImFontBaked.IsGlyphLoaded(_native, c);
    }

    internal FontBaked(ImFontBaked* native)
    {
        _native = native;
    }
}
