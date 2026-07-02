using static SdlSharp.ImGui.Native;

namespace SdlSharp.ImGui;

/// <summary>
/// A read-only view of a single baked glyph's metrics and texture coordinates. Obtain via
/// <see cref="FontBaked.FindGlyph"/> / <see cref="FontBaked.GetGlyph"/>.
/// <para>
/// The underlying pointer is owned by the font atlas and is transient: it may be invalidated
/// whenever the atlas bakes new glyphs, repacks, or resizes its texture (which can happen any
/// frame). Read the values you need immediately; do not cache a <see cref="FontGlyph"/> across
/// frames.
/// </para>
/// </summary>
public readonly unsafe struct FontGlyph
{
    internal readonly IGSharp_FontGlyph* Handle;

    internal FontGlyph(IGSharp_FontGlyph* handle) => Handle = handle;

    /// <summary>True if this handle refers to a valid glyph (e.g. <see cref="FontBaked.FindGlyphNoFallback"/> found one).</summary>
    public bool IsValid => Handle != null;

    /// <summary>The Unicode codepoint this glyph renders.</summary>
    public uint Codepoint => IGSharp_FontGlyph_GetCodepoint(Handle);

    /// <summary>True if the glyph is a multi-colored bitmap (e.g. color emoji) — render it without tinting.</summary>
    public bool Colored => IGSharp_FontGlyph_GetColored(Handle);

    /// <summary>True if the glyph has visible pixels (false for spaces and other blank glyphs).</summary>
    public bool Visible => IGSharp_FontGlyph_GetVisible(Handle);

    /// <summary>Index of the font source (config) this glyph was loaded from, for merged fonts.</summary>
    public int SourceIndex => IGSharp_FontGlyph_GetSourceIdx(Handle);

    /// <summary>Horizontal distance to advance the pen after drawing this glyph, in pixels.</summary>
    public float AdvanceX => IGSharp_FontGlyph_GetAdvanceX(Handle);

    /// <summary>Top-left corner of the glyph quad, in pixels relative to the pen position.</summary>
    public Vec2 Min => new(IGSharp_FontGlyph_GetX0(Handle), IGSharp_FontGlyph_GetY0(Handle));

    /// <summary>Bottom-right corner of the glyph quad, in pixels relative to the pen position.</summary>
    public Vec2 Max => new(IGSharp_FontGlyph_GetX1(Handle), IGSharp_FontGlyph_GetY1(Handle));

    /// <summary>Top-left UV coordinate in the atlas texture.</summary>
    public Vec2 Uv0 => new(IGSharp_FontGlyph_GetU0(Handle), IGSharp_FontGlyph_GetV0(Handle));

    /// <summary>Bottom-right UV coordinate in the atlas texture.</summary>
    public Vec2 Uv1 => new(IGSharp_FontGlyph_GetU1(Handle), IGSharp_FontGlyph_GetV1(Handle));

    /// <summary>
    /// Atlas rect id for this glyph's packed rectangle (-1 if none). Pass to
    /// <see cref="FontAtlas.TryGetCustomRect"/> to refresh coordinates after the atlas repacks.
    /// </summary>
    public int PackId => IGSharp_FontGlyph_GetPackId(Handle);
}
