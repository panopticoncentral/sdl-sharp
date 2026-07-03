using System.Runtime.InteropServices;

using static SdlSharp.Native.Events;

namespace SdlSharp.Input;

/// <summary>
/// A raw SDL event handed to <see cref="Application.RawEventFilter"/> before typed dispatch,
/// and to <see cref="Application.AddEventWatch"/>/<see cref="Application.SetEventFilter"/>/<see cref="Application.FilterEvents"/> callbacks.
/// </summary>
/// <param name="Type">The event type.</param>
/// <param name="Pointer">
/// The address of the native SDL_Event. Intended for passing to external
/// backends (for example the Dear ImGui SDL3 platform backend). The pointer
/// targets a short-lived native SDL_Event — a stack local during
/// <see cref="Application.DispatchEvents"/>/<see cref="Application.WaitDispatchEvent"/>,
/// or a transient queue entry during a watch/filter callback (which may run on
/// another thread). In every case it is valid only for the duration of the callback:
/// storing the pointer and dereferencing it later reads reused or freed memory.
/// </param>
public readonly record struct RawEvent(EventType Type, nint Pointer)
{
    /// <summary>
    /// Gets the window associated with this event, or <c>null</c> if it has none. The
    /// returned wrapper does not own the window; each access returns a new wrapper and
    /// wrappers are not equal to each other. Only valid while <see cref="Pointer"/> is.
    /// </summary>
    public unsafe Graphics.Window? Window
    {
        get
        {
            var window = SDL_GetWindowFromEvent((Native.SDL_Event*)Pointer);
            return window == null ? null : new Graphics.Window(window, ownsHandle: false);
        }
    }

    /// <summary>
    /// Gets a human-readable description of this event, for logging and debugging. Only
    /// valid while <see cref="Pointer"/> is.
    /// </summary>
    public unsafe string Description
    {
        get
        {
            var e = (Native.SDL_Event*)Pointer;
            var length = SDL_GetEventDescription(e, null, 0);
            if (length <= 0) return string.Empty;

            Span<byte> buffer = length < 256 ? stackalloc byte[length + 1] : new byte[length + 1];
            fixed (byte* buf = buffer)
            {
                var written = SDL_GetEventDescription(e, buf, buffer.Length);
                return written <= 0 ? string.Empty : Marshal.PtrToStringUTF8((nint)buf) ?? string.Empty;
            }
        }
    }
}
