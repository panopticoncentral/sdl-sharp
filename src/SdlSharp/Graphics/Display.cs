namespace SdlSharp.Graphics
{
    /// <summary>
    /// A display in the system.
    /// </summary>
    public sealed unsafe class Display : NativeStaticIndexBase<int, Display>
    {
        private static ItemCollection<Display>? s_displays;

        private ItemCollection<DisplayMode>? _displayModes;

        /// <summary>
        /// The displays in the system.
        /// </summary>
        public static IReadOnlyList<Display> Displays => s_displays ??= new ItemCollection<Display>(IndexToInstance, Native.SDL_GetNumVideoDisplays);

        /// <summary>
        /// The display name, if any.
        /// </summary>
        public string? Name => Native.SDL_GetDisplayName(Index).ToString();

        /// <summary>
        /// The display bounds.
        /// </summary>
        public Rectangle Bounds
        {
            get
            {
                _ = Native.CheckError(Native.SDL_GetDisplayBounds(Index, out var rectangle));
                return rectangle;
            }
        }

        /// <summary>
        /// The display's usable bounds.
        /// </summary>
        public Rectangle UsableBounds
        {
            get
            {
                _ = Native.CheckError(Native.SDL_GetDisplayUsableBounds(Index, out var rectangle));
                return rectangle;
            }
        }

        /// <summary>
        /// The display's DPI.
        /// </summary>
        public (float Diagonal, float Horizontal, float Vertical) Dpi
        {
            get
            {
                _ = Native.CheckError(Native.SDL_GetDisplayDPI(Index, out var diagonal, out var horizontal, out var vertical));
                return (diagonal, horizontal, vertical);
            }
        }

        /// <summary>
        /// The display's orientation.
        /// </summary>
        public DisplayOrientation Orientation => Native.SDL_GetDisplayOrientation(Index);

        /// <summary>
        /// The display's supported modes.
        /// </summary>
        public IReadOnlyList<DisplayMode> DisplayModes => _displayModes ??= new ItemCollection<DisplayMode>(
            index =>
            {
                _ = Native.CheckError(Native.SDL_GetDisplayMode(Index, index, out var mode));
                return mode;
            },
            () => Native.SDL_GetNumDisplayModes(Index));

        /// <summary>
        /// The display's desktop display mode.
        /// </summary>
        public DisplayMode DesktopMode
        {
            get
            {
                _ = Native.CheckError(Native.SDL_GetDesktopDisplayMode(Index, out var mode));
                return mode;
            }
        }

        /// <summary>
        /// The display's current display mode.
        /// </summary>
        public DisplayMode CurrentMode
        {
            get
            {
                _ = Native.CheckError(Native.SDL_GetCurrentDisplayMode(Index, out var mode));
                return mode;
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
        public event EventHandler<OrientationChangedEventArgs>? OrientationChanged;

        /// <summary>
        /// Gets the closest supported display mode to the specified one.
        /// </summary>
        /// <param name="mode">The desired display mode.</param>
        /// <returns>The closest display mode, if any.</returns>
        public DisplayMode? GetClosestMode(DisplayMode mode)
        {
            var ret = Native.SDL_GetClosestDisplayMode(Index, ref mode, out var closest);
            return ret == null ? null : closest;
        }

        internal static void DispatchEvent(Native.SDL_Event e)
        {
            switch ((Native.SDL_EventType)e.type)
            {
                case Native.SDL_EventType.SDL_DISPLAYEVENT:
                    {
                        var display = IndexToInstance((int)e.display.display);

                        switch ((Native.SDL_DisplayEventID)e.display.@event)
                        {
                            case Native.SDL_DisplayEventID.Orientation:
                                display.OrientationChanged?.Invoke(display, new OrientationChangedEventArgs(e.display));
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
