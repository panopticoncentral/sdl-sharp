using static Sdl3Sharp.Native.Common;
using static Sdl3Sharp.Native.Pixels;

namespace Sdl3Sharp.Graphics;

/// <summary>
/// A palette of colors.
/// </summary>
public sealed unsafe class Palette : IDisposable
{
    private readonly SDL_Palette* _palette;

    internal Palette(SDL_Palette* palette)
    {
        _palette = palette;
    }

    /// <summary>
    /// Creates a new palette.
    /// </summary>
    /// <param name="colorCount">The number of colors in the palette.</param>
    /// <returns>The palette.</returns>
    public Palette(int colorCount)
    {
        _palette = CheckErrorPointer(SDL_CreatePalette(colorCount));
    }

    /// <inheritdoc/>
    public void Dispose()
    {
        SDL_DestroyPalette(_palette);
    }

    /// <summary>
    /// Sets the colors in the palette.
    /// </summary>
    /// <param name="colors">The colors.</param>
    /// <param name="firstColor">The first color to set in the palette.</param>
    public void SetColors(Color[] colors, int firstColor)
    {
        fixed (Color* ptr = colors)
        {
            _ = CheckErrorBool(SDL_SetPaletteColors(_palette, (SDL_Color*)ptr, firstColor, colors.Length));
        }
    }

    internal SDL_Palette* ToNative()
    {
        return _palette;
    }
}
