using static Sdl3Sharp.Native.Events;

namespace Sdl3Sharp;

/// <summary>
/// Data for user-defined events.
/// </summary>
public readonly unsafe struct UserEventData
{
    private readonly SDL_UserEvent _event;

    internal UserEventData(SDL_UserEvent e) => _event = e;

    /// <summary>Gets the window ID associated with this event, if any.</summary>
    public uint WindowId => _event.windowID;

    /// <summary>Gets the user-defined event code.</summary>
    public int Code => _event.code;

    /// <summary>Gets the first user-defined data pointer.</summary>
    public nint Data1 => (nint)_event.data1;

    /// <summary>Gets the second user-defined data pointer.</summary>
    public nint Data2 => (nint)_event.data2;
}
