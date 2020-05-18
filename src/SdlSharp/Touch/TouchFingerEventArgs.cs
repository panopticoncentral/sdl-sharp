using SdlSharp.Graphics;

namespace SdlSharp.Touch
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
        public Window Window => Window.Get(_windowId);

        internal TouchFingerEventArgs(Native.SDL_TouchFingerEvent touchFinger, Finger finger) : base(touchFinger.Timestamp)
        {
            Finger = finger;
            X = touchFinger.X;
            Y = touchFinger.Y;
            Dx = touchFinger.Dx;
            Dy = touchFinger.Dy;
            Pressure = touchFinger.Pressure;
            _windowId = touchFinger.WindowId;
        }
    }
}
