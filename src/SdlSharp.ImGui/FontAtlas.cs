using System.Runtime.InteropServices;
using static SdlSharp.Native.Common;
using static SdlSharp.ImGui.Native;

namespace SdlSharp.ImGui;

/// <summary>
/// A handle to ImGui's shared font atlas. Obtain via <see cref="ImGui.GetFontAtlas"/>.
/// The atlas is owned by ImGui — do not dispose.
/// </summary>
public readonly unsafe struct FontAtlas
{
    private readonly NativeView<IGSharp_FontAtlas> _view;
    internal IGSharp_FontAtlas* Handle => _view.Handle;
    internal Lifetime Lifetime => _view.Lifetime!;

    internal FontAtlas(IGSharp_FontAtlas* handle, Lifetime? lifetime = null, bool? frame = null) => _view = new(handle, lifetime, frame ?? false);

    /// <summary>True if this handle refers to a valid font atlas.</summary>
    public bool IsValid => _view.IsValid;

    /// <summary>Deferred atlas texture reference, safe to record before GPU upload.</summary>
    public TextureReference TextureReference
    {
        get { var value = IGSharp_FontAtlas_GetTexRef(Handle); if (value.Data != null) _ = SdlSharp.ImGui.Lifetime.Capture((nint)value.Data, Lifetime); return new(value); }
    }

    private void EnsureMutable() { Context.TryGetCurrent()?.CheckOutsideCallback(); if (IsLocked) throw new InvalidOperationException("The font atlas is locked during a frame."); }
    internal Font Wrap(IGSharp_Font* pointer)
    {
        if (pointer == null) throw new InvalidOperationException("ImGui could not load the font.");
        return new(pointer, Lifetime.Capture((nint)pointer, Lifetime));
    }

    // --- Adding fonts ---

    /// <summary>Adds the default ProggyClean.ttf font.</summary>
    public Font AddDefaultFont(FontConfig? config = null)
    {
        EnsureMutable();
        if (config == null) return Wrap(IGSharp_FontAtlas_AddFontDefault(Handle, null));
        using var scope = config.PrepareForAdd(Handle);
        return scope.Finish(IGSharp_FontAtlas_AddFontDefault(Handle, scope.Handle), Lifetime);
    }

    /// <summary>Adds the default embedded vector font (scales cleanly to any size).</summary>
    public Font AddDefaultVectorFont(FontConfig? config = null)
    {
        EnsureMutable();
        if (config == null) return Wrap(IGSharp_FontAtlas_AddFontDefaultVector(Handle, null));
        using var scope = config.PrepareForAdd(Handle);
        return scope.Finish(IGSharp_FontAtlas_AddFontDefaultVector(Handle, scope.Handle), Lifetime);
    }

    /// <summary>Adds the default embedded bitmap font (ProggyClean, crisp at its native 13px size).</summary>
    public Font AddDefaultBitmapFont(FontConfig? config = null)
    {
        EnsureMutable();
        if (config == null) return Wrap(IGSharp_FontAtlas_AddFontDefaultBitmap(Handle, null));
        using var scope = config.PrepareForAdd(Handle);
        return scope.Finish(IGSharp_FontAtlas_AddFontDefaultBitmap(Handle, scope.Handle), Lifetime);
    }

    /// <summary>Loads a font from a TTF/OTF file on disk.</summary>
    public Font AddFontFromFileTTF(string filename, float sizePixels, FontConfig? config = null)
    {
        ArgumentException.ThrowIfNullOrEmpty(filename);
        return AddFontFromMemoryTTF(File.ReadAllBytes(filename), sizePixels, config);
    }

    /// <summary>
    /// Loads a font from an in-memory TTF/OTF buffer. The buffer is copied into ImGui-owned memory,
    /// so <paramref name="fontData"/> does not need to remain valid after this call.
    /// </summary>
    public Font AddFontFromMemoryTTF(ReadOnlySpan<byte> fontData, float sizePixels, FontConfig? config = null)
    {
        if (!TryAddFontFromMemoryTTF(fontData, sizePixels, out var font, config)) throw new InvalidOperationException("ImGui could not load the font.");
        return font;
    }

    /// <summary>
    /// Loads a font from a compressed TTF buffer (produced by ImGui's <c>binary_to_compressed_c</c>
    /// tool). The data is decompressed into ImGui-owned memory, so the buffer does not need to
    /// remain valid after this call. Invalid data throws InvalidDataException; decompressed data is limited to 64 MiB by default.
    /// </summary>
    public Font AddFontFromMemoryCompressedTTF(ReadOnlySpan<byte> compressedData, float sizePixels, FontConfig? config = null, int maximumDecompressedSize = CompressedFont.DefaultMaximumSize)
    {
        EnsureMutable();
        return AddFontFromMemoryTTF(CompressedFont.Decode(compressedData, maximumDecompressedSize), sizePixels, config);
    }

    /// <summary>Loads an ImGui Base85 compressed font after bounded decoding. Invalid data throws InvalidDataException; decompressed data is limited to 64 MiB by default.</summary>
    public Font AddFontFromMemoryCompressedBase85TTF(string compressedDataBase85, float sizePixels, FontConfig? config = null, int maximumDecompressedSize = CompressedFont.DefaultMaximumSize)
    {
        EnsureMutable();
        return AddFontFromMemoryCompressedTTF(CompressedFont.DecodeBase85(compressedDataBase85), sizePixels, config, maximumDecompressedSize);
    }

    /// <summary>Attempts to load a copied TTF/OTF buffer. Returns false for a rejected font.</summary>
    public bool TryAddFontFromMemoryTTF(ReadOnlySpan<byte> data, float sizePixels, out Font font, FontConfig? config = null)
    {
        var handle = Handle;
        if (IsLocked) throw new InvalidOperationException("Fonts cannot be added while the atlas is locked.");
        fixed (byte* pointer = data)
        {
            using var scope = config?.PrepareForAdd(handle);
            var result = IGSharp_FontAtlas_AddFontFromMemoryTTFCopy(handle, pointer, data.Length, sizePixels, scope == null ? null : scope.Handle, null);
            if (result == null) { font = default; return false; }
            font = scope == null ? Wrap(result) : scope.Finish(result, Lifetime); return true;
        }
    }

    // --- Font enumeration / removal ---

    /// <summary>Number of fonts currently in the atlas.</summary>
    public int FontCount => IGSharp_FontAtlas_GetFontCount(Handle);

    /// <summary>Gets a font at the given index (0 to <see cref="FontCount"/> - 1).</summary>
    public Font GetFont(int index) { ArgumentOutOfRangeException.ThrowIfNegative(index); ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(index, FontCount); return Wrap(IGSharp_FontAtlas_GetFont(Handle, index)); }

    /// <summary>Removes a font from the atlas and releases its baked data.</summary>
    public void RemoveFont(Font font)
    {
        EnsureMutable();
        var handle = Handle; var target = font.Handle;
        if (IGSharp_Font_GetOwnerAtlas(target) != handle) throw new ArgumentException("The font belongs to another atlas.", nameof(font));
        Lifetime.EnsureUnretained(); font.Lifetime.EnsureUnretained(); IGSharp_FontAtlas_RemoveFont(handle, target); font.Lifetime.Dispose();
    }

    // --- Clearing / cache management ---


    /// <summary>Clears everything: fonts, input data, and baked texture data.</summary>
    public void Clear() { EnsureMutable(); Lifetime.EnsureUnretained(); IGSharp_FontAtlas_Clear(Handle); Lifetime.InvalidateChildren(); }

    /// <summary>Clears all fonts (input configs and their baked output).</summary>
    public void ClearFonts() { EnsureMutable(); Lifetime.EnsureUnretained(); IGSharp_FontAtlas_ClearFonts(Handle); Lifetime.InvalidateChildren(); }

    /// <summary>Clears source buffers and baked glyph data. Font identities remain, but cannot load new glyphs until new sources are added.</summary>
    public void ClearInputData()
    {
        EnsureMutable(); Lifetime.EnsureUnretained();
        var fonts = new Font[FontCount]; for (var i = 0; i < fonts.Length; i++) fonts[i] = GetFont(i);
        IGSharp_FontAtlas_ClearInputData(Handle);
        foreach (var font in fonts) { font.Lifetime.InvalidateChildren(); font.Lifetime.ReleaseAttachments(); }
    }

    /// <summary>Clears baked texture/glyph data; glyphs re-bake lazily on next use.</summary>
    public void ClearTexData()
    {
        EnsureMutable(); Lifetime.EnsureUnretained();
        if (RendererHasTextures) throw new InvalidOperationException("Dynamic atlases do not support ClearTexData. Use Clear instead.");
        IGSharp_FontAtlas_ClearTexData(Handle);
    }

    /// <summary>Discards unused baked glyphs and sizes to shrink the atlas texture (safe to call any time).</summary>
    public void CompactCache() { Lifetime.EnsureUnretained(); IGSharp_FontAtlas_CompactCache(Handle); Lifetime.InvalidateGrandchildren(); }

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

    /// <summary>Application-owned opaque native association; no ownership is transferred.</summary>
    public nint UserData { get => (nint)IGSharp_FontAtlas_GetUserData(Handle); set => IGSharp_FontAtlas_SetUserData(Handle, (void*)value); }

    // --- Configuration ---

    /// <summary>Build flags for the atlas.</summary>
    public FontAtlasFlags Flags
    {
        get => (FontAtlasFlags)IGSharp_FontAtlas_GetFlags(Handle);
        set => IGSharp_FontAtlas_SetFlags(Handle, (int)value);
    }

    /// <summary>Preferred pixel format for the atlas texture. Default <see cref="TextureFormat.RGBA32"/>.</summary>
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
    public TextureData GetTexData() { var pointer = IGSharp_FontAtlas_GetTexData(Handle); return new(pointer, pointer == null ? null : Lifetime.Capture((nint)pointer, Lifetime)); }

    /// <summary>
    /// Backend-specific identifier for the current atlas texture (SDL_GPU backend:
    /// an SDL_GPUTexture*), usable with <see cref="DrawList.AddImage(TextureReference, Vec2, Vec2, Vec2, Vec2, uint)"/>.
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
