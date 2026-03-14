using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
// ReSharper disable InconsistentNaming

namespace SdlSharp.Native;

// Deferred: mapping management (SDL_AddGamepadMapping*, SDL_GetGamepadMapping*),
// binding (SDL_GamepadBinding, SDL_GetGamepadBindings),
// rumble (SDL_RumbleGamepad, SDL_RumbleGamepadTriggers),
// LED (SDL_SetGamepadLED), player index (SDL_SetGamepadPlayerIndex),
// sensors (SDL_GamepadHasSensor, SDL_SetGamepadSensorEnabled, SDL_GetGamepadSensorData),
// touchpad detail (SDL_GetNumGamepadTouchpads, SDL_GetNumGamepadTouchpadFingers, SDL_GetGamepadTouchpadFinger),
// Steam handle (SDL_GetGamepadSteamHandle),
// power (SDL_GetGamepadPowerInfo),
// properties (SDL_GetGamepadProperties, property constants),
// string for button/axis (SDL_GetGamepadStringForButton, SDL_GetGamepadStringForAxis).

/// <summary>
/// Opaque gamepad handle.
/// </summary>
public struct SDL_Gamepad;

/// <summary>
/// Standard gamepad types.
/// </summary>
public enum SDL_GamepadType
{
    SDL_GAMEPAD_TYPE_UNKNOWN = 0,
    SDL_GAMEPAD_TYPE_STANDARD,
    SDL_GAMEPAD_TYPE_XBOX360,
    SDL_GAMEPAD_TYPE_XBOXONE,
    SDL_GAMEPAD_TYPE_PS3,
    SDL_GAMEPAD_TYPE_PS4,
    SDL_GAMEPAD_TYPE_PS5,
    SDL_GAMEPAD_TYPE_NINTENDO_SWITCH_PRO,
    SDL_GAMEPAD_TYPE_NINTENDO_SWITCH_JOYCON_LEFT,
    SDL_GAMEPAD_TYPE_NINTENDO_SWITCH_JOYCON_RIGHT,
    SDL_GAMEPAD_TYPE_NINTENDO_SWITCH_JOYCON_PAIR,
    SDL_GAMEPAD_TYPE_GAMECUBE,
    SDL_GAMEPAD_TYPE_COUNT,
}

/// <summary>
/// The list of buttons available on a gamepad.
/// </summary>
public enum SDL_GamepadButton
{
    SDL_GAMEPAD_BUTTON_INVALID = -1,
    SDL_GAMEPAD_BUTTON_SOUTH,
    SDL_GAMEPAD_BUTTON_EAST,
    SDL_GAMEPAD_BUTTON_WEST,
    SDL_GAMEPAD_BUTTON_NORTH,
    SDL_GAMEPAD_BUTTON_BACK,
    SDL_GAMEPAD_BUTTON_GUIDE,
    SDL_GAMEPAD_BUTTON_START,
    SDL_GAMEPAD_BUTTON_LEFT_STICK,
    SDL_GAMEPAD_BUTTON_RIGHT_STICK,
    SDL_GAMEPAD_BUTTON_LEFT_SHOULDER,
    SDL_GAMEPAD_BUTTON_RIGHT_SHOULDER,
    SDL_GAMEPAD_BUTTON_DPAD_UP,
    SDL_GAMEPAD_BUTTON_DPAD_DOWN,
    SDL_GAMEPAD_BUTTON_DPAD_LEFT,
    SDL_GAMEPAD_BUTTON_DPAD_RIGHT,
    SDL_GAMEPAD_BUTTON_MISC1,
    SDL_GAMEPAD_BUTTON_RIGHT_PADDLE1,
    SDL_GAMEPAD_BUTTON_LEFT_PADDLE1,
    SDL_GAMEPAD_BUTTON_RIGHT_PADDLE2,
    SDL_GAMEPAD_BUTTON_LEFT_PADDLE2,
    SDL_GAMEPAD_BUTTON_TOUCHPAD,
    SDL_GAMEPAD_BUTTON_MISC2,
    SDL_GAMEPAD_BUTTON_MISC3,
    SDL_GAMEPAD_BUTTON_MISC4,
    SDL_GAMEPAD_BUTTON_MISC5,
    SDL_GAMEPAD_BUTTON_MISC6,
    SDL_GAMEPAD_BUTTON_COUNT,
}

/// <summary>
/// The set of gamepad button labels.
/// </summary>
public enum SDL_GamepadButtonLabel
{
    SDL_GAMEPAD_BUTTON_LABEL_UNKNOWN,
    SDL_GAMEPAD_BUTTON_LABEL_A,
    SDL_GAMEPAD_BUTTON_LABEL_B,
    SDL_GAMEPAD_BUTTON_LABEL_X,
    SDL_GAMEPAD_BUTTON_LABEL_Y,
    SDL_GAMEPAD_BUTTON_LABEL_CROSS,
    SDL_GAMEPAD_BUTTON_LABEL_CIRCLE,
    SDL_GAMEPAD_BUTTON_LABEL_SQUARE,
    SDL_GAMEPAD_BUTTON_LABEL_TRIANGLE,
}

/// <summary>
/// The list of axes available on a gamepad.
/// </summary>
public enum SDL_GamepadAxis
{
    SDL_GAMEPAD_AXIS_INVALID = -1,
    SDL_GAMEPAD_AXIS_LEFTX,
    SDL_GAMEPAD_AXIS_LEFTY,
    SDL_GAMEPAD_AXIS_RIGHTX,
    SDL_GAMEPAD_AXIS_RIGHTY,
    SDL_GAMEPAD_AXIS_LEFT_TRIGGER,
    SDL_GAMEPAD_AXIS_RIGHT_TRIGGER,
    SDL_GAMEPAD_AXIS_COUNT,
}

/// <summary>
/// Native bindings for SDL_gamepad.h.
/// </summary>
[SuppressMessage("Interoperability", "CA1401:P/Invokes should not be visible")]
public static partial class Gamepad
{
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetGamepads")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial SDL_JoystickID* SDL_GetGamepads(int* count);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_IsGamepad")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_IsGamepad(SDL_JoystickID instance_id);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetGamepadNameForID")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial byte* SDL_GetGamepadNameForID(SDL_JoystickID instance_id);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetGamepadTypeForID")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL_GamepadType SDL_GetGamepadTypeForID(SDL_JoystickID instance_id);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_OpenGamepad")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial SDL_Gamepad* SDL_OpenGamepad(SDL_JoystickID instance_id);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetGamepadFromID")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial SDL_Gamepad* SDL_GetGamepadFromID(SDL_JoystickID instance_id);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetGamepadName")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial byte* SDL_GetGamepadName(SDL_Gamepad* gamepad);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetGamepadType")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial SDL_GamepadType SDL_GetGamepadType(SDL_Gamepad* gamepad);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetGamepadJoystick")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial SDL_Joystick* SDL_GetGamepadJoystick(SDL_Gamepad* gamepad);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetGamepadAxis")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial short SDL_GetGamepadAxis(SDL_Gamepad* gamepad, SDL_GamepadAxis axis);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetGamepadButton")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_GetGamepadButton(SDL_Gamepad* gamepad, SDL_GamepadButton button);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetGamepadButtonLabel")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial SDL_GamepadButtonLabel SDL_GetGamepadButtonLabel(SDL_Gamepad* gamepad, SDL_GamepadButton button);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetGamepadConnectionState")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial SDL_JoystickConnectionState SDL_GetGamepadConnectionState(SDL_Gamepad* gamepad);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_CloseGamepad")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial void SDL_CloseGamepad(SDL_Gamepad* gamepad);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_UpdateGamepads")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SDL_UpdateGamepads();
}
