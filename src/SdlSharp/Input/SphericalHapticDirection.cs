namespace SdlSharp.Input
{
    /// <summary>
    /// A spherical haptic direction.
    /// </summary>
    public sealed class SphericalHapticDirection : HapticDirection
    {
        /// <summary>
        /// The degrees on XY axes.
        /// </summary>
        public short XYDegrees { get; }

        /// <summary>
        /// The degrees on the Z axis.
        /// </summary>
        public short ZDegrees { get; }

        /// <summary>
        /// Create a spherical haptic direction.
        /// </summary>
        /// <param name="xyDegress">The degrees on XY axes.</param>
        /// <param name="zDegrees">The degrees on the Z axis.</param>
        public SphericalHapticDirection(short xyDegress, short zDegrees)
        {
            XYDegrees = xyDegress;
            ZDegrees = zDegrees;
        }

        internal override Native.SDL_HapticDirection ToNative() =>
            new()
            {
                type = Native.SDL_HAPTIC_SPHERICAL,
                dir0 = XYDegrees,
                dir1 = ZDegrees
            };
    }
}
