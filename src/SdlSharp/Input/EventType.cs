namespace SdlSharp.Input;

/// <summary>
/// The types of events that can be delivered.
/// </summary>
public enum EventType : uint
{
    /// <summary>Unused (do not remove).</summary>
    First = (uint)Native.SDL_EventType.SDL_EVENT_FIRST,

    // Application events
    /// <summary>User-requested quit.</summary>
    Quit = (uint)Native.SDL_EventType.SDL_EVENT_QUIT,
    /// <summary>The application is being terminated by the OS.</summary>
    Terminating = (uint)Native.SDL_EventType.SDL_EVENT_TERMINATING,
    /// <summary>The application is low on memory; free memory if possible.</summary>
    LowMemory = (uint)Native.SDL_EventType.SDL_EVENT_LOW_MEMORY,
    /// <summary>The application is about to enter the background.</summary>
    WillEnterBackground = (uint)Native.SDL_EventType.SDL_EVENT_WILL_ENTER_BACKGROUND,
    /// <summary>The application did enter the background and may not get CPU for some time.</summary>
    DidEnterBackground = (uint)Native.SDL_EventType.SDL_EVENT_DID_ENTER_BACKGROUND,
    /// <summary>The application is about to enter the foreground.</summary>
    WillEnterForeground = (uint)Native.SDL_EventType.SDL_EVENT_WILL_ENTER_FOREGROUND,
    /// <summary>The application is now interactive.</summary>
    DidEnterForeground = (uint)Native.SDL_EventType.SDL_EVENT_DID_ENTER_FOREGROUND,
    /// <summary>The user's locale preferences have changed.</summary>
    LocaleChanged = (uint)Native.SDL_EventType.SDL_EVENT_LOCALE_CHANGED,
    /// <summary>The system theme changed.</summary>
    SystemThemeChanged = (uint)Native.SDL_EventType.SDL_EVENT_SYSTEM_THEME_CHANGED,

    // Display events
    /// <summary>Display orientation has changed.</summary>
    DisplayOrientation = (uint)Native.SDL_EventType.SDL_EVENT_DISPLAY_ORIENTATION,
    /// <summary>Display has been added to the system.</summary>
    DisplayAdded = (uint)Native.SDL_EventType.SDL_EVENT_DISPLAY_ADDED,
    /// <summary>Display has been removed from the system.</summary>
    DisplayRemoved = (uint)Native.SDL_EventType.SDL_EVENT_DISPLAY_REMOVED,
    /// <summary>Display has changed position.</summary>
    DisplayMoved = (uint)Native.SDL_EventType.SDL_EVENT_DISPLAY_MOVED,
    /// <summary>Display has changed desktop mode.</summary>
    DisplayDesktopModeChanged = (uint)Native.SDL_EventType.SDL_EVENT_DISPLAY_DESKTOP_MODE_CHANGED,
    /// <summary>Display has changed current mode.</summary>
    DisplayCurrentModeChanged = (uint)Native.SDL_EventType.SDL_EVENT_DISPLAY_CURRENT_MODE_CHANGED,
    /// <summary>Display has changed content scale.</summary>
    DisplayContentScaleChanged = (uint)Native.SDL_EventType.SDL_EVENT_DISPLAY_CONTENT_SCALE_CHANGED,
    /// <summary>Display has changed usable bounds.</summary>
    DisplayUsableBoundsChanged = (uint)Native.SDL_EventType.SDL_EVENT_DISPLAY_USABLE_BOUNDS_CHANGED,
    /// <summary>The first display event.</summary>
    DisplayFirst = (uint)Native.SDL_EventType.SDL_EVENT_DISPLAY_FIRST,
    /// <summary>The last display event.</summary>
    DisplayLast = (uint)Native.SDL_EventType.SDL_EVENT_DISPLAY_LAST,

    // Window events
    /// <summary>Window has been shown.</summary>
    WindowShown = (uint)Native.SDL_EventType.SDL_EVENT_WINDOW_SHOWN,
    /// <summary>Window has been hidden.</summary>
    WindowHidden = (uint)Native.SDL_EventType.SDL_EVENT_WINDOW_HIDDEN,
    /// <summary>Window has been exposed and should be redrawn.</summary>
    WindowExposed = (uint)Native.SDL_EventType.SDL_EVENT_WINDOW_EXPOSED,
    /// <summary>Window has been moved.</summary>
    WindowMoved = (uint)Native.SDL_EventType.SDL_EVENT_WINDOW_MOVED,
    /// <summary>Window has been resized.</summary>
    WindowResized = (uint)Native.SDL_EventType.SDL_EVENT_WINDOW_RESIZED,
    /// <summary>The pixel size of the window has changed.</summary>
    WindowPixelSizeChanged = (uint)Native.SDL_EventType.SDL_EVENT_WINDOW_PIXEL_SIZE_CHANGED,
    /// <summary>The pixel size of a Metal view associated with the window has changed.</summary>
    WindowMetalViewResized = (uint)Native.SDL_EventType.SDL_EVENT_WINDOW_METAL_VIEW_RESIZED,
    /// <summary>Window has been minimized.</summary>
    WindowMinimized = (uint)Native.SDL_EventType.SDL_EVENT_WINDOW_MINIMIZED,
    /// <summary>Window has been maximized.</summary>
    WindowMaximized = (uint)Native.SDL_EventType.SDL_EVENT_WINDOW_MAXIMIZED,
    /// <summary>Window has been restored to normal size and position.</summary>
    WindowRestored = (uint)Native.SDL_EventType.SDL_EVENT_WINDOW_RESTORED,
    /// <summary>Window has gained mouse focus.</summary>
    WindowMouseEnter = (uint)Native.SDL_EventType.SDL_EVENT_WINDOW_MOUSE_ENTER,
    /// <summary>Window has lost mouse focus.</summary>
    WindowMouseLeave = (uint)Native.SDL_EventType.SDL_EVENT_WINDOW_MOUSE_LEAVE,
    /// <summary>Window has gained keyboard focus.</summary>
    WindowFocusGained = (uint)Native.SDL_EventType.SDL_EVENT_WINDOW_FOCUS_GAINED,
    /// <summary>Window has lost keyboard focus.</summary>
    WindowFocusLost = (uint)Native.SDL_EventType.SDL_EVENT_WINDOW_FOCUS_LOST,
    /// <summary>The window manager requests that the window be closed.</summary>
    WindowCloseRequested = (uint)Native.SDL_EventType.SDL_EVENT_WINDOW_CLOSE_REQUESTED,
    /// <summary>Window had a hit test that wasn't a normal region.</summary>
    WindowHitTest = (uint)Native.SDL_EventType.SDL_EVENT_WINDOW_HIT_TEST,
    /// <summary>The ICC profile of the window's display has changed.</summary>
    WindowIccprofChanged = (uint)Native.SDL_EventType.SDL_EVENT_WINDOW_ICCPROF_CHANGED,
    /// <summary>Window has been moved to a different display.</summary>
    WindowDisplayChanged = (uint)Native.SDL_EventType.SDL_EVENT_WINDOW_DISPLAY_CHANGED,
    /// <summary>Window display scale has been changed.</summary>
    WindowDisplayScaleChanged = (uint)Native.SDL_EventType.SDL_EVENT_WINDOW_DISPLAY_SCALE_CHANGED,
    /// <summary>The window safe area has been changed.</summary>
    WindowSafeAreaChanged = (uint)Native.SDL_EventType.SDL_EVENT_WINDOW_SAFE_AREA_CHANGED,
    /// <summary>The window has been occluded.</summary>
    WindowOccluded = (uint)Native.SDL_EventType.SDL_EVENT_WINDOW_OCCLUDED,
    /// <summary>The window has entered fullscreen mode.</summary>
    WindowEnterFullscreen = (uint)Native.SDL_EventType.SDL_EVENT_WINDOW_ENTER_FULLSCREEN,
    /// <summary>The window has left fullscreen mode.</summary>
    WindowLeaveFullscreen = (uint)Native.SDL_EventType.SDL_EVENT_WINDOW_LEAVE_FULLSCREEN,
    /// <summary>The window with the associated ID is being or has been destroyed.</summary>
    WindowDestroyed = (uint)Native.SDL_EventType.SDL_EVENT_WINDOW_DESTROYED,
    /// <summary>Window HDR properties have changed.</summary>
    WindowHdrStateChanged = (uint)Native.SDL_EventType.SDL_EVENT_WINDOW_HDR_STATE_CHANGED,
    /// <summary>The first window event.</summary>
    WindowFirst = (uint)Native.SDL_EventType.SDL_EVENT_WINDOW_FIRST,
    /// <summary>The last window event.</summary>
    WindowLast = (uint)Native.SDL_EventType.SDL_EVENT_WINDOW_LAST,

    // Keyboard events
    /// <summary>Key pressed.</summary>
    KeyDown = (uint)Native.SDL_EventType.SDL_EVENT_KEY_DOWN,
    /// <summary>Key released.</summary>
    KeyUp = (uint)Native.SDL_EventType.SDL_EVENT_KEY_UP,
    /// <summary>Keyboard text editing (composition).</summary>
    TextEditing = (uint)Native.SDL_EventType.SDL_EVENT_TEXT_EDITING,
    /// <summary>Keyboard text input.</summary>
    TextInput = (uint)Native.SDL_EventType.SDL_EVENT_TEXT_INPUT,
    /// <summary>Keymap changed due to a system event such as an input language or keyboard layout change.</summary>
    KeymapChanged = (uint)Native.SDL_EventType.SDL_EVENT_KEYMAP_CHANGED,
    /// <summary>A new keyboard has been inserted into the system.</summary>
    KeyboardAdded = (uint)Native.SDL_EventType.SDL_EVENT_KEYBOARD_ADDED,
    /// <summary>A keyboard has been removed.</summary>
    KeyboardRemoved = (uint)Native.SDL_EventType.SDL_EVENT_KEYBOARD_REMOVED,
    /// <summary>Keyboard text editing candidates.</summary>
    TextEditingCandidates = (uint)Native.SDL_EventType.SDL_EVENT_TEXT_EDITING_CANDIDATES,
    /// <summary>The on-screen keyboard has been shown.</summary>
    ScreenKeyboardShown = (uint)Native.SDL_EventType.SDL_EVENT_SCREEN_KEYBOARD_SHOWN,
    /// <summary>The on-screen keyboard has been hidden.</summary>
    ScreenKeyboardHidden = (uint)Native.SDL_EventType.SDL_EVENT_SCREEN_KEYBOARD_HIDDEN,

    // Mouse events
    /// <summary>Mouse moved.</summary>
    MouseMotion = (uint)Native.SDL_EventType.SDL_EVENT_MOUSE_MOTION,
    /// <summary>Mouse button pressed.</summary>
    MouseButtonDown = (uint)Native.SDL_EventType.SDL_EVENT_MOUSE_BUTTON_DOWN,
    /// <summary>Mouse button released.</summary>
    MouseButtonUp = (uint)Native.SDL_EventType.SDL_EVENT_MOUSE_BUTTON_UP,
    /// <summary>Mouse wheel motion.</summary>
    MouseWheel = (uint)Native.SDL_EventType.SDL_EVENT_MOUSE_WHEEL,
    /// <summary>A new mouse has been inserted into the system.</summary>
    MouseAdded = (uint)Native.SDL_EventType.SDL_EVENT_MOUSE_ADDED,
    /// <summary>A mouse has been removed.</summary>
    MouseRemoved = (uint)Native.SDL_EventType.SDL_EVENT_MOUSE_REMOVED,

    // Joystick events
    /// <summary>Joystick axis motion.</summary>
    JoystickAxisMotion = (uint)Native.SDL_EventType.SDL_EVENT_JOYSTICK_AXIS_MOTION,
    /// <summary>Joystick trackball motion.</summary>
    JoystickBallMotion = (uint)Native.SDL_EventType.SDL_EVENT_JOYSTICK_BALL_MOTION,
    /// <summary>Joystick hat position change.</summary>
    JoystickHatMotion = (uint)Native.SDL_EventType.SDL_EVENT_JOYSTICK_HAT_MOTION,
    /// <summary>Joystick button pressed.</summary>
    JoystickButtonDown = (uint)Native.SDL_EventType.SDL_EVENT_JOYSTICK_BUTTON_DOWN,
    /// <summary>Joystick button released.</summary>
    JoystickButtonUp = (uint)Native.SDL_EventType.SDL_EVENT_JOYSTICK_BUTTON_UP,
    /// <summary>A new joystick has been inserted into the system.</summary>
    JoystickAdded = (uint)Native.SDL_EventType.SDL_EVENT_JOYSTICK_ADDED,
    /// <summary>An opened joystick has been removed.</summary>
    JoystickRemoved = (uint)Native.SDL_EventType.SDL_EVENT_JOYSTICK_REMOVED,
    /// <summary>Joystick battery level change.</summary>
    JoystickBatteryUpdated = (uint)Native.SDL_EventType.SDL_EVENT_JOYSTICK_BATTERY_UPDATED,
    /// <summary>Joystick update is complete.</summary>
    JoystickUpdateComplete = (uint)Native.SDL_EventType.SDL_EVENT_JOYSTICK_UPDATE_COMPLETE,

    // Gamepad events
    /// <summary>Gamepad axis motion.</summary>
    GamepadAxisMotion = (uint)Native.SDL_EventType.SDL_EVENT_GAMEPAD_AXIS_MOTION,
    /// <summary>Gamepad button pressed.</summary>
    GamepadButtonDown = (uint)Native.SDL_EventType.SDL_EVENT_GAMEPAD_BUTTON_DOWN,
    /// <summary>Gamepad button released.</summary>
    GamepadButtonUp = (uint)Native.SDL_EventType.SDL_EVENT_GAMEPAD_BUTTON_UP,
    /// <summary>A new gamepad has been inserted into the system.</summary>
    GamepadAdded = (uint)Native.SDL_EventType.SDL_EVENT_GAMEPAD_ADDED,
    /// <summary>A gamepad has been removed.</summary>
    GamepadRemoved = (uint)Native.SDL_EventType.SDL_EVENT_GAMEPAD_REMOVED,
    /// <summary>The gamepad mapping was updated.</summary>
    GamepadRemapped = (uint)Native.SDL_EventType.SDL_EVENT_GAMEPAD_REMAPPED,
    /// <summary>Gamepad touchpad was touched.</summary>
    GamepadTouchpadDown = (uint)Native.SDL_EventType.SDL_EVENT_GAMEPAD_TOUCHPAD_DOWN,
    /// <summary>Gamepad touchpad finger was moved.</summary>
    GamepadTouchpadMotion = (uint)Native.SDL_EventType.SDL_EVENT_GAMEPAD_TOUCHPAD_MOTION,
    /// <summary>Gamepad touchpad finger was lifted.</summary>
    GamepadTouchpadUp = (uint)Native.SDL_EventType.SDL_EVENT_GAMEPAD_TOUCHPAD_UP,
    /// <summary>Gamepad sensor was updated.</summary>
    GamepadSensorUpdate = (uint)Native.SDL_EventType.SDL_EVENT_GAMEPAD_SENSOR_UPDATE,
    /// <summary>Gamepad update is complete.</summary>
    GamepadUpdateComplete = (uint)Native.SDL_EventType.SDL_EVENT_GAMEPAD_UPDATE_COMPLETE,
    /// <summary>Gamepad Steam handle has changed.</summary>
    GamepadSteamHandleUpdated = (uint)Native.SDL_EventType.SDL_EVENT_GAMEPAD_STEAM_HANDLE_UPDATED,

    // Touch events
    /// <summary>A finger touched the surface.</summary>
    FingerDown = (uint)Native.SDL_EventType.SDL_EVENT_FINGER_DOWN,
    /// <summary>A finger was lifted from the surface.</summary>
    FingerUp = (uint)Native.SDL_EventType.SDL_EVENT_FINGER_UP,
    /// <summary>A finger moved on the surface.</summary>
    FingerMotion = (uint)Native.SDL_EventType.SDL_EVENT_FINGER_MOTION,
    /// <summary>A finger touch was canceled.</summary>
    FingerCanceled = (uint)Native.SDL_EventType.SDL_EVENT_FINGER_CANCELED,

    // Pinch events
    /// <summary>Pinch gesture started.</summary>
    PinchBegin = (uint)Native.SDL_EventType.SDL_EVENT_PINCH_BEGIN,
    /// <summary>Pinch gesture updated.</summary>
    PinchUpdate = (uint)Native.SDL_EventType.SDL_EVENT_PINCH_UPDATE,
    /// <summary>Pinch gesture ended.</summary>
    PinchEnd = (uint)Native.SDL_EventType.SDL_EVENT_PINCH_END,

    // Clipboard events
    /// <summary>The clipboard changed.</summary>
    ClipboardUpdate = (uint)Native.SDL_EventType.SDL_EVENT_CLIPBOARD_UPDATE,

    // Drag and drop events
    /// <summary>The system requests a file open.</summary>
    DropFile = (uint)Native.SDL_EventType.SDL_EVENT_DROP_FILE,
    /// <summary>A text/plain drag-and-drop event.</summary>
    DropText = (uint)Native.SDL_EventType.SDL_EVENT_DROP_TEXT,
    /// <summary>A new set of drops is beginning.</summary>
    DropBegin = (uint)Native.SDL_EventType.SDL_EVENT_DROP_BEGIN,
    /// <summary>Current set of drops is now complete.</summary>
    DropComplete = (uint)Native.SDL_EventType.SDL_EVENT_DROP_COMPLETE,
    /// <summary>Position while moving over the window.</summary>
    DropPosition = (uint)Native.SDL_EventType.SDL_EVENT_DROP_POSITION,

    // Audio hotplug events
    /// <summary>A new audio device is available.</summary>
    AudioDeviceAdded = (uint)Native.SDL_EventType.SDL_EVENT_AUDIO_DEVICE_ADDED,
    /// <summary>An audio device has been removed.</summary>
    AudioDeviceRemoved = (uint)Native.SDL_EventType.SDL_EVENT_AUDIO_DEVICE_REMOVED,
    /// <summary>An audio device's format has been changed by the system.</summary>
    AudioDeviceFormatChanged = (uint)Native.SDL_EventType.SDL_EVENT_AUDIO_DEVICE_FORMAT_CHANGED,

    // Sensor events
    /// <summary>A sensor was updated.</summary>
    SensorUpdate = (uint)Native.SDL_EventType.SDL_EVENT_SENSOR_UPDATE,

    // Pen events
    /// <summary>Pressure-sensitive pen has become available.</summary>
    PenProximityIn = (uint)Native.SDL_EventType.SDL_EVENT_PEN_PROXIMITY_IN,
    /// <summary>Pressure-sensitive pen has become unavailable.</summary>
    PenProximityOut = (uint)Native.SDL_EventType.SDL_EVENT_PEN_PROXIMITY_OUT,
    /// <summary>Pressure-sensitive pen touched drawing surface.</summary>
    PenDown = (uint)Native.SDL_EventType.SDL_EVENT_PEN_DOWN,
    /// <summary>Pressure-sensitive pen stopped touching drawing surface.</summary>
    PenUp = (uint)Native.SDL_EventType.SDL_EVENT_PEN_UP,
    /// <summary>Pressure-sensitive pen button pressed.</summary>
    PenButtonDown = (uint)Native.SDL_EventType.SDL_EVENT_PEN_BUTTON_DOWN,
    /// <summary>Pressure-sensitive pen button released.</summary>
    PenButtonUp = (uint)Native.SDL_EventType.SDL_EVENT_PEN_BUTTON_UP,
    /// <summary>Pressure-sensitive pen is moving on the tablet.</summary>
    PenMotion = (uint)Native.SDL_EventType.SDL_EVENT_PEN_MOTION,
    /// <summary>Pressure-sensitive pen angle/pressure/etc changed.</summary>
    PenAxis = (uint)Native.SDL_EventType.SDL_EVENT_PEN_AXIS,

    // Camera events
    /// <summary>A new camera device is available.</summary>
    CameraDeviceAdded = (uint)Native.SDL_EventType.SDL_EVENT_CAMERA_DEVICE_ADDED,
    /// <summary>A camera device has been removed.</summary>
    CameraDeviceRemoved = (uint)Native.SDL_EventType.SDL_EVENT_CAMERA_DEVICE_REMOVED,
    /// <summary>A camera device has been approved for use by the user.</summary>
    CameraDeviceApproved = (uint)Native.SDL_EventType.SDL_EVENT_CAMERA_DEVICE_APPROVED,
    /// <summary>A camera device has been denied for use by the user.</summary>
    CameraDeviceDenied = (uint)Native.SDL_EventType.SDL_EVENT_CAMERA_DEVICE_DENIED,

    // Render events
    /// <summary>The render targets have been reset and their contents need to be updated.</summary>
    RenderTargetsReset = (uint)Native.SDL_EventType.SDL_EVENT_RENDER_TARGETS_RESET,
    /// <summary>The device has been reset and all textures need to be recreated.</summary>
    RenderDeviceReset = (uint)Native.SDL_EventType.SDL_EVENT_RENDER_DEVICE_RESET,
    /// <summary>The device has been lost and can't be recovered.</summary>
    RenderDeviceLost = (uint)Native.SDL_EventType.SDL_EVENT_RENDER_DEVICE_LOST,

    // Internal events
    /// <summary>Signals the end of an event poll cycle.</summary>
    PollSentinel = (uint)Native.SDL_EventType.SDL_EVENT_POLL_SENTINEL,

    // User events
    /// <summary>The start of user-defined event types.</summary>
    User = (uint)Native.SDL_EventType.SDL_EVENT_USER,
    /// <summary>The last valid event type.</summary>
    Last = (uint)Native.SDL_EventType.SDL_EVENT_LAST,
}
