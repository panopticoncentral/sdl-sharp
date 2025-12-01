using System.Runtime.InteropServices;
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
    /// <summary>
    /// Occurs when a display's orientation has changed.
    /// </summary>
    public static event EventHandler<DisplayOrientationEventArgs>? OrientationChanged;

    /// <summary>
    /// Occurs when a display has been added to the system.
    /// </summary>
    public static event EventHandler<DisplayEventArgs>? Added;

    /// <summary>
    /// Occurs when a display has been removed from the system.
    /// </summary>
    public static event EventHandler<DisplayEventArgs>? Removed;

    /// <summary>
    /// Occurs when a display has been moved.
    /// </summary>
    public static event EventHandler<DisplayEventArgs>? Moved;

    /// <summary>
    /// Occurs when a display's desktop mode has changed.
    /// </summary>
    public static event EventHandler<DisplayEventArgs>? DesktopModeChanged;

    /// <summary>
    /// Occurs when a display's current mode has changed.
    /// </summary>
    public static event EventHandler<DisplayEventArgs>? CurrentModeChanged;

    /// <summary>
    /// Occurs when a display's content scale has changed.
    /// </summary>
    public static event EventHandler<DisplayEventArgs>? ContentScaleChanged;

    internal static void DispatchEvent(Event e)
    {
        switch (e.Type)
        {
            case EventType.DisplayOrientation:
                OrientationChanged?.Invoke(null, (DisplayOrientationEventArgs)e.TranslateEvent());
                break;
            case EventType.DisplayAdded:
                Added?.Invoke(null, (DisplayOrientationEventArgs)e.TranslateEvent());
                break;
            case EventType.DisplayRemoved:
                Removed?.Invoke(null, (DisplayEventArgs)e.TranslateEvent());
                break;
            case EventType.DisplayMoved:
                Moved?.Invoke(null, (DisplayEventArgs)e.TranslateEvent());
                break;
            case EventType.DisplayDesktopModeChanged:
                DesktopModeChanged?.Invoke(null, (DisplayEventArgs)e.TranslateEvent());
                break;
            case EventType.DisplayCurrentModeChanged:
                CurrentModeChanged?.Invoke(null, (DisplayEventArgs)e.TranslateEvent());
                break;
            case EventType.DisplayContentScaleChanged:
                ContentScaleChanged?.Invoke(null, (DisplayEventArgs)e.TranslateEvent());
                break;
        }
    }

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
    public string Name
    {
        get
        {
            var name = CheckErrorPointer(SDL_GetDisplayName(_displayID));
            return Marshal.PtrToStringUTF8((nint)name)!;
        }
    }

    /// <summary>
    /// Gets the desktop area represented by this display.
    /// </summary>
    public Rectangle Bounds
    {
        get
        {
            SDL_Rect rect;
            _ = CheckErrorBool(SDL_GetDisplayBounds(_displayID, &rect));
            return new(rect);
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
            return new(rect);
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
    /// <param name="size">The size in pixels of the desired display mode.</param>
    /// <param name="refreshRate">The refresh rate of the desired display mode, or 0.0f for the desktop refresh rate.</param>
    /// <param name="includeHighDensityModes">Whether to include high density modes in the search.</param>
    /// <returns>The closest matching display mode, or null if no match found.</returns>
    public DisplayMode GetClosestFullscreenMode(Size size, float refreshRate = 0.0f, bool includeHighDensityModes = false)
    {
        SDL_DisplayMode closest;
        _ = CheckErrorBool(SDL_GetClosestFullscreenDisplayMode(_displayID, size.Width, size.Height, refreshRate, includeHighDensityModes, &closest));
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
        return new(CheckErrorZero(SDL_GetDisplayForPoint(&point.Native)));
    }

    /// <summary>
    /// Gets the display primarily containing a rectangle.
    /// </summary>
    /// <param name="rect">The rectangle to query.</param>
    /// <returns>The display entirely containing the rect or closest to the center of the rect.</returns>
    public static Display GetDisplayForRect(Rectangle rect)
    {
        return new(CheckErrorZero(SDL_GetDisplayForRect(&rect.Native)));
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
