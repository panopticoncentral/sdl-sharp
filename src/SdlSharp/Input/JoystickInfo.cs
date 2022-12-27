namespace SdlSharp.Input
{
    /// <summary>
    /// Information about a specific joystick
    /// </summary>
    public sealed unsafe class JoystickInfo
    {
        private readonly int _index;

        /// <summary>
        /// The name of the joystick, if any.
        /// </summary>
        public string? Name => Native.Utf8ToString(Native.CheckPointer(Native.SDL_JoystickNameForIndex(_index)));

        /// <summary>
        /// The path of the joystick, if any.
        /// </summary>
        public string? Path => Native.Utf8ToString(Native.CheckPointer(Native.SDL_JoystickPathForIndex(_index)));

        /// <summary>
        /// The player index of the joystick.
        /// </summary>
        public int PlayerIndex => Native.SDL_JoystickGetDevicePlayerIndex(_index);

        /// <summary>
        /// The GUID of the joystick.
        /// </summary>
        public Guid Id => Native.SDL_JoystickGetDeviceGUID(_index);

        /// <summary>
        /// The vendor ID of the joystick.
        /// </summary>
        public ushort Vendor => Native.SDL_JoystickGetDeviceVendor(_index);

        /// <summary>
        /// The product ID of the joystick.
        /// </summary>
        public ushort Product => Native.SDL_JoystickGetDeviceProduct(_index);

        /// <summary>
        /// The product version of the joystick.
        /// </summary>
        public ushort ProductVersion => Native.SDL_JoystickGetDeviceProductVersion(_index);

        /// <summary>
        /// The type of joystick.
        /// </summary>
        public JoystickType Type => (JoystickType)Native.SDL_JoystickGetDeviceType(_index);

        /// <summary>
        /// Whether this joystick is a game controller.
        /// </summary>
        public bool IsGameController => Native.SDL_IsGameController(_index);

        /// <summary>
        /// The name of the game controller associated with this joystick.
        /// </summary>
        public string? GameControllerName =>
            Native.Utf8ToString(Native.SDL_GameControllerNameForIndex(_index));

        /// <summary>
        /// The game controller path for this joystick.
        /// </summary>
        public string? GameControllerPath =>
            Native.Utf8ToString(Native.SDL_GameControllerPathForIndex(_index));

        /// <summary>
        /// The type of the game controller.
        /// </summary>
        public GameControllerType GameControllerType =>
            (GameControllerType)Native.SDL_GameControllerTypeForIndex(_index);

        /// <summary>
        /// The mapping for the game controller.
        /// </summary>
        public string? GameControllerMapping =>
            Native.Utf8ToStringAndFree(Native.SDL_GameControllerMappingForDeviceIndex(_index));

        /// <summary>
        /// Whether the joystick is virtual.
        /// </summary>
        public bool IsVirtual =>
            Native.SDL_JoystickIsVirtual(_index);

        internal JoystickInfo(int index)
        {
            _index = index;
        }

        /// <summary>
        /// Opens the joystick.
        /// </summary>
        /// <returns>The joystick.</returns>
        public Joystick Open() =>
            new(Native.SDL_JoystickOpen(_index));

        /// <summary>
        /// Gets the game controller instance.
        /// </summary>
        /// <returns>The game controller instance.</returns>
        public GameController OpenGameController() =>
            new(Native.CheckPointer(Native.SDL_GameControllerOpen(_index)));

        /// <summary>
        /// Detaches a virtual joystick.
        /// </summary>
        public void DetachVirtual() =>
            Native.CheckError(Native.SDL_JoystickDetachVirtual(_index));
    }
}
