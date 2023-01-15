using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SdlSharp
{
    /// <summary>
    /// A hint is a setting that can change SDL behavior.
    /// </summary>
    public sealed unsafe class Hint
    {
        private static readonly Dictionary<string, Hint> s_hints = new();

        private readonly string _name;
        private EventHandler<HintChangedEventArgs>? _hintChanged;

        /// <summary>
        /// A variable controlling whether the Android / iOS built-in accelerometer should be listed as a joystick device.
        /// </summary>
        public static Hint AccelerometerAsJoystick => GetHint(Native.SDL_HINT_ACCELEROMETER_AS_JOYSTICK);

        /// <summary>
        /// Specify the behavior of Alt+Tab while the keyboard is grabbed.
        /// </summary>
        public static Hint AllowAltTabWhileGrabbed => GetHint(Native.SDL_HINT_ALLOW_ALT_TAB_WHILE_GRABBED);

        /// <summary>
        /// If set to "0" then never set the top most bit on a SDL Window, even if the video mode expects it.
        /// </summary>
        public static Hint AllowTopmost => GetHint(Native.SDL_HINT_ALLOW_TOPMOST);

        /// <summary>
        /// Android APK expansion main file version. Should be a string number like "1", "2" etc.
        /// </summary>
        public static Hint AndroidApkExpansionMainFileVersion => GetHint(Native.SDL_HINT_ANDROID_APK_EXPANSION_MAIN_FILE_VERSION);

        /// <summary>
        /// Android APK expansion patch file version. Should be a string number like "1", "2" etc.
        /// </summary>
        public static Hint AndroidApkExpansionPatchFileVersion => GetHint(Native.SDL_HINT_ANDROID_APK_EXPANSION_PATCH_FILE_VERSION);

        /// <summary>
        /// A variable to control whether the event loop will block itself when the app is paused.
        /// </summary>
        public static Hint AndroidBlockOnPause => GetHint(Native.SDL_HINT_ANDROID_BLOCK_ON_PAUSE);

        /// <summary>
        /// A variable to control whether SDL will pause audio in background.
        /// </summary>
        public static Hint AndroidBlockOnPausePauseAudio => GetHint(Native.SDL_HINT_ANDROID_BLOCK_ON_PAUSE_PAUSEAUDIO);

        /// <summary>
        /// A variable to control whether we trap the Android back button to handle it manually.
        /// </summary>
        public static Hint AndroidTrapBackButton => GetHint(Native.SDL_HINT_ANDROID_TRAP_BACK_BUTTON);

        /// <summary>
        /// Specify an application name.
        /// </summary>
        public static Hint AppName => GetHint(Native.SDL_HINT_APP_NAME);

        /// <summary>
        /// A variable controlling whether controllers used with the Apple TV generate UI events.
        /// </summary>
        public static Hint AppleTvControllerUiEvents => GetHint(Native.SDL_HINT_APPLE_TV_CONTROLLER_UI_EVENTS);

        /// <summary>
        /// A variable controlling whether the Apple TV remote's joystick axes will automatically match the rotation of the remote.
        /// </summary>
        public static Hint AppleTvRemoteAllowRotation => GetHint(Native.SDL_HINT_APPLE_TV_REMOTE_ALLOW_ROTATION);

        /// <summary>
        /// A variable controlling the audio category on iOS and Mac OS X.
        /// </summary>
        public static Hint AudioCategory => GetHint(Native.SDL_HINT_AUDIO_CATEGORY);

        /// <summary>
        /// Specify an application name for an audio device.
        /// </summary>
        public static Hint AudioDeviceAppName => GetHint(Native.SDL_HINT_AUDIO_DEVICE_APP_NAME);

        /// <summary>
        /// Specify an application name for an audio device.
        /// </summary>
        public static Hint AudioDeviceStreamName => GetHint(Native.SDL_HINT_AUDIO_DEVICE_STREAM_NAME);

        /// <summary>
        /// Specify an application role for an audio device.
        /// </summary>
        public static Hint AudioDeviceStreamRole => GetHint(Native.SDL_HINT_AUDIO_DEVICE_STREAM_ROLE);

        /// <summary>
        /// A variable controlling speed/quality tradeoff of audio resampling.
        /// </summary>
        public static Hint AudioResamplingMode => GetHint(Native.SDL_HINT_AUDIO_RESAMPLING_MODE);

        /// <summary>
        /// A variable controlling whether SDL updates joystick state when getting input events.
        /// </summary>
        public static Hint AutoUpdateJoysticks => GetHint(Native.SDL_HINT_AUTO_UPDATE_JOYSTICKS);

        /// <summary>
        /// A variable controlling whether SDL updates sensor state when getting input events.
        /// </summary>
        public static Hint AutoUpdateSensors => GetHint(Native.SDL_HINT_AUTO_UPDATE_SENSORS);

        /// <summary>
        /// Prevent SDL from using version 4 of the bitmap header when saving BMPs.
        /// </summary>
        public static Hint BmpSaveLegacyFormat => GetHint(Native.SDL_HINT_BMP_SAVE_LEGACY_FORMAT);

        /// <summary>
        /// Override for SDL_GetDisplayUsableBounds().
        /// </summary>
        public static Hint DisplayUsableBounds => GetHint(Native.SDL_HINT_DISPLAY_USABLE_BOUNDS);

        /// <summary>
        /// Disable giving back control to the browser automatically when running with asyncify.
        /// </summary>
        public static Hint EmscriptemAsyncify => GetHint(Native.SDL_HINT_EMSCRIPTEN_ASYNCIFY);

        /// <summary>
        /// Override the binding element for keyboard inputs for Emscripten builds.
        /// </summary>
        public static Hint EmscriptemKeyboardElement => GetHint(Native.SDL_HINT_EMSCRIPTEN_KEYBOARD_ELEMENT);

        /// <summary>
        /// A variable that controls whether Steam Controllers should be exposed using the SDL joystick and game controller APIs.
        /// </summary>
        public static Hint EnableSteamControllers => GetHint(Native.SDL_HINT_ENABLE_STEAM_CONTROLLERS);

        /// <summary>
        /// A variable controlling verbosity of the logging of SDL events pushed onto the internal queue.
        /// </summary>
        public static Hint EventLogging => GetHint(Native.SDL_HINT_EVENT_LOGGING);

        /// <summary>
        /// A variable controlling whether raising the window should be done more forcefully.
        /// </summary>
        public static Hint ForceRaiseWindow => GetHint(Native.SDL_HINT_FORCE_RAISEWINDOW);

        /// <summary>
        /// A variable controlling how 3D acceleration is used to accelerate the SDL screen surface.
        /// </summary>
        public static Hint FramebufferAcceleration => GetHint(Native.SDL_HINT_FRAMEBUFFER_ACCELERATION);

        /// <summary>
        /// A variable that lets you manually hint extra gamecontroller db entries.
        /// </summary>
        public static Hint GameControllerConfig => GetHint(Native.SDL_HINT_GAMECONTROLLERCONFIG);

        /// <summary>
        /// A variable that lets you provide a file with extra gamecontroller db entries.
        /// </summary>
        public static Hint GameControllerConfigFile => GetHint(Native.SDL_HINT_GAMECONTROLLERCONFIG_FILE);

        /// <summary>
        /// A variable that overrides the automatic controller type detection.
        /// </summary>
        public static Hint GameControllerType => GetHint(Native.SDL_HINT_GAMECONTROLLERTYPE);

        /// <summary>
        /// A variable containing a list of devices to skip when scanning for game controllers.
        /// </summary>
        public static Hint GameControllerIgnoreDevices => GetHint(Native.SDL_HINT_GAMECONTROLLER_IGNORE_DEVICES);

        /// <summary>
        /// If set, all devices will be skipped when scanning for game controllers except for the ones listed in this variable.
        /// </summary>
        public static Hint GameControllerIgnoreDevicesExcept => GetHint(Native.SDL_HINT_GAMECONTROLLER_IGNORE_DEVICES_EXCEPT);

        /// <summary>
        /// If set, game controller face buttons report their values according to their labels instead of their positional layout.
        /// </summary>
        public static Hint GameControllerUseButtonLabels => GetHint(Native.SDL_HINT_GAMECONTROLLER_USE_BUTTON_LABELS);

        /// <summary>
        /// A variable controlling whether grabbing input grabs the keyboard.
        /// </summary>
        public static Hint GrabKeyboards => GetHint(Native.SDL_HINT_GRAB_KEYBOARD);

        /// <summary>
        /// A variable containing a list of devices to ignore in SDL_hid_enumerate().
        /// </summary>
        public static Hint HidApiIgnoreDevices => GetHint(Native.SDL_HINT_HIDAPI_IGNORE_DEVICES);

        /// <summary>
        /// A variable controlling whether the idle timer is disabled on iOS.
        /// </summary>
        public static Hint IdleTimerDisabled => GetHint(Native.SDL_HINT_IDLE_TIMER_DISABLED);

        /// <summary>
        /// A variable to control whether certain IMEs should handle text editing internally instead of sending SDL_TEXTEDITING events.
        /// </summary>
        public static Hint ImeInternalEditing => GetHint(Native.SDL_HINT_IME_INTERNAL_EDITING);

        /// <summary>
        /// A variable to control whether certain IMEs should show native UI components (such as the Candidate List) instead of suppressing them.
        /// </summary>
        public static Hint ImeShowUi => GetHint(Native.SDL_HINT_IME_SHOW_UI);

        /// <summary>
        /// A variable to control if extended IME text support is enabled.
        /// </summary>
        public static Hint ImeSupportExtendedText => GetHint(Native.SDL_HINT_IME_SUPPORT_EXTENDED_TEXT);

        /// <summary>
        /// A variable controlling whether the home indicator bar on iPhone X should be hidden.
        /// </summary>
        public static Hint IosHideHomeIndicator => GetHint(Native.SDL_HINT_IOS_HIDE_HOME_INDICATOR);

        /// <summary>
        /// A variable that lets you enable joystick (and gamecontroller) events even when your app is in the background.
        /// </summary>
        public static Hint JoystickAllowBackgroundEvents => GetHint(Native.SDL_HINT_JOYSTICK_ALLOW_BACKGROUND_EVENTS);

        /// <summary>
        /// A variable controlling whether the HIDAPI joystick drivers should be used.
        /// </summary>
        public static Hint JoystickHidApi => GetHint(Native.SDL_HINT_JOYSTICK_HIDAPI);

        /// <summary>
        /// A variable controlling whether the HIDAPI driver for Nintendo GameCube controllers should be used.
        /// </summary>
        public static Hint JoystickHidApiGamecube => GetHint(Native.SDL_HINT_JOYSTICK_HIDAPI_GAMECUBE);

        /// <summary>
        /// A variable controlling whether "low_frequency_rumble" and "high_frequency_rumble" is used to implement
        /// the GameCube controller's 3 rumble modes.
        /// </summary>
        public static Hint JoystickGamecubRumbleBrake => GetHint(Native.SDL_HINT_JOYSTICK_GAMECUBE_RUMBLE_BRAKE);

        /// <summary>
        /// A variable controlling whether the HIDAPI driver for Nintendo Switch Joy-Cons should be used.
        /// </summary>
        public static Hint JoystickHidApiJoyCons => GetHint(Native.SDL_HINT_JOYSTICK_HIDAPI_JOY_CONS);

        /// <summary>
        /// A variable controlling whether Nintendo Switch Joy-Con controllers will be combined into a single Pro-like controller when using the HIDAPI driver.
        /// </summary>
        public static Hint JoystickHidApiCombineJoyCons => GetHint(Native.SDL_HINT_JOYSTICK_HIDAPI_COMBINE_JOY_CONS);

        /// <summary>
        /// A variable controlling whether Nintendo Switch Joy-Con controllers will be in vertical mode when using the HIDAPI driver.
        /// </summary>
        public static Hint JoystickHidApiVerticalJoyCons => GetHint(Native.SDL_HINT_JOYSTICK_HIDAPI_VERTICAL_JOY_CONS);

        /// <summary>
        /// A variable controlling whether the HIDAPI driver for Amazon Luna controllers connected via Bluetooth should be used.
        /// </summary>
        public static Hint JoystickHidApiLuna => GetHint(Native.SDL_HINT_JOYSTICK_HIDAPI_LUNA);

        /// <summary>
        /// A variable controlling whether the HIDAPI driver for Nintendo Online classic controllers should be used.
        /// </summary>
        public static Hint JoystickHidApiNintendoClassic => GetHint(Native.SDL_HINT_JOYSTICK_HIDAPI_NINTENDO_CLASSIC);

        /// <summary>
        /// A variable controlling whether the HIDAPI driver for NVIDIA SHIELD controllers should be used.
        /// </summary>
        public static Hint JoystickHidApiShield => GetHint(Native.SDL_HINT_JOYSTICK_HIDAPI_SHIELD);

        /// <summary>
        /// A variable controlling whether the HIDAPI driver for PS3 controllers should be used.
        /// </summary>
        public static Hint JoystickHidApiPs3 => GetHint(Native.SDL_HINT_JOYSTICK_HIDAPI_PS3);

        /// <summary>
        /// A variable controlling whether the HIDAPI driver for PS4 controllers should be used.
        /// </summary>
        public static Hint JoystickHidApiPs4 => GetHint(Native.SDL_HINT_JOYSTICK_HIDAPI_PS4);

        /// <summary>
        /// A variable controlling whether extended input reports should be used for PS4 controllers when using the HIDAPI driver.
        /// </summary>
        public static Hint JoystickHidApiPs4Rumble => GetHint(Native.SDL_HINT_JOYSTICK_HIDAPI_PS4_RUMBLE);

        /// <summary>
        /// A variable controlling whether the HIDAPI driver for PS5 controllers should be used.
        /// </summary>
        public static Hint JoystickHidApiPs5 => GetHint(Native.SDL_HINT_JOYSTICK_HIDAPI_PS5);

        /// <summary>
        /// A variable controlling whether the player LEDs should be lit to indicate which player is associated with a PS5 controller.
        /// </summary>
        public static Hint JoystickHidApiPs5PlayerLed => GetHint(Native.SDL_HINT_JOYSTICK_HIDAPI_PS5_PLAYER_LED);

        /// <summary>
        /// A variable controlling whether extended input reports should be used for PS5 controllers when using the HIDAPI driver.
        /// </summary>
        public static Hint JoystockHidApiPs5Rumble => GetHint(Native.SDL_HINT_JOYSTICK_HIDAPI_PS5_RUMBLE);

        /// <summary>
        /// A variable controlling whether the HIDAPI driver for Google Stadia controllers should be used.
        /// </summary>
        public static Hint JoystackHidApiStadia => GetHint(Native.SDL_HINT_JOYSTICK_HIDAPI_STADIA);

        /// <summary>
        /// A variable controlling whether the HIDAPI driver for Bluetooth Steam Controllers should be used.
        /// </summary>
        public static Hint JoystickHidApiSteam => GetHint(Native.SDL_HINT_JOYSTICK_HIDAPI_STEAM);

        /// <summary>
        /// A variable controlling whether the HIDAPI driver for Nintendo Switch controllers should be used.
        /// </summary>
        public static Hint JoystickHidApiSwitch => GetHint(Native.SDL_HINT_JOYSTICK_HIDAPI_SWITCH);

        /// <summary>
        /// A variable controlling whether the Home button LED should be turned on when a Nintendo Switch Pro controller is opened.
        /// </summary>
        public static Hint JoystickHidApiSwitchHomeLed => GetHint(Native.SDL_HINT_JOYSTICK_HIDAPI_SWITCH_HOME_LED);

        /// <summary>
        /// A variable controlling whether the Home button LED should be turned on when a Nintendo Switch Joy-Con controller is opened.
        /// </summary>
        public static Hint JoystickHidApiJoyconHomeLed => GetHint(Native.SDL_HINT_JOYSTICK_HIDAPI_JOYCON_HOME_LED);

        /// <summary>
        /// A variable controlling whether the player LEDs should be lit to indicate which player is associated with a Nintendo Switch controller.
        /// </summary>
        public static Hint JoystickHidApiSwitchPlayerLed => GetHint(Native.SDL_HINT_JOYSTICK_HIDAPI_SWITCH_PLAYER_LED);

        /// <summary>
        /// A variable controlling whether the HIDAPI driver for Nintendo Wii and Wii U controllers should be used.
        /// </summary>
        public static Hint JoystickHidApiWii => GetHint(Native.SDL_HINT_JOYSTICK_HIDAPI_WII);

        /// <summary>
        /// A variable controlling whether the player LEDs should be lit to indicate which player is associated with a Wii controller.
        /// </summary>
        public static Hint JoystickHidApiWiiPlayerLed => GetHint(Native.SDL_HINT_JOYSTICK_HIDAPI_WII_PLAYER_LED);

        /// <summary>
        /// A variable controlling whether the HIDAPI driver for XBox controllers should be used.
        /// </summary>
        public static Hint JoystickHidApiXbox => GetHint(Native.SDL_HINT_JOYSTICK_HIDAPI_XBOX);

        /// <summary>
        /// A variable controlling whether the HIDAPI driver for XBox 360 controllers should be used.
        /// </summary>
        public static Hint JoystickHidApiXbox360 => GetHint(Native.SDL_HINT_JOYSTICK_HIDAPI_XBOX_360);

        /// <summary>
        /// A variable controlling whether the player LEDs should be lit to indicate which player is associated with an Xbox 360 controller.
        /// </summary>
        public static Hint JoystickHidApiXbox360PlayerLed => GetHint(Native.SDL_HINT_JOYSTICK_HIDAPI_XBOX_360_PLAYER_LED);

        /// <summary>
        /// A variable controlling whether the HIDAPI driver for XBox 360 wireless controllers should be used.
        /// </summary>
        public static Hint JoystickHidApiXbox360Wireless => GetHint(Native.SDL_HINT_JOYSTICK_HIDAPI_XBOX_360_WIRELESS);

        /// <summary>
        /// A variable controlling whether the HIDAPI driver for XBox One controllers should be used.
        /// </summary>
        public static Hint JoystickHidApiXboxOne => GetHint(Native.SDL_HINT_JOYSTICK_HIDAPI_XBOX_ONE);

        /// <summary>
        /// A variable controlling whether the Home button LED should be turned on when an Xbox One controller is opened.
        /// </summary>
        public static Hint JoystickHidApiXboxOneHomeLed => GetHint(Native.SDL_HINT_JOYSTICK_HIDAPI_XBOX_ONE_HOME_LED);

        /// <summary>
        /// A variable controlling whether the RAWINPUT joystick drivers should be used for better handling XInput-capable devices.
        /// </summary>
        public static Hint JoystickRawInput => GetHint(Native.SDL_HINT_JOYSTICK_RAWINPUT);

        /// <summary>
        /// A variable controlling whether the RAWINPUT driver should pull correlated data from XInput.
        /// </summary>
        public static Hint JoystickRawInputCorrelateXinput => GetHint(Native.SDL_HINT_JOYSTICK_RAWINPUT_CORRELATE_XINPUT);

        /// <summary>
        /// A variable controlling whether the ROG Chakram mice should show up as joysticks.
        /// </summary>
        public static Hint JoystickRogChakram => GetHint(Native.SDL_HINT_JOYSTICK_ROG_CHAKRAM);

        /// <summary>
        /// A variable controlling whether a separate thread should be used.
        /// </summary>
        public static Hint JoystickThread => GetHint(Native.SDL_HINT_JOYSTICK_THREAD);

        /// <summary>
        /// Determines whether SDL enforces that DRM master is required in order to initialize the KMSDRM video backend.
        /// </summary>
        public static Hint KmsDrmRequireDrmMaster => GetHint(Native.SDL_HINT_KMSDRM_REQUIRE_DRM_MASTER);

        /// <summary>
        /// A comma separated list of devices to open as joysticks.
        /// </summary>
        public static Hint JoystickDevice => GetHint(Native.SDL_HINT_JOYSTICK_DEVICE);

        /// <summary>
        /// A variable controlling whether joysticks on Linux will always treat 'hat' axis inputs (ABS_HAT0X - ABS_HAT3Y) as 8-way digital hats without checking whether they may be analog.
        /// </summary>
        public static Hint LinuxDigitalHats => GetHint(Native.SDL_HINT_LINUX_DIGITAL_HATS);

        /// <summary>
        /// A variable controlling whether digital hats on Linux will apply deadzones to their underlying input axes or use unfiltered values.
        /// </summary>
        public static Hint LinuxHatDeadZones => GetHint(Native.SDL_HINT_LINUX_HAT_DEADZONES);

        /// <summary>
        /// A variable controlling whether to use the classic /dev/input/js.
        /// </summary>
        public static Hint LinuxJoystickClassic => GetHint(Native.SDL_HINT_LINUX_JOYSTICK_CLASSIC);

        /// <summary>
        /// A variable controlling whether joysticks on Linux adhere to their HID-defined deadzones or return unfiltered values.
        /// </summary>
        public static Hint LinuxJoystickDeadZones => GetHint(Native.SDL_HINT_LINUX_JOYSTICK_DEADZONES);

        /// <summary>
        /// When set don't force the SDL app to become a foreground process.
        /// </summary>
        public static Hint MacBackgroundApp => GetHint(Native.SDL_HINT_MAC_BACKGROUND_APP);

        /// <summary>
        /// A variable that determines whether ctrl+click should generate a right-click event on Mac.
        /// </summary>
        public static Hint MacCtrlClickEmulateRightClick => GetHint(Native.SDL_HINT_MAC_CTRL_CLICK_EMULATE_RIGHT_CLICK);

        /// <summary>
        /// A variable controlling whether dispatching OpenGL context updates should block the dispatching thread until the main thread finishes processing.
        /// </summary>
        public static Hint MacOpenGlAsyncDispatch => GetHint(Native.SDL_HINT_MAC_OPENGL_ASYNC_DISPATCH);

        /// <summary>
        /// A variable setting the double click radius, in pixels.
        /// </summary>
        public static Hint MouseDoubleClickRadius => GetHint(Native.SDL_HINT_MOUSE_DOUBLE_CLICK_RADIUS);

        /// <summary>
        /// A variable setting the double click time, in milliseconds.
        /// </summary>
        public static Hint MouseDoubleClickTime => GetHint(Native.SDL_HINT_MOUSE_DOUBLE_CLICK_TIME);

        /// <summary>
        /// Allow mouse click events when clicking to focus an SDL window.
        /// </summary>
        public static Hint MouseFocusClickthrough => GetHint(Native.SDL_HINT_MOUSE_FOCUS_CLICKTHROUGH);

        /// <summary>
        /// A variable setting the speed scale for mouse motion, in floating point, when the mouse is not in relative mode.
        /// </summary>
        public static Hint MouseNormalSpeedScale => GetHint(Native.SDL_HINT_MOUSE_NORMAL_SPEED_SCALE);

        /// <summary>
        /// A variable controlling whether relative mouse mode constrains the mouse to the center of the window.
        /// </summary>
        public static Hint MouseRelativeModeCenter => GetHint(Native.SDL_HINT_MOUSE_RELATIVE_MODE_CENTER);

        /// <summary>
        /// A variable controlling whether relative mouse mode is implemented using mouse warping.
        /// </summary>
        public static Hint MouseRelativeModeWarp => GetHint(Native.SDL_HINT_MOUSE_RELATIVE_MODE_WARP);

        /// <summary>
        /// A variable controlling whether relative mouse motion is affected by renderer scaling.
        /// </summary>
        public static Hint MouseRelativeScaling => GetHint(Native.SDL_HINT_MOUSE_RELATIVE_SCALING);

        /// <summary>
        /// A variable setting the scale for mouse motion, in floating point, when the mouse is in relative mode.
        /// </summary>
        public static Hint MouseRelativeSpeedScale => GetHint(Native.SDL_HINT_MOUSE_RELATIVE_SPEED_SCALE);

        /// <summary>
        /// A variable controlling whether the system mouse acceleration curve is used for relative mouse motion.
        /// </summary>
        public static Hint MouseRelativeSystemScale => GetHint(Native.SDL_HINT_MOUSE_RELATIVE_SYSTEM_SCALE);

        /// <summary>
        /// A variable controlling whether a motion event should be generated for mouse warping in relative mode.
        /// </summary>
        public static Hint MouseRelativeWarpMotion => GetHint(Native.SDL_HINT_MOUSE_RELATIVE_WARP_MOTION);

        /// <summary>
        /// A variable controlling whether mouse events should generate synthetic touch events.
        /// </summary>
        public static Hint MouseTouchEvents => GetHint(Native.SDL_HINT_MOUSE_TOUCH_EVENTS);

        /// <summary>
        /// A variable controlling whether the mouse is captured while mouse buttons are pressed.
        /// </summary>
        public static Hint MouseAutoCapture => GetHint(Native.SDL_HINT_MOUSE_AUTO_CAPTURE);

        /// <summary>
        /// Tell SDL not to catch the SIGINT or SIGTERM signals.
        /// </summary>
        public static Hint NoSignalHandlers => GetHint(Native.SDL_HINT_NO_SIGNAL_HANDLERS);

        /// <summary>
        /// A variable controlling what driver to use for OpenGL ES contexts.
        /// </summary>
        public static Hint OpenGlEsDriver => GetHint(Native.SDL_HINT_OPENGL_ES_DRIVER);

        /// <summary>
        /// A variable controlling which orientations are allowed on iOS/Android.
        /// </summary>
        public static Hint Orientations => GetHint(Native.SDL_HINT_ORIENTATIONS);

        /// <summary>
        /// A variable controlling the use of a sentinel event when polling the event queue
        /// </summary>
        public static Hint PollSentinel => GetHint(Native.SDL_HINT_POLL_SENTINEL);

        /// <summary>
        /// Override for SDL_GetPreferredLocales()
        /// </summary>
        public static Hint PreferredLocales => GetHint(Native.SDL_HINT_PREFERRED_LOCALES);

        /// <summary>
        /// A variable describing the content orientation on QtWayland-based platforms.
        /// </summary>
        public static Hint QtWaylandContentOrientation => GetHint(Native.SDL_HINT_QTWAYLAND_CONTENT_ORIENTATION);

        /// <summary>
        /// Flags to set on QtWayland windows to integrate with the native window manager.
        /// </summary>
        public static Hint QtWaylandWindowFlags => GetHint(Native.SDL_HINT_QTWAYLAND_WINDOW_FLAGS);

        /// <summary>
        /// A variable controlling whether the 2D render API is compatible or efficient.
        /// </summary>
        public static Hint RenderBatching => GetHint(Native.SDL_HINT_RENDER_BATCHING);

        /// <summary>
        /// A variable controlling how the 2D render API renders lines
        /// </summary>
        public static Hint RenderLineMethod => GetHint(Native.SDL_HINT_RENDER_LINE_METHOD);

        /// <summary>
        /// A variable controlling whether to enable Direct3D 11+'s Debug Layer.
        /// </summary>
        public static Hint RenderDirect3d11Debugs => GetHint(Native.SDL_HINT_RENDER_DIRECT3D11_DEBUG);

        /// <summary>
        /// A variable controlling whether the Direct3D device is initialized for thread-safe operations.
        /// </summary>
        public static Hint RenderDirect3dThreadSafe => GetHint(Native.SDL_HINT_RENDER_DIRECT3D_THREADSAFE);

        /// <summary>
        /// A variable specifying which render driver to use.
        /// </summary>
        public static Hint RenderDriver => GetHint(Native.SDL_HINT_RENDER_DRIVER);

        /// <summary>
        /// A variable controlling the scaling policy for SDL_RenderSetLogicalSize.
        /// </summary>
        public static Hint RenderLogicalSizeMode => GetHint(Native.SDL_HINT_RENDER_LOGICAL_SIZE_MODE);

        /// <summary>
        /// A variable controlling whether the OpenGL render driver uses shaders if they are available.
        /// </summary>
        public static Hint RenderOpenGlShaders => GetHint(Native.SDL_HINT_RENDER_OPENGL_SHADERS);

        /// <summary>
        /// A variable controlling the scaling quality
        /// </summary>
        public static Hint RenderScaleQuality => GetHint(Native.SDL_HINT_RENDER_SCALE_QUALITY);

        /// <summary>
        /// A variable controlling whether updates to the SDL screen surface should be synchronized with the vertical refresh, to avoid tearing.
        /// </summary>
        public static Hint RenderVsync => GetHint(Native.SDL_HINT_RENDER_VSYNC);

        /// <summary>
        /// A variable controlling if VSYNC is automatically disable if doesn't reach the enough FPS
        /// </summary>
        public static Hint Ps2DynamicVsync => GetHint(Native.SDL_HINT_PS2_DYNAMIC_VSYNC);

        /// <summary>
        /// A variable to control whether the return key on the soft keyboard
        /// </summary>
        public static Hint ReturnKeyHidesIme => GetHint(Native.SDL_HINT_RETURN_KEY_HIDES_IME);

        /// <summary>
        /// Tell SDL which Dispmanx layer to use on a Raspberry PI
        /// </summary>
        public static Hint RpiVideoLayer => GetHint(Native.SDL_HINT_RPI_VIDEO_LAYER);

        /// <summary>
        /// Specify an "activity name" for screensaver inhibition.
        /// </summary>
        public static Hint ScreensaverInhibitActivityName => GetHint(Native.SDL_HINT_SCREENSAVER_INHIBIT_ACTIVITY_NAME);

        /// <summary>
        /// Specifies whether SDL_THREAD_PRIORITY_TIME_CRITICAL should be treated as realtime.
        /// </summary>
        public static Hint ThreadForceRealtimeTimeCritical => GetHint(Native.SDL_HINT_THREAD_FORCE_REALTIME_TIME_CRITICAL);

        /// <summary>
        /// A string specifying additional information to use with SDL_SetThreadPriority.
        /// </summary>
        public static Hint ThreadPriorityPolicy => GetHint(Native.SDL_HINT_THREAD_PRIORITY_POLICY);

        /// <summary>
        /// A string specifying SDL's threads stack size in bytes or "0" for the backend's default size
        /// </summary>
        public static Hint ThreadStackSize => GetHint(Native.SDL_HINT_THREAD_STACK_SIZE);

        /// <summary>
        /// A variable that controls the timer resolution, in milliseconds.
        /// </summary>
        public static Hint TimerResolution => GetHint(Native.SDL_HINT_TIMER_RESOLUTION);

        /// <summary>
        /// A variable controlling whether touch events should generate synthetic mouse events
        /// </summary>
        public static Hint TouchMouseEvents => GetHint(Native.SDL_HINT_TOUCH_MOUSE_EVENTS);

        /// <summary>
        /// A variable controlling which touchpad should generate synthetic mouse events
        /// </summary>
        public static Hint VitaTouchMouseDevice => GetHint(Native.SDL_HINT_VITA_TOUCH_MOUSE_DEVICE);

        /// <summary>
        /// A variable controlling whether the Android / tvOS remotes
        /// </summary>
        public static Hint TvRemoteAsJoystick => GetHint(Native.SDL_HINT_TV_REMOTE_AS_JOYSTICK);

        /// <summary>
        /// A variable controlling whether the screensaver is enabled. 
        /// </summary>
        public static Hint VideoAllowScreensaver => GetHint(Native.SDL_HINT_VIDEO_ALLOW_SCREENSAVER);

        /// <summary>
        /// Tell the video driver that we only want a double buffer.
        /// </summary>
        public static Hint VideoDoubleBuffer => GetHint(Native.SDL_HINT_VIDEO_DOUBLE_BUFFER);

        /// <summary>
        /// A variable controlling whether the EGL window is allowed to be
        /// </summary>
        public static Hint VideoEglAllowTransparency => GetHint(Native.SDL_HINT_VIDEO_EGL_ALLOW_TRANSPARENCY);

        /// <summary>
        /// A variable controlling whether the graphics context is externally managed.
        /// </summary>
        public static Hint VideoExternalContext => GetHint(Native.SDL_HINT_VIDEO_EXTERNAL_CONTEXT);

        /// <summary>
        /// If set to 1, then do not allow high-DPI windows. ("Retina" on Mac and iOS)
        /// </summary>
        public static Hint VideoHighDpiDisabled => GetHint(Native.SDL_HINT_VIDEO_HIGHDPI_DISABLED);

        /// <summary>
        /// A variable that dictates policy for fullscreen Spaces on Mac OS X.
        /// </summary>
        public static Hint VideoMacFullscreenSpaces => GetHint(Native.SDL_HINT_VIDEO_MAC_FULLSCREEN_SPACES);

        /// <summary>
        /// Minimize your SDL_Window if it loses key focus when in fullscreen mode. Defaults to false.
        /// </summary>
        public static Hint VideoMinimizeOnFocusLoss => GetHint(Native.SDL_HINT_VIDEO_MINIMIZE_ON_FOCUS_LOSS);

        /// <summary>
        /// A variable controlling whether the libdecor Wayland backend is allowed to be used.
        /// </summary>
        public static Hint VideoWaylandAllowLibdecor => GetHint(Native.SDL_HINT_VIDEO_WAYLAND_ALLOW_LIBDECOR);

        /// <summary>
        /// A variable controlling whether the libdecor Wayland backend is preferred over native decrations.
        /// </summary>
        public static Hint VideoWaylandPreferLibdecor => GetHint(Native.SDL_HINT_VIDEO_WAYLAND_PREFER_LIBDECOR);

        /// <summary>
        /// A variable controlling whether video mode emulation is enabled under Wayland.
        /// </summary>
        public static Hint VideoWaylandModeEmulation => GetHint(Native.SDL_HINT_VIDEO_WAYLAND_MODE_EMULATION);

        /// <summary>
        /// Enable or disable mouse pointer warp emulation, needed by some older games.
        /// </summary>
        public static Hint VideoWaylandEmulateMouseWarp => GetHint(Native.SDL_HINT_VIDEO_WAYLAND_EMULATE_MOUSE_WARP);

        /// <summary>
        /// A variable that is the address of another SDL_Window* (as a hex string formatted with "%p").
        /// </summary>
        public static Hint VideoWindowSharePixelFormat => GetHint(Native.SDL_HINT_VIDEO_WINDOW_SHARE_PIXEL_FORMAT);

        /// <summary>
        /// When calling SDL_CreateWindowFrom(), make the window compatible with OpenGL.
        /// </summary>
        public static Hint VideoForeignWindowOpenGl => GetHint(Native.SDL_HINT_VIDEO_FOREIGN_WINDOW_OPENGL);

        /// <summary>
        /// When calling SDL_CreateWindowFrom(), make the window compatible with Vulkan.
        /// </summary>
        public static Hint VideoForeignWindowVulkan => GetHint(Native.SDL_HINT_VIDEO_FOREIGN_WINDOW_VULKAN);

        /// <summary>
        /// A variable specifying which shader compiler to preload when using the Chrome ANGLE binaries
        /// </summary>
        public static Hint VideoWinD3dCompiler => GetHint(Native.SDL_HINT_VIDEO_WIN_D3DCOMPILER);

        /// <summary>
        /// A variable controlling whether X11 should use GLX or EGL by default
        /// </summary>
        public static Hint VideoX11ForceEgl => GetHint(Native.SDL_HINT_VIDEO_X11_FORCE_EGL);

        /// <summary>
        /// A variable controlling whether the X11 _NET_WM_BYPASS_COMPOSITOR hint should be used.
        /// </summary>
        public static Hint VideoX11NetWmBypassCompositor => GetHint(Native.SDL_HINT_VIDEO_X11_NET_WM_BYPASS_COMPOSITOR);

        /// <summary>
        /// A variable controlling whether the X11 _NET_WM_PING protocol should be supported.
        /// </summary>
        public static Hint VideoX11NetWmPing => GetHint(Native.SDL_HINT_VIDEO_X11_NET_WM_PING);

        /// <summary>
        /// A variable forcing the visual ID chosen for new X11 windows
        /// </summary>
        public static Hint VideoX11WindowVisualId => GetHint(Native.SDL_HINT_VIDEO_X11_WINDOW_VISUALID);

        /// <summary>
        /// A no-longer-used variable controlling whether the X11 Xinerama extension should be used.
        /// </summary>
        public static Hint VideoX11Xinerama => GetHint(Native.SDL_HINT_VIDEO_X11_XINERAMA);

        /// <summary>
        /// A variable controlling whether the X11 XRandR extension should be used.
        /// </summary>
        public static Hint VideoX11Xrandr => GetHint(Native.SDL_HINT_VIDEO_X11_XRANDR);

        /// <summary>
        /// A no-longer-used variable controlling whether the X11 VidMode extension should be used.
        /// </summary>
        public static Hint VideoX11Xvidmode => GetHint(Native.SDL_HINT_VIDEO_X11_XVIDMODE);

        /// <summary>
        /// Controls how the fact chunk affects the loading of a WAVE file.
        /// </summary>
        public static Hint WaveFactChunk => GetHint(Native.SDL_HINT_WAVE_FACT_CHUNK);

        /// <summary>
        /// Controls how the size of the RIFF chunk affects the loading of a WAVE file.
        /// </summary>
        public static Hint WaveRiffChunkSize => GetHint(Native.SDL_HINT_WAVE_RIFF_CHUNK_SIZE);

        /// <summary>
        /// Controls how a truncated WAVE file is handled.
        /// </summary>
        public static Hint WaveTruncation => GetHint(Native.SDL_HINT_WAVE_TRUNCATION);

        /// <summary>
        /// Tell SDL not to name threads on Windows with the 0x406D1388 Exception.
        /// </summary>
        public static Hint WindowsDisableThreadNaming => GetHint(Native.SDL_HINT_WINDOWS_DISABLE_THREAD_NAMING);

        /// <summary>
        /// A variable controlling whether the windows message loop is processed by SDL 
        /// </summary>
        public static Hint WindowsEnableMessageLoop => GetHint(Native.SDL_HINT_WINDOWS_ENABLE_MESSAGELOOP);

        /// <summary>
        /// Force SDL to use Critical Sections for mutexes on Windows.
        /// </summary>
        public static Hint WindowsForceMutexCriticalSections => GetHint(Native.SDL_HINT_WINDOWS_FORCE_MUTEX_CRITICAL_SECTIONS);

        /// <summary>
        /// Force SDL to use Kernel Semaphores on Windows.
        /// </summary>
        public static Hint WindowsForceSemaphoreKernel => GetHint(Native.SDL_HINT_WINDOWS_FORCE_SEMAPHORE_KERNEL);

        /// <summary>
        /// A variable to specify custom icon resource id from RC file on Windows platform 
        /// </summary>
        public static Hint WindowsIntResourceIcon => GetHint(Native.SDL_HINT_WINDOWS_INTRESOURCE_ICON);

        /// <summary>
        /// A variable to specify custom icon resource id from RC file on Windows platform 
        /// </summary>
        public static Hint WindowsIntResourceIconSmall => GetHint(Native.SDL_HINT_WINDOWS_INTRESOURCE_ICON_SMALL);

        /// <summary>
        /// Tell SDL not to generate window-close events for Alt+F4 on Windows.
        /// </summary>
        public static Hint WindowsNoCloseOnAltF4 => GetHint(Native.SDL_HINT_WINDOWS_NO_CLOSE_ON_ALT_F4);

        /// <summary>
        /// Use the D3D9Ex API introduced in Windows Vista, instead of normal D3D9.
        /// </summary>
        public static Hint WindowsUseD3d9ex => GetHint(Native.SDL_HINT_WINDOWS_USE_D3D9EX);

        /// <summary>
        /// Controls whether SDL will declare the process to be DPI aware.
        /// </summary>
        public static Hint WindowsDpiAwareness => GetHint(Native.SDL_HINT_WINDOWS_DPI_AWARENESS);

        /// <summary>
        /// Uses DPI-scaled points as the SDL coordinate system on Windows.
        /// </summary>
        public static Hint WindowsDpiScaling => GetHint(Native.SDL_HINT_WINDOWS_DPI_SCALING);

        /// <summary>
        /// A variable controlling whether the window frame and title bar are interactive when the cursor is hidden 
        /// </summary>
        public static Hint WindowFrameUsableWhileCursorHidden => GetHint(Native.SDL_HINT_WINDOW_FRAME_USABLE_WHILE_CURSOR_HIDDEN);

        /// <summary>
        /// A variable controlling whether the window is activated when the SDL_ShowWindow function is called 
        /// </summary>
        public static Hint WindowNoActivationWhenShown => GetHint(Native.SDL_HINT_WINDOW_NO_ACTIVATION_WHEN_SHOWN);

        /// <summary>
        /// Allows back-button-press events on Windows Phone to be marked as handled
        /// </summary>
        public static Hint WinrtHandleBackButton => GetHint(Native.SDL_HINT_WINRT_HANDLE_BACK_BUTTON);

        /// <summary>
        /// Label text for a WinRT app's privacy policy link
        /// </summary>
        public static Hint WintrPrivacyPolicyLabel => GetHint(Native.SDL_HINT_WINRT_PRIVACY_POLICY_LABEL);

        /// <summary>
        /// A URL to a WinRT app's privacy policy
        /// </summary>
        public static Hint WinrtPrivatePolicyUrl => GetHint(Native.SDL_HINT_WINRT_PRIVACY_POLICY_URL);

        /// <summary>
        /// Mark X11 windows as override-redirect.
        /// </summary>
        public static Hint X11ForceOverrideRedirect => GetHint(Native.SDL_HINT_X11_FORCE_OVERRIDE_REDIRECT);

        /// <summary>
        /// A variable that lets you disable the detection and use of Xinput gamepad devices
        /// </summary>
        public static Hint XinputEnabled => GetHint(Native.SDL_HINT_XINPUT_ENABLED);

        /// <summary>
        /// A variable that lets you disable the detection and use of DirectInput gamepad devices
        /// </summary>
        public static Hint DirectInputEnabled => GetHint(Native.SDL_HINT_DIRECTINPUT_ENABLED);

        /// <summary>
        /// A variable that causes SDL to use the old axis and button mapping for XInput devices.
        /// </summary>
        public static Hint XinputUseOldJoystickMapping => GetHint(Native.SDL_HINT_XINPUT_USE_OLD_JOYSTICK_MAPPING);

        /// <summary>
        /// A variable that causes SDL to not ignore audio "monitors"
        /// </summary>
        public static Hint AudioIncludeMonitors => GetHint(Native.SDL_HINT_AUDIO_INCLUDE_MONITORS);

        /// <summary>
        /// A variable that forces X11 windows to create as a custom type.
        /// </summary>
        public static Hint X11WindowType => GetHint(Native.SDL_HINT_X11_WINDOW_TYPE);

        /// <summary>
        /// A variable that decides whether to send SDL_QUIT when closing the final window.
        /// </summary>
        public static Hint QuitOnLastWindowClose => GetHint(Native.SDL_HINT_QUIT_ON_LAST_WINDOW_CLOSE);

        /// <summary>
        /// A variable that decides what video backend to use.
        /// </summary>
        public static Hint VideoDriver => GetHint(Native.SDL_HINT_VIDEODRIVER);

        /// <summary>
        /// A variable that decides what audio backend to use.
        /// </summary>
        public static Hint AudioDriver => GetHint(Native.SDL_HINT_AUDIODRIVER);

        /// <summary>
        /// A variable that decides what KMSDRM device to use.
        /// </summary>
        public static Hint KmsdrmDeviceIndex => GetHint(Native.SDL_HINT_KMSDRM_DEVICE_INDEX);

        /// <summary>
        /// A variable that treats trackpads as touch devices.
        /// </summary>
        public static Hint TrackpadIsTouchOnly => GetHint(Native.SDL_HINT_TRACKPAD_IS_TOUCH_ONLY);

        /// <summary>
        /// An event that fires when the hint is changed.
        /// </summary>
        public event EventHandler<HintChangedEventArgs> HintChanged
        {
            add
            {
                lock (this)
                {
                    if (_hintChanged == null)
                    {
                        Native.StringToUtf8Action(_name, namePtr => Native.SDL_AddHintCallback(namePtr, &HintCallback, 0));
                    }

                    _hintChanged += value;
                }
            }

            remove
            {
                lock (this)
                {
                    _hintChanged -= value;

                    if (_hintChanged == null)
                    {
                        Native.StringToUtf8Action(_name, namePtr => Native.SDL_DelHintCallback(namePtr, &HintCallback, 0));
                    }
                }
            }
        }

        private Hint(string name)
        {
            _name = name;
        }

        [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        internal static unsafe void HintCallback(nint _, byte* name, byte* oldValue, byte* newValue)
        {
            var nameString = Native.Utf8ToString(name)!;
            var hint = GetHint(nameString);

            if (hint != null)
            {
                var oldValueString = Native.Utf8ToString(oldValue);
                var newValueString = Native.Utf8ToString(newValue);
                hint._hintChanged?.Invoke(hint, new HintChangedEventArgs(oldValueString, newValueString));
            }
        }

        private static Hint GetHint(string name)
        {
            lock (s_hints)
            {
                if (s_hints.TryGetValue(name, out var hint))
                {
                    return hint;
                }

                hint = new Hint(name);
                s_hints[name] = hint;
                return hint;
            }
        }

        /// <summary>
        /// Sets the hint's valud.
        /// </summary>
        /// <param name="value">The value to set.</param>
        /// <param name="priority">The priority of the value.</param>
        /// <returns><c>true</c> if the hint was set, <c>false</c> otherwise.</returns>
        public bool Set(string value, HintPriority priority) => Native.StringToUtf8Func(_name, value, (namePtr, valuePtr) => Native.SDL_SetHintWithPriority(namePtr, valuePtr, (Native.SDL_HintPriority)priority));

        /// <summary>
        /// Sets the hint's value.
        /// </summary>
        /// <param name="value">The value to set.</param>
        /// <returns><c>true</c> if the hint was set, <c>false</c> otherwise.</returns>
        public bool Set(string value) => Native.StringToUtf8Func(_name, value, Native.SDL_SetHint);

        /// <summary>
        /// Resets the hint's value.
        /// </summary>
        /// <returns><c>true</c> if the hint was reset, <c>false</c> otherwise.</returns>
        public bool Reset() => Native.StringToUtf8Func(_name, Native.SDL_ResetHint);

        /// <summary>
        /// Resets all hints.
        /// </summary>
        public static void ResetHints() => Native.SDL_ResetHints();

        /// <summary>
        /// Get the hint's value.
        /// </summary>
        /// <returns>The hint's value.</returns>
        public string? Get() => Native.StringToUtf8Func(_name, namePtr => Native.Utf8ToString(Native.SDL_GetHint(namePtr)));

        /// <summary>
        /// Gets the value of hint as a Boolean value.
        /// </summary>
        /// <param name="defaultValue">The default value to return if the hint is not set.</param>
        /// <returns>The value of the hint.</returns>
        public bool GetBoolean(bool defaultValue) => Native.StringToUtf8Func(_name, namePtr => Native.SDL_GetHintBoolean(namePtr, defaultValue));

        /// <summary>
        /// Clears all hint settings.
        /// </summary>
        public static void Clear() =>
            Native.SDL_ClearHints();
    }
}
