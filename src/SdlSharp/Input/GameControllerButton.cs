namespace SdlSharp.Input
{
    /// <summary>
    /// A game controller button.
    /// </summary>
    public readonly record struct GameControllerButton
    {
        internal readonly Native.SDL_GameControllerButton Value;

        /// <summary>
        /// Invalid button.
        /// </summary>
        public static GameControllerButton Invalid => new(Native.SDL_GameControllerButton.SDL_CONTROLLER_BUTTON_INVALID);

        /// <summary>
        /// A button.
        /// </summary>
        public static GameControllerButton A => new(Native.SDL_GameControllerButton.SDL_CONTROLLER_BUTTON_A);

        /// <summary>
        /// B button.
        /// </summary>
        public static GameControllerButton B => new(Native.SDL_GameControllerButton.SDL_CONTROLLER_BUTTON_B);

        /// <summary>
        /// X button.
        /// </summary>
        public static GameControllerButton X => new(Native.SDL_GameControllerButton.SDL_CONTROLLER_BUTTON_X);

        /// <summary>
        /// Y button.
        /// </summary>
        public static GameControllerButton Y => new(Native.SDL_GameControllerButton.SDL_CONTROLLER_BUTTON_Y);

        /// <summary>
        /// Back button.
        /// </summary>
        public static GameControllerButton Back => new(Native.SDL_GameControllerButton.SDL_CONTROLLER_BUTTON_BACK);

        /// <summary>
        /// Guide button.
        /// </summary>
        public static GameControllerButton Guide => new(Native.SDL_GameControllerButton.SDL_CONTROLLER_BUTTON_GUIDE);

        /// <summary>
        /// Start button.
        /// </summary>
        public static GameControllerButton Start => new(Native.SDL_GameControllerButton.SDL_CONTROLLER_BUTTON_START);

        /// <summary>
        /// Left stick button.
        /// </summary>
        public static GameControllerButton LeftStick => new(Native.SDL_GameControllerButton.SDL_CONTROLLER_BUTTON_LEFTSTICK);

        /// <summary>
        /// Right stick button.
        /// </summary>
        public static GameControllerButton RightStick => new(Native.SDL_GameControllerButton.SDL_CONTROLLER_BUTTON_RIGHTSTICK);

        /// <summary>
        /// Left shoulder button.
        /// </summary>
        public static GameControllerButton LeftShoulder => new(Native.SDL_GameControllerButton.SDL_CONTROLLER_BUTTON_LEFTSHOULDER);

        /// <summary>
        /// Right shoulder button.
        /// </summary>
        public static GameControllerButton RightShoulder => new(Native.SDL_GameControllerButton.SDL_CONTROLLER_BUTTON_RIGHTSHOULDER);

        /// <summary>
        /// Directional pad up.
        /// </summary>
        public static GameControllerButton DpadUp => new(Native.SDL_GameControllerButton.SDL_CONTROLLER_BUTTON_DPAD_UP);

        /// <summary>
        /// Directional pad down.
        /// </summary>
        public static GameControllerButton DpadDown => new(Native.SDL_GameControllerButton.SDL_CONTROLLER_BUTTON_DPAD_DOWN);

        /// <summary>
        /// Directional pad left.
        /// </summary>
        public static GameControllerButton DpadLeft => new(Native.SDL_GameControllerButton.SDL_CONTROLLER_BUTTON_DPAD_LEFT);

        /// <summary>
        /// Direction pad right.
        /// </summary>
        public static GameControllerButton DpadRight => new(Native.SDL_GameControllerButton.SDL_CONTROLLER_BUTTON_DPAD_RIGHT);

        /// <summary>
        /// Xbox Series X share button, PS5 microphone button, Nintendo Switch Pro capture button, Amazon Luna microphone button.
        /// </summary>
        public static GameControllerButton Misc1 => new(Native.SDL_GameControllerButton.SDL_CONTROLLER_BUTTON_MISC1);

        /// <summary>
        /// XBox Elite paddle 1.
        /// </summary>
        public static GameControllerButton Paddle1 => new(Native.SDL_GameControllerButton.SDL_CONTROLLER_BUTTON_PADDLE1);

        /// <summary>
        /// XBox Elite paddle 2.
        /// </summary>
        public static GameControllerButton Paddle2 => new(Native.SDL_GameControllerButton.SDL_CONTROLLER_BUTTON_PADDLE2);

        /// <summary>
        /// XBox Elite paddle 3.
        /// </summary>
        public static GameControllerButton Paddle3 => new(Native.SDL_GameControllerButton.SDL_CONTROLLER_BUTTON_PADDLE3);

        /// <summary>
        /// XBox Elite paddle 4.
        /// </summary>
        public static GameControllerButton Paddle4 => new(Native.SDL_GameControllerButton.SDL_CONTROLLER_BUTTON_PADDLE4);

        /// <summary>
        /// PS4/PS5 touchpad button.
        /// </summary>
        public static GameControllerButton Touchpad => new(Native.SDL_GameControllerButton.SDL_CONTROLLER_BUTTON_TOUCHPAD);

        /// <summary>
        /// Max button.
        /// </summary>
        public static GameControllerButton Max => new(Native.SDL_GameControllerButton.SDL_CONTROLLER_BUTTON_MAX);

        internal GameControllerButton(Native.SDL_GameControllerButton value)
        {
            Value = value;
        }

        /// <summary>
        /// Creates a button from a name.
        /// </summary>
        /// <param name="name">The name.</param>
        public unsafe GameControllerButton(string name)
        {
            fixed (byte* ptr = Native.StringToUtf8(name))
            {
                Value = Native.SDL_GameControllerGetButtonFromString(ptr);
            }
        }

        /// <summary>
        /// Maps a button to a name.
        /// </summary>
        public override unsafe string? ToString() =>
            Native.Utf8ToString(Native.SDL_GameControllerGetStringForButton(Value));
    }
}
