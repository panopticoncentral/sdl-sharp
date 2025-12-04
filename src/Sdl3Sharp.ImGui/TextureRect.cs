using Sdl3Sharp.ImGui.Native;
using System.Runtime.InteropServices;

namespace Sdl3Sharp.ImGui;

/// <summary>
/// Represents coordinates of a rectangle within a texture.
/// </summary>
/// <remarks>
/// When a texture is in WantUpdates state, a list of individual rectangles is provided
/// to copy to the graphics system.
/// </remarks>
public struct TextureRect
{
    private ImTextureRect _native;

    internal TextureRect(ImTextureRect native)
    {
        _native = native;
    }

    /// <summary>
    /// Creates a new TextureRect with the specified coordinates and dimensions.
    /// </summary>
    /// <param name="x">The X coordinate of the upper-left corner.</param>
    /// <param name="y">The Y coordinate of the upper-left corner.</param>
    /// <param name="width">The width of the rectangle.</param>
    /// <param name="height">The height of the rectangle.</param>
    public TextureRect(ushort x, ushort y, ushort width, ushort height)
    {
        _native = new ImTextureRect
        {
            X = x,
            Y = y,
            W = width,
            H = height
        };
    }

    /// <summary>
    /// The X coordinate of the upper-left corner of the rectangle.
    /// </summary>
    public ushort X
    {
        readonly get => _native.X;
        set => _native.X = value;
    }

    /// <summary>
    /// The Y coordinate of the upper-left corner of the rectangle.
    /// </summary>
    public ushort Y
    {
        readonly get => _native.Y;
        set => _native.Y = value;
    }

    /// <summary>
    /// The width of the rectangle in pixels.
    /// </summary>
    public ushort Width
    {
        readonly get => _native.W;
        set => _native.W = value;
    }

    /// <summary>
    /// The height of the rectangle in pixels.
    /// </summary>
    public ushort Height
    {
        readonly get => _native.H;
        set => _native.H = value;
    }
}