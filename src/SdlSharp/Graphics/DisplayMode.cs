namespace SdlSharp.Graphics
{
    /// <summary>
    /// A display mode.
    /// </summary>
    public readonly unsafe struct DisplayMode
    {
        /// <summary>
        /// The pixel format.
        /// </summary>
        public readonly EnumeratedPixelFormat PixelFormat { get; }

        /// <summary>
        /// The size of the display.
        /// </summary>
        public readonly Size Size { get; }

        /// <summary>
        /// The refresh rate.
        /// </summary>
        public readonly int RefreshRate { get; }

        internal DisplayMode(Native.SDL_DisplayMode mode)
        {
            PixelFormat = new(mode.format);
            Size = (mode.h, mode.w);
            RefreshRate = mode.refresh_rate;
        }

        internal Native.SDL_DisplayMode ToNative()
        {
            Native.SDL_DisplayMode mode;
            mode.format = PixelFormat.Value;
            mode.h = Size.Height;
            mode.w = Size.Width;
            mode.refresh_rate = RefreshRate;
            mode.driverdata = null;
            return mode;
        }
    }
}
