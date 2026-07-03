using System.Runtime.InteropServices;

using SdlSharp.Native;

namespace SdlSharp.Graphics;

/// <summary>
/// A point using integer coordinates.
/// </summary>
[StructLayout(LayoutKind.Sequential)] // Layout must match SDL_Point (x, y ints): spans of this type are reinterpret-cast to SDL_Point*.
public readonly record struct Point(int X, int Y)
{
    /// <summary>
    /// A point at the origin (0, 0).
    /// </summary>
    public static readonly Point Zero = new(0, 0);

    internal SDL_Point ToNative() => new() { x = X, y = Y };

    internal static Point FromNative(SDL_Point p) => new(p.x, p.y);
}
