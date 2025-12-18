using System.Text;
using Sdl3Sharp.ImGui.Native;
using static Sdl3Sharp.ImGui.Native.ImGui;

namespace Sdl3Sharp.ImGui;

/// <summary>
/// Represents a font atlas that loads and rasterizes multiple TTF/OTF fonts into a single texture.
/// </summary>
public unsafe readonly struct FontAtlas
{
    internal FontAtlas(ImFontAtlas* native)
    {
        Native = native;
    }

    internal readonly ImFontAtlas* Native { get; }

    /// <summary>
    /// Gets or sets the build flags for the atlas.
    /// </summary>
    public FontAtlasFlags Flags
    {
        get => (FontAtlasFlags)Native->Flags;
        set => Native->Flags = (ImFontAtlasFlags)value;
    }

    /// <summary>
    /// Gets or sets the desired texture format (default is RGBA32).
    /// </summary>
    public TextureFormat TexDesiredFormat
    {
        get => (TextureFormat)Native->TexDesiredFormat;
        set => Native->TexDesiredFormat = (ImTextureFormat)value;
    }

    /// <summary>
    /// Gets or sets the padding between glyphs within the texture in pixels (default is 1).
    /// </summary>
    public int TexGlyphPadding
    {
        get => Native->TexGlyphPadding;
        set => Native->TexGlyphPadding = value;
    }

    /// <summary>
    /// Gets or sets the minimum desired texture width. Must be a power of two (default is 512).
    /// </summary>
    public int TexMinWidth
    {
        get => Native->TexMinWidth;
        set => Native->TexMinWidth = value;
    }

    /// <summary>
    /// Gets or sets the minimum desired texture height. Must be a power of two (default is 128).
    /// </summary>
    public int TexMinHeight
    {
        get => Native->TexMinHeight;
        set => Native->TexMinHeight = value;
    }

    /// <summary>
    /// Gets or sets the maximum desired texture width. Must be a power of two (default is 8192).
    /// </summary>
    public int TexMaxWidth
    {
        get => Native->TexMaxWidth;
        set => Native->TexMaxWidth = value;
    }

    /// <summary>
    /// Gets or sets the maximum desired texture height. Must be a power of two (default is 8192).
    /// </summary>
    public int TexMaxHeight
    {
        get => Native->TexMaxHeight;
        set => Native->TexMaxHeight = value;
    }

    /// <summary>
    /// Gets or sets custom user data for the atlas.
    /// </summary>
    public nint UserData
    {
        get => Native->UserData;
        set => Native->UserData = value;
    }

    /// <summary>
    /// Gets the latest texture reference.
    /// </summary>
    public TextureRef TexRef => new(Native->TexRef);

    /// <summary>
    /// Gets the latest texture data.
    /// </summary>
    public TextureData TexData => new(Native->TexData);

    /// <summary>
    /// Adds a font from a font configuration.
    /// </summary>
    /// <param name="fontConfig">The font configuration.</param>
    /// <returns>The newly added font.</returns>
    public Font AddFont(FontConfig fontConfig)
    {
        return new(ImFontAtlas.AddFont(Native, (ImFontConfig*)&fontConfig));
    }

    /// <summary>
    /// Adds the default embedded font.
    /// </summary>
    /// <param name="fontConfig">Optional font configuration (can pass null to use defaults).</param>
    /// <returns>The newly added font.</returns>
    public Font AddFontDefault(FontConfig? fontConfig = null)
    {
        if (fontConfig is null)
        {
            return new(ImFontAtlas.AddFontDefault(Native, null));
        }

        FontConfig config = fontConfig.Value;
        return new(ImFontAtlas.AddFontDefault(Native, (ImFontConfig*)&config));
    }

    /// <summary>
    /// Adds a font from a TTF/OTF file.
    /// </summary>
    /// <param name="filename">The path to the font file.</param>
    /// <param name="sizePixels">The font size in pixels.</param>
    /// <param name="fontConfig">Optional font configuration.</param>
    /// <returns>The newly added font.</returns>
    public Font AddFontFromFileTtf(string filename, float sizePixels, FontConfig? fontConfig = null)
    {
        fixed (byte* filenamePtr = Encoding.UTF8.GetBytes(filename + '\0'))
        {
            if (fontConfig is null)
            {
                return new(ImFontAtlas.AddFontFromFileTTF(Native, filenamePtr, sizePixels, null, null));
            }

            FontConfig config = fontConfig.Value;
            return new(ImFontAtlas.AddFontFromFileTTF(Native, filenamePtr, sizePixels, (ImFontConfig*)&config, null));
        }
    }

    /// <summary>
    /// Adds a font from TTF/OTF data in memory.
    /// </summary>
    /// <param name="fontData">The font data.</param>
    /// <param name="sizePixels">The font size in pixels.</param>
    /// <param name="fontConfig">Optional font configuration. If FontDataOwnedByAtlas is true (the default),
    /// the data will be copied into ImGui-allocated memory. If false, the caller must ensure the data
    /// remains valid for the lifetime of the font atlas.</param>
    /// <returns>The newly added font.</returns>
    public Font AddFontFromMemoryTtf(ReadOnlySpan<byte> fontData, float sizePixels, FontConfig? fontConfig = null)
    {
        var ownedByAtlas = fontConfig is null || fontConfig.Value.FontDataOwnedByAtlas;

        if (ownedByAtlas)
        {
            // ImGui will free this memory, so we must use ImGui's allocator
            var imguiMemory = ImGui_MemAlloc((nuint)fontData.Length);
            fontData.CopyTo(new Span<byte>((void*)imguiMemory, fontData.Length));

            if (fontConfig is null)
            {
                return new(ImFontAtlas.AddFontFromMemoryTTF(Native, imguiMemory, fontData.Length, sizePixels, null, null));
            }

            FontConfig config = fontConfig.Value;
            return new(ImFontAtlas.AddFontFromMemoryTTF(Native, imguiMemory, fontData.Length, sizePixels, (ImFontConfig*)&config, null));
        }
        else
        {
            // Caller owns the memory and must keep it alive
            FontConfig config = fontConfig!.Value;
            fixed (byte* dataPtr = fontData)
            {
                return new(ImFontAtlas.AddFontFromMemoryTTF(Native, (nint)dataPtr, fontData.Length, sizePixels, (ImFontConfig*)&config, null));
            }
        }
    }

    /// <summary>
    /// Adds a font from compressed TTF/OTF data in memory.
    /// </summary>
    /// <param name="compressedFontData">The compressed font data (still owned by caller).</param>
    /// <param name="sizePixels">The font size in pixels.</param>
    /// <param name="fontConfig">Optional font configuration.</param>
    /// <returns>The newly added font.</returns>
    public Font AddFontFromMemoryCompressedTtf(ReadOnlySpan<byte> compressedFontData, float sizePixels, FontConfig? fontConfig = null)
    {
        fixed (byte* dataPtr = compressedFontData)
        {
            if (fontConfig is null)
            {
                return new(ImFontAtlas.AddFontFromMemoryCompressedTTF(Native, (nint)dataPtr, compressedFontData.Length, sizePixels, null, null));
            }

            FontConfig config = fontConfig.Value;
            return new(ImFontAtlas.AddFontFromMemoryCompressedTTF(Native, (nint)dataPtr, compressedFontData.Length, sizePixels, (ImFontConfig*)&config, null));
        }
    }

    /// <summary>
    /// Adds a font from base85-encoded compressed TTF/OTF data.
    /// </summary>
    /// <param name="compressedFontDataBase85">Base85-encoded compressed font data (still owned by caller).</param>
    /// <param name="sizePixels">The font size in pixels.</param>
    /// <param name="fontConfig">Optional font configuration.</param>
    /// <returns>The newly added font.</returns>
    public Font AddFontFromMemoryCompressedBase85Ttf(Span<byte> compressedFontDataBase85, float sizePixels, FontConfig? fontConfig = null)
    {
        fixed (byte* dataPtr = compressedFontDataBase85)
        {
            if (fontConfig is null)
            {
                return new(ImFontAtlas.AddFontFromMemoryCompressedBase85TTF(Native, dataPtr, sizePixels, null, null));
            }

            FontConfig config = fontConfig.Value;
            return new(ImFontAtlas.AddFontFromMemoryCompressedBase85TTF(Native, dataPtr, sizePixels, (ImFontConfig*)&config, null));
        }
    }

    /// <summary>
    /// Removes a font from the atlas.
    /// </summary>
    /// <param name="font">The font to remove.</param>
    public void RemoveFont(Font font)
    {
        ImFontAtlas.RemoveFont(Native, font.Native);
    }

    /// <summary>
    /// Clears everything (input fonts, output glyphs/textures).
    /// </summary>
    public void Clear()
    {
        ImFontAtlas.Clear(Native);
    }

    /// <summary>
    /// Compacts cached glyphs and texture.
    /// </summary>
    public void CompactCache()
    {
        ImFontAtlas.CompactCache(Native);
    }

    /// <summary>
    /// Registers a custom rectangle in the atlas.
    /// </summary>
    /// <param name="width">The width of the rectangle.</param>
    /// <param name="height">The height of the rectangle.</param>
    /// <param name="outRect">Receives the rectangle information.</param>
    /// <returns>The rectangle ID, or <see cref="FontAtlasRectId.Invalid"/> on error.</returns>
    public FontAtlasRectId AddCustomRect(int width, int height, out FontAtlasRect outRect)
    {
        ImFontAtlasRect nativeRect;
        FontAtlasRectId id = new(ImFontAtlas.AddCustomRect(Native, width, height, &nativeRect));
        outRect = new(nativeRect);
        return id;
    }

    /// <summary>
    /// Unregisters a custom rectangle. Existing pixels stay in texture until resized/garbage collected.
    /// </summary>
    /// <param name="id">The rectangle ID to remove.</param>
    public void RemoveCustomRect(FontAtlasRectId id)
    {
        ImFontAtlas.RemoveCustomRect(Native, id.Native);
    }

    /// <summary>
    /// Gets rectangle coordinates for the current texture. Valid immediately, never store this!
    /// </summary>
    /// <param name="id">The rectangle ID.</param>
    /// <param name="outRect">Receives the rectangle information.</param>
    /// <returns>True if the rectangle exists; otherwise, false.</returns>
    public bool GetCustomRect(FontAtlasRectId id, out FontAtlasRect outRect)
    {
        ImFontAtlasRect nativeRect;
        var result = ImFontAtlas.GetCustomRect(Native, id.Native, &nativeRect);
        outRect = new(nativeRect);
        return result;
    }
}
