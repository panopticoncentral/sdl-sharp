using System.Runtime.InteropServices;
using System.Text;
using Sdl3Sharp.ImGui.Native;

namespace Sdl3Sharp.ImGui;

/// <summary>
/// Represents a font atlas that loads and rasterizes multiple TTF/OTF fonts into a single texture.
/// </summary>
/// <remarks>
/// <para>
/// The font atlas will build a single texture holding one or more fonts, custom graphics data needed
/// to render the shapes needed by Dear ImGui, and mouse cursor shapes for software cursor rendering.
/// </para>
/// <para>
/// If you don't call any AddFont*** methods, the default font embedded in the code will be loaded for you.
/// </para>
/// </remarks>
[StructLayout(LayoutKind.Sequential)]
public unsafe struct FontAtlas
{
    private ImFontAtlas _native;

    /// <summary>
    /// Gets or sets the build flags for the atlas.
    /// </summary>
    public FontAtlasFlags Flags
    {
        readonly get => (FontAtlasFlags)_native.Flags;
        set => _native.Flags = (ImFontAtlasFlags)value;
    }

    /// <summary>
    /// Gets or sets the desired texture format (default is RGBA32).
    /// </summary>
    public TextureFormat TexDesiredFormat
    {
        readonly get => (TextureFormat)_native.TexDesiredFormat;
        set => _native.TexDesiredFormat = (ImTextureFormat)value;
    }

    /// <summary>
    /// Gets or sets the padding between glyphs within the texture in pixels (default is 1).
    /// </summary>
    public int TexGlyphPadding
    {
        readonly get => _native.TexGlyphPadding;
        set => _native.TexGlyphPadding = value;
    }

    /// <summary>
    /// Gets or sets the minimum desired texture width. Must be a power of two (default is 512).
    /// </summary>
    public int TexMinWidth
    {
        readonly get => _native.TexMinWidth;
        set => _native.TexMinWidth = value;
    }

    /// <summary>
    /// Gets or sets the minimum desired texture height. Must be a power of two (default is 128).
    /// </summary>
    public int TexMinHeight
    {
        readonly get => _native.TexMinHeight;
        set => _native.TexMinHeight = value;
    }

    /// <summary>
    /// Gets or sets the maximum desired texture width. Must be a power of two (default is 8192).
    /// </summary>
    public int TexMaxWidth
    {
        readonly get => _native.TexMaxWidth;
        set => _native.TexMaxWidth = value;
    }

    /// <summary>
    /// Gets or sets the maximum desired texture height. Must be a power of two (default is 8192).
    /// </summary>
    public int TexMaxHeight
    {
        readonly get => _native.TexMaxHeight;
        set => _native.TexMaxHeight = value;
    }

    /// <summary>
    /// Gets or sets custom user data for the atlas.
    /// </summary>
    public nint UserData
    {
        readonly get => _native.UserData;
        set => _native.UserData = value;
    }

    /// <summary>
    /// Gets the latest texture reference.
    /// </summary>
    public readonly TextureRef TexRef => new(_native.TexRef);

    /// <summary>
    /// Gets the latest texture data.
    /// </summary>
    public readonly TextureData TexData => TextureData.FromNative(_native.TexData);

    /// <summary>
    /// Adds a font from a font configuration.
    /// </summary>
    /// <param name="fontConfig">The font configuration.</param>
    /// <returns>The newly added font.</returns>
    public readonly Font AddFont(FontConfig fontConfig)
    {
        fixed (ImFontAtlas* ptr = &_native)
        {
            return new(ImFontAtlas.AddFont(ptr, (ImFontConfig*)&fontConfig));
        }
    }

    /// <summary>
    /// Adds the default embedded font.
    /// </summary>
    /// <param name="fontConfig">Optional font configuration (can pass null to use defaults).</param>
    /// <returns>The newly added font.</returns>
    public readonly Font AddFontDefault(FontConfig? fontConfig = null)
    {
        fixed (ImFontAtlas* ptr = &_native)
        {
            if (fontConfig is null)
            {
                return new(ImFontAtlas.AddFontDefault(ptr, null));
            }

            FontConfig config = fontConfig.Value;
            return new(ImFontAtlas.AddFontDefault(ptr, (ImFontConfig*)&config));
        }
    }

    /// <summary>
    /// Adds a font from a TTF/OTF file.
    /// </summary>
    /// <param name="filename">The path to the font file.</param>
    /// <param name="sizePixels">The font size in pixels.</param>
    /// <param name="fontConfig">Optional font configuration.</param>
    /// <returns>The newly added font.</returns>
    public readonly Font AddFontFromFileTtf(string filename, float sizePixels, FontConfig? fontConfig = null)
    {
        fixed (ImFontAtlas* ptr = &_native)
        fixed (byte* filenamePtr = Encoding.UTF8.GetBytes(filename + '\0'))
        {
            if (fontConfig is null)
            {
                return new(ImFontAtlas.AddFontFromFileTTF(ptr, filenamePtr, sizePixels, null, null));
            }

            FontConfig config = fontConfig.Value;
            return new(ImFontAtlas.AddFontFromFileTTF(ptr, filenamePtr, sizePixels, (ImFontConfig*)&config, null));
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
    public readonly Font AddFontFromMemoryTtf(ReadOnlySpan<byte> fontData, float sizePixels, FontConfig? fontConfig = null)
    {
        fixed (ImFontAtlas* ptr = &_native)
        {
            var ownedByAtlas = fontConfig is null || fontConfig.Value.FontDataOwnedByAtlas;

            if (ownedByAtlas)
            {
                // ImGui will free this memory, so we must use ImGui's allocator
                var imguiMemory = Native.ImGui.MemAlloc((nuint)fontData.Length);
                fontData.CopyTo(new Span<byte>((void*)imguiMemory, fontData.Length));

                if (fontConfig is null)
                {
                    return new(ImFontAtlas.AddFontFromMemoryTTF(ptr, imguiMemory, fontData.Length, sizePixels, null, null));
                }

                FontConfig config = fontConfig.Value;
                return new(ImFontAtlas.AddFontFromMemoryTTF(ptr, imguiMemory, fontData.Length, sizePixels, (ImFontConfig*)&config, null));
            }
            else
            {
                // Caller owns the memory and must keep it alive
                FontConfig config = fontConfig!.Value;
                fixed (byte* dataPtr = fontData)
                {
                    return new(ImFontAtlas.AddFontFromMemoryTTF(ptr, (nint)dataPtr, fontData.Length, sizePixels, (ImFontConfig*)&config, null));
                }
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
    public readonly Font AddFontFromMemoryCompressedTtf(ReadOnlySpan<byte> compressedFontData, float sizePixels, FontConfig? fontConfig = null)
    {
        fixed (ImFontAtlas* ptr = &_native)
        fixed (byte* dataPtr = compressedFontData)
        {
            if (fontConfig is null)
            {
                return new(ImFontAtlas.AddFontFromMemoryCompressedTTF(ptr, (nint)dataPtr, compressedFontData.Length, sizePixels, null, null));
            }

            FontConfig config = fontConfig.Value;
            return new(ImFontAtlas.AddFontFromMemoryCompressedTTF(ptr, (nint)dataPtr, compressedFontData.Length, sizePixels, (ImFontConfig*)&config, null));
        }
    }

    /// <summary>
    /// Adds a font from base85-encoded compressed TTF/OTF data.
    /// </summary>
    /// <param name="compressedFontDataBase85">Base85-encoded compressed font data (still owned by caller).</param>
    /// <param name="sizePixels">The font size in pixels.</param>
    /// <param name="fontConfig">Optional font configuration.</param>
    /// <returns>The newly added font.</returns>
    public readonly Font AddFontFromMemoryCompressedBase85Ttf(Span<byte> compressedFontDataBase85, float sizePixels, FontConfig? fontConfig = null)
    {
        fixed (ImFontAtlas* ptr = &_native)
        fixed (byte* dataPtr = compressedFontDataBase85)
        {
            if (fontConfig is null)
            {
                return new(ImFontAtlas.AddFontFromMemoryCompressedBase85TTF(ptr, dataPtr, sizePixels, null, null));
            }

            FontConfig config = fontConfig.Value;
            return new(ImFontAtlas.AddFontFromMemoryCompressedBase85TTF(ptr, dataPtr, sizePixels, (ImFontConfig*)&config, null));
        }
    }

    /// <summary>
    /// Removes a font from the atlas.
    /// </summary>
    /// <param name="font">The font to remove.</param>
    public readonly void RemoveFont(Font font)
    {
        fixed (ImFontAtlas* ptr = &_native)
        {
            ImFontAtlas.RemoveFont(ptr, font.ToNative());
        }
    }

    /// <summary>
    /// Clears everything (input fonts, output glyphs/textures).
    /// </summary>
    public readonly void Clear()
    {
        fixed (ImFontAtlas* ptr = &_native)
        {
            ImFontAtlas.Clear(ptr);
        }
    }

    /// <summary>
    /// Compacts cached glyphs and texture.
    /// </summary>
    public readonly void CompactCache()
    {
        fixed (ImFontAtlas* ptr = &_native)
        {
            ImFontAtlas.CompactCache(ptr);
        }
    }

    /// <summary>
    /// Registers a custom rectangle in the atlas.
    /// </summary>
    /// <param name="width">The width of the rectangle.</param>
    /// <param name="height">The height of the rectangle.</param>
    /// <param name="outRect">Receives the rectangle information.</param>
    /// <returns>The rectangle ID, or <see cref="FontAtlasRectId.Invalid"/> on error.</returns>
    public readonly FontAtlasRectId AddCustomRect(int width, int height, out FontAtlasRect outRect)
    {
        fixed (ImFontAtlas* ptr = &_native)
        {
            ImFontAtlasRect nativeRect;
            FontAtlasRectId id = ImFontAtlas.AddCustomRect(ptr, width, height, &nativeRect);
            outRect = *FontAtlasRect.FromNative(&nativeRect);
            return id;
        }
    }

    /// <summary>
    /// Unregisters a custom rectangle. Existing pixels stay in texture until resized/garbage collected.
    /// </summary>
    /// <param name="id">The rectangle ID to remove.</param>
    public readonly void RemoveCustomRect(FontAtlasRectId id)
    {
        fixed (ImFontAtlas* ptr = &_native)
        {
            ImFontAtlas.RemoveCustomRect(ptr, id);
        }
    }

    /// <summary>
    /// Gets rectangle coordinates for the current texture. Valid immediately, never store this!
    /// </summary>
    /// <param name="id">The rectangle ID.</param>
    /// <param name="outRect">Receives the rectangle information.</param>
    /// <returns>True if the rectangle exists; otherwise, false.</returns>
    public readonly bool GetCustomRect(FontAtlasRectId id, out FontAtlasRect outRect)
    {
        fixed (ImFontAtlas* ptr = &_native)
        {
            ImFontAtlasRect nativeRect;
            var result = ImFontAtlas.GetCustomRect(ptr, id, &nativeRect);
            outRect = *FontAtlasRect.FromNative(&nativeRect);
            return result;
        }
    }

    /// <summary>
    /// Casts a pointer to a native ImFontAtlas to a pointer to a FontAtlas.
    /// </summary>
    /// <param name="native">The native ImFontAtlas pointer.</param>
    /// <returns>A pointer to a FontAtlas.</returns>
    public static FontAtlas* FromNative(ImFontAtlas* native)
    {
        return (FontAtlas*)native;
    }

    /// <summary>
    /// Casts a pointer to a FontAtlas to a pointer to a native ImFontAtlas.
    /// </summary>
    /// <param name="fontAtlas">The FontAtlas pointer.</param>
    /// <returns>A pointer to an ImFontAtlas.</returns>
    public static ImFontAtlas* ToNative(FontAtlas* fontAtlas)
    {
        return (ImFontAtlas*)fontAtlas;
    }
}
