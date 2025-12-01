using static Sdl3Sharp.Native.Events;

namespace Sdl3Sharp;

/// <summary>
/// The types of events that can be delivered.
/// </summary>
public enum EventType : uint
{
    /// <summary>Unused (do not remove).</summary>
    First = SDL_EventType.SDL_EVENT_FIRST,

    /// <summary>User-requested quit.</summary>
    Quit = SDL_EventType.SDL_EVENT_QUIT,

    /// <summary>The application is being terminated by the OS.</summary>
    Terminating = SDL_EventType.SDL_EVENT_TERMINATING,
    /// <summary>The application is low on memory, free memory if possible.</summary>
    LowMemory = SDL_EventType.SDL_EVENT_LOW_MEMORY,
    /// <summary>The application is about to enter the background.</summary>
    WillEnterBackground = SDL_EventType.SDL_EVENT_WILL_ENTER_BACKGROUND,
    /// <summary>The application did enter the background and may not get CPU for some time.</summary>
    DidEnterBackground = SDL_EventType.SDL_EVENT_DID_ENTER_BACKGROUND,
    /// <summary>The application is about to enter the foreground.</summary>
    WillEnterForeground = SDL_EventType.SDL_EVENT_WILL_ENTER_FOREGROUND,
    /// <summary>The application is now interactive.</summary>
    DidEnterForeground = SDL_EventType.SDL_EVENT_DID_ENTER_FOREGROUND,

    /// <summary>The user's locale preferences have changed.</summary>
    LocaleChanged = SDL_EventType.SDL_EVENT_LOCALE_CHANGED,

    /// <summary>The system theme changed.</summary>
    SystemThemeChanged = SDL_EventType.SDL_EVENT_SYSTEM_THEME_CHANGED,

    /// <summary>Display orientation has changed.</summary>
    DisplayOrientation = SDL_EventType.SDL_EVENT_DISPLAY_ORIENTATION,
    /// <summary>Display has been added to the system.</summary>
    DisplayAdded = SDL_EventType.SDL_EVENT_DISPLAY_ADDED,
    /// <summary>Display has been removed from the system.</summary>
    DisplayRemoved = SDL_EventType.SDL_EVENT_DISPLAY_REMOVED,
    /// <summary>Display has changed position.</summary>
    DisplayMoved = SDL_EventType.SDL_EVENT_DISPLAY_MOVED,
    /// <summary>Display has changed desktop mode.</summary>
    DisplayDesktopModeChanged = SDL_EventType.SDL_EVENT_DISPLAY_DESKTOP_MODE_CHANGED,
    /// <summary>Display has changed current mode.</summary>
    DisplayCurrentModeChanged = SDL_EventType.SDL_EVENT_DISPLAY_CURRENT_MODE_CHANGED,
    /// <summary>Display has changed content scale.</summary>
    DisplayContentScaleChanged = SDL_EventType.SDL_EVENT_DISPLAY_CONTENT_SCALE_CHANGED,

    /// <summary>Window has been shown.</summary>
    WindowShown = SDL_EventType.SDL_EVENT_WINDOW_SHOWN,
    /// <summary>Window has been hidden.</summary>
    WindowHidden = SDL_EventType.SDL_EVENT_WINDOW_HIDDEN,
    /// <summary>Window has been exposed and should be redrawn.</summary>
    WindowExposed = SDL_EventType.SDL_EVENT_WINDOW_EXPOSED,
    /// <summary>Window has been moved.</summary>
    WindowMoved = SDL_EventType.SDL_EVENT_WINDOW_MOVED,
    /// <summary>Window has been resized.</summary>
    WindowResized = SDL_EventType.SDL_EVENT_WINDOW_RESIZED,
    /// <summary>The pixel size of the window has changed.</summary>
    WindowPixelSizeChanged = SDL_EventType.SDL_EVENT_WINDOW_PIXEL_SIZE_CHANGED,
    /// <summary>The pixel size of a Metal view associated with the window has changed.</summary>
    WindowMetalViewResized = SDL_EventType.SDL_EVENT_WINDOW_METAL_VIEW_RESIZED,
    /// <summary>Window has been minimized.</summary>
    WindowMinimized = SDL_EventType.SDL_EVENT_WINDOW_MINIMIZED,
    /// <summary>Window has been maximized.</summary>
    WindowMaximized = SDL_EventType.SDL_EVENT_WINDOW_MAXIMIZED,
    /// <summary>Window has been restored to normal size and position.</summary>
    WindowRestored = SDL_EventType.SDL_EVENT_WINDOW_RESTORED,
    /// <summary>Window has gained mouse focus.</summary>
    WindowMouseEnter = SDL_EventType.SDL_EVENT_WINDOW_MOUSE_ENTER,
    /// <summary>Window has lost mouse focus.</summary>
    WindowMouseLeave = SDL_EventType.SDL_EVENT_WINDOW_MOUSE_LEAVE,
    /// <summary>Window has gained keyboard focus.</summary>
    WindowFocusGained = SDL_EventType.SDL_EVENT_WINDOW_FOCUS_GAINED,
    /// <summary>Window has lost keyboard focus.</summary>
    WindowFocusLost = SDL_EventType.SDL_EVENT_WINDOW_FOCUS_LOST,
    /// <summary>The window manager requests that the window be closed.</summary>
    WindowCloseRequested = SDL_EventType.SDL_EVENT_WINDOW_CLOSE_REQUESTED,
    /// <summary>Window had a hit test that wasn't normal.</summary>
    WindowHitTest = SDL_EventType.SDL_EVENT_WINDOW_HIT_TEST,
    /// <summary>The ICC profile of the window's display has changed.</summary>
    WindowIccProfileChanged = SDL_EventType.SDL_EVENT_WINDOW_ICCPROF_CHANGED,
    /// <summary>Window has been moved to a different display.</summary>
    WindowDisplayChanged = SDL_EventType.SDL_EVENT_WINDOW_DISPLAY_CHANGED,
    /// <summary>Window display scale has been changed.</summary>
    WindowDisplayScaleChanged = SDL_EventType.SDL_EVENT_WINDOW_DISPLAY_SCALE_CHANGED,
    /// <summary>The window safe area has been changed.</summary>
    WindowSafeAreaChanged = SDL_EventType.SDL_EVENT_WINDOW_SAFE_AREA_CHANGED,
    /// <summary>The window has been occluded.</summary>
    WindowOccluded = SDL_EventType.SDL_EVENT_WINDOW_OCCLUDED,
    /// <summary>The window has entered fullscreen mode.</summary>
    WindowEnterFullscreen = SDL_EventType.SDL_EVENT_WINDOW_ENTER_FULLSCREEN,
    /// <summary>The window has left fullscreen mode.</summary>
    WindowLeaveFullscreen = SDL_EventType.SDL_EVENT_WINDOW_LEAVE_FULLSCREEN,
    /// <summary>The window with the associated ID is being or has been destroyed.</summary>
    WindowDestroyed = SDL_EventType.SDL_EVENT_WINDOW_DESTROYED,
    /// <summary>Window HDR properties have changed.</summary>
    WindowHdrStateChanged = SDL_EventType.SDL_EVENT_WINDOW_HDR_STATE_CHANGED,

    /// <summary>Key pressed.</summary>
    KeyDown = SDL_EventType.SDL_EVENT_KEY_DOWN,
    /// <summary>Key released.</summary>
    KeyUp = SDL_EventType.SDL_EVENT_KEY_UP,
    /// <summary>Keyboard text editing (composition).</summary>
    TextEditing = SDL_EventType.SDL_EVENT_TEXT_EDITING,
    /// <summary>Keyboard text input.</summary>
    TextInput = SDL_EventType.SDL_EVENT_TEXT_INPUT,
    /// <summary>Keymap changed due to a system event.</summary>
    KeymapChanged = SDL_EventType.SDL_EVENT_KEYMAP_CHANGED,
    /// <summary>A new keyboard has been inserted into the system.</summary>
    KeyboardAdded = SDL_EventType.SDL_EVENT_KEYBOARD_ADDED,
    /// <summary>A keyboard has been removed.</summary>
    KeyboardRemoved = SDL_EventType.SDL_EVENT_KEYBOARD_REMOVED,
    /// <summary>Keyboard text editing candidates.</summary>
    TextEditingCandidates = SDL_EventType.SDL_EVENT_TEXT_EDITING_CANDIDATES,

    /// <summary>Mouse moved.</summary>
    MouseMotion = SDL_EventType.SDL_EVENT_MOUSE_MOTION,
    /// <summary>Mouse button pressed.</summary>
    MouseButtonDown = SDL_EventType.SDL_EVENT_MOUSE_BUTTON_DOWN,
    /// <summary>Mouse button released.</summary>
    MouseButtonUp = SDL_EventType.SDL_EVENT_MOUSE_BUTTON_UP,
    /// <summary>Mouse wheel motion.</summary>
    MouseWheel = SDL_EventType.SDL_EVENT_MOUSE_WHEEL,
    /// <summary>A new mouse has been inserted into the system.</summary>
    MouseAdded = SDL_EventType.SDL_EVENT_MOUSE_ADDED,
    /// <summary>A mouse has been removed.</summary>
    MouseRemoved = SDL_EventType.SDL_EVENT_MOUSE_REMOVED,

    /// <summary>Joystick axis motion.</summary>
    JoystickAxisMotion = SDL_EventType.SDL_EVENT_JOYSTICK_AXIS_MOTION,
    /// <summary>Joystick trackball motion.</summary>
    JoystickBallMotion = SDL_EventType.SDL_EVENT_JOYSTICK_BALL_MOTION,
    /// <summary>Joystick hat position change.</summary>
    JoystickHatMotion = SDL_EventType.SDL_EVENT_JOYSTICK_HAT_MOTION,
    /// <summary>Joystick button pressed.</summary>
    JoystickButtonDown = SDL_EventType.SDL_EVENT_JOYSTICK_BUTTON_DOWN,
    /// <summary>Joystick button released.</summary>
    JoystickButtonUp = SDL_EventType.SDL_EVENT_JOYSTICK_BUTTON_UP,
    /// <summary>A new joystick has been inserted into the system.</summary>
    JoystickAdded = SDL_EventType.SDL_EVENT_JOYSTICK_ADDED,
    /// <summary>An opened joystick has been removed.</summary>
    JoystickRemoved = SDL_EventType.SDL_EVENT_JOYSTICK_REMOVED,
    /// <summary>Joystick battery level change.</summary>
    JoystickBatteryUpdated = SDL_EventType.SDL_EVENT_JOYSTICK_BATTERY_UPDATED,
    /// <summary>Joystick update is complete.</summary>
    JoystickUpdateComplete = SDL_EventType.SDL_EVENT_JOYSTICK_UPDATE_COMPLETE,

    /// <summary>Gamepad axis motion.</summary>
    GamepadAxisMotion = SDL_EventType.SDL_EVENT_GAMEPAD_AXIS_MOTION,
    /// <summary>Gamepad button pressed.</summary>
    GamepadButtonDown = SDL_EventType.SDL_EVENT_GAMEPAD_BUTTON_DOWN,
    /// <summary>Gamepad button released.</summary>
    GamepadButtonUp = SDL_EventType.SDL_EVENT_GAMEPAD_BUTTON_UP,
    /// <summary>A new gamepad has been inserted into the system.</summary>
    GamepadAdded = SDL_EventType.SDL_EVENT_GAMEPAD_ADDED,
    /// <summary>A gamepad has been removed.</summary>
    GamepadRemoved = SDL_EventType.SDL_EVENT_GAMEPAD_REMOVED,
    /// <summary>The gamepad mapping was updated.</summary>
    GamepadRemapped = SDL_EventType.SDL_EVENT_GAMEPAD_REMAPPED,
    /// <summary>Gamepad touchpad was touched.</summary>
    GamepadTouchpadDown = SDL_EventType.SDL_EVENT_GAMEPAD_TOUCHPAD_DOWN,
    /// <summary>Gamepad touchpad finger was moved.</summary>
    GamepadTouchpadMotion = SDL_EventType.SDL_EVENT_GAMEPAD_TOUCHPAD_MOTION,
    /// <summary>Gamepad touchpad finger was lifted.</summary>
    GamepadTouchpadUp = SDL_EventType.SDL_EVENT_GAMEPAD_TOUCHPAD_UP,
    /// <summary>Gamepad sensor was updated.</summary>
    GamepadSensorUpdate = SDL_EventType.SDL_EVENT_GAMEPAD_SENSOR_UPDATE,
    /// <summary>Gamepad update is complete.</summary>
    GamepadUpdateComplete = SDL_EventType.SDL_EVENT_GAMEPAD_UPDATE_COMPLETE,
    /// <summary>Gamepad Steam handle has changed.</summary>
    GamepadSteamHandleUpdated = SDL_EventType.SDL_EVENT_GAMEPAD_STEAM_HANDLE_UPDATED,

    /// <summary>Touch finger down.</summary>
    FingerDown = SDL_EventType.SDL_EVENT_FINGER_DOWN,
    /// <summary>Touch finger up.</summary>
    FingerUp = SDL_EventType.SDL_EVENT_FINGER_UP,
    /// <summary>Touch finger motion.</summary>
    FingerMotion = SDL_EventType.SDL_EVENT_FINGER_MOTION,
    /// <summary>Touch finger canceled.</summary>
    FingerCanceled = SDL_EventType.SDL_EVENT_FINGER_CANCELED,

    /// <summary>The clipboard or primary selection changed.</summary>
    ClipboardUpdate = SDL_EventType.SDL_EVENT_CLIPBOARD_UPDATE,

    /// <summary>The system requests a file open.</summary>
    DropFile = SDL_EventType.SDL_EVENT_DROP_FILE,
    /// <summary>Text/plain drag-and-drop event.</summary>
    DropText = SDL_EventType.SDL_EVENT_DROP_TEXT,
    /// <summary>A new set of drops is beginning.</summary>
    DropBegin = SDL_EventType.SDL_EVENT_DROP_BEGIN,
    /// <summary>Current set of drops is now complete.</summary>
    DropComplete = SDL_EventType.SDL_EVENT_DROP_COMPLETE,
    /// <summary>Position while moving over the window.</summary>
    DropPosition = SDL_EventType.SDL_EVENT_DROP_POSITION,

    /// <summary>A new audio device is available.</summary>
    AudioDeviceAdded = SDL_EventType.SDL_EVENT_AUDIO_DEVICE_ADDED,
    /// <summary>An audio device has been removed.</summary>
    AudioDeviceRemoved = SDL_EventType.SDL_EVENT_AUDIO_DEVICE_REMOVED,
    /// <summary>An audio device's format has been changed by the system.</summary>
    AudioDeviceFormatChanged = SDL_EventType.SDL_EVENT_AUDIO_DEVICE_FORMAT_CHANGED,

    /// <summary>A sensor was updated.</summary>
    SensorUpdate = SDL_EventType.SDL_EVENT_SENSOR_UPDATE,

    /// <summary>Pressure-sensitive pen has become available.</summary>
    PenProximityIn = SDL_EventType.SDL_EVENT_PEN_PROXIMITY_IN,
    /// <summary>Pressure-sensitive pen has become unavailable.</summary>
    PenProximityOut = SDL_EventType.SDL_EVENT_PEN_PROXIMITY_OUT,
    /// <summary>Pressure-sensitive pen touched drawing surface.</summary>
    PenDown = SDL_EventType.SDL_EVENT_PEN_DOWN,
    /// <summary>Pressure-sensitive pen stopped touching drawing surface.</summary>
    PenUp = SDL_EventType.SDL_EVENT_PEN_UP,
    /// <summary>Pressure-sensitive pen button pressed.</summary>
    PenButtonDown = SDL_EventType.SDL_EVENT_PEN_BUTTON_DOWN,
    /// <summary>Pressure-sensitive pen button released.</summary>
    PenButtonUp = SDL_EventType.SDL_EVENT_PEN_BUTTON_UP,
    /// <summary>Pressure-sensitive pen is moving on the tablet.</summary>
    PenMotion = SDL_EventType.SDL_EVENT_PEN_MOTION,
    /// <summary>Pressure-sensitive pen angle/pressure/etc changed.</summary>
    PenAxis = SDL_EventType.SDL_EVENT_PEN_AXIS,

    /// <summary>A new camera device is available.</summary>
    CameraDeviceAdded = SDL_EventType.SDL_EVENT_CAMERA_DEVICE_ADDED,
    /// <summary>A camera device has been removed.</summary>
    CameraDeviceRemoved = SDL_EventType.SDL_EVENT_CAMERA_DEVICE_REMOVED,
    /// <summary>A camera device has been approved for use by the user.</summary>
    CameraDeviceApproved = SDL_EventType.SDL_EVENT_CAMERA_DEVICE_APPROVED,
    /// <summary>A camera device has been denied for use by the user.</summary>
    CameraDeviceDenied = SDL_EventType.SDL_EVENT_CAMERA_DEVICE_DENIED,

    /// <summary>The render targets have been reset and their contents need to be updated.</summary>
    RenderTargetsReset = SDL_EventType.SDL_EVENT_RENDER_TARGETS_RESET,
    /// <summary>The device has been reset and all textures need to be recreated.</summary>
    RenderDeviceReset = SDL_EventType.SDL_EVENT_RENDER_DEVICE_RESET,
    /// <summary>The device has been lost and can't be recovered.</summary>
    RenderDeviceLost = SDL_EventType.SDL_EVENT_RENDER_DEVICE_LOST,

    /// <summary>Signals the end of an event poll cycle.</summary>
    PollSentinel = SDL_EventType.SDL_EVENT_POLL_SENTINEL,

    /// <summary>Events from User through Last are for your use, and should be allocated with RegisterEvents.</summary>
    User = SDL_EventType.SDL_EVENT_USER,

    /// <summary>This last event is only for bounding internal arrays.</summary>
    Last = SDL_EventType.SDL_EVENT_LAST
}