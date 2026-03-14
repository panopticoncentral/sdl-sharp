using SdlSharp.Native;

namespace SdlSharp.Graphics;

/// <summary>
/// A color with 8-bit RGBA components.
/// </summary>
public readonly record struct Color(byte R, byte G, byte B, byte A)
{
    /// <summary>
    /// Creates a fully opaque color.
    /// </summary>
    public Color(byte r, byte g, byte b) : this(r, g, b, 255) { }

    /// <summary>Black (0, 0, 0, 255).</summary>
    public static readonly Color Black = new(0, 0, 0);
    /// <summary>White (255, 255, 255, 255).</summary>
    public static readonly Color White = new(255, 255, 255);
    /// <summary>Red (255, 0, 0, 255).</summary>
    public static readonly Color Red = new(255, 0, 0);
    /// <summary>Green (0, 255, 0, 255).</summary>
    public static readonly Color Green = new(0, 255, 0);
    /// <summary>Blue (0, 0, 255, 255).</summary>
    public static readonly Color Blue = new(0, 0, 255);
    /// <summary>Transparent (0, 0, 0, 0).</summary>
    public static readonly Color Transparent = new(0, 0, 0, 0);

    /// <summary>A fully opaque alpha value (255).</summary>
    public const byte Opaque = Native.Pixels.SDL_ALPHA_OPAQUE;
    /// <summary>A fully transparent alpha value (0).</summary>
    public const byte TransparentAlpha = Native.Pixels.SDL_ALPHA_TRANSPARENT;

    internal SDL_Color ToNative() => new() { r = R, g = G, b = B, a = A };

    internal static Color FromNative(SDL_Color c) => new(c.r, c.g, c.b, c.a);
}
