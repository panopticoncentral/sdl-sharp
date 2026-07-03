using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

using SdlSharp.Input;
using static SdlSharp.Native.Common;
using static SdlSharp.Native.Events;
using static SdlSharp.Native.Init;

namespace SdlSharp;

/// <summary>
/// Manages the SDL library lifecycle: initialization, metadata, and shutdown.
/// </summary>
public sealed unsafe class Application : IDisposable
{
    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
    private static void RunOnMainThreadCallback(void* userdata)
    {
        var handle = GCHandle.FromIntPtr((nint)userdata);
        try
        {
            ((Action)handle.Target!)();
        }
        finally
        {
            handle.Free();
        }
    }

    /// <summary>
    /// Initializes SDL with the specified subsystems.
    /// </summary>
    /// <param name="flags">The subsystems to initialize.</param>
    public Application(InitFlags flags)
    {
        Check(SDL_Init((Native.SDL_InitFlags)flags));
    }

    /// <summary>
    /// Initializes additional SDL subsystems.
    /// </summary>
    /// <param name="flags">The additional subsystems to initialize.</param>
    public static void InitSubSystem(InitFlags flags) =>
        Check(SDL_InitSubSystem((Native.SDL_InitFlags)flags));

    /// <summary>
    /// Shuts down specific SDL subsystems.
    /// </summary>
    /// <param name="flags">The subsystems to shut down.</param>
    public static void QuitSubSystem(InitFlags flags) =>
        SDL_QuitSubSystem((Native.SDL_InitFlags)flags);

    /// <summary>
    /// Gets a mask of the currently initialized subsystems.
    /// </summary>
    /// <param name="flags">The subsystems to check, or 0 for all.</param>
    public static InitFlags WasInit(InitFlags flags = 0) =>
        (InitFlags)SDL_WasInit((Native.SDL_InitFlags)flags);

    /// <summary>
    /// Returns whether this is the main thread.
    /// </summary>
    public static bool IsMainThread => SDL_IsMainThread();

    /// <summary>
    /// Gets the current system theme (light, dark, or unknown).
    /// </summary>
    public static Graphics.SystemTheme SystemTheme => (Graphics.SystemTheme)Native.Video.SDL_GetSystemTheme();

    /// <summary>
    /// Runs an action on the main thread during event processing.
    /// If called from the main thread, the action executes immediately.
    /// </summary>
    /// <param name="action">The action to run on the main thread.</param>
    /// <param name="waitComplete">true to block until the action completes, false to return immediately.</param>
    public static void RunOnMainThread(Action action, bool waitComplete = true)
    {
        var handle = GCHandle.Alloc(action);
        if (!SDL_RunOnMainThread(&RunOnMainThreadCallback, (void*)(nint)handle, waitComplete))
        {
            handle.Free();
            throw new SdlException();
        }
    }

    /// <summary>
    /// Sets basic metadata about the application.
    /// </summary>
    /// <param name="name">The name of the application.</param>
    /// <param name="version">The version of the application.</param>
    /// <param name="identifier">A unique reverse-domain identifier.</param>
    public static void SetMetadata(string? name, string? version, string? identifier) =>
        Check(SDL_SetAppMetadata(ToUtf8(name), ToUtf8(version), ToUtf8(identifier)));

    /// <summary>
    /// Sets a specific metadata property.
    /// </summary>
    /// <param name="name">The property name.</param>
    /// <param name="value">The property value, or null to remove.</param>
    public static void SetMetadataProperty(string name, string? value) =>
        Check(SDL_SetAppMetadataProperty(ToUtf8(name), ToUtf8(value)));

    /// <summary>
    /// Gets a metadata property value.
    /// </summary>
    /// <param name="name">The property name.</param>
    /// <returns>The property value, or null if not set.</returns>
    public static string? GetMetadataProperty(string name) =>
        Marshal.PtrToStringUTF8((nint)SDL_GetAppMetadataProperty(ToUtf8(name)));

    /// <summary>
    /// Pumps the event loop, gathering pending input events.
    /// Must be called on the main thread.
    /// </summary>
    public static void PumpEvents() => Native.Events.SDL_PumpEvents();

    /// <summary>
    /// Handles a raw event before typed dispatch.
    /// </summary>
    /// <param name="e">The raw event.</param>
    public delegate void RawEventHandler(in RawEvent e);

    /// <summary>
    /// Raised for every polled event before typed dispatch. The event's
    /// <see cref="RawEvent.Pointer"/> is only valid during the callback.
    /// </summary>
    public static event RawEventHandler? RawEventFilter;

    /// <summary>
    /// A predicate applied to a raw event at queue-insertion time (see
    /// <see cref="SetEventFilter"/> and <see cref="FilterEvents"/>). Return true to keep
    /// the event, false to drop it. The event's <see cref="RawEvent.Pointer"/> is only
    /// valid during the call.
    /// </summary>
    /// <param name="e">The raw event.</param>
    /// <returns>True to keep the event, false to drop it.</returns>
    public delegate bool RawEventPredicate(in RawEvent e);

    private static readonly Dictionary<RawEventHandler, GCHandle> WatchHandles = new();
    private static GCHandle _eventFilterHandle;

    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
    private static byte WatchTrampoline(void* userdata, Native.SDL_Event* e)
    {
        try
        {
            var handler = (RawEventHandler)GCHandle.FromIntPtr((nint)userdata).Target!;
            handler(new RawEvent((EventType)e->type, (nint)e));
        }
        catch
        {
            // A throwing watch must not drop the event or cross the boundary.
        }

        return 1; // Watches ignore the return value; keep the event regardless.
    }

    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
    private static byte FilterTrampoline(void* userdata, Native.SDL_Event* e)
    {
        try
        {
            var predicate = (RawEventPredicate)GCHandle.FromIntPtr((nint)userdata).Target!;
            return predicate(new RawEvent((EventType)e->type, (nint)e)) ? (byte)1 : (byte)0;
        }
        catch
        {
            return 1; // On error keep the event rather than silently dropping it.
        }
    }

    /// <summary>
    /// Registers a callback invoked for every event as it is added to the queue. Watches
    /// may fire from the thread that pushes the event, including during
    /// <see cref="PumpEvents"/> or from SDL-internal threads. The same handler cannot be
    /// added twice.
    /// </summary>
    /// <param name="watch">The watch callback.</param>
    /// <exception cref="ArgumentException">The handler is already registered.</exception>
    public static void AddEventWatch(RawEventHandler watch)
    {
        lock (WatchHandles)
        {
            if (WatchHandles.ContainsKey(watch))
            {
                throw new ArgumentException("This handler is already registered as an event watch.", nameof(watch));
            }

            var handle = GCHandle.Alloc(watch);
            if (!SDL_AddEventWatch(&WatchTrampoline, (void*)GCHandle.ToIntPtr(handle)))
            {
                handle.Free();
                throw new SdlException();
            }

            WatchHandles[watch] = handle;
        }
    }

    /// <summary>
    /// Removes a watch previously registered with <see cref="AddEventWatch"/>. Does nothing
    /// if the handler was not registered.
    /// </summary>
    /// <param name="watch">The watch callback to remove.</param>
    public static void RemoveEventWatch(RawEventHandler watch)
    {
        lock (WatchHandles)
        {
            if (!WatchHandles.TryGetValue(watch, out var handle))
            {
                return;
            }

            SDL_RemoveEventWatch(&WatchTrampoline, (void*)GCHandle.ToIntPtr(handle));
            handle.Free();
            WatchHandles.Remove(watch);
        }
    }

    /// <summary>
    /// Sets or clears (<c>null</c>) the event filter. Unlike <see cref="RawEventFilter"/>
    /// (which observes events during dispatch on the main loop), this filter runs at the
    /// moment an event is added to the queue — possibly on another thread — and dropping
    /// an event here prevents it from ever being queued. Only one filter is installed at a
    /// time; setting a new one replaces the previous.
    /// </summary>
    /// <param name="filter">The filter predicate, or <c>null</c> to clear.</param>
    public static void SetEventFilter(RawEventPredicate? filter)
    {
        var previous = _eventFilterHandle;

        if (filter == null)
        {
            SDL_SetEventFilter(null, null);
            _eventFilterHandle = default;
        }
        else
        {
            var handle = GCHandle.Alloc(filter);
            SDL_SetEventFilter(&FilterTrampoline, (void*)GCHandle.ToIntPtr(handle));
            _eventFilterHandle = handle;
        }

        if (previous.IsAllocated)
        {
            previous.Free();
        }
    }

    /// <summary>
    /// Runs a predicate over every event currently in the queue, removing those for which
    /// it returns false. This is a one-shot sweep and does not install a persistent filter.
    /// </summary>
    /// <param name="predicate">The predicate; return false to remove an event.</param>
    public static void FilterEvents(RawEventPredicate predicate)
    {
        var handle = GCHandle.Alloc(predicate);
        try
        {
            SDL_FilterEvents(&FilterTrampoline, (void*)GCHandle.ToIntPtr(handle));
        }
        finally
        {
            handle.Free();
        }
    }

    /// <summary>Raised when the user requests a quit (e.g. closes the last window).</summary>
    public static event Action<QuitEventArgs>? Quit;

    /// <summary>Raised when a key is pressed.</summary>
    public static event Action<KeyEventArgs>? KeyDown;

    /// <summary>Raised when a key is released.</summary>
    public static event Action<KeyEventArgs>? KeyUp;

    /// <summary>Raised when text is entered via the keyboard.</summary>
    public static event Action<TextInputEventArgs>? TextInput;

    /// <summary>Raised when the mouse moves.</summary>
    public static event Action<MouseMotionEventArgs>? MouseMotion;

    /// <summary>Raised when a mouse button is pressed.</summary>
    public static event Action<MouseButtonEventArgs>? MouseButtonDown;

    /// <summary>Raised when a mouse button is released.</summary>
    public static event Action<MouseButtonEventArgs>? MouseButtonUp;

    /// <summary>Raised when the mouse wheel is scrolled.</summary>
    public static event Action<MouseWheelEventArgs>? MouseWheel;

    /// <summary>Raised for window events (shown, hidden, moved, resized, etc.).</summary>
    public static event Action<WindowEventArgs>? Window;

    /// <summary>Raised for user-defined events (see <see cref="RegisterEvents"/>).</summary>
    public static event Action<UserEventArgs>? UserEvent;

    /// <summary>Raised for display events (orientation, connect/disconnect, moved, etc.).</summary>
    public static event Action<DisplayEventArgs>? Display;

    /// <summary>Raised when a keyboard is connected.</summary>
    public static event Action<InputDeviceEventArgs>? KeyboardAdded;
    /// <summary>Raised when a keyboard is disconnected.</summary>
    public static event Action<InputDeviceEventArgs>? KeyboardRemoved;
    /// <summary>Raised when a mouse is connected.</summary>
    public static event Action<InputDeviceEventArgs>? MouseAdded;
    /// <summary>Raised when a mouse is disconnected.</summary>
    public static event Action<InputDeviceEventArgs>? MouseRemoved;

    /// <summary>Raised while composing text through an IME.</summary>
    public static event Action<TextEditingEventArgs>? TextEditing;
    /// <summary>Raised when the IME candidate list changes.</summary>
    public static event Action<TextEditingCandidatesEventArgs>? TextEditingCandidates;

    /// <summary>Raised on joystick axis motion.</summary>
    public static event Action<JoyAxisEventArgs>? JoystickAxisMotion;
    /// <summary>Raised on joystick trackball motion.</summary>
    public static event Action<JoyBallEventArgs>? JoystickBallMotion;
    /// <summary>Raised on joystick hat motion.</summary>
    public static event Action<JoyHatEventArgs>? JoystickHatMotion;
    /// <summary>Raised when a joystick button is pressed.</summary>
    public static event Action<JoyButtonEventArgs>? JoystickButtonDown;
    /// <summary>Raised when a joystick button is released.</summary>
    public static event Action<JoyButtonEventArgs>? JoystickButtonUp;
    /// <summary>Raised when a joystick is connected.</summary>
    public static event Action<JoyDeviceEventArgs>? JoystickAdded;
    /// <summary>Raised when a joystick is disconnected.</summary>
    public static event Action<JoyDeviceEventArgs>? JoystickRemoved;
    /// <summary>Raised when a joystick finishes an update cycle.</summary>
    public static event Action<JoyDeviceEventArgs>? JoystickUpdateComplete;
    /// <summary>Raised when a joystick battery level changes.</summary>
    public static event Action<JoyBatteryEventArgs>? JoystickBatteryUpdated;

    /// <summary>Raised on gamepad axis motion.</summary>
    public static event Action<GamepadAxisEventArgs>? GamepadAxisMotion;
    /// <summary>Raised when a gamepad button is pressed.</summary>
    public static event Action<GamepadButtonEventArgs>? GamepadButtonDown;
    /// <summary>Raised when a gamepad button is released.</summary>
    public static event Action<GamepadButtonEventArgs>? GamepadButtonUp;
    /// <summary>Raised when a gamepad is connected.</summary>
    public static event Action<GamepadDeviceEventArgs>? GamepadAdded;
    /// <summary>Raised when a gamepad is disconnected.</summary>
    public static event Action<GamepadDeviceEventArgs>? GamepadRemoved;
    /// <summary>Raised when a gamepad mapping is updated.</summary>
    public static event Action<GamepadDeviceEventArgs>? GamepadRemapped;
    /// <summary>Raised when a gamepad finishes an update cycle.</summary>
    public static event Action<GamepadDeviceEventArgs>? GamepadUpdateComplete;
    /// <summary>Raised when a gamepad's Steam handle changes.</summary>
    public static event Action<GamepadDeviceEventArgs>? GamepadSteamHandleUpdated;
    /// <summary>Raised when a gamepad touchpad finger touches down.</summary>
    public static event Action<GamepadTouchpadEventArgs>? GamepadTouchpadDown;
    /// <summary>Raised when a gamepad touchpad finger moves.</summary>
    public static event Action<GamepadTouchpadEventArgs>? GamepadTouchpadMotion;
    /// <summary>Raised when a gamepad touchpad finger lifts.</summary>
    public static event Action<GamepadTouchpadEventArgs>? GamepadTouchpadUp;
    /// <summary>Raised on a gamepad sensor update.</summary>
    public static event Action<GamepadSensorEventArgs>? GamepadSensorUpdate;

    /// <summary>Raised when a finger touches down.</summary>
    public static event Action<TouchFingerEventArgs>? FingerDown;
    /// <summary>Raised when a finger lifts.</summary>
    public static event Action<TouchFingerEventArgs>? FingerUp;
    /// <summary>Raised when a finger moves.</summary>
    public static event Action<TouchFingerEventArgs>? FingerMotion;
    /// <summary>Raised when a finger touch is canceled.</summary>
    public static event Action<TouchFingerEventArgs>? FingerCanceled;
    /// <summary>Raised when a pinch gesture begins.</summary>
    public static event Action<PinchFingerEventArgs>? PinchBegin;
    /// <summary>Raised as a pinch gesture updates.</summary>
    public static event Action<PinchFingerEventArgs>? PinchUpdate;
    /// <summary>Raised when a pinch gesture ends.</summary>
    public static event Action<PinchFingerEventArgs>? PinchEnd;

    /// <summary>Raised when a pen enters proximity.</summary>
    public static event Action<PenProximityEventArgs>? PenProximityIn;
    /// <summary>Raised when a pen leaves proximity.</summary>
    public static event Action<PenProximityEventArgs>? PenProximityOut;
    /// <summary>Raised when a pen touches down.</summary>
    public static event Action<PenTouchEventArgs>? PenDown;
    /// <summary>Raised when a pen lifts.</summary>
    public static event Action<PenTouchEventArgs>? PenUp;
    /// <summary>Raised when a pen moves.</summary>
    public static event Action<PenMotionEventArgs>? PenMotion;
    /// <summary>Raised when a pen button is pressed.</summary>
    public static event Action<PenButtonEventArgs>? PenButtonDown;
    /// <summary>Raised when a pen button is released.</summary>
    public static event Action<PenButtonEventArgs>? PenButtonUp;
    /// <summary>Raised on a pen axis change.</summary>
    public static event Action<PenAxisEventArgs>? PenAxisMotion;

    /// <summary>Raised for each drop event (begin, file, text, position, complete).</summary>
    public static event Action<DropEventArgs>? Drop;

    /// <summary>Raised when the clipboard contents change.</summary>
    public static event Action<ClipboardEventArgs>? ClipboardUpdate;

    /// <summary>Raised on a sensor update.</summary>
    public static event Action<SensorEventArgs>? SensorUpdate;

    /// <summary>Raised on audio-device hotplug (added, removed, format changed).</summary>
    public static event Action<AudioDeviceEventArgs>? AudioDeviceEvent;

    /// <summary>Raised on camera-device hotplug/permission changes.</summary>
    public static event Action<CameraDeviceEventArgs>? CameraDeviceEvent;

    /// <summary>Raised on render target/device reset or loss.</summary>
    public static event Action<RenderEventArgs>? RenderEvent;

    /// <summary>
    /// Allocates a contiguous block of user event types for use with <see cref="PushUserEvent"/>.
    /// </summary>
    /// <param name="count">The number of event types to allocate.</param>
    /// <returns>The first allocated event type; subsequent types are consecutive.</returns>
    /// <exception cref="SdlException">No more user event types are available.</exception>
    public static EventType RegisterEvents(int count)
    {
        var first = SDL_RegisterEvents(count);
        if (first == 0)
        {
            throw new SdlException();
        }

        return (EventType)first;
    }

    /// <summary>
    /// Pushes a user-defined event onto the queue. Register the type with
    /// <see cref="RegisterEvents"/> first. Any memory referenced by <paramref name="data1"/>
    /// or <paramref name="data2"/> is owned by the caller and must outlive the event's dispatch.
    /// </summary>
    /// <param name="type">A type obtained from <see cref="RegisterEvents"/>.</param>
    /// <param name="code">A user-defined code.</param>
    /// <param name="data1">A user-defined pointer, or 0.</param>
    /// <param name="data2">A user-defined pointer, or 0.</param>
    /// <returns>True if the event was queued; false if a filter dropped it.</returns>
    public static bool PushUserEvent(EventType type, int code, nint data1 = 0, nint data2 = 0)
    {
        var e = new Native.SDL_Event
        {
            user = new Native.SDL_UserEvent
            {
                type = (uint)type,
                code = code,
                data1 = (void*)data1,
                data2 = (void*)data2,
            }
        };

        return SDL_PushEvent(&e);
    }

    /// <summary>
    /// Polls all pending events and dispatches them to the appropriate event handlers.
    /// Must be called on the main thread. Returns true if a quit event was received.
    /// </summary>
    /// <returns>True if a quit event was received.</returns>
    public static bool DispatchEvents()
    {
        Native.SDL_Event e;
        var gotQuit = false;

        while (SDL_PollEvent(&e))
        {
            if (DispatchOne(&e))
            {
                gotQuit = true;
            }
        }

        return gotQuit;
    }

    /// <summary>
    /// Waits for the next event (up to an optional timeout), then dispatches it through
    /// the same filter and typed-handler path as <see cref="DispatchEvents"/>.
    /// Must be called on the main thread.
    /// </summary>
    /// <param name="timeoutMilliseconds">Maximum time to wait, or -1 to wait indefinitely.</param>
    /// <returns>True if an event was dispatched, false if the wait timed out.</returns>
    public static bool WaitDispatchEvent(int timeoutMilliseconds = -1)
    {
        Native.SDL_Event e;
        var got = timeoutMilliseconds < 0
            ? SDL_WaitEvent(&e)
            : SDL_WaitEventTimeout(&e, timeoutMilliseconds);

        if (!got) return false;

        DispatchOne(&e);
        return true;
    }

    private static bool DispatchOne(Native.SDL_Event* ep)
    {
        ref var e = ref *ep;
        var gotQuit = false;

        RawEventFilter?.Invoke(new RawEvent((EventType)e.type, (nint)ep));

        switch ((Native.SDL_EventType)e.type)
        {
            case Native.SDL_EventType.SDL_EVENT_QUIT:
                gotQuit = true;
                Quit?.Invoke(new QuitEventArgs());
                break;

            case Native.SDL_EventType.SDL_EVENT_KEY_DOWN:
            case Native.SDL_EventType.SDL_EVENT_KEY_UP:
                var keyHandler = e.type == (uint)Native.SDL_EventType.SDL_EVENT_KEY_DOWN ? KeyDown : KeyUp;
                keyHandler?.Invoke(new KeyEventArgs(
                    e.key.windowID.Value,
                    (Scancode)e.key.scancode,
                    (Keycode)e.key.key,
                    (KeyModifiers)e.key.mod,
                    e.key.down != 0,
                    e.key.repeat != 0));
                break;

            case Native.SDL_EventType.SDL_EVENT_TEXT_INPUT:
                TextInput?.Invoke(new TextInputEventArgs(
                    e.text.windowID.Value,
                    Marshal.PtrToStringUTF8((nint)e.text.text)));
                break;

            case Native.SDL_EventType.SDL_EVENT_MOUSE_MOTION:
                MouseMotion?.Invoke(new MouseMotionEventArgs(
                    e.motion.windowID.Value,
                    e.motion.x, e.motion.y,
                    e.motion.xrel, e.motion.yrel,
                    e.motion.state));
                break;

            case Native.SDL_EventType.SDL_EVENT_MOUSE_BUTTON_DOWN:
            case Native.SDL_EventType.SDL_EVENT_MOUSE_BUTTON_UP:
                var btnHandler = e.type == (uint)Native.SDL_EventType.SDL_EVENT_MOUSE_BUTTON_DOWN ? MouseButtonDown : MouseButtonUp;
                btnHandler?.Invoke(new MouseButtonEventArgs(
                    e.button.windowID.Value,
                    (MouseButton)e.button.button,
                    e.button.down != 0,
                    e.button.clicks,
                    e.button.x, e.button.y));
                break;

            case Native.SDL_EventType.SDL_EVENT_MOUSE_WHEEL:
                MouseWheel?.Invoke(new MouseWheelEventArgs(
                    e.wheel.windowID.Value,
                    e.wheel.x, e.wheel.y,
                    e.wheel.mouse_x, e.wheel.mouse_y,
                    (MouseWheelDirection)e.wheel.direction));
                break;

            case >= Native.SDL_EventType.SDL_EVENT_WINDOW_FIRST and <= Native.SDL_EventType.SDL_EVENT_WINDOW_LAST:
                Window?.Invoke(new WindowEventArgs(
                    e.window.windowID.Value,
                    e.window.data1, e.window.data2));
                break;

            case >= Native.SDL_EventType.SDL_EVENT_USER:
                UserEvent?.Invoke(new UserEventArgs(
                    (EventType)e.type,
                    e.user.windowID.Value,
                    e.user.code,
                    (nint)e.user.data1,
                    (nint)e.user.data2));
                break;

            default:
                DispatchExtended(ref e);
                break;
        }

        return gotQuit;
    }

    private static void DispatchExtended(ref Native.SDL_Event e)
    {
        switch ((Native.SDL_EventType)e.type)
        {
            case >= Native.SDL_EventType.SDL_EVENT_DISPLAY_FIRST and <= Native.SDL_EventType.SDL_EVENT_DISPLAY_LAST:
                Display?.Invoke(new DisplayEventArgs(
                    (EventType)e.type, e.display.displayID.Value, e.display.data1, e.display.data2));
                break;

            case Native.SDL_EventType.SDL_EVENT_KEYBOARD_ADDED:
                KeyboardAdded?.Invoke(new InputDeviceEventArgs(e.kdevice.which));
                break;
            case Native.SDL_EventType.SDL_EVENT_KEYBOARD_REMOVED:
                KeyboardRemoved?.Invoke(new InputDeviceEventArgs(e.kdevice.which));
                break;
            case Native.SDL_EventType.SDL_EVENT_MOUSE_ADDED:
                MouseAdded?.Invoke(new InputDeviceEventArgs(e.mdevice.which));
                break;
            case Native.SDL_EventType.SDL_EVENT_MOUSE_REMOVED:
                MouseRemoved?.Invoke(new InputDeviceEventArgs(e.mdevice.which));
                break;

            case Native.SDL_EventType.SDL_EVENT_TEXT_EDITING:
                TextEditing?.Invoke(new TextEditingEventArgs(
                    e.edit.windowID.Value,
                    Marshal.PtrToStringUTF8((nint)e.edit.text),
                    e.edit.start, e.edit.length));
                break;
            case Native.SDL_EventType.SDL_EVENT_TEXT_EDITING_CANDIDATES:
                TextEditingCandidates?.Invoke(new TextEditingCandidatesEventArgs(
                    e.editCandidates.windowID.Value,
                    ReadStringArray(e.editCandidates.candidates, e.editCandidates.num_candidates),
                    e.editCandidates.selected_candidate,
                    e.editCandidates.horizontal != 0));
                break;

            case Native.SDL_EventType.SDL_EVENT_JOYSTICK_AXIS_MOTION:
                JoystickAxisMotion?.Invoke(new JoyAxisEventArgs(e.jaxis.which.Value, e.jaxis.axis, e.jaxis.value));
                break;
            case Native.SDL_EventType.SDL_EVENT_JOYSTICK_BALL_MOTION:
                JoystickBallMotion?.Invoke(new JoyBallEventArgs(e.jball.which.Value, e.jball.ball, e.jball.xrel, e.jball.yrel));
                break;
            case Native.SDL_EventType.SDL_EVENT_JOYSTICK_HAT_MOTION:
                JoystickHatMotion?.Invoke(new JoyHatEventArgs(e.jhat.which.Value, e.jhat.hat, (HatPosition)e.jhat.value));
                break;
            case Native.SDL_EventType.SDL_EVENT_JOYSTICK_BUTTON_DOWN:
                JoystickButtonDown?.Invoke(new JoyButtonEventArgs(e.jbutton.which.Value, e.jbutton.button, e.jbutton.down != 0));
                break;
            case Native.SDL_EventType.SDL_EVENT_JOYSTICK_BUTTON_UP:
                JoystickButtonUp?.Invoke(new JoyButtonEventArgs(e.jbutton.which.Value, e.jbutton.button, e.jbutton.down != 0));
                break;
            case Native.SDL_EventType.SDL_EVENT_JOYSTICK_ADDED:
                JoystickAdded?.Invoke(new JoyDeviceEventArgs(e.jdevice.which.Value));
                break;
            case Native.SDL_EventType.SDL_EVENT_JOYSTICK_REMOVED:
                JoystickRemoved?.Invoke(new JoyDeviceEventArgs(e.jdevice.which.Value));
                break;
            case Native.SDL_EventType.SDL_EVENT_JOYSTICK_UPDATE_COMPLETE:
                JoystickUpdateComplete?.Invoke(new JoyDeviceEventArgs(e.jdevice.which.Value));
                break;
            case Native.SDL_EventType.SDL_EVENT_JOYSTICK_BATTERY_UPDATED:
                JoystickBatteryUpdated?.Invoke(new JoyBatteryEventArgs(
                    e.jbattery.which.Value, (PowerState)e.jbattery.state, e.jbattery.percent));
                break;

            case Native.SDL_EventType.SDL_EVENT_GAMEPAD_AXIS_MOTION:
                GamepadAxisMotion?.Invoke(new GamepadAxisEventArgs(e.gaxis.which.Value, (GamepadAxis)e.gaxis.axis, e.gaxis.value));
                break;
            case Native.SDL_EventType.SDL_EVENT_GAMEPAD_BUTTON_DOWN:
                GamepadButtonDown?.Invoke(new GamepadButtonEventArgs(e.gbutton.which.Value, (GamepadButton)e.gbutton.button, e.gbutton.down != 0));
                break;
            case Native.SDL_EventType.SDL_EVENT_GAMEPAD_BUTTON_UP:
                GamepadButtonUp?.Invoke(new GamepadButtonEventArgs(e.gbutton.which.Value, (GamepadButton)e.gbutton.button, e.gbutton.down != 0));
                break;
            case Native.SDL_EventType.SDL_EVENT_GAMEPAD_ADDED:
                GamepadAdded?.Invoke(new GamepadDeviceEventArgs(e.gdevice.which.Value));
                break;
            case Native.SDL_EventType.SDL_EVENT_GAMEPAD_REMOVED:
                GamepadRemoved?.Invoke(new GamepadDeviceEventArgs(e.gdevice.which.Value));
                break;
            case Native.SDL_EventType.SDL_EVENT_GAMEPAD_REMAPPED:
                GamepadRemapped?.Invoke(new GamepadDeviceEventArgs(e.gdevice.which.Value));
                break;
            case Native.SDL_EventType.SDL_EVENT_GAMEPAD_UPDATE_COMPLETE:
                GamepadUpdateComplete?.Invoke(new GamepadDeviceEventArgs(e.gdevice.which.Value));
                break;
            case Native.SDL_EventType.SDL_EVENT_GAMEPAD_STEAM_HANDLE_UPDATED:
                GamepadSteamHandleUpdated?.Invoke(new GamepadDeviceEventArgs(e.gdevice.which.Value));
                break;
            case Native.SDL_EventType.SDL_EVENT_GAMEPAD_TOUCHPAD_DOWN:
                GamepadTouchpadDown?.Invoke(ToTouchpadArgs(ref e));
                break;
            case Native.SDL_EventType.SDL_EVENT_GAMEPAD_TOUCHPAD_MOTION:
                GamepadTouchpadMotion?.Invoke(ToTouchpadArgs(ref e));
                break;
            case Native.SDL_EventType.SDL_EVENT_GAMEPAD_TOUCHPAD_UP:
                GamepadTouchpadUp?.Invoke(ToTouchpadArgs(ref e));
                break;
            case Native.SDL_EventType.SDL_EVENT_GAMEPAD_SENSOR_UPDATE:
                GamepadSensorUpdate?.Invoke(new GamepadSensorEventArgs(
                    e.gsensor.which.Value, e.gsensor.sensor,
                    e.gsensor.data[0], e.gsensor.data[1], e.gsensor.data[2],
                    e.gsensor.sensor_timestamp));
                break;

            case Native.SDL_EventType.SDL_EVENT_FINGER_DOWN:
                FingerDown?.Invoke(ToFingerArgs(ref e));
                break;
            case Native.SDL_EventType.SDL_EVENT_FINGER_UP:
                FingerUp?.Invoke(ToFingerArgs(ref e));
                break;
            case Native.SDL_EventType.SDL_EVENT_FINGER_MOTION:
                FingerMotion?.Invoke(ToFingerArgs(ref e));
                break;
            case Native.SDL_EventType.SDL_EVENT_FINGER_CANCELED:
                FingerCanceled?.Invoke(ToFingerArgs(ref e));
                break;
            case Native.SDL_EventType.SDL_EVENT_PINCH_BEGIN:
                PinchBegin?.Invoke(new PinchFingerEventArgs(e.pinch.scale, e.pinch.windowID.Value));
                break;
            case Native.SDL_EventType.SDL_EVENT_PINCH_UPDATE:
                PinchUpdate?.Invoke(new PinchFingerEventArgs(e.pinch.scale, e.pinch.windowID.Value));
                break;
            case Native.SDL_EventType.SDL_EVENT_PINCH_END:
                PinchEnd?.Invoke(new PinchFingerEventArgs(e.pinch.scale, e.pinch.windowID.Value));
                break;

            case Native.SDL_EventType.SDL_EVENT_PEN_PROXIMITY_IN:
                PenProximityIn?.Invoke(new PenProximityEventArgs(e.pproximity.windowID.Value, e.pproximity.which));
                break;
            case Native.SDL_EventType.SDL_EVENT_PEN_PROXIMITY_OUT:
                PenProximityOut?.Invoke(new PenProximityEventArgs(e.pproximity.windowID.Value, e.pproximity.which));
                break;
            case Native.SDL_EventType.SDL_EVENT_PEN_DOWN:
                PenDown?.Invoke(new PenTouchEventArgs(e.ptouch.windowID.Value, e.ptouch.which, e.ptouch.x, e.ptouch.y, e.ptouch.eraser != 0, e.ptouch.down != 0));
                break;
            case Native.SDL_EventType.SDL_EVENT_PEN_UP:
                PenUp?.Invoke(new PenTouchEventArgs(e.ptouch.windowID.Value, e.ptouch.which, e.ptouch.x, e.ptouch.y, e.ptouch.eraser != 0, e.ptouch.down != 0));
                break;
            case Native.SDL_EventType.SDL_EVENT_PEN_MOTION:
                PenMotion?.Invoke(new PenMotionEventArgs(e.pmotion.windowID.Value, e.pmotion.which, e.pmotion.x, e.pmotion.y));
                break;
            case Native.SDL_EventType.SDL_EVENT_PEN_BUTTON_DOWN:
                PenButtonDown?.Invoke(new PenButtonEventArgs(e.pbutton.windowID.Value, e.pbutton.which, e.pbutton.x, e.pbutton.y, e.pbutton.button, e.pbutton.down != 0));
                break;
            case Native.SDL_EventType.SDL_EVENT_PEN_BUTTON_UP:
                PenButtonUp?.Invoke(new PenButtonEventArgs(e.pbutton.windowID.Value, e.pbutton.which, e.pbutton.x, e.pbutton.y, e.pbutton.button, e.pbutton.down != 0));
                break;
            case Native.SDL_EventType.SDL_EVENT_PEN_AXIS:
                PenAxisMotion?.Invoke(new PenAxisEventArgs(e.paxis.windowID.Value, e.paxis.which, e.paxis.x, e.paxis.y, (PenAxis)e.paxis.axis, e.paxis.value));
                break;

            case Native.SDL_EventType.SDL_EVENT_DROP_FILE:
            case Native.SDL_EventType.SDL_EVENT_DROP_TEXT:
            case Native.SDL_EventType.SDL_EVENT_DROP_BEGIN:
            case Native.SDL_EventType.SDL_EVENT_DROP_COMPLETE:
            case Native.SDL_EventType.SDL_EVENT_DROP_POSITION:
                Drop?.Invoke(new DropEventArgs(
                    (EventType)e.type, e.drop.windowID.Value, e.drop.x, e.drop.y,
                    Marshal.PtrToStringUTF8((nint)e.drop.source),
                    Marshal.PtrToStringUTF8((nint)e.drop.data)));
                break;

            case Native.SDL_EventType.SDL_EVENT_CLIPBOARD_UPDATE:
                ClipboardUpdate?.Invoke(new ClipboardEventArgs(
                    e.clipboard.owner != 0,
                    ReadStringArray(e.clipboard.mime_types, e.clipboard.num_mime_types)));
                break;

            case Native.SDL_EventType.SDL_EVENT_SENSOR_UPDATE:
                SensorUpdate?.Invoke(ToSensorArgs(ref e));
                break;

            case >= Native.SDL_EventType.SDL_EVENT_AUDIO_DEVICE_ADDED and <= Native.SDL_EventType.SDL_EVENT_AUDIO_DEVICE_FORMAT_CHANGED:
                AudioDeviceEvent?.Invoke(new AudioDeviceEventArgs(
                    (EventType)e.type, e.adevice.which.Value, e.adevice.recording != 0));
                break;

            case >= Native.SDL_EventType.SDL_EVENT_CAMERA_DEVICE_ADDED and <= Native.SDL_EventType.SDL_EVENT_CAMERA_DEVICE_DENIED:
                CameraDeviceEvent?.Invoke(new CameraDeviceEventArgs((EventType)e.type, e.cdevice.which.Value));
                break;

            case >= Native.SDL_EventType.SDL_EVENT_RENDER_TARGETS_RESET and <= Native.SDL_EventType.SDL_EVENT_RENDER_DEVICE_LOST:
                RenderEvent?.Invoke(new RenderEventArgs((EventType)e.type, e.render.windowID.Value));
                break;
        }
    }

    private static GamepadTouchpadEventArgs ToTouchpadArgs(ref Native.SDL_Event e) =>
        new(e.gtouchpad.which.Value, e.gtouchpad.touchpad, e.gtouchpad.finger, e.gtouchpad.x, e.gtouchpad.y, e.gtouchpad.pressure);

    private static TouchFingerEventArgs ToFingerArgs(ref Native.SDL_Event e) =>
        new(e.tfinger.touchID, e.tfinger.fingerID, e.tfinger.x, e.tfinger.y, e.tfinger.dx, e.tfinger.dy, e.tfinger.pressure, e.tfinger.windowID.Value);

    private static SensorEventArgs ToSensorArgs(ref Native.SDL_Event e)
    {
        var data = new float[6];
        for (var i = 0; i < 6; i++)
        {
            data[i] = e.sensor.data[i];
        }

        return new SensorEventArgs(e.sensor.which, data, e.sensor.sensor_timestamp);
    }

    private static string?[] ReadStringArray(byte** array, int count)
    {
        if (array == null || count <= 0)
        {
            return [];
        }

        var result = new string?[count];
        for (var i = 0; i < count; i++)
        {
            result[i] = Marshal.PtrToStringUTF8((nint)array[i]);
        }

        return result;
    }

    /// <summary>Gets whether at least one event of the given type is queued.</summary>
    /// <param name="type">The event type.</param>
    public static bool HasEvent(EventType type) => SDL_HasEvent((uint)type);

    /// <summary>Gets whether at least one event in the inclusive type range is queued.</summary>
    /// <param name="minType">The lowest event type.</param>
    /// <param name="maxType">The highest event type.</param>
    public static bool HasEvents(EventType minType, EventType maxType) =>
        SDL_HasEvents((uint)minType, (uint)maxType);

    /// <summary>Removes all queued events of the given type.</summary>
    /// <param name="type">The event type to flush.</param>
    public static void FlushEvent(EventType type) => SDL_FlushEvent((uint)type);

    /// <summary>Removes all queued events in the inclusive type range.</summary>
    /// <param name="minType">The lowest event type.</param>
    /// <param name="maxType">The highest event type.</param>
    public static void FlushEvents(EventType minType, EventType maxType) =>
        SDL_FlushEvents((uint)minType, (uint)maxType);

    /// <summary>Enables or disables processing of the given event type.</summary>
    /// <param name="type">The event type.</param>
    /// <param name="enabled">True to enable, false to disable.</param>
    public static void SetEventEnabled(EventType type, bool enabled) =>
        SDL_SetEventEnabled((uint)type, enabled);

    /// <summary>Gets whether the given event type is currently enabled.</summary>
    /// <param name="type">The event type.</param>
    public static bool IsEventEnabled(EventType type) => SDL_EventEnabled((uint)type);

    /// <inheritdoc/>
    public void Dispose()
    {
        SDL_SetEventFilter(null, null);
        if (_eventFilterHandle.IsAllocated) _eventFilterHandle.Free();
        _eventFilterHandle = default;

        lock (WatchHandles)
        {
            foreach (var (handler, handle) in WatchHandles)
            {
                SDL_RemoveEventWatch(&WatchTrampoline, (void*)GCHandle.ToIntPtr(handle));
                handle.Free();
            }

            WatchHandles.Clear();
        }

        SDL_Quit();
    }
}
