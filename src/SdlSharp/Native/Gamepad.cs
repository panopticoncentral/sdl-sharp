using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
// ReSharper disable InconsistentNaming

namespace SdlSharp.Native;

// Deferred: SDL_AddGamepadMappingsFromIO (SDL_IOStream-based mapping load — project policy
// favors the .NET-facing SDL_AddGamepadMappingsFromFile path over stream interop),
// SDL_GetGamepadMappings (niche full-database enumerator; individual lookups are covered by
// SDL_GetGamepadMapping/SDL_GetGamepadMappingForGUID/SDL_GetGamepadMappingForID),
// SDL_GamepadBinding + SDL_GamepadBindingType + SDL_GetGamepadBindings (low-level binding
// introspection; niche — mapping strings are the supported way to inspect/configure bindings),
// SDL_GetGamepadAppleSFSymbolsNameForButton/Axis (Apple-specific SF Symbols glyph names, platform
// niche).

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
    // The gamepad CAP properties are aliases of the joystick CAP properties (SDL_gamepad.h
    // lines 813-817: #define SDL_PROP_GAMEPAD_CAP_*_BOOLEAN SDL_PROP_JOYSTICK_CAP_*_BOOLEAN).
    public const string SDL_PROP_GAMEPAD_CAP_MONO_LED_BOOLEAN = Joystick.SDL_PROP_JOYSTICK_CAP_MONO_LED_BOOLEAN;
    public const string SDL_PROP_GAMEPAD_CAP_RGB_LED_BOOLEAN = Joystick.SDL_PROP_JOYSTICK_CAP_RGB_LED_BOOLEAN;
    public const string SDL_PROP_GAMEPAD_CAP_PLAYER_LED_BOOLEAN = Joystick.SDL_PROP_JOYSTICK_CAP_PLAYER_LED_BOOLEAN;
    public const string SDL_PROP_GAMEPAD_CAP_RUMBLE_BOOLEAN = Joystick.SDL_PROP_JOYSTICK_CAP_RUMBLE_BOOLEAN;
    public const string SDL_PROP_GAMEPAD_CAP_TRIGGER_RUMBLE_BOOLEAN = Joystick.SDL_PROP_JOYSTICK_CAP_TRIGGER_RUMBLE_BOOLEAN;

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

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_AddGamepadMapping")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int SDL_AddGamepadMapping(ReadOnlySpan<byte> mapping);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_AddGamepadMappingsFromFile")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int SDL_AddGamepadMappingsFromFile(ReadOnlySpan<byte> file);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_ReloadGamepadMappings")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_ReloadGamepadMappings();

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetGamepadMappingForGUID")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial byte* SDL_GetGamepadMappingForGUID(SDL_GUID guid); // caller frees via SDL_free

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetGamepadMapping")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial byte* SDL_GetGamepadMapping(SDL_Gamepad* gamepad); // caller frees via SDL_free

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_SetGamepadMapping")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_SetGamepadMapping(SDL_JoystickID instance_id, ReadOnlySpan<byte> mapping);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetGamepadMappingForID")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial byte* SDL_GetGamepadMappingForID(SDL_JoystickID instance_id); // caller frees via SDL_free

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_HasGamepad")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_HasGamepad();

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetGamepadPathForID")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial byte* SDL_GetGamepadPathForID(SDL_JoystickID instance_id);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetGamepadPlayerIndexForID")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int SDL_GetGamepadPlayerIndexForID(SDL_JoystickID instance_id);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetGamepadGUIDForID")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL_GUID SDL_GetGamepadGUIDForID(SDL_JoystickID instance_id);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetGamepadVendorForID")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial ushort SDL_GetGamepadVendorForID(SDL_JoystickID instance_id);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetGamepadProductForID")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial ushort SDL_GetGamepadProductForID(SDL_JoystickID instance_id);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetGamepadProductVersionForID")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial ushort SDL_GetGamepadProductVersionForID(SDL_JoystickID instance_id);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetRealGamepadTypeForID")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL_GamepadType SDL_GetRealGamepadTypeForID(SDL_JoystickID instance_id);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetGamepadFromPlayerIndex")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial SDL_Gamepad* SDL_GetGamepadFromPlayerIndex(int player_index);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetGamepadProperties")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial SDL_PropertiesID SDL_GetGamepadProperties(SDL_Gamepad* gamepad);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetGamepadID")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial SDL_JoystickID SDL_GetGamepadID(SDL_Gamepad* gamepad);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetGamepadPath")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial byte* SDL_GetGamepadPath(SDL_Gamepad* gamepad);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetRealGamepadType")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial SDL_GamepadType SDL_GetRealGamepadType(SDL_Gamepad* gamepad);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetGamepadPlayerIndex")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial int SDL_GetGamepadPlayerIndex(SDL_Gamepad* gamepad);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_SetGamepadPlayerIndex")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_SetGamepadPlayerIndex(SDL_Gamepad* gamepad, int player_index);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetGamepadVendor")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial ushort SDL_GetGamepadVendor(SDL_Gamepad* gamepad);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetGamepadProduct")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial ushort SDL_GetGamepadProduct(SDL_Gamepad* gamepad);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetGamepadProductVersion")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial ushort SDL_GetGamepadProductVersion(SDL_Gamepad* gamepad);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetGamepadFirmwareVersion")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial ushort SDL_GetGamepadFirmwareVersion(SDL_Gamepad* gamepad);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetGamepadSerial")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial byte* SDL_GetGamepadSerial(SDL_Gamepad* gamepad);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetGamepadSteamHandle")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial ulong SDL_GetGamepadSteamHandle(SDL_Gamepad* gamepad);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetGamepadPowerInfo")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial SDL_PowerState SDL_GetGamepadPowerInfo(SDL_Gamepad* gamepad, int* percent);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GamepadConnected")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_GamepadConnected(SDL_Gamepad* gamepad);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_SetGamepadEventsEnabled")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SDL_SetGamepadEventsEnabled([MarshalAs(UnmanagedType.U1)] bool enabled);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GamepadEventsEnabled")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_GamepadEventsEnabled();

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetGamepadTypeFromString")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL_GamepadType SDL_GetGamepadTypeFromString(ReadOnlySpan<byte> str);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetGamepadStringForType")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial byte* SDL_GetGamepadStringForType(SDL_GamepadType type); // SDL-owned, no free

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetGamepadAxisFromString")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL_GamepadAxis SDL_GetGamepadAxisFromString(ReadOnlySpan<byte> str);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetGamepadStringForAxis")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial byte* SDL_GetGamepadStringForAxis(SDL_GamepadAxis axis); // SDL-owned, no free

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GamepadHasAxis")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_GamepadHasAxis(SDL_Gamepad* gamepad, SDL_GamepadAxis axis);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetGamepadButtonFromString")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL_GamepadButton SDL_GetGamepadButtonFromString(ReadOnlySpan<byte> str);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetGamepadStringForButton")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial byte* SDL_GetGamepadStringForButton(SDL_GamepadButton button); // SDL-owned, no free

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GamepadHasButton")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_GamepadHasButton(SDL_Gamepad* gamepad, SDL_GamepadButton button);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetGamepadButtonLabelForType")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL_GamepadButtonLabel SDL_GetGamepadButtonLabelForType(SDL_GamepadType type, SDL_GamepadButton button);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetNumGamepadTouchpads")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial int SDL_GetNumGamepadTouchpads(SDL_Gamepad* gamepad);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetNumGamepadTouchpadFingers")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial int SDL_GetNumGamepadTouchpadFingers(SDL_Gamepad* gamepad, int touchpad);

    // down is a C `bool *` out-param (1-byte stdbool ABI). No existing bool* precedent in
    // Native/ to follow, so it is bound as byte*, matching the plan's explicit guidance; the
    // managed layer converts (down = d != 0).
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetGamepadTouchpadFinger")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_GetGamepadTouchpadFinger(SDL_Gamepad* gamepad, int touchpad, int finger, byte* down, float* x, float* y, float* pressure);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GamepadHasSensor")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_GamepadHasSensor(SDL_Gamepad* gamepad, SDL_SensorType type);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_SetGamepadSensorEnabled")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_SetGamepadSensorEnabled(SDL_Gamepad* gamepad, SDL_SensorType type, [MarshalAs(UnmanagedType.U1)] bool enabled);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GamepadSensorEnabled")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_GamepadSensorEnabled(SDL_Gamepad* gamepad, SDL_SensorType type);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetGamepadSensorDataRate")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial float SDL_GetGamepadSensorDataRate(SDL_Gamepad* gamepad, SDL_SensorType type);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetGamepadSensorData")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_GetGamepadSensorData(SDL_Gamepad* gamepad, SDL_SensorType type, float* data, int num_values);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_RumbleGamepad")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_RumbleGamepad(SDL_Gamepad* gamepad, ushort low_frequency_rumble, ushort high_frequency_rumble, uint duration_ms);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_RumbleGamepadTriggers")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_RumbleGamepadTriggers(SDL_Gamepad* gamepad, ushort left_rumble, ushort right_rumble, uint duration_ms);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_SetGamepadLED")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_SetGamepadLED(SDL_Gamepad* gamepad, byte red, byte green, byte blue);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_SendGamepadEffect")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_SendGamepadEffect(SDL_Gamepad* gamepad, void* data, int size);
}
