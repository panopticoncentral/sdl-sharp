using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

using static Sdl3Sharp.Native.Audio;
using static Sdl3Sharp.Native.Camera;
using static Sdl3Sharp.Native.Common;
using static Sdl3Sharp.Native.Joystick;
using static Sdl3Sharp.Native.Keyboard;
using static Sdl3Sharp.Native.Keycode;
using static Sdl3Sharp.Native.Mouse;
using static Sdl3Sharp.Native.Pen;
using static Sdl3Sharp.Native.Power;
using static Sdl3Sharp.Native.Scancode;
using static Sdl3Sharp.Native.Sensor;
using static Sdl3Sharp.Native.Touch;
using static Sdl3Sharp.Native.Video;

// We are intentionally exposing the P/Invoke calls so people can do low-level calls if needed
#pragma warning disable CA1401 // P/Invokes should not be visible

namespace Sdl3Sharp.Native;

/// <summary>
/// P/Invoke bindings for SDL_events.h - Event queue management.
/// </summary>
/// <remarks>
/// It's extremely common--often required--that an app deal with SDL's event queue.
/// Almost all useful information about interactions with the real world flow through here:
/// the user interacting with the computer and app, hardware coming and going, the system
/// changing in some way, etc.
/// </remarks>
public static unsafe partial class Events
{
    /// <summary>
    /// The types of events that can be delivered.
    /// </summary>
    public enum SDL_EventType : uint
    {
        /// <summary>Unused (do not remove).</summary>
        SDL_EVENT_FIRST = 0,

        /// <summary>User-requested quit.</summary>
        SDL_EVENT_QUIT = 0x100,

        /// <summary>The application is being terminated by the OS. This event must be handled in a callback set with SDL_AddEventWatch().</summary>
        SDL_EVENT_TERMINATING,
        /// <summary>The application is low on memory, free memory if possible. This event must be handled in a callback set with SDL_AddEventWatch().</summary>
        SDL_EVENT_LOW_MEMORY,
        /// <summary>The application is about to enter the background. This event must be handled in a callback set with SDL_AddEventWatch().</summary>
        SDL_EVENT_WILL_ENTER_BACKGROUND,
        /// <summary>The application did enter the background and may not get CPU for some time. This event must be handled in a callback set with SDL_AddEventWatch().</summary>
        SDL_EVENT_DID_ENTER_BACKGROUND,
        /// <summary>The application is about to enter the foreground. This event must be handled in a callback set with SDL_AddEventWatch().</summary>
        SDL_EVENT_WILL_ENTER_FOREGROUND,
        /// <summary>The application is now interactive. This event must be handled in a callback set with SDL_AddEventWatch().</summary>
        SDL_EVENT_DID_ENTER_FOREGROUND,

        /// <summary>The user's locale preferences have changed.</summary>
        SDL_EVENT_LOCALE_CHANGED,

        /// <summary>The system theme changed.</summary>
        SDL_EVENT_SYSTEM_THEME_CHANGED,

        /// <summary>Display orientation has changed to data1.</summary>
        SDL_EVENT_DISPLAY_ORIENTATION = 0x151,
        /// <summary>Display has been added to the system.</summary>
        SDL_EVENT_DISPLAY_ADDED,
        /// <summary>Display has been removed from the system.</summary>
        SDL_EVENT_DISPLAY_REMOVED,
        /// <summary>Display has changed position.</summary>
        SDL_EVENT_DISPLAY_MOVED,
        /// <summary>Display has changed desktop mode.</summary>
        SDL_EVENT_DISPLAY_DESKTOP_MODE_CHANGED,
        /// <summary>Display has changed current mode.</summary>
        SDL_EVENT_DISPLAY_CURRENT_MODE_CHANGED,
        /// <summary>Display has changed content scale.</summary>
        SDL_EVENT_DISPLAY_CONTENT_SCALE_CHANGED,
        /// <summary>First display event.</summary>
        SDL_EVENT_DISPLAY_FIRST = SDL_EVENT_DISPLAY_ORIENTATION,
        /// <summary>Last display event.</summary>
        SDL_EVENT_DISPLAY_LAST = SDL_EVENT_DISPLAY_CONTENT_SCALE_CHANGED,

        /// <summary>Window has been shown.</summary>
        SDL_EVENT_WINDOW_SHOWN = 0x202,
        /// <summary>Window has been hidden.</summary>
        SDL_EVENT_WINDOW_HIDDEN,
        /// <summary>Window has been exposed and should be redrawn.</summary>
        SDL_EVENT_WINDOW_EXPOSED,
        /// <summary>Window has been moved to data1, data2.</summary>
        SDL_EVENT_WINDOW_MOVED,
        /// <summary>Window has been resized to data1xdata2.</summary>
        SDL_EVENT_WINDOW_RESIZED,
        /// <summary>The pixel size of the window has changed to data1xdata2.</summary>
        SDL_EVENT_WINDOW_PIXEL_SIZE_CHANGED,
        /// <summary>The pixel size of a Metal view associated with the window has changed.</summary>
        SDL_EVENT_WINDOW_METAL_VIEW_RESIZED,
        /// <summary>Window has been minimized.</summary>
        SDL_EVENT_WINDOW_MINIMIZED,
        /// <summary>Window has been maximized.</summary>
        SDL_EVENT_WINDOW_MAXIMIZED,
        /// <summary>Window has been restored to normal size and position.</summary>
        SDL_EVENT_WINDOW_RESTORED,
        /// <summary>Window has gained mouse focus.</summary>
        SDL_EVENT_WINDOW_MOUSE_ENTER,
        /// <summary>Window has lost mouse focus.</summary>
        SDL_EVENT_WINDOW_MOUSE_LEAVE,
        /// <summary>Window has gained keyboard focus.</summary>
        SDL_EVENT_WINDOW_FOCUS_GAINED,
        /// <summary>Window has lost keyboard focus.</summary>
        SDL_EVENT_WINDOW_FOCUS_LOST,
        /// <summary>The window manager requests that the window be closed.</summary>
        SDL_EVENT_WINDOW_CLOSE_REQUESTED,
        /// <summary>Window had a hit test that wasn't SDL_HITTEST_NORMAL.</summary>
        SDL_EVENT_WINDOW_HIT_TEST,
        /// <summary>The ICC profile of the window's display has changed.</summary>
        SDL_EVENT_WINDOW_ICCPROF_CHANGED,
        /// <summary>Window has been moved to display data1.</summary>
        SDL_EVENT_WINDOW_DISPLAY_CHANGED,
        /// <summary>Window display scale has been changed.</summary>
        SDL_EVENT_WINDOW_DISPLAY_SCALE_CHANGED,
        /// <summary>The window safe area has been changed.</summary>
        SDL_EVENT_WINDOW_SAFE_AREA_CHANGED,
        /// <summary>The window has been occluded.</summary>
        SDL_EVENT_WINDOW_OCCLUDED,
        /// <summary>The window has entered fullscreen mode.</summary>
        SDL_EVENT_WINDOW_ENTER_FULLSCREEN,
        /// <summary>The window has left fullscreen mode.</summary>
        SDL_EVENT_WINDOW_LEAVE_FULLSCREEN,
        /// <summary>The window with the associated ID is being or has been destroyed.</summary>
        SDL_EVENT_WINDOW_DESTROYED,
        /// <summary>Window HDR properties have changed.</summary>
        SDL_EVENT_WINDOW_HDR_STATE_CHANGED,
        /// <summary>First window event.</summary>
        SDL_EVENT_WINDOW_FIRST = SDL_EVENT_WINDOW_SHOWN,
        /// <summary>Last window event.</summary>
        SDL_EVENT_WINDOW_LAST = SDL_EVENT_WINDOW_HDR_STATE_CHANGED,

        /// <summary>Key pressed.</summary>
        SDL_EVENT_KEY_DOWN = 0x300,
        /// <summary>Key released.</summary>
        SDL_EVENT_KEY_UP,
        /// <summary>Keyboard text editing (composition).</summary>
        SDL_EVENT_TEXT_EDITING,
        /// <summary>Keyboard text input.</summary>
        SDL_EVENT_TEXT_INPUT,
        /// <summary>Keymap changed due to a system event such as an input language or keyboard layout change.</summary>
        SDL_EVENT_KEYMAP_CHANGED,
        /// <summary>A new keyboard has been inserted into the system.</summary>
        SDL_EVENT_KEYBOARD_ADDED,
        /// <summary>A keyboard has been removed.</summary>
        SDL_EVENT_KEYBOARD_REMOVED,
        /// <summary>Keyboard text editing candidates.</summary>
        SDL_EVENT_TEXT_EDITING_CANDIDATES,

        /// <summary>Mouse moved.</summary>
        SDL_EVENT_MOUSE_MOTION = 0x400,
        /// <summary>Mouse button pressed.</summary>
        SDL_EVENT_MOUSE_BUTTON_DOWN,
        /// <summary>Mouse button released.</summary>
        SDL_EVENT_MOUSE_BUTTON_UP,
        /// <summary>Mouse wheel motion.</summary>
        SDL_EVENT_MOUSE_WHEEL,
        /// <summary>A new mouse has been inserted into the system.</summary>
        SDL_EVENT_MOUSE_ADDED,
        /// <summary>A mouse has been removed.</summary>
        SDL_EVENT_MOUSE_REMOVED,

        /// <summary>Joystick axis motion.</summary>
        SDL_EVENT_JOYSTICK_AXIS_MOTION = 0x600,
        /// <summary>Joystick trackball motion.</summary>
        SDL_EVENT_JOYSTICK_BALL_MOTION,
        /// <summary>Joystick hat position change.</summary>
        SDL_EVENT_JOYSTICK_HAT_MOTION,
        /// <summary>Joystick button pressed.</summary>
        SDL_EVENT_JOYSTICK_BUTTON_DOWN,
        /// <summary>Joystick button released.</summary>
        SDL_EVENT_JOYSTICK_BUTTON_UP,
        /// <summary>A new joystick has been inserted into the system.</summary>
        SDL_EVENT_JOYSTICK_ADDED,
        /// <summary>An opened joystick has been removed.</summary>
        SDL_EVENT_JOYSTICK_REMOVED,
        /// <summary>Joystick battery level change.</summary>
        SDL_EVENT_JOYSTICK_BATTERY_UPDATED,
        /// <summary>Joystick update is complete.</summary>
        SDL_EVENT_JOYSTICK_UPDATE_COMPLETE,

        /// <summary>Gamepad axis motion.</summary>
        SDL_EVENT_GAMEPAD_AXIS_MOTION = 0x650,
        /// <summary>Gamepad button pressed.</summary>
        SDL_EVENT_GAMEPAD_BUTTON_DOWN,
        /// <summary>Gamepad button released.</summary>
        SDL_EVENT_GAMEPAD_BUTTON_UP,
        /// <summary>A new gamepad has been inserted into the system.</summary>
        SDL_EVENT_GAMEPAD_ADDED,
        /// <summary>A gamepad has been removed.</summary>
        SDL_EVENT_GAMEPAD_REMOVED,
        /// <summary>The gamepad mapping was updated.</summary>
        SDL_EVENT_GAMEPAD_REMAPPED,
        /// <summary>Gamepad touchpad was touched.</summary>
        SDL_EVENT_GAMEPAD_TOUCHPAD_DOWN,
        /// <summary>Gamepad touchpad finger was moved.</summary>
        SDL_EVENT_GAMEPAD_TOUCHPAD_MOTION,
        /// <summary>Gamepad touchpad finger was lifted.</summary>
        SDL_EVENT_GAMEPAD_TOUCHPAD_UP,
        /// <summary>Gamepad sensor was updated.</summary>
        SDL_EVENT_GAMEPAD_SENSOR_UPDATE,
        /// <summary>Gamepad update is complete.</summary>
        SDL_EVENT_GAMEPAD_UPDATE_COMPLETE,
        /// <summary>Gamepad Steam handle has changed.</summary>
        SDL_EVENT_GAMEPAD_STEAM_HANDLE_UPDATED,

        /// <summary>Touch finger down.</summary>
        SDL_EVENT_FINGER_DOWN = 0x700,
        /// <summary>Touch finger up.</summary>
        SDL_EVENT_FINGER_UP,
        /// <summary>Touch finger motion.</summary>
        SDL_EVENT_FINGER_MOTION,
        /// <summary>Touch finger canceled.</summary>
        SDL_EVENT_FINGER_CANCELED,

        /// <summary>The clipboard or primary selection changed.</summary>
        SDL_EVENT_CLIPBOARD_UPDATE = 0x900,

        /// <summary>The system requests a file open.</summary>
        SDL_EVENT_DROP_FILE = 0x1000,
        /// <summary>Text/plain drag-and-drop event.</summary>
        SDL_EVENT_DROP_TEXT,
        /// <summary>A new set of drops is beginning (NULL filename).</summary>
        SDL_EVENT_DROP_BEGIN,
        /// <summary>Current set of drops is now complete (NULL filename).</summary>
        SDL_EVENT_DROP_COMPLETE,
        /// <summary>Position while moving over the window.</summary>
        SDL_EVENT_DROP_POSITION,

        /// <summary>A new audio device is available.</summary>
        SDL_EVENT_AUDIO_DEVICE_ADDED = 0x1100,
        /// <summary>An audio device has been removed.</summary>
        SDL_EVENT_AUDIO_DEVICE_REMOVED,
        /// <summary>An audio device's format has been changed by the system.</summary>
        SDL_EVENT_AUDIO_DEVICE_FORMAT_CHANGED,

        /// <summary>A sensor was updated.</summary>
        SDL_EVENT_SENSOR_UPDATE = 0x1200,

        /// <summary>Pressure-sensitive pen has become available.</summary>
        SDL_EVENT_PEN_PROXIMITY_IN = 0x1300,
        /// <summary>Pressure-sensitive pen has become unavailable.</summary>
        SDL_EVENT_PEN_PROXIMITY_OUT,
        /// <summary>Pressure-sensitive pen touched drawing surface.</summary>
        SDL_EVENT_PEN_DOWN,
        /// <summary>Pressure-sensitive pen stopped touching drawing surface.</summary>
        SDL_EVENT_PEN_UP,
        /// <summary>Pressure-sensitive pen button pressed.</summary>
        SDL_EVENT_PEN_BUTTON_DOWN,
        /// <summary>Pressure-sensitive pen button released.</summary>
        SDL_EVENT_PEN_BUTTON_UP,
        /// <summary>Pressure-sensitive pen is moving on the tablet.</summary>
        SDL_EVENT_PEN_MOTION,
        /// <summary>Pressure-sensitive pen angle/pressure/etc changed.</summary>
        SDL_EVENT_PEN_AXIS,

        /// <summary>A new camera device is available.</summary>
        SDL_EVENT_CAMERA_DEVICE_ADDED = 0x1400,
        /// <summary>A camera device has been removed.</summary>
        SDL_EVENT_CAMERA_DEVICE_REMOVED,
        /// <summary>A camera device has been approved for use by the user.</summary>
        SDL_EVENT_CAMERA_DEVICE_APPROVED,
        /// <summary>A camera device has been denied for use by the user.</summary>
        SDL_EVENT_CAMERA_DEVICE_DENIED,

        /// <summary>The render targets have been reset and their contents need to be updated.</summary>
        SDL_EVENT_RENDER_TARGETS_RESET = 0x2000,
        /// <summary>The device has been reset and all textures need to be recreated.</summary>
        SDL_EVENT_RENDER_DEVICE_RESET,
        /// <summary>The device has been lost and can't be recovered.</summary>
        SDL_EVENT_RENDER_DEVICE_LOST,

        /// <summary>Reserved event for private platforms.</summary>
        SDL_EVENT_PRIVATE0 = 0x4000,
        /// <summary>Reserved event for private platforms.</summary>
        SDL_EVENT_PRIVATE1,
        /// <summary>Reserved event for private platforms.</summary>
        SDL_EVENT_PRIVATE2,
        /// <summary>Reserved event for private platforms.</summary>
        SDL_EVENT_PRIVATE3,

        /// <summary>Signals the end of an event poll cycle.</summary>
        SDL_EVENT_POLL_SENTINEL = 0x7F00,

        /// <summary>Events SDL_EVENT_USER through SDL_EVENT_LAST are for your use, and should be allocated with SDL_RegisterEvents().</summary>
        SDL_EVENT_USER = 0x8000,

        /// <summary>This last event is only for bounding internal arrays.</summary>
        SDL_EVENT_LAST = 0xFFFF,

        /// <summary>This just makes sure the enum is the size of Uint32.</summary>
        SDL_EVENT_ENUM_PADDING = 0x7FFFFFFF
    }

    /// <summary>
    /// The type of action to request from SDL_PeepEvents().
    /// </summary>
    public enum SDL_EventAction
    {
        /// <summary>Add events to the back of the queue.</summary>
        SDL_ADDEVENT,
        /// <summary>Check but don't remove events from the queue front.</summary>
        SDL_PEEKEVENT,
        /// <summary>Retrieve/remove events from the front of the queue.</summary>
        SDL_GETEVENT
    }

    /// <summary>
    /// Fields shared by every event.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct SDL_CommonEvent
    {
        /// <summary>Event type, shared with all events, Uint32 to cover user events which are not in the SDL_EventType enumeration.</summary>
        public uint type;
        /// <summary>Reserved.</summary>
        public uint reserved;
        /// <summary>In nanoseconds, populated using SDL_GetTicksNS().</summary>
        public ulong timestamp;
    }

    /// <summary>
    /// Display state change event data (event.display.*).
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct SDL_DisplayEvent
    {
        /// <summary>SDL_DISPLAYEVENT_*.</summary>
        public SDL_EventType type;
        /// <summary>Reserved.</summary>
        public uint reserved;
        /// <summary>In nanoseconds, populated using SDL_GetTicksNS().</summary>
        public ulong timestamp;
        /// <summary>The associated display.</summary>
        public SDL_DisplayID displayID;
        /// <summary>Event dependent data.</summary>
        public int data1;
        /// <summary>Event dependent data.</summary>
        public int data2;
    }

    /// <summary>
    /// Window state change event data (event.window.*).
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct SDL_WindowEvent
    {
        /// <summary>SDL_EVENT_WINDOW_*.</summary>
        public SDL_EventType type;
        /// <summary>Reserved.</summary>
        public uint reserved;
        /// <summary>In nanoseconds, populated using SDL_GetTicksNS().</summary>
        public ulong timestamp;
        /// <summary>The associated window.</summary>
        public SDL_WindowID windowID;
        /// <summary>Event dependent data.</summary>
        public int data1;
        /// <summary>Event dependent data.</summary>
        public int data2;
    }

    /// <summary>
    /// Keyboard device event structure (event.kdevice.*).
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct SDL_KeyboardDeviceEvent
    {
        /// <summary>SDL_EVENT_KEYBOARD_ADDED or SDL_EVENT_KEYBOARD_REMOVED.</summary>
        public SDL_EventType type;
        /// <summary>Reserved.</summary>
        public uint reserved;
        /// <summary>In nanoseconds, populated using SDL_GetTicksNS().</summary>
        public ulong timestamp;
        /// <summary>The keyboard instance id.</summary>
        public SDL_KeyboardID which;
    }

    /// <summary>
    /// Keyboard button event structure (event.key.*).
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct SDL_KeyboardEvent
    {
        /// <summary>SDL_EVENT_KEY_DOWN or SDL_EVENT_KEY_UP.</summary>
        public SDL_EventType type;
        /// <summary>Reserved.</summary>
        public uint reserved;
        /// <summary>In nanoseconds, populated using SDL_GetTicksNS().</summary>
        public ulong timestamp;
        /// <summary>The window with keyboard focus, if any.</summary>
        public SDL_WindowID windowID;
        /// <summary>The keyboard instance id, or 0 if unknown or virtual.</summary>
        public SDL_KeyboardID which;
        /// <summary>SDL physical key code.</summary>
        public SDL_Scancode scancode;
        /// <summary>SDL virtual key code.</summary>
        public SDL_Keycode key;
        /// <summary>Current key modifiers.</summary>
        public SDL_Keymod mod;
        /// <summary>The platform dependent scancode for this event.</summary>
        public ushort raw;
        /// <summary>True if the key is pressed.</summary>
        [MarshalAs(UnmanagedType.U1)]
        public bool down;
        /// <summary>True if this is a key repeat.</summary>
        [MarshalAs(UnmanagedType.U1)]
        public bool repeat;
    }

    /// <summary>
    /// Keyboard text editing event structure (event.edit.*).
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct SDL_TextEditingEvent
    {
        /// <summary>SDL_EVENT_TEXT_EDITING.</summary>
        public SDL_EventType type;
        /// <summary>Reserved.</summary>
        public uint reserved;
        /// <summary>In nanoseconds, populated using SDL_GetTicksNS().</summary>
        public ulong timestamp;
        /// <summary>The window with keyboard focus, if any.</summary>
        public SDL_WindowID windowID;
        /// <summary>The editing text.</summary>
        public byte* text;
        /// <summary>The start cursor of selected editing text, or -1 if not set.</summary>
        public int start;
        /// <summary>The length of selected editing text, or -1 if not set.</summary>
        public int length;
    }

    /// <summary>
    /// Keyboard IME candidates event structure (event.edit_candidates.*).
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct SDL_TextEditingCandidatesEvent
    {
        /// <summary>SDL_EVENT_TEXT_EDITING_CANDIDATES.</summary>
        public SDL_EventType type;
        /// <summary>Reserved.</summary>
        public uint reserved;
        /// <summary>In nanoseconds, populated using SDL_GetTicksNS().</summary>
        public ulong timestamp;
        /// <summary>The window with keyboard focus, if any.</summary>
        public SDL_WindowID windowID;
        /// <summary>The list of candidates, or NULL if there are no candidates available.</summary>
        public byte** candidates;
        /// <summary>The number of strings in candidates.</summary>
        public int num_candidates;
        /// <summary>The index of the selected candidate, or -1 if no candidate is selected.</summary>
        public int selected_candidate;
        /// <summary>True if the list is horizontal, false if it's vertical.</summary>
        [MarshalAs(UnmanagedType.U1)]
        public bool horizontal;
        /// <summary>Padding.</summary>
        public byte padding1;
        /// <summary>Padding.</summary>
        public byte padding2;
        /// <summary>Padding.</summary>
        public byte padding3;
    }

    /// <summary>
    /// Keyboard text input event structure (event.text.*).
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct SDL_TextInputEvent
    {
        /// <summary>SDL_EVENT_TEXT_INPUT.</summary>
        public SDL_EventType type;
        /// <summary>Reserved.</summary>
        public uint reserved;
        /// <summary>In nanoseconds, populated using SDL_GetTicksNS().</summary>
        public ulong timestamp;
        /// <summary>The window with keyboard focus, if any.</summary>
        public SDL_WindowID windowID;
        /// <summary>The input text, UTF-8 encoded.</summary>
        public byte* text;
    }

    /// <summary>
    /// Mouse device event structure (event.mdevice.*).
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct SDL_MouseDeviceEvent
    {
        /// <summary>SDL_EVENT_MOUSE_ADDED or SDL_EVENT_MOUSE_REMOVED.</summary>
        public SDL_EventType type;
        /// <summary>Reserved.</summary>
        public uint reserved;
        /// <summary>In nanoseconds, populated using SDL_GetTicksNS().</summary>
        public ulong timestamp;
        /// <summary>The mouse instance id.</summary>
        public SDL_MouseID which;
    }

    /// <summary>
    /// Mouse motion event structure (event.motion.*).
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct SDL_MouseMotionEvent
    {
        /// <summary>SDL_EVENT_MOUSE_MOTION.</summary>
        public SDL_EventType type;
        /// <summary>Reserved.</summary>
        public uint reserved;
        /// <summary>In nanoseconds, populated using SDL_GetTicksNS().</summary>
        public ulong timestamp;
        /// <summary>The window with mouse focus, if any.</summary>
        public SDL_WindowID windowID;
        /// <summary>The mouse instance id in relative mode, SDL_TOUCH_MOUSEID for touch events, or 0.</summary>
        public SDL_MouseID which;
        /// <summary>The current button state.</summary>
        public SDL_MouseButtonFlags state;
        /// <summary>X coordinate, relative to window.</summary>
        public float x;
        /// <summary>Y coordinate, relative to window.</summary>
        public float y;
        /// <summary>The relative motion in the X direction.</summary>
        public float xrel;
        /// <summary>The relative motion in the Y direction.</summary>
        public float yrel;
    }

    /// <summary>
    /// Mouse button event structure (event.button.*).
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct SDL_MouseButtonEvent
    {
        /// <summary>SDL_EVENT_MOUSE_BUTTON_DOWN or SDL_EVENT_MOUSE_BUTTON_UP.</summary>
        public SDL_EventType type;
        /// <summary>Reserved.</summary>
        public uint reserved;
        /// <summary>In nanoseconds, populated using SDL_GetTicksNS().</summary>
        public ulong timestamp;
        /// <summary>The window with mouse focus, if any.</summary>
        public SDL_WindowID windowID;
        /// <summary>The mouse instance id in relative mode, SDL_TOUCH_MOUSEID for touch events, or 0.</summary>
        public SDL_MouseID which;
        /// <summary>The mouse button index.</summary>
        public byte button;
        /// <summary>True if the button is pressed.</summary>
        [MarshalAs(UnmanagedType.U1)]
        public bool down;
        /// <summary>1 for single-click, 2 for double-click, etc.</summary>
        public byte clicks;
        /// <summary>Padding.</summary>
        public byte padding;
        /// <summary>X coordinate, relative to window.</summary>
        public float x;
        /// <summary>Y coordinate, relative to window.</summary>
        public float y;
    }

    /// <summary>
    /// Mouse wheel event structure (event.wheel.*).
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct SDL_MouseWheelEvent
    {
        /// <summary>SDL_EVENT_MOUSE_WHEEL.</summary>
        public SDL_EventType type;
        /// <summary>Reserved.</summary>
        public uint reserved;
        /// <summary>In nanoseconds, populated using SDL_GetTicksNS().</summary>
        public ulong timestamp;
        /// <summary>The window with mouse focus, if any.</summary>
        public SDL_WindowID windowID;
        /// <summary>The mouse instance id in relative mode or 0.</summary>
        public SDL_MouseID which;
        /// <summary>The amount scrolled horizontally, positive to the right and negative to the left.</summary>
        public float x;
        /// <summary>The amount scrolled vertically, positive away from the user and negative toward the user.</summary>
        public float y;
        /// <summary>Set to one of the SDL_MOUSEWHEEL_* defines. When FLIPPED the values in X and Y will be opposite.</summary>
        public SDL_MouseWheelDirection direction;
        /// <summary>X coordinate, relative to window.</summary>
        public float mouse_x;
        /// <summary>Y coordinate, relative to window.</summary>
        public float mouse_y;
        /// <summary>The amount scrolled horizontally, accumulated to whole scroll "ticks".</summary>
        public int integer_x;
        /// <summary>The amount scrolled vertically, accumulated to whole scroll "ticks".</summary>
        public int integer_y;
    }

    /// <summary>
    /// Joystick axis motion event structure (event.jaxis.*).
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct SDL_JoyAxisEvent
    {
        /// <summary>SDL_EVENT_JOYSTICK_AXIS_MOTION.</summary>
        public SDL_EventType type;
        /// <summary>Reserved.</summary>
        public uint reserved;
        /// <summary>In nanoseconds, populated using SDL_GetTicksNS().</summary>
        public ulong timestamp;
        /// <summary>The joystick instance id.</summary>
        public SDL_JoystickID which;
        /// <summary>The joystick axis index.</summary>
        public byte axis;
        /// <summary>Padding.</summary>
        public byte padding1;
        /// <summary>Padding.</summary>
        public byte padding2;
        /// <summary>Padding.</summary>
        public byte padding3;
        /// <summary>The axis value (range: -32768 to 32767).</summary>
        public short value;
        /// <summary>Padding.</summary>
        public ushort padding4;
    }

    /// <summary>
    /// Joystick trackball motion event structure (event.jball.*).
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct SDL_JoyBallEvent
    {
        /// <summary>SDL_EVENT_JOYSTICK_BALL_MOTION.</summary>
        public SDL_EventType type;
        /// <summary>Reserved.</summary>
        public uint reserved;
        /// <summary>In nanoseconds, populated using SDL_GetTicksNS().</summary>
        public ulong timestamp;
        /// <summary>The joystick instance id.</summary>
        public SDL_JoystickID which;
        /// <summary>The joystick trackball index.</summary>
        public byte ball;
        /// <summary>Padding.</summary>
        public byte padding1;
        /// <summary>Padding.</summary>
        public byte padding2;
        /// <summary>Padding.</summary>
        public byte padding3;
        /// <summary>The relative motion in the X direction.</summary>
        public short xrel;
        /// <summary>The relative motion in the Y direction.</summary>
        public short yrel;
    }

    /// <summary>
    /// Joystick hat position change event structure (event.jhat.*).
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct SDL_JoyHatEvent
    {
        /// <summary>SDL_EVENT_JOYSTICK_HAT_MOTION.</summary>
        public SDL_EventType type;
        /// <summary>Reserved.</summary>
        public uint reserved;
        /// <summary>In nanoseconds, populated using SDL_GetTicksNS().</summary>
        public ulong timestamp;
        /// <summary>The joystick instance id.</summary>
        public SDL_JoystickID which;
        /// <summary>The joystick hat index.</summary>
        public byte hat;
        /// <summary>The hat position value (SDL_HAT_* constants).</summary>
        public byte value;
        /// <summary>Padding.</summary>
        public byte padding1;
        /// <summary>Padding.</summary>
        public byte padding2;
    }

    /// <summary>
    /// Joystick button event structure (event.jbutton.*).
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct SDL_JoyButtonEvent
    {
        /// <summary>SDL_EVENT_JOYSTICK_BUTTON_DOWN or SDL_EVENT_JOYSTICK_BUTTON_UP.</summary>
        public SDL_EventType type;
        /// <summary>Reserved.</summary>
        public uint reserved;
        /// <summary>In nanoseconds, populated using SDL_GetTicksNS().</summary>
        public ulong timestamp;
        /// <summary>The joystick instance id.</summary>
        public SDL_JoystickID which;
        /// <summary>The joystick button index.</summary>
        public byte button;
        /// <summary>True if the button is pressed.</summary>
        [MarshalAs(UnmanagedType.U1)]
        public bool down;
        /// <summary>Padding.</summary>
        public byte padding1;
        /// <summary>Padding.</summary>
        public byte padding2;
    }

    /// <summary>
    /// Joystick device event structure (event.jdevice.*).
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct SDL_JoyDeviceEvent
    {
        /// <summary>SDL_EVENT_JOYSTICK_ADDED or SDL_EVENT_JOYSTICK_REMOVED or SDL_EVENT_JOYSTICK_UPDATE_COMPLETE.</summary>
        public SDL_EventType type;
        /// <summary>Reserved.</summary>
        public uint reserved;
        /// <summary>In nanoseconds, populated using SDL_GetTicksNS().</summary>
        public ulong timestamp;
        /// <summary>The joystick instance id.</summary>
        public SDL_JoystickID which;
    }

    /// <summary>
    /// Joystick battery level change event structure (event.jbattery.*).
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct SDL_JoyBatteryEvent
    {
        /// <summary>SDL_EVENT_JOYSTICK_BATTERY_UPDATED.</summary>
        public SDL_EventType type;
        /// <summary>Reserved.</summary>
        public uint reserved;
        /// <summary>In nanoseconds, populated using SDL_GetTicksNS().</summary>
        public ulong timestamp;
        /// <summary>The joystick instance id.</summary>
        public SDL_JoystickID which;
        /// <summary>The joystick battery state.</summary>
        public SDL_PowerState state;
        /// <summary>The joystick battery percent charge remaining.</summary>
        public int percent;
    }

    /// <summary>
    /// Gamepad axis motion event structure (event.gaxis.*).
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct SDL_GamepadAxisEvent
    {
        /// <summary>SDL_EVENT_GAMEPAD_AXIS_MOTION.</summary>
        public SDL_EventType type;
        /// <summary>Reserved.</summary>
        public uint reserved;
        /// <summary>In nanoseconds, populated using SDL_GetTicksNS().</summary>
        public ulong timestamp;
        /// <summary>The joystick instance id.</summary>
        public SDL_JoystickID which;
        /// <summary>The gamepad axis (SDL_GamepadAxis).</summary>
        public byte axis;
        /// <summary>Padding.</summary>
        public byte padding1;
        /// <summary>Padding.</summary>
        public byte padding2;
        /// <summary>Padding.</summary>
        public byte padding3;
        /// <summary>The axis value (range: -32768 to 32767).</summary>
        public short value;
        /// <summary>Padding.</summary>
        public ushort padding4;
    }

    /// <summary>
    /// Gamepad button event structure (event.gbutton.*).
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct SDL_GamepadButtonEvent
    {
        /// <summary>SDL_EVENT_GAMEPAD_BUTTON_DOWN or SDL_EVENT_GAMEPAD_BUTTON_UP.</summary>
        public SDL_EventType type;
        /// <summary>Reserved.</summary>
        public uint reserved;
        /// <summary>In nanoseconds, populated using SDL_GetTicksNS().</summary>
        public ulong timestamp;
        /// <summary>The joystick instance id.</summary>
        public SDL_JoystickID which;
        /// <summary>The gamepad button (SDL_GamepadButton).</summary>
        public byte button;
        /// <summary>True if the button is pressed.</summary>
        [MarshalAs(UnmanagedType.U1)]
        public bool down;
        /// <summary>Padding.</summary>
        public byte padding1;
        /// <summary>Padding.</summary>
        public byte padding2;
    }

    /// <summary>
    /// Gamepad device event structure (event.gdevice.*).
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct SDL_GamepadDeviceEvent
    {
        /// <summary>SDL_EVENT_GAMEPAD_ADDED, SDL_EVENT_GAMEPAD_REMOVED, or SDL_EVENT_GAMEPAD_REMAPPED, SDL_EVENT_GAMEPAD_UPDATE_COMPLETE or SDL_EVENT_GAMEPAD_STEAM_HANDLE_UPDATED.</summary>
        public SDL_EventType type;
        /// <summary>Reserved.</summary>
        public uint reserved;
        /// <summary>In nanoseconds, populated using SDL_GetTicksNS().</summary>
        public ulong timestamp;
        /// <summary>The joystick instance id.</summary>
        public SDL_JoystickID which;
    }

    /// <summary>
    /// Gamepad touchpad event structure (event.gtouchpad.*).
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct SDL_GamepadTouchpadEvent
    {
        /// <summary>SDL_EVENT_GAMEPAD_TOUCHPAD_DOWN or SDL_EVENT_GAMEPAD_TOUCHPAD_MOTION or SDL_EVENT_GAMEPAD_TOUCHPAD_UP.</summary>
        public SDL_EventType type;
        /// <summary>Reserved.</summary>
        public uint reserved;
        /// <summary>In nanoseconds, populated using SDL_GetTicksNS().</summary>
        public ulong timestamp;
        /// <summary>The joystick instance id.</summary>
        public SDL_JoystickID which;
        /// <summary>The index of the touchpad.</summary>
        public int touchpad;
        /// <summary>The index of the finger on the touchpad.</summary>
        public int finger;
        /// <summary>Normalized in the range 0...1 with 0 being on the left.</summary>
        public float x;
        /// <summary>Normalized in the range 0...1 with 0 being at the top.</summary>
        public float y;
        /// <summary>Normalized in the range 0...1.</summary>
        public float pressure;
    }

    /// <summary>
    /// Gamepad sensor event structure (event.gsensor.*).
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct SDL_GamepadSensorEvent
    {
        /// <summary>SDL_EVENT_GAMEPAD_SENSOR_UPDATE.</summary>
        public SDL_EventType type;
        /// <summary>Reserved.</summary>
        public uint reserved;
        /// <summary>In nanoseconds, populated using SDL_GetTicksNS().</summary>
        public ulong timestamp;
        /// <summary>The joystick instance id.</summary>
        public SDL_JoystickID which;
        /// <summary>The type of the sensor, one of the values of SDL_SensorType.</summary>
        public int sensor;
        /// <summary>Up to 3 values from the sensor, as defined in SDL_sensor.h.</summary>
        public fixed float data[3];
        /// <summary>The timestamp of the sensor reading in nanoseconds, not necessarily synchronized with the system clock.</summary>
        public ulong sensor_timestamp;
    }

    /// <summary>
    /// Audio device event structure (event.adevice.*).
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct SDL_AudioDeviceEvent
    {
        /// <summary>SDL_EVENT_AUDIO_DEVICE_ADDED, or SDL_EVENT_AUDIO_DEVICE_REMOVED, or SDL_EVENT_AUDIO_DEVICE_FORMAT_CHANGED.</summary>
        public SDL_EventType type;
        /// <summary>Reserved.</summary>
        public uint reserved;
        /// <summary>In nanoseconds, populated using SDL_GetTicksNS().</summary>
        public ulong timestamp;
        /// <summary>SDL_AudioDeviceID for the device being added or removed or changing.</summary>
        public SDL_AudioDeviceID which;
        /// <summary>False if a playback device, true if a recording device.</summary>
        [MarshalAs(UnmanagedType.U1)]
        public bool recording;
        /// <summary>Padding.</summary>
        public byte padding1;
        /// <summary>Padding.</summary>
        public byte padding2;
        /// <summary>Padding.</summary>
        public byte padding3;
    }

    /// <summary>
    /// Camera device event structure (event.cdevice.*).
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct SDL_CameraDeviceEvent
    {
        /// <summary>SDL_EVENT_CAMERA_DEVICE_ADDED, SDL_EVENT_CAMERA_DEVICE_REMOVED, SDL_EVENT_CAMERA_DEVICE_APPROVED, SDL_EVENT_CAMERA_DEVICE_DENIED.</summary>
        public SDL_EventType type;
        /// <summary>Reserved.</summary>
        public uint reserved;
        /// <summary>In nanoseconds, populated using SDL_GetTicksNS().</summary>
        public ulong timestamp;
        /// <summary>SDL_CameraID for the device being added or removed or changing.</summary>
        public SDL_CameraID which;
    }

    /// <summary>
    /// Renderer event structure (event.render.*).
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct SDL_RenderEvent
    {
        /// <summary>SDL_EVENT_RENDER_TARGETS_RESET, SDL_EVENT_RENDER_DEVICE_RESET, SDL_EVENT_RENDER_DEVICE_LOST.</summary>
        public SDL_EventType type;
        /// <summary>Reserved.</summary>
        public uint reserved;
        /// <summary>In nanoseconds, populated using SDL_GetTicksNS().</summary>
        public ulong timestamp;
        /// <summary>The window containing the renderer in question.</summary>
        public SDL_WindowID windowID;
    }

    /// <summary>
    /// Touch finger event structure (event.tfinger.*).
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct SDL_TouchFingerEvent
    {
        /// <summary>SDL_EVENT_FINGER_DOWN, SDL_EVENT_FINGER_UP, SDL_EVENT_FINGER_MOTION, or SDL_EVENT_FINGER_CANCELED.</summary>
        public SDL_EventType type;
        /// <summary>Reserved.</summary>
        public uint reserved;
        /// <summary>In nanoseconds, populated using SDL_GetTicksNS().</summary>
        public ulong timestamp;
        /// <summary>The touch device id.</summary>
        public SDL_TouchID touchID;
        /// <summary>The finger id.</summary>
        public SDL_FingerID fingerID;
        /// <summary>Normalized in the range 0...1.</summary>
        public float x;
        /// <summary>Normalized in the range 0...1.</summary>
        public float y;
        /// <summary>Normalized in the range -1...1.</summary>
        public float dx;
        /// <summary>Normalized in the range -1...1.</summary>
        public float dy;
        /// <summary>Normalized in the range 0...1.</summary>
        public float pressure;
        /// <summary>The window underneath the finger, if any.</summary>
        public SDL_WindowID windowID;
    }

    /// <summary>
    /// Pressure-sensitive pen proximity event structure (event.pproximity.*).
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct SDL_PenProximityEvent
    {
        /// <summary>SDL_EVENT_PEN_PROXIMITY_IN or SDL_EVENT_PEN_PROXIMITY_OUT.</summary>
        public SDL_EventType type;
        /// <summary>Reserved.</summary>
        public uint reserved;
        /// <summary>In nanoseconds, populated using SDL_GetTicksNS().</summary>
        public ulong timestamp;
        /// <summary>The window with pen focus, if any.</summary>
        public SDL_WindowID windowID;
        /// <summary>The pen instance id.</summary>
        public SDL_PenID which;
    }

    /// <summary>
    /// Pressure-sensitive pen motion event structure (event.pmotion.*).
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct SDL_PenMotionEvent
    {
        /// <summary>SDL_EVENT_PEN_MOTION.</summary>
        public SDL_EventType type;
        /// <summary>Reserved.</summary>
        public uint reserved;
        /// <summary>In nanoseconds, populated using SDL_GetTicksNS().</summary>
        public ulong timestamp;
        /// <summary>The window with pen focus, if any.</summary>
        public SDL_WindowID windowID;
        /// <summary>The pen instance id.</summary>
        public SDL_PenID which;
        /// <summary>Complete pen input state at time of event.</summary>
        public SDL_PenInputFlags pen_state;
        /// <summary>X coordinate, relative to window.</summary>
        public float x;
        /// <summary>Y coordinate, relative to window.</summary>
        public float y;
    }

    /// <summary>
    /// Pressure-sensitive pen touched event structure (event.ptouch.*).
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct SDL_PenTouchEvent
    {
        /// <summary>SDL_EVENT_PEN_DOWN or SDL_EVENT_PEN_UP.</summary>
        public SDL_EventType type;
        /// <summary>Reserved.</summary>
        public uint reserved;
        /// <summary>In nanoseconds, populated using SDL_GetTicksNS().</summary>
        public ulong timestamp;
        /// <summary>The window with pen focus, if any.</summary>
        public SDL_WindowID windowID;
        /// <summary>The pen instance id.</summary>
        public SDL_PenID which;
        /// <summary>Complete pen input state at time of event.</summary>
        public SDL_PenInputFlags pen_state;
        /// <summary>X coordinate, relative to window.</summary>
        public float x;
        /// <summary>Y coordinate, relative to window.</summary>
        public float y;
        /// <summary>True if eraser end is used (not all pens support this).</summary>
        [MarshalAs(UnmanagedType.U1)]
        public bool eraser;
        /// <summary>True if the pen is touching or false if the pen is lifted off.</summary>
        [MarshalAs(UnmanagedType.U1)]
        public bool down;
    }

    /// <summary>
    /// Pressure-sensitive pen button event structure (event.pbutton.*).
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct SDL_PenButtonEvent
    {
        /// <summary>SDL_EVENT_PEN_BUTTON_DOWN or SDL_EVENT_PEN_BUTTON_UP.</summary>
        public SDL_EventType type;
        /// <summary>Reserved.</summary>
        public uint reserved;
        /// <summary>In nanoseconds, populated using SDL_GetTicksNS().</summary>
        public ulong timestamp;
        /// <summary>The window with mouse focus, if any.</summary>
        public SDL_WindowID windowID;
        /// <summary>The pen instance id.</summary>
        public SDL_PenID which;
        /// <summary>Complete pen input state at time of event.</summary>
        public SDL_PenInputFlags pen_state;
        /// <summary>X coordinate, relative to window.</summary>
        public float x;
        /// <summary>Y coordinate, relative to window.</summary>
        public float y;
        /// <summary>The pen button index (first button is 1).</summary>
        public byte button;
        /// <summary>True if the button is pressed.</summary>
        [MarshalAs(UnmanagedType.U1)]
        public bool down;
    }

    /// <summary>
    /// Pressure-sensitive pen pressure / angle event structure (event.paxis.*).
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct SDL_PenAxisEvent
    {
        /// <summary>SDL_EVENT_PEN_AXIS.</summary>
        public SDL_EventType type;
        /// <summary>Reserved.</summary>
        public uint reserved;
        /// <summary>In nanoseconds, populated using SDL_GetTicksNS().</summary>
        public ulong timestamp;
        /// <summary>The window with pen focus, if any.</summary>
        public SDL_WindowID windowID;
        /// <summary>The pen instance id.</summary>
        public SDL_PenID which;
        /// <summary>Complete pen input state at time of event.</summary>
        public SDL_PenInputFlags pen_state;
        /// <summary>X coordinate, relative to window.</summary>
        public float x;
        /// <summary>Y coordinate, relative to window.</summary>
        public float y;
        /// <summary>Axis that has changed.</summary>
        public SDL_PenAxis axis;
        /// <summary>New value of axis.</summary>
        public float value;
    }

    /// <summary>
    /// An event used to drop text or request a file open by the system (event.drop.*).
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct SDL_DropEvent
    {
        /// <summary>SDL_EVENT_DROP_BEGIN or SDL_EVENT_DROP_FILE or SDL_EVENT_DROP_TEXT or SDL_EVENT_DROP_COMPLETE or SDL_EVENT_DROP_POSITION.</summary>
        public SDL_EventType type;
        /// <summary>Reserved.</summary>
        public uint reserved;
        /// <summary>In nanoseconds, populated using SDL_GetTicksNS().</summary>
        public ulong timestamp;
        /// <summary>The window that was dropped on, if any.</summary>
        public SDL_WindowID windowID;
        /// <summary>X coordinate, relative to window (not on begin).</summary>
        public float x;
        /// <summary>Y coordinate, relative to window (not on begin).</summary>
        public float y;
        /// <summary>The source app that sent this drop event, or NULL if that isn't available.</summary>
        public byte* source;
        /// <summary>The text for SDL_EVENT_DROP_TEXT and the file name for SDL_EVENT_DROP_FILE, NULL for other events.</summary>
        public byte* data;
    }

    /// <summary>
    /// An event triggered when the clipboard contents have changed (event.clipboard.*).
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct SDL_ClipboardEvent
    {
        /// <summary>SDL_EVENT_CLIPBOARD_UPDATE.</summary>
        public SDL_EventType type;
        /// <summary>Reserved.</summary>
        public uint reserved;
        /// <summary>In nanoseconds, populated using SDL_GetTicksNS().</summary>
        public ulong timestamp;
        /// <summary>Are we owning the clipboard (internal update).</summary>
        [MarshalAs(UnmanagedType.U1)]
        public bool owner;
        /// <summary>Number of mime types.</summary>
        public int num_mime_types;
        /// <summary>Current mime types.</summary>
        public byte** mime_types;
    }

    /// <summary>
    /// Sensor event structure (event.sensor.*).
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct SDL_SensorEvent
    {
        /// <summary>SDL_EVENT_SENSOR_UPDATE.</summary>
        public SDL_EventType type;
        /// <summary>Reserved.</summary>
        public uint reserved;
        /// <summary>In nanoseconds, populated using SDL_GetTicksNS().</summary>
        public ulong timestamp;
        /// <summary>The instance ID of the sensor.</summary>
        public SDL_SensorID which;
        /// <summary>Up to 6 values from the sensor - additional values can be queried using SDL_GetSensorData().</summary>
        public fixed float data[6];
        /// <summary>The timestamp of the sensor reading in nanoseconds, not necessarily synchronized with the system clock.</summary>
        public ulong sensor_timestamp;
    }

    /// <summary>
    /// The "quit requested" event.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct SDL_QuitEvent
    {
        /// <summary>SDL_EVENT_QUIT.</summary>
        public SDL_EventType type;
        /// <summary>Reserved.</summary>
        public uint reserved;
        /// <summary>In nanoseconds, populated using SDL_GetTicksNS().</summary>
        public ulong timestamp;
    }

    /// <summary>
    /// A user-defined event type (event.user.*).
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct SDL_UserEvent
    {
        /// <summary>SDL_EVENT_USER through SDL_EVENT_LAST-1, Uint32 because these are not in the SDL_EventType enumeration.</summary>
        public uint type;
        /// <summary>Reserved.</summary>
        public uint reserved;
        /// <summary>In nanoseconds, populated using SDL_GetTicksNS().</summary>
        public ulong timestamp;
        /// <summary>The associated window if any.</summary>
        public SDL_WindowID windowID;
        /// <summary>User defined event code.</summary>
        public int code;
        /// <summary>User defined data pointer.</summary>
        public void* data1;
        /// <summary>User defined data pointer.</summary>
        public void* data2;
    }

    /// <summary>
    /// The structure for all events in SDL.
    /// The SDL_Event structure is the core of all event handling in SDL. SDL_Event is a union of all event structures used in SDL.
    /// </summary>
    [StructLayout(LayoutKind.Explicit, Size = 128)]
    public unsafe struct SDL_Event
    {
        /// <summary>Event type, shared with all events, Uint32 to cover user events which are not in the SDL_EventType enumeration.</summary>
        [FieldOffset(0)]
        public uint type;
        /// <summary>Common event data.</summary>
        [FieldOffset(0)]
        public SDL_CommonEvent common;
        /// <summary>Display event data.</summary>
        [FieldOffset(0)]
        public SDL_DisplayEvent display;
        /// <summary>Window event data.</summary>
        [FieldOffset(0)]
        public SDL_WindowEvent window;
        /// <summary>Keyboard device change event data.</summary>
        [FieldOffset(0)]
        public SDL_KeyboardDeviceEvent kdevice;
        /// <summary>Keyboard event data.</summary>
        [FieldOffset(0)]
        public SDL_KeyboardEvent key;
        /// <summary>Text editing event data.</summary>
        [FieldOffset(0)]
        public SDL_TextEditingEvent edit;
        /// <summary>Text editing candidates event data.</summary>
        [FieldOffset(0)]
        public SDL_TextEditingCandidatesEvent edit_candidates;
        /// <summary>Text input event data.</summary>
        [FieldOffset(0)]
        public SDL_TextInputEvent text;
        /// <summary>Mouse device change event data.</summary>
        [FieldOffset(0)]
        public SDL_MouseDeviceEvent mdevice;
        /// <summary>Mouse motion event data.</summary>
        [FieldOffset(0)]
        public SDL_MouseMotionEvent motion;
        /// <summary>Mouse button event data.</summary>
        [FieldOffset(0)]
        public SDL_MouseButtonEvent button;
        /// <summary>Mouse wheel event data.</summary>
        [FieldOffset(0)]
        public SDL_MouseWheelEvent wheel;
        /// <summary>Joystick device change event data.</summary>
        [FieldOffset(0)]
        public SDL_JoyDeviceEvent jdevice;
        /// <summary>Joystick axis event data.</summary>
        [FieldOffset(0)]
        public SDL_JoyAxisEvent jaxis;
        /// <summary>Joystick ball event data.</summary>
        [FieldOffset(0)]
        public SDL_JoyBallEvent jball;
        /// <summary>Joystick hat event data.</summary>
        [FieldOffset(0)]
        public SDL_JoyHatEvent jhat;
        /// <summary>Joystick button event data.</summary>
        [FieldOffset(0)]
        public SDL_JoyButtonEvent jbutton;
        /// <summary>Joystick battery event data.</summary>
        [FieldOffset(0)]
        public SDL_JoyBatteryEvent jbattery;
        /// <summary>Gamepad device event data.</summary>
        [FieldOffset(0)]
        public SDL_GamepadDeviceEvent gdevice;
        /// <summary>Gamepad axis event data.</summary>
        [FieldOffset(0)]
        public SDL_GamepadAxisEvent gaxis;
        /// <summary>Gamepad button event data.</summary>
        [FieldOffset(0)]
        public SDL_GamepadButtonEvent gbutton;
        /// <summary>Gamepad touchpad event data.</summary>
        [FieldOffset(0)]
        public SDL_GamepadTouchpadEvent gtouchpad;
        /// <summary>Gamepad sensor event data.</summary>
        [FieldOffset(0)]
        public SDL_GamepadSensorEvent gsensor;
        /// <summary>Audio device event data.</summary>
        [FieldOffset(0)]
        public SDL_AudioDeviceEvent adevice;
        /// <summary>Camera device event data.</summary>
        [FieldOffset(0)]
        public SDL_CameraDeviceEvent cdevice;
        /// <summary>Sensor event data.</summary>
        [FieldOffset(0)]
        public SDL_SensorEvent sensor;
        /// <summary>Quit request event data.</summary>
        [FieldOffset(0)]
        public SDL_QuitEvent quit;
        /// <summary>Custom event data.</summary>
        [FieldOffset(0)]
        public SDL_UserEvent user;
        /// <summary>Touch finger event data.</summary>
        [FieldOffset(0)]
        public SDL_TouchFingerEvent tfinger;
        /// <summary>Pen proximity event data.</summary>
        [FieldOffset(0)]
        public SDL_PenProximityEvent pproximity;
        /// <summary>Pen tip touching event data.</summary>
        [FieldOffset(0)]
        public SDL_PenTouchEvent ptouch;
        /// <summary>Pen motion event data.</summary>
        [FieldOffset(0)]
        public SDL_PenMotionEvent pmotion;
        /// <summary>Pen button event data.</summary>
        [FieldOffset(0)]
        public SDL_PenButtonEvent pbutton;
        /// <summary>Pen axis event data.</summary>
        [FieldOffset(0)]
        public SDL_PenAxisEvent paxis;
        /// <summary>Render event data.</summary>
        [FieldOffset(0)]
        public SDL_RenderEvent render;
        /// <summary>Drag and drop event data.</summary>
        [FieldOffset(0)]
        public SDL_DropEvent drop;
        /// <summary>Clipboard event data.</summary>
        [FieldOffset(0)]
        public SDL_ClipboardEvent clipboard;
        /// <summary>Padding to ensure the struct is 128 bytes for ABI compatibility.</summary>
        [FieldOffset(0)]
        public fixed byte padding[128];
    }

    /// <summary>
    /// Pump the event loop, gathering events from the input devices.
    /// This function updates the event queue and internal input device state.
    /// </summary>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SDL_PumpEvents();

    /// <summary>
    /// Check the event queue for messages and optionally return them.
    /// </summary>
    /// <param name="events">Destination buffer for the retrieved events, may be NULL to leave the events in the queue and return the number of events that would have been stored.</param>
    /// <param name="numevents">If action is SDL_ADDEVENT, the number of events to add back to the event queue; if action is SDL_PEEKEVENT or SDL_GETEVENT, the maximum number of events to retrieve.</param>
    /// <param name="action">Action to take.</param>
    /// <param name="minType">Minimum value of the event type to be considered; SDL_EVENT_FIRST is a safe choice.</param>
    /// <param name="maxType">Maximum value of the event type to be considered; SDL_EVENT_LAST is a safe choice.</param>
    /// <returns>The number of events actually stored or -1 on failure; call SDL_GetError() for more information.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int SDL_PeepEvents(SDL_Event* events, int numevents, SDL_EventAction action, uint minType, uint maxType);

    /// <summary>
    /// Check for the existence of a certain event type in the event queue.
    /// </summary>
    /// <param name="type">The type of event to be queried; see SDL_EventType for details.</param>
    /// <returns>True if events matching type are present, or false if events matching type are not present.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_HasEvent(uint type);

    /// <summary>
    /// Check for the existence of certain event types in the event queue.
    /// </summary>
    /// <param name="minType">The low end of event type to be queried, inclusive; see SDL_EventType for details.</param>
    /// <param name="maxType">The high end of event type to be queried, inclusive; see SDL_EventType for details.</param>
    /// <returns>True if events with type >= minType and &lt;= maxType are present, or false if not.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_HasEvents(uint minType, uint maxType);

    /// <summary>
    /// Clear events of a specific type from the event queue.
    /// </summary>
    /// <param name="type">The type of event to be cleared; see SDL_EventType for details.</param>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SDL_FlushEvent(uint type);

    /// <summary>
    /// Clear events of a range of types from the event queue.
    /// </summary>
    /// <param name="minType">The low end of event type to be cleared, inclusive; see SDL_EventType for details.</param>
    /// <param name="maxType">The high end of event type to be cleared, inclusive; see SDL_EventType for details.</param>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SDL_FlushEvents(uint minType, uint maxType);

    /// <summary>
    /// Poll for currently pending events.
    /// </summary>
    /// <param name="event">The SDL_Event structure to be filled with the next event from the queue, or NULL.</param>
    /// <returns>True if this got an event or false if there are none available.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_PollEvent(SDL_Event* @event);

    /// <summary>
    /// Wait indefinitely for the next available event.
    /// </summary>
    /// <param name="event">The SDL_Event structure to be filled in with the next event from the queue, or NULL.</param>
    /// <returns>True on success or false if there was an error while waiting for events; call SDL_GetError() for more information.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_WaitEvent(SDL_Event* @event);

    /// <summary>
    /// Wait until the specified timeout (in milliseconds) for the next available event.
    /// </summary>
    /// <param name="event">The SDL_Event structure to be filled in with the next event from the queue, or NULL.</param>
    /// <param name="timeoutMS">The maximum number of milliseconds to wait for the next available event.</param>
    /// <returns>True if this got an event or false if the timeout elapsed without any events available.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_WaitEventTimeout(SDL_Event* @event, int timeoutMS);

    /// <summary>
    /// Add an event to the event queue.
    /// </summary>
    /// <param name="event">The SDL_Event to be added to the queue.</param>
    /// <returns>True on success, false if the event was filtered or on failure; call SDL_GetError() for more information.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_PushEvent(SDL_Event* @event);

    /// <summary>
    /// Set up a filter to process all events before they are added to the internal event queue.
    /// </summary>
    /// <param name="filter">An SDL_EventFilter function to call when an event happens.</param>
    /// <param name="userdata">A pointer that is passed to filter.</param>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SDL_SetEventFilter(delegate* unmanaged[Cdecl]<nuint, SDL_Event*, bool> filter, nuint userdata);

    /// <summary>
    /// Query the current event filter.
    /// </summary>
    /// <param name="filter">The current callback function will be stored here.</param>
    /// <param name="userdata">The pointer that is passed to the current event filter will be stored here.</param>
    /// <returns>True on success or false if there is no event filter set.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_GetEventFilter(delegate* unmanaged[Cdecl]<nuint, SDL_Event*, bool>* filter, nuint* userdata);

    /// <summary>
    /// Add a callback to be triggered when an event is added to the event queue.
    /// </summary>
    /// <param name="filter">An SDL_EventFilter function to call when an event happens.</param>
    /// <param name="userdata">A pointer that is passed to filter.</param>
    /// <returns>True on success or false on failure; call SDL_GetError() for more information.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_AddEventWatch(delegate* unmanaged[Cdecl]<nuint, SDL_Event*, bool> filter, nuint userdata);

    /// <summary>
    /// Remove an event watch callback added with SDL_AddEventWatch().
    /// </summary>
    /// <param name="filter">The function originally passed to SDL_AddEventWatch().</param>
    /// <param name="userdata">The pointer originally passed to SDL_AddEventWatch().</param>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SDL_RemoveEventWatch(delegate* unmanaged[Cdecl]<nuint, SDL_Event*, bool> filter, nuint userdata);

    /// <summary>
    /// Run a specific filter function on the current event queue, removing any events for which the filter returns false.
    /// </summary>
    /// <param name="filter">The SDL_EventFilter function to call when an event happens.</param>
    /// <param name="userdata">A pointer that is passed to filter.</param>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SDL_FilterEvents(delegate* unmanaged[Cdecl]<nuint, SDL_Event*, bool> filter, nuint userdata);

    /// <summary>
    /// Set the state of processing events by type.
    /// </summary>
    /// <param name="type">The type of event; see SDL_EventType for details.</param>
    /// <param name="enabled">Whether to process the event or not.</param>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SDL_SetEventEnabled(uint type, [MarshalAs(UnmanagedType.U1)] bool enabled);

    /// <summary>
    /// Query the state of processing events by type.
    /// </summary>
    /// <param name="type">The type of event; see SDL_EventType for details.</param>
    /// <returns>True if the event is being processed, false otherwise.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_EventEnabled(uint type);

    /// <summary>
    /// Allocate a set of user-defined events, and return the beginning event number for that set of events.
    /// </summary>
    /// <param name="numevents">The number of events to be allocated.</param>
    /// <returns>The beginning event number, or 0 if numevents is invalid or if there are not enough user-defined events left.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial uint SDL_RegisterEvents(int numevents);

    /// <summary>
    /// Get window associated with an event.
    /// </summary>
    /// <param name="event">An event containing a windowID.</param>
    /// <returns>The associated window on success or NULL if there is none.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL_Window* SDL_GetWindowFromEvent(SDL_Event* @event);
}