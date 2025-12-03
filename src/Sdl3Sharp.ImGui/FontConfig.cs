using System.Runtime.InteropServices;
using Sdl3Sharp.ImGui.Native;

namespace Sdl3Sharp.ImGui;

/// <summary>
/// Represents font input/source configuration for loading fonts into a font atlas.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public unsafe struct FontConfig
{
    private ImFontConfig _native;

    /// <summary>
    /// Gets or sets whether the font atlas owns the font data (and will delete it on destruction).
    /// </summary>
    public bool FontDataOwnedByAtlas
    {
        readonly get => _native.FontDataOwnedByAtlas;
        set => _native.FontDataOwnedByAtlas = value;
    }

    /// <summary>
    /// Gets or sets whether to merge into the previous font, allowing combining multiple input fonts into one.
    /// </summary>
    public bool MergeMode
    {
        readonly get => _native.MergeMode;
        set => _native.MergeMode = value;
    }

    /// <summary>
    /// Gets or sets whether to align every glyph AdvanceX to pixel boundaries.
    /// </summary>
    public bool PixelSnapH
    {
        readonly get => _native.PixelSnapH;
        set => _native.PixelSnapH = value;
    }

    /// <summary>
    /// Gets or sets whether to align scaled GlyphOffset.Y to pixel boundaries.
    /// </summary>
    public bool PixelSnapV
    {
        readonly get => _native.PixelSnapV;
        set => _native.PixelSnapV = value;
    }

    /// <summary>
    /// Gets or sets the horizontal oversampling for sub-pixel positioning (0 = auto).
    /// </summary>
    public sbyte OversampleH
    {
        readonly get => _native.OversampleH;
        set => _native.OversampleH = value;
    }

    /// <summary>
    /// Gets or sets the vertical oversampling for sub-pixel positioning (0 = auto).
    /// </summary>
    public sbyte OversampleV
    {
        readonly get => _native.OversampleV;
        set => _native.OversampleV = value;
    }

    /// <summary>
    /// Gets or sets the Unicode codepoint of the ellipsis character (0 for auto).
    /// </summary>
    public char EllipsisChar
    {
        readonly get => (char)_native.EllipsisChar;
        set => _native.EllipsisChar = value;
    }

    /// <summary>
    /// Gets or sets the font size in pixels for rasterization.
    /// </summary>
    public float SizePixels
    {
        readonly get => _native.SizePixels;
        set => _native.SizePixels = value;
    }

    // According to source, glyph ranges are not needed any more.

    /// <summary>
    /// Gets or sets the glyph offset in pixels.
    /// </summary>
    public Vec2 GlyphOffset
    {
        readonly get => Vec2.FromNative(_native.GlyphOffset);
        set => _native.GlyphOffset = value.ToNative();
    }

    /// <summary>
    /// Gets or sets the minimum AdvanceX for glyphs.
    /// </summary>
    public float GlyphMinAdvanceX
    {
        readonly get => _native.GlyphMinAdvanceX;
        set => _native.GlyphMinAdvanceX = value;
    }

    /// <summary>
    /// Gets or sets the maximum AdvanceX for glyphs.
    /// </summary>
    public float GlyphMaxAdvanceX
    {
        readonly get => _native.GlyphMaxAdvanceX;
        set => _native.GlyphMaxAdvanceX = value;
    }

    /// <summary>
    /// Gets or sets extra spacing between glyphs in pixels.
    /// </summary>
    public float GlyphExtraAdvanceX
    {
        readonly get => _native.GlyphExtraAdvanceX;
        set => _native.GlyphExtraAdvanceX = value;
    }

    /// <summary>
    /// Gets or sets the index of the font within a TTF/OTF file.
    /// </summary>
    public uint FontNo
    {
        readonly get => _native.FontNo;
        set => _native.FontNo = value;
    }

    /// <summary>
    /// Gets or sets custom font loader flags (builder implementation dependent).
    /// </summary>
    public uint FontLoaderFlags
    {
        readonly get => _native.FontLoaderFlags;
        set => _native.FontLoaderFlags = value;
    }

    /// <summary>
    /// Gets or sets the rasterizer brightness multiplier.
    /// </summary>
    public float RasterizerMultiply
    {
        readonly get => _native.RasterizerMultiply;
        set => _native.RasterizerMultiply = value;
    }

    /// <summary>
    /// Gets or sets the DPI scale multiplier for rasterization.
    /// </summary>
    public float RasterizerDensity
    {
        readonly get => _native.RasterizerDensity;
        set => _native.RasterizerDensity = value;
    }

    /// <summary>
    /// Constructs a new FontConfig with default values.
    /// </summary>
    public FontConfig()
    {
        FontDataOwnedByAtlas = true;
        OversampleH = 0; // Auto == 1 or 2 depending on size
        OversampleV = 0; // Auto == 1
        GlyphMaxAdvanceX = float.MaxValue;
        RasterizerMultiply = 1.0f;
        RasterizerDensity = 1.0f;
        EllipsisChar = '\0';
    }

    /// <summary>
    /// Casts a pointer to a native ImFontConfig to a pointer to a FontConfig.
    /// </summary>
    /// <param name="native">The native ImFontConfig pointer.</param>
    /// <returns>A pointer to a FontConfig.</returns>
    public static FontConfig* FromNative(ImFontConfig* native)
    {
        return (FontConfig*)native;
    }

    /// <summary>
    /// Casts a pointer to a FontConfig to a pointer to a native ImFontConfig.
    /// </summary>
    /// <param name="fontConfig">The FontConfig pointer.</param>
    /// <returns>A pointer to an ImFontConfig.</returns>
    public static ImFontConfig* ToNative(FontConfig* fontConfig)
    {
        return (ImFontConfig*)fontConfig;
    }
}
