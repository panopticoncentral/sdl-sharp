using static Sdl3Sharp.Native.Pixels;

namespace Sdl3Sharp.Graphics;

/// <summary>
/// A float color.
/// </summary>
/// <param name="Red">The red value.</param>
/// <param name="Green">The green value.</param>
/// <param name="Blue">The blue value.</param>
/// <param name="Alpha">The alpha value.</param>
public readonly record struct ColorF(float Red, float Green, float Blue, float Alpha = ColorF.AlphaOpaque)
{
    /// <summary>
    /// The opaque alpha value.
    /// </summary>
    public const float AlphaOpaque = SDL_ALPHA_OPAQUE_FLOAT;

    /// <summary>
    /// The transparent alpha value.
    /// </summary>
    public const float AlphaTransparent = SDL_ALPHA_TRANSPARENT_FLOAT;

    internal ColorF(SDL_FColor color) : this(color.r, color.g, color.b, color.a)
    {
    }

    internal SDL_FColor ToNative()
    {
        return new(Red, Green, Blue, Alpha);
    }
}
