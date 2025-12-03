using System.Runtime.InteropServices;
using Sdl3Sharp.ImGui.Native;

namespace Sdl3Sharp.ImGui;

/// <summary>
/// Represents a rectangle in the font atlas with its position, size, and UV coordinates.
/// </summary>
/// <remarks>
/// These values may not be cached/stored as they are only valid for the current value of atlas.TexRef.
/// </remarks>
[StructLayout(LayoutKind.Sequential)]
public unsafe readonly struct FontAtlasRect
{
    private readonly ImFontAtlasRect _native;

    /// <summary>
    /// Gets the X position in the texture.
    /// </summary>
    public readonly ushort X => _native.X;

    /// <summary>
    /// Gets the Y position in the texture.
    /// </summary>
    public readonly ushort Y => _native.Y;

    /// <summary>
    /// Gets the width of the rectangle.
    /// </summary>
    public readonly ushort Width => _native.W;

    /// <summary>
    /// Gets the height of the rectangle.
    /// </summary>
    public readonly ushort Height => _native.H;

    /// <summary>
    /// Gets the minimum UV coordinates in the current texture.
    /// </summary>
    public readonly Vec2 Uv0 => Vec2.FromNative(_native.Uv0);

    /// <summary>
    /// Gets the maximum UV coordinates in the current texture.
    /// </summary>
    public readonly Vec2 Uv1 => Vec2.FromNative(_native.Uv1);

    /// <summary>
    /// Casts a pointer to a native ImFontAtlasRect to a pointer to a FontAtlasRect.
    /// </summary>
    /// <param name="native">The native ImFontAtlasRect pointer.</param>
    /// <returns>A pointer to a FontAtlasRect.</returns>
    public static FontAtlasRect* FromNative(ImFontAtlasRect* native)
    {
        return (FontAtlasRect*)native;
    }
}
