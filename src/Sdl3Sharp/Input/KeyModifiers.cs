using static Sdl3Sharp.Native.Keycode;

namespace Sdl3Sharp.Input;

/// <summary>
/// Represents keyboard modifier keys that can be combined (OR'd together).
/// </summary>
[Flags]
public enum KeyModifiers : ushort
{
    /// <summary>No modifier is applicable.</summary>
    None = SDL_Keymod.SDL_KMOD_NONE,

    /// <summary>The left Shift key is down.</summary>
    LeftShift = SDL_Keymod.SDL_KMOD_LSHIFT,
    /// <summary>The right Shift key is down.</summary>
    RightShift = SDL_Keymod.SDL_KMOD_RSHIFT,
    /// <summary>The Level 5 Shift key is down.</summary>
    Level5 = SDL_Keymod.SDL_KMOD_LEVEL5,
    /// <summary>The left Ctrl (Control) key is down.</summary>
    LeftCtrl = SDL_Keymod.SDL_KMOD_LCTRL,
    /// <summary>The right Ctrl (Control) key is down.</summary>
    RightCtrl = SDL_Keymod.SDL_KMOD_RCTRL,
    /// <summary>The left Alt key is down.</summary>
    LeftAlt = SDL_Keymod.SDL_KMOD_LALT,
    /// <summary>The right Alt key is down.</summary>
    RightAlt = SDL_Keymod.SDL_KMOD_RALT,
    /// <summary>The left GUI key (often the Windows key) is down.</summary>
    LeftGui = SDL_Keymod.SDL_KMOD_LGUI,
    /// <summary>The right GUI key (often the Windows key) is down.</summary>
    RightGui = SDL_Keymod.SDL_KMOD_RGUI,
    /// <summary>The Num Lock key is down.</summary>
    Num = SDL_Keymod.SDL_KMOD_NUM,
    /// <summary>The Caps Lock key is down.</summary>
    Caps = SDL_Keymod.SDL_KMOD_CAPS,
    /// <summary>The AltGr key is down.</summary>
    Mode = SDL_Keymod.SDL_KMOD_MODE,
    /// <summary>The Scroll Lock key is down.</summary>
    Scroll = SDL_Keymod.SDL_KMOD_SCROLL,

    // Combined modifier constants

    /// <summary>Any Ctrl key is down (left or right).</summary>
    Ctrl = SDL_Keymod.SDL_KMOD_CTRL,
    /// <summary>Any Shift key is down (left or right).</summary>
    Shift = SDL_Keymod.SDL_KMOD_SHIFT,
    /// <summary>Any Alt key is down (left or right).</summary>
    Alt = SDL_Keymod.SDL_KMOD_ALT,
    /// <summary>Any GUI key is down (left or right).</summary>
    Gui = SDL_Keymod.SDL_KMOD_GUI
}