using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
// ReSharper disable InconsistentNaming

namespace SdlSharp.Native;

// Every SDL_events.h event struct is bound below. Payload-free event types (for
// example SDL_EVENT_KEYMAP_CHANGED, SDL_EVENT_SCREEN_KEYBOARD_SHOWN/HIDDEN) carry
// no dedicated struct — they arrive as SDL_CommonEvent.

/// <summary>
/// The types of events that can be delivered.
/// </summary>
public enum SDL_EventType : uint
{
    SDL_EVENT_FIRST = 0,

    // Application events
    SDL_EVENT_QUIT = 0x100,
    SDL_EVENT_TERMINATING,
    SDL_EVENT_LOW_MEMORY,
    SDL_EVENT_WILL_ENTER_BACKGROUND,
    SDL_EVENT_DID_ENTER_BACKGROUND,
    SDL_EVENT_WILL_ENTER_FOREGROUND,
    SDL_EVENT_DID_ENTER_FOREGROUND,
    SDL_EVENT_LOCALE_CHANGED,
    SDL_EVENT_SYSTEM_THEME_CHANGED,

    // Display events
    SDL_EVENT_DISPLAY_ORIENTATION = 0x151,
    SDL_EVENT_DISPLAY_ADDED,
    SDL_EVENT_DISPLAY_REMOVED,
    SDL_EVENT_DISPLAY_MOVED,
    SDL_EVENT_DISPLAY_DESKTOP_MODE_CHANGED,
    SDL_EVENT_DISPLAY_CURRENT_MODE_CHANGED,
    SDL_EVENT_DISPLAY_CONTENT_SCALE_CHANGED,
    SDL_EVENT_DISPLAY_USABLE_BOUNDS_CHANGED,
    SDL_EVENT_DISPLAY_FIRST = SDL_EVENT_DISPLAY_ORIENTATION,
    SDL_EVENT_DISPLAY_LAST = SDL_EVENT_DISPLAY_USABLE_BOUNDS_CHANGED,

    // Window events
    SDL_EVENT_WINDOW_SHOWN = 0x202,
    SDL_EVENT_WINDOW_HIDDEN,
    SDL_EVENT_WINDOW_EXPOSED,
    SDL_EVENT_WINDOW_MOVED,
    SDL_EVENT_WINDOW_RESIZED,
    SDL_EVENT_WINDOW_PIXEL_SIZE_CHANGED,
    SDL_EVENT_WINDOW_METAL_VIEW_RESIZED,
    SDL_EVENT_WINDOW_MINIMIZED,
    SDL_EVENT_WINDOW_MAXIMIZED,
    SDL_EVENT_WINDOW_RESTORED,
    SDL_EVENT_WINDOW_MOUSE_ENTER,
    SDL_EVENT_WINDOW_MOUSE_LEAVE,
    SDL_EVENT_WINDOW_FOCUS_GAINED,
    SDL_EVENT_WINDOW_FOCUS_LOST,
    SDL_EVENT_WINDOW_CLOSE_REQUESTED,
    SDL_EVENT_WINDOW_HIT_TEST,
    SDL_EVENT_WINDOW_ICCPROF_CHANGED,
    SDL_EVENT_WINDOW_DISPLAY_CHANGED,
    SDL_EVENT_WINDOW_DISPLAY_SCALE_CHANGED,
    SDL_EVENT_WINDOW_SAFE_AREA_CHANGED,
    SDL_EVENT_WINDOW_OCCLUDED,
    SDL_EVENT_WINDOW_ENTER_FULLSCREEN,
    SDL_EVENT_WINDOW_LEAVE_FULLSCREEN,
    SDL_EVENT_WINDOW_DESTROYED,
    SDL_EVENT_WINDOW_HDR_STATE_CHANGED,
    SDL_EVENT_WINDOW_FIRST = SDL_EVENT_WINDOW_SHOWN,
    SDL_EVENT_WINDOW_LAST = SDL_EVENT_WINDOW_HDR_STATE_CHANGED,

    // Keyboard events
    SDL_EVENT_KEY_DOWN = 0x300,
    SDL_EVENT_KEY_UP,
    SDL_EVENT_TEXT_EDITING,
    SDL_EVENT_TEXT_INPUT,
    SDL_EVENT_KEYMAP_CHANGED,
    SDL_EVENT_KEYBOARD_ADDED,
    SDL_EVENT_KEYBOARD_REMOVED,
    SDL_EVENT_TEXT_EDITING_CANDIDATES,
    SDL_EVENT_SCREEN_KEYBOARD_SHOWN,
    SDL_EVENT_SCREEN_KEYBOARD_HIDDEN,

    // Mouse events
    SDL_EVENT_MOUSE_MOTION = 0x400,
    SDL_EVENT_MOUSE_BUTTON_DOWN,
    SDL_EVENT_MOUSE_BUTTON_UP,
    SDL_EVENT_MOUSE_WHEEL,
    SDL_EVENT_MOUSE_ADDED,
    SDL_EVENT_MOUSE_REMOVED,

    // Joystick events
    SDL_EVENT_JOYSTICK_AXIS_MOTION = 0x600,
    SDL_EVENT_JOYSTICK_BALL_MOTION,
    SDL_EVENT_JOYSTICK_HAT_MOTION,
    SDL_EVENT_JOYSTICK_BUTTON_DOWN,
    SDL_EVENT_JOYSTICK_BUTTON_UP,
    SDL_EVENT_JOYSTICK_ADDED,
    SDL_EVENT_JOYSTICK_REMOVED,
    SDL_EVENT_JOYSTICK_BATTERY_UPDATED,
    SDL_EVENT_JOYSTICK_UPDATE_COMPLETE,

    // Gamepad events
    SDL_EVENT_GAMEPAD_AXIS_MOTION = 0x650,
    SDL_EVENT_GAMEPAD_BUTTON_DOWN,
    SDL_EVENT_GAMEPAD_BUTTON_UP,
    SDL_EVENT_GAMEPAD_ADDED,
    SDL_EVENT_GAMEPAD_REMOVED,
    SDL_EVENT_GAMEPAD_REMAPPED,
    SDL_EVENT_GAMEPAD_TOUCHPAD_DOWN,
    SDL_EVENT_GAMEPAD_TOUCHPAD_MOTION,
    SDL_EVENT_GAMEPAD_TOUCHPAD_UP,
    SDL_EVENT_GAMEPAD_SENSOR_UPDATE,
    SDL_EVENT_GAMEPAD_UPDATE_COMPLETE,
    SDL_EVENT_GAMEPAD_STEAM_HANDLE_UPDATED,

    // Touch events
    SDL_EVENT_FINGER_DOWN = 0x700,
    SDL_EVENT_FINGER_UP,
    SDL_EVENT_FINGER_MOTION,
    SDL_EVENT_FINGER_CANCELED,

    // Pinch events
    SDL_EVENT_PINCH_BEGIN = 0x710,
    SDL_EVENT_PINCH_UPDATE,
    SDL_EVENT_PINCH_END,

    // Clipboard events
    SDL_EVENT_CLIPBOARD_UPDATE = 0x900,

    // Drag and drop events
    SDL_EVENT_DROP_FILE = 0x1000,
    SDL_EVENT_DROP_TEXT,
    SDL_EVENT_DROP_BEGIN,
    SDL_EVENT_DROP_COMPLETE,
    SDL_EVENT_DROP_POSITION,

    // Audio hotplug events
    SDL_EVENT_AUDIO_DEVICE_ADDED = 0x1100,
    SDL_EVENT_AUDIO_DEVICE_REMOVED,
    SDL_EVENT_AUDIO_DEVICE_FORMAT_CHANGED,

    // Sensor events
    SDL_EVENT_SENSOR_UPDATE = 0x1200,

    // Pen events
    SDL_EVENT_PEN_PROXIMITY_IN = 0x1300,
    SDL_EVENT_PEN_PROXIMITY_OUT,
    SDL_EVENT_PEN_DOWN,
    SDL_EVENT_PEN_UP,
    SDL_EVENT_PEN_BUTTON_DOWN,
    SDL_EVENT_PEN_BUTTON_UP,
    SDL_EVENT_PEN_MOTION,
    SDL_EVENT_PEN_AXIS,

    // Camera events
    SDL_EVENT_CAMERA_DEVICE_ADDED = 0x1400,
    SDL_EVENT_CAMERA_DEVICE_REMOVED,
    SDL_EVENT_CAMERA_DEVICE_APPROVED,
    SDL_EVENT_CAMERA_DEVICE_DENIED,

    // Render events
    SDL_EVENT_RENDER_TARGETS_RESET = 0x2000,
    SDL_EVENT_RENDER_DEVICE_RESET,
    SDL_EVENT_RENDER_DEVICE_LOST,

    // Internal events
    SDL_EVENT_POLL_SENTINEL = 0x7F00,

    // User events
    SDL_EVENT_USER = 0x8000,
    SDL_EVENT_LAST = 0xFFFF,
}

/// <summary>
/// Fields shared by every event.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public struct SDL_CommonEvent
{
    public uint type;
    public uint reserved;
    public ulong timestamp;
}

/// <summary>
/// Display state change event data.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public struct SDL_DisplayEvent
{
    public SDL_EventType type;
    public uint reserved;
    public ulong timestamp;
    public SDL_DisplayID displayID;
    public int data1;
    public int data2;
}

/// <summary>
/// Window state change event data.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public struct SDL_WindowEvent
{
    public SDL_EventType type;
    public uint reserved;
    public ulong timestamp;
    public SDL_WindowID windowID;
    public int data1;
    public int data2;
}

/// <summary>
/// Keyboard device event structure.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public struct SDL_KeyboardDeviceEvent
{
    public SDL_EventType type;
    public uint reserved;
    public ulong timestamp;
    public uint which;
}

/// <summary>
/// Keyboard button event structure.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public struct SDL_KeyboardEvent
{
    public SDL_EventType type;
    public uint reserved;
    public ulong timestamp;
    public SDL_WindowID windowID;
    public uint which;
    public SDL_Scancode scancode;
    public SDL_Keycode key;
    public SDL_Keymod mod;
    public ushort raw;
    public byte down;
    public byte repeat;
}

/// <summary>
/// Keyboard text editing event structure.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public unsafe struct SDL_TextEditingEvent
{
    public SDL_EventType type;
    public uint reserved;
    public ulong timestamp;
    public SDL_WindowID windowID;
    public byte* text;
    public int start;
    public int length;
}

/// <summary>
/// Keyboard text input event structure.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public unsafe struct SDL_TextInputEvent
{
    public SDL_EventType type;
    public uint reserved;
    public ulong timestamp;
    public SDL_WindowID windowID;
    public byte* text;
}

/// <summary>
/// Mouse device event structure.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public struct SDL_MouseDeviceEvent
{
    public SDL_EventType type;
    public uint reserved;
    public ulong timestamp;
    public uint which;
}

/// <summary>
/// Mouse motion event structure.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public struct SDL_MouseMotionEvent
{
    public SDL_EventType type;
    public uint reserved;
    public ulong timestamp;
    public SDL_WindowID windowID;
    public uint which;
    public uint state;
    public float x;
    public float y;
    public float xrel;
    public float yrel;
}

/// <summary>
/// Mouse button event structure.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public struct SDL_MouseButtonEvent
{
    public SDL_EventType type;
    public uint reserved;
    public ulong timestamp;
    public SDL_WindowID windowID;
    public uint which;
    public byte button;
    public byte down;
    public byte clicks;
    public byte padding;
    public float x;
    public float y;
}

/// <summary>
/// Mouse wheel event structure.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public struct SDL_MouseWheelEvent
{
    public SDL_EventType type;
    public uint reserved;
    public ulong timestamp;
    public SDL_WindowID windowID;
    public uint which;
    public float x;
    public float y;
    public SDL_MouseWheelDirection direction;
    public float mouse_x;
    public float mouse_y;
    public int integer_x;
    public int integer_y;
}

/// <summary>
/// Joystick axis motion event structure.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public struct SDL_JoyAxisEvent
{
    public SDL_EventType type;
    public uint reserved;
    public ulong timestamp;
    public SDL_JoystickID which;
    public byte axis;
    public byte padding1;
    public byte padding2;
    public byte padding3;
    public short value;
    public ushort padding4;
}

/// <summary>
/// Joystick trackball motion event structure.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public struct SDL_JoyBallEvent
{
    public SDL_EventType type;
    public uint reserved;
    public ulong timestamp;
    public SDL_JoystickID which;
    public byte ball;
    public byte padding1;
    public byte padding2;
    public byte padding3;
    public short xrel;
    public short yrel;
}

/// <summary>
/// Joystick hat position change event structure.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public struct SDL_JoyHatEvent
{
    public SDL_EventType type;
    public uint reserved;
    public ulong timestamp;
    public SDL_JoystickID which;
    public byte hat;
    public byte value;
    public byte padding1;
    public byte padding2;
}

/// <summary>
/// Joystick button event structure.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public struct SDL_JoyButtonEvent
{
    public SDL_EventType type;
    public uint reserved;
    public ulong timestamp;
    public SDL_JoystickID which;
    public byte button;
    public byte down;
    public byte padding1;
    public byte padding2;
}

/// <summary>
/// Joystick device event structure.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public struct SDL_JoyDeviceEvent
{
    public SDL_EventType type;
    public uint reserved;
    public ulong timestamp;
    public SDL_JoystickID which;
}

/// <summary>
/// Joystick battery level change event structure.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public struct SDL_JoyBatteryEvent
{
    public SDL_EventType type;
    public uint reserved;
    public ulong timestamp;
    public SDL_JoystickID which;
    public SDL_PowerState state;
    public int percent;
}

/// <summary>
/// Power state enum (from SDL_power.h).
/// </summary>
public enum SDL_PowerState
{
    SDL_POWERSTATE_ERROR = -1,
    SDL_POWERSTATE_UNKNOWN,
    SDL_POWERSTATE_ON_BATTERY,
    SDL_POWERSTATE_NO_BATTERY,
    SDL_POWERSTATE_CHARGING,
    SDL_POWERSTATE_CHARGED,
}

/// <summary>
/// Gamepad axis motion event structure.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public struct SDL_GamepadAxisEvent
{
    public SDL_EventType type;
    public uint reserved;
    public ulong timestamp;
    public SDL_JoystickID which;
    public byte axis;
    public byte padding1;
    public byte padding2;
    public byte padding3;
    public short value;
    public ushort padding4;
}

/// <summary>
/// Gamepad button event structure.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public struct SDL_GamepadButtonEvent
{
    public SDL_EventType type;
    public uint reserved;
    public ulong timestamp;
    public SDL_JoystickID which;
    public byte button;
    public byte down;
    public byte padding1;
    public byte padding2;
}

/// <summary>
/// Gamepad device event structure.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public struct SDL_GamepadDeviceEvent
{
    public SDL_EventType type;
    public uint reserved;
    public ulong timestamp;
    public SDL_JoystickID which;
}

/// <summary>
/// Gamepad touchpad event structure.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public struct SDL_GamepadTouchpadEvent
{
    public SDL_EventType type;
    public uint reserved;
    public ulong timestamp;
    public SDL_JoystickID which;
    public int touchpad;
    public int finger;
    public float x;
    public float y;
    public float pressure;
}

/// <summary>
/// Gamepad sensor event structure.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public unsafe struct SDL_GamepadSensorEvent
{
    public SDL_EventType type;
    public uint reserved;
    public ulong timestamp;
    public SDL_JoystickID which;
    public int sensor;
    public fixed float data[3];
    public ulong sensor_timestamp;
}

/// <summary>
/// Touch finger event structure.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public struct SDL_TouchFingerEvent
{
    public SDL_EventType type;
    public uint reserved;
    public ulong timestamp;
    public ulong touchID;
    public ulong fingerID;
    public float x;
    public float y;
    public float dx;
    public float dy;
    public float pressure;
    public SDL_WindowID windowID;
}

/// <summary>
/// Pinch event structure.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public struct SDL_PinchFingerEvent
{
    public SDL_EventType type;
    public uint reserved;
    public ulong timestamp;
    public float scale;
    public SDL_WindowID windowID;
}

/// <summary>
/// Pen proximity event structure.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public struct SDL_PenProximityEvent
{
    public SDL_EventType type;
    public uint reserved;
    public ulong timestamp;
    public SDL_WindowID windowID;
    public uint which;
}

/// <summary>
/// Pen motion event structure.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public struct SDL_PenMotionEvent
{
    public SDL_EventType type;
    public uint reserved;
    public ulong timestamp;
    public SDL_WindowID windowID;
    public uint which;
    public uint pen_state;
    public float x;
    public float y;
}

/// <summary>
/// Pen touch event structure.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public struct SDL_PenTouchEvent
{
    public SDL_EventType type;
    public uint reserved;
    public ulong timestamp;
    public SDL_WindowID windowID;
    public uint which;
    public uint pen_state;
    public float x;
    public float y;
    public byte eraser;
    public byte down;
}

/// <summary>
/// Pen button event structure.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public struct SDL_PenButtonEvent
{
    public SDL_EventType type;
    public uint reserved;
    public ulong timestamp;
    public SDL_WindowID windowID;
    public uint which;
    public uint pen_state;
    public float x;
    public float y;
    public byte button;
    public byte down;
}

/// <summary>
/// Pen axis event structure.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public struct SDL_PenAxisEvent
{
    public SDL_EventType type;
    public uint reserved;
    public ulong timestamp;
    public SDL_WindowID windowID;
    public uint which;
    public uint pen_state;
    public float x;
    public float y;
    public SDL_PenAxis axis;
    public float value;
}

/// <summary>
/// Drop event structure.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public unsafe struct SDL_DropEvent
{
    public SDL_EventType type;
    public uint reserved;
    public ulong timestamp;
    public SDL_WindowID windowID;
    public float x;
    public float y;
    public byte* source;
    public byte* data;
}

/// <summary>
/// Clipboard event structure.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public unsafe struct SDL_ClipboardEvent
{
    public SDL_EventType type;
    public uint reserved;
    public ulong timestamp;
    public byte owner;
    public int num_mime_types;
    public byte** mime_types;
}

/// <summary>
/// Sensor event structure.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public unsafe struct SDL_SensorEvent
{
    public SDL_EventType type;
    public uint reserved;
    public ulong timestamp;
    public uint which;
    public fixed float data[6];
    public ulong sensor_timestamp;
}

/// <summary>
/// Quit request event.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public struct SDL_QuitEvent
{
    public SDL_EventType type;
    public uint reserved;
    public ulong timestamp;
}

/// <summary>
/// User-defined event type.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public unsafe struct SDL_UserEvent
{
    public uint type;
    public uint reserved;
    public ulong timestamp;
    public SDL_WindowID windowID;
    public int code;
    public void* data1;
    public void* data2;
}

/// <summary>
/// Render event structure.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public struct SDL_RenderEvent
{
    public SDL_EventType type;
    public uint reserved;
    public ulong timestamp;
    public SDL_WindowID windowID;
}

/// <summary>
/// Audio device hotplug event structure.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public struct SDL_AudioDeviceEvent
{
    public SDL_EventType type;
    public uint reserved;
    public ulong timestamp;
    public SDL_AudioDeviceID which;
    public byte recording;
    public byte padding1;
    public byte padding2;
    public byte padding3;
}

/// <summary>
/// Camera device hotplug/permission event structure.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public struct SDL_CameraDeviceEvent
{
    public SDL_EventType type;
    public uint reserved;
    public ulong timestamp;
    public SDL_CameraID which;
}

/// <summary>
/// Keyboard IME candidates event structure.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public unsafe struct SDL_TextEditingCandidatesEvent
{
    public SDL_EventType type;
    public uint reserved;
    public ulong timestamp;
    public SDL_WindowID windowID;
    public byte** candidates;
    public int num_candidates;
    public int selected_candidate;
    public byte horizontal;
    public byte padding1;
    public byte padding2;
    public byte padding3;
}

/// <summary>
/// The structure for all events in SDL.
/// </summary>
[StructLayout(LayoutKind.Explicit, Size = 128)]
public unsafe struct SDL_Event
{
    [FieldOffset(0)] public uint type;
    [FieldOffset(0)] public SDL_CommonEvent common;
    [FieldOffset(0)] public SDL_DisplayEvent display;
    [FieldOffset(0)] public SDL_WindowEvent window;
    [FieldOffset(0)] public SDL_KeyboardDeviceEvent kdevice;
    [FieldOffset(0)] public SDL_KeyboardEvent key;
    [FieldOffset(0)] public SDL_TextEditingEvent edit;
    [FieldOffset(0)] public SDL_TextInputEvent text;
    [FieldOffset(0)] public SDL_MouseDeviceEvent mdevice;
    [FieldOffset(0)] public SDL_MouseMotionEvent motion;
    [FieldOffset(0)] public SDL_MouseButtonEvent button;
    [FieldOffset(0)] public SDL_MouseWheelEvent wheel;
    [FieldOffset(0)] public SDL_JoyDeviceEvent jdevice;
    [FieldOffset(0)] public SDL_JoyAxisEvent jaxis;
    [FieldOffset(0)] public SDL_JoyBallEvent jball;
    [FieldOffset(0)] public SDL_JoyHatEvent jhat;
    [FieldOffset(0)] public SDL_JoyButtonEvent jbutton;
    [FieldOffset(0)] public SDL_JoyBatteryEvent jbattery;
    [FieldOffset(0)] public SDL_GamepadDeviceEvent gdevice;
    [FieldOffset(0)] public SDL_GamepadAxisEvent gaxis;
    [FieldOffset(0)] public SDL_GamepadButtonEvent gbutton;
    [FieldOffset(0)] public SDL_GamepadTouchpadEvent gtouchpad;
    [FieldOffset(0)] public SDL_GamepadSensorEvent gsensor;
    [FieldOffset(0)] public SDL_SensorEvent sensor;
    [FieldOffset(0)] public SDL_QuitEvent quit;
    [FieldOffset(0)] public SDL_UserEvent user;
    [FieldOffset(0)] public SDL_TouchFingerEvent tfinger;
    [FieldOffset(0)] public SDL_PinchFingerEvent pinch;
    [FieldOffset(0)] public SDL_PenProximityEvent pproximity;
    [FieldOffset(0)] public SDL_PenTouchEvent ptouch;
    [FieldOffset(0)] public SDL_PenMotionEvent pmotion;
    [FieldOffset(0)] public SDL_PenButtonEvent pbutton;
    [FieldOffset(0)] public SDL_PenAxisEvent paxis;
    [FieldOffset(0)] public SDL_RenderEvent render;
    [FieldOffset(0)] public SDL_DropEvent drop;
    [FieldOffset(0)] public SDL_ClipboardEvent clipboard;
    [FieldOffset(0)] public SDL_AudioDeviceEvent adevice;
    [FieldOffset(0)] public SDL_CameraDeviceEvent cdevice;
    [FieldOffset(0)] public SDL_TextEditingCandidatesEvent editCandidates;
}

/// <summary>
/// Native bindings for SDL_events.h.
/// </summary>
[SuppressMessage("Interoperability", "CA1401:P/Invokes should not be visible")]
public static partial class Events
{
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_PumpEvents")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SDL_PumpEvents();

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_PollEvent")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_PollEvent(SDL_Event* @event);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_WaitEvent")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_WaitEvent(SDL_Event* @event);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_WaitEventTimeout")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_WaitEventTimeout(SDL_Event* @event, int timeoutMS);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_PushEvent")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_PushEvent(SDL_Event* @event);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_HasEvent")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_HasEvent(uint type);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_HasEvents")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_HasEvents(uint minType, uint maxType);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_FlushEvent")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SDL_FlushEvent(uint type);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_FlushEvents")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SDL_FlushEvents(uint minType, uint maxType);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_SetEventEnabled")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SDL_SetEventEnabled(uint type, [MarshalAs(UnmanagedType.U1)] bool enabled);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_EventEnabled")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_EventEnabled(uint type);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_RegisterEvents")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial uint SDL_RegisterEvents(int numevents);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_AddEventWatch")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_AddEventWatch(
        delegate* unmanaged[Cdecl]<void*, SDL_Event*, byte> filter, void* userdata);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_RemoveEventWatch")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial void SDL_RemoveEventWatch(
        delegate* unmanaged[Cdecl]<void*, SDL_Event*, byte> filter, void* userdata);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_SetEventFilter")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial void SDL_SetEventFilter(
        delegate* unmanaged[Cdecl]<void*, SDL_Event*, byte> filter, void* userdata);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_FilterEvents")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial void SDL_FilterEvents(
        delegate* unmanaged[Cdecl]<void*, SDL_Event*, byte> filter, void* userdata);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetWindowFromEvent")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial SDL_Window* SDL_GetWindowFromEvent(SDL_Event* @event);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetEventDescription")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial int SDL_GetEventDescription(SDL_Event* @event, byte* buf, int buflen);

    // Skipped SDL_PeepEvents (and its SDL_EventAction enum): bulk add/peek/get on a
    // caller-supplied SDL_Event array does not fit the transient-pointer RawEvent
    // model and would require a second managed event representation. The queue is
    // served by SDL_PollEvent/SDL_WaitEvent/SDL_PushEvent/SDL_Has*/SDL_Flush*.

    // Skipped SDL_GetEventFilter: it returns the currently-installed native filter
    // pointer + userdata, which is only meaningful to whoever installed it. The
    // managed Application owns the single filter slot via SetEventFilter, so a getter
    // would only expose an opaque native callback identity.
}
