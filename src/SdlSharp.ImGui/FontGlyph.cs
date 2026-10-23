using static SdlSharp.ImGui.Native;

namespace SdlSharp.ImGui;

/// <summary>
/// A value snapshot of baked glyph metrics and UVs. Vector growth and font removal do not
/// invalidate this copy. UVs describe the atlas at capture time and do not track repacking.
/// </summary>
public readonly unsafe struct FontGlyph
{

    internal FontGlyph(IGSharp_FontGlyph* handle)
    {
        this = default;
        if (handle == null) return;
        IsValid = true;
        Codepoint = IGSharp_FontGlyph_GetCodepoint(handle);
        Colored = IGSharp_FontGlyph_GetColored(handle);
        Visible = IGSharp_FontGlyph_GetVisible(handle);
        SourceIndex = IGSharp_FontGlyph_GetSourceIdx(handle);
        AdvanceX = IGSharp_FontGlyph_GetAdvanceX(handle);
        Min = new(IGSharp_FontGlyph_GetX0(handle), IGSharp_FontGlyph_GetY0(handle));
        Max = new(IGSharp_FontGlyph_GetX1(handle), IGSharp_FontGlyph_GetY1(handle));
        Uv0 = new(IGSharp_FontGlyph_GetU0(handle), IGSharp_FontGlyph_GetV0(handle));
        Uv1 = new(IGSharp_FontGlyph_GetU1(handle), IGSharp_FontGlyph_GetV1(handle));
        PackId = IGSharp_FontGlyph_GetPackId(handle);
    }

    /// <summary>True if this handle refers to a valid glyph (e.g. <see cref="FontBaked.FindGlyphNoFallback"/> found one).</summary>
    public bool IsValid { get; }

    /// <summary>The Unicode codepoint this glyph renders.</summary>
    public uint Codepoint { get; }

    /// <summary>True if the glyph is a multi-colored bitmap (e.g. color emoji) — render it without tinting.</summary>
    public bool Colored { get; }

    /// <summary>True if the glyph has visible pixels (false for spaces and other blank glyphs).</summary>
    public bool Visible { get; }

    /// <summary>Index of the font source (config) this glyph was loaded from, for merged fonts.</summary>
    public int SourceIndex { get; }

    /// <summary>Horizontal distance to advance the pen after drawing this glyph, in pixels.</summary>
    public float AdvanceX { get; }

    /// <summary>Top-left corner of the glyph quad, in pixels relative to the pen position.</summary>
    public Vec2 Min { get; }

    /// <summary>Bottom-right corner of the glyph quad, in pixels relative to the pen position.</summary>
    public Vec2 Max { get; }

    /// <summary>Top-left UV coordinate in the atlas texture.</summary>
    public Vec2 Uv0 { get; }

    /// <summary>Bottom-right UV coordinate in the atlas texture.</summary>
    public Vec2 Uv1 { get; }

    /// <summary>
    /// Atlas rect id for this glyph's packed rectangle (-1 if none). Pass to
    /// <see cref="FontAtlas.TryGetCustomRect"/> to refresh coordinates after the atlas repacks.
    /// </summary>
    public int PackId { get; }
}
