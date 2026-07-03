using System.Runtime.InteropServices;

using SdlSharp.Native;
using static SdlSharp.Native.Common;
using static SdlSharp.Native.Video;

namespace SdlSharp.Graphics;

/// <summary>
/// Represents an SDL display.
/// </summary>
public readonly record struct Display(uint Id)
{
    /// <summary>
    /// Gets all currently connected displays.
    /// </summary>
    /// <returns>An array of displays.</returns>
    public static unsafe Display[] GetAll()
    {
        var ptr = SDL_GetDisplays(out var count);
        if (ptr == null)
        {
            throw new SdlException();
        }

        try
        {
            var displays = new Display[count];
            for (var i = 0; i < count; i++)
            {
                displays[i] = new Display(ptr[i].Value);
            }

            return displays;
        }
        finally
        {
            SDL_free(ptr);
        }
    }

    /// <summary>
    /// Gets the primary display.
    /// </summary>
    public static Display Primary
    {
        get
        {
            var id = SDL_GetPrimaryDisplay();
            return new Display(CheckId(id.Value));
        }
    }

    /// <summary>
    /// Gets the name of this display.
    /// </summary>
    public unsafe string? Name =>
        Marshal.PtrToStringUTF8((nint)SDL_GetDisplayName(new SDL_DisplayID(Id)));

    /// <summary>
    /// Gets the desktop area represented by this display.
    /// </summary>
    public Rectangle Bounds
    {
        get
        {
            Check(SDL_GetDisplayBounds(new SDL_DisplayID(Id), out var rect));
            return Rectangle.FromNative(rect);
        }
    }

    /// <summary>
    /// Gets the usable desktop area represented by this display.
    /// </summary>
    public Rectangle UsableBounds
    {
        get
        {
            Check(SDL_GetDisplayUsableBounds(new SDL_DisplayID(Id), out var rect));
            return Rectangle.FromNative(rect);
        }
    }

    /// <summary>
    /// Gets the content scale of this display.
    /// </summary>
    public float ContentScale => SDL_GetDisplayContentScale(new SDL_DisplayID(Id));

    /// <summary>
    /// Gets information about the desktop's display mode.
    /// </summary>
    public unsafe DisplayMode DesktopDisplayMode
    {
        get
        {
            var ptr = Check(SDL_GetDesktopDisplayMode(new SDL_DisplayID(Id)));
            return DisplayMode.FromNative(ptr);
        }
    }

    /// <summary>
    /// Gets information about the current display mode.
    /// </summary>
    public unsafe DisplayMode CurrentDisplayMode
    {
        get
        {
            var ptr = Check(SDL_GetCurrentDisplayMode(new SDL_DisplayID(Id)));
            return DisplayMode.FromNative(ptr);
        }
    }

    /// <summary>
    /// Gets the list of fullscreen display modes supported by this display,
    /// sorted from most to least desirable (largest, highest refresh rate,
    /// deepest color first).
    /// </summary>
    /// <returns>An array of fullscreen display modes.</returns>
    public unsafe DisplayMode[] GetFullscreenModes()
    {
        var ptr = SDL_GetFullscreenDisplayModes(new SDL_DisplayID(Id), out var count);
        if (ptr == null)
        {
            throw new SdlException();
        }

        try
        {
            var modes = new DisplayMode[count];
            for (var i = 0; i < count; i++)
            {
                modes[i] = DisplayMode.FromNative(ptr[i]);
            }

            return modes;
        }
        finally
        {
            SDL_free(ptr);
        }
    }

    /// <summary>
    /// Gets the closest match to the requested display mode. Prefer modes
    /// returned by <see cref="GetFullscreenModes"/> when an exact exclusive-
    /// fullscreen match is required.
    /// </summary>
    /// <param name="w">The desired width.</param>
    /// <param name="h">The desired height.</param>
    /// <param name="refreshRate">The desired refresh rate, or 0 for the desktop refresh rate.</param>
    /// <param name="includeHighDensityModes">Whether high pixel density modes should be included in the search.</param>
    /// <returns>The closest matching display mode.</returns>
    public unsafe DisplayMode GetClosestFullscreenMode(int w, int h, float refreshRate = 0f, bool includeHighDensityModes = false)
    {
        Check(SDL_GetClosestFullscreenDisplayMode(new SDL_DisplayID(Id), w, h, refreshRate, includeHighDensityModes, out var closest));
        return DisplayMode.FromNative(&closest);
    }

    /// <summary>
    /// Gets the orientation of this display when it is unrotated.
    /// </summary>
    public DisplayOrientation NaturalOrientation =>
        (DisplayOrientation)SDL_GetNaturalDisplayOrientation(new SDL_DisplayID(Id));

    /// <summary>
    /// Gets the orientation of this display.
    /// </summary>
    public DisplayOrientation Orientation =>
        (DisplayOrientation)SDL_GetCurrentDisplayOrientation(new SDL_DisplayID(Id));

    /// <summary>
    /// Gets the properties associated with this display (see <see cref="DisplayProperties"/>
    /// for the recognized names). The returned group is owned by SDL and must not be disposed.
    /// </summary>
    public PropertyGroup Properties =>
        new(CheckId(SDL_GetDisplayProperties(new SDL_DisplayID(Id))), ownsHandle: false);

    /// <summary>
    /// Gets the display containing the given point, or the closest display if no display
    /// contains the point.
    /// </summary>
    /// <param name="point">The point to query.</param>
    /// <returns>The display containing (or closest to) the point.</returns>
    public static unsafe Display GetForPoint(Point point)
    {
        var native = point.ToNative();
        var id = SDL_GetDisplayForPoint(&native);
        return new Display(CheckId(id.Value));
    }

    /// <summary>
    /// Gets the display that best matches the given rectangle, i.e. the display with the
    /// greatest overlap.
    /// </summary>
    /// <param name="rect">The rectangle to query.</param>
    /// <returns>The display best matching the rectangle.</returns>
    public static unsafe Display GetForRect(Rectangle rect)
    {
        var native = rect.ToNative();
        var id = SDL_GetDisplayForRect(&native);
        return new Display(CheckId(id.Value));
    }
}
