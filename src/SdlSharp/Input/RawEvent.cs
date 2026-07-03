namespace SdlSharp.Input;

/// <summary>
/// A raw SDL event passed to <see cref="Application.RawEventFilter"/> before typed dispatch.
/// </summary>
/// <param name="Type">The event type.</param>
/// <param name="Pointer">
/// The address of the native SDL_Event. Intended for passing to external
/// backends (for example the Dear ImGui SDL3 platform backend). Only valid for the
/// duration of the callback — do not store it.
/// </param>
public readonly record struct RawEvent(EventType Type, nint Pointer);
