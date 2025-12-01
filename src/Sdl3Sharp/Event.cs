using Sdl3Sharp.Audio;
using Sdl3Sharp.Graphics;
using Sdl3Sharp.Input;

using static Sdl3Sharp.Native.Events;

namespace Sdl3Sharp;

/// <summary>
/// Represents an SDL event with typed access to event-specific data.
/// </summary>
/// <remarks>
/// <para>This class provides a managed wrapper around SDL's event system. Events are
/// the core of SDL's input handling - all user interaction and system notifications
/// flow through the event queue.</para>
/// <para>Use <see cref="EventQueue.Poll"/> or <see cref="EventQueue.Wait()"/> to retrieve events,
/// then check the <see cref="Type"/> property and access the appropriate typed data property.</para>
/// </remarks>
public readonly unsafe struct Event
{

    /// <summary>
    /// Initializes a new instance of the <see cref="Event"/> struct from a native SDL_Event.
    /// </summary>
    /// <param name="sdlEvent">The native SDL event.</param>
    internal Event(SDL_Event sdlEvent)
    {
        Native = sdlEvent;
    }

    /// <summary>
    /// Gets the type of this event.
    /// </summary>
    public EventType Type => (EventType)Native.type;

    /// <summary>
    /// Gets the timestamp of when this event occurred, in nanoseconds.
    /// </summary>
    public ulong TimestampNs => Native.common.timestamp;

    /// <summary>
    /// Gets the timestamp of when this event occurred.
    /// </summary>
    public TimeSpan Timestamp => TimeSpan.FromMicroseconds(Native.common.timestamp / 1000.0);

    /// <summary>
    /// Gets the raw native event structure for advanced usage.
    /// </summary>
    public SDL_Event Native { get; }

    /// <summary>
    /// Gets the display event data. Only valid when <see cref="Type"/> is a display event.
    /// </summary>
    public DisplayEventData Display => new(Native.display);

    /// <summary>
    /// Gets the window event data. Only valid when <see cref="Type"/> is a window event.
    /// </summary>
    public WindowEventData Window => new(Native.window);

    /// <summary>
    /// Gets the keyboard event data. Only valid when <see cref="Type"/> is <see cref="EventType.KeyDown"/> or <see cref="EventType.KeyUp"/>.
    /// </summary>
    public KeyboardEventData Keyboard => new(Native.key);

    /// <summary>
    /// Gets the text editing event data. Only valid when <see cref="Type"/> is <see cref="EventType.TextEditing"/>.
    /// </summary>
    public TextEditingEventData TextEditing => new(Native.edit);

    /// <summary>
    /// Gets the text input event data. Only valid when <see cref="Type"/> is <see cref="EventType.TextInput"/>.
    /// </summary>
    public TextInputEventData TextInput => new(Native.text);

    /// <summary>
    /// Gets the mouse motion event data. Only valid when <see cref="Type"/> is <see cref="EventType.MouseMotion"/>.
    /// </summary>
    public MouseMotionEventData MouseMotion => new(Native.motion);

    /// <summary>
    /// Gets the mouse button event data. Only valid when <see cref="Type"/> is <see cref="EventType.MouseButtonDown"/> or <see cref="EventType.MouseButtonUp"/>.
    /// </summary>
    public MouseButtonEventData MouseButton => new(Native.button);

    /// <summary>
    /// Gets the mouse wheel event data. Only valid when <see cref="Type"/> is <see cref="EventType.MouseWheel"/>.
    /// </summary>
    public MouseWheelEventData MouseWheel => new(Native.wheel);

    /// <summary>
    /// Gets the joystick axis event data. Only valid when <see cref="Type"/> is <see cref="EventType.JoystickAxisMotion"/>.
    /// </summary>
    public JoyAxisEventData JoyAxis => new(Native.jaxis);

    /// <summary>
    /// Gets the joystick hat event data. Only valid when <see cref="Type"/> is <see cref="EventType.JoystickHatMotion"/>.
    /// </summary>
    public JoyHatEventData JoyHat => new(Native.jhat);

    /// <summary>
    /// Gets the joystick button event data. Only valid when <see cref="Type"/> is <see cref="EventType.JoystickButtonDown"/> or <see cref="EventType.JoystickButtonUp"/>.
    /// </summary>
    public JoyButtonEventData JoyButton => new(Native.jbutton);

    /// <summary>
    /// Gets the joystick device event data. Only valid when <see cref="Type"/> is a joystick device event.
    /// </summary>
    public JoyDeviceEventData JoyDevice => new(Native.jdevice);

    /// <summary>
    /// Gets the gamepad axis event data. Only valid when <see cref="Type"/> is <see cref="EventType.GamepadAxisMotion"/>.
    /// </summary>
    public GamepadAxisEventData GamepadAxis => new(Native.gaxis);

    /// <summary>
    /// Gets the gamepad button event data. Only valid when <see cref="Type"/> is <see cref="EventType.GamepadButtonDown"/> or <see cref="EventType.GamepadButtonUp"/>.
    /// </summary>
    public GamepadButtonEventData GamepadButton => new(Native.gbutton);

    /// <summary>
    /// Gets the gamepad device event data. Only valid when <see cref="Type"/> is a gamepad device event.
    /// </summary>
    public GamepadDeviceEventData GamepadDevice => new(Native.gdevice);

    /// <summary>
    /// Gets the gamepad touchpad event data. Only valid when <see cref="Type"/> is a gamepad touchpad event.
    /// </summary>
    public GamepadTouchpadEventData GamepadTouchpad => new(Native.gtouchpad);

    /// <summary>
    /// Gets the touch finger event data. Only valid when <see cref="Type"/> is a finger event.
    /// </summary>
    public TouchFingerEventData TouchFinger => new(Native.tfinger);

    /// <summary>
    /// Gets the drop event data. Only valid when <see cref="Type"/> is a drop event.
    /// </summary>
    public DropEventData Drop => new(Native.drop);

    /// <summary>
    /// Gets the audio device event data. Only valid when <see cref="Type"/> is an audio device event.
    /// </summary>
    public AudioDeviceEventData AudioDevice => new(Native.adevice);

    /// <summary>
    /// Gets the camera device event data. Only valid when <see cref="Type"/> is a camera device event.
    /// </summary>
    public CameraDeviceEventData CameraDevice => new(Native.cdevice);

    /// <summary>
    /// Gets the sensor event data. Only valid when <see cref="Type"/> is <see cref="EventType.SensorUpdate"/>.
    /// </summary>
    public SensorEventData Sensor => new(Native.sensor);

    /// <summary>
    /// Gets the pen proximity event data. Only valid when <see cref="Type"/> is <see cref="EventType.PenProximityIn"/> or <see cref="EventType.PenProximityOut"/>.
    /// </summary>
    public PenProximityEventData PenProximity => new(Native.pproximity);

    /// <summary>
    /// Gets the pen motion event data. Only valid when <see cref="Type"/> is <see cref="EventType.PenMotion"/>.
    /// </summary>
    public PenMotionEventData PenMotion => new(Native.pmotion);

    /// <summary>
    /// Gets the pen touch event data. Only valid when <see cref="Type"/> is <see cref="EventType.PenDown"/> or <see cref="EventType.PenUp"/>.
    /// </summary>
    public PenTouchEventData PenTouch => new(Native.ptouch);

    /// <summary>
    /// Gets the pen button event data. Only valid when <see cref="Type"/> is <see cref="EventType.PenButtonDown"/> or <see cref="EventType.PenButtonUp"/>.
    /// </summary>
    public PenButtonEventData PenButton => new(Native.pbutton);

    /// <summary>
    /// Gets the pen axis event data. Only valid when <see cref="Type"/> is <see cref="EventType.PenAxis"/>.
    /// </summary>
    public PenAxisEventData PenAxis => new(Native.paxis);

    /// <summary>
    /// Gets the user event data. Only valid when <see cref="Type"/> is a user-defined event.
    /// </summary>
    public UserEventData User => new(Native.user);
}
