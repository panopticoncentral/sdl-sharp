using SdlSharp.Native;

namespace SdlSharp.Graphics;

/// <summary>
/// A color with floating point RGBA components.
/// </summary>
public readonly record struct FColor(float R, float G, float B, float A)
{
    /// <summary>A fully opaque alpha value (1.0f).</summary>
    public const float Opaque = Native.Pixels.SDL_ALPHA_OPAQUE_FLOAT;
    /// <summary>A fully transparent alpha value (0.0f).</summary>
    public const float Transparent = Native.Pixels.SDL_ALPHA_TRANSPARENT_FLOAT;

    internal SDL_FColor ToNative() => new() { r = R, g = G, b = B, a = A };

    internal static FColor FromNative(SDL_FColor c) => new(c.r, c.g, c.b, c.a);
}
