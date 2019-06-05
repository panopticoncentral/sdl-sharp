namespace SdlSharp.Touch
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
            new Native.SDL_HapticDirection(Native.SDL_HapticDirectionType.Polar, Degree);
    }
}
