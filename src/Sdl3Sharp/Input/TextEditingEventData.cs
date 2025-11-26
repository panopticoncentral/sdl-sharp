using System.Runtime.InteropServices;

using static Sdl3Sharp.Native.Events;

namespace Sdl3Sharp.Input;

/// <summary>
/// Data for text editing events.
/// </summary>
public readonly unsafe struct TextEditingEventData
{
    private readonly SDL_TextEditingEvent _event;

    internal TextEditingEventData(SDL_TextEditingEvent e) => _event = e;

    /// <summary>Gets the window ID with keyboard focus.</summary>
    public uint WindowId => _event.windowID;

    /// <summary>Gets the editing text.</summary>
    public string? Text => _event.text is not null ? Marshal.PtrToStringUTF8((nint)_event.text) : null;

    /// <summary>Gets the start cursor position of selected editing text, or -1 if not set.</summary>
    public int Start => _event.start;

    /// <summary>Gets the length of selected editing text, or -1 if not set.</summary>
    public int Length => _event.length;
}
