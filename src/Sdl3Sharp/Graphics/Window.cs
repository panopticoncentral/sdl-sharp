using System.Runtime.InteropServices;
using System.Text;
using static Sdl3Sharp.Native.Common;
using static Sdl3Sharp.Native.Keyboard;
using static Sdl3Sharp.Native.Mouse;
using static Sdl3Sharp.Native.Rect;
using static Sdl3Sharp.Native.StdInc;
using static Sdl3Sharp.Native.Video;

namespace Sdl3Sharp.Graphics;

/// <summary>
/// Represents an SDL window.
/// </summary>
public sealed unsafe class Window : IDisposable
{
    private static Dictionary<nint, Func<Window, Point, HitTestResult>> HitTestCallbacks => field ??= [];

    private bool _disposed;

    internal Window(SDL_Window* handle)
    {
        Handle = handle != null ? handle : throw new ArgumentNullException(nameof(handle));
    }

    /// <summary>
    /// Gets a position value indicating that the window position doesn't matter on the primary display.
    /// </summary>
    public static int PositionUndefined => SDL_WINDOWPOS_UNDEFINED;

    /// <summary>
    /// Gets a position value indicating that the window should be centered on the primary display.
    /// </summary>
    public static int PositionCentered => SDL_WINDOWPOS_CENTERED;

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

    /// <summary>
    /// Gets the underlying SDL_Window pointer.
    /// </summary>
    public SDL_Window* Handle { get; private set; }

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
            return Marshal.PtrToStringUTF8((nint)SDL_GetWindowTitle(Handle))!;
        }
        set
        {
            ThrowIfDisposed();
            fixed (byte* titlePtr = Encoding.UTF8.GetBytes(value + '\0'))
            {
                _ = CheckErrorBool(SDL_SetWindowTitle(Handle, titlePtr));
            }
        }
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
            return new(rect);
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
    /// Whether this window has a border.
    /// </summary>
    public bool Bordered
    {
        get
        {
            ThrowIfDisposed();
            return !Flags.HasFlag(WindowFlags.Borderless);
        }
        set
        {
            ThrowIfDisposed();
            _ = CheckErrorBool(SDL_SetWindowBordered(Handle, value));
        }
    }

    /// <summary>
    /// Whether this window is resizable.
    /// </summary>
    public bool Resizable
    {
        get
        {
            ThrowIfDisposed();
            return Flags.HasFlag(WindowFlags.Resizable);
        }
        set
        {
            ThrowIfDisposed();
            _ = CheckErrorBool(SDL_SetWindowResizable(Handle, value));
        }
    }

    /// <summary>
    /// Whether this window should always be on top.
    /// </summary>
    public bool AlwaysOnTop
    {
        get
        {
            ThrowIfDisposed();
            return Flags.HasFlag(WindowFlags.AlwaysOnTop);
        }
        set
        {
            ThrowIfDisposed();
            _ = CheckErrorBool(SDL_SetWindowAlwaysOnTop(Handle, value));
        }
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
            return rect != null ? new Rectangle(*rect) : null;
        }
        set
        {
            ThrowIfDisposed();
            SDL_Rect rect;
            _ = CheckErrorBool(SDL_SetWindowMouseRect(Handle, Rectangle.ToNative(value, &rect)));
        }
    }

    /// <summary>
    /// Gets or sets whether relative mouse mode is enabled for this window.
    /// While relative mouse mode is enabled, the cursor is hidden, the mouse position
    /// is constrained to the window, and SDL will report continuous relative mouse motion.
    /// </summary>
    public bool RelativeMouseMode
    {
        get
        {
            ThrowIfDisposed();
            return SDL_GetWindowRelativeMouseMode(Handle);
        }
        set
        {
            ThrowIfDisposed();
            _ = CheckErrorBool(SDL_SetWindowRelativeMouseMode(Handle, value));
        }
    }

    /// <summary>
    /// Gets whether text input events are enabled for this window.
    /// </summary>
    public bool TextInputActive
    {
        get
        {
            ThrowIfDisposed();
            return SDL_TextInputActive(Handle);
        }
    }

    /// <summary>
    /// Gets whether the screen keyboard is shown for this window.
    /// </summary>
    public bool ScreenKeyboardShown
    {
        get
        {
            ThrowIfDisposed();
            return SDL_ScreenKeyboardShown(Handle);
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
        get
        {
            ThrowIfDisposed();
            return Flags.HasFlag(WindowFlags.Modal);
        }
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
        get
        {
            ThrowIfDisposed();
            return !Flags.HasFlag(WindowFlags.NotFocusable);
        }
        set
        {
            ThrowIfDisposed();
            _ = CheckErrorBool(SDL_SetWindowFocusable(Handle, value));
        }
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
            SDL_DisplayMode mode;
            _ = CheckErrorBool(SDL_SetWindowFullscreenMode(Handle, DisplayMode.ToNative(value, &mode)));
        }
    }

    /// <summary>
    /// Gets the raw ICC profile data for the screen this window is currently on.
    /// </summary>
    public byte[]? ICCProfile
    {
        get
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
    /// Creates a window with the specified dimensions and flags.
    /// </summary>
    /// <param name="title">The title of the window.</param>
    /// <param name="size">The size of the window.</param>
    /// <param name="flags">The window flags.</param>
    public static Window Create(string title, Size size, WindowFlags flags = WindowFlags.None)
    {
        fixed (byte* titlePtr = Encoding.UTF8.GetBytes(title + '\0'))
        {
            return new(CheckErrorPointer(SDL_CreateWindow(titlePtr, size.Width, size.Height, (SDL_WindowFlags)flags)));
        }
    }

    /// <summary>
    /// Creates a window from an existing properties object.
    /// </summary>
    /// <param name="properties">The properties to use.</param>
    public static Window Create(PropertyGroup properties)
    {
        ArgumentNullException.ThrowIfNull(properties);
        return new(CheckErrorPointer(SDL_CreateWindowWithProperties(properties.Id)));
    }

    /// <summary>
    /// Creates a child popup window of the specified parent window.
    /// </summary>
    /// <param name="parent">The parent of the window.</param>
    /// <param name="offset">The position of the popup window relative to the origin of the parent.</param>
    /// <param name="size">The size of the window.</param>
    /// <param name="flags">The window flags (should include Tooltip or PopupMenu).</param>
    public static Window CreatePopup(Window parent, Point offset, Size size, WindowFlags flags)
    {
        ArgumentNullException.ThrowIfNull(parent);
        parent.ThrowIfDisposed();
        return new(CheckErrorPointer(SDL_CreatePopupWindow(parent.Handle, offset.X, offset.Y, size.Width, size.Height, (SDL_WindowFlags)flags)));
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
    /// Checks if a position value represents an undefined position.
    /// </summary>
    /// <param name="position">The position to check.</param>
    /// <returns>True if the position is undefined.</returns>
    public static bool IsPositionUndefined(int position)
    {
        return position == SDL_WINDOWPOS_UNDEFINED;
    }

    /// <summary>
    /// Determines whether the specified window position value represents a centered position.
    /// </summary>
    /// <param name="position">The position to check.</param>
    /// <returns>true if the position value corresponds to a centered window position; otherwise, false.</returns>
    public static bool IsPositionCentered(int position)
    {
        return position == SDL_WINDOWPOS_CENTERED;
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
    /// Sets the icon for this window.
    /// </summary>
    /// <param name="icon">The icon surface.</param>
    public void SetIcon(Surface icon)
    {
        ThrowIfDisposed();
        _ = CheckErrorBool(SDL_SetWindowIcon(Handle, icon.Handle));
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
    /// Blocks until any pending window state is finalized.
    /// </summary>
    public void Sync()
    {
        ThrowIfDisposed();
        _ = CheckErrorBool(SDL_SyncWindow(Handle));
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
            sdlRects[i] = rects[i].Native;
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
    /// Moves the mouse cursor to the given position within this window.
    /// This function generates a mouse motion event if relative mode is not enabled.
    /// </summary>
    /// <param name="position">The position within the window.</param>
    public void WarpMouse(PointF position)
    {
        ThrowIfDisposed();
        SDL_WarpMouseInWindow(Handle, position.X, position.Y);
    }

    /// <summary>
    /// Starts accepting Unicode text input events in this window.
    /// Text input events are not received by default.
    /// On some platforms this shows the screen keyboard and/or activates an IME.
    /// </summary>
    public void StartTextInput()
    {
        ThrowIfDisposed();
        _ = CheckErrorBool(SDL_StartTextInput(Handle));
    }

    /// <summary>
    /// Starts accepting Unicode text input events in this window with properties describing the input.
    /// Text input events are not received by default.
    /// </summary>
    /// <param name="properties">The properties describing the text input.</param>
    public void StartTextInput(PropertyGroup properties)
    {
        ThrowIfDisposed();
        _ = CheckErrorBool(SDL_StartTextInputWithProperties(Handle, properties.Id));
    }

    /// <summary>
    /// Stops receiving text input events in this window.
    /// If StartTextInput showed the screen keyboard, this will hide it.
    /// </summary>
    public void StopTextInput()
    {
        ThrowIfDisposed();
        _ = CheckErrorBool(SDL_StopTextInput(Handle));
    }

    /// <summary>
    /// Dismisses the composition window/IME without disabling the text input subsystem.
    /// </summary>
    public void ClearComposition()
    {
        ThrowIfDisposed();
        _ = CheckErrorBool(SDL_ClearComposition(Handle));
    }

    /// <summary>
    /// Sets the area used to type Unicode text input.
    /// Native input methods may place a window with word suggestions near the cursor.
    /// </summary>
    /// <param name="rect">The rectangle representing the text input area, or null to clear it.</param>
    /// <param name="cursor">The offset of the current cursor location relative to rect.X.</param>
    public void SetTextInputArea(Rectangle? rect, int cursor = 0)
    {
        ThrowIfDisposed();
        if (rect.HasValue)
        {
            SDL_Rect r = new() { x = rect.Value.Location.X, y = rect.Value.Location.Y, w = rect.Value.Size.Width, h = rect.Value.Size.Height };
            _ = CheckErrorBool(SDL_SetTextInputArea(Handle, &r, cursor));
        }
        else
        {
            _ = CheckErrorBool(SDL_SetTextInputArea(Handle, null, cursor));
        }
    }

    /// <summary>
    /// Gets the area used to type Unicode text input.
    /// </summary>
    /// <param name="cursor">Receives the offset of the current cursor location relative to the rect.</param>
    /// <returns>The text input area rectangle.</returns>
    public Rectangle GetTextInputArea(out int cursor)
    {
        ThrowIfDisposed();
        SDL_Rect rect;
        int c;
        _ = CheckErrorBool(SDL_GetTextInputArea(Handle, &rect, &c));
        cursor = c;
        return new Rectangle(new Point(rect.x, rect.y), new Size(rect.w, rect.h));
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
    /// Sets a callback for window hit testing.
    /// </summary>
    /// <param name="callback">The callback function that determines the hit test result for a given point,
    /// or null to remove the current callback.</param>
    /// <remarks>
    /// Hit testing allows applications to implement custom window dragging and resizing behavior.
    /// When the user clicks or drags on the window, the callback is invoked with the mouse position.
    /// The callback returns a <see cref="HitTestResult"/> that determines what action should be taken
    /// (e.g., drag the window, resize from an edge, or treat as a normal click).
    /// </remarks>
    public void SetHitTest(Func<Window, Point, HitTestResult>? callback)
    {
        ThrowIfDisposed();
        if (callback == null)
        {
            _ = SDL_SetWindowHitTest(Handle, null, 0);
            _ = HitTestCallbacks.Remove((nint)Handle);
        }
        else
        {
            _ = CheckErrorBool(SDL_SetWindowHitTest(Handle, &HitTestCallback, 0));
            HitTestCallbacks[(nint)Handle] = callback;
        }
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

    [UnmanagedCallersOnly(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    private static SDL_HitTestResult HitTestCallback(SDL_Window* window, SDL_Point* point, nuint userdata)
    {
        return HitTestCallbacks.TryGetValue((nint)window, out Func<Window, Point, HitTestResult>? callback)
            ? (SDL_HitTestResult)callback(new Window(window), new Point(point->x, point->y))
            : SDL_HitTestResult.SDL_HITTEST_NORMAL;
    }

    private void ThrowIfDisposed()
    {
        ObjectDisposedException.ThrowIf(_disposed, this);
    }

    /// <summary>
    /// Property name constants for use with <see cref="PropertyGroup"/> when creating or querying windows.
    /// </summary>
    public static class PropertyNames
    {
        /// <summary>
        /// Property names for window creation via <see cref="Window.Create(PropertyGroup)"/>.
        /// </summary>
        public static class Create
        {
            /// <summary>
            /// Boolean property: true if the window should be always on top.
            /// </summary>
            public const string AlwaysOnTop = SDL_PROP_WINDOW_CREATE_ALWAYS_ON_TOP_BOOLEAN;

            /// <summary>
            /// Boolean property: true if the window should be borderless.
            /// </summary>
            public const string Borderless = SDL_PROP_WINDOW_CREATE_BORDERLESS_BOOLEAN;

            /// <summary>
            /// Boolean property: true if popup windows should be constrained to the parent window.
            /// </summary>
            public const string ConstrainPopup = SDL_PROP_WINDOW_CREATE_CONSTRAIN_POPUP_BOOLEAN;

            /// <summary>
            /// Boolean property: true if the window should be able to receive input focus.
            /// </summary>
            public const string Focusable = SDL_PROP_WINDOW_CREATE_FOCUSABLE_BOOLEAN;

            /// <summary>
            /// Boolean property: true if the window uses an external graphics context.
            /// </summary>
            public const string ExternalGraphicsContext = SDL_PROP_WINDOW_CREATE_EXTERNAL_GRAPHICS_CONTEXT_BOOLEAN;

            /// <summary>
            /// Number property: the window creation flags.
            /// </summary>
            public const string Flags = SDL_PROP_WINDOW_CREATE_FLAGS_NUMBER;

            /// <summary>
            /// Boolean property: true if the window should be fullscreen.
            /// </summary>
            public const string Fullscreen = SDL_PROP_WINDOW_CREATE_FULLSCREEN_BOOLEAN;

            /// <summary>
            /// Number property: the height of the window.
            /// </summary>
            public const string Height = SDL_PROP_WINDOW_CREATE_HEIGHT_NUMBER;

            /// <summary>
            /// Boolean property: true if the window should be hidden.
            /// </summary>
            public const string Hidden = SDL_PROP_WINDOW_CREATE_HIDDEN_BOOLEAN;

            /// <summary>
            /// Boolean property: true if the window should be created with high pixel density support.
            /// </summary>
            public const string HighPixelDensity = SDL_PROP_WINDOW_CREATE_HIGH_PIXEL_DENSITY_BOOLEAN;

            /// <summary>
            /// Boolean property: true if the window should be maximized.
            /// </summary>
            public const string Maximized = SDL_PROP_WINDOW_CREATE_MAXIMIZED_BOOLEAN;

            /// <summary>
            /// Boolean property: true if the window is a menu window.
            /// </summary>
            public const string Menu = SDL_PROP_WINDOW_CREATE_MENU_BOOLEAN;

            /// <summary>
            /// Boolean property: true if the window should be created with Metal support.
            /// </summary>
            public const string Metal = SDL_PROP_WINDOW_CREATE_METAL_BOOLEAN;

            /// <summary>
            /// Boolean property: true if the window should be minimized.
            /// </summary>
            public const string Minimized = SDL_PROP_WINDOW_CREATE_MINIMIZED_BOOLEAN;

            /// <summary>
            /// Boolean property: true if the window should be modal.
            /// </summary>
            public const string Modal = SDL_PROP_WINDOW_CREATE_MODAL_BOOLEAN;

            /// <summary>
            /// Boolean property: true if the window should grab the mouse.
            /// </summary>
            public const string MouseGrabbed = SDL_PROP_WINDOW_CREATE_MOUSE_GRABBED_BOOLEAN;

            /// <summary>
            /// Boolean property: true if the window should be created with OpenGL support.
            /// </summary>
            public const string OpenGL = SDL_PROP_WINDOW_CREATE_OPENGL_BOOLEAN;

            /// <summary>
            /// Pointer property: the parent window for this window.
            /// </summary>
            public const string Parent = SDL_PROP_WINDOW_CREATE_PARENT_POINTER;

            /// <summary>
            /// Boolean property: true if the window should be resizable.
            /// </summary>
            public const string Resizable = SDL_PROP_WINDOW_CREATE_RESIZABLE_BOOLEAN;

            /// <summary>
            /// String property: the title of the window.
            /// </summary>
            public const string Title = SDL_PROP_WINDOW_CREATE_TITLE_STRING;

            /// <summary>
            /// Boolean property: true if the window should be transparent.
            /// </summary>
            public const string Transparent = SDL_PROP_WINDOW_CREATE_TRANSPARENT_BOOLEAN;

            /// <summary>
            /// Boolean property: true if the window is a tooltip window.
            /// </summary>
            public const string Tooltip = SDL_PROP_WINDOW_CREATE_TOOLTIP_BOOLEAN;

            /// <summary>
            /// Boolean property: true if the window is a utility window.
            /// </summary>
            public const string Utility = SDL_PROP_WINDOW_CREATE_UTILITY_BOOLEAN;

            /// <summary>
            /// Boolean property: true if the window should be created with Vulkan support.
            /// </summary>
            public const string Vulkan = SDL_PROP_WINDOW_CREATE_VULKAN_BOOLEAN;

            /// <summary>
            /// Number property: the width of the window.
            /// </summary>
            public const string Width = SDL_PROP_WINDOW_CREATE_WIDTH_NUMBER;

            /// <summary>
            /// Number property: the x position of the window.
            /// </summary>
            public const string X = SDL_PROP_WINDOW_CREATE_X_NUMBER;

            /// <summary>
            /// Number property: the y position of the window.
            /// </summary>
            public const string Y = SDL_PROP_WINDOW_CREATE_Y_NUMBER;

            /// <summary>
            /// Platform-specific creation properties for macOS (Cocoa).
            /// </summary>
            public static class Cocoa
            {
                /// <summary>
                /// Pointer property: the NSWindow to wrap (to create a window from an existing native window).
                /// </summary>
                public const string Window = SDL_PROP_WINDOW_CREATE_COCOA_WINDOW_POINTER;

                /// <summary>
                /// Pointer property: the NSView to use for rendering.
                /// </summary>
                public const string View = SDL_PROP_WINDOW_CREATE_COCOA_VIEW_POINTER;
            }

            /// <summary>
            /// Platform-specific creation properties for Wayland.
            /// </summary>
            public static class Wayland
            {
                /// <summary>
                /// Boolean property: true if the application is providing its own surface role.
                /// </summary>
                public const string SurfaceRoleCustom = SDL_PROP_WINDOW_CREATE_WAYLAND_SURFACE_ROLE_CUSTOM_BOOLEAN;

                /// <summary>
                /// Boolean property: true to create an EGL window for the surface.
                /// </summary>
                public const string CreateEglWindow = SDL_PROP_WINDOW_CREATE_WAYLAND_CREATE_EGL_WINDOW_BOOLEAN;

                /// <summary>
                /// Pointer property: the wl_surface to use for the window.
                /// </summary>
                public const string WlSurface = SDL_PROP_WINDOW_CREATE_WAYLAND_WL_SURFACE_POINTER;
            }

            /// <summary>
            /// Platform-specific creation properties for Windows (Win32).
            /// </summary>
            public static class Win32
            {
                /// <summary>
                /// Pointer property: the HWND to wrap (to create a window from an existing native window).
                /// </summary>
                public const string Hwnd = SDL_PROP_WINDOW_CREATE_WIN32_HWND_POINTER;

                /// <summary>
                /// Pointer property: the HWND to use for setting pixel format.
                /// </summary>
                public const string PixelFormatHwnd = SDL_PROP_WINDOW_CREATE_WIN32_PIXEL_FORMAT_HWND_POINTER;
            }

            /// <summary>
            /// Platform-specific creation properties for X11.
            /// </summary>
            public static class X11
            {
                /// <summary>
                /// Number property: the X11 Window to wrap (to create a window from an existing native window).
                /// </summary>
                public const string Window = SDL_PROP_WINDOW_CREATE_X11_WINDOW_NUMBER;
            }
        }

        /// <summary>
        /// Pointer property: the surface associated with a shaped window.
        /// </summary>
        public const string Shape = SDL_PROP_WINDOW_SHAPE_POINTER;

        /// <summary>
        /// Boolean property: true if the window has HDR headroom above the SDR white point.
        /// </summary>
        public const string HdrEnabled = SDL_PROP_WINDOW_HDR_ENABLED_BOOLEAN;

        /// <summary>
        /// Float property: the value of SDR white in the SDL_COLORSPACE_SRGB_LINEAR colorspace.
        /// </summary>
        public const string SdrWhiteLevel = SDL_PROP_WINDOW_SDR_WHITE_LEVEL_FLOAT;

        /// <summary>
        /// Float property: the additional high dynamic range that can be displayed, in terms of the SDR white point.
        /// </summary>
        public const string HdrHeadroom = SDL_PROP_WINDOW_HDR_HEADROOM_FLOAT;

        /// <summary>
        /// Platform-specific runtime properties for Android.
        /// </summary>
        public static class Android
        {
            /// <summary>
            /// Pointer property: the ANativeWindow associated with the window.
            /// </summary>
            public const string Window = SDL_PROP_WINDOW_ANDROID_WINDOW_POINTER;

            /// <summary>
            /// Pointer property: the EGLSurface associated with the window.
            /// </summary>
            public const string Surface = SDL_PROP_WINDOW_ANDROID_SURFACE_POINTER;
        }

        /// <summary>
        /// Platform-specific runtime properties for iOS (UIKit).
        /// </summary>
        public static class UIKit
        {
            /// <summary>
            /// Pointer property: the UIWindow associated with the window.
            /// </summary>
            public const string Window = SDL_PROP_WINDOW_UIKIT_WINDOW_POINTER;

            /// <summary>
            /// Number property: the NSInteger tag associated with Metal views.
            /// </summary>
            public const string MetalViewTag = SDL_PROP_WINDOW_UIKIT_METAL_VIEW_TAG_NUMBER;

            /// <summary>
            /// Number property: the OpenGL framebuffer object for the view.
            /// </summary>
            public const string OpenGLFramebuffer = SDL_PROP_WINDOW_UIKIT_OPENGL_FRAMEBUFFER_NUMBER;

            /// <summary>
            /// Number property: the OpenGL renderbuffer object for the view.
            /// </summary>
            public const string OpenGLRenderbuffer = SDL_PROP_WINDOW_UIKIT_OPENGL_RENDERBUFFER_NUMBER;

            /// <summary>
            /// Number property: the OpenGL resolve framebuffer object when using MSAA.
            /// </summary>
            public const string OpenGLResolveFramebuffer = SDL_PROP_WINDOW_UIKIT_OPENGL_RESOLVE_FRAMEBUFFER_NUMBER;
        }

        /// <summary>
        /// Platform-specific runtime properties for KMS/DRM.
        /// </summary>
        public static class KmsDrm
        {
            /// <summary>
            /// Number property: the device index for the window.
            /// </summary>
            public const string DeviceIndex = SDL_PROP_WINDOW_KMSDRM_DEVICE_INDEX_NUMBER;

            /// <summary>
            /// Number property: the DRM file descriptor associated with the window.
            /// </summary>
            public const string DrmFd = SDL_PROP_WINDOW_KMSDRM_DRM_FD_NUMBER;

            /// <summary>
            /// Pointer property: the GBM device for the window.
            /// </summary>
            public const string GbmDevice = SDL_PROP_WINDOW_KMSDRM_GBM_DEVICE_POINTER;
        }

        /// <summary>
        /// Platform-specific runtime properties for macOS (Cocoa).
        /// </summary>
        public static class Cocoa
        {
            /// <summary>
            /// Pointer property: the NSWindow associated with the window.
            /// </summary>
            public const string Window = SDL_PROP_WINDOW_COCOA_WINDOW_POINTER;

            /// <summary>
            /// Number property: the NSInteger tag associated with Metal views.
            /// </summary>
            public const string MetalViewTag = SDL_PROP_WINDOW_COCOA_METAL_VIEW_TAG_NUMBER;
        }

        /// <summary>
        /// Platform-specific runtime properties for OpenVR.
        /// </summary>
        public static class OpenVR
        {
            /// <summary>
            /// Number property: the OpenVR Overlay Handle ID for the associated overlay window.
            /// </summary>
            public const string OverlayId = SDL_PROP_WINDOW_OPENVR_OVERLAY_ID;
        }

        /// <summary>
        /// Platform-specific runtime properties for Vivante.
        /// </summary>
        public static class Vivante
        {
            /// <summary>
            /// Pointer property: the EGLNativeDisplayType for the window.
            /// </summary>
            public const string Display = SDL_PROP_WINDOW_VIVANTE_DISPLAY_POINTER;

            /// <summary>
            /// Pointer property: the EGLNativeWindowType for the window.
            /// </summary>
            public const string Window = SDL_PROP_WINDOW_VIVANTE_WINDOW_POINTER;

            /// <summary>
            /// Pointer property: the EGLSurface associated with the window.
            /// </summary>
            public const string Surface = SDL_PROP_WINDOW_VIVANTE_SURFACE_POINTER;
        }

        /// <summary>
        /// Platform-specific runtime properties for Windows (Win32).
        /// </summary>
        public static class Win32
        {
            /// <summary>
            /// Pointer property: the HWND associated with the window.
            /// </summary>
            public const string Hwnd = SDL_PROP_WINDOW_WIN32_HWND_POINTER;

            /// <summary>
            /// Pointer property: the HDC associated with the window.
            /// </summary>
            public const string Hdc = SDL_PROP_WINDOW_WIN32_HDC_POINTER;

            /// <summary>
            /// Pointer property: the HINSTANCE associated with the window.
            /// </summary>
            public const string Instance = SDL_PROP_WINDOW_WIN32_INSTANCE_POINTER;
        }

        /// <summary>
        /// Platform-specific runtime properties for Wayland.
        /// </summary>
        public static class Wayland
        {
            /// <summary>
            /// Pointer property: the wl_display associated with the window.
            /// </summary>
            public const string Display = SDL_PROP_WINDOW_WAYLAND_DISPLAY_POINTER;

            /// <summary>
            /// Pointer property: the wl_surface associated with the window.
            /// </summary>
            public const string Surface = SDL_PROP_WINDOW_WAYLAND_SURFACE_POINTER;

            /// <summary>
            /// Pointer property: the wp_viewport associated with the window.
            /// </summary>
            public const string Viewport = SDL_PROP_WINDOW_WAYLAND_VIEWPORT_POINTER;

            /// <summary>
            /// Pointer property: the wl_egl_window associated with the window.
            /// </summary>
            public const string EglWindow = SDL_PROP_WINDOW_WAYLAND_EGL_WINDOW_POINTER;

            /// <summary>
            /// Pointer property: the xdg_surface associated with the window.
            /// </summary>
            public const string XdgSurface = SDL_PROP_WINDOW_WAYLAND_XDG_SURFACE_POINTER;

            /// <summary>
            /// Pointer property: the xdg_toplevel role associated with the window.
            /// </summary>
            public const string XdgToplevel = SDL_PROP_WINDOW_WAYLAND_XDG_TOPLEVEL_POINTER;

            /// <summary>
            /// String property: the export handle for the window.
            /// </summary>
            public const string XdgToplevelExportHandle = SDL_PROP_WINDOW_WAYLAND_XDG_TOPLEVEL_EXPORT_HANDLE_STRING;

            /// <summary>
            /// Pointer property: the xdg_popup role associated with the window.
            /// </summary>
            public const string XdgPopup = SDL_PROP_WINDOW_WAYLAND_XDG_POPUP_POINTER;

            /// <summary>
            /// Pointer property: the xdg_positioner used in popup mode.
            /// </summary>
            public const string XdgPositioner = SDL_PROP_WINDOW_WAYLAND_XDG_POSITIONER_POINTER;
        }

        /// <summary>
        /// Platform-specific runtime properties for X11.
        /// </summary>
        public static class X11
        {
            /// <summary>
            /// Pointer property: the X11 Display associated with the window.
            /// </summary>
            public const string Display = SDL_PROP_WINDOW_X11_DISPLAY_POINTER;

            /// <summary>
            /// Number property: the screen number associated with the window.
            /// </summary>
            public const string Screen = SDL_PROP_WINDOW_X11_SCREEN_NUMBER;

            /// <summary>
            /// Number property: the X11 Window associated with the window.
            /// </summary>
            public const string Window = SDL_PROP_WINDOW_X11_WINDOW_NUMBER;
        }
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
            if (HitTestCallbacks.Remove((nint)Handle))
            {
                _ = SDL_SetWindowHitTest(Handle, null, 0);
            }

            SDL_DestroyWindow(Handle);
            Handle = null;
        }

        _disposed = true;
        GC.SuppressFinalize(this);
    }
}
