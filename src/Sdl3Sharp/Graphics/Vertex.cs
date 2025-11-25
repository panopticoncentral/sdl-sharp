using System.Runtime.InteropServices;

namespace Sdl3Sharp.Graphics;

/// <summary>
/// Vertex structure for geometry rendering.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public struct Vertex
{
    /// <summary>
    /// Vertex position, in renderer coordinates.
    /// </summary>
    public PointF Position;

    /// <summary>
    /// Vertex color.
    /// </summary>
    public ColorF Color;

    /// <summary>
    /// Normalized texture coordinates (0.0 to 1.0), if needed.
    /// </summary>
    public PointF TexCoord;

    /// <summary>
    /// Creates a new vertex with the specified position and color.
    /// </summary>
    /// <param name="position">The vertex position.</param>
    /// <param name="color">The vertex color.</param>
    public Vertex(PointF position, ColorF color)
    {
        Position = position;
        Color = color;
        TexCoord = default;
    }

    /// <summary>
    /// Creates a new vertex with the specified position, color, and texture coordinates.
    /// </summary>
    /// <param name="position">The vertex position.</param>
    /// <param name="color">The vertex color.</param>
    /// <param name="texCoord">The texture coordinates.</param>
    public Vertex(PointF position, ColorF color, PointF texCoord)
    {
        Position = position;
        Color = color;
        TexCoord = texCoord;
    }
}
