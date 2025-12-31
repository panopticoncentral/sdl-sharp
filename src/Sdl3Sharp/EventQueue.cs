using Sdl3Sharp.Audio;
using Sdl3Sharp.Graphics;
using Sdl3Sharp.Input;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

using static Sdl3Sharp.Native.Common;
using static Sdl3Sharp.Native.Events;

namespace Sdl3Sharp;

/// <summary>
/// Provides access to the SDL event queue for polling, waiting, and managing events.
/// </summary>
/// <remarks>
/// <para>The event queue is the core of SDL's event handling. All user interactions
/// (keyboard, mouse, touch, gamepad, etc.) and system notifications (window events,
/// device connections, etc.) flow through this queue.</para>
/// <para>A typical game loop calls <see cref="Poll"/> in a loop until it returns null,
/// processing each event as it comes.</para>
/// </remarks>
public static unsafe class EventQueue
{
    /// <summary>
    /// Pumps the event loop, gathering events from the input devices.
    /// </summary>
    /// <remarks>
    /// <para>This function updates the event queue and internal input device state.
    /// This should only be called in the thread that initialized the video subsystem,
    /// and for extra safety, you should consider only doing those things on the main thread.</para>
    /// <para><see cref="Poll"/> and <see cref="Wait()"/> implicitly call this function,
    /// so you only need to call it if you're not polling or waiting for events.</para>
    /// </remarks>
    public static void Pump()
    {
        SDL_PumpEvents();
    }

    /// <summary>
    /// Dispatches a single event to the appropriate event handlers.
    /// </summary>
    /// <param name="e">The event to dispatch.</param>
    public static void DispatchEvent(Event e)
    {
        switch (e.Type)
        {
            case EventType.Quit:
            case EventType.Terminating:
            case EventType.LowMemory:
            case EventType.WillEnterBackground:
            case EventType.DidEnterBackground:
            case EventType.WillEnterForeground:
            case EventType.DidEnterForeground:
            case EventType.LocaleChanged:
            case EventType.SystemThemeChanged:
                Application.DispatchEvent(e);
                break;

            case EventType.DisplayOrientation:
            case EventType.DisplayAdded:
            case EventType.DisplayRemoved:
            case EventType.DisplayMoved:
            case EventType.DisplayDesktopModeChanged:
            case EventType.DisplayCurrentModeChanged:
            case EventType.DisplayContentScaleChanged:
                Display.DispatchEvent(e);
                break;

            case EventType.WindowShown:
            case EventType.WindowHidden:
            case EventType.WindowExposed:
            case EventType.WindowMoved:
            case EventType.WindowResized:
            case EventType.WindowPixelSizeChanged:
            case EventType.WindowMetalViewResized:
            case EventType.WindowMinimized:
            case EventType.WindowMaximized:
            case EventType.WindowRestored:
            case EventType.WindowMouseEnter:
            case EventType.WindowMouseLeave:
            case EventType.WindowFocusGained:
            case EventType.WindowFocusLost:
            case EventType.WindowCloseRequested:
            case EventType.WindowHitTest:
            case EventType.WindowIccProfileChanged:
            case EventType.WindowDisplayChanged:
            case EventType.WindowDisplayScaleChanged:
            case EventType.WindowSafeAreaChanged:
            case EventType.WindowOccluded:
            case EventType.WindowEnterFullscreen:
            case EventType.WindowLeaveFullscreen:
            case EventType.WindowDestroyed:
            case EventType.WindowHdrStateChanged:
                Window.DispatchEvent(e);
                break;

            case EventType.KeyDown:
            case EventType.KeyUp:
            case EventType.TextEditing:
            case EventType.TextInput:
            case EventType.KeymapChanged:
            case EventType.KeyboardAdded:
            case EventType.KeyboardRemoved:
            case EventType.TextEditingCandidates:
                Keyboard.DispatchEvent(e);
                break;

            case EventType.MouseMotion:
            case EventType.MouseButtonDown:
            case EventType.MouseButtonUp:
            case EventType.MouseWheel:
            case EventType.MouseAdded:
            case EventType.MouseRemoved:
                Mouse.DispatchEvent(e);
                break;

            case EventType.JoystickAxisMotion:
            case EventType.JoystickBallMotion:
            case EventType.JoystickHatMotion:
            case EventType.JoystickButtonDown:
            case EventType.JoystickButtonUp:
            case EventType.JoystickAdded:
            case EventType.JoystickRemoved:
            case EventType.JoystickBatteryUpdated:
            case EventType.JoystickUpdateComplete:
                Joystick.DispatchEvent(e);
                break;

            case EventType.GamepadAxisMotion:
            case EventType.GamepadButtonDown:
            case EventType.GamepadButtonUp:
            case EventType.GamepadAdded:
            case EventType.GamepadRemoved:
            case EventType.GamepadRemapped:
            case EventType.GamepadTouchpadDown:
            case EventType.GamepadTouchpadMotion:
            case EventType.GamepadTouchpadUp:
            case EventType.GamepadSensorUpdate:
            case EventType.GamepadUpdateComplete:
            case EventType.GamepadSteamHandleUpdated:
                Gamepad.DispatchEvent(e);
                break;

            case EventType.FingerDown:
            case EventType.FingerUp:
            case EventType.FingerMotion:
            case EventType.FingerCanceled:
                TouchDevice.DispatchEvent(e);
                break;

            case EventType.ClipboardUpdate:
            case EventType.DropFile:
            case EventType.DropText:
            case EventType.DropBegin:
            case EventType.DropComplete:
            case EventType.DropPosition:
                Application.DispatchEvent(e);
                break;

            case EventType.AudioDeviceAdded:
            case EventType.AudioDeviceRemoved:
            case EventType.AudioDeviceFormatChanged:
                AudioDevice.DispatchEvent(e);
                break;

            case EventType.SensorUpdate:
                Sensor.DispatchEvent(e);
                break;

            case EventType.PenProximityIn:
            case EventType.PenProximityOut:
            case EventType.PenDown:
            case EventType.PenUp:
            case EventType.PenButtonDown:
            case EventType.PenButtonUp:
            case EventType.PenMotion:
            case EventType.PenAxis:
                Pen.DispatchEvent(e);
                break;

            case EventType.CameraDeviceAdded:
            case EventType.CameraDeviceRemoved:
            case EventType.CameraDeviceApproved:
            case EventType.CameraDeviceDenied:
                Camera.DispatchEvent(e);
                break;

            case EventType.RenderTargetsReset:
            case EventType.RenderDeviceReset:
            case EventType.RenderDeviceLost:
                Renderer.DispatchEvent(e);
                break;
        }
    }

    /// <summary>
    /// Polls for currently pending events.
    /// </summary>
    /// <returns>The next event from the queue, or null if there are no events available.</returns>
    public static Event? Poll()
    {
        SDL_Event sdlEvent;
        return SDL_PollEvent(&sdlEvent) ? new Event(sdlEvent) : null;
    }

    /// <summary>
    /// Processes and dispatches all pending events until no more events are available.
    /// </summary>
    public static void DispatchAllEvents()
    {
        while (true)
        {
            Event? e = Poll();
            if (e == null)
            {
                break;
            }

            DispatchEvent(e.Value);
        }
    }

    /// <summary>
    /// Waits indefinitely for the next available event.
    /// </summary>
    /// <returns>The next event from the queue.</returns>
    /// <exception cref="SdlException">Thrown if there was an error while waiting for events.</exception>
    /// <remarks>
    /// <para>This function blocks until an event is available. Use this when your
    /// application is event-driven and doesn't need to continuously render.</para>
    /// </remarks>
    public static Event Wait()
    {
        SDL_Event sdlEvent;
        _ = CheckErrorBool(SDL_WaitEvent(&sdlEvent));
        return new Event(sdlEvent);
    }

    /// <summary>
    /// Waits until the specified timeout for the next available event.
    /// </summary>
    /// <param name="timeout">The maximum time to wait for an event.</param>
    /// <returns>The next event from the queue, or null if the timeout elapsed without any events.</returns>
    /// <remarks>
    /// <para>This function blocks until an event is available or the timeout expires.</para>
    /// </remarks>
    public static Event? Wait(TimeSpan timeout)
    {
        SDL_Event sdlEvent;
        var timeoutMs = (int)timeout.TotalMilliseconds;
        return SDL_WaitEventTimeout(&sdlEvent, timeoutMs) ? new Event(sdlEvent) : null;
    }

    /// <summary>
    /// Waits until the specified timeout (in milliseconds) for the next available event.
    /// </summary>
    /// <param name="timeoutMs">The maximum number of milliseconds to wait for an event.</param>
    /// <returns>The next event from the queue, or null if the timeout elapsed without any events.</returns>
    public static Event? Wait(int timeoutMs)
    {
        SDL_Event sdlEvent;
        return SDL_WaitEventTimeout(&sdlEvent, timeoutMs) ? new Event(sdlEvent) : null;
    }

    /// <summary>
    /// Adds an event to the event queue.
    /// </summary>
    /// <param name="event">The event to add.</param>
    /// <returns>True on success, false if the event was filtered or on failure.</returns>
    public static bool Push(Event @event)
    {
        SDL_Event sdlEvent = @event.Native;
        return SDL_PushEvent(&sdlEvent);
    }

    /// <summary>
    /// Checks for the existence of a certain event type in the event queue.
    /// </summary>
    /// <param name="type">The type of event to check for.</param>
    /// <returns>True if events of the specified type are present.</returns>
    public static bool HasEvent(EventType type)
    {
        return SDL_HasEvent((uint)type);
    }

    /// <summary>
    /// Checks for the existence of events within a range of types in the event queue.
    /// </summary>
    /// <param name="minType">The minimum event type (inclusive).</param>
    /// <param name="maxType">The maximum event type (inclusive).</param>
    /// <returns>True if events within the range are present.</returns>
    public static bool HasEvents(EventType minType, EventType maxType)
    {
        return SDL_HasEvents((uint)minType, (uint)maxType);
    }

    /// <summary>
    /// Clears events of a specific type from the event queue.
    /// </summary>
    /// <param name="type">The type of event to clear.</param>
    public static void Flush(EventType type)
    {
        SDL_FlushEvent((uint)type);
    }

    /// <summary>
    /// Clears events within a range of types from the event queue.
    /// </summary>
    /// <param name="minType">The minimum event type to clear (inclusive).</param>
    /// <param name="maxType">The maximum event type to clear (inclusive).</param>
    public static void Flush(EventType minType, EventType maxType)
    {
        SDL_FlushEvents((uint)minType, (uint)maxType);
    }

    /// <summary>
    /// Peeks at events in the queue without removing them.
    /// </summary>
    /// <param name="events">A span to receive the events.</param>
    /// <param name="minType">The minimum event type to retrieve (inclusive).</param>
    /// <param name="maxType">The maximum event type to retrieve (inclusive).</param>
    /// <returns>The number of events actually retrieved.</returns>
    public static int Peek(Span<Event> events, EventType minType = EventType.First, EventType maxType = EventType.Last)
    {
        SDL_Event* nativeEvents = stackalloc SDL_Event[events.Length];
        var count = SDL_PeepEvents(nativeEvents, events.Length, SDL_EventAction.SDL_PEEKEVENT, (uint)minType, (uint)maxType);

        if (count < 0)
        {
            throw new SdlException();
        }

        for (var i = 0; i < count; i++)
        {
            events[i] = new Event(nativeEvents[i]);
        }

        return count;
    }

    /// <summary>
    /// Gets and removes events from the queue.
    /// </summary>
    /// <param name="events">A span to receive the events.</param>
    /// <param name="minType">The minimum event type to retrieve (inclusive).</param>
    /// <param name="maxType">The maximum event type to retrieve (inclusive).</param>
    /// <returns>The number of events actually retrieved.</returns>
    public static int Get(Span<Event> events, EventType minType = EventType.First, EventType maxType = EventType.Last)
    {
        SDL_Event* nativeEvents = stackalloc SDL_Event[events.Length];
        var count = SDL_PeepEvents(nativeEvents, events.Length, SDL_EventAction.SDL_GETEVENT, (uint)minType, (uint)maxType);

        if (count < 0)
        {
            throw new SdlException();
        }

        for (var i = 0; i < count; i++)
        {
            events[i] = new Event(nativeEvents[i]);
        }

        return count;
    }

    /// <summary>
    /// Enables or disables processing of a specific event type.
    /// </summary>
    /// <param name="type">The type of event to enable or disable.</param>
    /// <param name="enabled">True to enable processing, false to disable.</param>
    /// <remarks>
    /// <para>Disabled events are automatically dropped from the event queue and will
    /// not be delivered to the application.</para>
    /// </remarks>
    public static void SetEnabled(EventType type, bool enabled)
    {
        SDL_SetEventEnabled((uint)type, enabled);
    }

    /// <summary>
    /// Checks whether processing of a specific event type is enabled.
    /// </summary>
    /// <param name="type">The type of event to check.</param>
    /// <returns>True if the event type is being processed.</returns>
    public static bool IsEnabled(EventType type)
    {
        return SDL_EventEnabled((uint)type);
    }

    /// <summary>
    /// Allocates a set of user-defined events.
    /// </summary>
    /// <param name="count">The number of events to allocate.</param>
    /// <returns>The beginning event number for the allocated range, or 0 if allocation failed.</returns>
    /// <remarks>
    /// <para>User events should have types between <see cref="EventType.User"/> and
    /// <see cref="EventType.Last"/>. This function reserves a contiguous range of
    /// event numbers for your application's use.</para>
    /// </remarks>
    public static uint RegisterEvents(int count)
    {
        return SDL_RegisterEvents(count);
    }

    private static GCHandle _currentFilterHandle;

    /// <summary>
    /// Sets a filter function to process all events before they are added to the queue.
    /// </summary>
    /// <param name="filter">The filter function. Return true to keep the event, false to drop it.
    /// Pass null to remove the current filter.</param>
    /// <remarks>
    /// <para>The filter function is called when an event is added to the queue. If it returns
    /// false, the event is dropped and will never be seen by the application.</para>
    /// <para>Be careful what you do in the filter function, as it may be called from a
    /// different thread.</para>
    /// </remarks>
    public static void SetFilter(Func<Event, bool>? filter)
    {
        // Clean up previous filter
        if (_currentFilterHandle.IsAllocated)
        {
            _currentFilterHandle.Free();
        }

        if (filter is null)
        {
            SDL_SetEventFilter(null, 0);
        }
        else
        {
            _currentFilterHandle = GCHandle.Alloc(filter);
            SDL_SetEventFilter(&EventFilterHandler, (nuint)GCHandle.ToIntPtr(_currentFilterHandle));
        }
    }

    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
    private static byte EventFilterHandler(nuint userdata, SDL_Event* sdlEvent)
    {
        var handle = GCHandle.FromIntPtr((nint)userdata);
        var callback = (Func<Event, bool>)handle.Target!;
        var e = new Event(*sdlEvent);
        return callback(e) ? (byte)1 : (byte)0;
    }

    private static readonly List<EventWatchRegistration> _eventWatches = [];

    /// <summary>
    /// Adds a callback to be triggered when an event is added to the event queue.
    /// </summary>
    /// <param name="callback">The callback function to invoke for each event.</param>
    /// <returns>A registration handle that can be used to remove the watch.</returns>
    /// <remarks>
    /// <para>Event watches are called for every event added to the queue, whether or not
    /// it passes the event filter. They cannot modify or block events.</para>
    /// <para>Be careful what you do in the callback, as it may be called from a different thread.</para>
    /// </remarks>
    public static EventWatchHandle AddWatch(Action<Event> callback)
    {
        var registration = new EventWatchRegistration(callback);
        var handle = GCHandle.Alloc(registration);
        registration.Handle = handle;

        lock (_eventWatches)
        {
            _eventWatches.Add(registration);
        }

        if (!SDL_AddEventWatch(&EventWatchHandler, (nuint)GCHandle.ToIntPtr(handle)))
        {
            handle.Free();
            lock (_eventWatches)
            {
                _ = _eventWatches.Remove(registration);
            }

            throw new SdlException();
        }

        return new EventWatchHandle(registration);
    }

    /// <summary>
    /// Removes an event watch callback.
    /// </summary>
    /// <param name="handle">The handle returned by <see cref="AddWatch"/>.</param>
    public static void RemoveWatch(EventWatchHandle handle)
    {
        EventWatchRegistration? registration = handle.Registration;
        if (registration is null || !registration.Handle.IsAllocated)
        {
            return;
        }

        SDL_RemoveEventWatch(&EventWatchHandler, (nuint)GCHandle.ToIntPtr(registration.Handle));

        lock (_eventWatches)
        {
            _ = _eventWatches.Remove(registration);
        }

        registration.Handle.Free();
    }

    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
    private static byte EventWatchHandler(nuint userdata, SDL_Event* sdlEvent)
    {
        var handle = GCHandle.FromIntPtr((nint)userdata);
        var registration = (EventWatchRegistration)handle.Target!;
        var e = new Event(*sdlEvent);
        registration.Callback(e);
        return 1; // Event watches always return true
    }

    /// <summary>
    /// Filters events currently in the queue, removing any for which the filter returns false.
    /// </summary>
    /// <param name="filter">The filter function. Return true to keep the event, false to remove it.</param>
    public static void Filter(Func<Event, bool> filter)
    {
        var handle = GCHandle.Alloc(filter);
        try
        {
            SDL_FilterEvents(&EventFilterHandler, (nuint)GCHandle.ToIntPtr(handle));
        }
        finally
        {
            handle.Free();
        }
    }

    internal sealed class EventWatchRegistration(Action<Event> callback)
    {
        public Action<Event> Callback { get; } = callback;
        public GCHandle Handle { get; set; }
    }
}