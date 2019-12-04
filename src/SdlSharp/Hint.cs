using System;
using System.Collections.Generic;

namespace SdlSharp
{
    /// <summary>
    /// A hint is a setting that can change SDL behavior.
    /// </summary>
    public sealed class Hint
    {
        public static readonly Hint FramebufferAcceleration = new Hint("SDL_FRAMEBUFFER_ACCELERATION");
        public static readonly Hint RenderDriver = new Hint("SDL_RENDER_DRIVER");
        public static readonly Hint RenderOpenGlShaders = new Hint("SDL_RENDER_OPENGL_SHADERS");
        public static readonly Hint RenderDirect3dThreadsafe = new Hint("SDL_RENDER_DIRECT3D_THREADSAFE");
        public static readonly Hint RenderDirect3d11Debug = new Hint("SDL_RENDER_DIRECT3D11_DEBUG");
        public static readonly Hint RenderLogicalSizeMode = new Hint("SDL_RENDER_LOGICAL_SIZE_MODE");
        public static readonly Hint RenderScaleQuality = new Hint("SDL_RENDER_SCALE_QUALITY");
        public static readonly Hint RenderVsync = new Hint("SDL_RENDER_VSYNC");
        public static readonly Hint VideoAllowScreensaver = new Hint("SDL_VIDEO_ALLOW_SCREENSAVER");
        public static readonly Hint VideoX11XVidMode = new Hint("SDL_VIDEO_X11_XVIDMODE");
        public static readonly Hint VideoX11Xinerama = new Hint("SDL_VIDEO_X11_XINERAMA");
        public static readonly Hint VideoX11XrandR = new Hint("SDL_VIDEO_X11_XRANDR");
        public static readonly Hint VideoX11NetWmPing = new Hint("SDL_VIDEO_X11_NET_WM_PING");
        public static readonly Hint VideoX11NetWmBypassCompositor = new Hint("SDL_VIDEO_X11_NET_WM_BYPASS_COMPOSITOR");
        public static readonly Hint WindowFrameUsableWhileCursorHidden = new Hint("SDL_WINDOW_FRAME_USABLE_WHILE_CURSOR_HIDDEN");
        public static readonly Hint WindowsIntResourceIcon = new Hint("SDL_WINDOWS_INTRESOURCE_ICON");
        public static readonly Hint WindowsIntResourceIconSmall = new Hint("SDL_WINDOWS_INTRESOURCE_ICON_SMALL");
        public static readonly Hint WindowsEnableMessageLoop = new Hint("SDL_WINDOWS_ENABLE_MESSAGELOOP");
        public static readonly Hint GrabKeyboard = new Hint("SDL_GRAB_KEYBOARD");
        public static readonly Hint MouseDoubleClickTime = new Hint("SDL_MOUSE_DOUBLE_CLICK_TIME");
        public static readonly Hint MouseDoubleClickRadius = new Hint("SDL_MOUSE_DOUBLE_CLICK_RADIUS");
        public static readonly Hint MouseNormalSpeedScale = new Hint("SDL_MOUSE_NORMAL_SPEED_SCALE");
        public static readonly Hint MouseRelativeSpeedScale = new Hint("SDL_MOUSE_RELATIVE_SPEED_SCALE");
        public static readonly Hint MouseRelativeModeWarp = new Hint("SDL_MOUSE_RELATIVE_MODE_WARP");
        public static readonly Hint MouseFocusClickthrough = new Hint("SDL_MOUSE_FOCUS_CLICKTHROUGH");
        public static readonly Hint TouchMouseEvents = new Hint("SDL_TOUCH_MOUSE_EVENTS");
        public static readonly Hint MouseTouchEvents = new Hint("SDL_MOUSE_TOUCH_EVENTS");
        public static readonly Hint VideoMinimizeOnFocusLoss = new Hint("SDL_VIDEO_MINIMIZE_ON_FOCUS_LOSS");
        public static readonly Hint IosIdleTimerDisabled = new Hint("SDL_IOS_IDLE_TIMER_DISABLED");
        public static readonly Hint IosOrientations = new Hint("SDL_IOS_ORIENTATIONS");
        public static readonly Hint AppleTvControllerUiEvents = new Hint("SDL_APPLE_TV_CONTROLLER_UI_EVENTS");
        public static readonly Hint AppleTvRemoteAllowRotation = new Hint("SDL_APPLE_TV_REMOTE_ALLOW_ROTATION");
        public static readonly Hint IosHideHomeIndicator = new Hint("SDL_IOS_HIDE_HOME_INDICATOR");
        public static readonly Hint AccelerometerAsJoystick = new Hint("SDL_ACCELEROMETER_AS_JOYSTICK");
        public static readonly Hint TvRemoteAsJoystick = new Hint("SDL_TV_REMOTE_AS_JOYSTICK");
        public static readonly Hint XinputEnabled = new Hint("SDL_XINPUT_ENABLED");
        public static readonly Hint XInputUseOldJoystickMapping = new Hint("SDL_XINPUT_USE_OLD_JOYSTICK_MAPPING");
        public static readonly Hint GameControllerConfig = new Hint("SDL_GAMECONTROLLERCONFIG");
        public static readonly Hint GameControllerConfigFile = new Hint("SDL_GAMECONTROLLERCONFIG_FILE");
        public static readonly Hint GameControllerIgnoreDevices = new Hint("SDL_GAMECONTROLLER_IGNORE_DEVICES");
        public static readonly Hint GameControllerIgnoreDevicesExcept = new Hint("SDL_GAMECONTROLLER_IGNORE_DEVICES_EXCEPT");
        public static readonly Hint JoystickAllowBackgroundEvents = new Hint("SDL_JOYSTICK_ALLOW_BACKGROUND_EVENTS");
        public static readonly Hint JoystickHidApi = new Hint("SDL_JOYSTICK_HIDAPI");
        public static readonly Hint JoystickHidApiPs4 = new Hint("SDL_JOYSTICK_HIDAPI_PS4");
        public static readonly Hint JoystickHidApiPs4Rumble = new Hint("SDL_JOYSTICK_HIDAPI_PS4_RUMBLE");
        public static readonly Hint JoystickHidApiSteam = new Hint("SDL_JOYSTICK_HIDAPI_STEAM");
        public static readonly Hint JoystickHidApiSwitch = new Hint("SDL_JOYSTICK_HIDAPI_SWITCH");
        public static readonly Hint JoystickHidApiXbox = new Hint("SDL_JOYSTICK_HIDAPI_XBOX");
        public static readonly Hint EnableSteamControllers = new Hint("SDL_ENABLE_STEAM_CONTROLLERS");
        public static readonly Hint AllowTopmost = new Hint("SDL_ALLOW_TOPMOST");
        public static readonly Hint TimerResolution = new Hint("SDL_TIMER_RESOLUTION");
        public static readonly Hint QtwaylandContentOrientation = new Hint("SDL_QTWAYLAND_CONTENT_ORIENTATION");
        public static readonly Hint QtwaylandWindowFlags = new Hint("SDL_QTWAYLAND_WINDOW_FLAGS");
        public static readonly Hint ThreadStackSize = new Hint("SDL_THREAD_STACK_SIZE");
        public static readonly Hint VideoHighDpiDisabled = new Hint("SDL_VIDEO_HIGHDPI_DISABLED");
        public static readonly Hint MacCtrlClickEmulateRightClick = new Hint("SDL_MAC_CTRL_CLICK_EMULATE_RIGHT_CLICK");
        public static readonly Hint VideoWinD3dCompiler = new Hint("SDL_VIDEO_WIN_D3DCOMPILER");
        public static readonly Hint VideoWindowSharePixelFormat = new Hint("SDL_VIDEO_WINDOW_SHARE_PIXEL_FORMAT");
        public static readonly Hint WinrtPrivacyPolicyUrl = new Hint("SDL_WINRT_PRIVACY_POLICY_URL");
        public static readonly Hint WinrtPrivacyPolicyLabel = new Hint("SDL_WINRT_PRIVACY_POLICY_LABEL");
        public static readonly Hint WinrtHandleBackButton = new Hint("SDL_WINRT_HANDLE_BACK_BUTTON");
        public static readonly Hint VideoMacFullscreenSpaces = new Hint("SDL_VIDEO_MAC_FULLSCREEN_SPACES");
        public static readonly Hint MacBackgroundApp = new Hint("SDL_MAC_BACKGROUND_APP");
        public static readonly Hint AndroidApkExpansionMainFileVersion = new Hint("SDL_ANDROID_APK_EXPANSION_MAIN_FILE_VERSION");
        public static readonly Hint AndroidApkExpansionPatchFileVersion = new Hint("SDL_ANDROID_APK_EXPANSION_PATCH_FILE_VERSION");
        public static readonly Hint ImeInternalEditing = new Hint("SDL_IME_INTERNAL_EDITING");
        public static readonly Hint AndroidTrapBackButton = new Hint("SDL_ANDROID_TRAP_BACK_BUTTON");
        public static readonly Hint AndroidBlockOnPause = new Hint("SDL_ANDROID_BLOCK_ON_PAUSE");
        public static readonly Hint ReturnKeyHidesIme = new Hint("SDL_RETURN_KEY_HIDES_IME");
        public static readonly Hint EmscriptenKeyboardElement = new Hint("SDL_EMSCRIPTEN_KEYBOARD_ELEMENT");
        public static readonly Hint NoSignalHandlers = new Hint("SDL_NO_SIGNAL_HANDLERS");
        public static readonly Hint WindowsNoCloseOnAltF4 = new Hint("SDL_WINDOWS_NO_CLOSE_ON_ALT_F4");
        public static readonly Hint BmpSaveLegacyFormat = new Hint("SDL_BMP_SAVE_LEGACY_FORMAT");
        public static readonly Hint WindowsDisableThreadNaming = new Hint("SDL_WINDOWS_DISABLE_THREAD_NAMING");
        public static readonly Hint RpiVideoLayer = new Hint("SDL_RPI_VIDEO_LAYER");
        public static readonly Hint VideoDoubleBuffer = new Hint("SDL_VIDEO_DOUBLE_BUFFER");
        public static readonly Hint OpenglEsDriver = new Hint("SDL_OPENGL_ES_DRIVER");
        public static readonly Hint AudioResamplingMode = new Hint("SDL_AUDIO_RESAMPLING_MODE");
        public static readonly Hint AudioCategory = new Hint("SDL_AUDIO_CATEGORY");
        public static readonly Hint RenderBatching = new Hint("SDL_RENDER_BATCHING");
        public static readonly Hint EventLogging = new Hint("SDL_EVENT_LOGGING");
        public static readonly Hint WaveRiffChunkSize = new Hint("SDL_WAVE_RIFF_CHUNK_SIZE");
        public static readonly Hint WaveTruncation = new Hint("SDL_WAVE_TRUNCATION");
        public static readonly Hint WaveFactChunk = new Hint("SDL_WAVE_FACT_CHUNK");

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
        public void AddCallback(HintCallback callback, IntPtr data)
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
        public void DeleteCallback(HintCallback callback, IntPtr data)
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
