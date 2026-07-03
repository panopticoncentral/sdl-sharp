namespace SdlSharp.Input;

/// <summary>
/// Virtual key code, dependent on the current keyboard layout.
/// </summary>
public enum Keycode : uint
{
    /// <summary>Unknown key.</summary>
    Unknown = (uint)Native.SDL_Keycode.SDLK_UNKNOWN,
    /// <summary>The Return (Enter) key.</summary>
    Return = (uint)Native.SDL_Keycode.SDLK_RETURN,
    /// <summary>The Escape key.</summary>
    Escape = (uint)Native.SDL_Keycode.SDLK_ESCAPE,
    /// <summary>The Backspace key.</summary>
    Backspace = (uint)Native.SDL_Keycode.SDLK_BACKSPACE,
    /// <summary>The Tab key.</summary>
    Tab = (uint)Native.SDL_Keycode.SDLK_TAB,
    /// <summary>The Space key.</summary>
    Space = (uint)Native.SDL_Keycode.SDLK_SPACE,
    /// <summary>The comma (',') key.</summary>
    Comma = (uint)Native.SDL_Keycode.SDLK_COMMA,
    /// <summary>The minus ('-') key.</summary>
    Minus = (uint)Native.SDL_Keycode.SDLK_MINUS,
    /// <summary>The period ('.') key.</summary>
    Period = (uint)Native.SDL_Keycode.SDLK_PERIOD,
    /// <summary>The slash ('/') key.</summary>
    Slash = (uint)Native.SDL_Keycode.SDLK_SLASH,
    /// <summary>The 0 key.</summary>
    Num0 = (uint)Native.SDL_Keycode.SDLK_0,
    /// <summary>The 1 key.</summary>
    Num1 = (uint)Native.SDL_Keycode.SDLK_1,
    /// <summary>The 2 key.</summary>
    Num2 = (uint)Native.SDL_Keycode.SDLK_2,
    /// <summary>The 3 key.</summary>
    Num3 = (uint)Native.SDL_Keycode.SDLK_3,
    /// <summary>The 4 key.</summary>
    Num4 = (uint)Native.SDL_Keycode.SDLK_4,
    /// <summary>The 5 key.</summary>
    Num5 = (uint)Native.SDL_Keycode.SDLK_5,
    /// <summary>The 6 key.</summary>
    Num6 = (uint)Native.SDL_Keycode.SDLK_6,
    /// <summary>The 7 key.</summary>
    Num7 = (uint)Native.SDL_Keycode.SDLK_7,
    /// <summary>The 8 key.</summary>
    Num8 = (uint)Native.SDL_Keycode.SDLK_8,
    /// <summary>The 9 key.</summary>
    Num9 = (uint)Native.SDL_Keycode.SDLK_9,
    /// <summary>The semicolon (';') key.</summary>
    Semicolon = (uint)Native.SDL_Keycode.SDLK_SEMICOLON,
    /// <summary>The equals ('=') key.</summary>
    Equals = (uint)Native.SDL_Keycode.SDLK_EQUALS,
    /// <summary>The left bracket ('[') key.</summary>
    LeftBracket = (uint)Native.SDL_Keycode.SDLK_LEFTBRACKET,
    /// <summary>The backslash ('\') key.</summary>
    Backslash = (uint)Native.SDL_Keycode.SDLK_BACKSLASH,
    /// <summary>The right bracket (']') key.</summary>
    RightBracket = (uint)Native.SDL_Keycode.SDLK_RIGHTBRACKET,
    /// <summary>The grave accent ('`') key.</summary>
    Grave = (uint)Native.SDL_Keycode.SDLK_GRAVE,
    /// <summary>The A key.</summary>
    A = (uint)Native.SDL_Keycode.SDLK_A,
    /// <summary>The B key.</summary>
    B = (uint)Native.SDL_Keycode.SDLK_B,
    /// <summary>The C key.</summary>
    C = (uint)Native.SDL_Keycode.SDLK_C,
    /// <summary>The D key.</summary>
    D = (uint)Native.SDL_Keycode.SDLK_D,
    /// <summary>The E key.</summary>
    E = (uint)Native.SDL_Keycode.SDLK_E,
    /// <summary>The F key.</summary>
    F = (uint)Native.SDL_Keycode.SDLK_F,
    /// <summary>The G key.</summary>
    G = (uint)Native.SDL_Keycode.SDLK_G,
    /// <summary>The H key.</summary>
    H = (uint)Native.SDL_Keycode.SDLK_H,
    /// <summary>The I key.</summary>
    I = (uint)Native.SDL_Keycode.SDLK_I,
    /// <summary>The J key.</summary>
    J = (uint)Native.SDL_Keycode.SDLK_J,
    /// <summary>The K key.</summary>
    K = (uint)Native.SDL_Keycode.SDLK_K,
    /// <summary>The L key.</summary>
    L = (uint)Native.SDL_Keycode.SDLK_L,
    /// <summary>The M key.</summary>
    M = (uint)Native.SDL_Keycode.SDLK_M,
    /// <summary>The N key.</summary>
    N = (uint)Native.SDL_Keycode.SDLK_N,
    /// <summary>The O key.</summary>
    O = (uint)Native.SDL_Keycode.SDLK_O,
    /// <summary>The P key.</summary>
    P = (uint)Native.SDL_Keycode.SDLK_P,
    /// <summary>The Q key.</summary>
    Q = (uint)Native.SDL_Keycode.SDLK_Q,
    /// <summary>The R key.</summary>
    R = (uint)Native.SDL_Keycode.SDLK_R,
    /// <summary>The S key.</summary>
    S = (uint)Native.SDL_Keycode.SDLK_S,
    /// <summary>The T key.</summary>
    T = (uint)Native.SDL_Keycode.SDLK_T,
    /// <summary>The U key.</summary>
    U = (uint)Native.SDL_Keycode.SDLK_U,
    /// <summary>The V key.</summary>
    V = (uint)Native.SDL_Keycode.SDLK_V,
    /// <summary>The W key.</summary>
    W = (uint)Native.SDL_Keycode.SDLK_W,
    /// <summary>The X key.</summary>
    X = (uint)Native.SDL_Keycode.SDLK_X,
    /// <summary>The Y key.</summary>
    Y = (uint)Native.SDL_Keycode.SDLK_Y,
    /// <summary>The Z key.</summary>
    Z = (uint)Native.SDL_Keycode.SDLK_Z,
    /// <summary>The Delete key.</summary>
    Delete = (uint)Native.SDL_Keycode.SDLK_DELETE,
    /// <summary>The Caps Lock key.</summary>
    CapsLock = (uint)Native.SDL_Keycode.SDLK_CAPSLOCK,
    /// <summary>The F1 key.</summary>
    F1 = (uint)Native.SDL_Keycode.SDLK_F1,
    /// <summary>The F2 key.</summary>
    F2 = (uint)Native.SDL_Keycode.SDLK_F2,
    /// <summary>The F3 key.</summary>
    F3 = (uint)Native.SDL_Keycode.SDLK_F3,
    /// <summary>The F4 key.</summary>
    F4 = (uint)Native.SDL_Keycode.SDLK_F4,
    /// <summary>The F5 key.</summary>
    F5 = (uint)Native.SDL_Keycode.SDLK_F5,
    /// <summary>The F6 key.</summary>
    F6 = (uint)Native.SDL_Keycode.SDLK_F6,
    /// <summary>The F7 key.</summary>
    F7 = (uint)Native.SDL_Keycode.SDLK_F7,
    /// <summary>The F8 key.</summary>
    F8 = (uint)Native.SDL_Keycode.SDLK_F8,
    /// <summary>The F9 key.</summary>
    F9 = (uint)Native.SDL_Keycode.SDLK_F9,
    /// <summary>The F10 key.</summary>
    F10 = (uint)Native.SDL_Keycode.SDLK_F10,
    /// <summary>The F11 key.</summary>
    F11 = (uint)Native.SDL_Keycode.SDLK_F11,
    /// <summary>The F12 key.</summary>
    F12 = (uint)Native.SDL_Keycode.SDLK_F12,
    /// <summary>The Print Screen key.</summary>
    PrintScreen = (uint)Native.SDL_Keycode.SDLK_PRINTSCREEN,
    /// <summary>The Scroll Lock key.</summary>
    ScrollLock = (uint)Native.SDL_Keycode.SDLK_SCROLLLOCK,
    /// <summary>The Pause key.</summary>
    Pause = (uint)Native.SDL_Keycode.SDLK_PAUSE,
    /// <summary>The Insert key.</summary>
    Insert = (uint)Native.SDL_Keycode.SDLK_INSERT,
    /// <summary>The Home key.</summary>
    Home = (uint)Native.SDL_Keycode.SDLK_HOME,
    /// <summary>The Page Up key.</summary>
    PageUp = (uint)Native.SDL_Keycode.SDLK_PAGEUP,
    /// <summary>The End key.</summary>
    End = (uint)Native.SDL_Keycode.SDLK_END,
    /// <summary>The Page Down key.</summary>
    PageDown = (uint)Native.SDL_Keycode.SDLK_PAGEDOWN,
    /// <summary>The right arrow key.</summary>
    Right = (uint)Native.SDL_Keycode.SDLK_RIGHT,
    /// <summary>The left arrow key.</summary>
    Left = (uint)Native.SDL_Keycode.SDLK_LEFT,
    /// <summary>The down arrow key.</summary>
    Down = (uint)Native.SDL_Keycode.SDLK_DOWN,
    /// <summary>The up arrow key.</summary>
    Up = (uint)Native.SDL_Keycode.SDLK_UP,
    /// <summary>The left Ctrl (Control) key.</summary>
    LCtrl = (uint)Native.SDL_Keycode.SDLK_LCTRL,
    /// <summary>The left Shift key.</summary>
    LShift = (uint)Native.SDL_Keycode.SDLK_LSHIFT,
    /// <summary>The left Alt key.</summary>
    LAlt = (uint)Native.SDL_Keycode.SDLK_LALT,
    /// <summary>The left GUI key (often the Windows key).</summary>
    LGui = (uint)Native.SDL_Keycode.SDLK_LGUI,
    /// <summary>The right Ctrl (Control) key.</summary>
    RCtrl = (uint)Native.SDL_Keycode.SDLK_RCTRL,
    /// <summary>The right Shift key.</summary>
    RShift = (uint)Native.SDL_Keycode.SDLK_RSHIFT,
    /// <summary>The right Alt key.</summary>
    RAlt = (uint)Native.SDL_Keycode.SDLK_RALT,
    /// <summary>The right GUI key (often the Windows key).</summary>
    RGui = (uint)Native.SDL_Keycode.SDLK_RGUI,
}

/// <summary>
/// Key modifier flags.
/// </summary>
[Flags]
public enum KeyModifiers : ushort
{
    /// <summary>No modifier is applicable.</summary>
    None = (ushort)Native.SDL_Keymod.SDL_KMOD_NONE,
    /// <summary>The left Shift key is down.</summary>
    LShift = (ushort)Native.SDL_Keymod.SDL_KMOD_LSHIFT,
    /// <summary>The right Shift key is down.</summary>
    RShift = (ushort)Native.SDL_Keymod.SDL_KMOD_RSHIFT,
    /// <summary>The left Ctrl (Control) key is down.</summary>
    LCtrl = (ushort)Native.SDL_Keymod.SDL_KMOD_LCTRL,
    /// <summary>The right Ctrl (Control) key is down.</summary>
    RCtrl = (ushort)Native.SDL_Keymod.SDL_KMOD_RCTRL,
    /// <summary>The left Alt key is down.</summary>
    LAlt = (ushort)Native.SDL_Keymod.SDL_KMOD_LALT,
    /// <summary>The right Alt key is down.</summary>
    RAlt = (ushort)Native.SDL_Keymod.SDL_KMOD_RALT,
    /// <summary>The left GUI key (often the Windows key) is down.</summary>
    LGui = (ushort)Native.SDL_Keymod.SDL_KMOD_LGUI,
    /// <summary>The right GUI key (often the Windows key) is down.</summary>
    RGui = (ushort)Native.SDL_Keymod.SDL_KMOD_RGUI,
    /// <summary>The Num Lock key (may be located on an extended keypad) is down.</summary>
    Num = (ushort)Native.SDL_Keymod.SDL_KMOD_NUM,
    /// <summary>The Caps Lock key is down.</summary>
    Caps = (ushort)Native.SDL_Keymod.SDL_KMOD_CAPS,
    /// <summary>The AltGr key is down.</summary>
    Mode = (ushort)Native.SDL_Keymod.SDL_KMOD_MODE,
    /// <summary>The Scroll Lock key is down.</summary>
    Scroll = (ushort)Native.SDL_Keymod.SDL_KMOD_SCROLL,
    /// <summary>Any Ctrl key is down.</summary>
    Ctrl = LCtrl | RCtrl,
    /// <summary>Any Shift key is down.</summary>
    Shift = LShift | RShift,
    /// <summary>Any Alt key is down.</summary>
    Alt = LAlt | RAlt,
    /// <summary>Any GUI key is down.</summary>
    Gui = LGui | RGui,
}
