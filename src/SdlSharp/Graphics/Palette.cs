using static SdlSharp.Native.Common;
using static SdlSharp.Native.Pixels;

namespace SdlSharp.Graphics;

/// <summary>
/// A set of indexed colors.
/// </summary>
public sealed unsafe class Palette : IDisposable
{
    private readonly bool _ownsHandle;

    internal Native.SDL_Palette* Handle { get; private set; }

    internal Palette(Native.SDL_Palette* handle, bool ownsHandle = true)
    {
        Handle = handle;
        _ownsHandle = ownsHandle;
    }

    /// <summary>
    /// Creates a new palette with the specified number of color entries (initialized to white).
    /// </summary>
    public Palette(int colorCount) : this(Check(SDL_CreatePalette(colorCount)))
    {
    }

    /// <summary>
    /// The number of colors in the palette.
    /// </summary>
    public int Count => Handle->ncolors;

    /// <summary>
    /// Gets a color at the specified index.
    /// </summary>
    public Color this[int index]
    {
        get
        {
            ArgumentOutOfRangeException.ThrowIfNegative(index);
            ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(index, Handle->ncolors);
            return Color.FromNative(Handle->colors[index]);
        }
    }

    /// <summary>
    /// Sets a range of colors in the palette.
    /// </summary>
    /// <param name="colors">The colors to set.</param>
    /// <param name="firstColor">The index of the first palette entry to modify.</param>
    public void SetColors(ReadOnlySpan<Color> colors, int firstColor = 0)
    {
        // Color and SDL_Color have the same layout (4 bytes: r, g, b, a)
        fixed (Color* ptr = colors)
        {
            Check(SDL_SetPaletteColors(Handle, (Native.SDL_Color*)ptr, firstColor, colors.Length));
        }
    }

    /// <inheritdoc/>
    public void Dispose()
    {
        if (_ownsHandle && Handle != null)
        {
            SDL_DestroyPalette(Handle);
            Handle = null;
        }
    }
}
