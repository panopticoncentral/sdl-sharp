namespace SdlSharp
{
    /// <summary>
    /// A hint is a setting that can change SDL behavior.
    /// </summary>
    public sealed class Hint
    {
        /// <summary>
        /// SDL_FRAMEBUFFER_ACCELERATION hint
        /// </summary>
        public static readonly Hint FramebufferAcceleration = new("SDL_FRAMEBUFFER_ACCELERATION");

        /// <summary>
        /// SDL_RENDER_DRIVER hint
        /// </summary>
        public static readonly Hint RenderDriver = new("SDL_RENDER_DRIVER");

        /// <summary>
        /// SDL_RENDER_OPENGL_SHADERS hint
        /// </summary>
        public static readonly Hint RenderOpenGlShaders = new("SDL_RENDER_OPENGL_SHADERS");

        /// <summary>
        /// SDL_RENDER_DIRECT3D_THREADSAFE hint
        /// </summary>
        public static readonly Hint RenderDirect3dThreadsafe = new("SDL_RENDER_DIRECT3D_THREADSAFE");

        /// <summary>
        /// SDL_RENDER_DIRECT3D11_DEBUG hint
        /// </summary>
        public static readonly Hint RenderDirect3d11Debug = new("SDL_RENDER_DIRECT3D11_DEBUG");

        /// <summary>
        /// SDL_RENDER_LOGICAL_SIZE_MODE hint
        /// </summary>
        public static readonly Hint RenderLogicalSizeMode = new("SDL_RENDER_LOGICAL_SIZE_MODE");

        /// <summary>
        /// SDL_RENDER_SCALE_QUALITY hint
        /// </summary>
        public static readonly Hint RenderScaleQuality = new("SDL_RENDER_SCALE_QUALITY");

        /// <summary>
        /// SDL_RENDER_VSYNC hint
        /// </summary>
        public static readonly Hint RenderVsync = new("SDL_RENDER_VSYNC");

        /// <summary>
        /// SDL_VIDEO_ALLOW_SCREENSAVER hint
        /// </summary>
        public static readonly Hint VideoAllowScreensaver = new("SDL_VIDEO_ALLOW_SCREENSAVER");

        /// <summary>
        /// SDL_VIDEO_EXTERNAL_CONTEXT hint
        /// </summary>
        public static readonly Hint VideoExternalContext = new("SDL_VIDEO_EXTERNAL_CONTEXT");

        /// <summary>
        /// SDL_VIDEO_X11_XVIDMODE hint
        /// </summary>
        public static readonly Hint VideoX11XVidMode = new("SDL_VIDEO_X11_XVIDMODE");

        /// <summary>
        /// SDL_VIDEO_X11_XINERAMA hint
        /// </summary>
        public static readonly Hint VideoX11Xinerama = new("SDL_VIDEO_X11_XINERAMA");

        /// <summary>
        /// SDL_VIDEO_X11_XRANDR hint
        /// </summary>
        public static readonly Hint VideoX11XrandR = new("SDL_VIDEO_X11_XRANDR");

        /// <summary>
        /// SDL_VIDEO_X11_WINDOW_VISUALID hint
        /// </summary>
        public static readonly Hint VideoX11WindowVisualId = new("SDL_VIDEO_X11_WINDOW_VISUALID");

        /// <summary>
        /// SDL_VIDEO_X11_NET_WM_PING hint
        /// </summary>
        public static readonly Hint VideoX11NetWmPing = new("SDL_VIDEO_X11_NET_WM_PING");

        /// <summary>
        /// SDL_VIDEO_X11_NET_WM_BYPASS_COMPOSITOR hint
        /// </summary>
        public static readonly Hint VideoX11NetWmBypassCompositor = new("SDL_VIDEO_X11_NET_WM_BYPASS_COMPOSITOR");

        /// <summary>
        /// SDL_VIDEO_X11_FORCE_EGL hint
        /// </summary>
        public static readonly Hint VideoX11ForceEgl = new("SDL_VIDEO_X11_FORCE_EGL");

        /// <summary>
        /// SDL_WINDOW_FRAME_USABLE_WHILE_CURSOR_HIDDEN hint
        /// </summary>
        public static readonly Hint WindowFrameUsableWhileCursorHidden = new("SDL_WINDOW_FRAME_USABLE_WHILE_CURSOR_HIDDEN");

        /// <summary>
        /// SDL_WINDOWS_INTRESOURCE_ICON hint
        /// </summary>
        public static readonly Hint WindowsIntResourceIcon = new("SDL_WINDOWS_INTRESOURCE_ICON");

        /// <summary>
        /// SDL_WINDOWS_INTRESOURCE_ICON_SMALL hint
        /// </summary>
        public static readonly Hint WindowsIntResourceIconSmall = new("SDL_WINDOWS_INTRESOURCE_ICON_SMALL");

        /// <summary>
        /// SDL_WINDOWS_ENABLE_MESSAGELOOP hint
        /// </summary>
        public static readonly Hint WindowsEnableMessageLoop = new("SDL_WINDOWS_ENABLE_MESSAGELOOP");

        /// <summary>
        /// SDL_GRAB_KEYBOARD hint
        /// </summary>
        public static readonly Hint GrabKeyboard = new("SDL_GRAB_KEYBOARD");

        /// <summary>
        /// SDL_MOUSE_DOUBLE_CLICK_TIME hint
        /// </summary>
        public static readonly Hint MouseDoubleClickTime = new("SDL_MOUSE_DOUBLE_CLICK_TIME");

        /// <summary>
        /// SDL_MOUSE_DOUBLE_CLICK_RADIUS hint
        /// </summary>
        public static readonly Hint MouseDoubleClickRadius = new("SDL_MOUSE_DOUBLE_CLICK_RADIUS");

        /// <summary>
        /// SDL_MOUSE_NORMAL_SPEED_SCALE hint
        /// </summary>
        public static readonly Hint MouseNormalSpeedScale = new("SDL_MOUSE_NORMAL_SPEED_SCALE");

        /// <summary>
        /// SDL_MOUSE_RELATIVE_SPEED_SCALE hint
        /// </summary>
        public static readonly Hint MouseRelativeSpeedScale = new("SDL_MOUSE_RELATIVE_SPEED_SCALE");

        /// <summary>
        /// SDL_MOUSE_RELATIVE_MODE_WARP hint
        /// </summary>
        public static readonly Hint MouseRelativeModeWarp = new("SDL_MOUSE_RELATIVE_MODE_WARP");

        /// <summary>
        /// SDL_MOUSE_FOCUS_CLICKTHROUGH hint
        /// </summary>
        public static readonly Hint MouseFocusClickthrough = new("SDL_MOUSE_FOCUS_CLICKTHROUGH");

        /// <summary>
        /// SDL_TOUCH_MOUSE_EVENTS hint
        /// </summary>
        public static readonly Hint TouchMouseEvents = new("SDL_TOUCH_MOUSE_EVENTS");

        /// <summary>
        /// SDL_MOUSE_TOUCH_EVENTS hint
        /// </summary>
        public static readonly Hint MouseTouchEvents = new("SDL_MOUSE_TOUCH_EVENTS");

        /// <summary>
        /// SDL_VIDEO_MINIMIZE_ON_FOCUS_LOSS hint
        /// </summary>
        public static readonly Hint VideoMinimizeOnFocusLoss = new("SDL_VIDEO_MINIMIZE_ON_FOCUS_LOSS");

        /// <summary>
        /// SDL_IOS_IDLE_TIMER_DISABLED hint
        /// </summary>
        public static readonly Hint IosIdleTimerDisabled = new("SDL_IOS_IDLE_TIMER_DISABLED");

        /// <summary>
        /// SDL_IOS_ORIENTATIONS hint
        /// </summary>
        public static readonly Hint IosOrientations = new("SDL_IOS_ORIENTATIONS");

        /// <summary>
        /// SDL_APPLE_TV_CONTROLLER_UI_EVENTS hint
        /// </summary>
        public static readonly Hint AppleTvControllerUiEvents = new("SDL_APPLE_TV_CONTROLLER_UI_EVENTS");

        /// <summary>
        /// SDL_APPLE_TV_REMOTE_ALLOW_ROTATION hint
        /// </summary>
        public static readonly Hint AppleTvRemoteAllowRotation = new("SDL_APPLE_TV_REMOTE_ALLOW_ROTATION");

        /// <summary>
        /// SDL_IOS_HIDE_HOME_INDICATOR hint
        /// </summary>
        public static readonly Hint IosHideHomeIndicator = new("SDL_IOS_HIDE_HOME_INDICATOR");

        /// <summary>
        /// SDL_ACCELEROMETER_AS_JOYSTICK hint
        /// </summary>
        public static readonly Hint AccelerometerAsJoystick = new("SDL_ACCELEROMETER_AS_JOYSTICK");

        /// <summary>
        /// SDL_TV_REMOTE_AS_JOYSTICK hint
        /// </summary>
        public static readonly Hint TvRemoteAsJoystick = new("SDL_TV_REMOTE_AS_JOYSTICK");

        /// <summary>
        /// SDL_XINPUT_ENABLED hint
        /// </summary>
        public static readonly Hint XinputEnabled = new("SDL_XINPUT_ENABLED");

        /// <summary>
        /// SDL_XINPUT_USE_OLD_JOYSTICK_MAPPING hint
        /// </summary>
        public static readonly Hint XInputUseOldJoystickMapping = new("SDL_XINPUT_USE_OLD_JOYSTICK_MAPPING");

        /// <summary>
        /// SDL_GAMECONTROLLERTYPE hint
        /// </summary>
        public static readonly Hint GameControllerType = new("SDL_GAMECONTROLLERTYPE");

        /// <summary>
        /// SDL_GAMECONTROLLERCONFIG hint
        /// </summary>
        public static readonly Hint GameControllerConfig = new("SDL_GAMECONTROLLERCONFIG");

        /// <summary>
        /// SDL_GAMECONTROLLERCONFIG_FILE hint
        /// </summary>
        public static readonly Hint GameControllerConfigFile = new("SDL_GAMECONTROLLERCONFIG_FILE");

        /// <summary>
        /// SDL_GAMECONTROLLER_IGNORE_DEVICES hint
        /// </summary>
        public static readonly Hint GameControllerIgnoreDevices = new("SDL_GAMECONTROLLER_IGNORE_DEVICES");

        /// <summary>
        /// SDL_GAMECONTROLLER_IGNORE_DEVICES_EXCEPT hint
        /// </summary>
        public static readonly Hint GameControllerIgnoreDevicesExcept = new("SDL_GAMECONTROLLER_IGNORE_DEVICES_EXCEPT");

        /// <summary>
        /// SDL_GAMECONTROLLER_USE_BUTTON_LABELS hint
        /// </summary>
        public static readonly Hint GameControllerUseButtonLabels = new("SDL_GAMECONTROLLER_USE_BUTTON_LABELS");

        /// <summary>
        /// SDL_JOYSTICK_ALLOW_BACKGROUND_EVENTS hint
        /// </summary>
        public static readonly Hint JoystickAllowBackgroundEvents = new("SDL_JOYSTICK_ALLOW_BACKGROUND_EVENTS");

        /// <summary>
        /// SDL_JOYSTICK_HIDAPI hint
        /// </summary>
        public static readonly Hint JoystickHidApi = new("SDL_JOYSTICK_HIDAPI");

        /// <summary>
        /// SDL_JOYSTICK_HIDAPI_PS4 hint
        /// </summary>
        public static readonly Hint JoystickHidApiPs4 = new("SDL_JOYSTICK_HIDAPI_PS4");

        /// <summary>
        /// SDL_JOYSTICK_HIDAPI_PS4_RUMBLE hint
        /// </summary>
        public static readonly Hint JoystickHidApiPs4Rumble = new("SDL_JOYSTICK_HIDAPI_PS4_RUMBLE");

        /// <summary>
        /// SDL_JOYSTICK_HIDAPI_STEAM hint
        /// </summary>
        public static readonly Hint JoystickHidApiSteam = new("SDL_JOYSTICK_HIDAPI_STEAM");

        /// <summary>
        /// SDL_JOYSTICK_HIDAPI_SWITCH hint
        /// </summary>
        public static readonly Hint JoystickHidApiSwitch = new("SDL_JOYSTICK_HIDAPI_SWITCH");

        /// <summary>
        /// SDL_JOYSTICK_HIDAPI_XBOX hint
        /// </summary>
        public static readonly Hint JoystickHidApiXbox = new("SDL_JOYSTICK_HIDAPI_XBOX");

        /// <summary>
        /// SDL_JOYSTICK_HIDAPI_GAMECUBE hint
        /// </summary>
        public static readonly Hint JoystickHidApiGameCube = new("SDL_JOYSTICK_HIDAPI_GAMECUBE");

        /// <summary>
        /// SDL_ENABLE_STEAM_CONTROLLERS hint
        /// </summary>
        public static readonly Hint EnableSteamControllers = new("SDL_ENABLE_STEAM_CONTROLLERS");

        /// <summary>
        /// SDL_ALLOW_TOPMOST hint
        /// </summary>
        public static readonly Hint AllowTopmost = new("SDL_ALLOW_TOPMOST");

        /// <summary>
        /// SDL_TIMER_RESOLUTION hint
        /// </summary>
        public static readonly Hint TimerResolution = new("SDL_TIMER_RESOLUTION");

        /// <summary>
        /// SDL_QTWAYLAND_CONTENT_ORIENTATION hint
        /// </summary>
        public static readonly Hint QtwaylandContentOrientation = new("SDL_QTWAYLAND_CONTENT_ORIENTATION");

        /// <summary>
        /// SDL_QTWAYLAND_WINDOW_FLAGS hint
        /// </summary>
        public static readonly Hint QtwaylandWindowFlags = new("SDL_QTWAYLAND_WINDOW_FLAGS");

        /// <summary>
        /// SDL_THREAD_STACK_SIZE hint
        /// </summary>
        public static readonly Hint ThreadStackSize = new("SDL_THREAD_STACK_SIZE");

        /// <summary>
        /// SDL_VIDEO_HIGHDPI_DISABLED hint
        /// </summary>
        public static readonly Hint VideoHighDpiDisabled = new("SDL_VIDEO_HIGHDPI_DISABLED");

        /// <summary>
        /// SDL_MAC_CTRL_CLICK_EMULATE_RIGHT_CLICK hint
        /// </summary>
        public static readonly Hint MacCtrlClickEmulateRightClick = new("SDL_MAC_CTRL_CLICK_EMULATE_RIGHT_CLICK");

        /// <summary>
        /// SDL_VIDEO_WIN_D3DCOMPILER hint
        /// </summary>
        public static readonly Hint VideoWinD3dCompiler = new("SDL_VIDEO_WIN_D3DCOMPILER");

        /// <summary>
        /// SDL_VIDEO_WINDOW_SHARE_PIXEL_FORMAT hint
        /// </summary>
        public static readonly Hint VideoWindowSharePixelFormat = new("SDL_VIDEO_WINDOW_SHARE_PIXEL_FORMAT");

        /// <summary>
        /// SDL_WINRT_PRIVACY_POLICY_URL hint
        /// </summary>
        public static readonly Hint WinrtPrivacyPolicyUrl = new("SDL_WINRT_PRIVACY_POLICY_URL");

        /// <summary>
        /// SDL_WINRT_PRIVACY_POLICY_LABEL hint
        /// </summary>
        public static readonly Hint WinrtPrivacyPolicyLabel = new("SDL_WINRT_PRIVACY_POLICY_LABEL");

        /// <summary>
        /// SDL_WINRT_HANDLE_BACK_BUTTON hint
        /// </summary>
        public static readonly Hint WinrtHandleBackButton = new("SDL_WINRT_HANDLE_BACK_BUTTON");

        /// <summary>
        /// SDL_VIDEO_MAC_FULLSCREEN_SPACES hint
        /// </summary>
        public static readonly Hint VideoMacFullscreenSpaces = new("SDL_VIDEO_MAC_FULLSCREEN_SPACES");

        /// <summary>
        /// SDL_MAC_BACKGROUND_APP hint
        /// </summary>
        public static readonly Hint MacBackgroundApp = new("SDL_MAC_BACKGROUND_APP");

        /// <summary>
        /// SDL_ANDROID_APK_EXPANSION_MAIN_FILE_VERSION hint
        /// </summary>
        public static readonly Hint AndroidApkExpansionMainFileVersion = new("SDL_ANDROID_APK_EXPANSION_MAIN_FILE_VERSION");

        /// <summary>
        /// SDL_ANDROID_APK_EXPANSION_PATCH_FILE_VERSION hint
        /// </summary>
        public static readonly Hint AndroidApkExpansionPatchFileVersion = new("SDL_ANDROID_APK_EXPANSION_PATCH_FILE_VERSION");

        /// <summary>
        /// SDL_IME_INTERNAL_EDITING hint
        /// </summary>
        public static readonly Hint ImeInternalEditing = new("SDL_IME_INTERNAL_EDITING");

        /// <summary>
        /// SDL_ANDROID_TRAP_BACK_BUTTON hint
        /// </summary>
        public static readonly Hint AndroidTrapBackButton = new("SDL_ANDROID_TRAP_BACK_BUTTON");

        /// <summary>
        /// SDL_ANDROID_BLOCK_ON_PAUSE hint
        /// </summary>
        public static readonly Hint AndroidBlockOnPause = new("SDL_ANDROID_BLOCK_ON_PAUSE");

        /// <summary>
        /// SDL_RETURN_KEY_HIDES_IME hint
        /// </summary>
        public static readonly Hint ReturnKeyHidesIme = new("SDL_RETURN_KEY_HIDES_IME");

        /// <summary>
        /// SDL_EMSCRIPTEN_KEYBOARD_ELEMENT hint
        /// </summary>
        public static readonly Hint EmscriptenKeyboardElement = new("SDL_EMSCRIPTEN_KEYBOARD_ELEMENT");

        /// <summary>
        /// SDL_NO_SIGNAL_HANDLERS hint
        /// </summary>
        public static readonly Hint NoSignalHandlers = new("SDL_NO_SIGNAL_HANDLERS");

        /// <summary>
        /// SDL_WINDOWS_NO_CLOSE_ON_ALT_F4 hint
        /// </summary>
        public static readonly Hint WindowsNoCloseOnAltF4 = new("SDL_WINDOWS_NO_CLOSE_ON_ALT_F4");

        /// <summary>
        /// SDL_BMP_SAVE_LEGACY_FORMAT hint
        /// </summary>
        public static readonly Hint BmpSaveLegacyFormat = new("SDL_BMP_SAVE_LEGACY_FORMAT");

        /// <summary>
        /// SDL_WINDOWS_DISABLE_THREAD_NAMING hint
        /// </summary>
        public static readonly Hint WindowsDisableThreadNaming = new("SDL_WINDOWS_DISABLE_THREAD_NAMING");

        /// <summary>
        /// SDL_RPI_VIDEO_LAYER hint
        /// </summary>
        public static readonly Hint RpiVideoLayer = new("SDL_RPI_VIDEO_LAYER");

        /// <summary>
        /// SDL_VIDEO_DOUBLE_BUFFER hint
        /// </summary>
        public static readonly Hint VideoDoubleBuffer = new("SDL_VIDEO_DOUBLE_BUFFER");

        /// <summary>
        /// SDL_OPENGL_ES_DRIVER hint
        /// </summary>
        public static readonly Hint OpenglEsDriver = new("SDL_OPENGL_ES_DRIVER");

        /// <summary>
        /// SDL_AUDIO_RESAMPLING_MODE hint
        /// </summary>
        public static readonly Hint AudioResamplingMode = new("SDL_AUDIO_RESAMPLING_MODE");

        /// <summary>
        /// SDL_AUDIO_CATEGORY hint
        /// </summary>
        public static readonly Hint AudioCategory = new("SDL_AUDIO_CATEGORY");

        /// <summary>
        /// SDL_RENDER_BATCHING hint
        /// </summary>
        public static readonly Hint RenderBatching = new("SDL_RENDER_BATCHING");

        /// <summary>
        /// SDL_EVENT_LOGGING hint
        /// </summary>
        public static readonly Hint EventLogging = new("SDL_EVENT_LOGGING");

        /// <summary>
        /// SDL_WAVE_RIFF_CHUNK_SIZE hint
        /// </summary>
        public static readonly Hint WaveRiffChunkSize = new("SDL_WAVE_RIFF_CHUNK_SIZE");

        /// <summary>
        /// SDL_WAVE_TRUNCATION hint
        /// </summary>
        public static readonly Hint WaveTruncation = new("SDL_WAVE_TRUNCATION");

        /// <summary>
        /// SDL_WAVE_FACT_CHUNK hint
        /// </summary>
        public static readonly Hint WaveFactChunk = new("SDL_WAVE_FACT_CHUNK");

        /// <summary>
        /// SDL_DISPLAY_USABLE_BOUNDS hint
        /// </summary>
        public static readonly Hint DisplayUsableBounds = new("SDL_DISPLAY_USABLE_BOUNDS");

        private readonly string _name;
        private Dictionary<HintCallback, int>? _callbacks;

        private Hint(string name)
        {
            _name = name;
        }

        /// <summary>
        /// Sets the hint's valud.
        /// </summary>
        /// <param name="value">The value to set.</param>
        /// <param name="priority">The priority of the value.</param>
        /// <returns><c>true</c> if the hint was set, <c>false</c> otherwise.</returns>
        public bool Set(string value, HintPriority priority) =>
            Native.SDL_SetHintWithPriority(_name, value, priority);

        /// <summary>
        /// Sets the hint's value.
        /// </summary>
        /// <param name="value">The value to set.</param>
        /// <returns><c>true</c> if the hint was set, <c>false</c> otherwise.</returns>
        public bool Set(string value) =>
            Native.SDL_SetHint(_name, value);

        /// <summary>
        /// Get the hint's value.
        /// </summary>
        /// <returns>The hint's value.</returns>
        public string Get() =>
            Native.SDL_GetHint(_name);

        /// <summary>
        /// Gets the value of hint as a Boolean value.
        /// </summary>
        /// <param name="defaultValue">The default value to return if the hint is not set.</param>
        /// <returns>The value of the hint.</returns>
        public bool GetBoolean(bool defaultValue) =>
            Native.SDL_GetHintBoolean(_name, defaultValue);

        /// <summary>
        /// Adds a callback for when the hint is set.
        /// </summary>
        /// <param name="callback">The callback.</param>
        /// <param name="data">Data to pass to the callback.</param>
        public void AddCallback(HintCallback callback, nint data)
        {
            Native.SDL_AddHintCallback(_name, callback, data);

            if (_callbacks == null)
            {
                _callbacks = new Dictionary<HintCallback, int>();
            }

            if (!_callbacks.TryGetValue(callback, out var count))
            {
                count = 1;
            }

            _callbacks[callback] = count;
        }

        /// <summary>
        /// Deletes a callback for when the hint is set.
        /// </summary>
        /// <param name="callback">The callback.</param>
        /// <param name="data">Data to pass to the callback.</param>
        public void DeleteCallback(HintCallback callback, nint data)
        {
            Native.SDL_DelHintCallback(_name, callback, data);

            if (_callbacks != null)
            {
                var count = _callbacks[callback] - 1;
                if (count == 0)
                {
                    _ = _callbacks.Remove(callback);
                }
                else
                {
                    _callbacks[callback] = count;
                }
            }
        }

        /// <summary>
        /// Clears all hint settings.
        /// </summary>
        public static void Clear() =>
            Native.SDL_ClearHints();
    }
}
