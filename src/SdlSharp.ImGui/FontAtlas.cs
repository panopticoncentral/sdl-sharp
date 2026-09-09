using System.Runtime.InteropServices;
using static SdlSharp.Native.Common;
using static SdlSharp.ImGui.Native;

namespace SdlSharp.ImGui;

/// <summary>
/// Flags for a <see cref="FontAtlas"/>.
/// </summary>
[Flags]
public enum FontAtlasFlags
{
    /// <summary>No flags.</summary>
    None = 0,
    /// <summary>Don't round the texture height to the next power of two.</summary>
    NoPowerOfTwoHeight = 1 << 0,
    /// <summary>Don't build software mouse cursors into the atlas (saves a little texture memory).</summary>
    NoMouseCursors = 1 << 1,
    /// <summary>Don't build thick-line textures into the atlas (saves texture memory, allows point/nearest filtering).</summary>
    NoBakedLines = 1 << 2,
}

/// <summary>
/// A handle to ImGui's shared font atlas. Obtain via <see cref="ImGui.GetFontAtlas"/>.
/// The atlas is owned by ImGui — do not dispose.
/// </summary>
public readonly unsafe struct FontAtlas
{
    internal readonly IGSharp_FontAtlas* Handle;

    internal FontAtlas(IGSharp_FontAtlas* handle) => Handle = handle;

    /// <summary>True if this handle refers to a valid font atlas.</summary>
    public bool IsValid => Handle != null;

    // --- Adding fonts ---

    /// <summary>Adds the default ProggyClean.ttf font.</summary>
    public Font AddDefaultFont(FontConfig? config = null)
    {
        if (config == null) return new(IGSharp_FontAtlas_AddFontDefault(Handle, null));
        using var scope = config.PrepareForAdd(Handle);
        return new(IGSharp_FontAtlas_AddFontDefault(Handle, scope.Handle));
    }

    /// <summary>Adds the default embedded vector font (scales cleanly to any size).</summary>
    public Font AddDefaultVectorFont(FontConfig? config = null)
    {
        if (config == null) return new(IGSharp_FontAtlas_AddFontDefaultVector(Handle, null));
        using var scope = config.PrepareForAdd(Handle);
        return new(IGSharp_FontAtlas_AddFontDefaultVector(Handle, scope.Handle));
    }

    /// <summary>Adds the default embedded bitmap font (ProggyClean, crisp at its native 13px size).</summary>
    public Font AddDefaultBitmapFont(FontConfig? config = null)
    {
        if (config == null) return new(IGSharp_FontAtlas_AddFontDefaultBitmap(Handle, null));
        using var scope = config.PrepareForAdd(Handle);
        return new(IGSharp_FontAtlas_AddFontDefaultBitmap(Handle, scope.Handle));
    }

    /// <summary>Loads a font from a TTF/OTF file on disk.</summary>
    public Font AddFontFromFileTTF(string filename, float sizePixels, FontConfig? config = null)
    {
        if (config == null)
            return new(IGSharp_FontAtlas_AddFontFromFileTTF(Handle, ToUtf8(filename), sizePixels, null, null));
        using var scope = config.PrepareForAdd(Handle);
        return new(IGSharp_FontAtlas_AddFontFromFileTTF(Handle, ToUtf8(filename), sizePixels, scope.Handle, null));
    }

    /// <summary>
    /// Loads a font from an in-memory TTF/OTF buffer. The buffer is copied into ImGui-owned memory,
    /// so <paramref name="fontData"/> does not need to remain valid after this call.
    /// </summary>
    public Font AddFontFromMemoryTTF(ReadOnlySpan<byte> fontData, float sizePixels, FontConfig? config = null)
    {
        // The atlas bakes glyphs lazily and reads the TTF data long after this call returns,
        // so copy the managed buffer into ImGui-allocated memory and transfer ownership to
        // the atlas (freed with the atlas), preserving the "buffer may be discarded" contract.
        var copy = (byte*)IGSharp_MemAlloc((nuint)fontData.Length);
        fontData.CopyTo(new Span<byte>(copy, fontData.Length));
        if (config != null)
        {
            using var scope = config.PrepareForAdd(Handle);
            var configHandle = scope.Handle;
            // The atlas copies the config on add, so temporarily marking the caller's config is safe.
            var previousOwnership = IGSharp_FontConfig_GetFontDataOwnedByAtlas(configHandle);
            IGSharp_FontConfig_SetFontDataOwnedByAtlas(configHandle, true);
            try
            {
                return new(IGSharp_FontAtlas_AddFontFromMemoryTTF(Handle, copy, fontData.Length, sizePixels, configHandle, null));
            }
            finally { IGSharp_FontConfig_SetFontDataOwnedByAtlas(configHandle, previousOwnership); }
        }
        var cfg = IGSharp_FontConfig_Create();
        try
        {
            IGSharp_FontConfig_SetFontDataOwnedByAtlas(cfg, true);
            return new(IGSharp_FontAtlas_AddFontFromMemoryTTF(Handle, copy, fontData.Length, sizePixels, cfg, null));
        }
        finally { IGSharp_FontConfig_Destroy(cfg); }
    }

    /// <summary>
    /// Loads a font from a compressed TTF buffer (produced by ImGui's <c>binary_to_compressed_c</c>
    /// tool). The data is decompressed into ImGui-owned memory, so the buffer does not need to
    /// remain valid after this call.
    /// </summary>
    public Font AddFontFromMemoryCompressedTTF(ReadOnlySpan<byte> compressedData, float sizePixels, FontConfig? config = null)
    {
        if (config == null)
        {
            fixed (byte* p = compressedData)
                return new(IGSharp_FontAtlas_AddFontFromMemoryCompressedTTF(Handle, p, compressedData.Length, sizePixels, null, null));
        }
        using var scope = config.PrepareForAdd(Handle);
        fixed (byte* p = compressedData)
            return new(IGSharp_FontAtlas_AddFontFromMemoryCompressedTTF(Handle, p, compressedData.Length, sizePixels,
                scope.Handle, null));
    }

    /// <summary>
    /// Loads a font from a base85-encoded compressed TTF string (produced by ImGui's
    /// <c>binary_to_compressed_c</c> tool with <c>-base85</c>).
    /// </summary>
    public Font AddFontFromMemoryCompressedBase85TTF(string compressedDataBase85, float sizePixels, FontConfig? config = null)
    {
        if (config == null)
            return new(IGSharp_FontAtlas_AddFontFromMemoryCompressedBase85TTF(Handle, ToUtf8(compressedDataBase85), sizePixels, null, null));
        using var scope = config.PrepareForAdd(Handle);
        return new(IGSharp_FontAtlas_AddFontFromMemoryCompressedBase85TTF(Handle, ToUtf8(compressedDataBase85), sizePixels, scope.Handle, null));
    }

    // --- Font enumeration / removal ---

    /// <summary>Number of fonts currently in the atlas.</summary>
    public int FontCount => IGSharp_FontAtlas_GetFontCount(Handle);

    /// <summary>Gets a font at the given index (0 to <see cref="FontCount"/> - 1).</summary>
    public Font GetFont(int index) => new(IGSharp_FontAtlas_GetFont(Handle, index));

    /// <summary>Removes a font from the atlas and releases its baked data.</summary>
    public void RemoveFont(Font font) => IGSharp_FontAtlas_RemoveFont(Handle, font.Handle);

    // --- Clearing / cache management ---

    /// <summary>Forces the atlas to build (rasterize) all queued fonts. Normally called automatically on first use.</summary>
    public bool Build() => true; // ImGui now bakes fonts lazily on demand; there is no explicit atlas build step.

    /// <summary>Clears everything: fonts, input data, and baked texture data.</summary>
    public void Clear() => IGSharp_FontAtlas_Clear(Handle);

    /// <summary>Clears all fonts (input configs and their baked output).</summary>
    public void ClearFonts() => IGSharp_FontAtlas_ClearFonts(Handle);

    /// <summary>Clears input font data (file buffers), keeping fonts and baked output. Frees CPU memory for loaded TTF data.</summary>
    public void ClearInputData() => IGSharp_FontAtlas_ClearInputData(Handle);

    /// <summary>Clears baked texture/glyph data; glyphs re-bake lazily on next use.</summary>
    public void ClearTexData() => IGSharp_FontAtlas_ClearTexData(Handle);

    /// <summary>Discards unused baked glyphs and sizes to shrink the atlas texture (safe to call any time).</summary>
    public void CompactCache() => IGSharp_FontAtlas_CompactCache(Handle);

    // --- Glyph ranges ---

    /// <summary>
    /// The default glyph ranges (Basic Latin + Latin Supplement) as pairs of inclusive
    /// (first, last) codepoints. The returned data is static — safe to hold and assign to
    /// <see cref="FontConfig.GlyphRanges"/>.
    /// </summary>
    public ReadOnlySpan<ushort> GetGlyphRangesDefault()
        => FontConfig.RangesFromPointer(IGSharp_FontAtlas_GetGlyphRangesDefault(Handle));

    // --- Custom rects ---

    /// <summary>
    /// Registers a <paramref name="width"/> x <paramref name="height"/> rectangle to be packed
    /// into the atlas texture, so you can render custom graphics alongside glyphs. Returns the
    /// rect id (or -1 on failure) and the packed location in <paramref name="rect"/>.
    /// If you render colored pixels into the rect, also set <see cref="TexPixelsUseColors"/> to true.
    /// </summary>
    public int AddCustomRect(int width, int height, out FontAtlasRect rect)
    {
        IGSharp_FontAtlasRect r = default;
        var id = IGSharp_FontAtlas_AddCustomRect(Handle, width, height, &r);
        rect = FontAtlasRect.FromNative(r);
        return id;
    }

    /// <summary>Registers a custom rectangle to be packed into the atlas texture. Returns the rect id (or -1 on failure).</summary>
    public int AddCustomRect(int width, int height)
        => IGSharp_FontAtlas_AddCustomRect(Handle, width, height, null);

    /// <summary>
    /// Gets the current packed location of a custom rect. Returns false if <paramref name="id"/>
    /// is invalid. Re-query every frame — the atlas texture can be resized/repacked at any time.
    /// </summary>
    public bool TryGetCustomRect(int id, out FontAtlasRect rect)
    {
        IGSharp_FontAtlasRect r = default;
        var ok = IGSharp_FontAtlas_GetCustomRect(Handle, id, &r);
        rect = ok ? FontAtlasRect.FromNative(r) : default;
        return ok;
    }

    /// <summary>Unregisters a custom rect, releasing its space in the atlas.</summary>
    public void RemoveCustomRect(int id) => IGSharp_FontAtlas_RemoveCustomRect(Handle, id);

    // --- Configuration ---

    /// <summary>Build flags for the atlas.</summary>
    public FontAtlasFlags Flags
    {
        get => (FontAtlasFlags)IGSharp_FontAtlas_GetFlags(Handle);
        set => IGSharp_FontAtlas_SetFlags(Handle, (int)value);
    }

    /// <summary>Preferred pixel format for the atlas texture. Default <see cref="TextureFormat.Rgba32"/>.</summary>
    public TextureFormat TexDesiredFormat
    {
        get => (TextureFormat)IGSharp_FontAtlas_GetTexDesiredFormat(Handle);
        set => IGSharp_FontAtlas_SetTexDesiredFormat(Handle, (int)value);
    }

    /// <summary>Padding between glyphs in the atlas texture, in texels. Default 1.</summary>
    public int TexGlyphPadding
    {
        get => IGSharp_FontAtlas_GetTexGlyphPadding(Handle);
        set => IGSharp_FontAtlas_SetTexGlyphPadding(Handle, value);
    }

    /// <summary>Minimum width for the atlas texture.</summary>
    public int TexMinWidth
    {
        get => IGSharp_FontAtlas_GetTexMinWidth(Handle);
        set => IGSharp_FontAtlas_SetTexMinWidth(Handle, value);
    }

    /// <summary>Minimum height for the atlas texture.</summary>
    public int TexMinHeight
    {
        get => IGSharp_FontAtlas_GetTexMinHeight(Handle);
        set => IGSharp_FontAtlas_SetTexMinHeight(Handle, value);
    }

    /// <summary>Maximum width for the atlas texture (should match the renderer's texture size limit).</summary>
    public int TexMaxWidth
    {
        get => IGSharp_FontAtlas_GetTexMaxWidth(Handle);
        set => IGSharp_FontAtlas_SetTexMaxWidth(Handle, value);
    }

    /// <summary>Maximum height for the atlas texture (should match the renderer's texture size limit).</summary>
    public int TexMaxHeight
    {
        get => IGSharp_FontAtlas_GetTexMaxHeight(Handle);
        set => IGSharp_FontAtlas_SetTexMaxHeight(Handle, value);
    }

    /// <summary>
    /// Set to true when rendering colored (non-white) pixels into custom rects, so backends that
    /// sample alpha only know to use the full color channels.
    /// </summary>
    public bool TexPixelsUseColors
    {
        get => IGSharp_FontAtlas_GetTexPixelsUseColors(Handle);
        set => IGSharp_FontAtlas_SetTexPixelsUseColors(Handle, value);
    }

    /// <summary>Shared font-loader flags (e.g. FreeType rasterizer flags) applied to all fonts in the atlas.</summary>
    public uint FontLoaderFlags
    {
        get => IGSharp_FontAtlas_GetFontLoaderFlags(Handle);
        set => IGSharp_FontAtlas_SetFontLoaderFlags(Handle, value);
    }

    // --- Texture output (read-only) ---

    /// <summary>
    /// The atlas texture and its CPU-side pixel data. The texture is created and updated by the
    /// renderer backend — see <see cref="TextureData"/> for lifetime caveats.
    /// </summary>
    public TextureData GetTexData() => new(IGSharp_FontAtlas_GetTexData(Handle));

    /// <summary>
    /// Backend-specific identifier for the current atlas texture (SDL_GPU backend:
    /// an SDL_GPUTexture*), usable with <see cref="DrawList.AddImage(ulong, Vec2, Vec2, Vec2, Vec2, uint)"/>.
    /// 0 until the backend creates the texture.
    /// </summary>
    public ulong TextureId => IGSharp_FontAtlas_GetTexID(Handle);

    /// <summary>Multiply texel coordinates by this to get UV coordinates (1/Width, 1/Height).</summary>
    public Vec2 TexUvScale
    {
        get { var v = IGSharp_FontAtlas_GetTexUvScale(Handle); return new Vec2(v.X, v.Y); }
    }

    /// <summary>UV coordinate of a guaranteed-white texel, for drawing untextured shapes with the atlas bound.</summary>
    public Vec2 TexUvWhitePixel
    {
        get { var v = IGSharp_FontAtlas_GetTexUvWhitePixel(Handle); return new Vec2(v.X, v.Y); }
    }

    /// <summary>True once the backend has created the atlas texture.</summary>
    public bool TexIsBuilt => IGSharp_FontAtlas_GetTexIsBuilt(Handle);

    /// <summary>True while ImGui is mid-frame and the atlas cannot be modified.</summary>
    public bool IsLocked => IGSharp_FontAtlas_GetLocked(Handle);

    /// <summary>True if the bound renderer backend supports dynamic texture updates (required by the dynamic font system).</summary>
    public bool RendererHasTextures => IGSharp_FontAtlas_GetRendererHasTextures(Handle);

    /// <summary>Name of the active font loader (e.g. "stb_truetype" or "FreeType"), for display/debugging.</summary>
    public string? FontLoaderName => Marshal.PtrToStringUTF8((nint)IGSharp_FontAtlas_GetFontLoaderName(Handle));
}
