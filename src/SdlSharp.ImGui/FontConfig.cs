using System.Runtime.InteropServices;
using static SdlSharp.Native.Common;
using static SdlSharp.ImGui.Native;

namespace SdlSharp.ImGui;

/// <summary>
/// Configuration for adding a font to a <see cref="FontAtlas"/>. Create one, set the desired
/// properties, then pass it to the <c>FontAtlas.AddFont*</c> methods.
/// <para>
/// The atlas copies the configuration when a font is added, so the same <see cref="FontConfig"/>
/// can be reused (and re-tweaked) across multiple <c>AddFont*</c> calls. However, glyph range data
/// set via <see cref="GlyphRanges"/> / <see cref="GlyphExcludeRanges"/> is referenced by pointer
/// for the lifetime of the font — when those are set, keep this object undisposed while fonts
/// created from it are still in the atlas.
/// </para>
/// </summary>
public sealed unsafe class FontConfig : IDisposable
{
    private IGSharp_FontConfig* _handle;
    private ushort* _glyphRanges;        // unmanaged copy owned by this object
    private ushort* _glyphExcludeRanges; // unmanaged copy owned by this object

    /// <summary>Creates a new font configuration with ImGui's defaults.</summary>
    public FontConfig() => _handle = IGSharp_FontConfig_Create();

    internal IGSharp_FontConfig* Handle
    {
        get
        {
            ThrowIfDisposed();
            return _handle;
        }
    }

    /// <summary>Display name for the font, shown in debug tools (defaults to the filename when loading from disk).</summary>
    public string Name
    {
        get { ThrowIfDisposed(); return Marshal.PtrToStringUTF8((nint)IGSharp_FontConfig_GetName(_handle)) ?? string.Empty; }
        set { ThrowIfDisposed(); IGSharp_FontConfig_SetName(_handle, ToUtf8(value ?? string.Empty)); }
    }

    /// <summary>
    /// When true, the next added font is merged into the previously added font instead of creating
    /// a new one (useful for adding icon glyphs to a text font). Default false.
    /// </summary>
    public bool MergeMode
    {
        get { ThrowIfDisposed(); return IGSharp_FontConfig_GetMergeMode(_handle); }
        set { ThrowIfDisposed(); IGSharp_FontConfig_SetMergeMode(_handle, value); }
    }

    /// <summary>Align every glyph AdvanceX to pixel boundaries (useful with some bitmap fonts). Default false.</summary>
    public bool PixelSnapH
    {
        get { ThrowIfDisposed(); return IGSharp_FontConfig_GetPixelSnapH(_handle); }
        set { ThrowIfDisposed(); IGSharp_FontConfig_SetPixelSnapH(_handle, value); }
    }

    /// <summary>Horizontal rasterizer oversampling. 0 = auto. Default 0.</summary>
    public int OversampleH
    {
        get { ThrowIfDisposed(); return IGSharp_FontConfig_GetOversampleH(_handle); }
        set { ThrowIfDisposed(); IGSharp_FontConfig_SetOversampleH(_handle, value); }
    }

    /// <summary>Vertical rasterizer oversampling. 0 = auto. Default 0.</summary>
    public int OversampleV
    {
        get { ThrowIfDisposed(); return IGSharp_FontConfig_GetOversampleV(_handle); }
        set { ThrowIfDisposed(); IGSharp_FontConfig_SetOversampleV(_handle, value); }
    }

    /// <summary>Character used for the "..." ellipsis when text is clipped. (char)0xFFFF = auto. </summary>
    public char EllipsisChar
    {
        get { ThrowIfDisposed(); return (char)IGSharp_FontConfig_GetEllipsisChar(_handle); }
        set { ThrowIfDisposed(); IGSharp_FontConfig_SetEllipsisChar(_handle, value); }
    }

    /// <summary>Base font size in pixels. Optional: fonts bake at any size on demand; this sets the reference size.</summary>
    public float SizePixels
    {
        get { ThrowIfDisposed(); return IGSharp_FontConfig_GetSizePixels(_handle); }
        set { ThrowIfDisposed(); IGSharp_FontConfig_SetSizePixels(_handle, value); }
    }

    /// <summary>
    /// Restricts the glyphs loaded from this font to the given ranges: pairs of inclusive
    /// (first, last) codepoints (e.g. <c>[0x0020, 0x00FF]</c> for Basic Latin + Latin Supplement).
    /// The setter copies the data into unmanaged memory owned by this <see cref="FontConfig"/>;
    /// because the atlas references that memory for the font's lifetime, do not dispose this object
    /// (or reassign this property) while fonts loaded with these ranges are still in use.
    /// Set an empty span to clear (load all available glyphs). Default empty.
    /// </summary>
    public ReadOnlySpan<ushort> GlyphRanges
    {
        get { ThrowIfDisposed(); return RangesFromPointer(IGSharp_FontConfig_GetGlyphRanges(_handle)); }
        set
        {
            ThrowIfDisposed();
            var copy = CopyRanges(value);
            if (_glyphRanges != null)
                IGSharp_MemFree(_glyphRanges);
            _glyphRanges = copy;
            IGSharp_FontConfig_SetGlyphRanges(_handle, copy);
        }
    }

    /// <summary>
    /// Ranges of codepoints to exclude from this font source — pairs of inclusive (first, last)
    /// codepoints. Useful in <see cref="MergeMode"/> when merged fonts have overlapping glyphs.
    /// Same lifetime rules as <see cref="GlyphRanges"/>. Default empty.
    /// </summary>
    public ReadOnlySpan<ushort> GlyphExcludeRanges
    {
        get { ThrowIfDisposed(); return RangesFromPointer(IGSharp_FontConfig_GetGlyphExcludeRanges(_handle)); }
        set
        {
            ThrowIfDisposed();
            var copy = CopyRanges(value);
            if (_glyphExcludeRanges != null)
                IGSharp_MemFree(_glyphExcludeRanges);
            _glyphExcludeRanges = copy;
            IGSharp_FontConfig_SetGlyphExcludeRanges(_handle, copy);
        }
    }

    /// <summary>Offset applied to every glyph of this font, in pixels. Default (0, 0).</summary>
    public Vec2 GlyphOffset
    {
        get { ThrowIfDisposed(); var v = IGSharp_FontConfig_GetGlyphOffset(_handle); return new Vec2(v.X, v.Y); }
        set { ThrowIfDisposed(); IGSharp_FontConfig_SetGlyphOffset(_handle, new IGSharp_Vec2(value.X, value.Y)); }
    }

    /// <summary>Minimum AdvanceX for glyphs — use to make a font appear monospaced. Default 0.</summary>
    public float GlyphMinAdvanceX
    {
        get { ThrowIfDisposed(); return IGSharp_FontConfig_GetGlyphMinAdvanceX(_handle); }
        set { ThrowIfDisposed(); IGSharp_FontConfig_SetGlyphMinAdvanceX(_handle, value); }
    }

    /// <summary>Maximum AdvanceX for glyphs. Default float.MaxValue.</summary>
    public float GlyphMaxAdvanceX
    {
        get { ThrowIfDisposed(); return IGSharp_FontConfig_GetGlyphMaxAdvanceX(_handle); }
        set { ThrowIfDisposed(); IGSharp_FontConfig_SetGlyphMaxAdvanceX(_handle, value); }
    }

    /// <summary>Extra spacing added to every glyph's AdvanceX, in pixels. Default 0.</summary>
    public float GlyphExtraAdvanceX
    {
        get { ThrowIfDisposed(); return IGSharp_FontConfig_GetGlyphExtraAdvanceX(_handle); }
        set { ThrowIfDisposed(); IGSharp_FontConfig_SetGlyphExtraAdvanceX(_handle, value); }
    }

    /// <summary>Index of the font face to load when the TTF/OTF file contains multiple fonts. Default 0.</summary>
    public uint FontNo
    {
        get { ThrowIfDisposed(); return IGSharp_FontConfig_GetFontNo(_handle); }
        set { ThrowIfDisposed(); IGSharp_FontConfig_SetFontNo(_handle, value); }
    }

    /// <summary>Font-loader-specific flags (e.g. FreeType rasterizer flags when the FreeType loader is active). Default 0.</summary>
    public uint FontLoaderFlags
    {
        get { ThrowIfDisposed(); return IGSharp_FontConfig_GetFontLoaderFlags(_handle); }
        set { ThrowIfDisposed(); IGSharp_FontConfig_SetFontLoaderFlags(_handle, value); }
    }

    /// <summary>Brightness multiplier applied to rasterized coverage values (&gt;1 makes glyphs bolder/brighter). Default 1.</summary>
    public float RasterizerMultiply
    {
        get { ThrowIfDisposed(); return IGSharp_FontConfig_GetRasterizerMultiply(_handle); }
        set { ThrowIfDisposed(); IGSharp_FontConfig_SetRasterizerMultiply(_handle, value); }
    }

    /// <summary>DPI density used when rasterizing glyphs (e.g. 2 for retina displays). Default 1.</summary>
    public float RasterizerDensity
    {
        get { ThrowIfDisposed(); return IGSharp_FontConfig_GetRasterizerDensity(_handle); }
        set { ThrowIfDisposed(); IGSharp_FontConfig_SetRasterizerDensity(_handle, value); }
    }

    /// <summary>Extra scale applied to the requested size when baking this source. Default 1.</summary>
    public float ExtraSizeScale
    {
        get { ThrowIfDisposed(); return IGSharp_FontConfig_GetExtraSizeScale(_handle); }
        set { ThrowIfDisposed(); IGSharp_FontConfig_SetExtraSizeScale(_handle, value); }
    }

    /// <summary>
    /// Advanced: whether the atlas owns (and frees) the raw font data buffer. The
    /// <c>FontAtlas.AddFont*</c> wrappers manage this automatically; only change it if you are
    /// doing your own unmanaged font-data plumbing.
    /// </summary>
    public bool FontDataOwnedByAtlas
    {
        get { ThrowIfDisposed(); return IGSharp_FontConfig_GetFontDataOwnedByAtlas(_handle); }
        set { ThrowIfDisposed(); IGSharp_FontConfig_SetFontDataOwnedByAtlas(_handle, value); }
    }

    /// <summary>
    /// Releases the unmanaged configuration and any glyph range copies. Do not dispose while fonts
    /// loaded with <see cref="GlyphRanges"/>/<see cref="GlyphExcludeRanges"/> set are still in use.
    /// </summary>
    public void Dispose()
    {
        if (_handle != null)
        {
            IGSharp_FontConfig_Destroy(_handle);
            _handle = null;
        }
        if (_glyphRanges != null)
        {
            IGSharp_MemFree(_glyphRanges);
            _glyphRanges = null;
        }
        if (_glyphExcludeRanges != null)
        {
            IGSharp_MemFree(_glyphExcludeRanges);
            _glyphExcludeRanges = null;
        }
    }

    private void ThrowIfDisposed()
    {
        if (_handle == null) throw new ObjectDisposedException(nameof(FontConfig));
    }

    /// <summary>Wraps a zero-terminated list of (first, last) codepoint pairs as a span (terminator excluded).</summary>
    internal static ReadOnlySpan<ushort> RangesFromPointer(ushort* ranges)
    {
        if (ranges == null) return default;
        var length = 0;
        while (ranges[length] != 0) length += 2;
        return new ReadOnlySpan<ushort>(ranges, length);
    }

    /// <summary>Copies range pairs into a zero-terminated ImGui-allocated buffer (null for empty input).</summary>
    private static ushort* CopyRanges(ReadOnlySpan<ushort> value)
    {
        if (value.IsEmpty) return null;
        if (value.Length % 2 != 0)
            throw new ArgumentException("Glyph ranges must be pairs of (first, last) codepoints.", nameof(value));
        var copy = (ushort*)IGSharp_MemAlloc((nuint)((value.Length + 1) * sizeof(ushort)));
        value.CopyTo(new Span<ushort>(copy, value.Length));
        copy[value.Length] = 0;
        return copy;
    }
}
