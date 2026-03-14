using SdlSharp.Native;

namespace SdlSharp.Graphics;

/// <summary>
/// Vertex structure for geometry rendering.
/// </summary>
public readonly record struct Vertex(FPoint Position, FColor Color, FPoint TexCoord)
{
    internal SDL_Vertex ToNative() => new()
    {
        position = Position.ToNative(),
        color = Color.ToNative(),
        tex_coord = TexCoord.ToNative()
    };

    internal static Vertex FromNative(SDL_Vertex v) =>
        new(FPoint.FromNative(v.position), FColor.FromNative(v.color), FPoint.FromNative(v.tex_coord));
}
