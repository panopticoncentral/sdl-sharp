using SdlSharp.Graphics;

namespace SdlSharp.Input
{
    /// <summary>
    /// Event arguments for a finger touch event.
    /// </summary>
    public sealed class TouchFingerEventArgs : SdlEventArgs
    {
        private readonly uint _windowId;

        /// <summary>
        /// The finger.
        /// </summary>
        public Finger Finger { get; }

        /// <summary>
        /// The X location of the touch.
        /// </summary>
        public float X { get; }

        /// <summary>
        /// The Y location of the touch.
        /// </summary>
        public float Y { get; }

        /// <summary>
        /// The delta X location of the touch.
        /// </summary>
        public float Dx { get; }

        /// <summary>
        /// The delta Y location of the touch
        /// </summary>
        public float Dy { get; }

        /// <summary>
        /// The amount of pressure.
        /// </summary>
        public float Pressure { get; }

        /// <summary>
        /// The window that had focus, if any.
        /// </summary>
        public Window Window => new(_windowId);

        internal TouchFingerEventArgs(Native.SDL_TouchFingerEvent touchFinger, Finger finger) : base(touchFinger.timestamp)
        {
            Finger = finger;
            X = touchFinger.x;
            Y = touchFinger.y;
            Dx = touchFinger.dx;
            Dy = touchFinger.dy;
            Pressure = touchFinger.pressure;
            _windowId = touchFinger.windowID;
        }
    }
}
