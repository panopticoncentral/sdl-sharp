namespace SdlSharp.Graphics
{
    /// <summary>
    /// A SDL window.
    /// </summary>
    public sealed unsafe class Window : NativePointerBase<Native.SDL_Window, Window>
    {
        /// <summary>
        /// A window position that is undefined.
        /// </summary>
        public const int UndefinedWindowPosition = 0x1FFF0000;

        /// <summary>
        /// An undefined window location.
        /// </summary>
        public static readonly Point UndefinedWindowLocation = new(UndefinedWindowPosition, UndefinedWindowPosition);

        /// <summary>
        /// A window position that is centered.
        /// </summary>
        public const int CenteredWindowPosition = 0x2FFF0000;

        private WindowData? _data;
        private Native.HitTestCallback? _hitTestDelegate;

        /// <summary>
        /// Whether a screen saver is enabled.
        /// </summary>
        public static bool ScreensaverEnabled
        {
            get => SdlSharp.Native.SDL_IsScreenSaverEnabled();
            set
            {
                if (value)
                {
                    SdlSharp.Native.SDL_EnableScreenSaver();
                }
                else
                {
                    SdlSharp.Native.SDL_DisableScreenSaver();
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
                var windowPointer = SdlSharp.Native.SDL_GetGrabbedWindow();
                return windowPointer == null ? null : PointerToInstanceNotNull(windowPointer);
            }
        }

        /// <summary>
        /// User defined window data.
        /// </summary>
        public WindowData Data => _data ??= new WindowData(this);

        /// <summary>
        /// The display this window is on.
        /// </summary>
        public Display Display => Display.IndexToInstance(SdlSharp.Native.CheckError(SdlSharp.Native.SDL_GetWindowDisplayIndex(Native)));

        /// <summary>
        /// The display mode of the display the window is on.
        /// </summary>
        public DisplayMode DisplayMode
        {
            get
            {
                _ = SdlSharp.Native.CheckError(SdlSharp.Native.SDL_GetWindowDisplayMode(Native, out var mode));
                return mode;
            }

            set => _ = SdlSharp.Native.CheckError(SdlSharp.Native.SDL_SetWindowDisplayMode(Native, ref value));
        }

        /// <summary>
        /// The pixel format of the window.
        /// </summary>
        public EnumeratedPixelFormat PixelFormat => new(SdlSharp.Native.SDL_GetWindowPixelFormat(Native));

        /// <summary>
        /// A window ID.
        /// </summary>
        public uint Id => SdlSharp.Native.SDL_GetWindowID(Native);

        /// <summary>
        /// The window flags.
        /// </summary>
        public WindowOptions Flags => SdlSharp.Native.SDL_GetWindowFlags(Native);

        /// <summary>
        /// The title of the window.
        /// </summary>
        public string? Title
        {
            get => SdlSharp.Native.SDL_GetWindowTitle(Native).ToString();
            set
            {
                using var utf8Title = Utf8String.ToUtf8String(value);
                SdlSharp.Native.SDL_SetWindowTitle(Native, utf8Title);
            }
        }

        /// <summary>
        /// The position of the window.
        /// </summary>
        public Point Position
        {
            get
            {
                SdlSharp.Native.SDL_GetWindowPosition(Native, out var x, out var y);
                return (x, y);
            }
            set => SdlSharp.Native.SDL_SetWindowPosition(Native, value.X, value.Y);
        }

        /// <summary>
        /// The size of the window.
        /// </summary>
        public Size Size
        {
            get
            {
                SdlSharp.Native.SDL_GetWindowSize(Native, out var width, out var height);
                return (width, height);
            }
            set => SdlSharp.Native.SDL_SetWindowSize(Native, value.Width, value.Height);
        }

        /// <summary>
        /// The size of the window borders.
        /// </summary>
        public (int Top, int Left, int Bottom, int Right) BordersSize
        {
            get
            {
                _ = SdlSharp.Native.CheckError(SdlSharp.Native.SDL_GetWindowBordersSize(Native, out var top, out var left, out var bottom, out var right));
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
                SdlSharp.Native.SDL_GetWindowMinimumSize(Native, out var width, out var height);
                return (width, height);
            }
            set => SdlSharp.Native.SDL_SetWindowMinimumSize(Native, value.Width, value.Height);
        }

        /// <summary>
        /// The maximum size of the window.
        /// </summary>
        public Size MaximumSize
        {
            get
            {
                SdlSharp.Native.SDL_GetWindowMaximumSize(Native, out var width, out var height);
                return (width, height);
            }
            set => SdlSharp.Native.SDL_SetWindowMaximumSize(Native, value.Width, value.Height);
        }

        /// <summary>
        /// The window's surface.
        /// </summary>
        public Surface Surface => Surface.PointerToInstanceNotNull(SdlSharp.Native.SDL_GetWindowSurface(Native));

        /// <summary>
        /// Whether the window has been grabbed.
        /// </summary>
        public bool Grabbed
        {
            get => SdlSharp.Native.SDL_GetWindowGrab(Native);
            set => SdlSharp.Native.SDL_SetWindowGrab(Native, value);
        }

        /// <summary>
        /// The brightness of the window.
        /// </summary>
        public float Brightness
        {
            get => SdlSharp.Native.SDL_GetWindowBrightness(Native);
            set => SdlSharp.Native.CheckError(SdlSharp.Native.SDL_SetWindowBrightness(Native, value));
        }

        /// <summary>
        /// The opacity of the window.
        /// </summary>
        public float Opacity
        {
            get
            {
                _ = SdlSharp.Native.CheckError(SdlSharp.Native.SDL_GetWindowOpacity(Native, out var value));
                return value;
            }
            set => SdlSharp.Native.CheckError(SdlSharp.Native.SDL_SetWindowOpacity(Native, value));
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
                _ = SdlSharp.Native.CheckError(SdlSharp.Native.SDL_GetWindowGammaRamp(Native, red, green, blue));
                return (red, green, blue);
            }
            set => SdlSharp.Native.CheckError(SdlSharp.Native.SDL_SetWindowGammaRamp(Native, value.Red, value.Green, value.Blue));
        }

        /// <summary>
        /// Whether the screen keyboard is being shown for this window.
        /// </summary>
        public bool IsScreenKeyboardShown =>
            SdlSharp.Native.SDL_IsScreenKeyboardShown(Native);

        /// <summary>
        /// Gets the renderer for this window, if any.
        /// </summary>
        public Renderer? Renderer =>
            Renderer.PointerToInstance(SdlSharp.Native.SDL_GetRenderer(Native));

        /// <summary>
        /// Whether the window is shaped.
        /// </summary>
        public bool IsShaped =>
            SdlSharp.Native.SDL_IsShapedWindow(Native);

        /// <summary>
        /// Gets the window's shape mode.
        /// </summary>
        /// <returns></returns>
        public WindowShapeMode? ShapeMode
        {
            get
            {
                var result = SdlSharp.Native.SDL_GetShapedWindowMode(Native, out var mode);

                if (result is SdlSharp.Native.SDL_NonShapeableWindow or SdlSharp.Native.SDL_WindowLacksShape)
                {
                    return null;
                }

                _ = SdlSharp.Native.CheckError(result);
                return WindowShapeMode.FromNative(mode);
            }
        }

        /// <summary>
        /// An event that's fired when a system window message comes in.
        /// </summary>
        public static event EventHandler<SystemWindowMessageEventArgs>? SystemWindowMessage;

        /// <summary>
        /// An event that's fired when the window is shown.
        /// </summary>
        public event EventHandler<SdlEventArgs>? Shown;

        /// <summary>
        /// An event that's fired when the window is hidden.
        /// </summary>
        public event EventHandler<SdlEventArgs>? Hidden;

        /// <summary>
        /// An event that's fired when the window is exposed.
        /// </summary>
        public event EventHandler<SdlEventArgs>? Exposed;

        /// <summary>
        /// An event that's fired when the window is minimized.
        /// </summary>
        public event EventHandler<SdlEventArgs>? Minimized;

        /// <summary>
        /// An event that's fired when the window is maximized.
        /// </summary>
        public event EventHandler<SdlEventArgs>? Maximized;

        /// <summary>
        /// An event that's fired when the window is restored.
        /// </summary>
        public event EventHandler<SdlEventArgs>? Restored;

        /// <summary>
        /// An event that's fired when the window is entered.
        /// </summary>
        public event EventHandler<SdlEventArgs>? Entered;

        /// <summary>
        /// An event that's fired when the window is left.
        /// </summary>
        public event EventHandler<SdlEventArgs>? Left;

        /// <summary>
        /// An event that's fired when the window gains focus.
        /// </summary>
        public event EventHandler<SdlEventArgs>? FocusGained;

        /// <summary>
        /// An event that's fired when the window loses focus.
        /// </summary>
        public event EventHandler<SdlEventArgs>? FocusLost;

        /// <summary>
        /// An event that's fired when the window is closed.
        /// </summary>
        public event EventHandler<SdlEventArgs>? Closed;

        /// <summary>
        /// An event that's fired when the window takes focus.
        /// </summary>
        public event EventHandler<SdlEventArgs>? TookFocus;

        /// <summary>
        /// An event that's fired when the window has a hit test.
        /// </summary>
        public event EventHandler<SdlEventArgs>? HitTest;

        /// <summary>
        /// An event that's fired when the window is moved.
        /// </summary>
        public event EventHandler<LocationEventArgs>? Moved;

        /// <summary>
        /// An event that's fired when the window is resized.
        /// </summary>
        public event EventHandler<SizeEventArgs>? Resized;

        /// <summary>
        /// An event that's fired when the window's size changes.
        /// </summary>
        public event EventHandler<SizeEventArgs>? SizeChanged;

        /// <summary>
        /// Creates a new window.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <param name="rectangle">The dimensions.</param>
        /// <param name="flags">Window flags.</param>
        /// <returns></returns>
        public static Window Create(string title, Rectangle rectangle, WindowOptions flags)
        {
            using var utf8Title = Utf8String.ToUtf8String(title);
            return PointerToInstanceNotNull(SdlSharp.Native.SDL_CreateWindow(utf8Title, rectangle.Location.X, rectangle.Location.Y, rectangle.Size.Width, rectangle.Size.Height, flags));
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
            _ = SdlSharp.Native.CheckError(SdlSharp.Native.SDL_CreateWindowAndRenderer(size.Width, size.Height, flags, out var windowPointer, out var rendererPointer));
            renderer = Renderer.PointerToInstanceNotNull(rendererPointer);
            return PointerToInstanceNotNull(windowPointer);
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
            using var utf8Title = Utf8String.ToUtf8String(title);
            return SdlSharp.Native.CheckNotNull(PointerToInstance(SdlSharp.Native.SDL_CreateShapedWindow(utf8Title, (uint)rectangle.Location.X, (uint)rectangle.Location.Y, (uint)rectangle.Size.Width, (uint)rectangle.Size.Height, flags)));
        }

        /// <summary>
        /// Gets the window with the specified ID.
        /// </summary>
        /// <param name="id">The ID.</param>
        /// <returns>The window.</returns>
        public static Window Get(uint id) =>
            PointerToInstanceNotNull(SdlSharp.Native.SDL_GetWindowFromID(id));

        /// <inheritdoc/>
        public override void Dispose()
        {
            if (_hitTestDelegate != null)
            {
                _ = SdlSharp.Native.SDL_SetWindowHitTest(Native, null, 0);
                _hitTestDelegate = null;
            }
            SdlSharp.Native.SDL_DestroyWindow(Native);

            base.Dispose();
        }

        /// <summary>
        /// Sets the window's icon.
        /// </summary>
        /// <param name="icon">The icon.</param>
        public void SetIcon(Surface icon) =>
            SdlSharp.Native.SDL_SetWindowIcon(Native, icon.Native);

        /// <summary>
        /// Sets whether the window has  border.
        /// </summary>
        /// <param name="bordered">Whether the window has a border or not.</param>
        public void SetBordered(bool bordered) =>
            SdlSharp.Native.SDL_SetWindowBordered(Native, bordered);

        /// <summary>
        /// Sets whether the window is resizable.
        /// </summary>
        /// <param name="resizable">Whether the window is resizable.</param>
        public void SetResizable(bool resizable) =>
            SdlSharp.Native.SDL_SetWindowResizable(Native, resizable);

        /// <summary>
        /// Sets whether the window is visible.
        /// </summary>
        /// <param name="visible">Whether the window is visible.</param>
        public void SetVisible(bool visible)
        {
            if (visible)
            {
                SdlSharp.Native.SDL_ShowWindow(Native);
            }
            else
            {
                SdlSharp.Native.SDL_HideWindow(Native);
            }
        }

        /// <summary>
        /// Raises the window.
        /// </summary>
        public void Raise() =>
            SdlSharp.Native.SDL_RaiseWindow(Native);

        /// <summary>
        /// Minimizes the window.
        /// </summary>
        public void Minimize() =>
            SdlSharp.Native.SDL_MinimizeWindow(Native);

        /// <summary>
        /// Maximizes the window.
        /// </summary>
        public void Maximize() =>
            SdlSharp.Native.SDL_MaximizeWindow(Native);

        /// <summary>
        /// Restores the window.
        /// </summary>
        public void Restore() =>
            SdlSharp.Native.SDL_RestoreWindow(Native);

        /// <summary>
        /// Sets whether the window is fullscreen.
        /// </summary>
        /// <param name="fullscreen">Whether the window is fullscreen.</param>
        /// <param name="desktop">Whether fullscreen is full screen or full desktop.</param>
        public void SetFullscreen(bool fullscreen, bool desktop = false) =>
            SdlSharp.Native.CheckError(SdlSharp.Native.SDL_SetWindowFullscreen(Native, fullscreen ? (desktop ? WindowOptions.FullscreenDesktop : WindowOptions.Fullscreen) : WindowOptions.None));

        /// <summary>
        /// Updates the window's surface.
        /// </summary>
        public void UpdateSurface() =>
            SdlSharp.Native.CheckError(SdlSharp.Native.SDL_UpdateWindowSurface(Native));

        /// <summary>
        /// Updates portions of the window's surface.
        /// </summary>
        /// <param name="rectangles">The areas to update.</param>
        public void UpdateSurface(Rectangle[] rectangles) =>
            SdlSharp.Native.CheckError(SdlSharp.Native.SDL_UpdateWindowSurfaceRects(Native, rectangles, rectangles.Length));

        /// <summary>
        /// Sets the window to modal for another window.
        /// </summary>
        /// <param name="otherWindow">The other window.</param>
        public void SetModalFor(Window otherWindow) =>
            SdlSharp.Native.CheckError(SdlSharp.Native.SDL_SetWindowModalFor(Native, otherWindow.Native));

        /// <summary>
        /// Sets the input focus to the window.
        /// </summary>
        public void SetInputFocus() =>
            SdlSharp.Native.CheckError(SdlSharp.Native.SDL_SetWindowInputFocus(Native));

        /// <summary>
        /// Sets a hit test callback function.
        /// </summary>
        /// <param name="callback">The callback function.</param>
        /// <param name="data">User data.</param>
        public void SetHitTest(Func<Window, Point, nint, HitTestResult> callback, nint data)
        {
            HitTestResult Callback(Native.SDL_Window* w, ref Point a, nint d) => callback(PointerToInstanceNotNull(w), a, d);
            _hitTestDelegate = callback == null ? null : new Native.HitTestCallback(Callback);
            _ = SdlSharp.Native.CheckError(SdlSharp.Native.SDL_SetWindowHitTest(Native, _hitTestDelegate, data));
        }

        /// <summary>
        /// Sets the window's shape.
        /// </summary>
        /// <param name="surface">The surface that specifies the shape.</param>
        /// <param name="shapeMode">The shaping mode.</param>
        public void SetShape(Surface surface, WindowShapeMode shapeMode)
        {
            var nativeShapeMode = shapeMode.ToNative();
            _ = SdlSharp.Native.CheckError(SdlSharp.Native.SDL_SetWindowShape(Native, surface.Native, ref nativeShapeMode));
        }

        internal static void DispatchEvent(Native.SDL_Event e)
        {
            if ((Native.SDL_EventType)e.type == SdlSharp.Native.SDL_EventType.SDL_SYSWMEVENT)
            {
                SystemWindowMessage?.Invoke(null, new SystemWindowMessageEventArgs(e.syswm));
                return;
            }

            var window = Get(e.window.windowID);

            switch ((Native.SDL_WindowEventID)e.window.@event)
            {
                case SdlSharp.Native.SDL_WindowEventID.Shown:
                    window.Shown?.Invoke(window, new SdlEventArgs(e.common));
                    break;

                case SdlSharp.Native.SDL_WindowEventID.Hidden:
                    window.Hidden?.Invoke(window, new SdlEventArgs(e.common));
                    break;

                case SdlSharp.Native.SDL_WindowEventID.Exposed:
                    window.Exposed?.Invoke(window, new SdlEventArgs(e.common));
                    break;

                case SdlSharp.Native.SDL_WindowEventID.Minimized:
                    window.Minimized?.Invoke(window, new SdlEventArgs(e.common));
                    break;

                case SdlSharp.Native.SDL_WindowEventID.Maximized:
                    window.Maximized?.Invoke(window, new SdlEventArgs(e.common));
                    break;

                case SdlSharp.Native.SDL_WindowEventID.Restored:
                    window.Restored?.Invoke(window, new SdlEventArgs(e.common));
                    break;

                case SdlSharp.Native.SDL_WindowEventID.Enter:
                    window.Entered?.Invoke(window, new SdlEventArgs(e.common));
                    break;

                case SdlSharp.Native.SDL_WindowEventID.Leave:
                    window.Left?.Invoke(window, new SdlEventArgs(e.common));
                    break;

                case SdlSharp.Native.SDL_WindowEventID.FocusGained:
                    window.FocusGained?.Invoke(window, new SdlEventArgs(e.common));
                    break;

                case SdlSharp.Native.SDL_WindowEventID.FocusLost:
                    window.FocusLost?.Invoke(window, new SdlEventArgs(e.common));
                    break;

                case SdlSharp.Native.SDL_WindowEventID.Close:
                    window.Closed?.Invoke(window, new SdlEventArgs(e.common));
                    break;

                case SdlSharp.Native.SDL_WindowEventID.TakeFocus:
                    window.TookFocus?.Invoke(window, new SdlEventArgs(e.common));
                    break;

                case SdlSharp.Native.SDL_WindowEventID.HitTest:
                    window.HitTest?.Invoke(window, new SdlEventArgs(e.common));
                    break;

                case SdlSharp.Native.SDL_WindowEventID.Moved:
                    window.Moved?.Invoke(window, new LocationEventArgs(e.window));
                    break;

                case SdlSharp.Native.SDL_WindowEventID.Resized:
                    window.Resized?.Invoke(window, new SizeEventArgs(e.window));
                    break;

                case SdlSharp.Native.SDL_WindowEventID.SizeChanged:
                    window.SizeChanged?.Invoke(window, new SizeEventArgs(e.window));
                    break;

                default:
                    throw new InvalidOperationException();
            }
        }

        /// <summary>
        /// User data attached to the window.
        /// </summary>
        public sealed class WindowData
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
                get => SdlSharp.Native.SDL_GetWindowData(_window.Native, name);
                set => SdlSharp.Native.SDL_SetWindowData(_window.Native, name, value);
            }
        }
    }
}
