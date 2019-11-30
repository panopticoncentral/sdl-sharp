using System;

namespace SdlSharp.Input
{
    /// <summary>
    /// Information about a specific joystick
    /// </summary>
    public sealed unsafe class JoystickInfo : NativeStaticIndexBase<int, JoystickInfo>
    {
        /// <summary>
        /// The name of the joystick, if any.
        /// </summary>
        public string? Name => Native.SDL_JoystickNameForIndex(Index);

        /// <summary>
        /// The player index of the joystick.
        /// </summary>
        public int PlayerIndex => Native.SDL_JoystickGetDevicePlayerIndex(Index);

        /// <summary>
        /// The GUID of the joystick.
        /// </summary>
        public Guid Guid => Native.SDL_JoystickGetDeviceGUID(Index);

        /// <summary>
        /// The vendor ID of the joystick.
        /// </summary>
        public ushort Vendor => Native.SDL_JoystickGetDeviceVendor(Index);

        /// <summary>
        /// The product ID of the joystick.
        /// </summary>
        public ushort Product => Native.SDL_JoystickGetDeviceProduct(Index);

        /// <summary>
        /// The product version of the joystick.
        /// </summary>
        public ushort ProductVersion => Native.SDL_JoystickGetDeviceProductVersion(Index);

        /// <summary>
        /// The type of joystick.
        /// </summary>
        public JoystickType Type => Native.SDL_JoystickGetDeviceType(Index);

        /// <summary>
        /// Whether this joystick is a game controller.
        /// </summary>
        public bool IsGameController => Native.SDL_IsGameController(Index);

        /// <summary>
        /// The name of the game controller.
        /// </summary>
        public string GameControllerName => Native.SDL_GameControllerNameForIndex(Index);

        /// <summary>
        /// The mapping for the game controller.
        /// </summary>
        public string? GameControllerMapping
        {
            get
            {
                using var mapping = Native.SDL_GameControllerMappingForDeviceIndex(Index);
                return mapping.ToString();
            }
        }

        internal static JoystickInfo Get(int index) =>
            IndexToInstance(index);

        /// <summary>
        /// Opens the joystick.
        /// </summary>
        /// <returns>The joystick.</returns>
        public Joystick Open() =>
            Joystick.PointerToInstanceNotNull(Native.SDL_JoystickOpen(Index));

        /// <summary>
        /// Gets the game controller instance.
        /// </summary>
        /// <returns>The game controller instance.</returns>
        public GameController OpenGameController() =>
            GameController.PointerToInstanceNotNull(Native.SDL_GameControllerOpen(Index));
    }
}
