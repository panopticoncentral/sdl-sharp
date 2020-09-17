namespace SdlSharp.Graphics
{
    /// <summary>
    /// A display mode.
    /// </summary>
    public readonly struct DisplayMode
    {
        /// <summary>
        /// The pixel format.
        /// </summary>
        public readonly uint PixelFormat { get; }

        private readonly int _width;
        private readonly int _height;

        /// <summary>
        /// The size of the display.
        /// </summary>
        public readonly Size Size => new Size(_width, _height);

        /// <summary>
        /// The refresh rate.
        /// </summary>
        public readonly int RefreshRate { get; }

#pragma warning disable IDE0052 // Remove unread private members
        private readonly nint _driverData;
#pragma warning restore IDE0052 // Remove unread private members

        /// <summary>
        /// Creates a new display mode.
        /// </summary>
        /// <param name="pixelFormat">The pixel format.</param>
        /// <param name="size">The size.</param>
        /// <param name="refreshRate">The refresh rate.</param>
        public DisplayMode(uint pixelFormat, Size size, int refreshRate)
        {
            PixelFormat = pixelFormat;
            _width = size.Width;
            _height = size.Height;
            RefreshRate = refreshRate;
            _driverData = 0;
        }
    }
}
