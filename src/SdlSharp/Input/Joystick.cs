using System;
using System.Collections.Generic;

namespace SdlSharp.Input
{
    /// <summary>
    /// A joystick.
    /// </summary>
    public unsafe sealed class Joystick : NativePointerBase<Native.SDL_Joystick, Joystick>
    {
        private static ItemCollection<JoystickInfo>? s_infos;

        /// <summary>
        /// The joysticks in the system.
        /// </summary>
        public static IReadOnlyList<JoystickInfo> Infos => s_infos ?? (s_infos = new ItemCollection<JoystickInfo>(JoystickInfo.Get, Native.SDL_NumJoysticks));

        /// <summary>
        /// The name of the joystick, if any.
        /// </summary>
        public string? Name => Native.SDL_JoystickName(Pointer);

        /// <summary>
        /// The player index assigned to this joystick.
        /// </summary>
        public int PlayerIndex => Native.SDL_JoystickGetPlayerIndex(Pointer);

        /// <summary>
        /// The GUID of this joystick.
        /// </summary>
        public Guid Guid => Native.SDL_JoystickGetGUID(Pointer);

        /// <summary>
        /// The vendor code for this joystick.
        /// </summary>
        public ushort Vendor => Native.SDL_JoystickGetVendor(Pointer);

        /// <summary>
        /// The product code for this joystick.
        /// </summary>
        public ushort Product => Native.SDL_JoystickGetProduct(Pointer);

        /// <summary>
        /// The product version of this joystick.
        /// </summary>
        public ushort ProductVersion => Native.SDL_JoystickGetProductVersion(Pointer);

        /// <summary>
        /// The type of the joystick.
        /// </summary>
        public JoystickType Type => Native.SDL_JoystickGetType(Pointer);

        /// <summary>
        /// Whether the joystick is attached.
        /// </summary>
        public bool Attached => Native.SDL_JoystickGetAttached(Pointer);

        /// <summary>
        /// The number of axes in the joystick.
        /// </summary>
        public int Axes => Native.SDL_JoystickNumAxes(Pointer);

        /// <summary>
        /// The number of balls in the joystick.
        /// </summary>
        public int Balls => Native.SDL_JoystickNumBalls(Pointer);

        /// <summary>
        /// The number of hats in the joystick.
        /// </summary>
        public int Hats => Native.SDL_JoystickNumHats(Pointer);

        /// <summary>
        /// The number of buttons in the joystick.
        /// </summary>
        public int Buttons => Native.SDL_JoystickNumButtons(Pointer);

        /// <summary>
        /// The power level of the joystick.
        /// </summary>
        public JoystickPowerLevel PowerLevel => Native.SDL_JoystickCurrentPowerLevel(Pointer);

        /// <summary>
        /// Whether the joystick supports haptic effects.
        /// </summary>
        public bool IsHaptic =>
            Native.SDL_JoystickIsHaptic(Pointer);

        /// <summary>
        /// Returns the haptic support for the joystick.
        /// </summary>
        public Haptic Haptic =>
            Haptic.PointerToInstanceNotNull(Native.SDL_HapticOpenFromJoystick(Pointer));

        /// <summary>
        /// An event that is fired when a joystick is added.
        /// </summary>
        public static event EventHandler<JoystickAddedEventArgs>? Added;

        /// <summary>
        /// An event that fires when there is motion on an axis.
        /// </summary>
        public event EventHandler<JoystickAxisMotionEventArgs>? AxisMotion;

        /// <summary>
        /// An event that fires when there is motion on a ball.
        /// </summary>
        public event EventHandler<JoystickBallMotionEventArgs>? BallMotion;

        /// <summary>
        /// An event that fires when a button is pressed.
        /// </summary>
        public event EventHandler<JoystickButtonEventArgs>? ButtonDown;

        /// <summary>
        /// An event that fires when a button is released.
        /// </summary>
        public event EventHandler<JoystickButtonEventArgs>? ButtonUp;

        /// <summary>
        /// An event that is fired when a joystick is removed.
        /// </summary>
        public event EventHandler<SdlEventArgs>? Removed;

        /// <summary>
        /// An event that fires when a hat moves.
        /// </summary>
        public event EventHandler<JoystickHatMotionEventArgs>? HatMotion;

        internal static Joystick Get(Native.SDL_JoystickID instanceId) =>
            PointerToInstanceNotNull(Native.SDL_JoystickFromInstanceID(instanceId));

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
        public override void Dispose()
        {
            Native.SDL_JoystickClose(Pointer);
            base.Dispose();
        }

        /// <summary>
        /// Sets the state for joystick events.
        /// </summary>
        /// <param name="state">The state.</param>
        /// <returns>The old state.</returns>
        public static State SetEventState(State state) =>
            (State)Native.SDL_JoystickEventState(state);

        /// <summary>
        /// Gets the value of the axis.
        /// </summary>
        /// <param name="axis">The axis.</param>
        /// <returns>The value.</returns>
        public short GetAxis(int axis) =>
            Native.SDL_JoystickGetAxis(Pointer, axis);

        /// <summary>
        /// Gets the axis's initial state.
        /// </summary>
        /// <param name="axis">The axis.</param>
        /// <param name="state">The initial state.</param>
        /// <returns>Whether the axis exists.</returns>
        public bool GetAxisInitialState(int axis, out short state) =>
            Native.SDL_JoystickGetAxisInitialState(Pointer, axis, out state);

        /// <summary>
        /// Gets the value of the hat.
        /// </summary>
        /// <param name="hat">The hat.</param>
        /// <returns>The value.</returns>
        public HatFlags GetHat(int hat) =>
            Native.SDL_JoystickGetHat(Pointer, hat);

        /// <summary>
        /// Gets the value of the ball.
        /// </summary>
        /// <param name="ball">The ball.</param>
        /// <returns>The value.</returns>
        public (int XDelta, int YDelta) GetBall(int ball)
        {
            _ = Native.CheckError(Native.SDL_JoystickGetBall(Pointer, ball, out var dx, out var dy));
            return (dx, dy);
        }

        /// <summary>
        /// Gets the button state.
        /// </summary>
        /// <param name="button">The button.</param>
        /// <returns><c>true</c> if the button is pressed, <c>false</c> otherwise.</returns>
        public bool GetButton(int button) =>
            Native.SDL_JoystickGetButton(Pointer, button);

        /// <summary>
        /// Rumbles the joystick.
        /// </summary>
        /// <param name="lowFrequency">The low frequency.</param>
        /// <param name="highFrequency">The high frequency.</param>
        /// <param name="duration">The duration.</param>
        public void Rumble(ushort lowFrequency, ushort highFrequency, uint duration) =>
            Native.CheckError(Native.SDL_JoystickRumble(Pointer, lowFrequency, highFrequency, duration));

        internal static void DispatchEvent(Native.SDL_Event e)
        {
            switch (e.Type)
            {
                case Native.SDL_EventType.JoystickAxisMotion:
                    {
                        var joystick = Get(e.Jaxis.Which);
                        joystick?.AxisMotion?.Invoke(joystick, new JoystickAxisMotionEventArgs(e.Jaxis));
                        break;
                    }

                case Native.SDL_EventType.JoystickBallMotion:
                    {
                        var joystick = Get(e.Jball.Which);
                        joystick?.BallMotion?.Invoke(joystick, new JoystickBallMotionEventArgs(e.Jball));
                        break;
                    }

                case Native.SDL_EventType.JoystickButtonDown:
                    {
                        var joystick = Get(e.Jbutton.Which);
                        joystick?.ButtonDown?.Invoke(joystick, new JoystickButtonEventArgs(e.Jbutton));
                        break;
                    }

                case Native.SDL_EventType.JoystickButtonUp:
                    {
                        var joystick = Get(e.Jbutton.Which);
                        joystick?.ButtonUp?.Invoke(joystick, new JoystickButtonEventArgs(e.Jbutton));
                        break;
                    }

                case Native.SDL_EventType.JoystickDeviceAdded:
                    {
                        Added?.Invoke(null, new JoystickAddedEventArgs(e.Jdevice));
                        break;
                    }

                case Native.SDL_EventType.JoystickDeviceRemoved:
                    {
                        var joystick = Get(new Native.SDL_JoystickID(e.Jdevice.Which));
                        joystick.Removed?.Invoke(joystick, new SdlEventArgs(e.Common));
                        break;
                    }

                case Native.SDL_EventType.JoystickHatMotion:
                    {
                        var joystick = Get(e.Jhat.Which);
                        joystick?.HatMotion?.Invoke(joystick, new JoystickHatMotionEventArgs(e.Jhat));
                        break;
                    }

                default:
                    throw new InvalidOperationException();
            }
        }
    }
}
