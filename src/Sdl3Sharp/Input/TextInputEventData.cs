using System.Runtime.InteropServices;

using static Sdl3Sharp.Native.Events;

namespace Sdl3Sharp.Input;

/// <summary>
/// Data for text input events.
/// </summary>
public readonly unsafe struct TextInputEventData
{
    private readonly SDL_TextInputEvent _event;

    internal TextInputEventData(SDL_TextInputEvent e) => _event = e;

    /// <summary>Gets the window ID with keyboard focus.</summary>
    public uint WindowId => _event.windowID;

    /// <summary>Gets the input text (UTF-8 encoded).</summary>
    public string? Text => _event.text is not null ? Marshal.PtrToStringUTF8((nint)_event.text) : null;
}
