using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
// ReSharper disable InconsistentNaming

namespace SdlSharp.Native;

// Deferred: virtual joystick suite (SDL_AttachVirtualJoystick, SDL_DetachVirtualJoystick,
// SDL_IsJoystickVirtual, SDL_SetJoystickVirtualAxis, SDL_SetJoystickVirtualBall,
// SDL_SetJoystickVirtualButton, SDL_SetJoystickVirtualHat, SDL_SetJoystickVirtualTouchpad,
// SDL_SendJoystickVirtualSensorData, SDL_VirtualJoystickDesc + its callbacks — apps that need
// virtual/synthetic joysticks are a niche use case),
// locking (SDL_LockJoysticks, SDL_UnlockJoysticks — thread-safety guards not needed by this
// wrapper's usage model),
// SDL_GetJoystickGUIDInfo (GUID decomposition — SdlGuid exposes the canonical string form instead).

/// <summary>
/// Opaque joystick handle.
/// </summary>
public struct SDL_Joystick;

/// <summary>
/// A unique ID for a joystick.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public readonly record struct SDL_JoystickID(uint Value);

/// <summary>
/// An enum of some common joystick types.
/// </summary>
public enum SDL_JoystickType
{
    SDL_JOYSTICK_TYPE_UNKNOWN,
    SDL_JOYSTICK_TYPE_GAMEPAD,
    SDL_JOYSTICK_TYPE_WHEEL,
    SDL_JOYSTICK_TYPE_ARCADE_STICK,
    SDL_JOYSTICK_TYPE_FLIGHT_STICK,
    SDL_JOYSTICK_TYPE_DANCE_PAD,
    SDL_JOYSTICK_TYPE_GUITAR,
    SDL_JOYSTICK_TYPE_DRUM_KIT,
    SDL_JOYSTICK_TYPE_ARCADE_PAD,
    SDL_JOYSTICK_TYPE_THROTTLE,
    SDL_JOYSTICK_TYPE_COUNT,
}

/// <summary>
/// Possible connection states for a joystick device.
/// </summary>
public enum SDL_JoystickConnectionState
{
    SDL_JOYSTICK_CONNECTION_INVALID = -1,
    SDL_JOYSTICK_CONNECTION_UNKNOWN,
    SDL_JOYSTICK_CONNECTION_WIRED,
    SDL_JOYSTICK_CONNECTION_WIRELESS,
}

/// <summary>
/// Native bindings for SDL_joystick.h.
/// </summary>
[SuppressMessage("Interoperability", "CA1401:P/Invokes should not be visible")]
public static partial class Joystick
{
    public const byte SDL_HAT_CENTERED = 0x00;
    public const byte SDL_HAT_UP = 0x01;
    public const byte SDL_HAT_RIGHT = 0x02;
    public const byte SDL_HAT_DOWN = 0x04;
    public const byte SDL_HAT_LEFT = 0x08;
    public const byte SDL_HAT_RIGHTUP = SDL_HAT_RIGHT | SDL_HAT_UP;
    public const byte SDL_HAT_RIGHTDOWN = SDL_HAT_RIGHT | SDL_HAT_DOWN;
    public const byte SDL_HAT_LEFTUP = SDL_HAT_LEFT | SDL_HAT_UP;
    public const byte SDL_HAT_LEFTDOWN = SDL_HAT_LEFT | SDL_HAT_DOWN;

    public const string SDL_PROP_JOYSTICK_CAP_MONO_LED_BOOLEAN = "SDL.joystick.cap.mono_led";
    public const string SDL_PROP_JOYSTICK_CAP_RGB_LED_BOOLEAN = "SDL.joystick.cap.rgb_led";
    public const string SDL_PROP_JOYSTICK_CAP_PLAYER_LED_BOOLEAN = "SDL.joystick.cap.player_led";
    public const string SDL_PROP_JOYSTICK_CAP_RUMBLE_BOOLEAN = "SDL.joystick.cap.rumble";
    public const string SDL_PROP_JOYSTICK_CAP_TRIGGER_RUMBLE_BOOLEAN = "SDL.joystick.cap.trigger_rumble";

    public const short SDL_JOYSTICK_AXIS_MIN = -32768;
    public const short SDL_JOYSTICK_AXIS_MAX = 32767;

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetJoysticks")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial SDL_JoystickID* SDL_GetJoysticks(int* count);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetJoystickNameForID")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial byte* SDL_GetJoystickNameForID(SDL_JoystickID instance_id);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetJoystickTypeForID")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL_JoystickType SDL_GetJoystickTypeForID(SDL_JoystickID instance_id);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_OpenJoystick")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial SDL_Joystick* SDL_OpenJoystick(SDL_JoystickID instance_id);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetJoystickFromID")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial SDL_Joystick* SDL_GetJoystickFromID(SDL_JoystickID instance_id);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetJoystickName")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial byte* SDL_GetJoystickName(SDL_Joystick* joystick);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetJoystickType")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial SDL_JoystickType SDL_GetJoystickType(SDL_Joystick* joystick);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetJoystickID")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial SDL_JoystickID SDL_GetJoystickID(SDL_Joystick* joystick);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetNumJoystickAxes")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial int SDL_GetNumJoystickAxes(SDL_Joystick* joystick);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetNumJoystickBalls")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial int SDL_GetNumJoystickBalls(SDL_Joystick* joystick);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetNumJoystickHats")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial int SDL_GetNumJoystickHats(SDL_Joystick* joystick);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetNumJoystickButtons")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial int SDL_GetNumJoystickButtons(SDL_Joystick* joystick);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetJoystickAxis")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial short SDL_GetJoystickAxis(SDL_Joystick* joystick, int axis);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetJoystickAxisInitialState")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_GetJoystickAxisInitialState(SDL_Joystick* joystick, int axis, short* state);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetJoystickHat")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial byte SDL_GetJoystickHat(SDL_Joystick* joystick, int hat);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetJoystickButton")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_GetJoystickButton(SDL_Joystick* joystick, int button);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetJoystickConnectionState")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial SDL_JoystickConnectionState SDL_GetJoystickConnectionState(SDL_Joystick* joystick);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_CloseJoystick")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial void SDL_CloseJoystick(SDL_Joystick* joystick);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_UpdateJoysticks")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SDL_UpdateJoysticks();

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_HasJoystick")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_HasJoystick();

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_JoystickConnected")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_JoystickConnected(SDL_Joystick* joystick);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetJoystickGUID")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial SDL_GUID SDL_GetJoystickGUID(SDL_Joystick* joystick);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetJoystickGUIDForID")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL_GUID SDL_GetJoystickGUIDForID(SDL_JoystickID instance_id);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetJoystickVendor")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial ushort SDL_GetJoystickVendor(SDL_Joystick* joystick);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetJoystickVendorForID")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial ushort SDL_GetJoystickVendorForID(SDL_JoystickID instance_id);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetJoystickProduct")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial ushort SDL_GetJoystickProduct(SDL_Joystick* joystick);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetJoystickProductForID")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial ushort SDL_GetJoystickProductForID(SDL_JoystickID instance_id);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetJoystickProductVersion")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial ushort SDL_GetJoystickProductVersion(SDL_Joystick* joystick);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetJoystickProductVersionForID")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial ushort SDL_GetJoystickProductVersionForID(SDL_JoystickID instance_id);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetJoystickFirmwareVersion")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial ushort SDL_GetJoystickFirmwareVersion(SDL_Joystick* joystick);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetJoystickSerial")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial byte* SDL_GetJoystickSerial(SDL_Joystick* joystick);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetJoystickPath")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial byte* SDL_GetJoystickPath(SDL_Joystick* joystick);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetJoystickPathForID")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial byte* SDL_GetJoystickPathForID(SDL_JoystickID instance_id);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetJoystickProperties")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial SDL_PropertiesID SDL_GetJoystickProperties(SDL_Joystick* joystick);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetJoystickPlayerIndex")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial int SDL_GetJoystickPlayerIndex(SDL_Joystick* joystick);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_SetJoystickPlayerIndex")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_SetJoystickPlayerIndex(SDL_Joystick* joystick, int player_index);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetJoystickPlayerIndexForID")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int SDL_GetJoystickPlayerIndexForID(SDL_JoystickID instance_id);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetJoystickFromPlayerIndex")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial SDL_Joystick* SDL_GetJoystickFromPlayerIndex(int player_index);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetJoystickPowerInfo")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial SDL_PowerState SDL_GetJoystickPowerInfo(SDL_Joystick* joystick, int* percent);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_RumbleJoystick")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_RumbleJoystick(SDL_Joystick* joystick, ushort low_frequency_rumble, ushort high_frequency_rumble, uint duration_ms);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_RumbleJoystickTriggers")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_RumbleJoystickTriggers(SDL_Joystick* joystick, ushort left_rumble, ushort right_rumble, uint duration_ms);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_SetJoystickLED")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_SetJoystickLED(SDL_Joystick* joystick, byte red, byte green, byte blue);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_SendJoystickEffect")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_SendJoystickEffect(SDL_Joystick* joystick, void* data, int size);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_SetJoystickEventsEnabled")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SDL_SetJoystickEventsEnabled([MarshalAs(UnmanagedType.U1)] bool enabled);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_JoystickEventsEnabled")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_JoystickEventsEnabled();

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetJoystickBall")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_GetJoystickBall(SDL_Joystick* joystick, int ball, int* dx, int* dy);
}
