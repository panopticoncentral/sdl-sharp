using static SdlSharp.ImGui.Native;

namespace SdlSharp.ImGui;

/// <summary>
/// A read-only view of a font baked at a specific size and density. Obtain via
/// <see cref="Font.GetFontBaked"/>.
/// <para>
/// The underlying pointer is owned by the font atlas and is transient: baked data can be
/// garbage-collected or rebuilt between frames (e.g. by <see cref="FontAtlas.CompactCache"/> or
/// atlas texture changes). Query it again each frame rather than caching a
/// <see cref="FontBaked"/> instance.
/// </para>
/// </summary>
public readonly unsafe struct FontBaked
{
    internal readonly IGSharp_FontBaked* Handle;

    internal FontBaked(IGSharp_FontBaked* handle) => Handle = handle;

    /// <summary>True if this handle refers to valid baked font data.</summary>
    public bool IsValid => Handle != null;

    /// <summary>The size in pixels this instance was baked at.</summary>
    public float Size => IGSharp_FontBaked_GetSize(Handle);

    /// <summary>Distance from the baseline to the top of the glyphs, in pixels (positive).</summary>
    public float Ascent => IGSharp_FontBaked_GetAscent(Handle);

    /// <summary>Distance from the baseline to the bottom of the glyphs, in pixels (negative).</summary>
    public float Descent => IGSharp_FontBaked_GetDescent(Handle);

    /// <summary>AdvanceX of the fallback glyph, in pixels.</summary>
    public float FallbackAdvanceX => IGSharp_FontBaked_GetFallbackAdvanceX(Handle);

    /// <summary>The rasterizer density this instance was baked at.</summary>
    public float RasterizerDensity => IGSharp_FontBaked_GetRasterizerDensity(Handle);

    /// <summary>Number of glyphs baked so far (glyphs bake lazily, so this can grow between frames).</summary>
    public int GlyphCount => IGSharp_FontBaked_GetGlyphsCount(Handle);

    /// <summary>Gets a baked glyph by index (0 to <see cref="GlyphCount"/> - 1).</summary>
    public FontGlyph GetGlyph(int index) => new(IGSharp_FontBaked_GetGlyph(Handle, index));

    /// <summary>
    /// Finds the glyph for <paramref name="c"/>, baking it on demand. Returns the fallback glyph
    /// (U+FFFD) if the character is not present in the font.
    /// </summary>
    public FontGlyph FindGlyph(char c) => new(IGSharp_FontBaked_FindGlyph(Handle, c));

    /// <summary>
    /// Finds the glyph for <paramref name="c"/> without falling back. Check
    /// <see cref="FontGlyph.IsValid"/> on the result — it is false if the glyph doesn't exist.
    /// </summary>
    public FontGlyph FindGlyphNoFallback(char c) => new(IGSharp_FontBaked_FindGlyphNoFallback(Handle, c));

    /// <summary>Horizontal advance for <paramref name="c"/>, in pixels (fallback advance if not present).</summary>
    public float GetCharAdvance(char c) => IGSharp_FontBaked_GetCharAdvance(Handle, c);

    /// <summary>True if the glyph for <paramref name="c"/> has already been baked (does not trigger baking).</summary>
    public bool IsGlyphLoaded(char c) => IGSharp_FontBaked_IsGlyphLoaded(Handle, c);
}
