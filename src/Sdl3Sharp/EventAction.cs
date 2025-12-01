using static Sdl3Sharp.Native.Events;

namespace Sdl3Sharp;

/// <summary>
/// The type of action to request from event peep operations.
/// </summary>
public enum EventAction
{
    /// <summary>Add events to the back of the queue.</summary>
    Add = SDL_EventAction.SDL_ADDEVENT,
    /// <summary>Check but don't remove events from the queue front.</summary>
    Peek = SDL_EventAction.SDL_PEEKEVENT,
    /// <summary>Retrieve/remove events from the front of the queue.</summary>
    Get = SDL_EventAction.SDL_GETEVENT
}