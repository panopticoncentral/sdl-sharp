namespace SdlSharp.Input
{
    /// <summary>
    /// A joystick.
    /// </summary>
    public sealed unsafe class Joystick : IDisposable
    {
        private readonly Native.SDL_Joystick* _joystick;

        /// <summary>
        /// The joysticks in the system.
        /// </summary>
        public static IReadOnlyList<JoystickInfo> Infos =>
            Native.GetIndexedCollection(i => new JoystickInfo(i), Native.SDL_NumJoysticks);

        /// <summary>
        /// The ID of the joystick.
        /// </summary>
        public nuint Id => (nuint)_joystick;

        /// <summary>
        /// The name of the joystick, if any.
        /// </summary>
        public string? Name => Native.Utf8ToString(Native.CheckPointer(Native.SDL_JoystickName(_joystick)));

        /// <summary>
        /// The path of the joystick, if any.
        /// </summary>
        public string? Path => Native.Utf8ToString(Native.CheckPointer(Native.SDL_JoystickPath(_joystick)));

        /// <summary>
        /// The player index assigned to this joystick.
        /// </summary>
        public int PlayerIndex
        {
            get => Native.SDL_JoystickGetPlayerIndex(_joystick);
            set => Native.SDL_JoystickSetPlayerIndex(_joystick, value);
        }

        /// <summary>
        /// The GUID of this joystick.
        /// </summary>
        public Guid JoystickId => Native.SDL_JoystickGetGUID(_joystick);

        /// <summary>
        /// The vendor code for this joystick.
        /// </summary>
        public ushort Vendor => Native.SDL_JoystickGetVendor(_joystick);

        /// <summary>
        /// The product code for this joystick.
        /// </summary>
        public ushort Product => Native.SDL_JoystickGetProduct(_joystick);

        /// <summary>
        /// The product version of this joystick.
        /// </summary>
        public ushort ProductVersion => Native.SDL_JoystickGetProductVersion(_joystick);

        /// <summary>
        /// The firmware version of this joystick.
        /// </summary>
        public ushort FirmwareVersion => Native.SDL_JoystickGetFirmwareVersion(_joystick);

        /// <summary>
        /// The serial number of this joystick.
        /// </summary>
        public string? Serial => Native.Utf8ToString(Native.SDL_JoystickGetSerial(_joystick));

        /// <summary>
        /// The type of the joystick.
        /// </summary>
        public JoystickType Type => (JoystickType)Native.SDL_JoystickGetType(_joystick);

        /// <summary>
        /// Whether the joystick is attached.
        /// </summary>
        public bool Attached => Native.SDL_JoystickGetAttached(_joystick);

        /// <summary>
        /// The number of axes in the joystick.
        /// </summary>
        public int Axes => Native.SDL_JoystickNumAxes(_joystick);

        /// <summary>
        /// The number of balls in the joystick.
        /// </summary>
        public int Balls => Native.SDL_JoystickNumBalls(_joystick);

        /// <summary>
        /// The number of hats in the joystick.
        /// </summary>
        public int Hats => Native.SDL_JoystickNumHats(_joystick);

        /// <summary>
        /// The number of buttons in the joystick.
        /// </summary>
        public int Buttons => Native.SDL_JoystickNumButtons(_joystick);

        /// <summary>
        /// Whether the joystick has LED lights.
        /// </summary>
        public bool HasLed => Native.SDL_JoystickHasLED(_joystick);

        /// <summary>
        /// Whether the joystick supports rumble.
        /// </summary>
        public bool HasRumble => Native.SDL_JoystickHasRumble(_joystick);

        /// <summary>
        /// Whether the joystick supports rumble triggers.
        /// </summary>
        public bool HasRumbleTriggers => Native.SDL_JoystickHasRumbleTriggers(_joystick);

        /// <summary>
        /// The power level of the joystick.
        /// </summary>
        public JoystickPowerLevel PowerLevel => (JoystickPowerLevel)Native.SDL_JoystickCurrentPowerLevel(_joystick);

        /// <summary>
        /// The game controller associated with this joystick.
        /// </summary>
        public GameController GameController =>
            new(Native.CheckPointer(Native.SDL_GameControllerFromInstanceID(Native.CheckError(Native.SDL_JoystickInstanceID(_joystick), joystickId => joystickId.Id >= 0))));

        /// <summary>
        /// Whether the joystick supports haptic effects.
        /// </summary>
        public bool IsHaptic =>
            Native.SDL_JoystickIsHaptic(_joystick) != 0;

        /// <summary>
        /// Whether game controller events are polled.
        /// </summary>
        public static bool EventEnabled
        {
            get => Native.SDL_JoystickEventState(Native.SDL_QUERY) == Native.SDL_ENABLE;
            set => _ = Native.SDL_JoystickEventState(value ? Native.SDL_ENABLE : Native.SDL_DISABLE);
        }

        /// <summary>
        /// An event that is fired when a joystick is added.
        /// </summary>
        public static event EventHandler<JoystickAddedEventArgs>? Added;

        /// <summary>
        /// An event that fires when there is motion on an axis.
        /// </summary>
        public static event EventHandler<JoystickAxisMotionEventArgs>? AxisMotion;

        /// <summary>
        /// An event that fires when there is motion on a ball.
        /// </summary>
        public static event EventHandler<JoystickBallMotionEventArgs>? BallMotion;

        /// <summary>
        /// An event that fires when a button is pressed.
        /// </summary>
        public static event EventHandler<JoystickButtonEventArgs>? ButtonDown;

        /// <summary>
        /// An event that fires when a button is released.
        /// </summary>
        public static event EventHandler<JoystickButtonEventArgs>? ButtonUp;

        /// <summary>
        /// An event that is fired when a joystick is removed.
        /// </summary>
        public static event EventHandler<SdlEventArgs>? Removed;

        /// <summary>
        /// An event that fires when a hat moves.
        /// </summary>
        public static event EventHandler<JoystickHatMotionEventArgs>? HatMotion;

        /// <summary>
        /// An event that fires when a joystick's battery status is updated.
        /// </summary>
        public static event EventHandler<JoystickBatteryUpdatedEventArgs>? BatteryUpdated;

        internal Joystick(Native.SDL_JoystickID instanceId)
            : this(Native.SDL_JoystickFromInstanceID(instanceId))
        {
        }

        internal Joystick(Native.SDL_Joystick* joystick)
        {
            _joystick = joystick;
        }

        /// <summary>
        /// Gets the joystick that corresponds to the player index.
        /// </summary>
        /// <param name="playerIndex">The player index.</param>
        /// <returns>The joystick that corresponds to the player index.</returns>
        public static Joystick Get(int playerIndex) =>
            new(Native.SDL_JoystickFromPlayerIndex(playerIndex));

        /// <summary>
        /// Attaches a new virtual joystick to the system.
        /// </summary>
        /// <param name="type">The type of the joystick.</param>
        /// <param name="axes">The number of axes on the joystick.</param>
        /// <param name="buttons">The number of buttons on the joystick.</param>
        /// <param name="hats">The number of hats on the joystick.</param>
        /// <returns>The info of the new joystick.</returns>
        public static JoystickInfo AttachVirtual(JoystickType type, int axes, int buttons, int hats) =>
            new(Native.CheckError(Native.SDL_JoystickAttachVirtual((Native.SDL_JoystickType)type, axes, buttons, hats)));

        /// <summary>
        /// Attaches a new virtual joystick to the system.
        /// </summary>
        /// <param name="joystick">The description of the joystick.</param>
        /// <returns>The joystick info.</returns>
        public static JoystickInfo AttachVirtual(VirtualJoystickBase joystick)
        {
            fixed (byte* namePtr = Native.StringToUtf8(joystick.Name))
            {
                Native.SDL_VirtualJoystickDesc desc = new()
                {
                    version = Native.SDL_VIRTUAL_JOYSTICK_DESC_VERSION,
                    type = (ushort)joystick.Type,
                    naxes = joystick.Axes,
                    nbuttons = joystick.Buttons,
                    nhats = joystick.Hats,
                    vendor_id = joystick.VendorId,
                    product_id = joystick.ProductId,
                    button_mask = joystick.ButtonMask,
                    axis_mask = joystick.AxisMask,
                    name = namePtr,
                    Update = &VirtualJoystickBase.UpdateCallback,
                    SetPlayerIndex = &VirtualJoystickBase.SetPlayerIndexCallback,
                    Rumble = &VirtualJoystickBase.RumbleCallback,
                    RumbleTriggers = &VirtualJoystickBase.RumbleTriggersCallback,
                    SetLED = &VirtualJoystickBase.SetLedCallback,
                    SendEffect = &VirtualJoystickBase.SendEffectCallback
                };

                return new(Native.CheckError(Native.SDL_JoystickAttachVirtualEx(&desc)));
            }
        }

        /// <summary>
        /// Locks the joysticks.
        /// </summary>
        public static void LockJoysticks() =>
            Native.SDL_LockJoysticks();

        /// <summary>
        /// Unlocks the joysticks.
        /// </summary>
        public static void UnlockJoysticks() =>
            Native.SDL_UnlockJoysticks();

        /// <summary>
        /// Polls joysticks for events.
        /// </summary>
        public static void Update() =>
            Native.SDL_JoystickUpdate();

        /// <inheritdoc/>
        public void Dispose() => Native.SDL_JoystickClose(_joystick);

        /// <summary>
        /// Returns the haptic support for the joystick.
        /// </summary>
        public Haptic GetHaptic() =>
            new(Native.CheckPointer(Native.SDL_HapticOpenFromJoystick(_joystick)));

        /// <summary>
        /// Gets the value of the axis.
        /// </summary>
        /// <param name="axis">The axis.</param>
        /// <returns>The value.</returns>
        public short GetAxis(int axis) =>
            Native.SDL_JoystickGetAxis(_joystick, axis);

        /// <summary>
        /// Gets the axis's initial state.
        /// </summary>
        /// <param name="axis">The axis.</param>
        /// <param name="state">The initial state.</param>
        /// <returns>Whether the axis exists.</returns>
        public bool GetAxisInitialState(int axis, out short state)
        {
            fixed (short* ptr = &state)
            {
                return Native.SDL_JoystickGetAxisInitialState(_joystick, axis, ptr);
            }
        }

        /// <summary>
        /// Gets the value of the hat.
        /// </summary>
        /// <param name="hat">The hat.</param>
        /// <returns>The value.</returns>
        public HatState GetHat(int hat) =>
            (HatState)Native.SDL_JoystickGetHat(_joystick, hat);

        /// <summary>
        /// Gets the value of the ball.
        /// </summary>
        /// <param name="ball">The ball.</param>
        /// <returns>The value.</returns>
        public (int XDelta, int YDelta) GetBall(int ball)
        {
            int dx;
            int dy;
            _ = Native.CheckError(Native.SDL_JoystickGetBall(_joystick, ball, &dx, &dy));
            return (dx, dy);
        }

        /// <summary>
        /// Gets the button state.
        /// </summary>
        /// <param name="button">The button.</param>
        /// <returns><c>true</c> if the button is pressed, <c>false</c> otherwise.</returns>
        public bool GetButton(int button) =>
            Native.SDL_JoystickGetButton(_joystick, button) != 0;

        /// <summary>
        /// Rumbles the joystick.
        /// </summary>
        /// <param name="lowFrequency">The low frequency.</param>
        /// <param name="highFrequency">The high frequency.</param>
        /// <param name="duration">The duration.</param>
        public void Rumble(ushort lowFrequency, ushort highFrequency, uint duration) =>
            Native.CheckError(Native.SDL_JoystickRumble(_joystick, lowFrequency, highFrequency, duration));

        /// <summary>
        /// Rumbles the joystick triggers.
        /// </summary>
        /// <param name="leftRumble">The left rumble.</param>
        /// <param name="rightRumble">The right rumble.</param>
        /// <param name="duration">The duration.</param>
        public void RumbleTriggers(ushort leftRumble, ushort rightRumble, uint duration) =>
            Native.CheckError(Native.SDL_JoystickRumbleTriggers(_joystick, leftRumble, rightRumble, duration));

        /// <summary>
        /// Sets the value of an axis on a virtual joystick.
        /// </summary>
        /// <param name="axis">The axis.</param>
        /// <param name="value">The value.</param>
        public void SetVirtualAxis(int axis, short value) =>
            Native.CheckError(Native.SDL_JoystickSetVirtualAxis(_joystick, axis, value));

        /// <summary>
        /// Sets the value of a button on a virtual joystick.
        /// </summary>
        /// <param name="button">The button.</param>
        /// <param name="value">The value.</param>
        public void SetVirtualButton(int button, bool value) =>
            Native.CheckError(Native.SDL_JoystickSetVirtualButton(_joystick, button, (byte)(value ? 1 : 0)));

        /// <summary>
        /// Sets the value of a hat on a virtual joystick.
        /// </summary>
        /// <param name="hat">The hat.</param>
        /// <param name="value">The value.</param>
        public void SetVirtualHat(int hat, HatState value) =>
            Native.CheckError(Native.SDL_JoystickSetVirtualHat(_joystick, hat, (byte)value));

        /// <summary>
        /// Gets the joystick information encoded in its ID.
        /// </summary>
        /// <param name="id">The joystick ID.</param>
        /// <param name="vendor">The vendor.</param>
        /// <param name="product">The product.</param>
        /// <param name="version">The version.</param>
        /// <param name="crc16">The distinguishing CRC, if any.</param>
        public void GetIdInfo(Guid id, out ushort vendor, out ushort product, out ushort version, out ushort crc16)
        {
            fixed (ushort* vendorPtr = &vendor)
            fixed (ushort* productPtr = &product)
            fixed (ushort* versionPtr = &version)
            fixed (ushort* crcPtr = &crc16)
            {
                Native.SDL_GetJoystickGUIDInfo(id, vendorPtr, productPtr, versionPtr, crcPtr);
            }
        }

        /// <summary>
        /// Set the LED lights of the joystick.
        /// </summary>
        /// <param name="red">The red value.</param>
        /// <param name="green">The green value.</param>
        /// <param name="blue">The blue value.</param>
        public void SetLed(byte red, byte green, byte blue) =>
            _ = Native.CheckError(Native.SDL_JoystickSetLED(_joystick, red, green, blue));

        /// <summary>
        /// Sends a joystick-specific effect.
        /// </summary>
        /// <param name="effect">The effect.</param>
        public void SendEffect(Span<byte> effect)
        {
            fixed (byte* effectPtr = effect)
            {
                _ = Native.CheckError(Native.SDL_JoystickSendEffect(_joystick, effectPtr, effect.Length));
            }
        }

        internal static void DispatchEvent(Native.SDL_Event e)
        {
            switch ((Native.SDL_EventType)e.type)
            {
                case Native.SDL_EventType.SDL_JOYAXISMOTION:
                    AxisMotion?.Invoke(new Joystick(e.jaxis.which), new JoystickAxisMotionEventArgs(e.jaxis));
                    break;

                case Native.SDL_EventType.SDL_JOYBALLMOTION:
                    BallMotion?.Invoke(new Joystick(e.jball.which), new JoystickBallMotionEventArgs(e.jball));
                    break;

                case Native.SDL_EventType.SDL_JOYBUTTONDOWN:
                    ButtonDown?.Invoke(new Joystick(e.jbutton.which), new JoystickButtonEventArgs(e.jbutton));
                    break;

                case Native.SDL_EventType.SDL_JOYBUTTONUP:
                    ButtonUp?.Invoke(new Joystick(e.jbutton.which), new JoystickButtonEventArgs(e.jbutton));
                    break;

                case Native.SDL_EventType.SDL_JOYDEVICEADDED:
                    Added?.Invoke(null, new JoystickAddedEventArgs(e.jdevice));
                    break;

                case Native.SDL_EventType.SDL_JOYDEVICEREMOVED:
                    Removed?.Invoke(new Joystick(new Native.SDL_JoystickID(e.jdevice.which)), new SdlEventArgs(e.common));
                    break;

                case Native.SDL_EventType.SDL_JOYHATMOTION:
                    HatMotion?.Invoke(new Joystick(e.jhat.which), new JoystickHatMotionEventArgs(e.jhat));
                    break;

                case Native.SDL_EventType.SDL_JOYBATTERYUPDATED:
                    BatteryUpdated?.Invoke(new Joystick(e.jbattery.which), new JoystickBatteryUpdatedEventArgs(e.jbattery));
                    break;

                default:
                    throw new InvalidOperationException();
            }
        }
    }
}
