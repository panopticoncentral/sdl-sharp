using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

using static Sdl3Sharp.Native.Common;
using static Sdl3Sharp.Native.IOStream;
using static Sdl3Sharp.Native.Joystick;
using static Sdl3Sharp.Native.Power;
using static Sdl3Sharp.Native.Properties;
using static Sdl3Sharp.Native.Sensor;

// We are intentionally exposing the P/Invoke calls so people can do low-level calls if needed
#pragma warning disable CA1401 // P/Invokes should not be visible

namespace Sdl3Sharp.Native;

/// <summary>
/// P/Invoke bindings for SDL_gamepad.h - Gamepad support.
/// The gamepad API provides a higher-level interface than the joystick API,
/// where button and axis positions are well-defined (like a standard console controller).
/// </summary>
public static unsafe partial class Gamepad
{
    /// <summary>
    /// The opaque structure used to identify an SDL gamepad.
    /// </summary>
    public struct SDL_Gamepad
    {
    }

    /// <summary>
    /// Standard gamepad types.
    /// This type does not necessarily map to first-party controllers from
    /// Microsoft/Sony/Nintendo; in many cases, third-party controllers can report
    /// as these, either because they were designed for a specific console, or they
    /// simply most closely match that console's controllers.
    /// </summary>
    public enum SDL_GamepadType
    {
        /// <summary>Unknown gamepad type.</summary>
        SDL_GAMEPAD_TYPE_UNKNOWN = 0,
        /// <summary>Standard gamepad.</summary>
        SDL_GAMEPAD_TYPE_STANDARD,
        /// <summary>Xbox 360 controller.</summary>
        SDL_GAMEPAD_TYPE_XBOX360,
        /// <summary>Xbox One controller.</summary>
        SDL_GAMEPAD_TYPE_XBOXONE,
        /// <summary>PlayStation 3 controller.</summary>
        SDL_GAMEPAD_TYPE_PS3,
        /// <summary>PlayStation 4 controller.</summary>
        SDL_GAMEPAD_TYPE_PS4,
        /// <summary>PlayStation 5 controller.</summary>
        SDL_GAMEPAD_TYPE_PS5,
        /// <summary>Nintendo Switch Pro controller.</summary>
        SDL_GAMEPAD_TYPE_NINTENDO_SWITCH_PRO,
        /// <summary>Nintendo Switch Joy-Con (left).</summary>
        SDL_GAMEPAD_TYPE_NINTENDO_SWITCH_JOYCON_LEFT,
        /// <summary>Nintendo Switch Joy-Con (right).</summary>
        SDL_GAMEPAD_TYPE_NINTENDO_SWITCH_JOYCON_RIGHT,
        /// <summary>Nintendo Switch Joy-Con pair.</summary>
        SDL_GAMEPAD_TYPE_NINTENDO_SWITCH_JOYCON_PAIR,
        /// <summary>Number of gamepad types.</summary>
        SDL_GAMEPAD_TYPE_COUNT
    }

    /// <summary>
    /// The list of buttons available on a gamepad.
    /// For controllers that use a diamond pattern for the face buttons, the
    /// south/east/west/north buttons correspond to the locations in the diamond pattern.
    /// </summary>
    public enum SDL_GamepadButton
    {
        /// <summary>Invalid button.</summary>
        SDL_GAMEPAD_BUTTON_INVALID = -1,
        /// <summary>Bottom face button (e.g. Xbox A button).</summary>
        SDL_GAMEPAD_BUTTON_SOUTH,
        /// <summary>Right face button (e.g. Xbox B button).</summary>
        SDL_GAMEPAD_BUTTON_EAST,
        /// <summary>Left face button (e.g. Xbox X button).</summary>
        SDL_GAMEPAD_BUTTON_WEST,
        /// <summary>Top face button (e.g. Xbox Y button).</summary>
        SDL_GAMEPAD_BUTTON_NORTH,
        /// <summary>Back/Select button.</summary>
        SDL_GAMEPAD_BUTTON_BACK,
        /// <summary>Guide/Home button.</summary>
        SDL_GAMEPAD_BUTTON_GUIDE,
        /// <summary>Start button.</summary>
        SDL_GAMEPAD_BUTTON_START,
        /// <summary>Left stick button (L3).</summary>
        SDL_GAMEPAD_BUTTON_LEFT_STICK,
        /// <summary>Right stick button (R3).</summary>
        SDL_GAMEPAD_BUTTON_RIGHT_STICK,
        /// <summary>Left shoulder button (L1/LB).</summary>
        SDL_GAMEPAD_BUTTON_LEFT_SHOULDER,
        /// <summary>Right shoulder button (R1/RB).</summary>
        SDL_GAMEPAD_BUTTON_RIGHT_SHOULDER,
        /// <summary>D-pad up.</summary>
        SDL_GAMEPAD_BUTTON_DPAD_UP,
        /// <summary>D-pad down.</summary>
        SDL_GAMEPAD_BUTTON_DPAD_DOWN,
        /// <summary>D-pad left.</summary>
        SDL_GAMEPAD_BUTTON_DPAD_LEFT,
        /// <summary>D-pad right.</summary>
        SDL_GAMEPAD_BUTTON_DPAD_RIGHT,
        /// <summary>Additional button (e.g. Xbox Series X share button, PS5 microphone button).</summary>
        SDL_GAMEPAD_BUTTON_MISC1,
        /// <summary>Upper or primary paddle, under your right hand (e.g. Xbox Elite paddle P1).</summary>
        SDL_GAMEPAD_BUTTON_RIGHT_PADDLE1,
        /// <summary>Upper or primary paddle, under your left hand (e.g. Xbox Elite paddle P3).</summary>
        SDL_GAMEPAD_BUTTON_LEFT_PADDLE1,
        /// <summary>Lower or secondary paddle, under your right hand (e.g. Xbox Elite paddle P2).</summary>
        SDL_GAMEPAD_BUTTON_RIGHT_PADDLE2,
        /// <summary>Lower or secondary paddle, under your left hand (e.g. Xbox Elite paddle P4).</summary>
        SDL_GAMEPAD_BUTTON_LEFT_PADDLE2,
        /// <summary>PS4/PS5 touchpad button.</summary>
        SDL_GAMEPAD_BUTTON_TOUCHPAD,
        /// <summary>Additional button 2.</summary>
        SDL_GAMEPAD_BUTTON_MISC2,
        /// <summary>Additional button 3.</summary>
        SDL_GAMEPAD_BUTTON_MISC3,
        /// <summary>Additional button 4.</summary>
        SDL_GAMEPAD_BUTTON_MISC4,
        /// <summary>Additional button 5.</summary>
        SDL_GAMEPAD_BUTTON_MISC5,
        /// <summary>Additional button 6.</summary>
        SDL_GAMEPAD_BUTTON_MISC6,
        /// <summary>Number of gamepad buttons.</summary>
        SDL_GAMEPAD_BUTTON_COUNT
    }

    /// <summary>
    /// The set of gamepad button labels.
    /// This isn't a complete set, just the face buttons to make it easy to show button prompts.
    /// </summary>
    public enum SDL_GamepadButtonLabel
    {
        /// <summary>Unknown button label.</summary>
        SDL_GAMEPAD_BUTTON_LABEL_UNKNOWN,
        /// <summary>A button label.</summary>
        SDL_GAMEPAD_BUTTON_LABEL_A,
        /// <summary>B button label.</summary>
        SDL_GAMEPAD_BUTTON_LABEL_B,
        /// <summary>X button label.</summary>
        SDL_GAMEPAD_BUTTON_LABEL_X,
        /// <summary>Y button label.</summary>
        SDL_GAMEPAD_BUTTON_LABEL_Y,
        /// <summary>Cross button label (PlayStation).</summary>
        SDL_GAMEPAD_BUTTON_LABEL_CROSS,
        /// <summary>Circle button label (PlayStation).</summary>
        SDL_GAMEPAD_BUTTON_LABEL_CIRCLE,
        /// <summary>Square button label (PlayStation).</summary>
        SDL_GAMEPAD_BUTTON_LABEL_SQUARE,
        /// <summary>Triangle button label (PlayStation).</summary>
        SDL_GAMEPAD_BUTTON_LABEL_TRIANGLE
    }

    /// <summary>
    /// The list of axes available on a gamepad.
    /// Thumbstick axis values range from SDL_JOYSTICK_AXIS_MIN to SDL_JOYSTICK_AXIS_MAX.
    /// Trigger axis values range from 0 (released) to SDL_JOYSTICK_AXIS_MAX (fully pressed).
    /// </summary>
    public enum SDL_GamepadAxis
    {
        /// <summary>Invalid axis.</summary>
        SDL_GAMEPAD_AXIS_INVALID = -1,
        /// <summary>Left stick X axis.</summary>
        SDL_GAMEPAD_AXIS_LEFTX,
        /// <summary>Left stick Y axis.</summary>
        SDL_GAMEPAD_AXIS_LEFTY,
        /// <summary>Right stick X axis.</summary>
        SDL_GAMEPAD_AXIS_RIGHTX,
        /// <summary>Right stick Y axis.</summary>
        SDL_GAMEPAD_AXIS_RIGHTY,
        /// <summary>Left trigger axis.</summary>
        SDL_GAMEPAD_AXIS_LEFT_TRIGGER,
        /// <summary>Right trigger axis.</summary>
        SDL_GAMEPAD_AXIS_RIGHT_TRIGGER,
        /// <summary>Number of gamepad axes.</summary>
        SDL_GAMEPAD_AXIS_COUNT
    }

    /// <summary>
    /// Types of gamepad control bindings.
    /// A gamepad is a collection of bindings that map arbitrary joystick buttons,
    /// axes and hat switches to specific positions on a generic console-style gamepad.
    /// </summary>
    public enum SDL_GamepadBindingType
    {
        /// <summary>No binding.</summary>
        SDL_GAMEPAD_BINDTYPE_NONE = 0,
        /// <summary>Button binding.</summary>
        SDL_GAMEPAD_BINDTYPE_BUTTON,
        /// <summary>Axis binding.</summary>
        SDL_GAMEPAD_BINDTYPE_AXIS,
        /// <summary>Hat binding.</summary>
        SDL_GAMEPAD_BINDTYPE_HAT
    }

    /// <summary>
    /// Axis binding input information.
    /// </summary>
    public struct SDL_GamepadBindingInputAxis
    {
        /// <summary>The axis index.</summary>
        public int axis;
        /// <summary>The minimum axis value.</summary>
        public int axis_min;
        /// <summary>The maximum axis value.</summary>
        public int axis_max;
    }

    /// <summary>
    /// Hat binding input information.
    /// </summary>
    public struct SDL_GamepadBindingInputHat
    {
        /// <summary>The hat index.</summary>
        public int hat;
        /// <summary>The hat mask.</summary>
        public int hat_mask;
    }

    /// <summary>
    /// Union of input binding types.
    /// </summary>
    [StructLayout(LayoutKind.Explicit)]
    public struct SDL_GamepadBindingInput
    {
        /// <summary>Button index for button bindings.</summary>
        [FieldOffset(0)]
        public int button;
        /// <summary>Axis information for axis bindings.</summary>
        [FieldOffset(0)]
        public SDL_GamepadBindingInputAxis axis;
        /// <summary>Hat information for hat bindings.</summary>
        [FieldOffset(0)]
        public SDL_GamepadBindingInputHat hat;
    }

    /// <summary>
    /// Axis binding output information.
    /// </summary>
    public struct SDL_GamepadBindingOutputAxis
    {
        /// <summary>The gamepad axis.</summary>
        public SDL_GamepadAxis axis;
        /// <summary>The minimum axis value.</summary>
        public int axis_min;
        /// <summary>The maximum axis value.</summary>
        public int axis_max;
    }

    /// <summary>
    /// Union of output binding types.
    /// </summary>
    [StructLayout(LayoutKind.Explicit)]
    public struct SDL_GamepadBindingOutput
    {
        /// <summary>Button for button bindings.</summary>
        [FieldOffset(0)]
        public SDL_GamepadButton button;
        /// <summary>Axis information for axis bindings.</summary>
        [FieldOffset(0)]
        public SDL_GamepadBindingOutputAxis axis;
    }

    /// <summary>
    /// A mapping between one joystick input to a gamepad control.
    /// A gamepad has a collection of several bindings, to say, for example, when
    /// joystick button number 5 is pressed, that should be treated like the
    /// gamepad's "start" button.
    /// </summary>
    public struct SDL_GamepadBinding
    {
        /// <summary>The type of input binding.</summary>
        public SDL_GamepadBindingType input_type;
        /// <summary>The input binding data.</summary>
        public SDL_GamepadBindingInput input;
        /// <summary>The type of output binding.</summary>
        public SDL_GamepadBindingType output_type;
        /// <summary>The output binding data.</summary>
        public SDL_GamepadBindingOutput output;
    }

    /// <summary>Property name indicating whether this gamepad has an LED that has adjustable brightness.</summary>
    public const string SDL_PROP_GAMEPAD_CAP_MONO_LED_BOOLEAN = "SDL.joystick.cap.mono_led";
    /// <summary>Property name indicating whether this gamepad has an LED that has adjustable color.</summary>
    public const string SDL_PROP_GAMEPAD_CAP_RGB_LED_BOOLEAN = "SDL.joystick.cap.rgb_led";
    /// <summary>Property name indicating whether this gamepad has a player LED.</summary>
    public const string SDL_PROP_GAMEPAD_CAP_PLAYER_LED_BOOLEAN = "SDL.joystick.cap.player_led";
    /// <summary>Property name indicating whether this gamepad has left/right rumble.</summary>
    public const string SDL_PROP_GAMEPAD_CAP_RUMBLE_BOOLEAN = "SDL.joystick.cap.rumble";
    /// <summary>Property name indicating whether this gamepad has simple trigger rumble.</summary>
    public const string SDL_PROP_GAMEPAD_CAP_TRIGGER_RUMBLE_BOOLEAN = "SDL.joystick.cap.trigger_rumble";

    /// <summary>
    /// Add support for gamepads that SDL is unaware of or change the binding of an existing gamepad.
    /// The mapping string has the format "GUID,name,mapping", where GUID is the string value from
    /// SDL_GUIDToString(), name is the human readable string for the device and mappings are
    /// gamepad mappings to joystick ones.
    /// </summary>
    /// <param name="mapping">The mapping string.</param>
    /// <returns>1 if a new mapping is added, 0 if an existing mapping is updated, -1 on failure; call SDL_GetError() for more information.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int SDL_AddGamepadMapping([MarshalUsing(typeof(Utf8StringMarshaller))] string mapping);

    /// <summary>
    /// Load a set of gamepad mappings from an SDL_IOStream.
    /// </summary>
    /// <param name="src">The data stream for the mappings to be added.</param>
    /// <param name="closeio">If true, calls SDL_CloseIO() on src before returning, even in the case of an error.</param>
    /// <returns>The number of mappings added or -1 on failure; call SDL_GetError() for more information.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int SDL_AddGamepadMappingsFromIO(SDL_IOStream* src, [MarshalAs(UnmanagedType.U1)] bool closeio);

    /// <summary>
    /// Load a set of gamepad mappings from a file.
    /// </summary>
    /// <param name="file">The mappings file to load.</param>
    /// <returns>The number of mappings added or -1 on failure; call SDL_GetError() for more information.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int SDL_AddGamepadMappingsFromFile([MarshalUsing(typeof(Utf8StringMarshaller))] string file);

    /// <summary>
    /// Reinitialize the SDL mapping database to its initial state.
    /// This will generate gamepad events as needed if device mappings change.
    /// </summary>
    /// <returns>True on success or false on failure; call SDL_GetError() for more information.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_ReloadGamepadMappings();

    /// <summary>
    /// Get the current gamepad mappings.
    /// </summary>
    /// <param name="count">A pointer filled in with the number of mappings returned, can be NULL.</param>
    /// <returns>An array of the mapping strings, NULL-terminated, or NULL on failure; call SDL_GetError() for more information. This is a single allocation that should be freed with SDL_free() when it is no longer needed.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial byte** SDL_GetGamepadMappings(int* count);

    /// <summary>
    /// Get the gamepad mapping string for a given GUID.
    /// </summary>
    /// <param name="guid">A structure containing the GUID for which a mapping is desired.</param>
    /// <returns>A mapping string or NULL on failure; call SDL_GetError() for more information. This should be freed with SDL_free() when it is no longer needed.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial byte* SDL_GetGamepadMappingForGUID(Guid guid);

    /// <summary>
    /// Get the current mapping of a gamepad.
    /// </summary>
    /// <param name="gamepad">The gamepad you want to get the current mapping for.</param>
    /// <returns>A string that has the gamepad's mapping or NULL if no mapping is available; call SDL_GetError() for more information. This should be freed with SDL_free() when it is no longer needed.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial byte* SDL_GetGamepadMapping(SDL_Gamepad* gamepad);

    /// <summary>
    /// Set the current mapping of a joystick or gamepad.
    /// </summary>
    /// <param name="instance_id">The joystick instance ID.</param>
    /// <param name="mapping">The mapping to use for this device, or NULL to clear the mapping.</param>
    /// <returns>True on success or false on failure; call SDL_GetError() for more information.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_SetGamepadMapping(SDL_JoystickID instance_id, [MarshalUsing(typeof(Utf8StringMarshaller))] string? mapping);

    /// <summary>
    /// Return whether a gamepad is currently connected.
    /// </summary>
    /// <returns>True if a gamepad is connected, false otherwise.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_HasGamepad();

    /// <summary>
    /// Get a list of currently connected gamepads.
    /// </summary>
    /// <param name="count">A pointer filled in with the number of gamepads returned, may be NULL.</param>
    /// <returns>A 0 terminated array of joystick instance IDs or NULL on failure; call SDL_GetError() for more information. This should be freed with SDL_free() when it is no longer needed.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL_JoystickID* SDL_GetGamepads(int* count);

    /// <summary>
    /// Check if the given joystick is supported by the gamepad interface.
    /// </summary>
    /// <param name="instance_id">The joystick instance ID.</param>
    /// <returns>True if the given joystick is supported by the gamepad interface, false if it isn't or it's an invalid index.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_IsGamepad(SDL_JoystickID instance_id);

    /// <summary>
    /// Get the implementation dependent name of a gamepad.
    /// This can be called before any gamepads are opened.
    /// </summary>
    /// <param name="instance_id">The joystick instance ID.</param>
    /// <returns>The name of the selected gamepad. If no name can be found, this function returns NULL; call SDL_GetError() for more information.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(Utf8StringMarshaller))]
    public static partial string? SDL_GetGamepadNameForID(SDL_JoystickID instance_id);

    /// <summary>
    /// Get the implementation dependent path of a gamepad.
    /// This can be called before any gamepads are opened.
    /// </summary>
    /// <param name="instance_id">The joystick instance ID.</param>
    /// <returns>The path of the selected gamepad. If no path can be found, this function returns NULL; call SDL_GetError() for more information.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(Utf8StringMarshaller))]
    public static partial string? SDL_GetGamepadPathForID(SDL_JoystickID instance_id);

    /// <summary>
    /// Get the player index of a gamepad.
    /// This can be called before any gamepads are opened.
    /// </summary>
    /// <param name="instance_id">The joystick instance ID.</param>
    /// <returns>The player index of a gamepad, or -1 if it's not available.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int SDL_GetGamepadPlayerIndexForID(SDL_JoystickID instance_id);

    /// <summary>
    /// Get the implementation-dependent GUID of a gamepad.
    /// This can be called before any gamepads are opened.
    /// </summary>
    /// <param name="instance_id">The joystick instance ID.</param>
    /// <returns>The GUID of the selected gamepad. If called on an invalid index, this function returns a zero GUID.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial Guid SDL_GetGamepadGUIDForID(SDL_JoystickID instance_id);

    /// <summary>
    /// Get the USB vendor ID of a gamepad, if available.
    /// This can be called before any gamepads are opened. If the vendor ID isn't available this function returns 0.
    /// </summary>
    /// <param name="instance_id">The joystick instance ID.</param>
    /// <returns>The USB vendor ID of the selected gamepad. If called on an invalid index, this function returns zero.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial ushort SDL_GetGamepadVendorForID(SDL_JoystickID instance_id);

    /// <summary>
    /// Get the USB product ID of a gamepad, if available.
    /// This can be called before any gamepads are opened. If the product ID isn't available this function returns 0.
    /// </summary>
    /// <param name="instance_id">The joystick instance ID.</param>
    /// <returns>The USB product ID of the selected gamepad. If called on an invalid index, this function returns zero.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial ushort SDL_GetGamepadProductForID(SDL_JoystickID instance_id);

    /// <summary>
    /// Get the product version of a gamepad, if available.
    /// This can be called before any gamepads are opened. If the product version isn't available this function returns 0.
    /// </summary>
    /// <param name="instance_id">The joystick instance ID.</param>
    /// <returns>The product version of the selected gamepad. If called on an invalid index, this function returns zero.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial ushort SDL_GetGamepadProductVersionForID(SDL_JoystickID instance_id);

    /// <summary>
    /// Get the type of a gamepad.
    /// This can be called before any gamepads are opened.
    /// </summary>
    /// <param name="instance_id">The joystick instance ID.</param>
    /// <returns>The gamepad type.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL_GamepadType SDL_GetGamepadTypeForID(SDL_JoystickID instance_id);

    /// <summary>
    /// Get the type of a gamepad, ignoring any mapping override.
    /// This can be called before any gamepads are opened.
    /// </summary>
    /// <param name="instance_id">The joystick instance ID.</param>
    /// <returns>The gamepad type.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL_GamepadType SDL_GetRealGamepadTypeForID(SDL_JoystickID instance_id);

    /// <summary>
    /// Get the mapping of a gamepad.
    /// This can be called before any gamepads are opened.
    /// </summary>
    /// <param name="instance_id">The joystick instance ID.</param>
    /// <returns>The mapping string. Returns NULL if no mapping is available. This should be freed with SDL_free() when it is no longer needed.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial byte* SDL_GetGamepadMappingForID(SDL_JoystickID instance_id);

    /// <summary>
    /// Open a gamepad for use.
    /// </summary>
    /// <param name="instance_id">The joystick instance ID.</param>
    /// <returns>A gamepad identifier or NULL if an error occurred; call SDL_GetError() for more information.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL_Gamepad* SDL_OpenGamepad(SDL_JoystickID instance_id);

    /// <summary>
    /// Get the SDL_Gamepad associated with a joystick instance ID, if it has been opened.
    /// </summary>
    /// <param name="instance_id">The joystick instance ID of the gamepad.</param>
    /// <returns>An SDL_Gamepad on success or NULL on failure or if it hasn't been opened yet; call SDL_GetError() for more information.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL_Gamepad* SDL_GetGamepadFromID(SDL_JoystickID instance_id);

    /// <summary>
    /// Get the SDL_Gamepad associated with a player index.
    /// </summary>
    /// <param name="player_index">The player index, which is different from the instance ID.</param>
    /// <returns>The SDL_Gamepad associated with a player index.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL_Gamepad* SDL_GetGamepadFromPlayerIndex(int player_index);

    /// <summary>
    /// Get the properties associated with an opened gamepad.
    /// These properties are shared with the underlying joystick object.
    /// </summary>
    /// <param name="gamepad">A gamepad identifier previously returned by SDL_OpenGamepad().</param>
    /// <returns>A valid property ID on success or 0 on failure; call SDL_GetError() for more information.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL_PropertiesID SDL_GetGamepadProperties(SDL_Gamepad* gamepad);

    /// <summary>
    /// Get the instance ID of an opened gamepad.
    /// </summary>
    /// <param name="gamepad">A gamepad identifier previously returned by SDL_OpenGamepad().</param>
    /// <returns>The instance ID of the specified gamepad on success or 0 on failure; call SDL_GetError() for more information.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL_JoystickID SDL_GetGamepadID(SDL_Gamepad* gamepad);

    /// <summary>
    /// Get the implementation-dependent name for an opened gamepad.
    /// </summary>
    /// <param name="gamepad">A gamepad identifier previously returned by SDL_OpenGamepad().</param>
    /// <returns>The implementation dependent name for the gamepad, or NULL if there is no name or the identifier passed is invalid.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(Utf8StringMarshaller))]
    public static partial string? SDL_GetGamepadName(SDL_Gamepad* gamepad);

    /// <summary>
    /// Get the implementation-dependent path for an opened gamepad.
    /// </summary>
    /// <param name="gamepad">A gamepad identifier previously returned by SDL_OpenGamepad().</param>
    /// <returns>The implementation dependent path for the gamepad, or NULL if there is no path or the identifier passed is invalid.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(Utf8StringMarshaller))]
    public static partial string? SDL_GetGamepadPath(SDL_Gamepad* gamepad);

    /// <summary>
    /// Get the type of an opened gamepad.
    /// </summary>
    /// <param name="gamepad">The gamepad object to query.</param>
    /// <returns>The gamepad type, or SDL_GAMEPAD_TYPE_UNKNOWN if it's not available.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL_GamepadType SDL_GetGamepadType(SDL_Gamepad* gamepad);

    /// <summary>
    /// Get the type of an opened gamepad, ignoring any mapping override.
    /// </summary>
    /// <param name="gamepad">The gamepad object to query.</param>
    /// <returns>The gamepad type, or SDL_GAMEPAD_TYPE_UNKNOWN if it's not available.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL_GamepadType SDL_GetRealGamepadType(SDL_Gamepad* gamepad);

    /// <summary>
    /// Get the player index of an opened gamepad.
    /// For XInput gamepads this returns the XInput user index.
    /// </summary>
    /// <param name="gamepad">The gamepad object to query.</param>
    /// <returns>The player index for gamepad, or -1 if it's not available.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int SDL_GetGamepadPlayerIndex(SDL_Gamepad* gamepad);

    /// <summary>
    /// Set the player index of an opened gamepad.
    /// </summary>
    /// <param name="gamepad">The gamepad object to adjust.</param>
    /// <param name="player_index">Player index to assign to this gamepad, or -1 to clear the player index and turn off player LEDs.</param>
    /// <returns>True on success or false on failure; call SDL_GetError() for more information.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_SetGamepadPlayerIndex(SDL_Gamepad* gamepad, int player_index);

    /// <summary>
    /// Get the USB vendor ID of an opened gamepad, if available.
    /// If the vendor ID isn't available this function returns 0.
    /// </summary>
    /// <param name="gamepad">The gamepad object to query.</param>
    /// <returns>The USB vendor ID, or zero if unavailable.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial ushort SDL_GetGamepadVendor(SDL_Gamepad* gamepad);

    /// <summary>
    /// Get the USB product ID of an opened gamepad, if available.
    /// If the product ID isn't available this function returns 0.
    /// </summary>
    /// <param name="gamepad">The gamepad object to query.</param>
    /// <returns>The USB product ID, or zero if unavailable.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial ushort SDL_GetGamepadProduct(SDL_Gamepad* gamepad);

    /// <summary>
    /// Get the product version of an opened gamepad, if available.
    /// If the product version isn't available this function returns 0.
    /// </summary>
    /// <param name="gamepad">The gamepad object to query.</param>
    /// <returns>The USB product version, or zero if unavailable.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial ushort SDL_GetGamepadProductVersion(SDL_Gamepad* gamepad);

    /// <summary>
    /// Get the firmware version of an opened gamepad, if available.
    /// If the firmware version isn't available this function returns 0.
    /// </summary>
    /// <param name="gamepad">The gamepad object to query.</param>
    /// <returns>The gamepad firmware version, or zero if unavailable.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial ushort SDL_GetGamepadFirmwareVersion(SDL_Gamepad* gamepad);

    /// <summary>
    /// Get the serial number of an opened gamepad, if available.
    /// Returns the serial number of the gamepad, or NULL if it is not available.
    /// </summary>
    /// <param name="gamepad">The gamepad object to query.</param>
    /// <returns>The serial number, or NULL if unavailable.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(Utf8StringMarshaller))]
    public static partial string? SDL_GetGamepadSerial(SDL_Gamepad* gamepad);

    /// <summary>
    /// Get the Steam Input handle of an opened gamepad, if available.
    /// Returns an InputHandle_t for the gamepad that can be used with Steam Input API.
    /// </summary>
    /// <param name="gamepad">The gamepad object to query.</param>
    /// <returns>The gamepad handle, or 0 if unavailable.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial ulong SDL_GetGamepadSteamHandle(SDL_Gamepad* gamepad);

    /// <summary>
    /// Get the connection state of a gamepad.
    /// </summary>
    /// <param name="gamepad">The gamepad object to query.</param>
    /// <returns>The connection state on success or SDL_JOYSTICK_CONNECTION_INVALID on failure; call SDL_GetError() for more information.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL_JoystickConnectionState SDL_GetGamepadConnectionState(SDL_Gamepad* gamepad);

    /// <summary>
    /// Get the battery state of a gamepad.
    /// You should never take a battery status as absolute truth. Batteries
    /// (especially failing batteries) are delicate hardware, and the values
    /// reported here are best estimates based on what that hardware reports.
    /// </summary>
    /// <param name="gamepad">The gamepad object to query.</param>
    /// <param name="percent">A pointer filled in with the percentage of battery life left, between 0 and 100, or NULL to ignore. This will be filled in with -1 we can't determine a value or there is no battery.</param>
    /// <returns>The current battery state.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL_PowerState SDL_GetGamepadPowerInfo(SDL_Gamepad* gamepad, int* percent);

    /// <summary>
    /// Check if a gamepad has been opened and is currently connected.
    /// </summary>
    /// <param name="gamepad">A gamepad identifier previously returned by SDL_OpenGamepad().</param>
    /// <returns>True if the gamepad has been opened and is currently connected, or false if not.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_GamepadConnected(SDL_Gamepad* gamepad);

    /// <summary>
    /// Get the underlying joystick from a gamepad.
    /// This function will give you a SDL_Joystick object, which allows you to use
    /// the SDL_Joystick functions with a SDL_Gamepad object.
    /// The pointer returned is owned by the SDL_Gamepad. You should not call SDL_CloseJoystick() on it.
    /// </summary>
    /// <param name="gamepad">The gamepad object that you want to get a joystick from.</param>
    /// <returns>An SDL_Joystick object, or NULL on failure; call SDL_GetError() for more information.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL_Joystick* SDL_GetGamepadJoystick(SDL_Gamepad* gamepad);

    /// <summary>
    /// Set the state of gamepad event processing.
    /// If gamepad events are disabled, you must call SDL_UpdateGamepads() yourself
    /// and check the state of the gamepad when you want gamepad information.
    /// </summary>
    /// <param name="enabled">Whether to process gamepad events or not.</param>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SDL_SetGamepadEventsEnabled([MarshalAs(UnmanagedType.U1)] bool enabled);

    /// <summary>
    /// Query the state of gamepad event processing.
    /// If gamepad events are disabled, you must call SDL_UpdateGamepads() yourself
    /// and check the state of the gamepad when you want gamepad information.
    /// </summary>
    /// <returns>True if gamepad events are being processed, false otherwise.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_GamepadEventsEnabled();

    /// <summary>
    /// Get the SDL joystick layer bindings for a gamepad.
    /// </summary>
    /// <param name="gamepad">A gamepad.</param>
    /// <param name="count">A pointer filled in with the number of bindings returned.</param>
    /// <returns>A NULL terminated array of pointers to bindings or NULL on failure; call SDL_GetError() for more information. This is a single allocation that should be freed with SDL_free() when it is no longer needed.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL_GamepadBinding** SDL_GetGamepadBindings(SDL_Gamepad* gamepad, int* count);

    /// <summary>
    /// Manually pump gamepad updates if not using the loop.
    /// This function is called automatically by the event loop if events are enabled.
    /// Under such circumstances, it will not be necessary to call this function.
    /// </summary>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SDL_UpdateGamepads();

    /// <summary>
    /// Convert a string into SDL_GamepadType enum.
    /// </summary>
    /// <param name="str">String representing a SDL_GamepadType type.</param>
    /// <returns>The SDL_GamepadType enum corresponding to the input string, or SDL_GAMEPAD_TYPE_UNKNOWN if no match was found.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL_GamepadType SDL_GetGamepadTypeFromString([MarshalUsing(typeof(Utf8StringMarshaller))] string str);

    /// <summary>
    /// Convert from an SDL_GamepadType enum to a string.
    /// </summary>
    /// <param name="type">An enum value for a given SDL_GamepadType.</param>
    /// <returns>A string for the given type, or NULL if an invalid type is specified.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(Utf8StringMarshaller))]
    public static partial string? SDL_GetGamepadStringForType(SDL_GamepadType type);

    /// <summary>
    /// Convert a string into SDL_GamepadAxis enum.
    /// </summary>
    /// <param name="str">String representing a SDL_Gamepad axis.</param>
    /// <returns>The SDL_GamepadAxis enum corresponding to the input string, or SDL_GAMEPAD_AXIS_INVALID if no match was found.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL_GamepadAxis SDL_GetGamepadAxisFromString([MarshalUsing(typeof(Utf8StringMarshaller))] string str);

    /// <summary>
    /// Convert from an SDL_GamepadAxis enum to a string.
    /// </summary>
    /// <param name="axis">An enum value for a given SDL_GamepadAxis.</param>
    /// <returns>A string for the given axis, or NULL if an invalid axis is specified.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(Utf8StringMarshaller))]
    public static partial string? SDL_GetGamepadStringForAxis(SDL_GamepadAxis axis);

    /// <summary>
    /// Query whether a gamepad has a given axis.
    /// This merely reports whether the gamepad's mapping defined this axis, as
    /// that is all the information SDL has about the physical device.
    /// </summary>
    /// <param name="gamepad">A gamepad.</param>
    /// <param name="axis">An axis enum value (an SDL_GamepadAxis value).</param>
    /// <returns>True if the gamepad has this axis, false otherwise.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_GamepadHasAxis(SDL_Gamepad* gamepad, SDL_GamepadAxis axis);

    /// <summary>
    /// Get the current state of an axis control on a gamepad.
    /// For thumbsticks, the state is a value ranging from -32768 (up/left) to 32767 (down/right).
    /// Triggers range from 0 when released to 32767 when fully pressed.
    /// </summary>
    /// <param name="gamepad">A gamepad.</param>
    /// <param name="axis">An axis index (one of the SDL_GamepadAxis values).</param>
    /// <returns>Axis state (including 0) on success or 0 (also) on failure; call SDL_GetError() for more information.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial short SDL_GetGamepadAxis(SDL_Gamepad* gamepad, SDL_GamepadAxis axis);

    /// <summary>
    /// Convert a string into an SDL_GamepadButton enum.
    /// </summary>
    /// <param name="str">String representing a SDL_Gamepad axis.</param>
    /// <returns>The SDL_GamepadButton enum corresponding to the input string, or SDL_GAMEPAD_BUTTON_INVALID if no match was found.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL_GamepadButton SDL_GetGamepadButtonFromString([MarshalUsing(typeof(Utf8StringMarshaller))] string str);

    /// <summary>
    /// Convert from an SDL_GamepadButton enum to a string.
    /// </summary>
    /// <param name="button">An enum value for a given SDL_GamepadButton.</param>
    /// <returns>A string for the given button, or NULL if an invalid button is specified.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(Utf8StringMarshaller))]
    public static partial string? SDL_GetGamepadStringForButton(SDL_GamepadButton button);

    /// <summary>
    /// Query whether a gamepad has a given button.
    /// This merely reports whether the gamepad's mapping defined this button, as
    /// that is all the information SDL has about the physical device.
    /// </summary>
    /// <param name="gamepad">A gamepad.</param>
    /// <param name="button">A button enum value (an SDL_GamepadButton value).</param>
    /// <returns>True if the gamepad has this button, false otherwise.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_GamepadHasButton(SDL_Gamepad* gamepad, SDL_GamepadButton button);

    /// <summary>
    /// Get the current state of a button on a gamepad.
    /// </summary>
    /// <param name="gamepad">A gamepad.</param>
    /// <param name="button">A button index (one of the SDL_GamepadButton values).</param>
    /// <returns>True if the button is pressed, false otherwise.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_GetGamepadButton(SDL_Gamepad* gamepad, SDL_GamepadButton button);

    /// <summary>
    /// Get the label of a button on a gamepad for a specific gamepad type.
    /// </summary>
    /// <param name="type">The type of gamepad to check.</param>
    /// <param name="button">A button index (one of the SDL_GamepadButton values).</param>
    /// <returns>The SDL_GamepadButtonLabel enum corresponding to the button label.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL_GamepadButtonLabel SDL_GetGamepadButtonLabelForType(SDL_GamepadType type, SDL_GamepadButton button);

    /// <summary>
    /// Get the label of a button on a gamepad.
    /// </summary>
    /// <param name="gamepad">A gamepad.</param>
    /// <param name="button">A button index (one of the SDL_GamepadButton values).</param>
    /// <returns>The SDL_GamepadButtonLabel enum corresponding to the button label.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL_GamepadButtonLabel SDL_GetGamepadButtonLabel(SDL_Gamepad* gamepad, SDL_GamepadButton button);

    /// <summary>
    /// Get the number of touchpads on a gamepad.
    /// </summary>
    /// <param name="gamepad">A gamepad.</param>
    /// <returns>Number of touchpads.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int SDL_GetNumGamepadTouchpads(SDL_Gamepad* gamepad);

    /// <summary>
    /// Get the number of supported simultaneous fingers on a touchpad on a gamepad.
    /// </summary>
    /// <param name="gamepad">A gamepad.</param>
    /// <param name="touchpad">A touchpad.</param>
    /// <returns>Number of supported simultaneous fingers.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int SDL_GetNumGamepadTouchpadFingers(SDL_Gamepad* gamepad, int touchpad);

    /// <summary>
    /// Get the current state of a finger on a touchpad on a gamepad.
    /// </summary>
    /// <param name="gamepad">A gamepad.</param>
    /// <param name="touchpad">A touchpad.</param>
    /// <param name="finger">A finger.</param>
    /// <param name="down">A pointer filled with true (non-zero) if the finger is down, false (zero) otherwise, may be NULL.</param>
    /// <param name="x">A pointer filled with the x position, normalized 0 to 1, with the origin in the upper left, may be NULL.</param>
    /// <param name="y">A pointer filled with the y position, normalized 0 to 1, with the origin in the upper left, may be NULL.</param>
    /// <param name="pressure">A pointer filled with pressure value, may be NULL.</param>
    /// <returns>True on success or false on failure; call SDL_GetError() for more information.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_GetGamepadTouchpadFinger(SDL_Gamepad* gamepad, int touchpad, int finger, byte* down, float* x, float* y, float* pressure);

    /// <summary>
    /// Return whether a gamepad has a particular sensor.
    /// </summary>
    /// <param name="gamepad">The gamepad to query.</param>
    /// <param name="type">The type of sensor to query.</param>
    /// <returns>True if the sensor exists, false otherwise.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_GamepadHasSensor(SDL_Gamepad* gamepad, SDL_SensorType type);

    /// <summary>
    /// Set whether data reporting for a gamepad sensor is enabled.
    /// </summary>
    /// <param name="gamepad">The gamepad to update.</param>
    /// <param name="type">The type of sensor to enable/disable.</param>
    /// <param name="enabled">Whether data reporting should be enabled.</param>
    /// <returns>True on success or false on failure; call SDL_GetError() for more information.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_SetGamepadSensorEnabled(SDL_Gamepad* gamepad, SDL_SensorType type, [MarshalAs(UnmanagedType.U1)] bool enabled);

    /// <summary>
    /// Query whether sensor data reporting is enabled for a gamepad.
    /// </summary>
    /// <param name="gamepad">The gamepad to query.</param>
    /// <param name="type">The type of sensor to query.</param>
    /// <returns>True if the sensor is enabled, false otherwise.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_GamepadSensorEnabled(SDL_Gamepad* gamepad, SDL_SensorType type);

    /// <summary>
    /// Get the data rate (number of events per second) of a gamepad sensor.
    /// </summary>
    /// <param name="gamepad">The gamepad to query.</param>
    /// <param name="type">The type of sensor to query.</param>
    /// <returns>The data rate, or 0.0f if the data rate is not available.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial float SDL_GetGamepadSensorDataRate(SDL_Gamepad* gamepad, SDL_SensorType type);

    /// <summary>
    /// Get the current state of a gamepad sensor.
    /// The number of values and interpretation of the data is sensor dependent.
    /// See SDL_sensor.h for the details for each type of sensor.
    /// </summary>
    /// <param name="gamepad">The gamepad to query.</param>
    /// <param name="type">The type of sensor to query.</param>
    /// <param name="data">A pointer filled with the current sensor state.</param>
    /// <param name="num_values">The number of values to write to data.</param>
    /// <returns>True on success or false on failure; call SDL_GetError() for more information.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_GetGamepadSensorData(SDL_Gamepad* gamepad, SDL_SensorType type, float* data, int num_values);

    /// <summary>
    /// Start a rumble effect on a gamepad.
    /// Each call to this function cancels any previous rumble effect, and calling
    /// it with 0 intensity stops any rumbling.
    /// </summary>
    /// <param name="gamepad">The gamepad to vibrate.</param>
    /// <param name="low_frequency_rumble">The intensity of the low frequency (left) rumble motor, from 0 to 0xFFFF.</param>
    /// <param name="high_frequency_rumble">The intensity of the high frequency (right) rumble motor, from 0 to 0xFFFF.</param>
    /// <param name="duration_ms">The duration of the rumble effect, in milliseconds.</param>
    /// <returns>True on success or false on failure; call SDL_GetError() for more information.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_RumbleGamepad(SDL_Gamepad* gamepad, ushort low_frequency_rumble, ushort high_frequency_rumble, uint duration_ms);

    /// <summary>
    /// Start a rumble effect in the gamepad's triggers.
    /// Each call to this function cancels any previous trigger rumble effect, and
    /// calling it with 0 intensity stops any rumbling.
    /// Note that this is rumbling of the triggers and not the gamepad as a whole.
    /// This is currently only supported on Xbox One gamepads.
    /// </summary>
    /// <param name="gamepad">The gamepad to vibrate.</param>
    /// <param name="left_rumble">The intensity of the left trigger rumble motor, from 0 to 0xFFFF.</param>
    /// <param name="right_rumble">The intensity of the right trigger rumble motor, from 0 to 0xFFFF.</param>
    /// <param name="duration_ms">The duration of the rumble effect, in milliseconds.</param>
    /// <returns>True on success or false on failure; call SDL_GetError() for more information.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_RumbleGamepadTriggers(SDL_Gamepad* gamepad, ushort left_rumble, ushort right_rumble, uint duration_ms);

    /// <summary>
    /// Update a gamepad's LED color.
    /// An example of a joystick LED is the light on the back of a PlayStation 4's DualShock 4 controller.
    /// For gamepads with a single color LED, the maximum of the RGB values will be used as the LED brightness.
    /// </summary>
    /// <param name="gamepad">The gamepad to update.</param>
    /// <param name="red">The intensity of the red LED.</param>
    /// <param name="green">The intensity of the green LED.</param>
    /// <param name="blue">The intensity of the blue LED.</param>
    /// <returns>True on success or false on failure; call SDL_GetError() for more information.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_SetGamepadLED(SDL_Gamepad* gamepad, byte red, byte green, byte blue);

    /// <summary>
    /// Send a gamepad specific effect packet.
    /// </summary>
    /// <param name="gamepad">The gamepad to affect.</param>
    /// <param name="data">The data to send to the gamepad.</param>
    /// <param name="size">The size of the data to send to the gamepad.</param>
    /// <returns>True on success or false on failure; call SDL_GetError() for more information.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_SendGamepadEffect(SDL_Gamepad* gamepad, void* data, int size);

    /// <summary>
    /// Close a gamepad previously opened with SDL_OpenGamepad().
    /// </summary>
    /// <param name="gamepad">A gamepad identifier previously returned by SDL_OpenGamepad().</param>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SDL_CloseGamepad(SDL_Gamepad* gamepad);

    /// <summary>
    /// Return the sfSymbolsName for a given button on a gamepad on Apple platforms.
    /// </summary>
    /// <param name="gamepad">The gamepad to query.</param>
    /// <param name="button">A button on the gamepad.</param>
    /// <returns>The sfSymbolsName or NULL if the name can't be found.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(Utf8StringMarshaller))]
    public static partial string? SDL_GetGamepadAppleSFSymbolsNameForButton(SDL_Gamepad* gamepad, SDL_GamepadButton button);

    /// <summary>
    /// Return the sfSymbolsName for a given axis on a gamepad on Apple platforms.
    /// </summary>
    /// <param name="gamepad">The gamepad to query.</param>
    /// <param name="axis">An axis on the gamepad.</param>
    /// <returns>The sfSymbolsName or NULL if the name can't be found.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(Utf8StringMarshaller))]
    public static partial string? SDL_GetGamepadAppleSFSymbolsNameForAxis(SDL_Gamepad* gamepad, SDL_GamepadAxis axis);
}
