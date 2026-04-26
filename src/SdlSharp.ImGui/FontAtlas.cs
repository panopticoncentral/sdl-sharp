using static SdlSharp.Native.Common;
using static SdlSharp.ImGui.Native;

namespace SdlSharp.ImGui;

/// <summary>
/// A handle to ImGui's shared font atlas. Obtain via <see cref="ImGui.GetFontAtlas"/>.
/// The atlas is owned by ImGui — do not dispose.
/// </summary>
public readonly unsafe struct FontAtlas
{
    internal readonly void* Handle;

    internal FontAtlas(void* handle) => Handle = handle;

    /// <summary>True if this handle refers to a valid font atlas.</summary>
    public bool IsValid => Handle != null;

    /// <summary>Adds the default ProggyClean.ttf font.</summary>
    public Font AddDefaultFont() => new(IGSharp_FontAtlas_AddFontDefault(Handle));

    /// <summary>Loads a font from a TTF file on disk.</summary>
    public Font AddFontFromFileTTF(string filename, float sizePixels)
        => new(IGSharp_FontAtlas_AddFontFromFileTTF(Handle, ToUtf8(filename), sizePixels));

    /// <summary>
    /// Loads a font from an in-memory TTF buffer. ImGui copies the buffer internally,
    /// so <paramref name="fontData"/> does not need to remain valid after this call.
    /// </summary>
    public Font AddFontFromMemoryTTF(ReadOnlySpan<byte> fontData, float sizePixels)
    {
        fixed (byte* p = fontData)
            return new(IGSharp_FontAtlas_AddFontFromMemoryTTF(Handle, p, fontData.Length, sizePixels, transfer_ownership: false));
    }

    /// <summary>Loads a font from a compressed TTF buffer (produced by ImGui's <c>binary_to_compressed_c</c> tool).</summary>
    public Font AddFontFromMemoryCompressedTTF(ReadOnlySpan<byte> compressedData, float sizePixels)
    {
        fixed (byte* p = compressedData)
            return new(IGSharp_FontAtlas_AddFontFromMemoryCompressedTTF(Handle, p, compressedData.Length, sizePixels));
    }

    /// <summary>Forces the atlas to build (rasterize) all queued fonts. Normally called automatically on first use.</summary>
    public bool Build() => IGSharp_FontAtlas_Build(Handle);

    /// <summary>Clears all input data (glyphs, configs) and output data (baked texture). Called automatically on destroy.</summary>
    public void Clear() => IGSharp_FontAtlas_Clear(Handle);

    /// <summary>Clears only the font configs (input data); keeps baked output.</summary>
    public void ClearFonts() => IGSharp_FontAtlas_ClearFonts(Handle);

    /// <summary>Number of fonts currently in the atlas.</summary>
    public int FontCount => IGSharp_FontAtlas_GetFontCount(Handle);

    /// <summary>Gets a font at the given index.</summary>
    public Font GetFont(int index) => new(IGSharp_FontAtlas_GetFont(Handle, index));
}
