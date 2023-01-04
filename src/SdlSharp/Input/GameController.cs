namespace SdlSharp.Input
{
    /// <summary>
    /// A game controller.
    /// </summary>
    public sealed unsafe class GameController : IDisposable
    {
        private readonly Native.SDL_GameController* _gameController;

        /// <summary>
        /// The mappings for game controllers.
        /// </summary>
        public static IReadOnlyList<string> Mappings => Native.GetIndexedCollection(
            i => Native.Utf8ToStringAndFree(Native.SDL_GameControllerMappingForIndex(i))!,
            Native.SDL_GameControllerNumMappings);

        /// <summary>
        /// The game controller's ID.
        /// </summary>
        public nuint Id => (nuint)_gameController;

        /// <summary>
        /// The mapping for this game controller.
        /// </summary>
        public string? Mapping => Native.Utf8ToStringAndFree(Native.SDL_GameControllerMapping(_gameController));

        /// <summary>
        /// The name of the game controller, if any.
        /// </summary>
        public string? Name =>
            Native.Utf8ToString(Native.SDL_GameControllerName(_gameController));

        /// <summary>
        /// The path of the game controller, if any.
        /// </summary>
        public string? Path =>
            Native.Utf8ToString(Native.SDL_GameControllerPath(_gameController));

        /// <summary>
        /// The player index assigned to the controller.
        /// </summary>
        public int PlayerIndex
        {
            get => Native.SDL_GameControllerGetPlayerIndex(_gameController);
            set => Native.SDL_GameControllerSetPlayerIndex(_gameController, value);
        }

        /// <summary>
        /// The vendor code of the game controller
        /// </summary>
        public ushort Vendor =>
            Native.SDL_GameControllerGetVendor(_gameController);

        /// <summary>
        /// The product code of the game controller.
        /// </summary>
        public ushort Product =>
            Native.SDL_GameControllerGetProduct(_gameController);

        /// <summary>
        /// The product version of the game controller.
        /// </summary>
        public ushort ProductVersion =>
            Native.SDL_GameControllerGetProductVersion(_gameController);

        /// <summary>
        /// The firmware version of the game controller.
        /// </summary>
        public ushort FirmwareVersion =>
            Native.SDL_GameControllerGetFirmwareVersion(_gameController);

        /// <summary>
        /// The serial number of the game controller.
        /// </summary>
        public string? Serial =>
            Native.Utf8ToString(Native.SDL_GameControllerGetSerial(_gameController));

        /// <summary>
        /// Whether the game controller is attached.
        /// </summary>
        public bool Attached =>
            Native.SDL_GameControllerGetAttached(_gameController);

        /// <summary>
        /// Gets the underlying joystick.
        /// </summary>
        public Joystick Joystick =>
            new(Native.SDL_GameControllerGetJoystick(_gameController));

        /// <summary>
        /// Gets the type of the game controller.
        /// </summary>
        public GameControllerType Type =>
            (GameControllerType)Native.SDL_GameControllerGetType(_gameController);

        /// <summary>
        /// Gets the list of touchpads supported by this game controller.
        /// </summary>
        public IReadOnlyList<int> Touchpads => Native.GetIndexedCollection(i =>
            Native.SDL_GameControllerGetNumTouchpadFingers(_gameController, i),
            () => Native.SDL_GameControllerGetNumTouchpads(_gameController));

        /// <summary>
        /// Whether the controller has LED lights.
        /// </summary>
        public bool HasLed => Native.SDL_GameControllerHasLED(_gameController);

        /// <summary>
        /// Whether the controller supports rumbling.
        /// </summary>
        public bool HasRumble => Native.SDL_GameControllerHasRumble(_gameController);

        /// <summary>
        /// Whether the controller has triggers that support rumbling.
        /// </summary>
        public bool HasRumbleTriggers => Native.SDL_GameControllerHasRumbleTriggers(_gameController);

        /// <summary>
        /// Whether game controller events are polled.
        /// </summary>
        public static bool EventEnabled
        {
            get => Native.SDL_GameControllerEventState(Native.SDL_QUERY) == Native.SDL_ENABLE;
            set => _ = Native.SDL_GameControllerEventState(value ? Native.SDL_ENABLE : Native.SDL_DISABLE);
        }

        /// <summary>
        /// An event fired when a game controller is added to the system.
        /// </summary>
        public static event EventHandler<GameControllerAddedEventArgs>? Added;

        /// <summary>
        /// An event fired when the game controller axis is moved.
        /// </summary>
        public static event EventHandler<GameControllerAxisMotionEventArgs>? AxisMotion;

        /// <summary>
        /// An event fired when the game controller button is pressed.
        /// </summary>
        public static event EventHandler<GameControllerButtonEventArgs>? ButtonDown;

        /// <summary>
        /// An event fired when the game controller button is released.
        /// </summary>
        public static event EventHandler<GameControllerButtonEventArgs>? ButtonUp;

        /// <summary>
        /// An event fired when a game controller is removed.
        /// </summary>
        public static event EventHandler<SdlEventArgs>? Removed;

        /// <summary>
        /// An event fired when a game controller is remapped.
        /// </summary>
        public static event EventHandler<SdlEventArgs>? Remapped;

        /// <summary>
        /// An event fired when a finger goes down on a controller touchpad.
        /// </summary>
        public static event EventHandler<GameControllerTouchpadEventArgs>? TouchPadDown;

        /// <summary>
        /// An event fired when a finger moves on a controller touchpad.
        /// </summary>
        public static event EventHandler<GameControllerTouchpadEventArgs>? TouchPadMotion;

        /// <summary>
        /// An event fired when a finger goes up on a controller touchpad.
        /// </summary>
        public static event EventHandler<GameControllerTouchpadEventArgs>? TouchPadUp;

        /// <summary>
        /// An event fired when a sensor on a controller updates.
        /// </summary>
        public static event EventHandler<GameControllerSensorEventArgs>? SensorUpdate;

        internal GameController(Native.SDL_GameController* gameController)
        {
            _gameController = gameController;
        }

        internal GameController(Native.SDL_JoystickID joystickId)
            : this(Native.CheckPointer(Native.SDL_GameControllerFromInstanceID(joystickId)))
        {
        }

        /// <summary>
        /// Gets the game controller corresponding to the player index.
        /// </summary>
        /// <param name="playerIndex">The index of the player.</param>
        /// <returns>The game controller corresponding to the player.</returns>
        public static GameController? GetPlayer(int playerIndex) =>
            new(Native.CheckPointer(Native.SDL_GameControllerFromPlayerIndex(playerIndex)));

        /// <summary>
        /// Disposes a controller.
        /// </summary>
        public void Dispose() => Native.SDL_GameControllerClose(_gameController);

        /// <summary>
        /// Adds game controller mappings from the data source.
        /// </summary>
        /// <param name="rwops">The data source.</param>
        /// <param name="shouldDispose">Whether the data source should be disposed after use.</param>
        /// <returns>The number of mappings added.</returns>
        public static int AddMappings(RWOps rwops, bool shouldDispose) =>
            Native.CheckError(Native.SDL_GameControllerAddMappingsFromRW(rwops.ToNative(), Native.BoolToInt(shouldDispose)));

        /// <summary>
        /// Adds game controller mappings from a file.
        /// </summary>
        /// <param name="filename">The filename that holds the mappings.</param>
        /// <returns>The number of mappings added.</returns>
        public static int AddMappings(string filename)
        {
            fixed (byte* ptr = Native.StringToUtf8(filename))
            {
                return Native.CheckError(Native.SDL_GameControllerAddMappingsFromFile(ptr));
            }
        }

        /// <summary>
        /// Adds a game controller mapping.
        /// </summary>
        /// <param name="mapping">The mapping.</param>
        /// <returns><c>true</c> if the mapping was added, <c>false</c> if it updated an existing mapping.</returns>
        public static bool AddMapping(string mapping)
        {
            fixed (byte* ptr = Native.StringToUtf8(mapping))
            {
                return Native.CheckError(Native.SDL_GameControllerAddMapping(ptr)) == 1;
            }
        }

        /// <summary>
        /// Gets a controller mapping for a particular joystick ID.
        /// </summary>
        /// <param name="joystickId">The joystick ID.</param>
        /// <returns>The mapping, if it exists.</returns>
        public static string? GetJoystickMapping(Guid joystickId) =>
            Native.Utf8ToStringAndFree(Native.SDL_GameControllerMappingForGUID(joystickId));

        /// <summary>
        /// Polls game controllers for events.
        /// </summary>
        public static void Update() =>
            Native.SDL_GameControllerUpdate();

        /// <summary>
        /// Gets the binding for the axis.
        /// </summary>
        /// <param name="axis">The axis.</param>
        /// <returns>The binding.</returns>
        public GameControllerBinding? GetAxisBinding(GameControllerAxis axis) =>
            GameControllerBinding.FromNative(Native.SDL_GameControllerGetBindForAxis(_gameController, axis.ToNative()));

        /// <summary>
        /// Whether the game controller has the axis.
        /// </summary>
        /// <param name="axis">The axis.</param>
        /// <returns>Whether the controller has it.</returns>
        public bool HasAxis(GameControllerAxis axis) =>
            Native.SDL_GameControllerHasAxis(_gameController, axis.ToNative());

        /// <summary>
        /// The axis value.
        /// </summary>
        /// <param name="axis">The axis.</param>
        /// <returns>The value.</returns>
        public short GetAxis(GameControllerAxis axis) =>
            Native.SDL_GameControllerGetAxis(_gameController, axis.ToNative());

        /// <summary>
        /// Gets the Apple SFSymbols name for the axis, if any.
        /// </summary>
        /// <param name="axis">The axis.</param>
        /// <returns>The name.</returns>
        public string? GetAppleSfSymbolsNameForAxis(GameControllerAxis axis) =>
            Native.Utf8ToString(Native.SDL_GameControllerGetAppleSFSymbolsNameForAxis(_gameController, axis.ToNative()));

        /// <summary>
        /// Gets the binding for the button.
        /// </summary>
        /// <param name="button">The button.</param>
        /// <returns>The binding.</returns>
        public GameControllerBinding? GetButtonBinding(GameControllerButton button) =>
            GameControllerBinding.FromNative(Native.SDL_GameControllerGetBindForButton(_gameController, button.ToNative()));

        /// <summary>
        /// Whether the game controller has the button.
        /// </summary>
        /// <param name="button">The button.</param>
        /// <returns>Whether the controller has it.</returns>
        public bool HasButton(GameControllerButton button) =>
            Native.SDL_GameControllerHasButton(_gameController, button.ToNative());

        /// <summary>
        /// The button value.
        /// </summary>
        /// <param name="button">The button.</param>
        /// <returns>Whether the button is pressed.</returns>
        public bool GetButton(GameControllerButton button) =>
            Native.SDL_GameControllerGetButton(_gameController, button.ToNative()) == 1;

        /// <summary>
        /// Gets the Apple SFSymbols name for the button, if any.
        /// </summary>
        /// <param name="button">The button.</param>
        /// <returns>The name.</returns>
        public string? GetAppleSfSymbolsNameForButton(GameControllerButton button) =>
            Native.Utf8ToString(Native.SDL_GameControllerGetAppleSFSymbolsNameForButton(_gameController, button.ToNative()));

        /// <summary>
        /// Gets the state of a touchpad finger.
        /// </summary>
        /// <param name="touchpad">The touchpad.</param>
        /// <param name="finger">The finger.</param>
        /// <returns>The state of the finger.</returns>
        public (byte state, PointF point, float pressure) GetTouchpadFinger(int touchpad, int finger)
        {
            byte state;
            float x;
            float y;
            float pressure;

            _ = Native.CheckError(Native.SDL_GameControllerGetTouchpadFinger(_gameController, touchpad, finger, &state, &x, &y, &pressure));
            return (state, new(x, y), pressure);
        }

        /// <summary>
        /// Returns a sensor (if supported) of the game controller.
        /// </summary>
        /// <param name="type">The type of the sensor.</param>
        /// <returns>The sensor, or null if the game controller doesn't support the sensor.</returns>
        public GameControllerSensor? GetSensor(SensorType type)
        {
            return Native.SDL_GameControllerHasSensor(_gameController, (Native.SDL_SensorType)type)
                ? new GameControllerSensor(_gameController, (Native.SDL_SensorType)type)
                : null;
        }

        /// <summary>
        /// Rumbles the game controller.
        /// </summary>
        /// <param name="lowFrequency">The low frequency.</param>
        /// <param name="highFrequency">The high frequency.</param>
        /// <param name="duration">The duration.</param>
        public void Rumble(ushort lowFrequency, ushort highFrequency, uint duration) =>
            Native.CheckError(Native.SDL_GameControllerRumble(_gameController, lowFrequency, highFrequency, duration));

        /// <summary>
        /// Rumbles the game controller triggers.
        /// </summary>
        /// <param name="left">The intensity of the left trigger rumble.</param>
        /// <param name="right">The intensity of the right trigger rumble.</param>
        /// <param name="duration">The duration of the rumble in milliseconds.</param>
        public void RumbleTriggers(ushort left, ushort right, uint duration) =>
            Native.CheckError(Native.SDL_GameControllerRumbleTriggers(_gameController, left, right, duration));

        /// <summary>
        /// Set the LED lights of the controller.
        /// </summary>
        /// <param name="red">The red value.</param>
        /// <param name="green">The green value.</param>
        /// <param name="blue">The blue value.</param>
        public void SetLed(byte red, byte green, byte blue) =>
            _ = Native.CheckError(Native.SDL_GameControllerSetLED(_gameController, red, green, blue));

        /// <summary>
        /// Sends a controller-specific effect.
        /// </summary>
        /// <param name="effect">The effect.</param>
        public void SendEffect(Span<byte> effect)
        {
            fixed (byte* effectPtr = effect)
            {
                _ = Native.CheckError(Native.SDL_GameControllerSendEffect(_gameController, effectPtr, effect.Length));
            }
        }

        internal static void DispatchEvent(Native.SDL_Event e)
        {
            switch ((Native.SDL_EventType)e.type)
            {
                case Native.SDL_EventType.SDL_CONTROLLERAXISMOTION:
                    AxisMotion?.Invoke(new GameController(e.caxis.which), new GameControllerAxisMotionEventArgs(e.caxis));
                    break;

                case Native.SDL_EventType.SDL_CONTROLLERBUTTONDOWN:
                    ButtonDown?.Invoke(new GameController(e.cbutton.which), new GameControllerButtonEventArgs(e.cbutton));
                    break;

                case Native.SDL_EventType.SDL_CONTROLLERBUTTONUP:
                    ButtonUp?.Invoke(new GameController(e.caxis.which), new GameControllerButtonEventArgs(e.cbutton));
                    break;

                case Native.SDL_EventType.SDL_CONTROLLERDEVICEADDED:
                    Added?.Invoke(null, new GameControllerAddedEventArgs(e.cdevice));
                    break;

                case Native.SDL_EventType.SDL_CONTROLLERDEVICEREMOVED:
                    Removed?.Invoke(new GameController(new Native.SDL_JoystickID(e.cdevice.which)), new SdlEventArgs(e.common));
                    break;

                case Native.SDL_EventType.SDL_CONTROLLERDEVICEREMAPPED:
                    Remapped?.Invoke(new GameController(new Native.SDL_JoystickID(e.cdevice.which)), new SdlEventArgs(e.common));
                    break;

                case Native.SDL_EventType.SDL_CONTROLLERTOUCHPADDOWN:
                    TouchPadDown?.Invoke(new GameController(e.ctouchpad.which), new GameControllerTouchpadEventArgs(e.ctouchpad));
                    break;

                case Native.SDL_EventType.SDL_CONTROLLERTOUCHPADMOTION:
                    TouchPadMotion?.Invoke(new GameController(e.ctouchpad.which), new GameControllerTouchpadEventArgs(e.ctouchpad));
                    break;

                case Native.SDL_EventType.SDL_CONTROLLERTOUCHPADUP:
                    TouchPadUp?.Invoke(new GameController(e.ctouchpad.which), new GameControllerTouchpadEventArgs(e.ctouchpad));
                    break;

                case Native.SDL_EventType.SDL_CONTROLLERSENSORUPDATE:
                    SensorUpdate?.Invoke(new GameController(e.csensor.which), new GameControllerSensorEventArgs(e.csensor));
                    break;

                default:
                    throw new InvalidOperationException();
            }
        }
    }
}
