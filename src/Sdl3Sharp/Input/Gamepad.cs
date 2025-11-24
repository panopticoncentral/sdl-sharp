using Sdl3Sharp.Graphics;
using static Sdl3Sharp.Native.Common;
using static Sdl3Sharp.Native.Gamepad;
using static Sdl3Sharp.Native.Joystick;
using static Sdl3Sharp.Native.Sensor;
using static Sdl3Sharp.Native.StdInc;

namespace Sdl3Sharp.Input;

/// <summary>
/// Represents an opened SDL gamepad device.
/// The gamepad API provides a higher-level interface than the joystick API,
/// where button and axis positions are well-defined (like a standard console controller).
/// </summary>
public sealed unsafe class Gamepad : IDisposable
{
    private bool _disposed;
    private readonly bool _ownsHandle;

    /// <summary>
    /// Gets the underlying SDL_Gamepad pointer.
    /// </summary>
    public SDL_Gamepad* Handle { get; private set; }

    /// <summary>
    /// Gets the properties associated with this gamepad.
    /// These properties are shared with the underlying joystick object.
    /// </summary>
    public PropertyGroup Properties
    {
        get
        {
            ThrowIfDisposed();
            return new(CheckErrorZero(SDL_GetGamepadProperties(Handle)), ownsHandle: false);
        }
    }

    /// <summary>
    /// Gets the implementation dependent name of this gamepad.
    /// </summary>
    public string? Name
    {
        get
        {
            ThrowIfDisposed();
            return SDL_GetGamepadName(Handle);
        }
    }

    /// <summary>
    /// Gets the implementation dependent path of this gamepad.
    /// </summary>
    public string? Path
    {
        get
        {
            ThrowIfDisposed();
            return SDL_GetGamepadPath(Handle);
        }
    }

    /// <summary>
    /// The player index of this gamepad, or -1 if not available.
    /// For XInput gamepads this returns the XInput user index.
    /// </summary>
    public int PlayerIndex
    {
        get
        {
            ThrowIfDisposed();
            return SDL_GetGamepadPlayerIndex(Handle);
        }
        set
        {
            ThrowIfDisposed();
            _ = CheckErrorBool(SDL_SetGamepadPlayerIndex(Handle, value));
        }
    }

    /// <summary>
    /// Gets the USB vendor ID of this gamepad, or 0 if not available.
    /// </summary>
    public ushort VendorId
    {
        get
        {
            ThrowIfDisposed();
            return SDL_GetGamepadVendor(Handle);
        }
    }

    /// <summary>
    /// Gets the USB product ID of this gamepad, or 0 if not available.
    /// </summary>
    public ushort ProductId
    {
        get
        {
            ThrowIfDisposed();
            return SDL_GetGamepadProduct(Handle);
        }
    }

    /// <summary>
    /// Gets the product version of this gamepad, or 0 if not available.
    /// </summary>
    public ushort ProductVersion
    {
        get
        {
            ThrowIfDisposed();
            return SDL_GetGamepadProductVersion(Handle);
        }
    }

    /// <summary>
    /// Gets the firmware version of this gamepad, or 0 if not available.
    /// </summary>
    public ushort FirmwareVersion
    {
        get
        {
            ThrowIfDisposed();
            return SDL_GetGamepadFirmwareVersion(Handle);
        }
    }

    /// <summary>
    /// Gets the serial number of this gamepad, or null if not available.
    /// </summary>
    public string? SerialNumber
    {
        get
        {
            ThrowIfDisposed();
            return SDL_GetGamepadSerial(Handle);
        }
    }

    /// <summary>
    /// Gets the Steam Input handle of this gamepad, or 0 if not available.
    /// This can be used with the Steam Input API.
    /// </summary>
    public ulong SteamHandle
    {
        get
        {
            ThrowIfDisposed();
            return SDL_GetGamepadSteamHandle(Handle);
        }
    }

    /// <summary>
    /// Gets the type of this gamepad.
    /// </summary>
    public GamepadType Type
    {
        get
        {
            ThrowIfDisposed();
            return (GamepadType)SDL_GetGamepadType(Handle);
        }
    }

    /// <summary>
    /// Gets the real type of this gamepad, ignoring any mapping override.
    /// </summary>
    public GamepadType RealType
    {
        get
        {
            ThrowIfDisposed();
            return (GamepadType)SDL_GetRealGamepadType(Handle);
        }
    }

    /// <summary>
    /// Gets the connection state of this gamepad.
    /// </summary>
    public JoystickConnectionState ConnectionState
    {
        get
        {
            ThrowIfDisposed();
            return (JoystickConnectionState)SDL_GetGamepadConnectionState(Handle);
        }
    }

    /// <summary>
    /// Gets whether this gamepad is connected.
    /// </summary>
    public bool IsConnected
    {
        get
        {
            ThrowIfDisposed();
            return SDL_GamepadConnected(Handle);
        }
    }

    /// <summary>
    /// Gets the descriptor for this gamepad.
    /// </summary>
    public GamepadDescriptor Descriptor
    {
        get
        {
            ThrowIfDisposed();
            return new(CheckErrorZero(SDL_GetGamepadID(Handle)));
        }
    }

    /// <summary>
    /// Gets the underlying joystick from this gamepad.
    /// The returned joystick is owned by this gamepad and should not be closed.
    /// </summary>
    public Joystick Joystick
    {
        get
        {
            ThrowIfDisposed();
            return new(CheckErrorPointer(SDL_GetGamepadJoystick(Handle)), ownsHandle: false);
        }
    }

    /// <summary>
    /// Gets the current mapping string for this gamepad.
    /// </summary>
    public string? Mapping
    {
        get
        {
            ThrowIfDisposed();
            var mapping = SDL_GetGamepadMapping(Handle);
            if (mapping is null)
            {
                return null;
            }

            try
            {
                return System.Runtime.InteropServices.Marshal.PtrToStringUTF8((nint)mapping);
            }
            finally
            {
                SDL_free(mapping);
            }
        }
    }

    /// <summary>
    /// Gets the number of touchpads on this gamepad.
    /// </summary>
    public int TouchpadCount
    {
        get
        {
            ThrowIfDisposed();
            return SDL_GetNumGamepadTouchpads(Handle);
        }
    }

    /// <summary>
    /// Gets whether a gamepad is currently connected.
    /// </summary>
    public static bool HasGamepad => SDL_HasGamepad();

    /// <summary>
    /// Gets or sets whether gamepad events are enabled.
    /// If disabled, you must call <see cref="UpdateAll"/> yourself.
    /// </summary>
    public static bool EventsEnabled
    {
        get => SDL_GamepadEventsEnabled();
        set => SDL_SetGamepadEventsEnabled(value);
    }

    /// <summary>
    /// Gets a list of currently connected gamepads.
    /// </summary>
    /// <returns>An array of gamepad descriptors.</returns>
    public static GamepadDescriptor[] GetGamepads()
    {
        int count;
        SDL_JoystickID* gamepads = CheckErrorPointer(SDL_GetGamepads(&count));

        try
        {
            var result = new GamepadDescriptor[count];
            for (var i = 0; i < count; i++)
            {
                result[i] = new GamepadDescriptor(gamepads[i]);
            }

            return result;
        }
        finally
        {
            SDL_free(gamepads);
        }
    }

    /// <summary>
    /// Gets the gamepad associated with a player index.
    /// </summary>
    /// <param name="playerIndex">The player index to get the gamepad for.</param>
    /// <returns>A Gamepad instance, or null if not found.</returns>
    public static Gamepad? GetFromPlayerIndex(int playerIndex)
    {
        SDL_Gamepad* gamepad = SDL_GetGamepadFromPlayerIndex(playerIndex);
        return gamepad is not null ? new(gamepad, ownsHandle: false) : null;
    }

    /// <summary>
    /// Updates the current state of all open gamepads.
    /// This is called automatically by the event loop if gamepad events are enabled.
    /// </summary>
    public static void UpdateAll()
    {
        SDL_UpdateGamepads();
    }

    /// <summary>
    /// Adds a gamepad mapping from a string.
    /// </summary>
    /// <param name="mapping">The mapping string in SDL format.</param>
    /// <returns>1 if a new mapping is added, 0 if an existing mapping is updated.</returns>
    public static int AddMapping(string mapping)
    {
        return CheckErrorNegativeOne(SDL_AddGamepadMapping(mapping));
    }

    /// <summary>
    /// Loads gamepad mappings from a file.
    /// </summary>
    /// <param name="file">The path to the mappings file.</param>
    /// <returns>The number of mappings added.</returns>
    public static int AddMappingsFromFile(string file)
    {
        return CheckErrorNegativeOne(SDL_AddGamepadMappingsFromFile(file));
    }

    /// <summary>
    /// Loads gamepad mappings from an IOStream.
    /// </summary>
    /// <param name="stream">The IOStream containing the mappings.</param>
    /// <param name="closeStream">Whether to close the stream after reading.</param>
    /// <returns>The number of mappings added.</returns>
    public static int AddMappingsFromStream(IOStream stream, bool closeStream = false)
    {
        return CheckErrorNegativeOne(SDL_AddGamepadMappingsFromIO(stream.Handle, closeStream));
    }

    /// <summary>
    /// Reinitializes the SDL mapping database to its initial state.
    /// This will generate gamepad events as needed if device mappings change.
    /// </summary>
    public static void ReloadMappings()
    {
        _ = CheckErrorBool(SDL_ReloadGamepadMappings());
    }

    /// <summary>
    /// Gets the mapping string for a given GUID.
    /// </summary>
    /// <param name="guid">The GUID to look up.</param>
    /// <returns>The mapping string, or null if not found.</returns>
    public static string? GetMappingForGuid(Guid guid)
    {
        var mapping = SDL_GetGamepadMappingForGUID(guid);
        if (mapping is null)
        {
            return null;
        }

        try
        {
            return System.Runtime.InteropServices.Marshal.PtrToStringUTF8((nint)mapping);
        }
        finally
        {
            SDL_free(mapping);
        }
    }

    /// <summary>
    /// Checks if the given joystick is supported by the gamepad interface.
    /// </summary>
    /// <param name="joystickId">The joystick instance ID.</param>
    /// <returns>True if the joystick is supported as a gamepad.</returns>
    public static bool IsGamepad(SDL_JoystickID joystickId)
    {
        return SDL_IsGamepad(joystickId);
    }

    /// <summary>
    /// Converts a string to a GamepadType.
    /// </summary>
    /// <param name="str">The string to convert.</param>
    /// <returns>The corresponding GamepadType.</returns>
    public static GamepadType GetTypeFromString(string str)
    {
        return (GamepadType)SDL_GetGamepadTypeFromString(str);
    }

    /// <summary>
    /// Converts a GamepadType to a string.
    /// </summary>
    /// <param name="type">The type to convert.</param>
    /// <returns>The string representation, or null if invalid.</returns>
    public static string? GetStringForType(GamepadType type)
    {
        return SDL_GetGamepadStringForType((SDL_GamepadType)type);
    }

    /// <summary>
    /// Converts a string to a GamepadAxis.
    /// </summary>
    /// <param name="str">The string to convert.</param>
    /// <returns>The corresponding GamepadAxis, or null if invalid.</returns>
    public static GamepadAxis? GetAxisFromString(string str)
    {
        var axis = SDL_GetGamepadAxisFromString(str);
        return axis == SDL_GamepadAxis.SDL_GAMEPAD_AXIS_INVALID ? null : (GamepadAxis)axis;
    }

    /// <summary>
    /// Converts a GamepadAxis to a string.
    /// </summary>
    /// <param name="axis">The axis to convert.</param>
    /// <returns>The string representation, or null if invalid.</returns>
    public static string? GetStringForAxis(GamepadAxis axis)
    {
        return SDL_GetGamepadStringForAxis((SDL_GamepadAxis)axis);
    }

    /// <summary>
    /// Converts a string to a GamepadButton.
    /// </summary>
    /// <param name="str">The string to convert.</param>
    /// <returns>The corresponding GamepadButton, or null if invalid.</returns>
    public static GamepadButton? GetButtonFromString(string str)
    {
        var button = SDL_GetGamepadButtonFromString(str);
        return button == SDL_GamepadButton.SDL_GAMEPAD_BUTTON_INVALID ? null : (GamepadButton)button;
    }

    /// <summary>
    /// Converts a GamepadButton to a string.
    /// </summary>
    /// <param name="button">The button to convert.</param>
    /// <returns>The string representation, or null if invalid.</returns>
    public static string? GetStringForButton(GamepadButton button)
    {
        return SDL_GetGamepadStringForButton((SDL_GamepadButton)button);
    }

    /// <summary>
    /// Gets the button label for a specific gamepad type and button.
    /// </summary>
    /// <param name="type">The gamepad type.</param>
    /// <param name="button">The button.</param>
    /// <returns>The button label.</returns>
    public static GamepadButtonLabel GetButtonLabelForType(GamepadType type, GamepadButton button)
    {
        return (GamepadButtonLabel)SDL_GetGamepadButtonLabelForType((SDL_GamepadType)type, (SDL_GamepadButton)button);
    }

    /// <summary>
    /// Wraps an existing SDL_Gamepad pointer.
    /// </summary>
    /// <param name="handle">The SDL_Gamepad pointer to wrap.</param>
    /// <param name="ownsHandle">Whether this instance should close the gamepad when disposed.</param>
    internal Gamepad(SDL_Gamepad* handle, bool ownsHandle = true)
    {
        Handle = handle;
        _ownsHandle = ownsHandle;
    }

    /// <summary>
    /// Query whether this gamepad has a given axis.
    /// </summary>
    /// <param name="axis">The axis to query.</param>
    /// <returns>True if the gamepad has this axis.</returns>
    public bool HasAxis(GamepadAxis axis)
    {
        ThrowIfDisposed();
        return SDL_GamepadHasAxis(Handle, (SDL_GamepadAxis)axis);
    }

    /// <summary>
    /// Gets the current state of an axis control on this gamepad.
    /// For thumbsticks, the state is a value ranging from -32768 (up/left) to 32767 (down/right).
    /// Triggers range from 0 when released to 32767 when fully pressed.
    /// </summary>
    /// <param name="axis">The axis to query.</param>
    /// <returns>The current position of the axis.</returns>
    public short GetAxis(GamepadAxis axis)
    {
        ThrowIfDisposed();
        return SDL_GetGamepadAxis(Handle, (SDL_GamepadAxis)axis);
    }

    /// <summary>
    /// Query whether this gamepad has a given button.
    /// </summary>
    /// <param name="button">The button to query.</param>
    /// <returns>True if the gamepad has this button.</returns>
    public bool HasButton(GamepadButton button)
    {
        ThrowIfDisposed();
        return SDL_GamepadHasButton(Handle, (SDL_GamepadButton)button);
    }

    /// <summary>
    /// Gets the current state of a button on this gamepad.
    /// </summary>
    /// <param name="button">The button to query.</param>
    /// <returns>True if the button is pressed, false otherwise.</returns>
    public bool GetButton(GamepadButton button)
    {
        ThrowIfDisposed();
        return SDL_GetGamepadButton(Handle, (SDL_GamepadButton)button);
    }

    /// <summary>
    /// Gets the label of a button on this gamepad.
    /// </summary>
    /// <param name="button">The button to query.</param>
    /// <returns>The button label.</returns>
    public GamepadButtonLabel GetButtonLabel(GamepadButton button)
    {
        ThrowIfDisposed();
        return (GamepadButtonLabel)SDL_GetGamepadButtonLabel(Handle, (SDL_GamepadButton)button);
    }

    /// <summary>
    /// Gets the number of supported simultaneous fingers on a touchpad.
    /// </summary>
    /// <param name="touchpad">The touchpad index.</param>
    /// <returns>The number of supported simultaneous fingers.</returns>
    public int GetTouchpadFingerCount(int touchpad)
    {
        ThrowIfDisposed();
        return SDL_GetNumGamepadTouchpadFingers(Handle, touchpad);
    }

    /// <summary>
    /// Gets the current state of a finger on a touchpad.
    /// </summary>
    /// <param name="touchpad">The touchpad index.</param>
    /// <param name="finger">The finger index.</param>
    /// <returns>The finger state.</returns>
    public GamepadTouchpadFinger GetTouchpadFinger(int touchpad, int finger)
    {
        ThrowIfDisposed();
        byte down;
        float x, y, pressure;
        _ = CheckErrorBool(SDL_GetGamepadTouchpadFinger(Handle, touchpad, finger, &down, &x, &y, &pressure));
        return new GamepadTouchpadFinger(down != 0, x, y, pressure);
    }

    /// <summary>
    /// Returns whether this gamepad has a particular sensor.
    /// </summary>
    /// <param name="type">The sensor type to query.</param>
    /// <returns>True if the sensor exists.</returns>
    public bool HasSensor(SensorType type)
    {
        ThrowIfDisposed();
        return SDL_GamepadHasSensor(Handle, (SDL_SensorType)type);
    }

    /// <summary>
    /// Sets whether data reporting for a gamepad sensor is enabled.
    /// </summary>
    /// <param name="type">The sensor type.</param>
    /// <param name="enabled">Whether to enable data reporting.</param>
    public void SetSensorEnabled(SensorType type, bool enabled)
    {
        ThrowIfDisposed();
        _ = CheckErrorBool(SDL_SetGamepadSensorEnabled(Handle, (SDL_SensorType)type, enabled));
    }

    /// <summary>
    /// Query whether sensor data reporting is enabled for a gamepad sensor.
    /// </summary>
    /// <param name="type">The sensor type to query.</param>
    /// <returns>True if the sensor is enabled.</returns>
    public bool IsSensorEnabled(SensorType type)
    {
        ThrowIfDisposed();
        return SDL_GamepadSensorEnabled(Handle, (SDL_SensorType)type);
    }

    /// <summary>
    /// Gets the data rate (number of events per second) of a gamepad sensor.
    /// </summary>
    /// <param name="type">The sensor type to query.</param>
    /// <returns>The data rate, or 0 if not available.</returns>
    public float GetSensorDataRate(SensorType type)
    {
        ThrowIfDisposed();
        return SDL_GetGamepadSensorDataRate(Handle, (SDL_SensorType)type);
    }

    /// <summary>
    /// Gets the current state of a gamepad sensor.
    /// </summary>
    /// <param name="type">The sensor type.</param>
    /// <param name="data">A span to receive the sensor data.</param>
    public void GetSensorData(SensorType type, Span<float> data)
    {
        ThrowIfDisposed();
        fixed (float* ptr = data)
        {
            _ = CheckErrorBool(SDL_GetGamepadSensorData(Handle, (SDL_SensorType)type, ptr, data.Length));
        }
    }

    /// <summary>
    /// Gets the current state of a gamepad sensor.
    /// </summary>
    /// <param name="type">The sensor type.</param>
    /// <param name="numValues">The number of values to read.</param>
    /// <returns>An array containing the sensor data.</returns>
    public float[] GetSensorData(SensorType type, int numValues)
    {
        var data = new float[numValues];
        GetSensorData(type, data);
        return data;
    }

    /// <summary>
    /// Starts a rumble effect on this gamepad.
    /// Each call cancels any previous rumble effect, and calling with 0 intensity stops rumbling.
    /// </summary>
    /// <param name="lowFrequencyRumble">The intensity of the low frequency (left) motor, from 0 to 0xFFFF.</param>
    /// <param name="highFrequencyRumble">The intensity of the high frequency (right) motor, from 0 to 0xFFFF.</param>
    /// <param name="durationMs">The duration of the rumble effect, in milliseconds.</param>
    /// <returns>True if rumble is supported, false otherwise.</returns>
    public bool Rumble(ushort lowFrequencyRumble, ushort highFrequencyRumble, uint durationMs)
    {
        ThrowIfDisposed();
        return SDL_RumbleGamepad(Handle, lowFrequencyRumble, highFrequencyRumble, durationMs);
    }

    /// <summary>
    /// Starts a rumble effect in this gamepad's triggers.
    /// Currently only supported on Xbox One gamepads.
    /// </summary>
    /// <param name="leftRumble">The intensity of the left trigger motor, from 0 to 0xFFFF.</param>
    /// <param name="rightRumble">The intensity of the right trigger motor, from 0 to 0xFFFF.</param>
    /// <param name="durationMs">The duration of the rumble effect, in milliseconds.</param>
    /// <returns>True on success, false on failure.</returns>
    public bool RumbleTriggers(ushort leftRumble, ushort rightRumble, uint durationMs)
    {
        ThrowIfDisposed();
        return SDL_RumbleGamepadTriggers(Handle, leftRumble, rightRumble, durationMs);
    }

    /// <summary>
    /// Updates this gamepad's LED color.
    /// For gamepads with a single color LED, the maximum of the RGB values will be used as the LED brightness.
    /// </summary>
    /// <param name="color">The color.</param>
    public void SetLed(Color color)
    {
        ThrowIfDisposed();
        _ = CheckErrorBool(SDL_SetGamepadLED(Handle, color.Red, color.Green, color.Blue));
    }

    /// <summary>
    /// Sends a gamepad specific effect packet.
    /// </summary>
    /// <param name="data">The data to send to the gamepad.</param>
    public void SendEffect(ReadOnlySpan<byte> data)
    {
        ThrowIfDisposed();
        fixed (byte* ptr = data)
        {
            _ = CheckErrorBool(SDL_SendGamepadEffect(Handle, ptr, data.Length));
        }
    }

    /// <summary>
    /// Gets the battery state of this gamepad.
    /// Note: Battery status can be unreliable; values are best estimates based on hardware reports.
    /// </summary>
    /// <param name="percent">The percentage of battery life left (0-100), or -1 if unknown.</param>
    /// <returns>The current battery state.</returns>
    public PowerState GetPowerInfo(out int percent)
    {
        ThrowIfDisposed();
        fixed (int* pPercent = &percent)
        {
            return (PowerState)SDL_GetGamepadPowerInfo(Handle, pPercent);
        }
    }

    /// <summary>
    /// Gets the sfSymbolsName for a given button on Apple platforms.
    /// </summary>
    /// <param name="button">The button to query.</param>
    /// <returns>The sfSymbolsName or null if not available.</returns>
    public string? GetAppleSFSymbolsNameForButton(GamepadButton button)
    {
        ThrowIfDisposed();
        return SDL_GetGamepadAppleSFSymbolsNameForButton(Handle, (SDL_GamepadButton)button);
    }

    /// <summary>
    /// Gets the sfSymbolsName for a given axis on Apple platforms.
    /// </summary>
    /// <param name="axis">The axis to query.</param>
    /// <returns>The sfSymbolsName or null if not available.</returns>
    public string? GetAppleSFSymbolsNameForAxis(GamepadAxis axis)
    {
        ThrowIfDisposed();
        return SDL_GetGamepadAppleSFSymbolsNameForAxis(Handle, (SDL_GamepadAxis)axis);
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
            SDL_CloseGamepad(Handle);
            Handle = null;
        }

        _disposed = true;
    }

    private void ThrowIfDisposed()
    {
        ObjectDisposedException.ThrowIf(_disposed, this);
    }
}
