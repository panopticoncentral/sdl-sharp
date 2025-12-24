using System.Runtime.InteropServices;
using Sdl3Sharp.ImGui.Native;
using static Sdl3Sharp.ImGui.Native.ImGui;

namespace Sdl3Sharp.ImGui;

/// <summary>
/// Represents font runtime data and rendering information.
/// </summary>
public unsafe readonly struct Font
{
    internal readonly ImFont* Native { get; }

    /// <summary>
    /// Gets the current font.
    /// </summary>
    /// <returns>The current font.</returns>
    public static Font? Current
    {
        get
        {
            ImFont* fontPtr = ImGui_GetFont();
            return fontPtr != null ? new Font(fontPtr) : null;
        }
    }

    /// <summary>
    /// Gets the current font baked at the current size.
    /// </summary>
    /// <remarks>
    /// Only valid for the current frame.
    /// </remarks>
    public static FontBaked? Baked
    {
        get
        {
            ImFontBaked* bakedPtr = ImGui_GetFontBaked();
            return bakedPtr != null ? new FontBaked(bakedPtr) : null;
        }
    }

    internal Font(ImFont* native)
    {
        Native = native;
    }

    /// <summary>
    /// Gets the current scaled font size (height in pixels) after global scale factors are applied.
    /// </summary>
    /// <returns>The current font size in pixels.</returns>
    /// <remarks>
    /// Do NOT pass this value to <see cref="PushFont"/>! Use Style.FontSizeBase to get the value before global scale factors.
    /// </remarks>
    public static float GetFontSize()
    {
        return ImGui_GetFontSize();
    }

    /// <summary>
    /// Checks whether a glyph exists in this font for the specified character.
    /// </summary>
    /// <param name="c">The Unicode codepoint to check.</param>
    /// <returns>True if the glyph exists; otherwise, false.</returns>
    public bool IsGlyphInFont(ushort c)
    {
        return ImFont.IsGlyphInFont(Native, c);
    }

    /// <summary>
    /// Checks whether this font has been loaded.
    /// </summary>
    /// <returns>True if the font is loaded; otherwise, false.</returns>
    public bool IsLoaded()
    {
        return ImFont.IsLoaded(Native);
    }

    /// <summary>
    /// Gets the debug name of this font (from FontConfig.Name).
    /// </summary>
    /// <returns>The debug name string.</returns>
    public string GetDebugName()
    {
        var namePtr = ImFont.GetDebugName(Native);
        return Marshal.PtrToStringUTF8((nint)namePtr) ?? string.Empty;
    }

    /// <summary>
    /// Gets the UV coordinate for a white pixel, useful to draw custom shapes via the DrawList API.
    /// </summary>
    /// <returns>The UV coordinate for a white pixel in the font texture.</returns>
    public static Vec2 GetFontTexUvWhitePixel()
    {
        return new(ImGui_GetFontTexUvWhitePixel());
    }
}
