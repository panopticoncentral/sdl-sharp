// src/SdlSharp/Input/Gamepad.cs
using System.Runtime.InteropServices;

using SdlSharp.Graphics;
using static SdlSharp.Native.Common;
using static SdlSharp.Native.Gamepad;

namespace SdlSharp.Input;

/// <summary>
/// Managed wrapper for an SDL gamepad device.
/// </summary>
public sealed unsafe class Gamepad : IDisposable
{
    private readonly bool _ownsHandle;

    internal Native.SDL_Gamepad* Handle
    {
        get
        {
            ObjectDisposedException.ThrowIf(_handle == null, this);
            return _handle;
        }
    }

    private Native.SDL_Gamepad* _handle;

    internal Gamepad(Native.SDL_Gamepad* handle, bool ownsHandle = true)
    {
        _handle = handle;
        _ownsHandle = ownsHandle;
    }

    /// <summary>Opens a gamepad by joystick instance ID.</summary>
    public static Gamepad Open(uint id) =>
        new(Check(SDL_OpenGamepad(new Native.SDL_JoystickID(id))));

    /// <summary>Gets the joystick instance IDs of all connected gamepads.</summary>
    /// <exception cref="SdlException">The gamepad list could not be retrieved.</exception>
    public static uint[] GetDevices()
    {
        int count;
        var ids = SDL_GetGamepads(&count);
        if (ids == null) throw new SdlException();
        try
        {
            var result = new uint[count];
            for (var i = 0; i < count; i++)
                result[i] = ids[i].Value;
            return result;
        }
        finally
        {
            SDL_free(ids);
        }
    }

    /// <summary>Returns whether the given joystick instance ID is a gamepad.</summary>
    public static bool IsGamepad(uint joystickId) =>
        SDL_IsGamepad(new Native.SDL_JoystickID(joystickId));

    /// <summary>Gets the name of a gamepad by joystick instance ID.</summary>
    public static string? GetName(uint id) =>
        Marshal.PtrToStringUTF8((nint)SDL_GetGamepadNameForID(new Native.SDL_JoystickID(id)));

    /// <summary>Gets the type of a gamepad by joystick instance ID.</summary>
    public static GamepadType GetType(uint id) =>
        (GamepadType)SDL_GetGamepadTypeForID(new Native.SDL_JoystickID(id));

    /// <summary>Gets the name of this gamepad.</summary>
    public string? Name => Marshal.PtrToStringUTF8((nint)SDL_GetGamepadName(Handle));

    /// <summary>Gets the type of this gamepad.</summary>
    public GamepadType Type => (GamepadType)SDL_GetGamepadType(Handle);

    /// <summary>Gets the current value of an axis.</summary>
    public short GetAxis(GamepadAxis axis) =>
        SDL_GetGamepadAxis(Handle, (Native.SDL_GamepadAxis)axis);

    /// <summary>Gets the current state of a button.</summary>
    public bool GetButton(GamepadButton button) =>
        SDL_GetGamepadButton(Handle, (Native.SDL_GamepadButton)button);

    /// <summary>Gets the label for a button on this gamepad.</summary>
    public GamepadButtonLabel GetButtonLabel(GamepadButton button) =>
        (GamepadButtonLabel)SDL_GetGamepadButtonLabel(Handle, (Native.SDL_GamepadButton)button);

    /// <summary>Gets the label for a button on a given gamepad type.</summary>
    public static GamepadButtonLabel GetButtonLabelForType(GamepadType type, GamepadButton button) =>
        (GamepadButtonLabel)SDL_GetGamepadButtonLabelForType((Native.SDL_GamepadType)type, (Native.SDL_GamepadButton)button);

    /// <summary>Gets the connection state of this gamepad.</summary>
    public JoystickConnectionState ConnectionState =>
        (JoystickConnectionState)SDL_GetGamepadConnectionState(Handle);

    /// <summary>Updates the state of all open gamepads.</summary>
    public static void Update() => SDL_UpdateGamepads();

    /// <summary>Gets whether any gamepad is currently connected.</summary>
    public static bool HasGamepad => SDL_HasGamepad();

    /// <summary>Gets the instance ID of this gamepad.</summary>
    public uint Id => CheckId(SDL_GetGamepadID(Handle).Value);

    /// <summary>Gets the gamepad associated with an instance ID, or null if none. The returned wrapper does not own the handle.</summary>
    public static Gamepad? FromId(uint id)
    {
        var handle = SDL_GetGamepadFromID(new Native.SDL_JoystickID(id));
        return handle == null ? null : new Gamepad(handle, ownsHandle: false);
    }

    /// <summary>Gets the gamepad associated with a player index, or null. The returned wrapper does not own the handle.</summary>
    public static Gamepad? FromPlayerIndex(int playerIndex)
    {
        var handle = SDL_GetGamepadFromPlayerIndex(playerIndex);
        return handle == null ? null : new Gamepad(handle, ownsHandle: false);
    }

    /// <summary>Gets whether this gamepad is still connected.</summary>
    public bool Connected => SDL_GamepadConnected(Handle);

    /// <summary>Gets the GUID of the underlying joystick device.</summary>
    public SdlGuid Guid => new(SDL_GetGamepadGUIDForID(new Native.SDL_JoystickID(Id)));

    /// <summary>Gets the GUID for a gamepad instance ID.</summary>
    public static SdlGuid GetGuidForId(uint id) => new(SDL_GetGamepadGUIDForID(new Native.SDL_JoystickID(id)));

    /// <summary>Gets the underlying joystick as a non-owning wrapper.</summary>
    public Joystick GetJoystick() => new(Check(SDL_GetGamepadJoystick(Handle)), ownsHandle: false);

    /// <summary>Gets the Steam Input handle, or 0 if unavailable.</summary>
    public ulong SteamHandle => SDL_GetGamepadSteamHandle(Handle);

    /// <summary>Gets the hardware type, ignoring any mapping override (contrast <see cref="Type"/>).</summary>
    public GamepadType RealType => (GamepadType)SDL_GetRealGamepadType(Handle);

    /// <summary>Gets the hardware type for an instance ID, ignoring mapping overrides.</summary>
    public static GamepadType GetRealType(uint id) => (GamepadType)SDL_GetRealGamepadTypeForID(new Native.SDL_JoystickID(id));

    /// <summary>Gets the USB vendor ID, or 0 if unavailable.</summary>
    public ushort Vendor => SDL_GetGamepadVendor(Handle);

    /// <summary>Gets the USB vendor ID for a gamepad instance ID, or 0 if unavailable.</summary>
    public static ushort GetVendorForId(uint id) => SDL_GetGamepadVendorForID(new Native.SDL_JoystickID(id));

    /// <summary>Gets the USB product ID, or 0 if unavailable.</summary>
    public ushort Product => SDL_GetGamepadProduct(Handle);

    /// <summary>Gets the USB product ID for a gamepad instance ID, or 0 if unavailable.</summary>
    public static ushort GetProductForId(uint id) => SDL_GetGamepadProductForID(new Native.SDL_JoystickID(id));

    /// <summary>Gets the product version, or 0 if unavailable.</summary>
    public ushort ProductVersion => SDL_GetGamepadProductVersion(Handle);

    /// <summary>Gets the product version for a gamepad instance ID, or 0 if unavailable.</summary>
    public static ushort GetProductVersionForId(uint id) => SDL_GetGamepadProductVersionForID(new Native.SDL_JoystickID(id));

    /// <summary>Gets the firmware version, or 0 if unavailable.</summary>
    public ushort FirmwareVersion => SDL_GetGamepadFirmwareVersion(Handle);

    /// <summary>Gets the serial number, or null if unavailable.</summary>
    public string? Serial => Marshal.PtrToStringUTF8((nint)SDL_GetGamepadSerial(Handle));

    /// <summary>Gets the implementation-dependent path, or null if unavailable.</summary>
    public string? Path => Marshal.PtrToStringUTF8((nint)SDL_GetGamepadPath(Handle));

    /// <summary>Gets the implementation-dependent path for a gamepad instance ID, or null if unavailable.</summary>
    public static string? GetPathForId(uint id) =>
        Marshal.PtrToStringUTF8((nint)SDL_GetGamepadPathForID(new Native.SDL_JoystickID(id)));

    /// <summary>Gets the properties of this gamepad (capability flags — names in <see cref="GamepadProperties"/>). Owned by SDL.</summary>
    public PropertyGroup Properties => new(SDL_GetGamepadProperties(Handle), ownsHandle: false);

    /// <summary>
    /// Gets or sets the player index. The getter returns -1 when no player index is
    /// available for this gamepad; setting -1 clears the index.
    /// </summary>
    public int PlayerIndex
    {
        get => SDL_GetGamepadPlayerIndex(Handle);
        set => Check(SDL_SetGamepadPlayerIndex(Handle, value));
    }

    /// <summary>Gets the player index for an instance ID, or -1.</summary>
    public static int GetPlayerIndexForId(uint id) => SDL_GetGamepadPlayerIndexForID(new Native.SDL_JoystickID(id));

    /// <summary>
    /// Gets the battery state. This method does not throw on failure — unlike the
    /// <c>Check</c>-wrapped methods in this class, failure is reported by returning
    /// <see cref="PowerState.Error"/>, which callers must handle.
    /// </summary>
    /// <param name="percent">Receives the charge percentage, or -1 if unknown.</param>
    /// <returns>The power state (<see cref="PowerState.Error"/> on failure).</returns>
    public PowerState GetPowerInfo(out int percent)
    {
        int p;
        var state = SDL_GetGamepadPowerInfo(Handle, &p);
        percent = p;
        return (PowerState)state;
    }

    /// <summary>
    /// Rumbles the gamepad. Zero intensities stop rumbling. Throws <see cref="SdlException"/>
    /// on devices without rumble support. The rumble command is processed when events are
    /// pumped (<see cref="Update"/> or the event loop).
    /// </summary>
    /// <param name="lowFrequency">Low-frequency (left) motor intensity.</param>
    /// <param name="highFrequency">High-frequency (right) motor intensity.</param>
    /// <param name="duration">How long to rumble.</param>
    public void Rumble(ushort lowFrequency, ushort highFrequency, TimeSpan duration) =>
        Check(SDL_RumbleGamepad(Handle, lowFrequency, highFrequency, ToMilliseconds(duration)));

    /// <summary>
    /// Rumbles the triggers. Throws <see cref="SdlException"/> on devices without trigger
    /// rumble support. The rumble command is processed when events are pumped
    /// (<see cref="Update"/> or the event loop).
    /// </summary>
    /// <param name="left">Left trigger motor intensity.</param>
    /// <param name="right">Right trigger motor intensity.</param>
    /// <param name="duration">How long to rumble.</param>
    public void RumbleTriggers(ushort left, ushort right, TimeSpan duration) =>
        Check(SDL_RumbleGamepadTriggers(Handle, left, right, ToMilliseconds(duration)));

    /// <summary>
    /// Sets the LED color. Alpha is ignored. Throws <see cref="SdlException"/> on devices
    /// without an LED — query <see cref="GamepadProperties.CapRgbLed"/> via
    /// <see cref="Properties"/> to check support first.
    /// </summary>
    public void SetLed(Color color) => Check(SDL_SetGamepadLED(Handle, color.R, color.G, color.B));

    /// <summary>Sends a device-specific effect packet.</summary>
    public void SendEffect(ReadOnlySpan<byte> data)
    {
        fixed (byte* p = data)
            Check(SDL_SendGamepadEffect(Handle, p, data.Length));
    }

    /// <summary>Enables or disables gamepad event polling delivery.</summary>
    public static void SetEventsEnabled(bool enabled) => SDL_SetGamepadEventsEnabled(enabled);

    /// <summary>Gets whether gamepad events are enabled.</summary>
    public static bool EventsEnabled => SDL_GamepadEventsEnabled();

    /// <summary>Reloads the gamepad mapping database from the platform-provided source(s).</summary>
    public static void ReloadMappings() => Check(SDL_ReloadGamepadMappings());

    /// <summary>Adds a controller mapping string.</summary>
    /// <returns>True if a new mapping was added, false if an existing one was updated.</returns>
    public static bool AddMapping(string mapping)
    {
        var result = SDL_AddGamepadMapping(ToUtf8(mapping));
        if (result < 0) throw new SdlException();
        return result == 1;
    }

    /// <summary>Loads controller mappings from a file (e.g. gamecontrollerdb.txt).</summary>
    /// <returns>The number of mappings added.</returns>
    public static int AddMappingsFromFile(string path)
    {
        var result = SDL_AddGamepadMappingsFromFile(ToUtf8(path));
        if (result < 0) throw new SdlException();
        return result;
    }

    /// <summary>Gets this gamepad's mapping string, or null if none.</summary>
    public string? GetMapping()
    {
        var native = SDL_GetGamepadMapping(Handle);
        if (native == null) return null;
        var result = Marshal.PtrToStringUTF8((nint)native);
        SDL_free(native);
        return result;
    }

    /// <summary>Gets the mapping string for a device GUID, or null if none.</summary>
    public static string? GetMappingForGuid(SdlGuid guid)
    {
        var native = SDL_GetGamepadMappingForGUID(guid.ToNative());
        if (native == null) return null;
        var result = Marshal.PtrToStringUTF8((nint)native);
        SDL_free(native);
        return result;
    }

    /// <summary>Gets the mapping string for a gamepad instance ID, or null if none.</summary>
    public static string? GetMappingForId(uint id)
    {
        var native = SDL_GetGamepadMappingForID(new Native.SDL_JoystickID(id));
        if (native == null) return null;
        var result = Marshal.PtrToStringUTF8((nint)native);
        SDL_free(native);
        return result;
    }

    /// <summary>Sets or clears (null) the mapping for a joystick instance ID.</summary>
    public static void SetMapping(uint joystickId, string? mapping) =>
        Check(SDL_SetGamepadMapping(new Native.SDL_JoystickID(joystickId), ToUtf8(mapping)));

    /// <summary>Gets whether this gamepad reports the given axis.</summary>
    public bool HasAxis(GamepadAxis axis) => SDL_GamepadHasAxis(Handle, (Native.SDL_GamepadAxis)axis);

    /// <summary>Gets whether this gamepad reports the given button.</summary>
    public bool HasButton(GamepadButton button) => SDL_GamepadHasButton(Handle, (Native.SDL_GamepadButton)button);

    /// <summary>Parses an axis from its mapping-string name, or Invalid.</summary>
    public static GamepadAxis GetAxisFromString(string name) =>
        (GamepadAxis)SDL_GetGamepadAxisFromString(ToUtf8(name));

    /// <summary>Gets the mapping-string name for an axis, or null.</summary>
    public static string? GetStringForAxis(GamepadAxis axis) =>
        Marshal.PtrToStringUTF8((nint)SDL_GetGamepadStringForAxis((Native.SDL_GamepadAxis)axis));

    /// <summary>Parses a button from its mapping-string name, or Invalid.</summary>
    public static GamepadButton GetButtonFromString(string name) =>
        (GamepadButton)SDL_GetGamepadButtonFromString(ToUtf8(name));

    /// <summary>Gets the mapping-string name for a button, or null.</summary>
    public static string? GetStringForButton(GamepadButton button) =>
        Marshal.PtrToStringUTF8((nint)SDL_GetGamepadStringForButton((Native.SDL_GamepadButton)button));

    /// <summary>Parses a gamepad type from its string name, or Unknown.</summary>
    public static GamepadType GetTypeFromString(string name) =>
        (GamepadType)SDL_GetGamepadTypeFromString(ToUtf8(name));

    /// <summary>Gets the string name for a gamepad type, or null. Owned by SDL — not freed.</summary>
    public static string? GetStringForType(GamepadType type) =>
        Marshal.PtrToStringUTF8((nint)SDL_GetGamepadStringForType((Native.SDL_GamepadType)type));

    /// <summary>Gets the number of touchpads on this gamepad.</summary>
    public int NumTouchpads => SDL_GetNumGamepadTouchpads(Handle);

    /// <summary>Gets the number of simultaneous fingers a touchpad supports.</summary>
    public int GetNumTouchpadFingers(int touchpad) => SDL_GetNumGamepadTouchpadFingers(Handle, touchpad);

    /// <summary>Gets the state of a finger on a touchpad.</summary>
    public void GetTouchpadFinger(int touchpad, int finger, out bool down, out float x, out float y, out float pressure)
    {
        byte d;
        float fx, fy, fp;
        Check(SDL_GetGamepadTouchpadFinger(Handle, touchpad, finger, &d, &fx, &fy, &fp));
        down = d != 0;
        x = fx;
        y = fy;
        pressure = fp;
    }

    /// <summary>Gets whether this gamepad has the given sensor.</summary>
    public bool HasSensor(SensorType type) => SDL_GamepadHasSensor(Handle, (Native.SDL_SensorType)type);

    /// <summary>Enables or disables a sensor.</summary>
    public void SetSensorEnabled(SensorType type, bool enabled) =>
        Check(SDL_SetGamepadSensorEnabled(Handle, (Native.SDL_SensorType)type, enabled));

    /// <summary>Gets whether a sensor is enabled.</summary>
    public bool IsSensorEnabled(SensorType type) => SDL_GamepadSensorEnabled(Handle, (Native.SDL_SensorType)type);

    /// <summary>Gets a sensor's data rate in events per second, or 0.</summary>
    public float GetSensorDataRate(SensorType type) => SDL_GetGamepadSensorDataRate(Handle, (Native.SDL_SensorType)type);

    /// <summary>Reads the current sensor state into the given buffer.</summary>
    public void GetSensorData(SensorType type, Span<float> data)
    {
        fixed (float* p = data)
            Check(SDL_GetGamepadSensorData(Handle, (Native.SDL_SensorType)type, p, data.Length));
    }

    private static uint ToMilliseconds(TimeSpan duration) =>
        (uint)Math.Clamp(duration.TotalMilliseconds, 0, uint.MaxValue);

    /// <inheritdoc/>
    public void Dispose()
    {
        if (_ownsHandle && _handle != null)
        {
            SDL_CloseGamepad(_handle);
        }
        _handle = null;
    }
}
