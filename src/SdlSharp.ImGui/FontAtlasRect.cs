using static SdlSharp.ImGui.Native;

namespace SdlSharp.ImGui;

/// <summary>
/// A rectangle packed into the font atlas texture (e.g. a custom rect added via
/// <see cref="FontAtlas.AddCustomRect(int, int, out FontAtlasRect)"/>).
/// Coordinates are in texels; <paramref name="Uv0"/>/<paramref name="Uv1"/> are normalized
/// texture coordinates for rendering. The atlas texture may be resized or repacked between
/// frames, invalidating these values — re-query via <see cref="FontAtlas.TryGetCustomRect"/>
/// each frame rather than caching.
/// </summary>
/// <param name="X">Left edge in texels.</param>
/// <param name="Y">Top edge in texels.</param>
/// <param name="Width">Width in texels.</param>
/// <param name="Height">Height in texels.</param>
/// <param name="Uv0">Top-left UV coordinate.</param>
/// <param name="Uv1">Bottom-right UV coordinate.</param>
public readonly record struct FontAtlasRect(int X, int Y, int Width, int Height, Vec2 Uv0, Vec2 Uv1)
{
    internal static FontAtlasRect FromNative(in IGSharp_FontAtlasRect r) =>
        new(r.X, r.Y, r.W, r.H, new Vec2(r.Uv0.X, r.Uv0.Y), new Vec2(r.Uv1.X, r.Uv1.Y));
}
