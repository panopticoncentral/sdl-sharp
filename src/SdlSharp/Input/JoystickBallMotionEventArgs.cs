namespace SdlSharp.Input
{
    /// <summary>
    /// Event arguments for a joystick ball event.
    /// </summary>
    public sealed class JoystickBallMotionEventArgs : SdlEventArgs
    {
        /// <summary>
        /// Which ball.
        /// </summary>
        public byte Ball { get; }

        /// <summary>
        /// The relative movement.
        /// </summary>
        public Point RelativeLocation { get; }

        internal JoystickBallMotionEventArgs(Native.SDL_JoyBallEvent ball) : base(ball.Timestamp)
        {
            Ball = ball.Ball;
            RelativeLocation = (ball.Xrel, ball.Yrel);
        }
    }
}
