using System;

namespace SdlSharp.Graphics
{
    /// <summary>
    /// A SDL window.
    /// </summary>
    public unsafe sealed class Window : NativePointerBase<Native.SDL_Window, Window>
    {
        /// <summary>
        /// A window position that is undefined.
        /// </summary>
        public const int UndefinedWindowPosition = 0x1FFF0000;

        /// <summary>
        /// An undefined window location.
        /// </summary>
        public static readonly Point UndefinedWindowLocation = new Point(UndefinedWindowPosition, UndefinedWindowPosition);

        /// <summary>
        /// A window position that is centered.
        /// </summary>
        public const int CenteredWindowPosition = 0x2FFF0000;

        private WindowData? _data;
        private Native.HitTestDelegate? _hitTestDelegate;

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
                return windowPointer == null ? null : PointerToInstanceNotNull(windowPointer);
            }
        }

        /// <summary>
        /// User defined window data.
        /// </summary>
        public WindowData Data => _data ?? (_data = new WindowData(this));

        /// <summary>
        /// The display this window is on.
        /// </summary>
        public Display Display => Display.IndexToInstance(Native.CheckError(Native.SDL_GetWindowDisplayIndex(Pointer)));

        /// <summary>
        /// The display mode of the display the window is on.
        /// </summary>
        public DisplayMode DisplayMode
        {
            get
            {
                _ = Native.CheckError(Native.SDL_GetWindowDisplayMode(Pointer, out var mode));
                return mode;
            }

            set
            {
                _ = Native.CheckError(Native.SDL_SetWindowDisplayMode(Pointer, ref value));
            }
        }

        /// <summary>
        /// The pixel format of the window.
        /// </summary>
        public EnumeratedPixelFormat PixelFormat => new EnumeratedPixelFormat(Native.SDL_GetWindowPixelFormat(Pointer));

        /// <summary>
        /// A window ID.
        /// </summary>
        public uint Id => Native.SDL_GetWindowID(Pointer);

        /// <summary>
        /// The window flags.
        /// </summary>
        public WindowFlags Flags => Native.SDL_GetWindowFlags(Pointer);

        /// <summary>
        /// The title of the window.
        /// </summary>
        public string? Title
        {
            get => Native.SDL_GetWindowTitle(Pointer).ToString();
            set
            {
                using var utf8Title = Utf8String.ToUtf8String(value);
                Native.SDL_SetWindowTitle(Pointer, utf8Title);
            }
        }

        /// <summary>
        /// The position of the window.
        /// </summary>
        public Point Position
        {
            get
            {
                Native.SDL_GetWindowPosition(Pointer, out var x, out var y);
                return (x, y);
            }
            set => Native.SDL_SetWindowPosition(Pointer, value.X, value.Y);
        }

        /// <summary>
        /// The size of the window.
        /// </summary>
        public Size Size
        {
            get
            {
                Native.SDL_GetWindowSize(Pointer, out var width, out var height);
                return (width, height);
            }
            set => Native.SDL_SetWindowSize(Pointer, value.Width, value.Height);
        }

        /// <summary>
        /// The size of the window borders.
        /// </summary>
        public (int Top, int Left, int Bottom, int Right) BordersSize
        {
            get
            {
                _ = Native.CheckError(Native.SDL_GetWindowBordersSize(Pointer, out var top, out var left, out var bottom, out var right));
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
                Native.SDL_GetWindowMinimumSize(Pointer, out var width, out var height);
                return (width, height);
            }
            set => Native.SDL_SetWindowMinimumSize(Pointer, value.Width, value.Height);
        }

        /// <summary>
        /// The maximum size of the window.
        /// </summary>
        public Size MaximumSize
        {
            get
            {
                Native.SDL_GetWindowMaximumSize(Pointer, out var width, out var height);
                return (width, height);
            }
            set => Native.SDL_SetWindowMaximumSize(Pointer, value.Width, value.Height);
        }

        /// <summary>
        /// The window's surface.
        /// </summary>
        public Surface Surface => Surface.PointerToInstanceNotNull(Native.SDL_GetWindowSurface(Pointer));

        /// <summary>
        /// Whether the window has been grabbed.
        /// </summary>
        public bool Grabbed
        {
            get => Native.SDL_GetWindowGrab(Pointer);
            set => Native.SDL_SetWindowGrab(Pointer, value);
        }

        /// <summary>
        /// The brightness of the window.
        /// </summary>
        public float Brightness
        {
            get => Native.SDL_GetWindowBrightness(Pointer);
            set => Native.SDL_SetWindowBrightness(Pointer, value);
        }

        /// <summary>
        /// The opacity of the window.
        /// </summary>
        public float Opacity
        {
            get
            {
                _ = Native.CheckError(Native.SDL_GetWindowOpacity(Pointer, out var value));
                return value;
            }
            set => Native.CheckError(Native.SDL_SetWindowOpacity(Pointer, value));
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
                _ = Native.CheckError(Native.SDL_GetWindowGammaRamp(Pointer, red, green, blue));
                return (red, green, blue);
            }
            set => Native.CheckError(Native.SDL_SetWindowGammaRamp(Pointer, value.Red, value.Green, value.Blue));
        }

        /// <summary>
        /// Whether the screen keyboard is being shown for this window.
        /// </summary>
        public bool IsScreenKeyboardShown =>
            Native.SDL_IsScreenKeyboardShown(Pointer);

        /// <summary>
        /// Gets the renderer for this window.
        /// </summary>
        public Renderer Renderer =>
            Renderer.PointerToInstanceNotNull(Native.SDL_GetRenderer(Pointer));

        /// <summary>
        /// Whether the window is shaped.
        /// </summary>
        public bool IsShaped =>
            Native.SDL_IsShapedWindow(Pointer);

        /// <summary>
        /// Gets the window's shape mode.
        /// </summary>
        /// <returns></returns>
        public WindowShapeMode ShapeMode
        {
            get
            {
                _ = Native.CheckError(Native.SDL_GetShapedWindowMode(Pointer, out var mode));
                return WindowShapeMode.FromNative(mode);
            }
        }

        /// <summary>
        /// An event that's fired when a system window message comes in.
        /// </summary>
        public static event EventHandler<SystemWindowMessageEventArgs> SystemWindowMessage;

        /// <summary>
        /// An event that's fired when the window is shown.
        /// </summary>
        public event EventHandler<SdlEventArgs> Shown;

        /// <summary>
        /// An event that's fired when the window is hidden.
        /// </summary>
        public event EventHandler<SdlEventArgs> Hidden;

        /// <summary>
        /// An event that's fired when the window is exposed.
        /// </summary>
        public event EventHandler<SdlEventArgs> Exposed;

        /// <summary>
        /// An event that's fired when the window is minimized.
        /// </summary>
        public event EventHandler<SdlEventArgs> Minimized;

        /// <summary>
        /// An event that's fired when the window is maximized.
        /// </summary>
        public event EventHandler<SdlEventArgs> Maximized;

        /// <summary>
        /// An event that's fired when the window is restored.
        /// </summary>
        public event EventHandler<SdlEventArgs> Restored;

        /// <summary>
        /// An event that's fired when the window is entered.
        /// </summary>
        public event EventHandler<SdlEventArgs> Entered;

        /// <summary>
        /// An event that's fired when the window is left.
        /// </summary>
        public event EventHandler<SdlEventArgs> Left;

        /// <summary>
        /// An event that's fired when the window gains focus.
        /// </summary>
        public event EventHandler<SdlEventArgs> FocusGained;

        /// <summary>
        /// An event that's fired when the window loses focus.
        /// </summary>
        public event EventHandler<SdlEventArgs> FocusLost;

        /// <summary>
        /// An event that's fired when the window is closed.
        /// </summary>
        public event EventHandler<SdlEventArgs> Closed;

        /// <summary>
        /// An event that's fired when the window takes focus.
        /// </summary>
        public event EventHandler<SdlEventArgs> TookFocus;

        /// <summary>
        /// An event that's fired when the window has a hit test.
        /// </summary>
        public event EventHandler<SdlEventArgs> HitTest;

        /// <summary>
        /// An event that's fired when the window is moved.
        /// </summary>
        public event EventHandler<LocationEventArgs> Moved;

        /// <summary>
        /// An event that's fired when the window is resized.
        /// </summary>
        public event EventHandler<SizeEventArgs> Resized;

        /// <summary>
        /// An event that's fired when the window's size changes.
        /// </summary>
        public event EventHandler<SizeEventArgs> SizeChanged;

        /// <summary>
        /// Creates a new window.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <param name="rectangle">The dimensions.</param>
        /// <param name="flags">Window flags.</param>
        /// <returns></returns>
        public static Window Create(string title, Rectangle rectangle, WindowFlags flags)
        {
            using var utf8Title = Utf8String.ToUtf8String(title);
            return PointerToInstanceNotNull(Native.SDL_CreateWindow(utf8Title, rectangle.Location.X, rectangle.Location.Y, rectangle.Size.Width, rectangle.Size.Height, flags));
        }

        /// <summary>
        /// Creates a new window.
        /// </summary>
        /// <param name="size">The size of the window.</param>
        /// <param name="flags">The flags.</param>
        /// <param name="renderer">The window's renderer.</param>
        /// <returns></returns>
        public static Window Create(Size size, WindowFlags flags, out Renderer renderer)
        {
            _ = Native.CheckError(Native.SDL_CreateWindowAndRenderer(size.Width, size.Height, flags, out var windowPointer, out var rendererPointer));
            renderer = Renderer.PointerToInstanceNotNull(rendererPointer);
            return PointerToInstanceNotNull(windowPointer);
        }

        /// <summary>
        /// Create a window that can be shaped.
        /// </summary>
        /// <param name="title">The window title.</param>
        /// <param name="location">The window location.</param>
        /// <param name="size">The window size.</param>
        /// <param name="flags">The window flags.</param>
        /// <returns></returns>
        public static Window CreateShaped(string title, Rectangle rectangle, WindowFlags flags)
        {
            using var utf8Title = Utf8String.ToUtf8String(title);
            return Native.CheckNotNull(PointerToInstance(Native.SDL_CreateShapedWindow(utf8Title, (uint)rectangle.Location.X, (uint)rectangle.Location.Y, (uint)rectangle.Size.Width, (uint)rectangle.Size.Height, flags)));
        }

        /// <summary>
        /// Gets the window with the specified ID.
        /// </summary>
        /// <param name="id">The ID.</param>
        /// <returns>The window.</returns>
        public static Window Get(uint id) =>
            PointerToInstanceNotNull(Native.SDL_GetWindowFromID(id));

        /// <inheritdoc/>
        public override void Dispose()
        {
            if (_hitTestDelegate != null)
            {
                _ = Native.SDL_SetWindowHitTest(Pointer, null, IntPtr.Zero);
                _hitTestDelegate = null;
            }
            Native.SDL_DestroyWindow(Pointer);

            base.Dispose();
        }

        /// <summary>
        /// Sets the window's icon.
        /// </summary>
        /// <param name="icon">The icon.</param>
        public void SetIcon(Surface icon) =>
            Native.SDL_SetWindowIcon(Pointer, icon.Pointer);

        /// <summary>
        /// Sets whether the window has  border.
        /// </summary>
        /// <param name="bordered">Whether the window has a border or not.</param>
        public void SetBordered(bool bordered) =>
            Native.SDL_SetWindowBordered(Pointer, bordered);

        /// <summary>
        /// Sets whether the window is resizable.
        /// </summary>
        /// <param name="resizable">Whether the window is resizable.</param>
        public void SetResizable(bool resizable) =>
            Native.SDL_SetWindowResizable(Pointer, resizable);

        /// <summary>
        /// Sets whether the window is visible.
        /// </summary>
        /// <param name="visible">Whether the window is visible.</param>
        public void SetVisible(bool visible)
        {
            if (visible)
            {
                Native.SDL_ShowWindow(Pointer);
            }
            else
            {
                Native.SDL_HideWindow(Pointer);
            }
        }

        /// <summary>
        /// Raises the window.
        /// </summary>
        public void Raise() =>
            Native.SDL_RaiseWindow(Pointer);

        /// <summary>
        /// Minimizes the window.
        /// </summary>
        public void Minimize() =>
            Native.SDL_MinimizeWindow(Pointer);

        /// <summary>
        /// Maximizes the window.
        /// </summary>
        public void Maximize() =>
            Native.SDL_MaximizeWindow(Pointer);

        /// <summary>
        /// Restores the window.
        /// </summary>
        public void Restore() =>
            Native.SDL_RestoreWindow(Pointer);

        /// <summary>
        /// Sets whether the window is fullscreen.
        /// </summary>
        /// <param name="fullscreen">Whether the window is fullscreen.</param>
        /// <param name="desktop">Whether fullscreen is full screen or full desktop.</param>
        public void SetFullscreen(bool fullscreen, bool desktop = false) =>
            Native.SDL_SetWindowFullscreen(Pointer, fullscreen ? (desktop ? WindowFlags.FullscreenDesktop : WindowFlags.Fullscreen) : WindowFlags.None);

        /// <summary>
        /// Updates the window's surface.
        /// </summary>
        public void UpdateSurface() =>
            Native.CheckError(Native.SDL_UpdateWindowSurface(Pointer));

        /// <summary>
        /// Updates portions of the window's surface.
        /// </summary>
        /// <param name="rectangles">The areas to update.</param>
        public void UpdateSurface(Rectangle[] rectangles) =>
            Native.CheckError(Native.SDL_UpdateWindowSurfaceRects(Pointer, rectangles, rectangles.Length));

        /// <summary>
        /// Sets the window to modal for another window.
        /// </summary>
        /// <param name="otherWindow">The other window.</param>
        public void SetModalFor(Window otherWindow) =>
            Native.CheckError(Native.SDL_SetWindowModalFor(Pointer, otherWindow.Pointer));

        /// <summary>
        /// Sets the input focus to the window.
        /// </summary>
        public void SetInputFocus() =>
            Native.CheckError(Native.SDL_SetWindowInputFocus(Pointer));

        /// <summary>
        /// Sets a hit test callback function.
        /// </summary>
        /// <param name="callback">The callback function.</param>
        /// <param name="data">User data.</param>
        public void SetHitTest(Func<Window, Point, IntPtr, HitTestResult> callback, IntPtr data)
        {
            HitTestResult Callback(Native.SDL_Window* w, ref Point a, IntPtr d) => callback(PointerToInstanceNotNull(w), a, d);
            _hitTestDelegate = callback == null ? null : new Native.HitTestDelegate(Callback);
            _ = Native.CheckError(Native.SDL_SetWindowHitTest(Pointer, _hitTestDelegate, data));
        }

        /// <summary>
        /// Sets the window's shape.
        /// </summary>
        /// <param name="surface">The surface that specifies the shape.</param>
        /// <param name="shapeMode">The shaping mode.</param>
        public void SetShape(Surface surface, WindowShapeMode shapeMode)
        {
            var nativeShapeMode = shapeMode.ToNative();
            _ = Native.CheckError(Native.SDL_SetWindowShape(Pointer, surface.Pointer, ref nativeShapeMode));
        }

        internal static void DispatchEvent(Native.SDL_Event e)
        {
            if (e.Type == Native.SDL_EventType.SystemWindowMessageEvent)
            {
                SystemWindowMessage?.Invoke(null, new SystemWindowMessageEventArgs(e.Syswm));
                return;
            }

            var window = Get(e.Window.WindowId);

            switch (e.Window.WindowEventId)
            {
                case Native.SDL_WindowEventID.Shown:
                    window.Shown?.Invoke(window, new SdlEventArgs(e.Common));
                    break;

                case Native.SDL_WindowEventID.Hidden:
                    window.Hidden?.Invoke(window, new SdlEventArgs(e.Common));
                    break;

                case Native.SDL_WindowEventID.Exposed:
                    window.Exposed?.Invoke(window, new SdlEventArgs(e.Common));
                    break;

                case Native.SDL_WindowEventID.Minimized:
                    window.Minimized?.Invoke(window, new SdlEventArgs(e.Common));
                    break;

                case Native.SDL_WindowEventID.Maximized:
                    window.Maximized?.Invoke(window, new SdlEventArgs(e.Common));
                    break;

                case Native.SDL_WindowEventID.Restored:
                    window.Restored?.Invoke(window, new SdlEventArgs(e.Common));
                    break;

                case Native.SDL_WindowEventID.Enter:
                    window.Entered?.Invoke(window, new SdlEventArgs(e.Common));
                    break;

                case Native.SDL_WindowEventID.Leave:
                    window.Left?.Invoke(window, new SdlEventArgs(e.Common));
                    break;

                case Native.SDL_WindowEventID.FocusGained:
                    window.FocusGained?.Invoke(window, new SdlEventArgs(e.Common));
                    break;

                case Native.SDL_WindowEventID.FocusLost:
                    window.FocusLost?.Invoke(window, new SdlEventArgs(e.Common));
                    break;

                case Native.SDL_WindowEventID.Close:
                    window.Closed?.Invoke(window, new SdlEventArgs(e.Common));
                    break;

                case Native.SDL_WindowEventID.TakeFocus:
                    window.TookFocus?.Invoke(window, new SdlEventArgs(e.Common));
                    break;

                case Native.SDL_WindowEventID.HitTest:
                    window.HitTest?.Invoke(window, new SdlEventArgs(e.Common));
                    break;

                case Native.SDL_WindowEventID.Moved:
                    window.Moved?.Invoke(window, new LocationEventArgs(e.Window));
                    break;

                case Native.SDL_WindowEventID.Resized:
                    window.Resized?.Invoke(window, new SizeEventArgs(e.Window));
                    break;

                case Native.SDL_WindowEventID.SizeChanged:
                    window.SizeChanged?.Invoke(window, new SizeEventArgs(e.Window));
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

            public WindowData(Window window)
            {
                _window = window;
            }

            public IntPtr this[string name]
            {
                get => Native.SDL_GetWindowData(_window.Pointer, name);
                set => Native.SDL_SetWindowData(_window.Pointer, name, value);
            }
        }
    }
}
