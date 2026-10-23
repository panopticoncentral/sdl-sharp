using System.Runtime.InteropServices;
using static SdlSharp.Native.Common;
using static SdlSharp.ImGui.Native;

namespace SdlSharp.ImGui;

/// <summary>
/// Configuration for adding a font to a <see cref="FontAtlas"/>. Create one, set the desired
/// properties, then pass it to the <c>FontAtlas.AddFont*</c> methods.
/// <para>
/// Each font added to an atlas receives its own copy of the glyph ranges, so the same
/// <see cref="FontConfig"/> can be disposed, reused, or changed afterward, including across contexts.
/// </para>
/// </summary>
public sealed unsafe class FontConfig : IDisposable
{
    private readonly ConfigHandle _resource = new();
    private IGSharp_FontConfig* _handle => (IGSharp_FontConfig*)_resource.DangerousGetHandle();
    private ushort* _glyphRanges { get => _resource.Ranges; set => _resource.Ranges = value; }
    private ushort* _glyphExcludeRanges { get => _resource.Excluded; set => _resource.Excluded = value; }

    /// <summary>Creates a new font configuration with ImGui's defaults.</summary>
    public FontConfig() { }

    internal IGSharp_FontConfig* Handle
    {
        get
        {
            ThrowIfDisposed(); using var nativeLease = new NativeCallLease(_resource);
            return _handle;
        }
    }

    /// <summary>Flags applied before the font is loaded.</summary>
    public FontFlags Flags
    {
        get { ThrowIfDisposed(); using var nativeLease = new NativeCallLease(_resource); return (FontFlags)IGSharp_FontConfig_GetFlags(_handle); }
        set { ThrowIfDisposed(); using var nativeLease = new NativeCallLease(_resource); IGSharp_FontConfig_SetFlags(_handle, (int)value); }
    }

    /// <summary>Display name for the font, shown in debug tools (defaults to the filename when loading from disk).</summary>
    public string Name
    {
        get { ThrowIfDisposed(); using var nativeLease = new NativeCallLease(_resource); return Marshal.PtrToStringUTF8((nint)IGSharp_FontConfig_GetName(_handle)) ?? string.Empty; }
        set { ThrowIfDisposed(); using var nativeLease = new NativeCallLease(_resource); IGSharp_FontConfig_SetName(_handle, ToUtf8(value ?? string.Empty)); }
    }

    /// <summary>
    /// When true, the next added font is merged into the previously added font instead of creating
    /// a new one (useful for adding icon glyphs to a text font). Default false.
    /// </summary>
    public bool MergeMode
    {
        get { ThrowIfDisposed(); using var nativeLease = new NativeCallLease(_resource); return IGSharp_FontConfig_GetMergeMode(_handle); }
        set { ThrowIfDisposed(); using var nativeLease = new NativeCallLease(_resource); IGSharp_FontConfig_SetMergeMode(_handle, value); }
    }

    /// <summary>Align every glyph AdvanceX to pixel boundaries (useful with some bitmap fonts). Default false.</summary>
    public bool PixelSnapH
    {
        get { ThrowIfDisposed(); using var nativeLease = new NativeCallLease(_resource); return IGSharp_FontConfig_GetPixelSnapH(_handle); }
        set { ThrowIfDisposed(); using var nativeLease = new NativeCallLease(_resource); IGSharp_FontConfig_SetPixelSnapH(_handle, value); }
    }

    /// <summary>Horizontal rasterizer oversampling. 0 = auto. Default 0.</summary>
    public int OversampleH
    {
        get { ThrowIfDisposed(); using var nativeLease = new NativeCallLease(_resource); return IGSharp_FontConfig_GetOversampleH(_handle); }
        set { ThrowIfDisposed(); using var nativeLease = new NativeCallLease(_resource); IGSharp_FontConfig_SetOversampleH(_handle, value); }
    }

    /// <summary>Vertical rasterizer oversampling. 0 = auto. Default 0.</summary>
    public int OversampleV
    {
        get { ThrowIfDisposed(); using var nativeLease = new NativeCallLease(_resource); return IGSharp_FontConfig_GetOversampleV(_handle); }
        set { ThrowIfDisposed(); using var nativeLease = new NativeCallLease(_resource); IGSharp_FontConfig_SetOversampleV(_handle, value); }
    }

    /// <summary>Character used for the "..." ellipsis when text is clipped. (char)0xFFFF = auto. </summary>
    public char EllipsisChar
    {
        get { ThrowIfDisposed(); using var nativeLease = new NativeCallLease(_resource); return (char)IGSharp_FontConfig_GetEllipsisChar(_handle); }
        set { ThrowIfDisposed(); using var nativeLease = new NativeCallLease(_resource); IGSharp_FontConfig_SetEllipsisChar(_handle, value); }
    }

    /// <summary>Base font size in pixels. Optional: fonts bake at any size on demand; this sets the reference size.</summary>
    public float SizePixels
    {
        get { ThrowIfDisposed(); using var nativeLease = new NativeCallLease(_resource); return IGSharp_FontConfig_GetSizePixels(_handle); }
        set { ThrowIfDisposed(); using var nativeLease = new NativeCallLease(_resource); IGSharp_FontConfig_SetSizePixels(_handle, value); }
    }

    /// <summary>
    /// Restricts the glyphs loaded from this font to the given ranges: pairs of inclusive
    /// (first, last) codepoints (e.g. <c>[0x0020, 0x00FF]</c> for Basic Latin + Latin Supplement).
    /// The setter copies the data. Each added font receives an independent font-owned copy.
    /// Set an empty span to clear (load all available glyphs). Default empty.
    /// </summary>
    public ReadOnlySpan<ushort> GlyphRanges
    {
        get { ThrowIfDisposed(); using var nativeLease = new NativeCallLease(_resource); return RangesFromPointer(IGSharp_FontConfig_GetGlyphRanges(_handle)).ToArray(); }
        set
        {
            ThrowIfDisposed(); using var nativeLease = new NativeCallLease(_resource);
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
    /// Each added font receives an independent font-owned copy. Default empty.
    /// </summary>
    public ReadOnlySpan<ushort> GlyphExcludeRanges
    {
        get { ThrowIfDisposed(); using var nativeLease = new NativeCallLease(_resource); return RangesFromPointer(IGSharp_FontConfig_GetGlyphExcludeRanges(_handle)).ToArray(); }
        set
        {
            ThrowIfDisposed(); using var nativeLease = new NativeCallLease(_resource);
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
        get { ThrowIfDisposed(); using var nativeLease = new NativeCallLease(_resource); var v = IGSharp_FontConfig_GetGlyphOffset(_handle); return new Vec2(v.X, v.Y); }
        set { ThrowIfDisposed(); using var nativeLease = new NativeCallLease(_resource); IGSharp_FontConfig_SetGlyphOffset(_handle, new IGSharp_Vec2(value.X, value.Y)); }
    }

    /// <summary>Minimum AdvanceX for glyphs — use to make a font appear monospaced. Default 0.</summary>
    public float GlyphMinAdvanceX
    {
        get { ThrowIfDisposed(); using var nativeLease = new NativeCallLease(_resource); return IGSharp_FontConfig_GetGlyphMinAdvanceX(_handle); }
        set { ThrowIfDisposed(); using var nativeLease = new NativeCallLease(_resource); IGSharp_FontConfig_SetGlyphMinAdvanceX(_handle, value); }
    }

    /// <summary>Maximum AdvanceX for glyphs. Default float.MaxValue.</summary>
    public float GlyphMaxAdvanceX
    {
        get { ThrowIfDisposed(); using var nativeLease = new NativeCallLease(_resource); return IGSharp_FontConfig_GetGlyphMaxAdvanceX(_handle); }
        set { ThrowIfDisposed(); using var nativeLease = new NativeCallLease(_resource); IGSharp_FontConfig_SetGlyphMaxAdvanceX(_handle, value); }
    }

    /// <summary>Extra spacing added to every glyph's AdvanceX, in pixels. Default 0.</summary>
    public float GlyphExtraAdvanceX
    {
        get { ThrowIfDisposed(); using var nativeLease = new NativeCallLease(_resource); return IGSharp_FontConfig_GetGlyphExtraAdvanceX(_handle); }
        set { ThrowIfDisposed(); using var nativeLease = new NativeCallLease(_resource); IGSharp_FontConfig_SetGlyphExtraAdvanceX(_handle, value); }
    }

    /// <summary>Index of the font face to load when the TTF/OTF file contains multiple fonts. Default 0.</summary>
    public uint FontNo
    {
        get { ThrowIfDisposed(); using var nativeLease = new NativeCallLease(_resource); return IGSharp_FontConfig_GetFontNo(_handle); }
        set { ThrowIfDisposed(); using var nativeLease = new NativeCallLease(_resource); IGSharp_FontConfig_SetFontNo(_handle, value); }
    }

    /// <summary>Font-loader-specific flags (e.g. FreeType rasterizer flags when the FreeType loader is active). Default 0.</summary>
    public uint FontLoaderFlags
    {
        get { ThrowIfDisposed(); using var nativeLease = new NativeCallLease(_resource); return IGSharp_FontConfig_GetFontLoaderFlags(_handle); }
        set { ThrowIfDisposed(); using var nativeLease = new NativeCallLease(_resource); IGSharp_FontConfig_SetFontLoaderFlags(_handle, value); }
    }

    /// <summary>Brightness multiplier applied to rasterized coverage values (&gt;1 makes glyphs bolder/brighter). Default 1.</summary>
    public float RasterizerMultiply
    {
        get { ThrowIfDisposed(); using var nativeLease = new NativeCallLease(_resource); return IGSharp_FontConfig_GetRasterizerMultiply(_handle); }
        set { ThrowIfDisposed(); using var nativeLease = new NativeCallLease(_resource); IGSharp_FontConfig_SetRasterizerMultiply(_handle, value); }
    }

    /// <summary>DPI density used when rasterizing glyphs (e.g. 2 for retina displays). Default 1.</summary>
    public float RasterizerDensity
    {
        get { ThrowIfDisposed(); using var nativeLease = new NativeCallLease(_resource); return IGSharp_FontConfig_GetRasterizerDensity(_handle); }
        set { ThrowIfDisposed(); using var nativeLease = new NativeCallLease(_resource); IGSharp_FontConfig_SetRasterizerDensity(_handle, value); }
    }

    /// <summary>Extra scale applied to the requested size when baking this source. Default 1.</summary>
    public float ExtraSizeScale
    {
        get { ThrowIfDisposed(); using var nativeLease = new NativeCallLease(_resource); return IGSharp_FontConfig_GetExtraSizeScale(_handle); }
        set { ThrowIfDisposed(); using var nativeLease = new NativeCallLease(_resource); IGSharp_FontConfig_SetExtraSizeScale(_handle, value); }
    }

    /// <summary>
    /// Releases the unmanaged configuration and its source range copies. Fonts already added to
    /// atlases retain their own copies.
    /// </summary>
    public void Dispose() => _resource.Dispose();

    private sealed class ConfigHandle : SafeHandle
    {
        internal ushort* Ranges;
        internal ushort* Excluded;
        internal ConfigHandle() : base(0, true)
        {
            SetHandle((nint)IGSharp_FontConfig_Create());
            if (IsInvalid) throw new OutOfMemoryException();
        }
        public override bool IsInvalid => handle == 0;
        protected override bool ReleaseHandle()
        {
            IGSharp_FontConfig_Destroy((IGSharp_FontConfig*)handle);
            IGSharp_MemFree(Ranges); IGSharp_MemFree(Excluded); handle = 0; return true;
        }
    }

    private void ThrowIfDisposed()
    {
        ObjectDisposedException.ThrowIf(_resource.IsClosed, this);
    }

    internal AddScope PrepareForAdd(IGSharp_FontAtlas* atlas)
    {
        ThrowIfDisposed(); using var nativeLease = new NativeCallLease(_resource);
        var glyphRanges = CopyRanges(RangesFromPointer(_glyphRanges));
        ushort* glyphExcludeRanges;
        try { glyphExcludeRanges = CopyRanges(RangesFromPointer(_glyphExcludeRanges)); }
        catch { IGSharp_MemFree(glyphRanges); throw; }
        IGSharp_FontConfig_SetGlyphRanges(_handle, glyphRanges);
        IGSharp_FontConfig_SetGlyphExcludeRanges(_handle, glyphExcludeRanges);
        return new AddScope(this, glyphRanges, glyphExcludeRanges);
    }

    internal sealed class AddScope(FontConfig config, ushort* ranges, ushort* excluded) : IDisposable
    {
        private bool _adopted;
        public IGSharp_FontConfig* Handle => config._handle;
        internal Font Finish(IGSharp_Font* font, Lifetime atlas)
        {
            if (font == null) throw new InvalidOperationException("ImGui could not load the font.");
            var lifetime = Lifetime.Capture((nint)font, atlas);
            lifetime.AddCleanup(() => { IGSharp_MemFree(ranges); IGSharp_MemFree(excluded); });
            _adopted = true;
            return new Font(font, lifetime);
        }
        public void Dispose()
        {
            IGSharp_FontConfig_SetGlyphRanges(config._handle, config._glyphRanges);
            IGSharp_FontConfig_SetGlyphExcludeRanges(config._handle, config._glyphExcludeRanges);
            if (!_adopted) { IGSharp_MemFree(ranges); IGSharp_MemFree(excluded); }
        }
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
    internal static ushort* CopyRanges(ReadOnlySpan<ushort> value)
    {
        if (value.IsEmpty) return null;
        if (value.Length % 2 != 0)
            throw new ArgumentException("Glyph ranges must be pairs of (first, last) codepoints.", nameof(value));
        for (var i = 0; i < value.Length; i += 2)
            if (value[i] == 0 || value[i] > value[i + 1]) throw new ArgumentException("Glyph ranges must be nonzero, ordered pairs.", nameof(value));
        var copy = (ushort*)IGSharp_MemAlloc((nuint)((value.Length + 1) * sizeof(ushort)));
        if (copy == null) throw new OutOfMemoryException();
        value.CopyTo(new Span<ushort>(copy, value.Length));
        copy[value.Length] = 0;
        return copy;
    }
}
