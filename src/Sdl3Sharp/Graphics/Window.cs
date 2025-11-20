using Sdl3Sharp.Native;
using static Sdl3Sharp.Native.Common;
using static Sdl3Sharp.Native.Rect;
using static Sdl3Sharp.Native.StdInc;
using static Sdl3Sharp.Native.Video;

namespace Sdl3Sharp.Graphics;

/// <summary>
/// Represents an SDL window.
/// </summary>
public sealed unsafe class Window : IDisposable
{
    private bool _disposed;

    /// <summary>
    /// Gets the underlying SDL_Window pointer.
    /// </summary>
    public SDL_Window* Handle { get; private set; }

    /// <summary>
    /// Creates a window with the specified dimensions and flags.
    /// </summary>
    /// <param name="title">The title of the window.</param>
    /// <param name="size">The size of the window.</param>
    /// <param name="flags">The window flags.</param>
    public Window(string title, Size size, WindowFlags flags = WindowFlags.None)
    {
        Handle = CheckErrorPointer(SDL_CreateWindow(title, size.Width, size.Height, (SDL_WindowFlags)flags));
    }

    /// <summary>
    /// Creates a child popup window of the specified parent window.
    /// </summary>
    /// <param name="parent">The parent of the window.</param>
    /// <param name="offset">The position of the popup window relative to the origin of the parent.</param>
    /// <param name="size">The size of the window.</param>
    /// <param name="flags">The window flags (should include Tooltip or PopupMenu).</param>
    public Window(Window parent, Point offset, Size size, WindowFlags flags)
    {
        ArgumentNullException.ThrowIfNull(parent);
        parent.ThrowIfDisposed();
        Handle = CheckErrorPointer(SDL_CreatePopupWindow(parent.Handle, offset.X, offset.Y, size.Width, size.Height, (SDL_WindowFlags)flags));
    }

    /// <summary>
    /// Creates a window from an existing properties object.
    /// </summary>
    /// <param name="properties">The properties to use.</param>
    public Window(PropertyGroup properties)
    {
        ArgumentNullException.ThrowIfNull(properties);
        Handle = CheckErrorPointer(SDL_CreateWindowWithProperties(properties.Id));
    }

    internal Window(SDL_Window* handle)
    {
        Handle = handle != null ? handle : throw new ArgumentNullException(nameof(handle));
    }

    /// <summary>
    /// Gets the numeric ID of this window.
    /// </summary>
    public uint ID
    {
        get
        {
            ThrowIfDisposed();
            return CheckErrorZero(SDL_GetWindowID(Handle));
        }
    }

    /// <summary>
    /// Gets the parent of this window, or null if it has no parent.
    /// </summary>
    public Window? Parent
    {
        get
        {
            ThrowIfDisposed();
            SDL_Window* parent = SDL_GetWindowParent(Handle);
            return parent != null ? new Window(parent) : null;
        }
        set
        {
            ThrowIfDisposed();
            _ = CheckErrorBool(SDL_SetWindowParent(Handle, value != null ? value.Handle : null));
        }
    }

    /// <summary>
    /// Gets the properties associated with this window.
    /// </summary>
    public PropertyGroup Properties
    {
        get
        {
            ThrowIfDisposed();
            return new(CheckErrorZero(SDL_GetWindowProperties(Handle)), ownsHandle: false);
        }
    }

    /// <summary>
    /// Gets the window flags.
    /// </summary>
    public WindowFlags Flags
    {
        get
        {
            ThrowIfDisposed();
            return (WindowFlags)SDL_GetWindowFlags(Handle);
        }
    }

    /// <summary>
    /// Gets or sets the title of this window.
    /// </summary>
    public string Title
    {
        get
        {
            ThrowIfDisposed();
            return SDL_GetWindowTitle(Handle);
        }
        set
        {
            ThrowIfDisposed();
            _ = CheckErrorBool(SDL_SetWindowTitle(Handle, value));
        }
    }

    /// <summary>
    /// Sets the icon for this window.
    /// </summary>
    /// <param name="icon">The icon surface.</param>
    public void SetIcon(Surface icon)
    {
        ThrowIfDisposed();
        _ = CheckErrorBool(SDL_SetWindowIcon(Handle, icon.Handle));
    }

    /// <summary>
    /// Gets or sets the position of this window.
    /// </summary>
    public Point Position
    {
        get
        {
            ThrowIfDisposed();
            int x, y;
            _ = CheckErrorBool(SDL_GetWindowPosition(Handle, &x, &y));
            return new Point(x, y);
        }
        set
        {
            ThrowIfDisposed();
            _ = CheckErrorBool(SDL_SetWindowPosition(Handle, value.X, value.Y));
        }
    }

    /// <summary>
    /// Gets or sets the size of this window's client area.
    /// </summary>
    public Size Size
    {
        get
        {
            ThrowIfDisposed();
            int w, h;
            _ = CheckErrorBool(SDL_GetWindowSize(Handle, &w, &h));
            return new Size(w, h);
        }
        set
        {
            ThrowIfDisposed();
            _ = CheckErrorBool(SDL_SetWindowSize(Handle, value.Width, value.Height));
        }
    }

    /// <summary>
    /// Gets the safe area for this window (area safe for interactive content).
    /// </summary>
    public Rectangle SafeArea
    {
        get
        {
            ThrowIfDisposed();
            SDL_Rect rect;
            _ = CheckErrorBool(SDL_GetWindowSafeArea(Handle, &rect));
            return new(new Point(rect.x, rect.y), new Size(rect.w, rect.h));
        }
    }

    /// <summary>
    /// Gets or sets the aspect ratio of this window's client area.
    /// </summary>
    /// <value>A tuple containing the minimum and maximum aspect ratios. Use 0.0f for no limit.</value>
    public (float Min, float Max) AspectRatio
    {
        get
        {
            ThrowIfDisposed();
            float min, max;
            _ = CheckErrorBool(SDL_GetWindowAspectRatio(Handle, &min, &max));
            return (min, max);
        }
        set
        {
            ThrowIfDisposed();
            _ = CheckErrorBool(SDL_SetWindowAspectRatio(Handle, value.Min, value.Max));
        }
    }

    /// <summary>
    /// Gets the size of this window's borders (decorations) around the client area.
    /// </summary>
    public (int Top, int Left, int Bottom, int Right) BordersSize
    {
        get
        {
            ThrowIfDisposed();
            int top, left, bottom, right;
            _ = CheckErrorBool(SDL_GetWindowBordersSize(Handle, &top, &left, &bottom, &right));
            return (top, left, bottom, right);
        }
    }

    /// <summary>
    /// Gets the size of this window's client area in pixels.
    /// </summary>
    public Size SizeInPixels
    {
        get
        {
            ThrowIfDisposed();
            int w, h;
            _ = CheckErrorBool(SDL_GetWindowSizeInPixels(Handle, &w, &h));
            return new Size(w, h);
        }
    }

    /// <summary>
    /// Gets or sets the minimum size of this window's client area.
    /// </summary>
    public Size MinimumSize
    {
        get
        {
            ThrowIfDisposed();
            int w, h;
            _ = CheckErrorBool(SDL_GetWindowMinimumSize(Handle, &w, &h));
            return new Size(w, h);
        }
        set
        {
            ThrowIfDisposed();
            _ = CheckErrorBool(SDL_SetWindowMinimumSize(Handle, value.Width, value.Height));
        }
    }

    /// <summary>
    /// Gets or sets the maximum size of this window's client area.
    /// </summary>
    public Size MaximumSize
    {
        get
        {
            ThrowIfDisposed();
            int w, h;
            _ = CheckErrorBool(SDL_GetWindowMaximumSize(Handle, &w, &h));
            return new Size(w, h);
        }
        set
        {
            ThrowIfDisposed();
            _ = CheckErrorBool(SDL_SetWindowMaximumSize(Handle, value.Width, value.Height));
        }
    }

    /// <summary>
    /// Sets whether this window has a border.
    /// </summary>
    /// <param name="bordered">True to add border, false to remove border.</param>
    public void SetBordered(bool bordered)
    {
        ThrowIfDisposed();
        _ = CheckErrorBool(SDL_SetWindowBordered(Handle, bordered));
    }

    /// <summary>
    /// Sets whether this window is resizable.
    /// </summary>
    /// <param name="resizable">True to allow resizing, false to disallow.</param>
    public void SetResizable(bool resizable)
    {
        ThrowIfDisposed();
        _ = CheckErrorBool(SDL_SetWindowResizable(Handle, resizable));
    }

    /// <summary>
    /// Sets whether this window should always be on top.
    /// </summary>
    /// <param name="onTop">True to set always on top, false otherwise.</param>
    public void SetAlwaysOnTop(bool onTop)
    {
        ThrowIfDisposed();
        _ = CheckErrorBool(SDL_SetWindowAlwaysOnTop(Handle, onTop));
    }

    /// <summary>
    /// Shows this window.
    /// </summary>
    public void Show()
    {
        ThrowIfDisposed();
        _ = CheckErrorBool(SDL_ShowWindow(Handle));
    }

    /// <summary>
    /// Hides this window.
    /// </summary>
    public void Hide()
    {
        ThrowIfDisposed();
        _ = CheckErrorBool(SDL_HideWindow(Handle));
    }

    /// <summary>
    /// Raises this window above other windows and gains the input focus.
    /// </summary>
    public void Raise()
    {
        ThrowIfDisposed();
        _ = CheckErrorBool(SDL_RaiseWindow(Handle));
    }

    /// <summary>
    /// Makes this window as large as possible.
    /// </summary>
    public void Maximize()
    {
        ThrowIfDisposed();
        _ = CheckErrorBool(SDL_MaximizeWindow(Handle));
    }

    /// <summary>
    /// Minimizes this window to an iconic representation.
    /// </summary>
    public void Minimize()
    {
        ThrowIfDisposed();
        _ = CheckErrorBool(SDL_MinimizeWindow(Handle));
    }

    /// <summary>
    /// Restores the size and position of a minimized or maximized window.
    /// </summary>
    public void Restore()
    {
        ThrowIfDisposed();
        _ = CheckErrorBool(SDL_RestoreWindow(Handle));
    }

    /// <summary>
    /// Gets or sets whether this window is fullscreen.
    /// </summary>
    public bool Fullscreen
    {
        get
        {
            ThrowIfDisposed();
            return Flags.HasFlag(WindowFlags.Fullscreen);
        }
        set
        {
            ThrowIfDisposed();
            _ = CheckErrorBool(SDL_SetWindowFullscreen(Handle, value));
        }
    }

    /// <summary>
    /// Blocks until any pending window state is finalized.
    /// </summary>
    public void Sync()
    {
        ThrowIfDisposed();
        _ = CheckErrorBool(SDL_SyncWindow(Handle));
    }

    /// <summary>
    /// Gets whether this window has a surface associated with it.
    /// </summary>
    public bool HasSurface
    {
        get
        {
            ThrowIfDisposed();
            return SDL_WindowHasSurface(Handle);
        }
    }

    /// <summary>
    /// Gets the SDL surface associated with this window.
    /// </summary>
    /// <returns>The surface.</returns>
    public Surface GetSurface()
    {
        ThrowIfDisposed();
        return new(CheckErrorPointer(SDL_GetWindowSurface(Handle)), false);
    }

    /// <summary>
    /// Sets or gets the VSync mode for the window surface.
    /// </summary>
    public int SurfaceVSync
    {
        get
        {
            ThrowIfDisposed();
            int vsync;
            _ = CheckErrorBool(SDL_GetWindowSurfaceVSync(Handle, &vsync));
            return vsync;
        }
        set
        {
            ThrowIfDisposed();
            _ = CheckErrorBool(SDL_SetWindowSurfaceVSync(Handle, value));
        }
    }

    /// <summary>
    /// Copies the window surface to the screen.
    /// </summary>
    public void UpdateSurface()
    {
        ThrowIfDisposed();
        _ = CheckErrorBool(SDL_UpdateWindowSurface(Handle));
    }

    /// <summary>
    /// Copies areas of the window surface to the screen.
    /// </summary>
    /// <param name="rects">The rectangles to update.</param>
    public void UpdateSurfaceRects(ReadOnlySpan<Rectangle> rects)
    {
        ThrowIfDisposed();
        if (rects.Length == 0)
        {
            return;
        }

        SDL_Rect* sdlRects = stackalloc SDL_Rect[rects.Length];
        for (var i = 0; i < rects.Length; i++)
        {
            sdlRects[i] = new SDL_Rect { x = rects[i].Location.X, y = rects[i].Location.Y, w = rects[i].Size.Width, h = rects[i].Size.Height };
        }

        _ = CheckErrorBool(SDL_UpdateWindowSurfaceRects(Handle, sdlRects, rects.Length));
    }

    /// <summary>
    /// Destroys the surface associated with this window.
    /// </summary>
    public void DestroySurface()
    {
        ThrowIfDisposed();
        _ = CheckErrorBool(SDL_DestroyWindowSurface(Handle));
    }

    /// <summary>
    /// Gets or sets whether keyboard input is grabbed.
    /// </summary>
    public bool KeyboardGrabbed
    {
        get
        {
            ThrowIfDisposed();
            return SDL_GetWindowKeyboardGrab(Handle);
        }
        set
        {
            ThrowIfDisposed();
            _ = CheckErrorBool(SDL_SetWindowKeyboardGrab(Handle, value));
        }
    }

    /// <summary>
    /// Gets or sets whether mouse input is grabbed.
    /// </summary>
    public bool MouseGrabbed
    {
        get
        {
            ThrowIfDisposed();
            return SDL_GetWindowMouseGrab(Handle);
        }
        set
        {
            ThrowIfDisposed();
            _ = CheckErrorBool(SDL_SetWindowMouseGrab(Handle, value));
        }
    }

    /// <summary>
    /// The mouse confinement rectangle.
    /// </summary>
    public Rectangle? MouseRect
    {
        get
        {
            ThrowIfDisposed();
            SDL_Rect* rect = SDL_GetWindowMouseRect(Handle);
            return rect != null ? new Rectangle(new Point(rect->x, rect->y), new Size(rect->w, rect->h)) : null;
        }
        set
        {
            ThrowIfDisposed();
            if (value.HasValue)
            {
                SDL_Rect r = new() { x = value.Value.Location.X, y = value.Value.Location.Y, w = value.Value.Size.Width, h = value.Value.Size.Height };
                _ = CheckErrorBool(SDL_SetWindowMouseRect(Handle, &r));
            }
            else
            {
                _ = CheckErrorBool(SDL_SetWindowMouseRect(Handle, null));
            }
        }

    }

    /// <summary>
    /// Gets or sets the opacity of this window.
    /// </summary>
    public float Opacity
    {
        get
        {
            ThrowIfDisposed();
            return CheckErrorNegativeOne(SDL_GetWindowOpacity(Handle));
        }
        set
        {
            ThrowIfDisposed();
            _ = CheckErrorBool(SDL_SetWindowOpacity(Handle, value));
        }
    }

    /// <summary>
    /// Gets or sets whether this window is modal.
    /// </summary>
    public bool Modal
    {
        set
        {
            ThrowIfDisposed();
            _ = CheckErrorBool(SDL_SetWindowModal(Handle, value));
        }
    }

    /// <summary>
    /// Gets or sets whether this window may have input focus.
    /// </summary>
    public bool Focusable
    {
        set
        {
            ThrowIfDisposed();
            _ = CheckErrorBool(SDL_SetWindowFocusable(Handle, value));
        }
    }

    /// <summary>
    /// Displays the system-level window menu.
    /// </summary>
    /// <param name="point">The coordinate of the menu, relative to the client area.</param>
    public void ShowSystemMenu(Point point)
    {
        ThrowIfDisposed();
        _ = CheckErrorBool(SDL_ShowWindowSystemMenu(Handle, point.X, point.Y));
    }

    /// <summary>
    /// Sets the shape of this transparent window.
    /// </summary>
    /// <param name="shape">The surface representing the shape, or null to remove the current shape.</param>
    public void SetShape(Surface? shape)
    {
        ThrowIfDisposed();
        _ = CheckErrorBool(SDL_SetWindowShape(Handle, shape != null ? shape.Handle : null));
    }

    /// <summary>
    /// Requests that the window demand attention from the user.
    /// </summary>
    /// <param name="operation">The flash operation to perform.</param>
    public void Flash(FlashOperation operation)
    {
        ThrowIfDisposed();
        _ = CheckErrorBool(SDL_FlashWindow(Handle, (SDL_FlashOperation)operation));
    }

    /// <summary>
    /// Gets the pixel density of this window.
    /// </summary>
    public float PixelDensity
    {
        get
        {
            ThrowIfDisposed();
            return CheckErrorZero(SDL_GetWindowPixelDensity(Handle));
        }
    }

    /// <summary>
    /// Gets the content display scale relative to this window's pixel size.
    /// </summary>
    public float DisplayScale
    {
        get
        {
            ThrowIfDisposed();
            return CheckErrorZero(SDL_GetWindowDisplayScale(Handle));
        }
    }

    /// <summary>
    /// Gets or sets the display mode to use when this window is visible and fullscreen.
    /// </summary>
    public DisplayMode? FullscreenMode
    {
        get
        {
            ThrowIfDisposed();
            SDL_DisplayMode* mode = SDL_GetWindowFullscreenMode(Handle);
            return mode != null ? new DisplayMode(mode) : null;
        }
        set
        {
            ThrowIfDisposed();
            if (value != null)
            {
                SDL_DisplayMode mode = value.ToNative();
                _ = CheckErrorBool(SDL_SetWindowFullscreenMode(Handle, &mode));
            }
            else
            {
                _ = CheckErrorBool(SDL_SetWindowFullscreenMode(Handle, null));
            }
        }
    }

    /// <summary>
    /// Gets the raw ICC profile data for the screen this window is currently on.
    /// </summary>
    public byte[]? GetICCProfile()
    {
        ThrowIfDisposed();
        nuint size;
        var data = CheckErrorPointer(SDL_GetWindowICCProfile(Handle, &size));
        if (size == 0)
        {
            return null;
        }

        var result = new byte[size];
        new Span<byte>(data, (int)size).CopyTo(result);
        SDL_free(data);
        return result;
    }

    /// <summary>
    /// Gets the pixel format associated with this window.
    /// </summary>
    public PixelFormat PixelFormat
    {
        get
        {
            ThrowIfDisposed();
            return new(SDL_GetWindowPixelFormat(Handle));
        }
    }

    /// <summary>
    /// Gets the display associated with this window.
    /// </summary>
    public Display Display
    {
        get
        {
            ThrowIfDisposed();
            return new(CheckErrorZero(SDL_GetDisplayForWindow(Handle)));
        }
    }

    /// <summary>
    /// Gets all valid windows.
    /// </summary>
    public static Window[] GetWindows()
    {
        int count;
        SDL_Window** windows = CheckErrorPointer(SDL_GetWindows(&count));
        var result = new Window[count];
        for (var i = 0; i < count; i++)
        {
            result[i] = new Window(windows[i]);
        }

        SDL_free(windows);
        return result;
    }

    /// <summary>
    /// Gets a window from a stored ID.
    /// </summary>
    /// <param name="id">The window ID.</param>
    /// <returns>The window, or null if it doesn't exist.</returns>
    public static Window? FromID(uint id)
    {
        SDL_Window* window = SDL_GetWindowFromID(id);
        return window != null ? new Window(window) : null;
    }

    /// <summary>
    /// Gets the window that currently has an input grab enabled.
    /// </summary>
    public static Window? GrabbedWindow
    {
        get
        {
            SDL_Window* window = SDL_GetGrabbedWindow();
            return window != null ? new Window(window) : null;
        }
    }

    private void ThrowIfDisposed()
    {
        ObjectDisposedException.ThrowIf(_disposed, this);
    }

    /// <summary>
    /// Disposes this window.
    /// </summary>
    public void Dispose()
    {
        if (_disposed)
        {
            return;
        }

        if (Handle != null)
        {
            SDL_DestroyWindow(Handle);
            Handle = null;
        }

        _disposed = true;
        GC.SuppressFinalize(this);
    }
}
