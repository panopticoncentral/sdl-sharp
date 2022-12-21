namespace SdlSharp.Input
{
    /// <summary>
    /// A polar haptic direction.
    /// </summary>
    public sealed class PolarHapticDirection : HapticDirection
    {
        /// <summary>
        /// The degrees.
        /// </summary>
        public short Degree { get; }

        /// <summary>
        /// Creates a polar haptic direction.
        /// </summary>
        /// <param name="degree">The degrees.</param>
        public PolarHapticDirection(short degree)
        {
            Degree = degree;
        }

        internal override Native.SDL_HapticDirection ToNative() =>
            new()
            {
                type = Native.SDL_HAPTIC_POALR,
                dir0 = Degree
            };
    }
}
