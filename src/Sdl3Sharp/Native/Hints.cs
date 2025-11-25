using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;
using static Sdl3Sharp.Native.Common;

// We are intentionally exposing the P/Invoke calls so people can do low-level calls if needed
#pragma warning disable CA1401 // P/Invokes should not be visible

namespace Sdl3Sharp.Native;

/// <summary>
/// P/Invoke bindings for SDL_hints.h - SDL configuration hints API.
/// </summary>
public static unsafe partial class Hints
{
    /// <summary>
    /// An enumeration of hint priorities.
    /// </summary>
    public enum SDL_HintPriority
    {
        /// <summary>
        /// Low priority, used for default values.
        /// </summary>
        SDL_HINT_DEFAULT,

        /// <summary>
        /// Medium priority.
        /// </summary>
        SDL_HINT_NORMAL,

        /// <summary>
        /// High priority.
        /// </summary>
        SDL_HINT_OVERRIDE
    }

    /// <summary>
    /// Set a hint with a specific priority.
    /// </summary>
    /// <param name="name">The hint to set.</param>
    /// <param name="value">The value of the hint variable.</param>
    /// <param name="priority">The SDL_HintPriority level for the hint.</param>
    /// <returns>true on success or false on failure; call SDL_GetError() for more information.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_SetHintWithPriority(
        [MarshalUsing(typeof(Utf8StringMarshaller))] string name,
        [MarshalUsing(typeof(Utf8StringMarshaller))] string? value,
        SDL_HintPriority priority);

    /// <summary>
    /// Set a hint with normal priority.
    /// </summary>
    /// <param name="name">The hint to set.</param>
    /// <param name="value">The value of the hint variable.</param>
    /// <returns>true on success or false on failure; call SDL_GetError() for more information.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_SetHint(
        [MarshalUsing(typeof(Utf8StringMarshaller))] string name,
        [MarshalUsing(typeof(Utf8StringMarshaller))] string? value);

    /// <summary>
    /// Reset a hint to the default value.
    /// </summary>
    /// <param name="name">The hint to set.</param>
    /// <returns>true on success or false on failure; call SDL_GetError() for more information.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_ResetHint([MarshalUsing(typeof(Utf8StringMarshaller))] string name);

    /// <summary>
    /// Reset all hints to the default values.
    /// </summary>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    public static partial void SDL_ResetHints();

    /// <summary>
    /// Get the value of a hint.
    /// </summary>
    /// <param name="name">The hint to query.</param>
    /// <returns>The string value of a hint or NULL if the hint isn't set.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    [return: MarshalUsing(typeof(Utf8StringMarshaller))]
    public static partial string? SDL_GetHint([MarshalUsing(typeof(Utf8StringMarshaller))] string name);

    /// <summary>
    /// Get the boolean value of a hint variable.
    /// </summary>
    /// <param name="name">The name of the hint to get the boolean value from.</param>
    /// <param name="default_value">The value to return if the hint does not exist.</param>
    /// <returns>The boolean value of a hint or the provided default value if the hint does not exist.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_GetHintBoolean(
        [MarshalUsing(typeof(Utf8StringMarshaller))] string name,
        [MarshalAs(UnmanagedType.U1)] bool default_value);

    /// <summary>
    /// Add a function to watch a particular hint.
    /// </summary>
    /// <param name="name">The hint to watch.</param>
    /// <param name="callback">An SDL_HintCallback function that will be called when the hint value changes.</param>
    /// <param name="userdata">A pointer to pass to the callback function.</param>
    /// <returns>true on success or false on failure; call SDL_GetError() for more information.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_AddHintCallback(
        [MarshalUsing(typeof(Utf8StringMarshaller))] string name,
        delegate* unmanaged[Cdecl]<nuint, byte*, byte*, byte*, void> callback,
        nuint userdata);

    /// <summary>
    /// Remove a function watching a particular hint.
    /// </summary>
    /// <param name="name">The hint being watched.</param>
    /// <param name="callback">An SDL_HintCallback function that will be called when the hint value changes.</param>
    /// <param name="userdata">A pointer being passed to the callback function.</param>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    public static partial void SDL_RemoveHintCallback(
        [MarshalUsing(typeof(Utf8StringMarshaller))] string name,
        delegate* unmanaged[Cdecl]<nuint, byte*, byte*, byte*, void> callback,
        nuint userdata);

    // Hint name constants

    /// <summary>
    /// Specify the behavior of Alt+Tab while the keyboard is grabbed.
    /// </summary>
    public const string SDL_HINT_ALLOW_ALT_TAB_WHILE_GRABBED = "SDL_ALLOW_ALT_TAB_WHILE_GRABBED";

    /// <summary>
    /// A variable to control whether the SDL activity is allowed to be re-created on Android.
    /// </summary>
    public const string SDL_HINT_ANDROID_ALLOW_RECREATE_ACTIVITY = "SDL_ANDROID_ALLOW_RECREATE_ACTIVITY";

    /// <summary>
    /// A variable to control whether the event loop will block itself when the app is paused on Android.
    /// </summary>
    public const string SDL_HINT_ANDROID_BLOCK_ON_PAUSE = "SDL_ANDROID_BLOCK_ON_PAUSE";

    /// <summary>
    /// A variable to control whether low latency audio should be enabled on Android.
    /// </summary>
    public const string SDL_HINT_ANDROID_LOW_LATENCY_AUDIO = "SDL_ANDROID_LOW_LATENCY_AUDIO";

    /// <summary>
    /// A variable to control whether we trap the Android back button to handle it manually.
    /// </summary>
    public const string SDL_HINT_ANDROID_TRAP_BACK_BUTTON = "SDL_ANDROID_TRAP_BACK_BUTTON";

    /// <summary>
    /// A variable setting the app ID string.
    /// </summary>
    public const string SDL_HINT_APP_ID = "SDL_APP_ID";

    /// <summary>
    /// A variable setting the application name.
    /// </summary>
    public const string SDL_HINT_APP_NAME = "SDL_APP_NAME";

    /// <summary>
    /// A variable controlling whether controllers used with the Apple TV generate UI events.
    /// </summary>
    public const string SDL_HINT_APPLE_TV_CONTROLLER_UI_EVENTS = "SDL_APPLE_TV_CONTROLLER_UI_EVENTS";

    /// <summary>
    /// A variable controlling whether the Apple TV remote's joystick axes will automatically match the rotation of the remote.
    /// </summary>
    public const string SDL_HINT_APPLE_TV_REMOTE_ALLOW_ROTATION = "SDL_APPLE_TV_REMOTE_ALLOW_ROTATION";

    /// <summary>
    /// Specify the default ALSA audio device name.
    /// </summary>
    public const string SDL_HINT_AUDIO_ALSA_DEFAULT_DEVICE = "SDL_AUDIO_ALSA_DEFAULT_DEVICE";

    /// <summary>
    /// Specify the default ALSA audio playback device name.
    /// </summary>
    public const string SDL_HINT_AUDIO_ALSA_DEFAULT_PLAYBACK_DEVICE = "SDL_AUDIO_ALSA_DEFAULT_PLAYBACK_DEVICE";

    /// <summary>
    /// Specify the default ALSA audio recording device name.
    /// </summary>
    public const string SDL_HINT_AUDIO_ALSA_DEFAULT_RECORDING_DEVICE = "SDL_AUDIO_ALSA_DEFAULT_RECORDING_DEVICE";

    /// <summary>
    /// A variable controlling the audio category on iOS and macOS.
    /// </summary>
    public const string SDL_HINT_AUDIO_CATEGORY = "SDL_AUDIO_CATEGORY";

    /// <summary>
    /// A variable controlling the default audio channel count.
    /// </summary>
    public const string SDL_HINT_AUDIO_CHANNELS = "SDL_AUDIO_CHANNELS";

    /// <summary>
    /// Specify an application icon name for an audio device.
    /// </summary>
    public const string SDL_HINT_AUDIO_DEVICE_APP_ICON_NAME = "SDL_AUDIO_DEVICE_APP_ICON_NAME";

    /// <summary>
    /// A variable controlling device buffer size.
    /// </summary>
    public const string SDL_HINT_AUDIO_DEVICE_SAMPLE_FRAMES = "SDL_AUDIO_DEVICE_SAMPLE_FRAMES";

    /// <summary>
    /// Specify an audio stream name for an audio device.
    /// </summary>
    public const string SDL_HINT_AUDIO_DEVICE_STREAM_NAME = "SDL_AUDIO_DEVICE_STREAM_NAME";

    /// <summary>
    /// Specify an application role for an audio device.
    /// </summary>
    public const string SDL_HINT_AUDIO_DEVICE_STREAM_ROLE = "SDL_AUDIO_DEVICE_STREAM_ROLE";

    /// <summary>
    /// Specify the input file when recording audio using the disk audio driver.
    /// </summary>
    public const string SDL_HINT_AUDIO_DISK_INPUT_FILE = "SDL_AUDIO_DISK_INPUT_FILE";

    /// <summary>
    /// Specify the output file when playing audio using the disk audio driver.
    /// </summary>
    public const string SDL_HINT_AUDIO_DISK_OUTPUT_FILE = "SDL_AUDIO_DISK_OUTPUT_FILE";

    /// <summary>
    /// A variable controlling the audio rate when using the disk audio driver.
    /// </summary>
    public const string SDL_HINT_AUDIO_DISK_TIMESCALE = "SDL_AUDIO_DISK_TIMESCALE";

    /// <summary>
    /// A variable that specifies an audio backend to use.
    /// </summary>
    public const string SDL_HINT_AUDIO_DRIVER = "SDL_AUDIO_DRIVER";

    /// <summary>
    /// A variable controlling the audio rate when using the dummy audio driver.
    /// </summary>
    public const string SDL_HINT_AUDIO_DUMMY_TIMESCALE = "SDL_AUDIO_DUMMY_TIMESCALE";

    /// <summary>
    /// A variable controlling the default audio format.
    /// </summary>
    public const string SDL_HINT_AUDIO_FORMAT = "SDL_AUDIO_FORMAT";

    /// <summary>
    /// A variable controlling the default audio frequency.
    /// </summary>
    public const string SDL_HINT_AUDIO_FREQUENCY = "SDL_AUDIO_FREQUENCY";

    /// <summary>
    /// A variable that causes SDL to not ignore audio "monitors".
    /// </summary>
    public const string SDL_HINT_AUDIO_INCLUDE_MONITORS = "SDL_AUDIO_INCLUDE_MONITORS";

    /// <summary>
    /// A variable controlling whether SDL updates joystick state when getting input events.
    /// </summary>
    public const string SDL_HINT_AUTO_UPDATE_JOYSTICKS = "SDL_AUTO_UPDATE_JOYSTICKS";

    /// <summary>
    /// A variable controlling whether SDL updates sensor state when getting input events.
    /// </summary>
    public const string SDL_HINT_AUTO_UPDATE_SENSORS = "SDL_AUTO_UPDATE_SENSORS";

    /// <summary>
    /// Prevent SDL from using version 4 of the bitmap header when saving BMPs.
    /// </summary>
    public const string SDL_HINT_BMP_SAVE_LEGACY_FORMAT = "SDL_BMP_SAVE_LEGACY_FORMAT";

    /// <summary>
    /// A variable that decides what camera backend to use.
    /// </summary>
    public const string SDL_HINT_CAMERA_DRIVER = "SDL_CAMERA_DRIVER";

    /// <summary>
    /// A variable that limits what CPU features are available.
    /// </summary>
    public const string SDL_HINT_CPU_FEATURE_MASK = "SDL_CPU_FEATURE_MASK";

    /// <summary>
    /// A variable controlling whether DirectInput should be used for controllers.
    /// </summary>
    public const string SDL_HINT_JOYSTICK_DIRECTINPUT = "SDL_JOYSTICK_DIRECTINPUT";

    /// <summary>
    /// A variable that specifies a dialog backend to use.
    /// </summary>
    public const string SDL_HINT_FILE_DIALOG_DRIVER = "SDL_FILE_DIALOG_DRIVER";

    /// <summary>
    /// Override for SDL_GetDisplayUsableBounds().
    /// </summary>
    public const string SDL_HINT_DISPLAY_USABLE_BOUNDS = "SDL_DISPLAY_USABLE_BOUNDS";

    /// <summary>
    /// Disable giving back control to the browser automatically when running with asyncify.
    /// </summary>
    public const string SDL_HINT_EMSCRIPTEN_ASYNCIFY = "SDL_EMSCRIPTEN_ASYNCIFY";

    /// <summary>
    /// Specify the CSS selector used for the "default" window/canvas.
    /// </summary>
    public const string SDL_HINT_EMSCRIPTEN_CANVAS_SELECTOR = "SDL_EMSCRIPTEN_CANVAS_SELECTOR";

    /// <summary>
    /// Override the binding element for keyboard inputs for Emscripten builds.
    /// </summary>
    public const string SDL_HINT_EMSCRIPTEN_KEYBOARD_ELEMENT = "SDL_EMSCRIPTEN_KEYBOARD_ELEMENT";

    /// <summary>
    /// A variable that controls whether the on-screen keyboard should be shown when text input is active.
    /// </summary>
    public const string SDL_HINT_ENABLE_SCREEN_KEYBOARD = "SDL_ENABLE_SCREEN_KEYBOARD";

    /// <summary>
    /// A variable containing a list of evdev devices to use if udev is not available.
    /// </summary>
    public const string SDL_HINT_EVDEV_DEVICES = "SDL_EVDEV_DEVICES";

    /// <summary>
    /// A variable controlling verbosity of the logging of SDL events pushed onto the internal queue.
    /// </summary>
    public const string SDL_HINT_EVENT_LOGGING = "SDL_EVENT_LOGGING";

    /// <summary>
    /// A variable controlling whether raising the window should be done more forcefully.
    /// </summary>
    public const string SDL_HINT_FORCE_RAISEWINDOW = "SDL_FORCE_RAISEWINDOW";

    /// <summary>
    /// A variable controlling how 3D acceleration is used to accelerate the SDL screen surface.
    /// </summary>
    public const string SDL_HINT_FRAMEBUFFER_ACCELERATION = "SDL_FRAMEBUFFER_ACCELERATION";

    /// <summary>
    /// A variable that lets you manually hint extra gamecontroller db entries.
    /// </summary>
    public const string SDL_HINT_GAMECONTROLLERCONFIG = "SDL_GAMECONTROLLERCONFIG";

    /// <summary>
    /// A variable that lets you provide a file with extra gamecontroller db entries.
    /// </summary>
    public const string SDL_HINT_GAMECONTROLLERCONFIG_FILE = "SDL_GAMECONTROLLERCONFIG_FILE";

    /// <summary>
    /// A variable that overrides the automatic controller type detection.
    /// </summary>
    public const string SDL_HINT_GAMECONTROLLERTYPE = "SDL_GAMECONTROLLERTYPE";

    /// <summary>
    /// A variable containing a list of devices to skip when scanning for game controllers.
    /// </summary>
    public const string SDL_HINT_GAMECONTROLLER_IGNORE_DEVICES = "SDL_GAMECONTROLLER_IGNORE_DEVICES";

    /// <summary>
    /// A variable containing a list of devices to include when scanning for game controllers.
    /// </summary>
    public const string SDL_HINT_GAMECONTROLLER_IGNORE_DEVICES_EXCEPT = "SDL_GAMECONTROLLER_IGNORE_DEVICES_EXCEPT";

    /// <summary>
    /// A variable that controls whether the device's built-in accelerometer and gyro should be used as sensors for gamepads.
    /// </summary>
    public const string SDL_HINT_GAMECONTROLLER_SENSOR_FUSION = "SDL_GAMECONTROLLER_SENSOR_FUSION";

    /// <summary>
    /// This variable sets the default text of the TextInput window on GDK platforms.
    /// </summary>
    public const string SDL_HINT_GDK_TEXTINPUT_DEFAULT_TEXT = "SDL_GDK_TEXTINPUT_DEFAULT_TEXT";

    /// <summary>
    /// This variable sets the description of the TextInput window on GDK platforms.
    /// </summary>
    public const string SDL_HINT_GDK_TEXTINPUT_DESCRIPTION = "SDL_GDK_TEXTINPUT_DESCRIPTION";

    /// <summary>
    /// This variable sets the maximum input length of the TextInput window on GDK platforms.
    /// </summary>
    public const string SDL_HINT_GDK_TEXTINPUT_MAX_LENGTH = "SDL_GDK_TEXTINPUT_MAX_LENGTH";

    /// <summary>
    /// This variable sets the input scope of the TextInput window on GDK platforms.
    /// </summary>
    public const string SDL_HINT_GDK_TEXTINPUT_SCOPE = "SDL_GDK_TEXTINPUT_SCOPE";

    /// <summary>
    /// This variable sets the title of the TextInput window on GDK platforms.
    /// </summary>
    public const string SDL_HINT_GDK_TEXTINPUT_TITLE = "SDL_GDK_TEXTINPUT_TITLE";

    /// <summary>
    /// A variable to control whether HIDAPI uses libusb for device access.
    /// </summary>
    public const string SDL_HINT_HIDAPI_LIBUSB = "SDL_HIDAPI_LIBUSB";

    /// <summary>
    /// A variable to control whether HIDAPI uses libusb only for whitelisted devices.
    /// </summary>
    public const string SDL_HINT_HIDAPI_LIBUSB_WHITELIST = "SDL_HIDAPI_LIBUSB_WHITELIST";

    /// <summary>
    /// A variable to control whether HIDAPI uses udev for device detection.
    /// </summary>
    public const string SDL_HINT_HIDAPI_UDEV = "SDL_HIDAPI_UDEV";

    /// <summary>
    /// A variable that specifies a GPU backend to use.
    /// </summary>
    public const string SDL_HINT_GPU_DRIVER = "SDL_GPU_DRIVER";

    /// <summary>
    /// A variable to control whether SDL_hid_enumerate() enumerates all HID devices or only controllers.
    /// </summary>
    public const string SDL_HINT_HIDAPI_ENUMERATE_ONLY_CONTROLLERS = "SDL_HIDAPI_ENUMERATE_ONLY_CONTROLLERS";

    /// <summary>
    /// A variable containing a list of devices to ignore in SDL_hid_enumerate().
    /// </summary>
    public const string SDL_HINT_HIDAPI_IGNORE_DEVICES = "SDL_HIDAPI_IGNORE_DEVICES";

    /// <summary>
    /// A variable describing what IME UI elements the application can display.
    /// </summary>
    public const string SDL_HINT_IME_IMPLEMENTED_UI = "SDL_IME_IMPLEMENTED_UI";

    /// <summary>
    /// A variable controlling whether the home indicator bar on iPhone X should be hidden.
    /// </summary>
    public const string SDL_HINT_IOS_HIDE_HOME_INDICATOR = "SDL_IOS_HIDE_HOME_INDICATOR";

    /// <summary>
    /// A variable that lets you enable joystick (and gamecontroller) events even when your app is in the background.
    /// </summary>
    public const string SDL_HINT_JOYSTICK_ALLOW_BACKGROUND_EVENTS = "SDL_JOYSTICK_ALLOW_BACKGROUND_EVENTS";

    /// <summary>
    /// A variable containing a list of arcade stick style controllers.
    /// </summary>
    public const string SDL_HINT_JOYSTICK_ARCADESTICK_DEVICES = "SDL_JOYSTICK_ARCADESTICK_DEVICES";

    /// <summary>
    /// A variable containing a list of devices that are not arcade stick style controllers.
    /// </summary>
    public const string SDL_HINT_JOYSTICK_ARCADESTICK_DEVICES_EXCLUDED = "SDL_JOYSTICK_ARCADESTICK_DEVICES_EXCLUDED";

    /// <summary>
    /// A variable containing a list of devices that should not be considered joysticks.
    /// </summary>
    public const string SDL_HINT_JOYSTICK_BLACKLIST_DEVICES = "SDL_JOYSTICK_BLACKLIST_DEVICES";

    /// <summary>
    /// A variable containing a list of devices that should be considered joysticks.
    /// </summary>
    public const string SDL_HINT_JOYSTICK_BLACKLIST_DEVICES_EXCLUDED = "SDL_JOYSTICK_BLACKLIST_DEVICES_EXCLUDED";

    /// <summary>
    /// A variable containing a comma separated list of devices to open as joysticks.
    /// </summary>
    public const string SDL_HINT_JOYSTICK_DEVICE = "SDL_JOYSTICK_DEVICE";

    /// <summary>
    /// A variable controlling whether enhanced reports should be used for controllers when using the HIDAPI driver.
    /// </summary>
    public const string SDL_HINT_JOYSTICK_ENHANCED_REPORTS = "SDL_JOYSTICK_ENHANCED_REPORTS";

    /// <summary>
    /// A variable containing a list of flightstick style controllers.
    /// </summary>
    public const string SDL_HINT_JOYSTICK_FLIGHTSTICK_DEVICES = "SDL_JOYSTICK_FLIGHTSTICK_DEVICES";

    /// <summary>
    /// A variable containing a list of devices that are not flightstick style controllers.
    /// </summary>
    public const string SDL_HINT_JOYSTICK_FLIGHTSTICK_DEVICES_EXCLUDED = "SDL_JOYSTICK_FLIGHTSTICK_DEVICES_EXCLUDED";

    /// <summary>
    /// A variable controlling whether GameInput should be used for controller handling on Windows.
    /// </summary>
    public const string SDL_HINT_JOYSTICK_GAMEINPUT = "SDL_JOYSTICK_GAMEINPUT";

    /// <summary>
    /// A variable containing a list of devices known to have a GameCube form factor.
    /// </summary>
    public const string SDL_HINT_JOYSTICK_GAMECUBE_DEVICES = "SDL_JOYSTICK_GAMECUBE_DEVICES";

    /// <summary>
    /// A variable containing a list of devices known not to have a GameCube form factor.
    /// </summary>
    public const string SDL_HINT_JOYSTICK_GAMECUBE_DEVICES_EXCLUDED = "SDL_JOYSTICK_GAMECUBE_DEVICES_EXCLUDED";

    /// <summary>
    /// A variable controlling whether the HIDAPI joystick drivers should be used.
    /// </summary>
    public const string SDL_HINT_JOYSTICK_HIDAPI = "SDL_JOYSTICK_HIDAPI";

    /// <summary>
    /// A variable controlling whether Nintendo Switch Joy-Con controllers will be combined into a single Pro-like controller.
    /// </summary>
    public const string SDL_HINT_JOYSTICK_HIDAPI_COMBINE_JOY_CONS = "SDL_JOYSTICK_HIDAPI_COMBINE_JOY_CONS";

    /// <summary>
    /// A variable controlling whether the HIDAPI driver for Nintendo GameCube controllers should be used.
    /// </summary>
    public const string SDL_HINT_JOYSTICK_HIDAPI_GAMECUBE = "SDL_JOYSTICK_HIDAPI_GAMECUBE";

    /// <summary>
    /// A variable controlling whether rumble is used to implement the GameCube controller's 3 rumble modes.
    /// </summary>
    public const string SDL_HINT_JOYSTICK_HIDAPI_GAMECUBE_RUMBLE_BRAKE = "SDL_JOYSTICK_HIDAPI_GAMECUBE_RUMBLE_BRAKE";

    /// <summary>
    /// A variable controlling whether the HIDAPI driver for Nintendo Switch Joy-Cons should be used.
    /// </summary>
    public const string SDL_HINT_JOYSTICK_HIDAPI_JOY_CONS = "SDL_JOYSTICK_HIDAPI_JOY_CONS";

    /// <summary>
    /// A variable controlling whether the Home button LED should be turned on when a Nintendo Switch Joy-Con controller is opened.
    /// </summary>
    public const string SDL_HINT_JOYSTICK_HIDAPI_JOYCON_HOME_LED = "SDL_JOYSTICK_HIDAPI_JOYCON_HOME_LED";

    /// <summary>
    /// A variable controlling whether the HIDAPI driver for Amazon Luna controllers connected via Bluetooth should be used.
    /// </summary>
    public const string SDL_HINT_JOYSTICK_HIDAPI_LUNA = "SDL_JOYSTICK_HIDAPI_LUNA";

    /// <summary>
    /// A variable controlling whether the HIDAPI driver for Nintendo Online classic controllers should be used.
    /// </summary>
    public const string SDL_HINT_JOYSTICK_HIDAPI_NINTENDO_CLASSIC = "SDL_JOYSTICK_HIDAPI_NINTENDO_CLASSIC";

    /// <summary>
    /// A variable controlling whether the HIDAPI driver for PS3 controllers should be used.
    /// </summary>
    public const string SDL_HINT_JOYSTICK_HIDAPI_PS3 = "SDL_JOYSTICK_HIDAPI_PS3";

    /// <summary>
    /// A variable controlling whether the Sony driver (sixaxis.sys) for PS3 controllers should be used.
    /// </summary>
    public const string SDL_HINT_JOYSTICK_HIDAPI_PS3_SIXAXIS_DRIVER = "SDL_JOYSTICK_HIDAPI_PS3_SIXAXIS_DRIVER";

    /// <summary>
    /// A variable controlling whether the HIDAPI driver for PS4 controllers should be used.
    /// </summary>
    public const string SDL_HINT_JOYSTICK_HIDAPI_PS4 = "SDL_JOYSTICK_HIDAPI_PS4";

    /// <summary>
    /// A variable controlling the update rate of the PS4 controller over Bluetooth.
    /// </summary>
    public const string SDL_HINT_JOYSTICK_HIDAPI_PS4_REPORT_INTERVAL = "SDL_JOYSTICK_HIDAPI_PS4_REPORT_INTERVAL";

    /// <summary>
    /// A variable controlling whether the HIDAPI driver for PS5 controllers should be used.
    /// </summary>
    public const string SDL_HINT_JOYSTICK_HIDAPI_PS5 = "SDL_JOYSTICK_HIDAPI_PS5";

    /// <summary>
    /// A variable controlling whether the player LEDs should be lit to indicate which player is associated with a PS5 controller.
    /// </summary>
    public const string SDL_HINT_JOYSTICK_HIDAPI_PS5_PLAYER_LED = "SDL_JOYSTICK_HIDAPI_PS5_PLAYER_LED";

    /// <summary>
    /// A variable controlling whether the HIDAPI driver for NVIDIA SHIELD controllers should be used.
    /// </summary>
    public const string SDL_HINT_JOYSTICK_HIDAPI_SHIELD = "SDL_JOYSTICK_HIDAPI_SHIELD";

    /// <summary>
    /// A variable controlling whether the HIDAPI driver for Google Stadia controllers should be used.
    /// </summary>
    public const string SDL_HINT_JOYSTICK_HIDAPI_STADIA = "SDL_JOYSTICK_HIDAPI_STADIA";

    /// <summary>
    /// A variable controlling whether the HIDAPI driver for Bluetooth Steam Controllers should be used.
    /// </summary>
    public const string SDL_HINT_JOYSTICK_HIDAPI_STEAM = "SDL_JOYSTICK_HIDAPI_STEAM";

    /// <summary>
    /// A variable controlling whether the Steam button LED should be turned on when a Steam controller is opened.
    /// </summary>
    public const string SDL_HINT_JOYSTICK_HIDAPI_STEAM_HOME_LED = "SDL_JOYSTICK_HIDAPI_STEAM_HOME_LED";

    /// <summary>
    /// A variable controlling whether the HIDAPI driver for the Steam Deck builtin controller should be used.
    /// </summary>
    public const string SDL_HINT_JOYSTICK_HIDAPI_STEAMDECK = "SDL_JOYSTICK_HIDAPI_STEAMDECK";

    /// <summary>
    /// A variable controlling whether the HIDAPI driver for HORI licensed Steam controllers should be used.
    /// </summary>
    public const string SDL_HINT_JOYSTICK_HIDAPI_STEAM_HORI = "SDL_JOYSTICK_HIDAPI_STEAM_HORI";

    /// <summary>
    /// A variable controlling whether the HIDAPI driver for Nintendo Switch controllers should be used.
    /// </summary>
    public const string SDL_HINT_JOYSTICK_HIDAPI_SWITCH = "SDL_JOYSTICK_HIDAPI_SWITCH";

    /// <summary>
    /// A variable controlling whether the Home button LED should be turned on when a Nintendo Switch Pro controller is opened.
    /// </summary>
    public const string SDL_HINT_JOYSTICK_HIDAPI_SWITCH_HOME_LED = "SDL_JOYSTICK_HIDAPI_SWITCH_HOME_LED";

    /// <summary>
    /// A variable controlling whether the player LEDs should be lit to indicate which player is associated with a Nintendo Switch controller.
    /// </summary>
    public const string SDL_HINT_JOYSTICK_HIDAPI_SWITCH_PLAYER_LED = "SDL_JOYSTICK_HIDAPI_SWITCH_PLAYER_LED";

    /// <summary>
    /// A variable controlling whether Nintendo Switch Joy-Con controllers will be in vertical mode.
    /// </summary>
    public const string SDL_HINT_JOYSTICK_HIDAPI_VERTICAL_JOY_CONS = "SDL_JOYSTICK_HIDAPI_VERTICAL_JOY_CONS";

    /// <summary>
    /// A variable controlling whether the HIDAPI driver for Nintendo Wii and Wii U controllers should be used.
    /// </summary>
    public const string SDL_HINT_JOYSTICK_HIDAPI_WII = "SDL_JOYSTICK_HIDAPI_WII";

    /// <summary>
    /// A variable controlling whether the player LEDs should be lit to indicate which player is associated with a Wii controller.
    /// </summary>
    public const string SDL_HINT_JOYSTICK_HIDAPI_WII_PLAYER_LED = "SDL_JOYSTICK_HIDAPI_WII_PLAYER_LED";

    /// <summary>
    /// A variable controlling whether the HIDAPI driver for XBox controllers should be used.
    /// </summary>
    public const string SDL_HINT_JOYSTICK_HIDAPI_XBOX = "SDL_JOYSTICK_HIDAPI_XBOX";

    /// <summary>
    /// A variable controlling whether the HIDAPI driver for XBox 360 controllers should be used.
    /// </summary>
    public const string SDL_HINT_JOYSTICK_HIDAPI_XBOX_360 = "SDL_JOYSTICK_HIDAPI_XBOX_360";

    /// <summary>
    /// A variable controlling whether the player LEDs should be lit to indicate which player is associated with an Xbox 360 controller.
    /// </summary>
    public const string SDL_HINT_JOYSTICK_HIDAPI_XBOX_360_PLAYER_LED = "SDL_JOYSTICK_HIDAPI_XBOX_360_PLAYER_LED";

    /// <summary>
    /// A variable controlling whether the HIDAPI driver for XBox 360 wireless controllers should be used.
    /// </summary>
    public const string SDL_HINT_JOYSTICK_HIDAPI_XBOX_360_WIRELESS = "SDL_JOYSTICK_HIDAPI_XBOX_360_WIRELESS";

    /// <summary>
    /// A variable controlling whether the HIDAPI driver for XBox One controllers should be used.
    /// </summary>
    public const string SDL_HINT_JOYSTICK_HIDAPI_XBOX_ONE = "SDL_JOYSTICK_HIDAPI_XBOX_ONE";

    /// <summary>
    /// A variable controlling whether the Home button LED should be turned on when an Xbox One controller is opened.
    /// </summary>
    public const string SDL_HINT_JOYSTICK_HIDAPI_XBOX_ONE_HOME_LED = "SDL_JOYSTICK_HIDAPI_XBOX_ONE_HOME_LED";

    /// <summary>
    /// A variable controlling whether IOKit should be used for controller handling.
    /// </summary>
    public const string SDL_HINT_JOYSTICK_IOKIT = "SDL_JOYSTICK_IOKIT";

    /// <summary>
    /// A variable controlling whether to use the classic /dev/input/js* joystick interface or the newer /dev/input/event* interface on Linux.
    /// </summary>
    public const string SDL_HINT_JOYSTICK_LINUX_CLASSIC = "SDL_JOYSTICK_LINUX_CLASSIC";

    /// <summary>
    /// A variable controlling whether joysticks on Linux adhere to their HID-defined deadzones.
    /// </summary>
    public const string SDL_HINT_JOYSTICK_LINUX_DEADZONES = "SDL_JOYSTICK_LINUX_DEADZONES";

    /// <summary>
    /// A variable controlling whether joysticks on Linux will always treat 'hat' axis inputs as 8-way digital hats.
    /// </summary>
    public const string SDL_HINT_JOYSTICK_LINUX_DIGITAL_HATS = "SDL_JOYSTICK_LINUX_DIGITAL_HATS";

    /// <summary>
    /// A variable controlling whether digital hats on Linux will apply deadzones to their underlying input axes.
    /// </summary>
    public const string SDL_HINT_JOYSTICK_LINUX_HAT_DEADZONES = "SDL_JOYSTICK_LINUX_HAT_DEADZONES";

    /// <summary>
    /// A variable controlling whether GCController should be used for controller handling.
    /// </summary>
    public const string SDL_HINT_JOYSTICK_MFI = "SDL_JOYSTICK_MFI";

    /// <summary>
    /// A variable controlling whether the RAWINPUT joystick drivers should be used.
    /// </summary>
    public const string SDL_HINT_JOYSTICK_RAWINPUT = "SDL_JOYSTICK_RAWINPUT";

    /// <summary>
    /// A variable controlling whether the RAWINPUT driver should pull correlated data from XInput.
    /// </summary>
    public const string SDL_HINT_JOYSTICK_RAWINPUT_CORRELATE_XINPUT = "SDL_JOYSTICK_RAWINPUT_CORRELATE_XINPUT";

    /// <summary>
    /// A variable controlling whether the ROG Chakram mice should show up as joysticks.
    /// </summary>
    public const string SDL_HINT_JOYSTICK_ROG_CHAKRAM = "SDL_JOYSTICK_ROG_CHAKRAM";

    /// <summary>
    /// A variable controlling whether a separate thread should be used for handling joystick detection and raw input messages on Windows.
    /// </summary>
    public const string SDL_HINT_JOYSTICK_THREAD = "SDL_JOYSTICK_THREAD";

    /// <summary>
    /// A variable containing a list of throttle style controllers.
    /// </summary>
    public const string SDL_HINT_JOYSTICK_THROTTLE_DEVICES = "SDL_JOYSTICK_THROTTLE_DEVICES";

    /// <summary>
    /// A variable containing a list of devices that are not throttle style controllers.
    /// </summary>
    public const string SDL_HINT_JOYSTICK_THROTTLE_DEVICES_EXCLUDED = "SDL_JOYSTICK_THROTTLE_DEVICES_EXCLUDED";

    /// <summary>
    /// A variable controlling whether Windows.Gaming.Input should be used for controller handling.
    /// </summary>
    public const string SDL_HINT_JOYSTICK_WGI = "SDL_JOYSTICK_WGI";

    /// <summary>
    /// A variable containing a list of wheel style controllers.
    /// </summary>
    public const string SDL_HINT_JOYSTICK_WHEEL_DEVICES = "SDL_JOYSTICK_WHEEL_DEVICES";

    /// <summary>
    /// A variable containing a list of devices that are not wheel style controllers.
    /// </summary>
    public const string SDL_HINT_JOYSTICK_WHEEL_DEVICES_EXCLUDED = "SDL_JOYSTICK_WHEEL_DEVICES_EXCLUDED";

    /// <summary>
    /// A variable containing a list of devices known to have all axes centered at zero.
    /// </summary>
    public const string SDL_HINT_JOYSTICK_ZERO_CENTERED_DEVICES = "SDL_JOYSTICK_ZERO_CENTERED_DEVICES";

    /// <summary>
    /// A variable containing a list of devices and their desired number of haptic enabled axis.
    /// </summary>
    public const string SDL_HINT_JOYSTICK_HAPTIC_AXES = "SDL_JOYSTICK_HAPTIC_AXES";

    /// <summary>
    /// A variable that controls keycode representation in keyboard events.
    /// </summary>
    public const string SDL_HINT_KEYCODE_OPTIONS = "SDL_KEYCODE_OPTIONS";

    /// <summary>
    /// A variable that controls what KMSDRM device to use.
    /// </summary>
    public const string SDL_HINT_KMSDRM_DEVICE_INDEX = "SDL_KMSDRM_DEVICE_INDEX";

    /// <summary>
    /// A variable that controls whether SDL requires DRM master access in order to initialize the KMSDRM video backend.
    /// </summary>
    public const string SDL_HINT_KMSDRM_REQUIRE_DRM_MASTER = "SDL_KMSDRM_REQUIRE_DRM_MASTER";

    /// <summary>
    /// A variable controlling the default SDL log levels.
    /// </summary>
    public const string SDL_HINT_LOGGING = "SDL_LOGGING";

    /// <summary>
    /// A variable controlling whether to force the application to become the foreground process when launched on macOS.
    /// </summary>
    public const string SDL_HINT_MAC_BACKGROUND_APP = "SDL_MAC_BACKGROUND_APP";

    /// <summary>
    /// A variable that determines whether Ctrl+Click should generate a right-click event on macOS.
    /// </summary>
    public const string SDL_HINT_MAC_CTRL_CLICK_EMULATE_RIGHT_CLICK = "SDL_MAC_CTRL_CLICK_EMULATE_RIGHT_CLICK";

    /// <summary>
    /// A variable controlling whether dispatching OpenGL context updates should block the dispatching thread on macOS.
    /// </summary>
    public const string SDL_HINT_MAC_OPENGL_ASYNC_DISPATCH = "SDL_MAC_OPENGL_ASYNC_DISPATCH";

    /// <summary>
    /// A variable controlling whether the Option key on macOS should be remapped to act as the Alt key.
    /// </summary>
    public const string SDL_HINT_MAC_OPTION_AS_ALT = "SDL_MAC_OPTION_AS_ALT";

    /// <summary>
    /// A variable controlling whether SDL_EVENT_MOUSE_WHEEL event values will have momentum on macOS.
    /// </summary>
    public const string SDL_HINT_MAC_SCROLL_MOMENTUM = "SDL_MAC_SCROLL_MOMENTUM";

    /// <summary>
    /// Request SDL_AppIterate() be called at a specific rate.
    /// </summary>
    public const string SDL_HINT_MAIN_CALLBACK_RATE = "SDL_MAIN_CALLBACK_RATE";

    /// <summary>
    /// A variable controlling whether the mouse is captured while mouse buttons are pressed.
    /// </summary>
    public const string SDL_HINT_MOUSE_AUTO_CAPTURE = "SDL_MOUSE_AUTO_CAPTURE";

    /// <summary>
    /// A variable setting the double click radius, in pixels.
    /// </summary>
    public const string SDL_HINT_MOUSE_DOUBLE_CLICK_RADIUS = "SDL_MOUSE_DOUBLE_CLICK_RADIUS";

    /// <summary>
    /// A variable setting the double click time, in milliseconds.
    /// </summary>
    public const string SDL_HINT_MOUSE_DOUBLE_CLICK_TIME = "SDL_MOUSE_DOUBLE_CLICK_TIME";

    /// <summary>
    /// A variable setting which system cursor to use as the default cursor.
    /// </summary>
    public const string SDL_HINT_MOUSE_DEFAULT_SYSTEM_CURSOR = "SDL_MOUSE_DEFAULT_SYSTEM_CURSOR";

    /// <summary>
    /// A variable controlling whether warping a hidden mouse cursor will activate relative mouse mode.
    /// </summary>
    public const string SDL_HINT_MOUSE_EMULATE_WARP_WITH_RELATIVE = "SDL_MOUSE_EMULATE_WARP_WITH_RELATIVE";

    /// <summary>
    /// Allow mouse click events when clicking to focus an SDL window.
    /// </summary>
    public const string SDL_HINT_MOUSE_FOCUS_CLICKTHROUGH = "SDL_MOUSE_FOCUS_CLICKTHROUGH";

    /// <summary>
    /// A variable setting the speed scale for mouse motion, in floating point, when the mouse is not in relative mode.
    /// </summary>
    public const string SDL_HINT_MOUSE_NORMAL_SPEED_SCALE = "SDL_MOUSE_NORMAL_SPEED_SCALE";

    /// <summary>
    /// A variable controlling whether relative mouse mode constrains the mouse to the center of the window.
    /// </summary>
    public const string SDL_HINT_MOUSE_RELATIVE_MODE_CENTER = "SDL_MOUSE_RELATIVE_MODE_CENTER";

    /// <summary>
    /// A variable setting the scale for mouse motion, in floating point, when the mouse is in relative mode.
    /// </summary>
    public const string SDL_HINT_MOUSE_RELATIVE_SPEED_SCALE = "SDL_MOUSE_RELATIVE_SPEED_SCALE";

    /// <summary>
    /// A variable controlling whether the system mouse acceleration curve is used for relative mouse motion.
    /// </summary>
    public const string SDL_HINT_MOUSE_RELATIVE_SYSTEM_SCALE = "SDL_MOUSE_RELATIVE_SYSTEM_SCALE";

    /// <summary>
    /// A variable controlling whether a motion event should be generated for mouse warping in relative mode.
    /// </summary>
    public const string SDL_HINT_MOUSE_RELATIVE_WARP_MOTION = "SDL_MOUSE_RELATIVE_WARP_MOTION";

    /// <summary>
    /// A variable controlling whether the hardware cursor stays visible when relative mode is active.
    /// </summary>
    public const string SDL_HINT_MOUSE_RELATIVE_CURSOR_VISIBLE = "SDL_MOUSE_RELATIVE_CURSOR_VISIBLE";

    /// <summary>
    /// A variable controlling whether mouse events should generate synthetic touch events.
    /// </summary>
    public const string SDL_HINT_MOUSE_TOUCH_EVENTS = "SDL_MOUSE_TOUCH_EVENTS";

    /// <summary>
    /// A variable controlling whether the keyboard should be muted on the console.
    /// </summary>
    public const string SDL_HINT_MUTE_CONSOLE_KEYBOARD = "SDL_MUTE_CONSOLE_KEYBOARD";

    /// <summary>
    /// Tell SDL not to catch the SIGINT or SIGTERM signals on POSIX platforms.
    /// </summary>
    public const string SDL_HINT_NO_SIGNAL_HANDLERS = "SDL_NO_SIGNAL_HANDLERS";

    /// <summary>
    /// Specify the OpenGL library to load.
    /// </summary>
    public const string SDL_HINT_OPENGL_LIBRARY = "SDL_OPENGL_LIBRARY";

    /// <summary>
    /// Specify the EGL library to load.
    /// </summary>
    public const string SDL_HINT_EGL_LIBRARY = "SDL_EGL_LIBRARY";

    /// <summary>
    /// A variable controlling what driver to use for OpenGL ES contexts.
    /// </summary>
    public const string SDL_HINT_OPENGL_ES_DRIVER = "SDL_OPENGL_ES_DRIVER";

    /// <summary>
    /// Mechanism to specify openvr_api library location.
    /// </summary>
    public const string SDL_HINT_OPENVR_LIBRARY = "SDL_OPENVR_LIBRARY";

    /// <summary>
    /// A variable controlling which orientations are allowed on iOS/Android.
    /// </summary>
    public const string SDL_HINT_ORIENTATIONS = "SDL_ORIENTATIONS";

    /// <summary>
    /// A variable controlling the use of a sentinel event when polling the event queue.
    /// </summary>
    public const string SDL_HINT_POLL_SENTINEL = "SDL_POLL_SENTINEL";

    /// <summary>
    /// Override for SDL_GetPreferredLocales().
    /// </summary>
    public const string SDL_HINT_PREFERRED_LOCALES = "SDL_PREFERRED_LOCALES";

    /// <summary>
    /// A variable that decides whether to send SDL_EVENT_QUIT when closing the last window.
    /// </summary>
    public const string SDL_HINT_QUIT_ON_LAST_WINDOW_CLOSE = "SDL_QUIT_ON_LAST_WINDOW_CLOSE";

    /// <summary>
    /// A variable controlling whether the Direct3D device is initialized for thread-safe operations.
    /// </summary>
    public const string SDL_HINT_RENDER_DIRECT3D_THREADSAFE = "SDL_RENDER_DIRECT3D_THREADSAFE";

    /// <summary>
    /// A variable controlling whether to enable Direct3D 11+'s Debug Layer.
    /// </summary>
    public const string SDL_HINT_RENDER_DIRECT3D11_DEBUG = "SDL_RENDER_DIRECT3D11_DEBUG";

    /// <summary>
    /// A variable controlling whether to enable Vulkan Validation Layers.
    /// </summary>
    public const string SDL_HINT_RENDER_VULKAN_DEBUG = "SDL_RENDER_VULKAN_DEBUG";

    /// <summary>
    /// A variable controlling whether to create the GPU device in debug mode.
    /// </summary>
    public const string SDL_HINT_RENDER_GPU_DEBUG = "SDL_RENDER_GPU_DEBUG";

    /// <summary>
    /// A variable controlling whether to prefer a low-power GPU on multi-GPU systems.
    /// </summary>
    public const string SDL_HINT_RENDER_GPU_LOW_POWER = "SDL_RENDER_GPU_LOW_POWER";

    /// <summary>
    /// A variable specifying which render driver to use.
    /// </summary>
    public const string SDL_HINT_RENDER_DRIVER = "SDL_RENDER_DRIVER";

    /// <summary>
    /// A variable controlling how the 2D render API renders lines.
    /// </summary>
    public const string SDL_HINT_RENDER_LINE_METHOD = "SDL_RENDER_LINE_METHOD";

    /// <summary>
    /// A variable controlling whether the Metal render driver select low power device over default one.
    /// </summary>
    public const string SDL_HINT_RENDER_METAL_PREFER_LOW_POWER_DEVICE = "SDL_RENDER_METAL_PREFER_LOW_POWER_DEVICE";

    /// <summary>
    /// A variable controlling whether updates to the SDL screen surface should be synchronized with the vertical refresh.
    /// </summary>
    public const string SDL_HINT_RENDER_VSYNC = "SDL_RENDER_VSYNC";

    /// <summary>
    /// A variable to control whether the return key on the soft keyboard should hide the soft keyboard on Android and iOS.
    /// </summary>
    public const string SDL_HINT_RETURN_KEY_HIDES_IME = "SDL_RETURN_KEY_HIDES_IME";

    /// <summary>
    /// A variable containing a list of ROG gamepad capable mice.
    /// </summary>
    public const string SDL_HINT_ROG_GAMEPAD_MICE = "SDL_ROG_GAMEPAD_MICE";

    /// <summary>
    /// A variable containing a list of devices that are not ROG gamepad capable mice.
    /// </summary>
    public const string SDL_HINT_ROG_GAMEPAD_MICE_EXCLUDED = "SDL_ROG_GAMEPAD_MICE_EXCLUDED";

    /// <summary>
    /// A variable controlling which Dispmanx layer to use on a Raspberry PI.
    /// </summary>
    public const string SDL_HINT_RPI_VIDEO_LAYER = "SDL_RPI_VIDEO_LAYER";

    /// <summary>
    /// Specify an "activity name" for screensaver inhibition.
    /// </summary>
    public const string SDL_HINT_SCREENSAVER_INHIBIT_ACTIVITY_NAME = "SDL_SCREENSAVER_INHIBIT_ACTIVITY_NAME";

    /// <summary>
    /// A variable controlling whether SDL calls dbus_shutdown() on quit.
    /// </summary>
    public const string SDL_HINT_SHUTDOWN_DBUS_ON_QUIT = "SDL_SHUTDOWN_DBUS_ON_QUIT";

    /// <summary>
    /// A variable that specifies a backend to use for title storage.
    /// </summary>
    public const string SDL_HINT_STORAGE_TITLE_DRIVER = "SDL_STORAGE_TITLE_DRIVER";

    /// <summary>
    /// A variable that specifies a backend to use for user storage.
    /// </summary>
    public const string SDL_HINT_STORAGE_USER_DRIVER = "SDL_STORAGE_USER_DRIVER";

    /// <summary>
    /// Specifies whether SDL_THREAD_PRIORITY_TIME_CRITICAL should be treated as realtime.
    /// </summary>
    public const string SDL_HINT_THREAD_FORCE_REALTIME_TIME_CRITICAL = "SDL_THREAD_FORCE_REALTIME_TIME_CRITICAL";

    /// <summary>
    /// A string specifying additional information to use with SDL_SetCurrentThreadPriority.
    /// </summary>
    public const string SDL_HINT_THREAD_PRIORITY_POLICY = "SDL_THREAD_PRIORITY_POLICY";

    /// <summary>
    /// A variable that controls the timer resolution, in milliseconds.
    /// </summary>
    public const string SDL_HINT_TIMER_RESOLUTION = "SDL_TIMER_RESOLUTION";

    /// <summary>
    /// A variable controlling whether touch events should generate synthetic mouse events.
    /// </summary>
    public const string SDL_HINT_TOUCH_MOUSE_EVENTS = "SDL_TOUCH_MOUSE_EVENTS";

    /// <summary>
    /// A variable controlling whether trackpads should be treated as touch devices.
    /// </summary>
    public const string SDL_HINT_TRACKPAD_IS_TOUCH_ONLY = "SDL_TRACKPAD_IS_TOUCH_ONLY";

    /// <summary>
    /// A variable controlling whether the Android / tvOS remotes should be listed as joystick devices.
    /// </summary>
    public const string SDL_HINT_TV_REMOTE_AS_JOYSTICK = "SDL_TV_REMOTE_AS_JOYSTICK";

    /// <summary>
    /// A variable controlling whether the screensaver is enabled.
    /// </summary>
    public const string SDL_HINT_VIDEO_ALLOW_SCREENSAVER = "SDL_VIDEO_ALLOW_SCREENSAVER";

    /// <summary>
    /// A comma separated list containing the names of the displays that SDL should sort to the front of the display list.
    /// </summary>
    public const string SDL_HINT_VIDEO_DISPLAY_PRIORITY = "SDL_VIDEO_DISPLAY_PRIORITY";

    /// <summary>
    /// Tell the video driver that we only want a double buffer.
    /// </summary>
    public const string SDL_HINT_VIDEO_DOUBLE_BUFFER = "SDL_VIDEO_DOUBLE_BUFFER";

    /// <summary>
    /// A variable that specifies a video backend to use.
    /// </summary>
    public const string SDL_HINT_VIDEO_DRIVER = "SDL_VIDEO_DRIVER";

    /// <summary>
    /// A variable controlling whether the dummy video driver saves output frames.
    /// </summary>
    public const string SDL_HINT_VIDEO_DUMMY_SAVE_FRAMES = "SDL_VIDEO_DUMMY_SAVE_FRAMES";

    /// <summary>
    /// If eglGetPlatformDisplay fails, fall back to calling eglGetDisplay.
    /// </summary>
    public const string SDL_HINT_VIDEO_EGL_ALLOW_GETDISPLAY_FALLBACK = "SDL_VIDEO_EGL_ALLOW_GETDISPLAY_FALLBACK";

    /// <summary>
    /// A variable controlling whether the OpenGL context should be created with EGL.
    /// </summary>
    public const string SDL_HINT_VIDEO_FORCE_EGL = "SDL_VIDEO_FORCE_EGL";

    /// <summary>
    /// A variable that specifies the policy for fullscreen Spaces on macOS.
    /// </summary>
    public const string SDL_HINT_VIDEO_MAC_FULLSCREEN_SPACES = "SDL_VIDEO_MAC_FULLSCREEN_SPACES";

    /// <summary>
    /// A variable that specifies the menu visibility when a window is fullscreen in Spaces on macOS.
    /// </summary>
    public const string SDL_HINT_VIDEO_MAC_FULLSCREEN_MENU_VISIBILITY = "SDL_VIDEO_MAC_FULLSCREEN_MENU_VISIBILITY";

    /// <summary>
    /// A variable controlling whether fullscreen windows are minimized when they lose focus.
    /// </summary>
    public const string SDL_HINT_VIDEO_MINIMIZE_ON_FOCUS_LOSS = "SDL_VIDEO_MINIMIZE_ON_FOCUS_LOSS";

    /// <summary>
    /// A variable controlling whether the offscreen video driver saves output frames.
    /// </summary>
    public const string SDL_HINT_VIDEO_OFFSCREEN_SAVE_FRAMES = "SDL_VIDEO_OFFSCREEN_SAVE_FRAMES";

    /// <summary>
    /// A variable controlling whether all window operations will block until complete.
    /// </summary>
    public const string SDL_HINT_VIDEO_SYNC_WINDOW_OPERATIONS = "SDL_VIDEO_SYNC_WINDOW_OPERATIONS";

    /// <summary>
    /// A variable controlling whether the libdecor Wayland backend is allowed to be used.
    /// </summary>
    public const string SDL_HINT_VIDEO_WAYLAND_ALLOW_LIBDECOR = "SDL_VIDEO_WAYLAND_ALLOW_LIBDECOR";

    /// <summary>
    /// A variable controlling whether video mode emulation is enabled under Wayland.
    /// </summary>
    public const string SDL_HINT_VIDEO_WAYLAND_MODE_EMULATION = "SDL_VIDEO_WAYLAND_MODE_EMULATION";

    /// <summary>
    /// A variable controlling how modes with a non-native aspect ratio are displayed under Wayland.
    /// </summary>
    public const string SDL_HINT_VIDEO_WAYLAND_MODE_SCALING = "SDL_VIDEO_WAYLAND_MODE_SCALING";

    /// <summary>
    /// A variable controlling whether the libdecor Wayland backend is preferred over native decorations.
    /// </summary>
    public const string SDL_HINT_VIDEO_WAYLAND_PREFER_LIBDECOR = "SDL_VIDEO_WAYLAND_PREFER_LIBDECOR";

    /// <summary>
    /// A variable forcing non-DPI-aware Wayland windows to output at 1:1 scaling.
    /// </summary>
    public const string SDL_HINT_VIDEO_WAYLAND_SCALE_TO_DISPLAY = "SDL_VIDEO_WAYLAND_SCALE_TO_DISPLAY";

    /// <summary>
    /// A variable specifying which shader compiler to preload when using the Chrome ANGLE binaries.
    /// </summary>
    public const string SDL_HINT_VIDEO_WIN_D3DCOMPILER = "SDL_VIDEO_WIN_D3DCOMPILER";

    /// <summary>
    /// A variable controlling whether SDL should call XSelectInput() to enable input events on X11 windows wrapped by SDL.
    /// </summary>
    public const string SDL_HINT_VIDEO_X11_EXTERNAL_WINDOW_INPUT = "SDL_VIDEO_X11_EXTERNAL_WINDOW_INPUT";

    /// <summary>
    /// A variable controlling whether the X11 _NET_WM_BYPASS_COMPOSITOR hint should be used.
    /// </summary>
    public const string SDL_HINT_VIDEO_X11_NET_WM_BYPASS_COMPOSITOR = "SDL_VIDEO_X11_NET_WM_BYPASS_COMPOSITOR";

    /// <summary>
    /// A variable controlling whether the X11 _NET_WM_PING protocol should be supported.
    /// </summary>
    public const string SDL_HINT_VIDEO_X11_NET_WM_PING = "SDL_VIDEO_X11_NET_WM_PING";

    /// <summary>
    /// A variable controlling whether SDL uses DirectColor visuals.
    /// </summary>
    public const string SDL_HINT_VIDEO_X11_NODIRECTCOLOR = "SDL_VIDEO_X11_NODIRECTCOLOR";

    /// <summary>
    /// A variable forcing the content scaling factor for X11 displays.
    /// </summary>
    public const string SDL_HINT_VIDEO_X11_SCALING_FACTOR = "SDL_VIDEO_X11_SCALING_FACTOR";

    /// <summary>
    /// A variable forcing the visual ID used for X11 display modes.
    /// </summary>
    public const string SDL_HINT_VIDEO_X11_VISUALID = "SDL_VIDEO_X11_VISUALID";

    /// <summary>
    /// A variable forcing the visual ID chosen for new X11 windows.
    /// </summary>
    public const string SDL_HINT_VIDEO_X11_WINDOW_VISUALID = "SDL_VIDEO_X11_WINDOW_VISUALID";

    /// <summary>
    /// A variable controlling whether the X11 XRandR extension should be used.
    /// </summary>
    public const string SDL_HINT_VIDEO_X11_XRANDR = "SDL_VIDEO_X11_XRANDR";

    /// <summary>
    /// A variable controlling whether touch should be enabled on the back panel of the PlayStation Vita.
    /// </summary>
    public const string SDL_HINT_VITA_ENABLE_BACK_TOUCH = "SDL_VITA_ENABLE_BACK_TOUCH";

    /// <summary>
    /// A variable controlling whether touch should be enabled on the front panel of the PlayStation Vita.
    /// </summary>
    public const string SDL_HINT_VITA_ENABLE_FRONT_TOUCH = "SDL_VITA_ENABLE_FRONT_TOUCH";

    /// <summary>
    /// A variable controlling the module path on the PlayStation Vita.
    /// </summary>
    public const string SDL_HINT_VITA_MODULE_PATH = "SDL_VITA_MODULE_PATH";

    /// <summary>
    /// A variable controlling whether to perform PVR initialization on the PlayStation Vita.
    /// </summary>
    public const string SDL_HINT_VITA_PVR_INIT = "SDL_VITA_PVR_INIT";

    /// <summary>
    /// A variable overriding the resolution reported on the PlayStation Vita.
    /// </summary>
    public const string SDL_HINT_VITA_RESOLUTION = "SDL_VITA_RESOLUTION";

    /// <summary>
    /// A variable controlling whether OpenGL should be used instead of OpenGL ES on the PlayStation Vita.
    /// </summary>
    public const string SDL_HINT_VITA_PVR_OPENGL = "SDL_VITA_PVR_OPENGL";

    /// <summary>
    /// A variable controlling which touchpad should generate synthetic mouse events.
    /// </summary>
    public const string SDL_HINT_VITA_TOUCH_MOUSE_DEVICE = "SDL_VITA_TOUCH_MOUSE_DEVICE";

    /// <summary>
    /// A variable overriding the display index used in SDL_Vulkan_CreateSurface().
    /// </summary>
    public const string SDL_HINT_VULKAN_DISPLAY = "SDL_VULKAN_DISPLAY";

    /// <summary>
    /// Specify the Vulkan library to load.
    /// </summary>
    public const string SDL_HINT_VULKAN_LIBRARY = "SDL_VULKAN_LIBRARY";

    /// <summary>
    /// A variable controlling how the fact chunk affects the loading of a WAVE file.
    /// </summary>
    public const string SDL_HINT_WAVE_FACT_CHUNK = "SDL_WAVE_FACT_CHUNK";

    /// <summary>
    /// A variable controlling the maximum number of chunks in a WAVE file.
    /// </summary>
    public const string SDL_HINT_WAVE_CHUNK_LIMIT = "SDL_WAVE_CHUNK_LIMIT";

    /// <summary>
    /// A variable controlling how the size of the RIFF chunk affects the loading of a WAVE file.
    /// </summary>
    public const string SDL_HINT_WAVE_RIFF_CHUNK_SIZE = "SDL_WAVE_RIFF_CHUNK_SIZE";

    /// <summary>
    /// A variable controlling how a truncated WAVE file is handled.
    /// </summary>
    public const string SDL_HINT_WAVE_TRUNCATION = "SDL_WAVE_TRUNCATION";

    /// <summary>
    /// A variable controlling whether the window is activated when the SDL_RaiseWindow function is called.
    /// </summary>
    public const string SDL_HINT_WINDOW_ACTIVATE_WHEN_RAISED = "SDL_WINDOW_ACTIVATE_WHEN_RAISED";

    /// <summary>
    /// A variable controlling whether the window is activated when the SDL_ShowWindow function is called.
    /// </summary>
    public const string SDL_HINT_WINDOW_ACTIVATE_WHEN_SHOWN = "SDL_WINDOW_ACTIVATE_WHEN_SHOWN";

    /// <summary>
    /// If set to "0" then never set the top-most flag on an SDL Window even if the application requests it.
    /// </summary>
    public const string SDL_HINT_WINDOW_ALLOW_TOPMOST = "SDL_WINDOW_ALLOW_TOPMOST";

    /// <summary>
    /// A variable controlling whether the window frame and title bar are interactive when the cursor is hidden.
    /// </summary>
    public const string SDL_HINT_WINDOW_FRAME_USABLE_WHILE_CURSOR_HIDDEN = "SDL_WINDOW_FRAME_USABLE_WHILE_CURSOR_HIDDEN";

    /// <summary>
    /// A variable controlling whether SDL generates window-close events for Alt+F4 on Windows.
    /// </summary>
    public const string SDL_HINT_WINDOWS_CLOSE_ON_ALT_F4 = "SDL_WINDOWS_CLOSE_ON_ALT_F4";

    /// <summary>
    /// A variable controlling whether menus can be opened with their keyboard shortcut (Alt+mnemonic).
    /// </summary>
    public const string SDL_HINT_WINDOWS_ENABLE_MENU_MNEMONICS = "SDL_WINDOWS_ENABLE_MENU_MNEMONICS";

    /// <summary>
    /// A variable controlling whether the windows message loop is processed by SDL.
    /// </summary>
    public const string SDL_HINT_WINDOWS_ENABLE_MESSAGELOOP = "SDL_WINDOWS_ENABLE_MESSAGELOOP";

    /// <summary>
    /// A variable controlling whether GameInput is used for raw keyboard and mouse on Windows.
    /// </summary>
    public const string SDL_HINT_WINDOWS_GAMEINPUT = "SDL_WINDOWS_GAMEINPUT";

    /// <summary>
    /// A variable controlling whether raw keyboard events are used on Windows.
    /// </summary>
    public const string SDL_HINT_WINDOWS_RAW_KEYBOARD = "SDL_WINDOWS_RAW_KEYBOARD";

    /// <summary>
    /// A variable controlling whether SDL uses Kernel Semaphores on Windows.
    /// </summary>
    public const string SDL_HINT_WINDOWS_FORCE_SEMAPHORE_KERNEL = "SDL_WINDOWS_FORCE_SEMAPHORE_KERNEL";

    /// <summary>
    /// A variable to specify custom icon resource id from RC file on Windows platform.
    /// </summary>
    public const string SDL_HINT_WINDOWS_INTRESOURCE_ICON = "SDL_WINDOWS_INTRESOURCE_ICON";

    /// <summary>
    /// A variable to specify custom small icon resource id from RC file on Windows platform.
    /// </summary>
    public const string SDL_HINT_WINDOWS_INTRESOURCE_ICON_SMALL = "SDL_WINDOWS_INTRESOURCE_ICON_SMALL";

    /// <summary>
    /// A variable controlling whether SDL uses the D3D9Ex API introduced in Windows Vista.
    /// </summary>
    public const string SDL_HINT_WINDOWS_USE_D3D9EX = "SDL_WINDOWS_USE_D3D9EX";

    /// <summary>
    /// A variable controlling whether SDL will clear the window contents when the WM_ERASEBKGND message is received.
    /// </summary>
    public const string SDL_HINT_WINDOWS_ERASE_BACKGROUND_MODE = "SDL_WINDOWS_ERASE_BACKGROUND_MODE";

    /// <summary>
    /// A variable controlling whether X11 windows are marked as override-redirect.
    /// </summary>
    public const string SDL_HINT_X11_FORCE_OVERRIDE_REDIRECT = "SDL_X11_FORCE_OVERRIDE_REDIRECT";

    /// <summary>
    /// A variable specifying the type of an X11 window.
    /// </summary>
    public const string SDL_HINT_X11_WINDOW_TYPE = "SDL_X11_WINDOW_TYPE";

    /// <summary>
    /// Specify the XCB library to load for the X11 driver.
    /// </summary>
    public const string SDL_HINT_X11_XCB_LIBRARY = "SDL_X11_XCB_LIBRARY";

    /// <summary>
    /// A variable controlling whether XInput should be used for controller handling.
    /// </summary>
    public const string SDL_HINT_XINPUT_ENABLED = "SDL_XINPUT_ENABLED";

    /// <summary>
    /// A variable controlling response to SDL_assert failures.
    /// </summary>
    public const string SDL_HINT_ASSERT = "SDL_ASSERT";

    /// <summary>
    /// A variable controlling whether pen events should generate synthetic mouse events.
    /// </summary>
    public const string SDL_HINT_PEN_MOUSE_EVENTS = "SDL_PEN_MOUSE_EVENTS";

    /// <summary>
    /// A variable controlling whether pen events should generate synthetic touch events.
    /// </summary>
    public const string SDL_HINT_PEN_TOUCH_EVENTS = "SDL_PEN_TOUCH_EVENTS";
}
