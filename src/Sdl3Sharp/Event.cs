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
    private readonly SDL_Event _event;

    /// <summary>
    /// Initializes a new instance of the <see cref="Event"/> struct from a native SDL_Event.
    /// </summary>
    /// <param name="sdlEvent">The native SDL event.</param>
    internal Event(SDL_Event sdlEvent)
    {
        _event = sdlEvent;
    }

    /// <summary>
    /// Gets the type of this event.
    /// </summary>
    public EventType Type => (EventType)_event.type;

    /// <summary>
    /// Gets the timestamp of when this event occurred, in nanoseconds.
    /// </summary>
    public ulong TimestampNs => _event.common.timestamp;

    /// <summary>
    /// Gets the timestamp of when this event occurred.
    /// </summary>
    public TimeSpan Timestamp => TimeSpan.FromMicroseconds(_event.common.timestamp / 1000.0);

    /// <summary>
    /// Gets the raw native event structure for advanced usage.
    /// </summary>
    public SDL_Event Native => _event;

    /// <summary>
    /// Gets the display event data. Only valid when <see cref="Type"/> is a display event.
    /// </summary>
    public DisplayEventData Display => new(_event.display);

    /// <summary>
    /// Gets the window event data. Only valid when <see cref="Type"/> is a window event.
    /// </summary>
    public WindowEventData Window => new(_event.window);

    /// <summary>
    /// Gets the keyboard event data. Only valid when <see cref="Type"/> is <see cref="EventType.KeyDown"/> or <see cref="EventType.KeyUp"/>.
    /// </summary>
    public KeyboardEventData Keyboard => new(_event.key);

    /// <summary>
    /// Gets the text editing event data. Only valid when <see cref="Type"/> is <see cref="EventType.TextEditing"/>.
    /// </summary>
    public TextEditingEventData TextEditing => new(_event.edit);

    /// <summary>
    /// Gets the text input event data. Only valid when <see cref="Type"/> is <see cref="EventType.TextInput"/>.
    /// </summary>
    public TextInputEventData TextInput => new(_event.text);

    /// <summary>
    /// Gets the mouse motion event data. Only valid when <see cref="Type"/> is <see cref="EventType.MouseMotion"/>.
    /// </summary>
    public MouseMotionEventData MouseMotion => new(_event.motion);

    /// <summary>
    /// Gets the mouse button event data. Only valid when <see cref="Type"/> is <see cref="EventType.MouseButtonDown"/> or <see cref="EventType.MouseButtonUp"/>.
    /// </summary>
    public MouseButtonEventData MouseButton => new(_event.button);

    /// <summary>
    /// Gets the mouse wheel event data. Only valid when <see cref="Type"/> is <see cref="EventType.MouseWheel"/>.
    /// </summary>
    public MouseWheelEventData MouseWheel => new(_event.wheel);

    /// <summary>
    /// Gets the joystick axis event data. Only valid when <see cref="Type"/> is <see cref="EventType.JoystickAxisMotion"/>.
    /// </summary>
    public JoyAxisEventData JoyAxis => new(_event.jaxis);

    /// <summary>
    /// Gets the joystick hat event data. Only valid when <see cref="Type"/> is <see cref="EventType.JoystickHatMotion"/>.
    /// </summary>
    public JoyHatEventData JoyHat => new(_event.jhat);

    /// <summary>
    /// Gets the joystick button event data. Only valid when <see cref="Type"/> is <see cref="EventType.JoystickButtonDown"/> or <see cref="EventType.JoystickButtonUp"/>.
    /// </summary>
    public JoyButtonEventData JoyButton => new(_event.jbutton);

    /// <summary>
    /// Gets the joystick device event data. Only valid when <see cref="Type"/> is a joystick device event.
    /// </summary>
    public JoyDeviceEventData JoyDevice => new(_event.jdevice);

    /// <summary>
    /// Gets the gamepad axis event data. Only valid when <see cref="Type"/> is <see cref="EventType.GamepadAxisMotion"/>.
    /// </summary>
    public GamepadAxisEventData GamepadAxis => new(_event.gaxis);

    /// <summary>
    /// Gets the gamepad button event data. Only valid when <see cref="Type"/> is <see cref="EventType.GamepadButtonDown"/> or <see cref="EventType.GamepadButtonUp"/>.
    /// </summary>
    public GamepadButtonEventData GamepadButton => new(_event.gbutton);

    /// <summary>
    /// Gets the gamepad device event data. Only valid when <see cref="Type"/> is a gamepad device event.
    /// </summary>
    public GamepadDeviceEventData GamepadDevice => new(_event.gdevice);

    /// <summary>
    /// Gets the gamepad touchpad event data. Only valid when <see cref="Type"/> is a gamepad touchpad event.
    /// </summary>
    public GamepadTouchpadEventData GamepadTouchpad => new(_event.gtouchpad);

    /// <summary>
    /// Gets the touch finger event data. Only valid when <see cref="Type"/> is a finger event.
    /// </summary>
    public TouchFingerEventData TouchFinger => new(_event.tfinger);

    /// <summary>
    /// Gets the drop event data. Only valid when <see cref="Type"/> is a drop event.
    /// </summary>
    public DropEventData Drop => new(_event.drop);

    /// <summary>
    /// Gets the audio device event data. Only valid when <see cref="Type"/> is an audio device event.
    /// </summary>
    public AudioDeviceEventData AudioDevice => new(_event.adevice);

    /// <summary>
    /// Gets the camera device event data. Only valid when <see cref="Type"/> is a camera device event.
    /// </summary>
    public CameraDeviceEventData CameraDevice => new(_event.cdevice);

    /// <summary>
    /// Gets the sensor event data. Only valid when <see cref="Type"/> is <see cref="EventType.SensorUpdate"/>.
    /// </summary>
    public SensorEventData Sensor => new(_event.sensor);

    /// <summary>
    /// Gets the pen proximity event data. Only valid when <see cref="Type"/> is <see cref="EventType.PenProximityIn"/> or <see cref="EventType.PenProximityOut"/>.
    /// </summary>
    public PenProximityEventData PenProximity => new(_event.pproximity);

    /// <summary>
    /// Gets the pen motion event data. Only valid when <see cref="Type"/> is <see cref="EventType.PenMotion"/>.
    /// </summary>
    public PenMotionEventData PenMotion => new(_event.pmotion);

    /// <summary>
    /// Gets the pen touch event data. Only valid when <see cref="Type"/> is <see cref="EventType.PenDown"/> or <see cref="EventType.PenUp"/>.
    /// </summary>
    public PenTouchEventData PenTouch => new(_event.ptouch);

    /// <summary>
    /// Gets the pen button event data. Only valid when <see cref="Type"/> is <see cref="EventType.PenButtonDown"/> or <see cref="EventType.PenButtonUp"/>.
    /// </summary>
    public PenButtonEventData PenButton => new(_event.pbutton);

    /// <summary>
    /// Gets the pen axis event data. Only valid when <see cref="Type"/> is <see cref="EventType.PenAxis"/>.
    /// </summary>
    public PenAxisEventData PenAxis => new(_event.paxis);

    /// <summary>
    /// Gets the user event data. Only valid when <see cref="Type"/> is a user-defined event.
    /// </summary>
    public UserEventData User => new(_event.user);
}
