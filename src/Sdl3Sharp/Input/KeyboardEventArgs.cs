using System.Runtime.InteropServices;
using Sdl3Sharp.Graphics;
using static Sdl3Sharp.Native.Events;

namespace Sdl3Sharp.Input;

/// <summary>
/// Event arguments for keyboard key events.
/// </summary>
public sealed class KeyboardEventArgs : SdlEventArgs
{
    /// <summary>
    /// Gets the window ID with keyboard focus.
    /// </summary>
    public uint WindowId { get; }

    /// <summary>
    /// Gets the keyboard instance ID.
    /// </summary>
    public uint KeyboardId { get; }

    /// <summary>
    /// Gets the physical key code (scancode).
    /// </summary>
    public Scancode Scancode { get; }

    /// <summary>
    /// Gets the virtual key code.
    /// </summary>
    public Keycode Keycode { get; }

    /// <summary>
    /// Gets the current key modifiers.
    /// </summary>
    public KeyModifiers Modifiers { get; }

    /// <summary>
    /// Gets the platform-dependent scancode.
    /// </summary>
    public ushort RawScancode { get; }

    /// <summary>
    /// Gets a value indicating whether the key is pressed.
    /// </summary>
    public bool IsPressed { get; }

    /// <summary>
    /// Gets a value indicating whether this is a key repeat.
    /// </summary>
    public bool IsRepeat { get; }

    /// <summary>
    /// Gets the window with keyboard focus, if any.
    /// </summary>
    public Window? Window => Window.FromID(WindowId);

    internal KeyboardEventArgs(SDL_KeyboardEvent e) : base(e.timestamp)
    {
        WindowId = e.windowID;
        KeyboardId = e.which;
        Scancode = (Scancode)e.scancode;
        Keycode = (Keycode)e.key;
        Modifiers = (KeyModifiers)e.mod;
        RawScancode = e.raw;
        IsPressed = e.down;
        IsRepeat = e.repeat;
    }
}

/// <summary>
/// Event arguments for text editing events.
/// </summary>
public sealed unsafe class TextEditingEventArgs : SdlEventArgs
{
    /// <summary>
    /// Gets the window ID with keyboard focus.
    /// </summary>
    public uint WindowId { get; }

    /// <summary>
    /// Gets the editing text.
    /// </summary>
    public string Text { get; }

    /// <summary>
    /// Gets the start cursor of selected editing text, or -1 if not set.
    /// </summary>
    public int Start { get; }

    /// <summary>
    /// Gets the length of selected editing text, or -1 if not set.
    /// </summary>
    public int Length { get; }

    /// <summary>
    /// Gets the window with keyboard focus, if any.
    /// </summary>
    public Window? Window => Window.FromID(WindowId);

    internal TextEditingEventArgs(SDL_TextEditingEvent e) : base(e.timestamp)
    {
        WindowId = e.windowID;
        Text = e.text != null ? Marshal.PtrToStringUTF8((nint)e.text) ?? string.Empty : string.Empty;
        Start = e.start;
        Length = e.length;
    }
}

/// <summary>
/// Event arguments for text input events.
/// </summary>
public sealed unsafe class TextInputEventArgs : SdlEventArgs
{
    /// <summary>
    /// Gets the window ID with keyboard focus.
    /// </summary>
    public uint WindowId { get; }

    /// <summary>
    /// Gets the input text (UTF-8 encoded).
    /// </summary>
    public string Text { get; }

    /// <summary>
    /// Gets the window with keyboard focus, if any.
    /// </summary>
    public Window? Window => Window.FromID(WindowId);

    internal TextInputEventArgs(SDL_TextInputEvent e) : base(e.timestamp)
    {
        WindowId = e.windowID;
        Text = e.text != null ? Marshal.PtrToStringUTF8((nint)e.text) ?? string.Empty : string.Empty;
    }
}

/// <summary>
/// Event arguments for keyboard device events.
/// </summary>
public sealed class KeyboardDeviceEventArgs : SdlEventArgs
{
    /// <summary>
    /// Gets the keyboard instance ID.
    /// </summary>
    public uint KeyboardId { get; }

    internal KeyboardDeviceEventArgs(SDL_KeyboardDeviceEvent e) : base(e.timestamp)
    {
        KeyboardId = e.which;
    }
}
