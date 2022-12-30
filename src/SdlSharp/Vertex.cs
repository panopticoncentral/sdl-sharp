using SdlSharp.Graphics;

namespace SdlSharp
{
    /// <summary>
    /// A vertex.
    /// </summary>
    /// <param name="Position">The vertex position.</param>
    /// <param name="Color">The vertex color.</param>
    /// <param name="TextureCoordinates">The texture coordinates, if any.</param>
    public readonly record struct Vertex(PointF Position, Color Color, PointF TextureCoordinates)
    {
    }
}
