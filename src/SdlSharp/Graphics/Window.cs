using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SdlSharp.Graphics
{
    /// <summary>
    /// A SDL window.
    /// </summary>
    public sealed unsafe class Window : IDisposable
    {
        private static Dictionary<nint, Func<Window, Point, HitTestResult>>? s_hitTestCallbacks;

        private readonly Native.SDL_Window* _window;

        /// <summary>
        /// A window position that is undefined.
        /// </summary>
        public const int UndefinedWindowPosition = (int)Native.SDL_WINDOWPOS_UNDEFINED_MASK;

        /// <summary>
        /// An undefined window location.
        /// </summary>
        public static readonly Point UndefinedWindowLocation = new(UndefinedWindowPosition, UndefinedWindowPosition);

        /// <summary>
        /// A window position that is centered.
        /// </summary>
        public const int CenteredWindowPosition = (int)Native.SDL_WINDOWPOS_CENTERED_MASK;

        private static Dictionary<nint, Func<Window, Point, HitTestResult>> HitTestCallbacks => s_hitTestCallbacks ??= new();

        /// <summary>
        /// Whether a screen saver is enabled.
        /// </summary>
        public static bool ScreensaverEnabled
        {
            get => Native.SDL_IsScreenSaverEnabled();
            set
            {
                if (value)
                {
                    Native.SDL_EnableScreenSaver();
                }
                else
                {
                    Native.SDL_DisableScreenSaver();
                }
            }
        }

        /// <summary>
        /// The current grabbed window, if any.
        /// </summary>
        public static Window? GrabbedWindow
        {
            get
            {
                var windowPointer = Native.SDL_GetGrabbedWindow();
                return windowPointer == null ? null : new(windowPointer);
            }
        }

        /// <summary>
        /// The ID of the window.
        /// </summary>
        public nuint Id => (nuint)_window;

        /// <summary>
        /// User defined window data.
        /// </summary>
        public WindowData Data => new(this);

        /// <summary>
        /// The display this window is on.
        /// </summary>
        public Display Display => new(Native.CheckError(Native.SDL_GetWindowDisplayIndex(_window)));

        /// <summary>
        /// The display mode of the display the window is on.
        /// </summary>
        public DisplayMode DisplayMode
        {
            get
            {
                Native.SDL_DisplayMode mode;
                _ = Native.CheckError(Native.SDL_GetWindowDisplayMode(_window, &mode));
                return new(mode);
            }

            set
            {
                var mode = value.ToNative();
                _ = Native.CheckError(Native.SDL_SetWindowDisplayMode(_window, &mode));
            }
        }

        /// <summary>
        /// The pixel format of the window.
        /// </summary>
        public EnumeratedPixelFormat PixelFormat => new(Native.SDL_GetWindowPixelFormat(_window));

        /// <summary>
        /// The window flags.
        /// </summary>
        public WindowOptions Flags => (WindowOptions)Native.SDL_GetWindowFlags(_window);

        /// <summary>
        /// The title of the window.
        /// </summary>
        public string? Title
        {
            get => Native.Utf8ToString(Native.SDL_GetWindowTitle(_window));
            set
            {
                fixed (byte* ptr = Native.StringToUtf8(value))
                {
                    Native.SDL_SetWindowTitle(_window, ptr);
                }
            }
        }

        /// <summary>
        /// The position of the window.
        /// </summary>
        public Point Position
        {
            get
            {
                int x, y;
                Native.SDL_GetWindowPosition(_window, &x, &y);
                return (x, y);
            }
            set => Native.SDL_SetWindowPosition(_window, value.X, value.Y);
        }

        /// <summary>
        /// The size of the window.
        /// </summary>
        public Size Size
        {
            get
            {
                int width, height;
                Native.SDL_GetWindowSize(_window, &width, &height);
                return (width, height);
            }
            set => Native.SDL_SetWindowSize(_window, value.Width, value.Height);
        }

        /// <summary>
        /// The size of the window in pixels.
        /// </summary>
        public Size PixelSize
        {
            get
            {
                int width, height;
                Native.SDL_GetWindowSizeInPixels(_window, &width, &height);
                return (width, height);
            }
        }

        /// <summary>
        /// The size of the window borders.
        /// </summary>
        public (int Top, int Left, int Bottom, int Right) BordersSize
        {
            get
            {
                int top, left, bottom, right;
                _ = Native.CheckError(Native.SDL_GetWindowBordersSize(_window, &top, &left, &bottom, &right));
                return (top, left, bottom, right);
            }
        }

        /// <summary>
        /// The minimum size of the window.
        /// </summary>
        public Size MinimumSize
        {
            get
            {
                int width, height;
                Native.SDL_GetWindowMinimumSize(_window, &width, &height);
                return (width, height);
            }
            set => Native.SDL_SetWindowMinimumSize(_window, value.Width, value.Height);
        }

        /// <summary>
        /// The maximum size of the window.
        /// </summary>
        public Size MaximumSize
        {
            get
            {
                int width, height;
                Native.SDL_GetWindowMaximumSize(_window, &width, &height);
                return (width, height);
            }
            set => Native.SDL_SetWindowMaximumSize(_window, value.Width, value.Height);
        }

        /// <summary>
        /// The window's surface.
        /// </summary>
        public Surface Surface => new(Native.SDL_GetWindowSurface(_window));

        /// <summary>
        /// Whether the window has been grabbed.
        /// </summary>
        public bool Grabbed
        {
            get => Native.SDL_GetWindowGrab(_window);
            set => Native.SDL_SetWindowGrab(_window, value);
        }

        /// <summary>
        /// Whether the window has grabbed the keyboard.
        /// </summary>
        public bool KeyboardGrabbed
        {
            get => Native.SDL_GetWindowKeyboardGrab(_window);
            set => Native.SDL_SetWindowKeyboardGrab(_window, value);
        }

        /// <summary>
        /// Whether the window has grabbed the mouse.
        /// </summary>
        public bool MouseGrabbed
        {
            get => Native.SDL_GetWindowMouseGrab(_window);
            set => Native.SDL_SetWindowMouseGrab(_window, value);
        }

        /// <summary>
        /// The mouse confinement rectangle.
        /// </summary>
        public Rectangle? MouseRectangle
        {
            get
            {
                var rect = Native.SDL_GetWindowMouseRect(_window);
                return rect == null ? null : new Rectangle(*rect);
            }
            set
            {
                var rect = value;
                _ = Native.CheckError(Native.SDL_SetWindowMouseRect(_window, (Native.SDL_Rect*)&rect));
            }
        }

        /// <summary>
        /// The brightness of the window.
        /// </summary>
        public float Brightness
        {
            get => Native.SDL_GetWindowBrightness(_window);
            set => Native.CheckError(Native.SDL_SetWindowBrightness(_window, value));
        }

        /// <summary>
        /// The opacity of the window.
        /// </summary>
        public float Opacity
        {
            get
            {
                float value;
                _ = Native.CheckError(Native.SDL_GetWindowOpacity(_window, &value));
                return value;
            }
            set => Native.CheckError(Native.SDL_SetWindowOpacity(_window, value));
        }

        /// <summary>
        /// The gamma ramp of the window.
        /// </summary>
        public (ushort[] Red, ushort[] Green, ushort[] Blue) GammaRamp
        {
            get
            {
                var red = new ushort[256];
                var green = new ushort[256];
                var blue = new ushort[256];

                fixed (ushort* redPtr = red)
                fixed (ushort* greenPtr = green)
                fixed (ushort* bluePtr = blue)
                {
                    _ = Native.CheckError(Native.SDL_GetWindowGammaRamp(_window, redPtr, greenPtr, bluePtr));
                    return (red, green, blue);
                }
            }
            set
            {
                fixed (ushort* redPtr = value.Red)
                fixed (ushort* greenPtr = value.Green)
                fixed (ushort* bluePtr = value.Blue)
                {
                    _ = Native.CheckError(Native.SDL_SetWindowGammaRamp(_window, redPtr, greenPtr, bluePtr));
                }
            }
        }

        /// <summary>
        /// Whether the screen keyboard is being shown for this window.
        /// </summary>
        public bool ScreenKeyboardShown =>
            Native.SDL_IsScreenKeyboardShown(_window);

        /// <summary>
        /// Gets the renderer for this window, if any.
        /// </summary>
        public Renderer? Renderer =>
            new(Native.SDL_GetRenderer(_window));

        /// <summary>
        /// Whether the window is shaped.
        /// </summary>
        public bool IsShaped =>
            Native.SDL_IsShapedWindow(_window);

        /// <summary>
        /// Gets the window's shape mode.
        /// </summary>
        /// <returns></returns>
        public WindowShapeMode? ShapeMode
        {
            get
            {
                Native.SDL_WindowShapeMode mode;
                var result = Native.SDL_GetShapedWindowMode(_window, &mode);

                if (result is Native.SDL_NONSHAPEABLE_WINDOW or Native.SDL_WINDOW_LACKS_SHAPE)
                {
                    return null;
                }

                _ = Native.CheckError(result);
                return WindowShapeMode.FromNative(mode);
            }
        }

        /// <summary>
        /// An event that's fired when the window is shown.
        /// </summary>
        public static event EventHandler<SdlEventArgs>? Shown;

        /// <summary>
        /// An event that's fired when the window is hidden.
        /// </summary>
        public static event EventHandler<SdlEventArgs>? Hidden;

        /// <summary>
        /// An event that's fired when the window is exposed.
        /// </summary>
        public static event EventHandler<SdlEventArgs>? Exposed;

        /// <summary>
        /// An event that's fired when the window is minimized.
        /// </summary>
        public static event EventHandler<SdlEventArgs>? Minimized;

        /// <summary>
        /// An event that's fired when the window is maximized.
        /// </summary>
        public static event EventHandler<SdlEventArgs>? Maximized;

        /// <summary>
        /// An event that's fired when the window is restored.
        /// </summary>
        public static event EventHandler<SdlEventArgs>? Restored;

        /// <summary>
        /// An event that's fired when the window is entered.
        /// </summary>
        public static event EventHandler<SdlEventArgs>? Entered;

        /// <summary>
        /// An event that's fired when the window is left.
        /// </summary>
        public static event EventHandler<SdlEventArgs>? Left;

        /// <summary>
        /// An event that's fired when the window gains focus.
        /// </summary>
        public static event EventHandler<SdlEventArgs>? FocusGained;

        /// <summary>
        /// An event that's fired when the window loses focus.
        /// </summary>
        public static event EventHandler<SdlEventArgs>? FocusLost;

        /// <summary>
        /// An event that's fired when the window is closed.
        /// </summary>
        public static event EventHandler<SdlEventArgs>? Closed;

        /// <summary>
        /// An event that's fired when the window takes focus.
        /// </summary>
        public static event EventHandler<SdlEventArgs>? TookFocus;

        /// <summary>
        /// An event that's fired when the window has a hit test.
        /// </summary>
        public static event EventHandler<SdlEventArgs>? HitTest;

        /// <summary>
        /// An event that's fired when the window is moved.
        /// </summary>
        public static event EventHandler<LocationEventArgs>? Moved;

        /// <summary>
        /// An event that's fired when the window is resized.
        /// </summary>
        public static event EventHandler<SizeEventArgs>? Resized;

        /// <summary>
        /// An event that's fired when the window's size changes.
        /// </summary>
        public static event EventHandler<SizeEventArgs>? SizeChanged;

        /// <summary>
        /// An event that's fired when the window's ICC profile changes.
        /// </summary>
        public static event EventHandler<SdlEventArgs>? IccProfileChanged;

        /// <summary>
        /// An event that's fired when the window's display changes.
        /// </summary>
        public static event EventHandler<DisplayEventArgs>? DisplayChanged;

        internal Window(Native.SDL_Window* window)
        {
            _window = window;
        }

        internal Window(uint windowId) : this(Native.CheckPointer(Native.SDL_GetWindowFromID(windowId)))
        {
        }

        /// <summary>
        /// Creates a new window.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <param name="rectangle">The dimensions.</param>
        /// <param name="flags">Window flags.</param>
        /// <returns></returns>
        public static Window Create(string title, Rectangle rectangle, WindowOptions flags)
        {
            fixed (byte* ptr = Native.StringToUtf8(title))
            {
                return new(Native.SDL_CreateWindow(ptr, rectangle.Location.X, rectangle.Location.Y, rectangle.Size.Width, rectangle.Size.Height, (uint)flags));
            }
        }

        /// <summary>
        /// Creates a new window.
        /// </summary>
        /// <param name="size">The size of the window.</param>
        /// <param name="flags">The flags.</param>
        /// <param name="renderer">The window's renderer.</param>
        /// <returns></returns>
        public static Window Create(Size size, WindowOptions flags, out Renderer renderer)
        {
            Native.SDL_Window* windowPointer;
            Native.SDL_Renderer* rendererPointer;
            _ = Native.CheckError(Native.SDL_CreateWindowAndRenderer(size.Width, size.Height, (uint)flags, &windowPointer, &rendererPointer));
            renderer = new(rendererPointer);
            return new(windowPointer);
        }

        /// <summary>
        /// Create a window that can be shaped.
        /// </summary>
        /// <param name="title">The window title.</param>
        /// <param name="rectangle">The window.</param>
        /// <param name="flags">The window flags.</param>
        /// <returns></returns>
        public static Window CreateShaped(string title, Rectangle rectangle, WindowOptions flags)
        {
            fixed (byte* ptr = Native.StringToUtf8(title))
            {
                return Native.CheckNotNull(new Window(Native.SDL_CreateShapedWindow(ptr, (uint)rectangle.Location.X, (uint)rectangle.Location.Y, (uint)rectangle.Size.Width, (uint)rectangle.Size.Height, (uint)flags)));
            }
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            if (HitTestCallbacks.TryGetValue((nint)_window, out var _))
            {
                _ = Native.SDL_SetWindowHitTest(_window, null, 0);
                _ = HitTestCallbacks.Remove((nint)_window);
            }
            Native.SDL_DestroyWindow(_window);
        }

        /// <summary>
        /// Sets the window's icon.
        /// </summary>
        /// <param name="icon">The icon.</param>
        public void SetIcon(Surface icon) =>
            Native.SDL_SetWindowIcon(_window, icon.ToNative());

        /// <summary>
        /// Sets whether the window has  border.
        /// </summary>
        /// <param name="bordered">Whether the window has a border or not.</param>
        public void SetBordered(bool bordered) =>
            Native.SDL_SetWindowBordered(_window, bordered);

        /// <summary>
        /// Sets whether the window is resizable.
        /// </summary>
        /// <param name="resizable">Whether the window is resizable.</param>
        public void SetResizable(bool resizable) =>
            Native.SDL_SetWindowResizable(_window, resizable);

        /// <summary>
        /// Sets whether the window is always on top.
        /// </summary>
        /// <param name="onTop">Whether the window is always on top.</param>
        public void SetAlwaysOnTop(bool onTop) =>
            Native.SDL_SetWindowResizable(_window, onTop);

        /// <summary>
        /// Sets whether the window is visible.
        /// </summary>
        /// <param name="visible">Whether the window is visible.</param>
        public void SetVisible(bool visible)
        {
            if (visible)
            {
                Native.SDL_ShowWindow(_window);
            }
            else
            {
                Native.SDL_HideWindow(_window);
            }
        }

        /// <summary>
        /// Raises the window.
        /// </summary>
        public void Raise() =>
            Native.SDL_RaiseWindow(_window);

        /// <summary>
        /// Minimizes the window.
        /// </summary>
        public void Minimize() =>
            Native.SDL_MinimizeWindow(_window);

        /// <summary>
        /// Maximizes the window.
        /// </summary>
        public void Maximize() =>
            Native.SDL_MaximizeWindow(_window);

        /// <summary>
        /// Restores the window.
        /// </summary>
        public void Restore() =>
            Native.SDL_RestoreWindow(_window);

        /// <summary>
        /// Flashes the window.
        /// </summary>
        /// <param name="operation">The operation to perform.</param>
        public void Flash(FlashOperation operation) => Native.SDL_FlashWindow(_window, (Native.SDL_FlashOperation)operation);

        /// <summary>
        /// Sets whether the window is fullscreen.
        /// </summary>
        /// <param name="fullscreen">Whether the window is fullscreen.</param>
        /// <param name="desktop">Whether fullscreen is full screen or full desktop.</param>
        public void SetFullscreen(bool fullscreen, bool desktop = false) =>
            Native.CheckError(Native.SDL_SetWindowFullscreen(_window, (uint)(fullscreen ? (desktop ? WindowOptions.FullScreenDesktop : WindowOptions.Fullscreen) : WindowOptions.None)));

        /// <summary>
        /// Updates the window's surface.
        /// </summary>
        public void UpdateSurface() =>
            Native.CheckError(Native.SDL_UpdateWindowSurface(_window));

        /// <summary>
        /// Updates portions of the window's surface.
        /// </summary>
        /// <param name="rectangles">The areas to update.</param>
        public void UpdateSurface(Rectangle[] rectangles)
        {
            fixed (Rectangle* ptr = rectangles)
            {
                _ = Native.CheckError(Native.SDL_UpdateWindowSurfaceRects(_window, (Native.SDL_Rect*)ptr, rectangles.Length));
            }
        }

        /// <summary>
        /// Sets the window to modal for another window.
        /// </summary>
        /// <param name="otherWindow">The other window.</param>
        public void SetModalFor(Window otherWindow) =>
            Native.CheckError(Native.SDL_SetWindowModalFor(_window, otherWindow._window));

        /// <summary>
        /// Sets the input focus to the window.
        /// </summary>
        public void SetInputFocus() =>
            Native.CheckError(Native.SDL_SetWindowInputFocus(_window));

        /// <summary>
        /// Sets a hit test callback function.
        /// </summary>
        /// <param name="callback">The callback function.</param>
        public void SetHitTest(Func<Window, Point, HitTestResult>? callback)
        {
            if (callback == null)
            {
                _ = Native.SDL_SetWindowHitTest(_window, null, 0);
                _ = HitTestCallbacks.Remove((nint)_window);
            }
            else
            {
                _ = Native.CheckError(Native.SDL_SetWindowHitTest(_window, &HitTestCallback, 0));
                HitTestCallbacks[(nint)_window] = callback;
            }
        }

        [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        internal static unsafe Native.SDL_HitTestResult HitTestCallback(Native.SDL_Window* window, Native.SDL_Point* point, nint userdata)
        {
            return HitTestCallbacks.TryGetValue((nint)window, out var callback)
                ? (Native.SDL_HitTestResult)callback(new(window), new Point(point->x, point->y))
                : Native.SDL_HitTestResult.SDL_HITTEST_NORMAL;
        }

        /// <summary>
        /// Sets the window's shape.
        /// </summary>
        /// <param name="surface">The surface that specifies the shape.</param>
        /// <param name="shapeMode">The shaping mode.</param>
        public void SetShape(Surface surface, WindowShapeMode shapeMode)
        {
            var nativeShapeMode = shapeMode.ToNative();
            _ = Native.CheckError(Native.SDL_SetWindowShape(_window, surface.ToNative(), &nativeShapeMode));
        }

        /// <summary>
        /// Gets the ICC profile for the window.
        /// </summary>
        /// <returns>The ICC profile.</returns>
        public NativeMemoryBlock GetIccProfile()
        {
            nuint size;
            var buffer = Native.CheckPointer(Native.SDL_GetWindowICCProfile(_window, &size));
            return new NativeMemoryBlock(buffer, (uint)size);
        }

        internal static void DispatchEvent(Native.SDL_Event e)
        {
            if ((Native.SDL_EventType)e.type == Native.SDL_EventType.SDL_SYSWMEVENT)
            {
                // Not surfacing system specific events.
                return;
            }

            var window = new Window(e.window.windowID);

            switch ((Native.SDL_WindowEventID)e.window.@event)
            {
                case Native.SDL_WindowEventID.SDL_WINDOWEVENT_SHOWN:
                    Shown?.Invoke(window, new SdlEventArgs(e.common));
                    break;

                case Native.SDL_WindowEventID.SDL_WINDOWEVENT_HIDDEN:
                    Hidden?.Invoke(window, new SdlEventArgs(e.common));
                    break;

                case Native.SDL_WindowEventID.SDL_WINDOWEVENT_EXPOSED:
                    Exposed?.Invoke(window, new SdlEventArgs(e.common));
                    break;

                case Native.SDL_WindowEventID.SDL_WINDOWEVENT_MINIMIZED:
                    Minimized?.Invoke(window, new SdlEventArgs(e.common));
                    break;

                case Native.SDL_WindowEventID.SDL_WINDOWEVENT_MAXIMIZED:
                    Maximized?.Invoke(window, new SdlEventArgs(e.common));
                    break;

                case Native.SDL_WindowEventID.SDL_WINDOWEVENT_RESTORED:
                    Restored?.Invoke(window, new SdlEventArgs(e.common));
                    break;

                case Native.SDL_WindowEventID.SDL_WINDOWEVENT_ENTER:
                    Entered?.Invoke(window, new SdlEventArgs(e.common));
                    break;

                case Native.SDL_WindowEventID.SDL_WINDOWEVENT_LEAVE:
                    Left?.Invoke(window, new SdlEventArgs(e.common));
                    break;

                case Native.SDL_WindowEventID.SDL_WINDOWEVENT_FOCUS_GAINED:
                    FocusGained?.Invoke(window, new SdlEventArgs(e.common));
                    break;

                case Native.SDL_WindowEventID.SDL_WINDOWEVENT_FOCUS_LOST:
                    FocusLost?.Invoke(window, new SdlEventArgs(e.common));
                    break;

                case Native.SDL_WindowEventID.SDL_WINDOWEVENT_CLOSE:
                    Closed?.Invoke(window, new SdlEventArgs(e.common));
                    break;

                case Native.SDL_WindowEventID.SDL_WINDOWEVENT_TAKE_FOCUS:
                    TookFocus?.Invoke(window, new SdlEventArgs(e.common));
                    break;

                case Native.SDL_WindowEventID.SDL_WINDOWEVENT_HIT_TEST:
                    HitTest?.Invoke(window, new SdlEventArgs(e.common));
                    break;

                case Native.SDL_WindowEventID.SDL_WINDOWEVENT_MOVED:
                    Moved?.Invoke(window, new LocationEventArgs(e.window));
                    break;

                case Native.SDL_WindowEventID.SDL_WINDOWEVENT_RESIZED:
                    Resized?.Invoke(window, new SizeEventArgs(e.window));
                    break;

                case Native.SDL_WindowEventID.SDL_WINDOWEVENT_SIZE_CHANGED:
                    SizeChanged?.Invoke(window, new SizeEventArgs(e.window));
                    break;

                case Native.SDL_WindowEventID.SDL_WINDOWEVENT_ICCPROF_CHANGED:
                    IccProfileChanged?.Invoke(window, new SdlEventArgs(e.common));
                    break;

                case Native.SDL_WindowEventID.SDL_WINDOWEVENT_DISPLAY_CHANGED:
                    DisplayChanged?.Invoke(window, new DisplayEventArgs(e.window));
                    break;

                default:
                    throw new InvalidOperationException();
            }
        }

        internal Native.SDL_Window* ToNative() => _window;

        /// <summary>
        /// User data attached to the window.
        /// </summary>
        public readonly struct WindowData
        {
            private readonly Window _window;

            internal WindowData(Window window)
            {
                _window = window;
            }

            /// <summary>
            /// Gets the named window data.
            /// </summary>
            /// <param name="name">The name of the data.</param>
            /// <returns>The value of the data.</returns>
            public nint this[string name]
            {
                get
                {
                    fixed (byte* ptr = Native.StringToUtf8(name))
                    {
                        return Native.SDL_GetWindowData(_window._window, ptr);
                    }
                }
                set
                {
                    fixed (byte* ptr = Native.StringToUtf8(name))
                    {
                        Native.SDL_SetWindowData(_window._window, ptr, value);
                    }
                }
            }
        }
    }
}
