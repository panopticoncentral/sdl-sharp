namespace SdlSharp.Input
{
    /// <summary>
    /// A cartesian haptic direction.
    /// </summary>
    public sealed class CartesianHapticDirection : HapticDirection
    {
        /// <summary>
        /// The X axis value.
        /// </summary>
        public short XAxis { get; }

        /// <summary>
        /// The Y axis value.
        /// </summary>
        public short YAxis { get; }

        /// <summary>
        /// The Z axis value.
        /// </summary>
        public short ZAxis { get; }

        /// <summary>
        /// Constructs a new cartesian haptic direction.
        /// </summary>
        /// <param name="xAxis">The X axis value.</param>
        /// <param name="yAxis">The Y axis value.</param>
        /// <param name="zAxis">The Z axis value.</param>
        public CartesianHapticDirection(short xAxis, short yAxis, short zAxis)
        {
            XAxis = xAxis;
            YAxis = yAxis;
            ZAxis = zAxis;
        }

        internal override Native.SDL_HapticDirection ToNative() =>
            new()
            {
                type = Native.SDL_HAPTIC_CARTESIAN,
                dir0 = XAxis,
                dir1 = YAxis,
                dir2 = ZAxis
            };
    }
}
