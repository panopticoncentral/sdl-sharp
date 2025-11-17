using static Sdl3Sharp.Native.Pixels;

namespace Sdl3Sharp.Graphics;

/// <summary>
/// A color.
/// </summary>
/// <param name="Red">The red value.</param>
/// <param name="Green">The green value.</param>
/// <param name="Blue">The blue value.</param>
/// <param name="Alpha">The alpha value.</param>
public readonly record struct Color(byte Red, byte Green, byte Blue, byte Alpha = Color.AlphaOpaque)
{
    /// <summary>
    /// The opaque alpha value.
    /// </summary>
    public const byte AlphaOpaque = SDL_ALPHA_OPAQUE;

    /// <summary>
    /// The transparent alpha value.
    /// </summary>
    public const byte AlphaTransparent = SDL_ALPHA_TRANSPARENT;

    internal Color(SDL_Color color) : this(color.r, color.g, color.b, color.a)
    {
    }

    internal SDL_Color ToNative()
    {
        return new(Red, Green, Blue, Alpha);
    }
}
