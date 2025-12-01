using Sdl3Sharp.Graphics;
using static Sdl3Sharp.Native.Common;
using static Sdl3Sharp.Native.Joystick;
using static Sdl3Sharp.Native.StdInc;

namespace Sdl3Sharp.Input;

/// <summary>
/// Represents an opened SDL joystick device.
/// This is the lower-level joystick handling. If you want the simpler option,
/// where what each button does is well-defined, you should use the gamepad API instead.
/// </summary>
public sealed unsafe class Joystick : IDisposable
{
    /// <summary>
    /// Occurs when a joystick axis is moved.
    /// </summary>
    public static event EventHandler<JoystickAxisEventArgs>? AxisMotion;

    /// <summary>
    /// Occurs when a joystick trackball is moved.
    /// </summary>
    public static event EventHandler<JoystickBallEventArgs>? BallMotion;

    /// <summary>
    /// Occurs when a joystick hat position changes.
    /// </summary>
    public static event EventHandler<JoystickHatEventArgs>? HatMotion;

    /// <summary>
    /// Occurs when a joystick button is pressed.
    /// </summary>
    public static event EventHandler<JoystickButtonEventArgs>? ButtonDown;

    /// <summary>
    /// Occurs when a joystick button is released.
    /// </summary>
    public static event EventHandler<JoystickButtonEventArgs>? ButtonUp;

    /// <summary>
    /// Occurs when a joystick has been added to the system.
    /// </summary>
    public static event EventHandler<JoystickDeviceEventArgs>? Added;

    /// <summary>
    /// Occurs when a joystick has been removed from the system.
    /// </summary>
    public static event EventHandler<JoystickDeviceEventArgs>? Removed;

    /// <summary>
    /// Occurs when a joystick's battery level has been updated.
    /// </summary>
    public static event EventHandler<JoystickBatteryEventArgs>? BatteryUpdated;

    /// <summary>
    /// Occurs when a joystick update is complete.
    /// </summary>
    public static event EventHandler<JoystickDeviceEventArgs>? UpdateComplete;

    internal static void DispatchEvent(Event e)
    {
        switch (e.Type)
        {
            case EventType.JoystickAxisMotion:
                AxisMotion?.Invoke(null, (JoystickAxisEventArgs)e.TranslateEvent());
                break;
            case EventType.JoystickBallMotion:
                BallMotion?.Invoke(null, (JoystickBallEventArgs)e.TranslateEvent());
                break;
            case EventType.JoystickHatMotion:
                HatMotion?.Invoke(null, (JoystickHatEventArgs)e.TranslateEvent());
                break;
            case EventType.JoystickButtonDown:
                ButtonDown?.Invoke(null, (JoystickButtonEventArgs)e.TranslateEvent());
                break;
            case EventType.JoystickButtonUp:
                ButtonUp?.Invoke(null, (JoystickButtonEventArgs)e.TranslateEvent());
                break;
            case EventType.JoystickAdded:
                Added?.Invoke(null, (JoystickDeviceEventArgs)e.TranslateEvent());
                break;
            case EventType.JoystickRemoved:
                Removed?.Invoke(null, (JoystickDeviceEventArgs)e.TranslateEvent());
                break;
            case EventType.JoystickBatteryUpdated:
                BatteryUpdated?.Invoke(null, (JoystickBatteryEventArgs)e.TranslateEvent());
                break;
            case EventType.JoystickUpdateComplete:
                UpdateComplete?.Invoke(null, (JoystickDeviceEventArgs)e.TranslateEvent());
                break;
        }
    }

    private bool _disposed;
    private readonly bool _ownsHandle;

    /// <summary>
    /// The largest value a joystick axis can report.
    /// </summary>
    public const short AxisMax = SDL_JOYSTICK_AXIS_MAX;

    /// <summary>
    /// The smallest value a joystick axis can report. This is a negative number.
    /// </summary>
    public const short AxisMin = SDL_JOYSTICK_AXIS_MIN;

    /// <summary>
    /// Gets the underlying SDL_Joystick pointer.
    /// </summary>
    public SDL_Joystick* Handle { get; private set; }

    /// <summary>
    /// Gets the properties associated with this joystick.
    /// </summary>
    public PropertyGroup Properties
    {
        get
        {
            ThrowIfDisposed();
            return new(CheckErrorZero(SDL_GetJoystickProperties(Handle)), ownsHandle: false);
        }
    }

    /// <summary>
    /// Gets the implementation dependent name of this joystick.
    /// </summary>
    public string Name
    {
        get
        {
            ThrowIfDisposed();
            return CheckErrorNull(SDL_GetJoystickName(Handle));
        }
    }

    /// <summary>
    /// Gets the implementation dependent path of this joystick.
    /// </summary>
    public string Path
    {
        get
        {
            ThrowIfDisposed();
            return CheckErrorNull(SDL_GetJoystickPath(Handle));
        }
    }

    /// <summary>
    /// The player index of this joystick, or -1 if not available.
    /// For XInput controllers this returns the XInput user index.
    /// </summary>
    public int PlayerIndex
    {
        get
        {
            ThrowIfDisposed();
            return SDL_GetJoystickPlayerIndex(Handle);
        }
        set
        {
            ThrowIfDisposed();
            _ = CheckErrorBool(SDL_SetJoystickPlayerIndex(Handle, value));
        }
    }

    /// <summary>
    /// Gets the implementation-dependent GUID for this joystick.
    /// </summary>
    public Guid Guid
    {
        get
        {
            ThrowIfDisposed();
            return SDL_GetJoystickGUID(Handle);
        }
    }

    /// <summary>
    /// Gets the USB vendor ID of this joystick, or 0 if not available.
    /// </summary>
    public ushort VendorId
    {
        get
        {
            ThrowIfDisposed();
            return SDL_GetJoystickVendor(Handle);
        }
    }

    /// <summary>
    /// Gets the USB product ID of this joystick, or 0 if not available.
    /// </summary>
    public ushort ProductId
    {
        get
        {
            ThrowIfDisposed();
            return SDL_GetJoystickProduct(Handle);
        }
    }

    /// <summary>
    /// Gets the product version of this joystick, or 0 if not available.
    /// </summary>
    public ushort ProductVersion
    {
        get
        {
            ThrowIfDisposed();
            return SDL_GetJoystickProductVersion(Handle);
        }
    }

    /// <summary>
    /// Gets the firmware version of this joystick, or 0 if not available.
    /// </summary>
    public ushort FirmwareVersion
    {
        get
        {
            ThrowIfDisposed();
            return SDL_GetJoystickFirmwareVersion(Handle);
        }
    }

    /// <summary>
    /// Gets the serial number of this joystick, or null if not available.
    /// </summary>
    public string? SerialNumber
    {
        get
        {
            ThrowIfDisposed();
            return SDL_GetJoystickSerial(Handle);
        }
    }

    /// <summary>
    /// Gets the type of this joystick.
    /// </summary>
    public JoystickType Type
    {
        get
        {
            ThrowIfDisposed();
            return (JoystickType)SDL_GetJoystickType(Handle);
        }
    }

    /// <summary>
    /// Gets the connection state of this joystick.
    /// </summary>
    public JoystickConnectionState ConnectionState
    {
        get
        {
            ThrowIfDisposed();
            return (JoystickConnectionState)SDL_GetJoystickConnectionState(Handle);
        }
    }

    /// <summary>
    /// Gets whether this joystick is connected.
    /// </summary>
    public bool IsConnected
    {
        get
        {
            ThrowIfDisposed();
            return SDL_JoystickConnected(Handle);
        }
    }

    /// <summary>
    /// Gets the descriptor for this joystick.
    /// </summary>
    public JoystickDescriptor Descriptor
    {
        get
        {
            ThrowIfDisposed();
            return new(CheckErrorZero(SDL_GetJoystickID(Handle)));
        }
    }

    /// <summary>
    /// Gets the number of axes on this joystick.
    /// </summary>
    public int AxisCount
    {
        get
        {
            ThrowIfDisposed();
            return CheckErrorNegativeOne(SDL_GetNumJoystickAxes(Handle));
        }
    }

    /// <summary>
    /// Gets the number of trackballs on this joystick.
    /// Joystick trackballs have only relative motion events associated with them
    /// and their state cannot be polled. Most joysticks do not have trackballs.
    /// </summary>
    public int BallCount
    {
        get
        {
            ThrowIfDisposed();
            return CheckErrorNegativeOne(SDL_GetNumJoystickBalls(Handle));
        }
    }

    /// <summary>
    /// Gets the number of POV hats on this joystick.
    /// </summary>
    public int HatCount
    {
        get
        {
            ThrowIfDisposed();
            var count = SDL_GetNumJoystickHats(Handle);
            return count < 0 ? throw new SdlException() : count;
        }
    }

    /// <summary>
    /// Gets the number of buttons on this joystick.
    /// </summary>
    public int ButtonCount
    {
        get
        {
            ThrowIfDisposed();
            var count = SDL_GetNumJoystickButtons(Handle);
            return count < 0 ? throw new SdlException() : count;
        }
    }

    /// <summary>
    /// Gets whether a joystick is currently connected.
    /// </summary>
    public static bool HasJoystick => SDL_HasJoystick();

    /// <summary>
    /// Gets or sets whether joystick events are enabled.
    /// If disabled, you must call <see cref="UpdateAll"/> yourself.
    /// </summary>
    public static bool EventsEnabled
    {
        get => SDL_JoystickEventsEnabled();
        set => SDL_SetJoystickEventsEnabled(value);
    }

    /// <summary>
    /// Gets a list of currently connected joysticks.
    /// </summary>
    /// <returns>An array of joystick descriptors.</returns>
    public static JoystickDescriptor[] GetJoysticks()
    {
        int count;
        SDL_JoystickID* joysticks = CheckErrorPointer(SDL_GetJoysticks(&count));

        try
        {
            var result = new JoystickDescriptor[count];
            for (var i = 0; i < count; i++)
            {
                result[i] = new JoystickDescriptor(joysticks[i]);
            }

            return result;
        }
        finally
        {
            SDL_free(joysticks);
        }
    }

    /// <summary>
    /// Gets the joystick associated with a player index.
    /// </summary>
    /// <param name="playerIndex">The player index to get the joystick for.</param>
    /// <returns>A Joystick instance.</returns>
    public static Joystick GetFromPlayerIndex(int playerIndex)
    {
        return new(SDL_GetJoystickFromPlayerIndex(playerIndex), ownsHandle: false);
    }

    /// <summary>
    /// Updates the current state of all open joysticks.
    /// This is called automatically by the event loop if joystick events are enabled.
    /// </summary>
    public static void UpdateAll()
    {
        SDL_UpdateJoysticks();
    }

    /// <summary>
    /// Locks the joystick subsystem for atomic access.
    /// The SDL joystick functions are thread-safe, however you can lock the
    /// joysticks while processing to guarantee that the joystick list won't change
    /// and joystick and gamepad events will not be delivered.
    /// </summary>
    public static void Lock()
    {
        SDL_LockJoysticks();
    }

    /// <summary>
    /// Unlocks the joystick subsystem after atomic access.
    /// </summary>
    public static void Unlock()
    {
        SDL_UnlockJoysticks();
    }

    /// <summary>
    /// Gets the device information encoded in a GUID structure.
    /// </summary>
    /// <param name="guid">The GUID to extract information from.</param>
    /// <param name="vendorId">The device vendor ID, or 0 if not available.</param>
    /// <param name="productId">The device product ID, or 0 if not available.</param>
    /// <param name="version">The device version, or 0 if not available.</param>
    /// <param name="crc16">A CRC used to distinguish different products with the same VID/PID, or 0 if not available.</param>
    public static void GetGuidInfo(Guid guid, out ushort vendorId, out ushort productId, out ushort version, out ushort crc16)
    {
        fixed (ushort* pVendor = &vendorId) 
        fixed (ushort* pProduct = &productId)
        fixed (ushort* pVersion = &version)
        fixed (ushort* pCrc = &crc16)
        {
            SDL_GetJoystickGUIDInfo(guid, pVendor, pProduct, pVersion, pCrc);
        }
    }

    /// <summary>
    /// Wraps an existing SDL_Joystick pointer.
    /// </summary>
    /// <param name="handle">The SDL_Joystick pointer to wrap.</param>
    /// <param name="ownsHandle">Whether this instance should close the joystick when disposed.</param>
    internal Joystick(SDL_Joystick* handle, bool ownsHandle = true)
    {
        Handle = handle;
        _ownsHandle = ownsHandle;
    }

    /// <summary>
    /// Gets the current state of an axis control on this joystick.
    /// The value returned is a signed integer (-32768 to 32767).
    /// </summary>
    /// <param name="axis">The axis index (starting at 0).</param>
    /// <returns>The current position of the axis.</returns>
    public short GetAxis(int axis)
    {
        ThrowIfDisposed();
        return SDL_GetJoystickAxis(Handle, axis);
    }

    /// <summary>
    /// Gets the initial state of an axis control on this joystick.
    /// </summary>
    /// <param name="axis">The axis index (starting at 0).</param>
    /// <param name="state">The initial value of the axis, if it has one.</param>
    /// <returns>True if this axis has any initial value, false otherwise.</returns>
    public bool GetAxisInitialState(int axis, out short state)
    {
        ThrowIfDisposed();
        fixed (short* pState = &state)
        {
            return SDL_GetJoystickAxisInitialState(Handle, axis, pState);
        }
    }

    /// <summary>
    /// Gets the ball axis change since the last poll.
    /// Trackballs can only return relative motion since the last call.
    /// Most joysticks do not have trackballs.
    /// </summary>
    /// <param name="ball">The ball index (starting at 0).</param>
    /// <param name="dx">The difference in the x axis position since the last poll.</param>
    /// <param name="dy">The difference in the y axis position since the last poll.</param>
    public void GetBall(int ball, out int dx, out int dy)
    {
        ThrowIfDisposed();
        fixed (int* pDx = &dx)
        fixed (int* pDy = &dy)
        {
            _ = CheckErrorBool(SDL_GetJoystickBall(Handle, ball, pDx, pDy));
        }
    }

    /// <summary>
    /// Gets the current state of a POV hat on this joystick.
    /// </summary>
    /// <param name="hat">The hat index (starting at 0).</param>
    /// <returns>The current hat position.</returns>
    public HatPosition GetHat(int hat)
    {
        ThrowIfDisposed();
        return (HatPosition)SDL_GetJoystickHat(Handle, hat);
    }

    /// <summary>
    /// Gets the current state of a button on this joystick.
    /// </summary>
    /// <param name="button">The button index (starting at 0).</param>
    /// <returns>True if the button is pressed, false otherwise.</returns>
    public bool GetButton(int button)
    {
        ThrowIfDisposed();
        return SDL_GetJoystickButton(Handle, button);
    }

    /// <summary>
    /// Starts a rumble effect on this joystick.
    /// Each call cancels any previous rumble effect, and calling with 0 intensity stops rumbling.
    /// </summary>
    /// <param name="lowFrequencyRumble">The intensity of the low frequency (left) motor, from 0 to 0xFFFF.</param>
    /// <param name="highFrequencyRumble">The intensity of the high frequency (right) motor, from 0 to 0xFFFF.</param>
    /// <param name="durationMs">The duration of the rumble effect, in milliseconds.</param>
    /// <returns>True if rumble is supported, false otherwise.</returns>
    public bool Rumble(ushort lowFrequencyRumble, ushort highFrequencyRumble, uint durationMs)
    {
        ThrowIfDisposed();
        return SDL_RumbleJoystick(Handle, lowFrequencyRumble, highFrequencyRumble, durationMs);
    }

    /// <summary>
    /// Starts a rumble effect in this joystick's triggers.
    /// Currently only supported on Xbox One controllers.
    /// </summary>
    /// <param name="leftRumble">The intensity of the left trigger motor, from 0 to 0xFFFF.</param>
    /// <param name="rightRumble">The intensity of the right trigger motor, from 0 to 0xFFFF.</param>
    /// <param name="durationMs">The duration of the rumble effect, in milliseconds.</param>
    /// <returns>True on success, false on failure.</returns>
    public bool RumbleTriggers(ushort leftRumble, ushort rightRumble, uint durationMs)
    {
        ThrowIfDisposed();
        return SDL_RumbleJoystickTriggers(Handle, leftRumble, rightRumble, durationMs);
    }

    /// <summary>
    /// Updates this joystick's LED color.
    /// An example of a joystick LED is the light on the back of a PlayStation 4's DualShock 4 controller.
    /// </summary>
    /// <param name="color">The color.</param>
    public void SetLed(Color color)
    {
        ThrowIfDisposed();
        _ = CheckErrorBool(SDL_SetJoystickLED(Handle, color.Red, color.Green, color.Blue));
    }

    /// <summary>
    /// Sends a joystick specific effect packet.
    /// </summary>
    /// <param name="data">The data to send to the joystick.</param>
    public void SendEffect(ReadOnlySpan<byte> data)
    {
        ThrowIfDisposed();
        fixed (byte* ptr = data)
        {
            _ = CheckErrorBool(SDL_SendJoystickEffect(Handle, ptr, data.Length));
        }
    }

    /// <summary>
    /// Gets the battery state of this joystick.
    /// Note: Battery status can be unreliable; values are best estimates based on hardware reports.
    /// </summary>
    /// <param name="percent">The percentage of battery life left (0-100), or -1 if unknown.</param>
    /// <returns>The current battery state.</returns>
    public PowerState GetPowerInfo(out int percent)
    {
        ThrowIfDisposed();
        fixed (int* pPercent = &percent)
        {
            return (PowerState)SDL_GetJoystickPowerInfo(Handle, pPercent);
        }
    }

    /// <inheritdoc/>
    public void Dispose()
    {
        if (_disposed)
        {
            return;
        }

        if (_ownsHandle && Handle is not null)
        {
            SDL_CloseJoystick(Handle);
            Handle = null;
        }

        _disposed = true;
    }

    private void ThrowIfDisposed()
    {
        ObjectDisposedException.ThrowIf(_disposed, this);
    }
}
