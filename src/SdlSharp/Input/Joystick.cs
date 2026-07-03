// src/SdlSharp/Input/Joystick.cs
using System.Runtime.InteropServices;

using SdlSharp.Graphics;
using static SdlSharp.Native.Common;
using static SdlSharp.Native.Joystick;

namespace SdlSharp.Input;

/// <summary>
/// Managed wrapper for an SDL joystick device.
/// </summary>
public sealed unsafe class Joystick : IDisposable
{
    private readonly bool _ownsHandle;

    internal Native.SDL_Joystick* Handle
    {
        get
        {
            ObjectDisposedException.ThrowIf(_handle == null, this);
            return _handle;
        }
    }

    private Native.SDL_Joystick* _handle;

    internal Joystick(Native.SDL_Joystick* handle, bool ownsHandle = true)
    {
        _handle = handle;
        _ownsHandle = ownsHandle;
    }

    /// <summary>Opens a joystick by instance ID.</summary>
    public static Joystick Open(uint id) =>
        new(Check(SDL_OpenJoystick(new Native.SDL_JoystickID(id))));

    /// <summary>Gets the instance IDs of all connected joysticks.</summary>
    public static uint[] GetDevices()
    {
        int count;
        var ids = SDL_GetJoysticks(&count);
        if (ids == null) return [];
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

    /// <summary>Gets the name of a joystick by instance ID.</summary>
    public static string? GetName(uint id) =>
        Marshal.PtrToStringUTF8((nint)SDL_GetJoystickNameForID(new Native.SDL_JoystickID(id)));

    /// <summary>Gets the type of a joystick by instance ID.</summary>
    public static JoystickType GetType(uint id) =>
        (JoystickType)SDL_GetJoystickTypeForID(new Native.SDL_JoystickID(id));

    /// <summary>Gets the name of this joystick.</summary>
    public string? Name => Marshal.PtrToStringUTF8((nint)SDL_GetJoystickName(Handle));

    /// <summary>Gets the type of this joystick.</summary>
    public JoystickType Type => (JoystickType)SDL_GetJoystickType(Handle);

    /// <summary>Gets the number of axes.</summary>
    public int NumAxes => SDL_GetNumJoystickAxes(Handle);

    /// <summary>Gets the number of trackballs.</summary>
    public int NumBalls => SDL_GetNumJoystickBalls(Handle);

    /// <summary>Gets the number of hats.</summary>
    public int NumHats => SDL_GetNumJoystickHats(Handle);

    /// <summary>Gets the number of buttons.</summary>
    public int NumButtons => SDL_GetNumJoystickButtons(Handle);

    /// <summary>Gets the current value of an axis.</summary>
    public short GetAxis(int index) => SDL_GetJoystickAxis(Handle, index);

    /// <summary>Gets the current position of a hat.</summary>
    public HatPosition GetHat(int index) => (HatPosition)SDL_GetJoystickHat(Handle, index);

    /// <summary>Gets the current state of a button.</summary>
    public bool GetButton(int index) => SDL_GetJoystickButton(Handle, index);

    /// <summary>Gets the connection state of this joystick.</summary>
    public JoystickConnectionState ConnectionState =>
        (JoystickConnectionState)SDL_GetJoystickConnectionState(Handle);

    /// <summary>Updates the state of all open joysticks.</summary>
    public static void Update() => SDL_UpdateJoysticks();

    /// <summary>Gets whether any joystick is currently connected.</summary>
    public static bool HasJoystick => SDL_HasJoystick();

    /// <summary>Gets the instance ID of this joystick.</summary>
    public uint Id => CheckId(SDL_GetJoystickID(Handle).Value);

    /// <summary>Gets the joystick associated with an instance ID, or null if none. The returned wrapper does not own the handle.</summary>
    public static Joystick? FromId(uint id)
    {
        var handle = SDL_GetJoystickFromID(new Native.SDL_JoystickID(id));
        return handle == null ? null : new Joystick(handle, ownsHandle: false);
    }

    /// <summary>Gets the joystick associated with a player index, or null. The returned wrapper does not own the handle.</summary>
    public static Joystick? FromPlayerIndex(int playerIndex)
    {
        var handle = SDL_GetJoystickFromPlayerIndex(playerIndex);
        return handle == null ? null : new Joystick(handle, ownsHandle: false);
    }

    /// <summary>Gets whether this joystick is still connected.</summary>
    public bool Connected => SDL_JoystickConnected(Handle);

    /// <summary>Gets the GUID identifying this joystick model.</summary>
    public SdlGuid Guid => new(SDL_GetJoystickGUID(Handle));

    /// <summary>Gets the GUID for a joystick instance ID.</summary>
    public static SdlGuid GetGuidForId(uint id) => new(SDL_GetJoystickGUIDForID(new Native.SDL_JoystickID(id)));

    /// <summary>Gets the USB vendor ID, or 0 if unavailable.</summary>
    public ushort Vendor => SDL_GetJoystickVendor(Handle);

    /// <summary>Gets the USB product ID, or 0 if unavailable.</summary>
    public ushort Product => SDL_GetJoystickProduct(Handle);

    /// <summary>Gets the product version, or 0 if unavailable.</summary>
    public ushort ProductVersion => SDL_GetJoystickProductVersion(Handle);

    /// <summary>Gets the firmware version, or 0 if unavailable.</summary>
    public ushort FirmwareVersion => SDL_GetJoystickFirmwareVersion(Handle);

    /// <summary>Gets the serial number, or null if unavailable.</summary>
    public string? Serial => Marshal.PtrToStringUTF8((nint)SDL_GetJoystickSerial(Handle));

    /// <summary>Gets the implementation-dependent path, or null if unavailable.</summary>
    public string? Path => Marshal.PtrToStringUTF8((nint)SDL_GetJoystickPath(Handle));

    /// <summary>Gets the properties of this joystick (capability flags — names in <see cref="JoystickProperties"/>). Owned by SDL.</summary>
    public PropertyGroup Properties => new(SDL_GetJoystickProperties(Handle), ownsHandle: false);

    /// <summary>Gets or sets the player index, or -1 when unset. Setting -1 clears it.</summary>
    public int PlayerIndex
    {
        get => SDL_GetJoystickPlayerIndex(Handle);
        set => Check(SDL_SetJoystickPlayerIndex(Handle, value));
    }

    /// <summary>Gets the player index for an instance ID, or -1.</summary>
    public static int GetPlayerIndexForId(uint id) => SDL_GetJoystickPlayerIndexForID(new Native.SDL_JoystickID(id));

    /// <summary>Gets the battery state.</summary>
    /// <param name="percent">Receives the charge percentage, or -1 if unknown.</param>
    /// <returns>The power state.</returns>
    public PowerState GetPowerInfo(out int percent)
    {
        int p;
        var state = SDL_GetJoystickPowerInfo(Handle, &p);
        percent = p;
        return (PowerState)state;
    }

    /// <summary>Rumbles the joystick. Zero intensities stop rumbling.</summary>
    /// <param name="lowFrequency">Low-frequency (left) motor intensity.</param>
    /// <param name="highFrequency">High-frequency (right) motor intensity.</param>
    /// <param name="duration">How long to rumble.</param>
    public void Rumble(ushort lowFrequency, ushort highFrequency, TimeSpan duration) =>
        Check(SDL_RumbleJoystick(Handle, lowFrequency, highFrequency, ToMilliseconds(duration)));

    /// <summary>Rumbles the triggers. Not all devices support trigger rumble.</summary>
    /// <param name="left">Left trigger motor intensity.</param>
    /// <param name="right">Right trigger motor intensity.</param>
    /// <param name="duration">How long to rumble.</param>
    public void RumbleTriggers(ushort left, ushort right, TimeSpan duration) =>
        Check(SDL_RumbleJoystickTriggers(Handle, left, right, ToMilliseconds(duration)));

    /// <summary>Sets the LED color. Alpha is ignored.</summary>
    public void SetLed(Color color) => Check(SDL_SetJoystickLED(Handle, color.R, color.G, color.B));

    /// <summary>Sends a device-specific effect packet.</summary>
    public void SendEffect(ReadOnlySpan<byte> data)
    {
        fixed (byte* p = data)
            Check(SDL_SendJoystickEffect(Handle, p, data.Length));
    }

    /// <summary>Gets a trackball's movement delta since the last poll.</summary>
    public void GetBall(int ball, out int deltaX, out int deltaY)
    {
        int dx, dy;
        Check(SDL_GetJoystickBall(Handle, ball, &dx, &dy));
        deltaX = dx;
        deltaY = dy;
    }

    /// <summary>Enables or disables joystick event polling delivery.</summary>
    public static void SetEventsEnabled(bool enabled) => SDL_SetJoystickEventsEnabled(enabled);

    /// <summary>Gets whether joystick events are enabled.</summary>
    public static bool EventsEnabled => SDL_JoystickEventsEnabled();

    /// <summary>Minimum value a joystick axis reports.</summary>
    public const short AxisMin = Native.Joystick.SDL_JOYSTICK_AXIS_MIN;

    /// <summary>Maximum value a joystick axis reports.</summary>
    public const short AxisMax = Native.Joystick.SDL_JOYSTICK_AXIS_MAX;

    private static uint ToMilliseconds(TimeSpan duration) =>
        (uint)Math.Clamp(duration.TotalMilliseconds, 0, uint.MaxValue);

    /// <inheritdoc/>
    public void Dispose()
    {
        if (_ownsHandle && _handle != null)
        {
            SDL_CloseJoystick(_handle);
        }
        _handle = null;
    }
}
