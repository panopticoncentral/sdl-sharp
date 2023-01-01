namespace SdlSharp.Graphics
{
    /// <summary>
    /// A display in the system.
    /// </summary>
    public sealed unsafe class Display
    {
        private readonly int _index;

        /// <summary>
        /// The displays in the system.
        /// </summary>
        public static IReadOnlyList<Display> Displays => Native.GetIndexedCollection(i => new Display(i), Native.SDL_GetNumVideoDisplays);

        /// <summary>
        /// The display name, if any.
        /// </summary>
        public string? Name => Native.Utf8ToString(Native.SDL_GetDisplayName(_index));

        /// <summary>
        /// The display bounds.
        /// </summary>
        public Rectangle Bounds
        {
            get
            {
                Native.SDL_Rect rect;
                _ = Native.CheckError(Native.SDL_GetDisplayBounds(_index, &rect));
                return new(rect);
            }
        }

        /// <summary>
        /// The display's usable bounds.
        /// </summary>
        public Rectangle UsableBounds
        {
            get
            {
                Native.SDL_Rect rect;
                _ = Native.CheckError(Native.SDL_GetDisplayUsableBounds(_index, &rect));
                return new(rect);
            }
        }

        /// <summary>
        /// The display's DPI.
        /// </summary>
        public (float Diagonal, float Horizontal, float Vertical) Dpi
        {
            get
            {
                float diagonal, horizontal, vertical;
                _ = Native.CheckError(Native.SDL_GetDisplayDPI(_index, &diagonal, &horizontal, &vertical));
                return (diagonal, horizontal, vertical);
            }
        }

        /// <summary>
        /// The display's orientation.
        /// </summary>
        public DisplayOrientation Orientation => (DisplayOrientation)Native.SDL_GetDisplayOrientation(_index);

        /// <summary>
        /// The display's supported modes.
        /// </summary>
        public IReadOnlyList<DisplayMode> DisplayModes => Native.GetIndexedCollection(i =>
            {
                Native.SDL_DisplayMode mode;
                _ = Native.CheckError(Native.SDL_GetDisplayMode(_index, i, &mode));
                return new DisplayMode(mode);
            },
            () => Native.SDL_GetNumDisplayModes(_index));

        /// <summary>
        /// The display's desktop display mode.
        /// </summary>
        public DisplayMode DesktopMode
        {
            get
            {
                Native.SDL_DisplayMode mode;
                _ = Native.CheckError(Native.SDL_GetDesktopDisplayMode(_index, &mode));
                return new DisplayMode(mode);
            }
        }

        /// <summary>
        /// The display's current display mode.
        /// </summary>
        public DisplayMode CurrentMode
        {
            get
            {
                Native.SDL_DisplayMode mode;
                _ = Native.CheckError(Native.SDL_GetCurrentDisplayMode(_index, &mode));
                return new DisplayMode(mode);
            }
        }

        /// <summary>
        /// An event that is fired when the device has been reset and textures need to be recreated.
        /// </summary>
        public static event EventHandler<SdlEventArgs>? RenderDeviceReset;

        /// <summary>
        /// An event that is fired when the targets have been reset and textures need to be recreated.
        /// </summary>
        public static event EventHandler<SdlEventArgs>? RenderTargetsReset;

        /// <summary>
        /// An event that is fired when a device's orientation changes.
        /// </summary>
        public static event EventHandler<OrientationChangedEventArgs>? OrientationChanged;

        /// <summary>
        /// An event that is fired when a display is connected.
        /// </summary>
        public static event EventHandler<SdlEventArgs>? DisplayConnected;

        /// <summary>
        /// An event that is fired when a display is disconnected.
        /// </summary>
        public static event EventHandler<SdlEventArgs>? DisplayDisconnected;

        internal Display(int index)
        {
            _index = index;
        }

        /// <summary>
        /// Returns the display that corresponds to the point.
        /// </summary>
        /// <param name="point">The point.</param>
        /// <returns>The display.</returns>
        public static Display FromPoint(Point point)
        {
            var pointLocal = point;
            return new(Native.CheckError(Native.SDL_GetPointDisplayIndex((Native.SDL_Point*)&pointLocal)));
        }

        /// <summary>
        /// Returns the display that corresponds to the rectangle.
        /// </summary>
        /// <param name="rectangle">The rectangle.</param>
        /// <returns>The display.</returns>
        public static Display FromPoint(Rectangle rectangle)
        {
            var rectangleLocal = rectangle;
            return new(Native.CheckError(Native.SDL_GetRectDisplayIndex((Native.SDL_Rect*)&rectangleLocal)));
        }

        /// <summary>
        /// Gets the closest supported display mode to the specified one.
        /// </summary>
        /// <param name="mode">The desired display mode.</param>
        /// <returns>The closest display mode, if any.</returns>
        public DisplayMode? GetClosestMode(DisplayMode mode)
        {
            Native.SDL_DisplayMode desired = mode.ToNative(), closest;
            var ret = Native.SDL_GetClosestDisplayMode(_index, &desired, &closest);
            return ret == null ? null : new(closest);
        }

        internal static void DispatchEvent(Native.SDL_Event e)
        {
            switch ((Native.SDL_EventType)e.type)
            {
                case Native.SDL_EventType.SDL_DISPLAYEVENT:
                    {
                        switch ((Native.SDL_DisplayEventID)e.display.@event)
                        {
                            case Native.SDL_DisplayEventID.SDL_DISPLAYEVENT_ORIENTATION:
                                OrientationChanged?.Invoke(new Display((int)e.display.display), new OrientationChangedEventArgs(e.display));
                                break;

                            case Native.SDL_DisplayEventID.SDL_DISPLAYEVENT_CONNECTED:
                                DisplayConnected?.Invoke(new Display((int)e.display.display), new SdlEventArgs(e.display.timestamp));
                                break;

                            case Native.SDL_DisplayEventID.SDL_DISPLAYEVENT_DISCONNECTED:
                                DisplayDisconnected?.Invoke(new Display((int)e.display.display), new SdlEventArgs(e.display.timestamp));
                                break;

                            default:
                                throw new InvalidOperationException();
                        }

                        break;
                    }

                case Native.SDL_EventType.SDL_RENDER_DEVICE_RESET:
                    RenderDeviceReset?.Invoke(null, new SdlEventArgs(e.common));
                    break;

                case Native.SDL_EventType.SDL_RENDER_TARGETS_RESET:
                    RenderTargetsReset?.Invoke(null, new SdlEventArgs(e.common));
                    break;

                default:
                    throw new InvalidOperationException();
            }
        }
    }
}
