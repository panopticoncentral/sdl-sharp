using System.Runtime.InteropServices;
using Sdl3Sharp.ImGui.Native;

namespace Sdl3Sharp.ImGui;

/// <summary>
/// Represents font runtime data and rendering information.
/// </summary>
/// <remarks>
/// Since ImGui 1.92.0, a font may be rendered at any size. Use GetFontBaked(size) to retrieve
/// the FontBaked corresponding to a given size.
/// </remarks>
[StructLayout(LayoutKind.Sequential)]
public unsafe readonly struct Font
{
    private readonly ImFont* _native;

    internal Font(ImFont* native)
    {
        _native = native;
    }

    /// <summary>
    /// Checks whether a glyph exists in this font for the specified character.
    /// </summary>
    /// <param name="c">The Unicode codepoint to check.</param>
    /// <returns>True if the glyph exists; otherwise, false.</returns>
    public readonly bool IsGlyphInFont(ushort c)
    {
        return ImFont.IsGlyphInFont(_native, c);
    }

    /// <summary>
    /// Checks whether this font has been loaded.
    /// </summary>
    /// <returns>True if the font is loaded; otherwise, false.</returns>
    public readonly bool IsLoaded()
    {
        return ImFont.IsLoaded(_native);
    }

    /// <summary>
    /// Gets the debug name of this font (from FontConfig.Name).
    /// </summary>
    /// <returns>The debug name string.</returns>
    public readonly string GetDebugName()
    {
        var namePtr = ImFont.GetDebugName(_native);
        return Marshal.PtrToStringUTF8(namePtr) ?? string.Empty;
    }

    internal ImFont* ToNative()
    {
        return _native;
    }
}
