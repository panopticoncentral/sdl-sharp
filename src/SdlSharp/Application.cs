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

            default:
                DispatchExtended(ref e);
                break;
        }

        return gotQuit;
    }

    // Filled in by Task 10 with the remaining typed event families.
    private static void DispatchExtended(ref Native.SDL_Event e)
    {
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
    public void Dispose() => SDL_Quit();
}
