using static Sdl3Sharp.Native.Events;

namespace Sdl3Sharp.Input;

/// <summary>
/// Data for keyboard events.
/// </summary>
public readonly struct KeyboardEventData
{
    private readonly SDL_KeyboardEvent _event;

    internal KeyboardEventData(SDL_KeyboardEvent e) => _event = e;

    /// <summary>Gets the window ID with keyboard focus.</summary>
    public uint WindowId => _event.windowID;

    /// <summary>Gets the keyboard instance ID.</summary>
    public uint KeyboardId => _event.which;

    /// <summary>Gets the physical key code (scancode).</summary>
    public int Scancode => (int)_event.scancode;

    /// <summary>Gets the virtual key code.</summary>
    public int Keycode => (int)_event.key;

    /// <summary>Gets the current key modifiers.</summary>
    public ushort Modifiers => (ushort)_event.mod;

    /// <summary>Gets the platform-dependent scancode.</summary>
    public ushort RawScancode => _event.raw;

    /// <summary>Gets a value indicating whether the key is pressed.</summary>
    public bool IsPressed => _event.down;

    /// <summary>Gets a value indicating whether this is a key repeat.</summary>
    public bool IsRepeat => _event.repeat;
}
