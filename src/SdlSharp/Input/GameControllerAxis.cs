namespace SdlSharp.Input
{
    /// <summary>
    /// Game controller axes.
    /// </summary>
    public readonly record struct GameControllerAxis
    {
        private readonly Native.SDL_GameControllerAxis _axis;

        /// <summary>
        /// Invalid axis.
        /// </summary>
        public static GameControllerAxis Invalid => new(Native.SDL_GameControllerAxis.SDL_CONTROLLER_AXIS_INVALID);

        /// <summary>
        /// Left X axis.
        /// </summary>
        public static GameControllerAxis LeftX => new(Native.SDL_GameControllerAxis.SDL_CONTROLLER_AXIS_LEFTX);

        /// <summary>
        /// Left Y axis.
        /// </summary>
        public static GameControllerAxis LeftY => new(Native.SDL_GameControllerAxis.SDL_CONTROLLER_AXIS_LEFTY);

        /// <summary>
        /// Right X axis.
        /// </summary>
        public static GameControllerAxis RightX => new(Native.SDL_GameControllerAxis.SDL_CONTROLLER_AXIS_RIGHTX);

        /// <summary>
        /// Right Y axis.
        /// </summary>
        public static GameControllerAxis RightY => new(Native.SDL_GameControllerAxis.SDL_CONTROLLER_AXIS_RIGHTY);

        /// <summary>
        /// The left trigger.
        /// </summary>
        public static GameControllerAxis TriggerLeft => new(Native.SDL_GameControllerAxis.SDL_CONTROLLER_AXIS_TRIGGERLEFT);

        /// <summary>
        /// The right trigger.
        /// </summary>
        public static GameControllerAxis TriggerRight => new(Native.SDL_GameControllerAxis.SDL_CONTROLLER_AXIS_TRIGGERRIGHT);

        /// <summary>
        /// The maximum value.
        /// </summary>
        public static GameControllerAxis Max => new(Native.SDL_GameControllerAxis.SDL_CONTROLLER_AXIS_MAX);

        internal GameControllerAxis(Native.SDL_GameControllerAxis value)
        {
            _axis = value;
        }

        /// <summary>
        /// Creates an axis from a name.
        /// </summary>
        /// <param name="name">The name.</param>
        public unsafe GameControllerAxis(string name)
        {
            var axis = Native.StringToUtf8Func(name, Native.SDL_GameControllerGetAxisFromString);
            _axis = axis;
        }

        /// <summary>
        /// Maps an axis to a name.
        /// </summary>
        public override unsafe string? ToString() =>
            Native.Utf8ToString(Native.SDL_GameControllerGetStringForAxis(_axis));

        internal Native.SDL_GameControllerAxis ToNative() => _axis;
    }
}
