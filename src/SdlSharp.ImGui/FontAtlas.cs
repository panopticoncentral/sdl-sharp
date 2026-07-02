using static SdlSharp.Native.Common;
using static SdlSharp.ImGui.Native;

namespace SdlSharp.ImGui;

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

    /// <summary>Adds the default ProggyClean.ttf font.</summary>
    public Font AddDefaultFont() => new(IGSharp_FontAtlas_AddFontDefault(Handle, null));

    /// <summary>Loads a font from a TTF file on disk.</summary>
    public Font AddFontFromFileTTF(string filename, float sizePixels)
        => new(IGSharp_FontAtlas_AddFontFromFileTTF(Handle, ToUtf8(filename), sizePixels, null, null));

    /// <summary>
    /// Loads a font from an in-memory TTF buffer. The buffer is copied into ImGui-owned memory,
    /// so <paramref name="fontData"/> does not need to remain valid after this call.
    /// </summary>
    public Font AddFontFromMemoryTTF(ReadOnlySpan<byte> fontData, float sizePixels)
    {
        // The atlas bakes glyphs lazily and reads the TTF data long after this call returns,
        // so copy the managed buffer into ImGui-allocated memory and transfer ownership to
        // the atlas (freed with the atlas), preserving the "buffer may be discarded" contract.
        var copy = (byte*)IGSharp_MemAlloc((nuint)fontData.Length);
        fontData.CopyTo(new Span<byte>(copy, fontData.Length));
        var cfg = IGSharp_FontConfig_Create();
        try
        {
            IGSharp_FontConfig_SetFontDataOwnedByAtlas(cfg, true);
            return new(IGSharp_FontAtlas_AddFontFromMemoryTTF(Handle, copy, fontData.Length, sizePixels, cfg, null));
        }
        finally { IGSharp_FontConfig_Destroy(cfg); }
    }

    /// <summary>Loads a font from a compressed TTF buffer (produced by ImGui's <c>binary_to_compressed_c</c> tool).</summary>
    public Font AddFontFromMemoryCompressedTTF(ReadOnlySpan<byte> compressedData, float sizePixels)
    {
        fixed (byte* p = compressedData)
            return new(IGSharp_FontAtlas_AddFontFromMemoryCompressedTTF(Handle, p, compressedData.Length, sizePixels, null, null));
    }

    /// <summary>Forces the atlas to build (rasterize) all queued fonts. Normally called automatically on first use.</summary>
    public bool Build() => true; // ImGui now bakes fonts lazily on demand; there is no explicit atlas build step.

    /// <summary>Clears all input data (glyphs, configs) and output data (baked texture). Called automatically on destroy.</summary>
    public void Clear() => IGSharp_FontAtlas_Clear(Handle);

    /// <summary>Clears only the font configs (input data); keeps baked output.</summary>
    public void ClearFonts() => IGSharp_FontAtlas_ClearFonts(Handle);

    /// <summary>Number of fonts currently in the atlas.</summary>
    public int FontCount => IGSharp_FontAtlas_GetFontCount(Handle);

    /// <summary>Gets a font at the given index.</summary>
    public Font GetFont(int index) => new(IGSharp_FontAtlas_GetFont(Handle, index));
}
