using Sdl3Sharp.ImGui.Native;

namespace Sdl3Sharp.ImGui;

/// <summary>
/// An opaque identifier to a rectangle in the font atlas.
/// </summary>
/// <remarks>
/// <para>
/// The rectangle may move and UV may be invalidated, use GetCustomRect() to retrieve current values.
/// </para>
/// <para>
/// A value of -1 indicates an invalid ID.
/// </para>
/// </remarks>
/// <param name="value">The underlying rectangle ID value.</param>
public readonly struct FontAtlasRectId(int value)
{
    /// <summary>
    /// Represents an invalid rectangle ID.
    /// </summary>
    public static readonly FontAtlasRectId Invalid = new(-1);

    /// <summary>
    /// Gets the underlying rectangle ID value.
    /// </summary>
    public readonly int Value = value;

    /// <summary>
    /// Gets a value indicating whether this ID is valid.
    /// </summary>
    public readonly bool IsValid => Value != -1;

    /// <summary>
    /// Implicitly converts a FontAtlasRectId to int.
    /// </summary>
    /// <param name="id">The FontAtlasRectId to convert.</param>
    public static implicit operator int(FontAtlasRectId id)
    {
        return id.Value;
    }

    /// <summary>
    /// Implicitly converts an int to FontAtlasRectId.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    public static implicit operator FontAtlasRectId(int value)
    {
        return new(value);
    }

    /// <summary>
    /// Implicitly converts a FontAtlasRectId to ImFontAtlasRectId.
    /// </summary>
    /// <param name="id">The FontAtlasRectId to convert.</param>
    public static implicit operator ImFontAtlasRectId(FontAtlasRectId id)
    {
        return new(id.Value);
    }

    /// <summary>
    /// Implicitly converts an ImFontAtlasRectId to FontAtlasRectId.
    /// </summary>
    /// <param name="id">The ImFontAtlasRectId to convert.</param>
    public static implicit operator FontAtlasRectId(ImFontAtlasRectId id)
    {
        return new(id.Value);
    }
}
