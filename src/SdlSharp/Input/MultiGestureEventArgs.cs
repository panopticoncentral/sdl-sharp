namespace SdlSharp.Input
{
    /// <summary>
    /// The event arguments for a multi-gesture event.
    /// </summary>
    public sealed class MultiGestureEventArgs : SdlEventArgs
    {
        private readonly Native.SDL_TouchID _deviceIndex;

        /// <summary>
        /// The touch device that had the gesture.
        /// </summary>
        public TouchDevice Device => TouchDevice.IndexToInstance(_deviceIndex);

        /// <summary>
        /// The amount the fingers rotated during this motion.
        /// </summary>
        public float DTheta { get; }

        /// <summary>
        /// The amount the fingers pinched during this motion.
        /// </summary>
        public float DDist { get; }

        /// <summary>
        /// The center of the value.
        /// </summary>
        public float X { get; }

        /// <summary>
        /// The center of the value.
        /// </summary>
        public float Y { get; }

        /// <summary>
        /// The number of fingers.
        /// </summary>
        public ushort FingerCount { get; }

        internal MultiGestureEventArgs(Native.SDL_MultiGestureEvent multiGesture) : base(multiGesture.timestamp)
        {
            _deviceIndex = multiGesture.touchId;
            DTheta = multiGesture.dTheta;
            DDist = multiGesture.dDist;
            X = multiGesture.x;
            Y = multiGesture.y;
            FingerCount = multiGesture.numFingers;
        }
    }
}
