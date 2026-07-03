namespace SdlSharp.Input;

/// <summary>
/// A raw SDL event passed to <see cref="Application.RawEventFilter"/> before typed dispatch.
/// </summary>
/// <param name="Type">The event type.</param>
/// <param name="Pointer">
/// The address of the native SDL_Event. Intended for passing to external
/// backends (for example the Dear ImGui SDL3 platform backend). The pointer
/// targets a stack-local SDL_Event owned by <see cref="Application.DispatchEvents"/>,
/// which is overwritten when the next event is polled and is gone once dispatch
/// returns. Storing the pointer and dereferencing it later reads reused or freed
/// stack memory — use it only for the duration of the callback.
/// </param>
public readonly record struct RawEvent(EventType Type, nint Pointer);
