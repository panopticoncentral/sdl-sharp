using SdlSharp.Native;

namespace SdlSharp.Graphics;

/// <summary>
/// A point using integer coordinates.
/// </summary>
public readonly record struct Point(int X, int Y)
{
    /// <summary>
    /// A point at the origin (0, 0).
    /// </summary>
    public static readonly Point Zero = new(0, 0);

    internal SDL_Point ToNative() => new() { x = X, y = Y };

    internal static Point FromNative(SDL_Point p) => new(p.x, p.y);
}
