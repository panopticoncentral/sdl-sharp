using SdlSharp.Native;

namespace SdlSharp.Graphics;

/// <summary>
/// A point using floating point coordinates.
/// </summary>
public readonly record struct FPoint(float X, float Y)
{
    /// <summary>
    /// A point at the origin (0, 0).
    /// </summary>
    public static readonly FPoint Zero = new(0f, 0f);

    internal SDL_FPoint ToNative() => new() { x = X, y = Y };

    internal static FPoint FromNative(SDL_FPoint p) => new(p.x, p.y);
}
