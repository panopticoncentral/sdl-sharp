using Sdl3Sharp.Graphics;

using static Sdl3Sharp.Native.Common;
using static Sdl3Sharp.Native.StdInc;
using static Sdl3Sharp.Native.Touch;

namespace Sdl3Sharp.Input;

/// <summary>
/// Represents a touch input device.
/// </summary>
public unsafe readonly record struct TouchDevice(ulong Id)
{
    /// <summary>
    /// Occurs when a finger touches the screen.
    /// </summary>
    public static event EventHandler<TouchFingerEventArgs>? FingerDown;

    /// <summary>
    /// Occurs when a finger is lifted from the screen.
    /// </summary>
    public static event EventHandler<TouchFingerEventArgs>? FingerUp;

    /// <summary>
    /// Occurs when a finger moves on the screen.
    /// </summary>
    public static event EventHandler<TouchFingerEventArgs>? FingerMotion;

    /// <summary>
    /// Occurs when a touch is canceled.
    /// </summary>
    public static event EventHandler<TouchFingerEventArgs>? FingerCanceled;

    internal static void DispatchEvent(Event e)
    {
        switch (e.Type)
        {
            case EventType.FingerDown:
                FingerDown?.Invoke(null, (TouchFingerEventArgs)e.TranslateEvent());
                break;
            case EventType.FingerUp:
                FingerUp?.Invoke(null, (TouchFingerEventArgs)e.TranslateEvent());
                break;
            case EventType.FingerMotion:
                FingerMotion?.Invoke(null, (TouchFingerEventArgs)e.TranslateEvent());
                break;
            case EventType.FingerCanceled:
                FingerCanceled?.Invoke(null, (TouchFingerEventArgs)e.TranslateEvent());
                break;
        }
    }

    /// <summary>
    /// The mouse for mouse events simulated with touch input.
    /// Use this to filter out simulated mouse events when processing touch input separately.
    /// </summary>
    public static Mouse TouchMouse => new(SDL_TOUCH_MOUSEID);

    /// <summary>
    /// The touch for touch events simulated with mouse input.
    /// Use this to filter out simulated touch events when processing mouse input separately.
    /// </summary>
    public static TouchDevice MouseTouch => new(SDL_MOUSE_TOUCHID);

    /// <summary>
    /// Gets the touch device name as reported from the driver.
    /// </summary>
    public string Name => CheckErrorNull(SDL_GetTouchDeviceName(Id));

    /// <summary>
    /// Gets the type of this touch device.
    /// </summary>
    public TouchDeviceType DeviceType => (TouchDeviceType)SDL_GetTouchDeviceType(Id);

    /// <summary>
    /// Gets a list of registered touch devices.
    /// On some platforms SDL first sees the touch device if it was actually used.
    /// Therefore the returned list might be empty, although devices are available.
    /// After using all devices at least once the number will be correct.
    /// </summary>
    /// <returns>An array of touch devices.</returns>
    public static TouchDevice[] GetTouchDevices()
    {
        int count;
        SDL_TouchID* devices = CheckErrorPointer(SDL_GetTouchDevices(&count));
        var result = new TouchDevice[count];
        for (var i = 0; i < count; i++)
        {
            result[i] = new TouchDevice(devices[i]);
        }

        SDL_free(devices);
        return result;
    }

    /// <summary>
    /// Gets a list of active fingers for this touch device.
    /// </summary>
    /// <returns>An array of currently active fingers on this touch device.</returns>
    public Finger[] GetFingers()
    {
        int count;
        SDL_Finger** fingers = CheckErrorPointer(SDL_GetTouchFingers(Id, &count));
        var result = new Finger[count];
        for (var i = 0; i < count; i++)
        {
            SDL_Finger* finger = fingers[i];
            result[i] = new Finger(finger->id, new PointF(finger->x, finger->y), finger->pressure);
        }

        SDL_free(fingers);
        return result;
    }
}
