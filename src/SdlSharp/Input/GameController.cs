using System;

namespace SdlSharp.Input
{
    public sealed unsafe class GameController : NativePointerBase<Native.SDL_GameController, GameController>
    {
        /// <summary>
        /// The number of mappings for game controllers.
        /// </summary>
        public static int MappingsCount =>
            Native.SDL_GameControllerNumMappings();

        /// <summary>
        /// The mapping for this game controller.
        /// </summary>
        public string? Mapping
        {
            get
            {
                using var mapping = Native.SDL_GameControllerMapping(Pointer);
                return mapping.ToString();
            }
        }

        /// <summary>
        /// The name of the game controller, if any.
        /// </summary>
        public string? Name =>
            Native.SDL_GameControllerName(Pointer);

        /// <summary>
        /// The player index assigned to the controller.
        /// </summary>
        public int PlayerIndex
        {
            get => Native.SDL_GameControllerGetPlayerIndex(Pointer);
            set => Native.SDL_GameControllerSetPlayerIndex(Pointer, value);
        }

        /// <summary>
        /// The vendor code of the game controller
        /// </summary>
        public ushort Vendor =>
            Native.SDL_GameControllerGetVendor(Pointer);

        /// <summary>
        /// The product code of the game controller.
        /// </summary>
        public ushort Product =>
            Native.SDL_GameControllerGetProduct(Pointer);

        /// <summary>
        /// The product version of the game controller.
        /// </summary>
        public ushort ProductVersion =>
            Native.SDL_GameControllerGetProductVersion(Pointer);

        /// <summary>
        /// Whether the game controller is attached.
        /// </summary>
        public bool Attached =>
            Native.SDL_GameControllerGetAttached(Pointer);

        /// <summary>
        /// Gets the underlying joystick.
        /// </summary>
        public Joystick Joystick =>
            Joystick.PointerToInstanceNotNull(Native.SDL_GameControllerGetJoystick(Pointer));

        /// <summary>
        /// Gets the type of the game controller.
        /// </summary>
        public GameControllerType Type =>
            Native.SDL_GameControllerGetType(Pointer);

        /// <summary>
        /// An event fired when a game controller is added to the system.
        /// </summary>
        public static event EventHandler<GameControllerAddedEventArgs>? Added;

        /// <summary>
        /// An event fired when the game controller axis is moved.
        /// </summary>
        public event EventHandler<GameControllerAxisMotionEventArgs>? AxisMotion;

        /// <summary>
        /// An event fired when the game controller button is pressed.
        /// </summary>
        public event EventHandler<GameControllerButtonEventArgs>? ButtonDown;

        /// <summary>
        /// An event fired when the game controller button is released.
        /// </summary>
        public event EventHandler<GameControllerButtonEventArgs>? ButtonUp;

        /// <summary>
        /// An event fired when a game controller is removed.
        /// </summary>
        public event EventHandler<SdlEventArgs>? Removed;

        /// <summary>
        /// An event fired when a game controller is remapped.
        /// </summary>
        public event EventHandler<SdlEventArgs>? Remapped;

        internal static GameController? Get(Native.SDL_JoystickID joystickId) =>
            PointerToInstance(Native.SDL_GameControllerFromInstanceID(joystickId));

        /// <summary>
        /// Gets the game controller corresponding to the player index.
        /// </summary>
        /// <param name="playerIndex">The index of the player.</param>
        /// <returns>The game controller corresponding to the player.</returns>
        public static GameController? Get(int playerIndex) =>
            PointerToInstance(Native.SDL_GameControllerFromPlayerIndex(playerIndex));

        /// <inheritdoc/>
        public override void Dispose()
        {
            Native.SDL_GameControllerClose(Pointer);
            base.Dispose();
        }

        /// <summary>
        /// Adds game controller mappings from the data source.
        /// </summary>
        /// <param name="rwops">The data source.</param>
        /// <param name="shouldDispose">Whether the data source should be disposed after use.</param>
        /// <returns>The number of mappings added.</returns>
        public static int AddMappings(RWOps rwops, bool shouldDispose) =>
            Native.CheckError(Native.SDL_GameControllerAddMappingsFromRW(rwops.Pointer, shouldDispose));

        /// <summary>
        /// Adds game controller mappings from a file.
        /// </summary>
        /// <param name="filename">The filename that holds the mappings.</param>
        /// <returns>The number of mappings added.</returns>
        public static int AddMappings(string filename) =>
            Native.CheckError(Native.SDL_GameControllerAddMappingsFromFile(filename));

        /// <summary>
        /// Adds a game controller mapping.
        /// </summary>
        /// <param name="mapping">The mapping.</param>
        /// <returns><c>true</c> if the mapping was added, <c>false</c> if it updated an existing mapping.</returns>
        public static bool AddMapping(string mapping) =>
            Native.CheckError(Native.SDL_GameControllerAddMapping(mapping)) == 1;

        /// <summary>
        /// Gets a controller mapping.
        /// </summary>
        /// <param name="index">The index of the mapping.</param>
        /// <returns>The mapping, if it exists.</returns>
        public static string? GetMapping(int index)
        {
            using var mapping = Native.SDL_GameControllerMappingForIndex(index);
            return mapping.ToString();
        }

        /// <summary>
        /// Gets a controller mapping.
        /// </summary>
        /// <param name="guid">The GUID of the mapping.</param>
        /// <returns>The mapping, if it exists.</returns>
        public static string? GetMapping(Guid guid)
        {
            using var mapping = Native.SDL_GameControllerMappingForGUID(guid);
            return mapping.ToString();
        }

        /// <summary>
        /// Sets whether game controller events are polled.
        /// </summary>
        /// <param name="state">The state.</param>
        /// <returns>The previous state.</returns>
        public static State SetEventState(State state) =>
            (State)Native.SDL_GameControllerEventState(state);

        /// <summary>
        /// Polls game controllers for events.
        /// </summary>
        public static void Update() =>
            Native.SDL_GameControllerUpdate();

        /// <summary>
        /// Maps a name to an axis.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>The axis.</returns>
        public static GameControllerAxis GetAxisFromString(string name) =>
            Native.SDL_GameControllerGetAxisFromString(name);

        /// <summary>
        /// Maps an axis to a name.
        /// </summary>
        /// <param name="axis">The axis.</param>
        /// <returns>The name.</returns>
        public static string GetStringForAxis(GameControllerAxis axis) =>
            Native.SDL_GameControllerGetStringForAxis(axis);

        /// <summary>
        /// Gets the binding for the axis.
        /// </summary>
        /// <param name="axis">The axis.</param>
        /// <returns>The binding.</returns>
        public GameControllerBinding? GetAxisBinding(GameControllerAxis axis) =>
            GameControllerBinding.FromNative(Native.SDL_GameControllerGetBindForAxis(Pointer, axis));

        /// <summary>
        /// The axis value.
        /// </summary>
        /// <param name="axis">The axis.</param>
        /// <returns>The value.</returns>
        public short GetAxis(GameControllerAxis axis) =>
            Native.SDL_GameControllerGetAxis(Pointer, axis);

        /// <summary>
        /// Maps a name to an button.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>The button.</returns>
        public static GameControllerButton GetButtonFromString(string s) =>
            Native.SDL_GameControllerGetButtonFromString(s);

        /// <summary>
        /// Maps an button to a name.
        /// </summary>
        /// <param name="button">The button.</param>
        /// <returns>The name.</returns>
        public static string GetStringForButton(GameControllerButton button) =>
            Native.SDL_GameControllerGetStringForButton(button);

        /// <summary>
        /// Gets the binding for the button.
        /// </summary>
        /// <param name="button">The button.</param>
        /// <returns>The binding.</returns>
        public GameControllerBinding? GetButtonBinding(GameControllerButton button) =>
            GameControllerBinding.FromNative(Native.SDL_GameControllerGetBindForButton(Pointer, button));

        /// <summary>
        /// The button value.
        /// </summary>
        /// <param name="button">The button.</param>
        /// <returns>Whether the button is pressed.</returns>
        public bool GetButton(GameControllerButton button) =>
            Native.SDL_GameControllerGetButton(Pointer, button);

        /// <summary>
        /// Rumbles the game controller.
        /// </summary>
        /// <param name="lowFrequency">The low frequency.</param>
        /// <param name="highFrequency">The high frequency.</param>
        /// <param name="duration">The duration.</param>
        public void Rumble(ushort lowFrequency, ushort highFrequency, uint duration) =>
            Native.CheckError(Native.SDL_GameControllerRumble(Pointer, lowFrequency, highFrequency, duration));

        internal static void DispatchEvent(Native.SDL_Event e)
        {
            switch (e.Type)
            {
                case Native.SDL_EventType.ControllerAxisMotion:
                    {
                        var controller = Get(e.Caxis.Which);
                        controller?.AxisMotion?.Invoke(controller, new GameControllerAxisMotionEventArgs(e.Caxis));
                        break;
                    }

                case Native.SDL_EventType.ControllerButtonDown:
                    {
                        var controller = Get(e.Cbutton.Which);
                        controller?.ButtonDown?.Invoke(controller, new GameControllerButtonEventArgs(e.Cbutton));
                        break;
                    }

                case Native.SDL_EventType.ControllerButtonUp:
                    {
                        var controller = Get(e.Cbutton.Which);
                        controller?.ButtonUp?.Invoke(controller, new GameControllerButtonEventArgs(e.Cbutton));
                        break;
                    }

                case Native.SDL_EventType.ControllerDeviceAdded:
                    {
                        Added?.Invoke(null, new GameControllerAddedEventArgs(e.Cdevice));
                        break;
                    }

                case Native.SDL_EventType.ControllerDeviceRemoved:
                    {
                        var controller = Get(new Native.SDL_JoystickID(e.Cdevice.Which));
                        controller?.Removed?.Invoke(controller, new SdlEventArgs(e.Common));
                        break;
                    }

                case Native.SDL_EventType.ControllerDeviceRemapped:
                    {
                        var controller = Get(new Native.SDL_JoystickID(e.Cdevice.Which));
                        controller?.Remapped?.Invoke(controller, new SdlEventArgs(e.Common));
                        break;
                    }

                default:
                    throw new InvalidOperationException();
            }
        }
    }
}
