using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

using static Sdl3Sharp.Native.Common;
using static Sdl3Sharp.Native.Power;
using static Sdl3Sharp.Native.Properties;
using static Sdl3Sharp.Native.Sensor;

// We are intentionally exposing the P/Invoke calls so people can do low-level calls if needed
#pragma warning disable CA1401 // P/Invokes should not be visible

namespace Sdl3Sharp.Native;

/// <summary>
/// P/Invoke bindings for SDL_joystick.h - Joystick support.
/// This is the lower-level joystick handling. If you want the simpler option,
/// where what each button does is well-defined, you should use the gamepad API instead.
/// </summary>
public static unsafe partial class Joystick
{
    /// <summary>
    /// A unique ID for a joystick for the time it is connected to the system,
    /// and is never reused for the lifetime of the application.
    /// If the joystick is disconnected and reconnected, it will get a new ID.
    /// The value 0 is an invalid ID.
    /// </summary>
    /// <param name="value">The joystick ID value.</param>
    public readonly struct SDL_JoystickID(uint value)
    {
        /// <summary>The underlying joystick ID value.</summary>
        public readonly uint Value = value;

        /// <summary>Implicitly converts an SDL_JoystickID to uint.</summary>
        /// <param name="id">The joystick ID to convert.</param>
        public static implicit operator uint(SDL_JoystickID id)
        {
            return id.Value;
        }

        /// <summary>Implicitly converts a uint to SDL_JoystickID.</summary>
        /// <param name="value">The value to convert.</param>
        public static implicit operator SDL_JoystickID(uint value)
        {
            return new(value);
        }
    }

    /// <summary>
    /// An enum of some common joystick types.
    /// In some cases, SDL can identify a low-level joystick as being a certain
    /// type of device, and will report it through SDL_GetJoystickType.
    /// </summary>
    public enum SDL_JoystickType
    {
        /// <summary>Unknown joystick type.</summary>
        SDL_JOYSTICK_TYPE_UNKNOWN,
        /// <summary>Gamepad.</summary>
        SDL_JOYSTICK_TYPE_GAMEPAD,
        /// <summary>Steering wheel.</summary>
        SDL_JOYSTICK_TYPE_WHEEL,
        /// <summary>Arcade stick.</summary>
        SDL_JOYSTICK_TYPE_ARCADE_STICK,
        /// <summary>Flight stick.</summary>
        SDL_JOYSTICK_TYPE_FLIGHT_STICK,
        /// <summary>Dance pad.</summary>
        SDL_JOYSTICK_TYPE_DANCE_PAD,
        /// <summary>Guitar controller.</summary>
        SDL_JOYSTICK_TYPE_GUITAR,
        /// <summary>Drum kit.</summary>
        SDL_JOYSTICK_TYPE_DRUM_KIT,
        /// <summary>Arcade pad.</summary>
        SDL_JOYSTICK_TYPE_ARCADE_PAD,
        /// <summary>Throttle.</summary>
        SDL_JOYSTICK_TYPE_THROTTLE,
        /// <summary>Number of joystick types.</summary>
        SDL_JOYSTICK_TYPE_COUNT
    }

    /// <summary>
    /// Possible connection states for a joystick device.
    /// This is used by SDL_GetJoystickConnectionState to report how a device is connected to the system.
    /// </summary>
    public enum SDL_JoystickConnectionState
    {
        /// <summary>Invalid connection state.</summary>
        SDL_JOYSTICK_CONNECTION_INVALID = -1,
        /// <summary>Unknown connection state.</summary>
        SDL_JOYSTICK_CONNECTION_UNKNOWN,
        /// <summary>Joystick is connected via a wire.</summary>
        SDL_JOYSTICK_CONNECTION_WIRED,
        /// <summary>Joystick is connected wirelessly.</summary>
        SDL_JOYSTICK_CONNECTION_WIRELESS
    }

    /// <summary>The largest value an SDL_Joystick's axis can report.</summary>
    public const short SDL_JOYSTICK_AXIS_MAX = 32767;

    /// <summary>The smallest value an SDL_Joystick's axis can report. This is a negative number.</summary>
    public const short SDL_JOYSTICK_AXIS_MIN = -32768;

    /// <summary>Hat position: centered (no direction).</summary>
    public const byte SDL_HAT_CENTERED = 0x00;
    /// <summary>Hat position: up.</summary>
    public const byte SDL_HAT_UP = 0x01;
    /// <summary>Hat position: right.</summary>
    public const byte SDL_HAT_RIGHT = 0x02;
    /// <summary>Hat position: down.</summary>
    public const byte SDL_HAT_DOWN = 0x04;
    /// <summary>Hat position: left.</summary>
    public const byte SDL_HAT_LEFT = 0x08;
    /// <summary>Hat position: right and up.</summary>
    public const byte SDL_HAT_RIGHTUP = SDL_HAT_RIGHT | SDL_HAT_UP;
    /// <summary>Hat position: right and down.</summary>
    public const byte SDL_HAT_RIGHTDOWN = SDL_HAT_RIGHT | SDL_HAT_DOWN;
    /// <summary>Hat position: left and up.</summary>
    public const byte SDL_HAT_LEFTUP = SDL_HAT_LEFT | SDL_HAT_UP;
    /// <summary>Hat position: left and down.</summary>
    public const byte SDL_HAT_LEFTDOWN = SDL_HAT_LEFT | SDL_HAT_DOWN;

    /// <summary>Property name indicating whether this joystick has an LED that has adjustable brightness.</summary>
    public const string SDL_PROP_JOYSTICK_CAP_MONO_LED_BOOLEAN = "SDL.joystick.cap.mono_led";
    /// <summary>Property name indicating whether this joystick has an LED that has adjustable color.</summary>
    public const string SDL_PROP_JOYSTICK_CAP_RGB_LED_BOOLEAN = "SDL.joystick.cap.rgb_led";
    /// <summary>Property name indicating whether this joystick has a player LED.</summary>
    public const string SDL_PROP_JOYSTICK_CAP_PLAYER_LED_BOOLEAN = "SDL.joystick.cap.player_led";
    /// <summary>Property name indicating whether this joystick has left/right rumble.</summary>
    public const string SDL_PROP_JOYSTICK_CAP_RUMBLE_BOOLEAN = "SDL.joystick.cap.rumble";
    /// <summary>Property name indicating whether this joystick has simple trigger rumble.</summary>
    public const string SDL_PROP_JOYSTICK_CAP_TRIGGER_RUMBLE_BOOLEAN = "SDL.joystick.cap.trigger_rumble";

    /// <summary>
    /// The opaque structure used to identify an SDL joystick.
    /// </summary>
    public struct SDL_Joystick
    {
    }

    /// <summary>
    /// The structure that describes a virtual joystick touchpad.
    /// </summary>
    public struct SDL_VirtualJoystickTouchpadDesc
    {
        /// <summary>The number of simultaneous fingers on this touchpad.</summary>
        public ushort nfingers;
        /// <summary>Padding for alignment.</summary>
        public ushort padding1;
        /// <summary>Padding for alignment.</summary>
        public ushort padding2;
        /// <summary>Padding for alignment.</summary>
        public ushort padding3;
    }

    /// <summary>
    /// The structure that describes a virtual joystick sensor.
    /// </summary>
    public struct SDL_VirtualJoystickSensorDesc
    {
        /// <summary>The type of this sensor.</summary>
        public SDL_SensorType type;
        /// <summary>The update frequency of this sensor, may be 0.0f.</summary>
        public float rate;
    }

    /// <summary>
    /// The structure that describes a virtual joystick.
    /// This structure should be initialized using SDL_INIT_INTERFACE().
    /// All elements of this structure are optional.
    /// </summary>
    public struct SDL_VirtualJoystickDesc
    {
        /// <summary>The version of this interface.</summary>
        public uint version;
        /// <summary>SDL_JoystickType value.</summary>
        public ushort type;
        /// <summary>Unused padding.</summary>
        public ushort padding;
        /// <summary>The USB vendor ID of this joystick.</summary>
        public ushort vendor_id;
        /// <summary>The USB product ID of this joystick.</summary>
        public ushort product_id;
        /// <summary>The number of axes on this joystick.</summary>
        public ushort naxes;
        /// <summary>The number of buttons on this joystick.</summary>
        public ushort nbuttons;
        /// <summary>The number of balls on this joystick.</summary>
        public ushort nballs;
        /// <summary>The number of hats on this joystick.</summary>
        public ushort nhats;
        /// <summary>The number of touchpads on this joystick, requires touchpads to point at valid descriptions.</summary>
        public ushort ntouchpads;
        /// <summary>The number of sensors on this joystick, requires sensors to point at valid descriptions.</summary>
        public ushort nsensors;
        /// <summary>Unused padding.</summary>
        public ushort padding2_0;
        /// <summary>Unused padding.</summary>
        public ushort padding2_1;
        /// <summary>A mask of which buttons are valid for this controller, e.g. (1 &lt;&lt; SDL_GAMEPAD_BUTTON_SOUTH).</summary>
        public uint button_mask;
        /// <summary>A mask of which axes are valid for this controller, e.g. (1 &lt;&lt; SDL_GAMEPAD_AXIS_LEFTX).</summary>
        public uint axis_mask;
        /// <summary>The name of the joystick.</summary>
        public byte* name;
        /// <summary>A pointer to an array of touchpad descriptions, required if ntouchpads is greater than 0.</summary>
        public SDL_VirtualJoystickTouchpadDesc* touchpads;
        /// <summary>A pointer to an array of sensor descriptions, required if nsensors is greater than 0.</summary>
        public SDL_VirtualJoystickSensorDesc* sensors;
        /// <summary>User data pointer passed to callbacks.</summary>
        public void* userdata;
        /// <summary>Called when the joystick state should be updated.</summary>
        public delegate* unmanaged[Cdecl]<void*, void> Update;
        /// <summary>Called when the player index is set.</summary>
        public delegate* unmanaged[Cdecl]<void*, int, void> SetPlayerIndex;
        /// <summary>Implements SDL_RumbleJoystick().</summary>
        public delegate* unmanaged[Cdecl]<void*, ushort, ushort, byte> Rumble;
        /// <summary>Implements SDL_RumbleJoystickTriggers().</summary>
        public delegate* unmanaged[Cdecl]<void*, ushort, ushort, byte> RumbleTriggers;
        /// <summary>Implements SDL_SetJoystickLED().</summary>
        public delegate* unmanaged[Cdecl]<void*, byte, byte, byte, byte> SetLED;
        /// <summary>Implements SDL_SendJoystickEffect().</summary>
        public delegate* unmanaged[Cdecl]<void*, void*, int, byte> SendEffect;
        /// <summary>Implements SDL_SetGamepadSensorEnabled().</summary>
        public delegate* unmanaged[Cdecl]<void*, byte, byte> SetSensorsEnabled;
        /// <summary>Cleans up the userdata when the joystick is detached.</summary>
        public delegate* unmanaged[Cdecl]<void*, void> Cleanup;
    }

    /// <summary>
    /// Locking for atomic access to the joystick API.
    /// The SDL joystick functions are thread-safe, however you can lock the
    /// joysticks while processing to guarantee that the joystick list won't change
    /// and joystick and gamepad events will not be delivered.
    /// </summary>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SDL_LockJoysticks();

    /// <summary>
    /// Unlocking for atomic access to the joystick API.
    /// </summary>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SDL_UnlockJoysticks();

    /// <summary>
    /// Return whether a joystick is currently connected.
    /// </summary>
    /// <returns>True if a joystick is connected, false otherwise.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_HasJoystick();

    /// <summary>
    /// Get a list of currently connected joysticks.
    /// </summary>
    /// <param name="count">A pointer filled in with the number of joysticks returned, may be NULL.</param>
    /// <returns>A 0 terminated array of joystick instance IDs or NULL on failure; call SDL_GetError() for more information. This should be freed with SDL_free() when it is no longer needed.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL_JoystickID* SDL_GetJoysticks(int* count);

    /// <summary>
    /// Get the implementation dependent name of a joystick.
    /// This can be called before any joysticks are opened.
    /// </summary>
    /// <param name="instance_id">The joystick instance ID.</param>
    /// <returns>The name of the selected joystick. If no name can be found, this function returns NULL; call SDL_GetError() for more information.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(Utf8StringMarshaller))]
    public static partial string? SDL_GetJoystickNameForID(SDL_JoystickID instance_id);

    /// <summary>
    /// Get the implementation dependent path of a joystick.
    /// This can be called before any joysticks are opened.
    /// </summary>
    /// <param name="instance_id">The joystick instance ID.</param>
    /// <returns>The path of the selected joystick. If no path can be found, this function returns NULL; call SDL_GetError() for more information.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(Utf8StringMarshaller))]
    public static partial string? SDL_GetJoystickPathForID(SDL_JoystickID instance_id);

    /// <summary>
    /// Get the player index of a joystick.
    /// This can be called before any joysticks are opened.
    /// </summary>
    /// <param name="instance_id">The joystick instance ID.</param>
    /// <returns>The player index of a joystick, or -1 if it's not available.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int SDL_GetJoystickPlayerIndexForID(SDL_JoystickID instance_id);

    /// <summary>
    /// Get the implementation-dependent GUID of a joystick.
    /// This can be called before any joysticks are opened.
    /// </summary>
    /// <param name="instance_id">The joystick instance ID.</param>
    /// <returns>The GUID of the selected joystick. If called with an invalid instance_id, this function returns a zero GUID.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial Guid SDL_GetJoystickGUIDForID(SDL_JoystickID instance_id);

    /// <summary>
    /// Get the USB vendor ID of a joystick, if available.
    /// This can be called before any joysticks are opened. If the vendor ID isn't available this function returns 0.
    /// </summary>
    /// <param name="instance_id">The joystick instance ID.</param>
    /// <returns>The USB vendor ID of the selected joystick. If called with an invalid instance_id, this function returns 0.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial ushort SDL_GetJoystickVendorForID(SDL_JoystickID instance_id);

    /// <summary>
    /// Get the USB product ID of a joystick, if available.
    /// This can be called before any joysticks are opened. If the product ID isn't available this function returns 0.
    /// </summary>
    /// <param name="instance_id">The joystick instance ID.</param>
    /// <returns>The USB product ID of the selected joystick. If called with an invalid instance_id, this function returns 0.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial ushort SDL_GetJoystickProductForID(SDL_JoystickID instance_id);

    /// <summary>
    /// Get the product version of a joystick, if available.
    /// This can be called before any joysticks are opened. If the product version isn't available this function returns 0.
    /// </summary>
    /// <param name="instance_id">The joystick instance ID.</param>
    /// <returns>The product version of the selected joystick. If called with an invalid instance_id, this function returns 0.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial ushort SDL_GetJoystickProductVersionForID(SDL_JoystickID instance_id);

    /// <summary>
    /// Get the type of a joystick, if available.
    /// This can be called before any joysticks are opened.
    /// </summary>
    /// <param name="instance_id">The joystick instance ID.</param>
    /// <returns>The SDL_JoystickType of the selected joystick. If called with an invalid instance_id, this function returns SDL_JOYSTICK_TYPE_UNKNOWN.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL_JoystickType SDL_GetJoystickTypeForID(SDL_JoystickID instance_id);

    /// <summary>
    /// Open a joystick for use.
    /// The joystick subsystem must be initialized before a joystick can be opened for use.
    /// </summary>
    /// <param name="instance_id">The joystick instance ID.</param>
    /// <returns>A joystick identifier or NULL on failure; call SDL_GetError() for more information.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL_Joystick* SDL_OpenJoystick(SDL_JoystickID instance_id);

    /// <summary>
    /// Get the SDL_Joystick associated with an instance ID, if it has been opened.
    /// </summary>
    /// <param name="instance_id">The instance ID to get the SDL_Joystick for.</param>
    /// <returns>An SDL_Joystick on success or NULL on failure or if it hasn't been opened yet; call SDL_GetError() for more information.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL_Joystick* SDL_GetJoystickFromID(SDL_JoystickID instance_id);

    /// <summary>
    /// Get the SDL_Joystick associated with a player index.
    /// </summary>
    /// <param name="player_index">The player index to get the SDL_Joystick for.</param>
    /// <returns>An SDL_Joystick on success or NULL on failure; call SDL_GetError() for more information.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL_Joystick* SDL_GetJoystickFromPlayerIndex(int player_index);

    /// <summary>
    /// Attach a new virtual joystick.
    /// </summary>
    /// <param name="desc">Joystick description, initialized using SDL_INIT_INTERFACE().</param>
    /// <returns>The joystick instance ID, or 0 on failure; call SDL_GetError() for more information.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL_JoystickID SDL_AttachVirtualJoystick(SDL_VirtualJoystickDesc* desc);

    /// <summary>
    /// Detach a virtual joystick.
    /// </summary>
    /// <param name="instance_id">The joystick instance ID, previously returned from SDL_AttachVirtualJoystick().</param>
    /// <returns>True on success or false on failure; call SDL_GetError() for more information.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_DetachVirtualJoystick(SDL_JoystickID instance_id);

    /// <summary>
    /// Query whether or not a joystick is virtual.
    /// </summary>
    /// <param name="instance_id">The joystick instance ID.</param>
    /// <returns>True if the joystick is virtual, false otherwise.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_IsJoystickVirtual(SDL_JoystickID instance_id);

    /// <summary>
    /// Set the state of an axis on an opened virtual joystick.
    /// Please note that values set here will not be applied until the next call to SDL_UpdateJoysticks.
    /// </summary>
    /// <param name="joystick">The virtual joystick on which to set state.</param>
    /// <param name="axis">The index of the axis on the virtual joystick to update.</param>
    /// <param name="value">The new value for the specified axis.</param>
    /// <returns>True on success or false on failure; call SDL_GetError() for more information.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_SetJoystickVirtualAxis(SDL_Joystick* joystick, int axis, short value);

    /// <summary>
    /// Generate ball motion on an opened virtual joystick.
    /// Please note that values set here will not be applied until the next call to SDL_UpdateJoysticks.
    /// </summary>
    /// <param name="joystick">The virtual joystick on which to set state.</param>
    /// <param name="ball">The index of the ball on the virtual joystick to update.</param>
    /// <param name="xrel">The relative motion on the X axis.</param>
    /// <param name="yrel">The relative motion on the Y axis.</param>
    /// <returns>True on success or false on failure; call SDL_GetError() for more information.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_SetJoystickVirtualBall(SDL_Joystick* joystick, int ball, short xrel, short yrel);

    /// <summary>
    /// Set the state of a button on an opened virtual joystick.
    /// Please note that values set here will not be applied until the next call to SDL_UpdateJoysticks.
    /// </summary>
    /// <param name="joystick">The virtual joystick on which to set state.</param>
    /// <param name="button">The index of the button on the virtual joystick to update.</param>
    /// <param name="down">True if the button is pressed, false otherwise.</param>
    /// <returns>True on success or false on failure; call SDL_GetError() for more information.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_SetJoystickVirtualButton(SDL_Joystick* joystick, int button, [MarshalAs(UnmanagedType.U1)] bool down);

    /// <summary>
    /// Set the state of a hat on an opened virtual joystick.
    /// Please note that values set here will not be applied until the next call to SDL_UpdateJoysticks.
    /// </summary>
    /// <param name="joystick">The virtual joystick on which to set state.</param>
    /// <param name="hat">The index of the hat on the virtual joystick to update.</param>
    /// <param name="value">The new value for the specified hat.</param>
    /// <returns>True on success or false on failure; call SDL_GetError() for more information.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_SetJoystickVirtualHat(SDL_Joystick* joystick, int hat, byte value);

    /// <summary>
    /// Set touchpad finger state on an opened virtual joystick.
    /// Please note that values set here will not be applied until the next call to SDL_UpdateJoysticks.
    /// </summary>
    /// <param name="joystick">The virtual joystick on which to set state.</param>
    /// <param name="touchpad">The index of the touchpad on the virtual joystick to update.</param>
    /// <param name="finger">The index of the finger on the touchpad to set.</param>
    /// <param name="down">True if the finger is pressed, false if the finger is released.</param>
    /// <param name="x">The x coordinate of the finger on the touchpad, normalized 0 to 1, with the origin in the upper left.</param>
    /// <param name="y">The y coordinate of the finger on the touchpad, normalized 0 to 1, with the origin in the upper left.</param>
    /// <param name="pressure">The pressure of the finger.</param>
    /// <returns>True on success or false on failure; call SDL_GetError() for more information.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_SetJoystickVirtualTouchpad(SDL_Joystick* joystick, int touchpad, int finger, [MarshalAs(UnmanagedType.U1)] bool down, float x, float y, float pressure);

    /// <summary>
    /// Send a sensor update for an opened virtual joystick.
    /// Please note that values set here will not be applied until the next call to SDL_UpdateJoysticks.
    /// </summary>
    /// <param name="joystick">The virtual joystick on which to set state.</param>
    /// <param name="type">The type of the sensor on the virtual joystick to update.</param>
    /// <param name="sensor_timestamp">A 64-bit timestamp in nanoseconds associated with the sensor reading.</param>
    /// <param name="data">The data associated with the sensor reading.</param>
    /// <param name="num_values">The number of values pointed to by data.</param>
    /// <returns>True on success or false on failure; call SDL_GetError() for more information.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_SendJoystickVirtualSensorData(SDL_Joystick* joystick, SDL_SensorType type, ulong sensor_timestamp, float* data, int num_values);

    /// <summary>
    /// Get the properties associated with a joystick.
    /// </summary>
    /// <param name="joystick">The SDL_Joystick obtained from SDL_OpenJoystick().</param>
    /// <returns>A valid property ID on success or 0 on failure; call SDL_GetError() for more information.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL_PropertiesID SDL_GetJoystickProperties(SDL_Joystick* joystick);

    /// <summary>
    /// Get the implementation dependent name of a joystick.
    /// </summary>
    /// <param name="joystick">The SDL_Joystick obtained from SDL_OpenJoystick().</param>
    /// <returns>The name of the selected joystick. If no name can be found, this function returns NULL; call SDL_GetError() for more information.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(Utf8StringMarshaller))]
    public static partial string? SDL_GetJoystickName(SDL_Joystick* joystick);

    /// <summary>
    /// Get the implementation dependent path of a joystick.
    /// </summary>
    /// <param name="joystick">The SDL_Joystick obtained from SDL_OpenJoystick().</param>
    /// <returns>The path of the selected joystick. If no path can be found, this function returns NULL; call SDL_GetError() for more information.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(Utf8StringMarshaller))]
    public static partial string? SDL_GetJoystickPath(SDL_Joystick* joystick);

    /// <summary>
    /// Get the player index of an opened joystick.
    /// For XInput controllers this returns the XInput user index. Many joysticks will not be able to supply this information.
    /// </summary>
    /// <param name="joystick">The SDL_Joystick obtained from SDL_OpenJoystick().</param>
    /// <returns>The player index, or -1 if it's not available.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int SDL_GetJoystickPlayerIndex(SDL_Joystick* joystick);

    /// <summary>
    /// Set the player index of an opened joystick.
    /// </summary>
    /// <param name="joystick">The SDL_Joystick obtained from SDL_OpenJoystick().</param>
    /// <param name="player_index">Player index to assign to this joystick, or -1 to clear the player index and turn off player LEDs.</param>
    /// <returns>True on success or false on failure; call SDL_GetError() for more information.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_SetJoystickPlayerIndex(SDL_Joystick* joystick, int player_index);

    /// <summary>
    /// Get the implementation-dependent GUID for the joystick.
    /// This function requires an open joystick.
    /// </summary>
    /// <param name="joystick">The SDL_Joystick obtained from SDL_OpenJoystick().</param>
    /// <returns>The GUID of the given joystick. If called on an invalid index, this function returns a zero GUID; call SDL_GetError() for more information.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial Guid SDL_GetJoystickGUID(SDL_Joystick* joystick);

    /// <summary>
    /// Get the USB vendor ID of an opened joystick, if available.
    /// If the vendor ID isn't available this function returns 0.
    /// </summary>
    /// <param name="joystick">The SDL_Joystick obtained from SDL_OpenJoystick().</param>
    /// <returns>The USB vendor ID of the selected joystick, or 0 if unavailable.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial ushort SDL_GetJoystickVendor(SDL_Joystick* joystick);

    /// <summary>
    /// Get the USB product ID of an opened joystick, if available.
    /// If the product ID isn't available this function returns 0.
    /// </summary>
    /// <param name="joystick">The SDL_Joystick obtained from SDL_OpenJoystick().</param>
    /// <returns>The USB product ID of the selected joystick, or 0 if unavailable.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial ushort SDL_GetJoystickProduct(SDL_Joystick* joystick);

    /// <summary>
    /// Get the product version of an opened joystick, if available.
    /// If the product version isn't available this function returns 0.
    /// </summary>
    /// <param name="joystick">The SDL_Joystick obtained from SDL_OpenJoystick().</param>
    /// <returns>The product version of the selected joystick, or 0 if unavailable.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial ushort SDL_GetJoystickProductVersion(SDL_Joystick* joystick);

    /// <summary>
    /// Get the firmware version of an opened joystick, if available.
    /// If the firmware version isn't available this function returns 0.
    /// </summary>
    /// <param name="joystick">The SDL_Joystick obtained from SDL_OpenJoystick().</param>
    /// <returns>The firmware version of the selected joystick, or 0 if unavailable.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial ushort SDL_GetJoystickFirmwareVersion(SDL_Joystick* joystick);

    /// <summary>
    /// Get the serial number of an opened joystick, if available.
    /// Returns the serial number of the joystick, or NULL if it is not available.
    /// </summary>
    /// <param name="joystick">The SDL_Joystick obtained from SDL_OpenJoystick().</param>
    /// <returns>The serial number of the selected joystick, or NULL if unavailable.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(Utf8StringMarshaller))]
    public static partial string? SDL_GetJoystickSerial(SDL_Joystick* joystick);

    /// <summary>
    /// Get the type of an opened joystick.
    /// </summary>
    /// <param name="joystick">The SDL_Joystick obtained from SDL_OpenJoystick().</param>
    /// <returns>The SDL_JoystickType of the selected joystick.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL_JoystickType SDL_GetJoystickType(SDL_Joystick* joystick);

    /// <summary>
    /// Get the device information encoded in a SDL_GUID structure.
    /// </summary>
    /// <param name="guid">The SDL_GUID you wish to get info about.</param>
    /// <param name="vendor">A pointer filled in with the device VID, or 0 if not available.</param>
    /// <param name="product">A pointer filled in with the device PID, or 0 if not available.</param>
    /// <param name="version">A pointer filled in with the device version, or 0 if not available.</param>
    /// <param name="crc16">A pointer filled in with a CRC used to distinguish different products with the same VID/PID, or 0 if not available.</param>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SDL_GetJoystickGUIDInfo(Guid guid, ushort* vendor, ushort* product, ushort* version, ushort* crc16);

    /// <summary>
    /// Get the status of a specified joystick.
    /// </summary>
    /// <param name="joystick">The joystick to query.</param>
    /// <returns>True if the joystick has been opened, false if it has not; call SDL_GetError() for more information.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_JoystickConnected(SDL_Joystick* joystick);

    /// <summary>
    /// Get the instance ID of an opened joystick.
    /// </summary>
    /// <param name="joystick">An SDL_Joystick structure containing joystick information.</param>
    /// <returns>The instance ID of the specified joystick on success or 0 on failure; call SDL_GetError() for more information.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL_JoystickID SDL_GetJoystickID(SDL_Joystick* joystick);

    /// <summary>
    /// Get the number of general axis controls on a joystick.
    /// Often, the directional pad on a game controller will either look like 4
    /// separate buttons or a POV hat, and not axes, but all of this is up to the
    /// device and platform.
    /// </summary>
    /// <param name="joystick">An SDL_Joystick structure containing joystick information.</param>
    /// <returns>The number of axis controls/number of axes on success or -1 on failure; call SDL_GetError() for more information.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int SDL_GetNumJoystickAxes(SDL_Joystick* joystick);

    /// <summary>
    /// Get the number of trackballs on a joystick.
    /// Joystick trackballs have only relative motion events associated with them
    /// and their state cannot be polled. Most joysticks do not have trackballs.
    /// </summary>
    /// <param name="joystick">An SDL_Joystick structure containing joystick information.</param>
    /// <returns>The number of trackballs on success or -1 on failure; call SDL_GetError() for more information.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int SDL_GetNumJoystickBalls(SDL_Joystick* joystick);

    /// <summary>
    /// Get the number of POV hats on a joystick.
    /// </summary>
    /// <param name="joystick">An SDL_Joystick structure containing joystick information.</param>
    /// <returns>The number of POV hats on success or -1 on failure; call SDL_GetError() for more information.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int SDL_GetNumJoystickHats(SDL_Joystick* joystick);

    /// <summary>
    /// Get the number of buttons on a joystick.
    /// </summary>
    /// <param name="joystick">An SDL_Joystick structure containing joystick information.</param>
    /// <returns>The number of buttons on success or -1 on failure; call SDL_GetError() for more information.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int SDL_GetNumJoystickButtons(SDL_Joystick* joystick);

    /// <summary>
    /// Set the state of joystick event processing.
    /// If joystick events are disabled, you must call SDL_UpdateJoysticks()
    /// yourself and check the state of the joystick when you want joystick information.
    /// </summary>
    /// <param name="enabled">Whether to process joystick events or not.</param>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SDL_SetJoystickEventsEnabled([MarshalAs(UnmanagedType.U1)] bool enabled);

    /// <summary>
    /// Query the state of joystick event processing.
    /// If joystick events are disabled, you must call SDL_UpdateJoysticks()
    /// yourself and check the state of the joystick when you want joystick information.
    /// </summary>
    /// <returns>True if joystick events are being processed, false otherwise.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_JoystickEventsEnabled();

    /// <summary>
    /// Update the current state of the open joysticks.
    /// This is called automatically by the event loop if any joystick events are enabled.
    /// </summary>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SDL_UpdateJoysticks();

    /// <summary>
    /// Get the current state of an axis control on a joystick.
    /// SDL makes no promises about what part of the joystick any given axis refers to.
    /// The value returned is a signed integer (-32768 to 32767) representing the current position of the axis.
    /// </summary>
    /// <param name="joystick">An SDL_Joystick structure containing joystick information.</param>
    /// <param name="axis">The axis to query; the axis indices start at index 0.</param>
    /// <returns>A 16-bit signed integer representing the current position of the axis or 0 on failure; call SDL_GetError() for more information.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial short SDL_GetJoystickAxis(SDL_Joystick* joystick, int axis);

    /// <summary>
    /// Get the initial state of an axis control on a joystick.
    /// The state is a value ranging from -32768 to 32767. The axis indices start at index 0.
    /// </summary>
    /// <param name="joystick">An SDL_Joystick structure containing joystick information.</param>
    /// <param name="axis">The axis to query; the axis indices start at index 0.</param>
    /// <param name="state">Upon return, the initial value is supplied here.</param>
    /// <returns>True if this axis has any initial value, or false if not.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_GetJoystickAxisInitialState(SDL_Joystick* joystick, int axis, short* state);

    /// <summary>
    /// Get the ball axis change since the last poll.
    /// Trackballs can only return relative motion since the last call to SDL_GetJoystickBall().
    /// Most joysticks do not have trackballs.
    /// </summary>
    /// <param name="joystick">The SDL_Joystick to query.</param>
    /// <param name="ball">The ball index to query; ball indices start at index 0.</param>
    /// <param name="dx">Stores the difference in the x axis position since the last poll.</param>
    /// <param name="dy">Stores the difference in the y axis position since the last poll.</param>
    /// <returns>True on success or false on failure; call SDL_GetError() for more information.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_GetJoystickBall(SDL_Joystick* joystick, int ball, int* dx, int* dy);

    /// <summary>
    /// Get the current state of a POV hat on a joystick.
    /// The returned value will be one of the SDL_HAT_* values.
    /// </summary>
    /// <param name="joystick">An SDL_Joystick structure containing joystick information.</param>
    /// <param name="hat">The hat index to get the state from; indices start at index 0.</param>
    /// <returns>The current hat position.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial byte SDL_GetJoystickHat(SDL_Joystick* joystick, int hat);

    /// <summary>
    /// Get the current state of a button on a joystick.
    /// </summary>
    /// <param name="joystick">An SDL_Joystick structure containing joystick information.</param>
    /// <param name="button">The button index to get the state from; indices start at index 0.</param>
    /// <returns>True if the button is pressed, false otherwise.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_GetJoystickButton(SDL_Joystick* joystick, int button);

    /// <summary>
    /// Start a rumble effect.
    /// Each call to this function cancels any previous rumble effect, and calling
    /// it with 0 intensity stops any rumbling.
    /// </summary>
    /// <param name="joystick">The joystick to vibrate.</param>
    /// <param name="low_frequency_rumble">The intensity of the low frequency (left) rumble motor, from 0 to 0xFFFF.</param>
    /// <param name="high_frequency_rumble">The intensity of the high frequency (right) rumble motor, from 0 to 0xFFFF.</param>
    /// <param name="duration_ms">The duration of the rumble effect, in milliseconds.</param>
    /// <returns>True, or false if rumble isn't supported on this joystick.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_RumbleJoystick(SDL_Joystick* joystick, ushort low_frequency_rumble, ushort high_frequency_rumble, uint duration_ms);

    /// <summary>
    /// Start a rumble effect in the joystick's triggers.
    /// Each call to this function cancels any previous trigger rumble effect, and
    /// calling it with 0 intensity stops any rumbling.
    /// Note that this is rumbling of the triggers and not the game controller as a whole.
    /// This is currently only supported on Xbox One controllers.
    /// </summary>
    /// <param name="joystick">The joystick to vibrate.</param>
    /// <param name="left_rumble">The intensity of the left trigger rumble motor, from 0 to 0xFFFF.</param>
    /// <param name="right_rumble">The intensity of the right trigger rumble motor, from 0 to 0xFFFF.</param>
    /// <param name="duration_ms">The duration of the rumble effect, in milliseconds.</param>
    /// <returns>True on success or false on failure; call SDL_GetError() for more information.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_RumbleJoystickTriggers(SDL_Joystick* joystick, ushort left_rumble, ushort right_rumble, uint duration_ms);

    /// <summary>
    /// Update a joystick's LED color.
    /// An example of a joystick LED is the light on the back of a PlayStation 4's DualShock 4 controller.
    /// For joysticks with a single color LED, the maximum of the RGB values will be used as the LED brightness.
    /// </summary>
    /// <param name="joystick">The joystick to update.</param>
    /// <param name="red">The intensity of the red LED.</param>
    /// <param name="green">The intensity of the green LED.</param>
    /// <param name="blue">The intensity of the blue LED.</param>
    /// <returns>True on success or false on failure; call SDL_GetError() for more information.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_SetJoystickLED(SDL_Joystick* joystick, byte red, byte green, byte blue);

    /// <summary>
    /// Send a joystick specific effect packet.
    /// </summary>
    /// <param name="joystick">The joystick to affect.</param>
    /// <param name="data">The data to send to the joystick.</param>
    /// <param name="size">The size of the data to send to the joystick.</param>
    /// <returns>True on success or false on failure; call SDL_GetError() for more information.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_SendJoystickEffect(SDL_Joystick* joystick, void* data, int size);

    /// <summary>
    /// Close a joystick previously opened with SDL_OpenJoystick().
    /// </summary>
    /// <param name="joystick">The joystick device to close.</param>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SDL_CloseJoystick(SDL_Joystick* joystick);

    /// <summary>
    /// Get the connection state of a joystick.
    /// </summary>
    /// <param name="joystick">The joystick to query.</param>
    /// <returns>The connection state on success or SDL_JOYSTICK_CONNECTION_INVALID on failure; call SDL_GetError() for more information.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL_JoystickConnectionState SDL_GetJoystickConnectionState(SDL_Joystick* joystick);

    /// <summary>
    /// Get the battery state of a joystick.
    /// You should never take a battery status as absolute truth. Batteries
    /// (especially failing batteries) are delicate hardware, and the values
    /// reported here are best estimates based on what that hardware reports.
    /// </summary>
    /// <param name="joystick">The joystick to query.</param>
    /// <param name="percent">A pointer filled in with the percentage of battery life left, between 0 and 100, or NULL to ignore. This will be filled in with -1 if we can't determine a value or there is no battery.</param>
    /// <returns>The current battery state or SDL_POWERSTATE_ERROR on failure; call SDL_GetError() for more information.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL_PowerState SDL_GetJoystickPowerInfo(SDL_Joystick* joystick, int* percent);

    // SDL_joystick_lock is not wrapped - it's an internal symbol for thread safety analysis, not an exported API
}
