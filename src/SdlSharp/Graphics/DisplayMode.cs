using System;

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

        private readonly IntPtr _driverData;

        public DisplayMode(uint pixelFormat, Size size, int refreshRate)
        {
            PixelFormat = pixelFormat;
            _width = size.Width;
            _height = size.Height;
            RefreshRate = refreshRate;
            _driverData = IntPtr.Zero;
        }
    }
}
