using Sdl3Sharp.ImGui.Native;

namespace Sdl3Sharp.ImGui;

/// <summary>
/// Represents a rectangle in the font atlas with its position, size, and UV coordinates.
/// </summary>
/// <remarks>
/// These values may not be cached/stored as they are only valid for the current value of atlas.TexRef.
/// </remarks>
public unsafe readonly struct FontAtlasRect
{
    internal readonly ImFontAtlasRect Native { get; }

    /// <summary>
    /// Gets the X position in the texture.
    /// </summary>
    public readonly ushort X => Native.X;

    /// <summary>
    /// Gets the Y position in the texture.
    /// </summary>
    public readonly ushort Y => Native.Y;

    /// <summary>
    /// Gets the width of the rectangle.
    /// </summary>
    public readonly ushort Width => Native.W;

    /// <summary>
    /// Gets the height of the rectangle.
    /// </summary>
    public readonly ushort Height => Native.H;

    /// <summary>
    /// Gets the minimum UV coordinates in the current texture.
    /// </summary>
    public readonly Vec2 Uv0 => new(Native.Uv0);

    /// <summary>
    /// Gets the maximum UV coordinates in the current texture.
    /// </summary>
    public readonly Vec2 Uv1 => new(Native.Uv1);

    internal FontAtlasRect(ImFontAtlasRect native)
    {
        Native = native;
    }
}
