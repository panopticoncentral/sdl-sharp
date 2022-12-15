namespace SdlSharp.Input
{
    /// <summary>
    /// A type of game controller.
    /// </summary>
    public enum GameControllerType
    {
        /// <summary>
        /// Unknown
        /// </summary>
        Unknown = Native.SDL_GameControllerType.SDL_CONTROLLER_TYPE_UNKNOWN,

        /// <summary>
        /// Xbox 360 controller
        /// </summary>
        Xbox360 = Native.SDL_GameControllerType.SDL_CONTROLLER_TYPE_XBOX360,

        /// <summary>
        /// Xbox One controller
        /// </summary>
        XboxOne = Native.SDL_GameControllerType.SDL_CONTROLLER_TYPE_XBOXONE,

        /// <summary>
        /// Playstation 3 controller
        /// </summary>
        Ps3 = Native.SDL_GameControllerType.SDL_CONTROLLER_TYPE_PS3,

        /// <summary>
        /// Playstation 4 controller
        /// </summary>
        Ps4 = Native.SDL_GameControllerType.SDL_CONTROLLER_TYPE_PS4,

        /// <summary>
        /// Nintendo Switch Pro controller
        /// </summary>
        NintendoSwitchPro = Native.SDL_GameControllerType.SDL_CONTROLLER_TYPE_NINTENDO_SWITCH_PRO,

        /// <summary>
        /// Virtual controller
        /// </summary>
        Virtual = Native.SDL_GameControllerType.SDL_CONTROLLER_TYPE_VIRTUAL,

        /// <summary>
        /// Playstation 5 controller
        /// </summary>
        Ps5 = Native.SDL_GameControllerType.SDL_CONTROLLER_TYPE_PS5,

        /// <summary>
        /// Amazon Luna controller
        /// </summary>
        AmazonLuna = Native.SDL_GameControllerType.SDL_CONTROLLER_TYPE_AMAZON_LUNA,

        /// <summary>
        /// Google Stadia controller
        /// </summary>
        GoogleStadia = Native.SDL_GameControllerType.SDL_CONTROLLER_TYPE_GOOGLE_STADIA,

        /// <summary>
        /// NVidia Sheild controller
        /// </summary>
        NvidiaShield = Native.SDL_GameControllerType.SDL_CONTROLLER_TYPE_NVIDIA_SHIELD,

        /// <summary>
        /// Nintendo Switch left Joycon
        /// </summary>
        NintendoSwitchJoyconLeft = Native.SDL_GameControllerType.SDL_CONTROLLER_TYPE_NINTENDO_SWITCH_JOYCON_LEFT,

        /// <summary>
        /// Nintendo Switch right Joycon
        /// </summary>
        NintendoSwitchJoyconRight = Native.SDL_GameControllerType.SDL_CONTROLLER_TYPE_NINTENDO_SWITCH_JOYCON_RIGHT,

        /// <summary>
        /// Nintendo Switch paired Joycon
        /// </summary>
        NintendoSwitchJoyconPair = Native.SDL_GameControllerType.SDL_CONTROLLER_TYPE_NINTENDO_SWITCH_JOYCON_PAIR
    }
}
