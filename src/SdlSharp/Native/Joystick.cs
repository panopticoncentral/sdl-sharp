using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
// ReSharper disable InconsistentNaming

namespace SdlSharp.Native;

// Deferred: virtual joystick (SDL_AttachVirtualJoystick, SDL_DetachVirtualJoystick, SDL_SetJoystickVirtual*),
// rumble (SDL_RumbleJoystick, SDL_RumbleJoystickTriggers),
// LED (SDL_SetJoystickLED), player index (SDL_SetJoystickPlayerIndex),
// sensors (SDL_SetJoystickSensorEnabled, SDL_GetJoystickSensorData),
// GUID (SDL_GetJoystickGUIDForID, SDL_GetJoystickGUID — needs SDL_GUID struct),
// power (SDL_GetJoystickPowerInfo — needs SDL_PowerState from Events.cs),
// properties (SDL_GetJoystickProperties, property constants),
// locking (SDL_LockJoysticks, SDL_UnlockJoysticks).

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
}
