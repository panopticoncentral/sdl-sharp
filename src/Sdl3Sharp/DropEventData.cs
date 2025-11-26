using System.Runtime.InteropServices;

using static Sdl3Sharp.Native.Events;

namespace Sdl3Sharp;

/// <summary>
/// Data for drop events.
/// </summary>
public readonly unsafe struct DropEventData
{
    private readonly SDL_DropEvent _event;

    internal DropEventData(SDL_DropEvent e) => _event = e;

    /// <summary>Gets the window ID that was dropped on.</summary>
    public uint WindowId => _event.windowID;

    /// <summary>Gets the X coordinate relative to the window.</summary>
    public float X => _event.x;

    /// <summary>Gets the Y coordinate relative to the window.</summary>
    public float Y => _event.y;

    /// <summary>Gets the source application that sent this drop event, or null if unavailable.</summary>
    public string? Source => _event.source is not null ? Marshal.PtrToStringUTF8((nint)_event.source) : null;

    /// <summary>Gets the dropped file name or text, or null for begin/complete events.</summary>
    public string? Data => _event.data is not null ? Marshal.PtrToStringUTF8((nint)_event.data) : null;
}
