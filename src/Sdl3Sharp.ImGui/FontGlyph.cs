using System.Runtime.InteropServices;
using Sdl3Sharp.ImGui.Native;

namespace Sdl3Sharp.ImGui;

/// <summary>
/// Represents rendering data for a single glyph in a font.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public unsafe readonly struct FontGlyph
{
    private readonly ImFontGlyph* _native;

    /// <summary>
    /// Gets whether the glyph is colored and should generally ignore tinting.
    /// </summary>
    public readonly bool Colored => _native->Colored;

    /// <summary>
    /// Gets whether the glyph has visible pixels. False for glyphs like space.
    /// </summary>
    public readonly bool Visible => _native->Visible;

    /// <summary>
    /// Gets the index of the source in the parent font.
    /// </summary>
    public readonly uint SourceIndex => _native->SourceIdx;

    /// <summary>
    /// Gets the Unicode codepoint (0x0000..0x10FFFF).
    /// </summary>
    public readonly uint Codepoint => _native->Codepoint;

    /// <summary>
    /// Gets the horizontal distance to advance the cursor/layout position.
    /// </summary>
    public readonly float AdvanceX => _native->AdvanceX;

    /// <summary>
    /// Gets the left edge of the glyph (offset from cursor position).
    /// </summary>
    public readonly float X0 => _native->X0;

    /// <summary>
    /// Gets the top edge of the glyph (offset from cursor position).
    /// </summary>
    public readonly float Y0 => _native->Y0;

    /// <summary>
    /// Gets the right edge of the glyph (offset from cursor position).
    /// </summary>
    public readonly float X1 => _native->X1;

    /// <summary>
    /// Gets the bottom edge of the glyph (offset from cursor position).
    /// </summary>
    public readonly float Y1 => _native->Y1;

    /// <summary>
    /// Gets the left texture coordinate.
    /// </summary>
    public readonly float U0 => _native->U0;

    /// <summary>
    /// Gets the top texture coordinate.
    /// </summary>
    public readonly float V0 => _native->V0;

    /// <summary>
    /// Gets the right texture coordinate.
    /// </summary>
    public readonly float U1 => _native->U1;

    /// <summary>
    /// Gets the bottom texture coordinate.
    /// </summary>
    public readonly float V1 => _native->V1;

    internal FontGlyph(ImFontGlyph* native)
    {
        _native = native;
    }
}
