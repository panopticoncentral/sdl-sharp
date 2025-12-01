using Sdl3Sharp.Audio;
using Sdl3Sharp.Graphics;
using Sdl3Sharp.Input;

using static Sdl3Sharp.Native.Events;

namespace Sdl3Sharp;

/// <summary>
/// Represents an SDL event with typed access to event-specific data.
/// </summary>
public readonly unsafe record struct Event
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
    /// Translates a native SDL event structure into a managed event argument object representing the corresponding
    /// event type.
    /// </summary>
    /// <returns>An instance of SdlEventArgs or a derived type that encapsulates the event data for the specified SDL event.</returns>
    public unsafe SdlEventArgs TranslateEvent()
    {
        return Type switch
        {
            EventType.Quit
            or EventType.Terminating
            or EventType.LowMemory
            or EventType.WillEnterBackground
            or EventType.DidEnterBackground
            or EventType.WillEnterForeground
            or EventType.DidEnterForeground
            or EventType.LocaleChanged
            or EventType.SystemThemeChanged
            or EventType.ClipboardUpdate => new SdlEventArgs(Native.common.timestamp),

            EventType.DisplayOrientation
            or EventType.DisplayAdded
            or EventType.DisplayRemoved
            or EventType.DisplayMoved
            or EventType.DisplayDesktopModeChanged
            or EventType.DisplayCurrentModeChanged
            or EventType.DisplayContentScaleChanged => new DisplayOrientationEventArgs(Native.display),

            EventType.WindowShown
            or EventType.WindowHidden
            or EventType.WindowExposed
            or EventType.WindowMetalViewResized
            or EventType.WindowMinimized
            or EventType.WindowMaximized
            or EventType.WindowRestored
            or EventType.WindowMouseEnter
            or EventType.WindowMouseLeave
            or EventType.WindowFocusGained
            or EventType.WindowFocusLost
            or EventType.WindowCloseRequested
            or EventType.WindowHitTest
            or EventType.WindowIccProfileChanged
            or EventType.WindowDisplayScaleChanged
            or EventType.WindowSafeAreaChanged
            or EventType.WindowOccluded
            or EventType.WindowEnterFullscreen
            or EventType.WindowLeaveFullscreen
            or EventType.WindowDestroyed
            or EventType.WindowHdrStateChanged => new WindowEventArgs(Native.window),
            EventType.WindowMoved => new WindowMovedEventArgs(Native.window),
            EventType.WindowResized
            or EventType.WindowPixelSizeChanged => new WindowResizedEventArgs(Native.window),
            EventType.WindowDisplayChanged => new WindowDisplayChangedEventArgs(Native.window),
            EventType.KeyDown
            or EventType.KeyUp => new KeyboardEventArgs(Native.key),
            EventType.TextEditing => new TextEditingEventArgs(Native.edit),
            EventType.TextInput => new TextInputEventArgs(Native.text),
            EventType.KeymapChanged => new SdlEventArgs(Native.common.timestamp),
            EventType.KeyboardAdded
            or EventType.KeyboardRemoved => new KeyboardDeviceEventArgs(Native.kdevice),
            EventType.MouseMotion => new MouseMotionEventArgs(Native.motion),
            EventType.MouseButtonDown
            or EventType.MouseButtonUp => new MouseButtonEventArgs(Native.button),
            EventType.MouseWheel => new MouseWheelEventArgs(Native.wheel),
            EventType.MouseAdded
            or EventType.MouseRemoved => new MouseDeviceEventArgs(Native.mdevice),
            EventType.JoystickAxisMotion => new JoystickAxisEventArgs(Native.jaxis),
            EventType.JoystickBallMotion => new JoystickBallEventArgs(Native.jball),
            EventType.JoystickHatMotion => new JoystickHatEventArgs(Native.jhat),
            EventType.JoystickButtonDown
            or EventType.JoystickButtonUp => new JoystickButtonEventArgs(Native.jbutton),
            EventType.JoystickAdded
            or EventType.JoystickRemoved
            or EventType.JoystickUpdateComplete => new JoystickDeviceEventArgs(Native.jdevice),
            EventType.JoystickBatteryUpdated => new JoystickBatteryEventArgs(Native.jbattery),
            EventType.GamepadAxisMotion => new GamepadAxisEventArgs(Native.gaxis),
            EventType.GamepadButtonDown
            or EventType.GamepadButtonUp => new GamepadButtonEventArgs(Native.gbutton),
            EventType.GamepadAdded
            or EventType.GamepadRemoved
            or EventType.GamepadRemapped
            or EventType.GamepadUpdateComplete
            or EventType.GamepadSteamHandleUpdated => new GamepadDeviceEventArgs(Native.gdevice),
            EventType.GamepadTouchpadDown
            or EventType.GamepadTouchpadMotion
            or EventType.GamepadTouchpadUp => new GamepadTouchpadEventArgs(Native.gtouchpad),
            EventType.GamepadSensorUpdate => new GamepadSensorEventArgs(Native.gsensor),
            EventType.FingerDown
            or EventType.FingerUp
            or EventType.FingerMotion
            or EventType.FingerCanceled => new TouchFingerEventArgs(Native.tfinger),
            EventType.DropFile
            or EventType.DropText
            or EventType.DropBegin
            or EventType.DropComplete
            or EventType.DropPosition => new DropEventArgs(Native.drop),
            EventType.AudioDeviceAdded
            or EventType.AudioDeviceRemoved
            or EventType.AudioDeviceFormatChanged => new AudioDeviceEventArgs(Native.adevice),
            EventType.SensorUpdate => new SensorEventArgs(Native.sensor),
            EventType.PenProximityIn
            or EventType.PenProximityOut => new PenProximityEventArgs(Native.pproximity),
            EventType.PenDown
            or EventType.PenUp => new PenTouchEventArgs(Native.ptouch),
            EventType.PenButtonDown
            or EventType.PenButtonUp => new PenButtonEventArgs(Native.pbutton),
            EventType.PenMotion => new PenMotionEventArgs(Native.pmotion),
            EventType.PenAxis => new PenAxisEventArgs(Native.paxis),
            EventType.CameraDeviceAdded
            or EventType.CameraDeviceRemoved
            or EventType.CameraDeviceApproved
            or EventType.CameraDeviceDenied => new CameraDeviceEventArgs(Native.cdevice),
            EventType.RenderTargetsReset
            or EventType.RenderDeviceReset
            or EventType.RenderDeviceLost => new RenderEventArgs(Native.common.timestamp),
            _ => throw new InvalidDataException($"Unsupported event type: {Type}"),
        };
    }
}
