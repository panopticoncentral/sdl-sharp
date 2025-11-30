using static Sdl3Sharp.Native.Common;
using static Sdl3Sharp.Native.Rect;
using static Sdl3Sharp.Native.StdInc;
using static Sdl3Sharp.Native.Video;

namespace Sdl3Sharp.Graphics;

/// <summary>
/// Represents a physical display/monitor connected to the system.
/// </summary>
public sealed unsafe class Display
{
    private readonly SDL_DisplayID _displayID;

    internal Display(SDL_DisplayID displayID)
    {
        _displayID = displayID;
    }

    /// <summary>
    /// Gets the display ID.
    /// </summary>
    public uint ID => _displayID;

    /// <summary>
    /// Gets the properties associated with this display.
    /// </summary>
    public PropertyGroup Properties => new(SDL_GetDisplayProperties(_displayID), ownsHandle: false);

    /// <summary>
    /// Gets the name of this display.
    /// </summary>
    public string Name => CheckErrorNull(SDL_GetDisplayName(_displayID));

    /// <summary>
    /// Gets the desktop area represented by this display.
    /// </summary>
    public Rectangle Bounds
    {
        get
        {
            SDL_Rect rect;
            _ = CheckErrorBool(SDL_GetDisplayBounds(_displayID, &rect));
            return new(new Point(rect.x, rect.y), new Size(rect.w, rect.h));
        }
    }

    /// <summary>
    /// Gets the usable desktop area represented by this display (excludes areas reserved by the system like taskbars).
    /// </summary>
    public Rectangle UsableBounds
    {
        get
        {
            SDL_Rect rect;
            _ = CheckErrorBool(SDL_GetDisplayUsableBounds(_displayID, &rect));
            return new(new Point(rect.x, rect.y), new Size(rect.w, rect.h));
        }
    }

    /// <summary>
    /// Gets the natural orientation of this display when it is unrotated.
    /// </summary>
    public DisplayOrientation NaturalOrientation
        => (DisplayOrientation)SDL_GetNaturalDisplayOrientation(_displayID);

    /// <summary>
    /// Gets the current orientation of this display.
    /// </summary>
    public DisplayOrientation CurrentOrientation
        => (DisplayOrientation)SDL_GetCurrentDisplayOrientation(_displayID);

    /// <summary>
    /// Gets the content scale of this display.
    /// </summary>
    public float ContentScale => SDL_GetDisplayContentScale(_displayID);

    /// <summary>
    /// Gets a list of fullscreen display modes available on this display.
    /// </summary>
    public DisplayMode[] GetFullscreenModes()
    {
        int count;
        SDL_DisplayMode** modes = CheckErrorPointer(SDL_GetFullscreenDisplayModes(_displayID, &count));

        var result = new DisplayMode[count];
        for (var i = 0; i < count; i++)
        {
            result[i] = new DisplayMode(modes[i]);
        }

        SDL_free(modes);
        return result;
    }

    /// <summary>
    /// Gets the closest match to the requested display mode.
    /// </summary>
    /// <param name="width">The width in pixels of the desired display mode.</param>
    /// <param name="height">The height in pixels of the desired display mode.</param>
    /// <param name="refreshRate">The refresh rate of the desired display mode, or 0.0f for the desktop refresh rate.</param>
    /// <param name="includeHighDensityModes">Whether to include high density modes in the search.</param>
    /// <returns>The closest matching display mode, or null if no match found.</returns>
    public DisplayMode GetClosestFullscreenMode(int width, int height, float refreshRate = 0.0f, bool includeHighDensityModes = false)
    {
        SDL_DisplayMode closest;
        _ = CheckErrorBool(SDL_GetClosestFullscreenDisplayMode(_displayID, width, height, refreshRate, includeHighDensityModes, &closest));
        return new(closest);
    }

    /// <summary>
    /// Gets information about the desktop's display mode.
    /// </summary>
    public DisplayMode DesktopMode => new(CheckErrorPointer(SDL_GetDesktopDisplayMode(_displayID)));

    /// <summary>
    /// Gets information about the current display mode.
    /// </summary>
    public DisplayMode CurrentMode => new(CheckErrorPointer(SDL_GetCurrentDisplayMode(_displayID)));

    /// <summary>
    /// Gets all displays currently connected to the system.
    /// </summary>
    public static Display[] GetDisplays()
    {
        int count;
        SDL_DisplayID* displays = CheckErrorPointer(SDL_GetDisplays(&count));

        var result = new Display[count];
        for (var i = 0; i < count; i++)
        {
            result[i] = new Display(displays[i]);
        }

        SDL_free(displays);
        return result;
    }

    /// <summary>
    /// Gets the primary display.
    /// </summary>
    public static Display Primary => new(CheckErrorZero(SDL_GetPrimaryDisplay()));

    /// <summary>
    /// Gets the display containing a point.
    /// </summary>
    /// <param name="point">The point to query.</param>
    /// <returns>The display containing the point.</returns>
    public static Display GetDisplayForPoint(Point point)
    {
        SDL_Point p = new() { x = point.X, y = point.Y };
        return new(CheckErrorZero(SDL_GetDisplayForPoint(&p)));
    }

    /// <summary>
    /// Gets the display primarily containing a rectangle.
    /// </summary>
    /// <param name="rect">The rectangle to query.</param>
    /// <returns>The display entirely containing the rect or closest to the center of the rect.</returns>
    public static Display GetDisplayForRect(Rectangle rect)
    {
        SDL_Rect r = new() { x = rect.Location.X, y = rect.Location.Y, w = rect.Size.Width, h = rect.Size.Height };
        return new(CheckErrorZero(SDL_GetDisplayForRect(&r)));
    }

    /// <summary>
    /// Gets a position value indicating that the window position doesn't matter on this display.
    /// </summary>
    public int WindowPositionUndefined => SDL_WINDOWPOS_UNDEFINED_DISPLAY(_displayID);

    /// <summary>
    /// Gets a position value indicating that the window should be centered on this display.
    /// </summary>
    public int WindowPositionCentered => SDL_WINDOWPOS_CENTERED_DISPLAY(_displayID);

    /// <summary>
    /// Returns a string representation of this display.
    /// </summary>
    public override string ToString()
    {
        return Name ?? $"Display {ID}";
    }
}
