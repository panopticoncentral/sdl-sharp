using SdlSharp.Graphics;

namespace SdlSharp.Input
{
    /// <summary>
    /// A finger on a touch device.
    /// </summary>
    public readonly unsafe struct Finger
    {
        private readonly Native.SDL_Finger* _finger;

        /// <summary>
        /// The finger's ID.
        /// </summary>
        public long Id => _finger->id.Value;

        /// <summary>
        /// The location of the finger.
        /// </summary>
        public PointF Location => (PointF)(_finger->x, _finger->y);

        /// <summary>
        /// The amount of pressure.
        /// </summary>
        public float Pressure => _finger->pressure;

        internal Finger(Native.SDL_Finger* finger)
        {
            _finger = finger;
        }

        internal Native.SDL_Finger* ToNative() => _finger;
    }
}
