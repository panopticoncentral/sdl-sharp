using Sdl3Sharp.ImGui.Native;

namespace Sdl3Sharp.ImGui;

/// <summary>
/// An opaque identifier to a rectangle in the font atlas.
/// </summary>
public readonly struct FontAtlasRectId
{
    internal readonly ImFontAtlasRectId Native { get; }

    /// <summary>
    /// Represents an invalid rectangle ID.
    /// </summary>
    public static readonly FontAtlasRectId Invalid = new(-1);

    /// <summary>
    /// Gets a value indicating whether this ID is valid.
    /// </summary>
    public readonly bool IsValid => Native != -1;

    internal FontAtlasRectId(ImFontAtlasRectId native)
    {
        Native = native;
    }
}
