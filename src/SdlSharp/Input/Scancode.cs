namespace SdlSharp.Input;

/// <summary>
/// Physical keyboard scancode, independent of keyboard layout.
/// </summary>
public enum Scancode
{
    /// <summary>Unknown scancode.</summary>
    Unknown = (int)Native.SDL_Scancode.SDL_SCANCODE_UNKNOWN,
    /// <summary>The A key.</summary>
    A = (int)Native.SDL_Scancode.SDL_SCANCODE_A,
    /// <summary>The B key.</summary>
    B = (int)Native.SDL_Scancode.SDL_SCANCODE_B,
    /// <summary>The C key.</summary>
    C = (int)Native.SDL_Scancode.SDL_SCANCODE_C,
    /// <summary>The D key.</summary>
    D = (int)Native.SDL_Scancode.SDL_SCANCODE_D,
    /// <summary>The E key.</summary>
    E = (int)Native.SDL_Scancode.SDL_SCANCODE_E,
    /// <summary>The F key.</summary>
    F = (int)Native.SDL_Scancode.SDL_SCANCODE_F,
    /// <summary>The G key.</summary>
    G = (int)Native.SDL_Scancode.SDL_SCANCODE_G,
    /// <summary>The H key.</summary>
    H = (int)Native.SDL_Scancode.SDL_SCANCODE_H,
    /// <summary>The I key.</summary>
    I = (int)Native.SDL_Scancode.SDL_SCANCODE_I,
    /// <summary>The J key.</summary>
    J = (int)Native.SDL_Scancode.SDL_SCANCODE_J,
    /// <summary>The K key.</summary>
    K = (int)Native.SDL_Scancode.SDL_SCANCODE_K,
    /// <summary>The L key.</summary>
    L = (int)Native.SDL_Scancode.SDL_SCANCODE_L,
    /// <summary>The M key.</summary>
    M = (int)Native.SDL_Scancode.SDL_SCANCODE_M,
    /// <summary>The N key.</summary>
    N = (int)Native.SDL_Scancode.SDL_SCANCODE_N,
    /// <summary>The O key.</summary>
    O = (int)Native.SDL_Scancode.SDL_SCANCODE_O,
    /// <summary>The P key.</summary>
    P = (int)Native.SDL_Scancode.SDL_SCANCODE_P,
    /// <summary>The Q key.</summary>
    Q = (int)Native.SDL_Scancode.SDL_SCANCODE_Q,
    /// <summary>The R key.</summary>
    R = (int)Native.SDL_Scancode.SDL_SCANCODE_R,
    /// <summary>The S key.</summary>
    S = (int)Native.SDL_Scancode.SDL_SCANCODE_S,
    /// <summary>The T key.</summary>
    T = (int)Native.SDL_Scancode.SDL_SCANCODE_T,
    /// <summary>The U key.</summary>
    U = (int)Native.SDL_Scancode.SDL_SCANCODE_U,
    /// <summary>The V key.</summary>
    V = (int)Native.SDL_Scancode.SDL_SCANCODE_V,
    /// <summary>The W key.</summary>
    W = (int)Native.SDL_Scancode.SDL_SCANCODE_W,
    /// <summary>The X key.</summary>
    X = (int)Native.SDL_Scancode.SDL_SCANCODE_X,
    /// <summary>The Y key.</summary>
    Y = (int)Native.SDL_Scancode.SDL_SCANCODE_Y,
    /// <summary>The Z key.</summary>
    Z = (int)Native.SDL_Scancode.SDL_SCANCODE_Z,
    /// <summary>The 1 key.</summary>
    Num1 = (int)Native.SDL_Scancode.SDL_SCANCODE_1,
    /// <summary>The 2 key.</summary>
    Num2 = (int)Native.SDL_Scancode.SDL_SCANCODE_2,
    /// <summary>The 3 key.</summary>
    Num3 = (int)Native.SDL_Scancode.SDL_SCANCODE_3,
    /// <summary>The 4 key.</summary>
    Num4 = (int)Native.SDL_Scancode.SDL_SCANCODE_4,
    /// <summary>The 5 key.</summary>
    Num5 = (int)Native.SDL_Scancode.SDL_SCANCODE_5,
    /// <summary>The 6 key.</summary>
    Num6 = (int)Native.SDL_Scancode.SDL_SCANCODE_6,
    /// <summary>The 7 key.</summary>
    Num7 = (int)Native.SDL_Scancode.SDL_SCANCODE_7,
    /// <summary>The 8 key.</summary>
    Num8 = (int)Native.SDL_Scancode.SDL_SCANCODE_8,
    /// <summary>The 9 key.</summary>
    Num9 = (int)Native.SDL_Scancode.SDL_SCANCODE_9,
    /// <summary>The 0 key.</summary>
    Num0 = (int)Native.SDL_Scancode.SDL_SCANCODE_0,
    /// <summary>The Return (Enter) key.</summary>
    Return = (int)Native.SDL_Scancode.SDL_SCANCODE_RETURN,
    /// <summary>The Escape key.</summary>
    Escape = (int)Native.SDL_Scancode.SDL_SCANCODE_ESCAPE,
    /// <summary>The Backspace key.</summary>
    Backspace = (int)Native.SDL_Scancode.SDL_SCANCODE_BACKSPACE,
    /// <summary>The Tab key.</summary>
    Tab = (int)Native.SDL_Scancode.SDL_SCANCODE_TAB,
    /// <summary>The Space key.</summary>
    Space = (int)Native.SDL_Scancode.SDL_SCANCODE_SPACE,
    /// <summary>The minus (-) key.</summary>
    Minus = (int)Native.SDL_Scancode.SDL_SCANCODE_MINUS,
    /// <summary>The equals (=) key.</summary>
    Equals = (int)Native.SDL_Scancode.SDL_SCANCODE_EQUALS,
    /// <summary>The left bracket ([) key.</summary>
    LeftBracket = (int)Native.SDL_Scancode.SDL_SCANCODE_LEFTBRACKET,
    /// <summary>The right bracket (]) key.</summary>
    RightBracket = (int)Native.SDL_Scancode.SDL_SCANCODE_RIGHTBRACKET,
    /// <summary>The backslash (\) key.</summary>
    Backslash = (int)Native.SDL_Scancode.SDL_SCANCODE_BACKSLASH,
    /// <summary>The semicolon (;) key.</summary>
    Semicolon = (int)Native.SDL_Scancode.SDL_SCANCODE_SEMICOLON,
    /// <summary>The apostrophe (') key.</summary>
    Apostrophe = (int)Native.SDL_Scancode.SDL_SCANCODE_APOSTROPHE,
    /// <summary>The grave accent (`) key.</summary>
    Grave = (int)Native.SDL_Scancode.SDL_SCANCODE_GRAVE,
    /// <summary>The comma (,) key.</summary>
    Comma = (int)Native.SDL_Scancode.SDL_SCANCODE_COMMA,
    /// <summary>The period (.) key.</summary>
    Period = (int)Native.SDL_Scancode.SDL_SCANCODE_PERIOD,
    /// <summary>The slash (/) key.</summary>
    Slash = (int)Native.SDL_Scancode.SDL_SCANCODE_SLASH,
    /// <summary>The Caps Lock key.</summary>
    CapsLock = (int)Native.SDL_Scancode.SDL_SCANCODE_CAPSLOCK,
    /// <summary>The F1 key.</summary>
    F1 = (int)Native.SDL_Scancode.SDL_SCANCODE_F1,
    /// <summary>The F2 key.</summary>
    F2 = (int)Native.SDL_Scancode.SDL_SCANCODE_F2,
    /// <summary>The F3 key.</summary>
    F3 = (int)Native.SDL_Scancode.SDL_SCANCODE_F3,
    /// <summary>The F4 key.</summary>
    F4 = (int)Native.SDL_Scancode.SDL_SCANCODE_F4,
    /// <summary>The F5 key.</summary>
    F5 = (int)Native.SDL_Scancode.SDL_SCANCODE_F5,
    /// <summary>The F6 key.</summary>
    F6 = (int)Native.SDL_Scancode.SDL_SCANCODE_F6,
    /// <summary>The F7 key.</summary>
    F7 = (int)Native.SDL_Scancode.SDL_SCANCODE_F7,
    /// <summary>The F8 key.</summary>
    F8 = (int)Native.SDL_Scancode.SDL_SCANCODE_F8,
    /// <summary>The F9 key.</summary>
    F9 = (int)Native.SDL_Scancode.SDL_SCANCODE_F9,
    /// <summary>The F10 key.</summary>
    F10 = (int)Native.SDL_Scancode.SDL_SCANCODE_F10,
    /// <summary>The F11 key.</summary>
    F11 = (int)Native.SDL_Scancode.SDL_SCANCODE_F11,
    /// <summary>The F12 key.</summary>
    F12 = (int)Native.SDL_Scancode.SDL_SCANCODE_F12,
    /// <summary>The Print Screen key.</summary>
    PrintScreen = (int)Native.SDL_Scancode.SDL_SCANCODE_PRINTSCREEN,
    /// <summary>The Scroll Lock key.</summary>
    ScrollLock = (int)Native.SDL_Scancode.SDL_SCANCODE_SCROLLLOCK,
    /// <summary>The Pause key.</summary>
    Pause = (int)Native.SDL_Scancode.SDL_SCANCODE_PAUSE,
    /// <summary>The Insert key.</summary>
    Insert = (int)Native.SDL_Scancode.SDL_SCANCODE_INSERT,
    /// <summary>The Home key.</summary>
    Home = (int)Native.SDL_Scancode.SDL_SCANCODE_HOME,
    /// <summary>The Page Up key.</summary>
    PageUp = (int)Native.SDL_Scancode.SDL_SCANCODE_PAGEUP,
    /// <summary>The Delete key.</summary>
    Delete = (int)Native.SDL_Scancode.SDL_SCANCODE_DELETE,
    /// <summary>The End key.</summary>
    End = (int)Native.SDL_Scancode.SDL_SCANCODE_END,
    /// <summary>The Page Down key.</summary>
    PageDown = (int)Native.SDL_Scancode.SDL_SCANCODE_PAGEDOWN,
    /// <summary>The right arrow key.</summary>
    Right = (int)Native.SDL_Scancode.SDL_SCANCODE_RIGHT,
    /// <summary>The left arrow key.</summary>
    Left = (int)Native.SDL_Scancode.SDL_SCANCODE_LEFT,
    /// <summary>The down arrow key.</summary>
    Down = (int)Native.SDL_Scancode.SDL_SCANCODE_DOWN,
    /// <summary>The up arrow key.</summary>
    Up = (int)Native.SDL_Scancode.SDL_SCANCODE_UP,
    /// <summary>The Num Lock key (Clear on Mac keyboards).</summary>
    NumLockClear = (int)Native.SDL_Scancode.SDL_SCANCODE_NUMLOCKCLEAR,
    /// <summary>The keypad divide (/) key.</summary>
    KpDivide = (int)Native.SDL_Scancode.SDL_SCANCODE_KP_DIVIDE,
    /// <summary>The keypad multiply (*) key.</summary>
    KpMultiply = (int)Native.SDL_Scancode.SDL_SCANCODE_KP_MULTIPLY,
    /// <summary>The keypad minus (-) key.</summary>
    KpMinus = (int)Native.SDL_Scancode.SDL_SCANCODE_KP_MINUS,
    /// <summary>The keypad plus (+) key.</summary>
    KpPlus = (int)Native.SDL_Scancode.SDL_SCANCODE_KP_PLUS,
    /// <summary>The keypad Enter key.</summary>
    KpEnter = (int)Native.SDL_Scancode.SDL_SCANCODE_KP_ENTER,
    /// <summary>The keypad 1 key.</summary>
    Kp1 = (int)Native.SDL_Scancode.SDL_SCANCODE_KP_1,
    /// <summary>The keypad 2 key.</summary>
    Kp2 = (int)Native.SDL_Scancode.SDL_SCANCODE_KP_2,
    /// <summary>The keypad 3 key.</summary>
    Kp3 = (int)Native.SDL_Scancode.SDL_SCANCODE_KP_3,
    /// <summary>The keypad 4 key.</summary>
    Kp4 = (int)Native.SDL_Scancode.SDL_SCANCODE_KP_4,
    /// <summary>The keypad 5 key.</summary>
    Kp5 = (int)Native.SDL_Scancode.SDL_SCANCODE_KP_5,
    /// <summary>The keypad 6 key.</summary>
    Kp6 = (int)Native.SDL_Scancode.SDL_SCANCODE_KP_6,
    /// <summary>The keypad 7 key.</summary>
    Kp7 = (int)Native.SDL_Scancode.SDL_SCANCODE_KP_7,
    /// <summary>The keypad 8 key.</summary>
    Kp8 = (int)Native.SDL_Scancode.SDL_SCANCODE_KP_8,
    /// <summary>The keypad 9 key.</summary>
    Kp9 = (int)Native.SDL_Scancode.SDL_SCANCODE_KP_9,
    /// <summary>The keypad 0 key.</summary>
    Kp0 = (int)Native.SDL_Scancode.SDL_SCANCODE_KP_0,
    /// <summary>The keypad period (.) key.</summary>
    KpPeriod = (int)Native.SDL_Scancode.SDL_SCANCODE_KP_PERIOD,
    /// <summary>The Application key (windows contextual menu, compose).</summary>
    Application = (int)Native.SDL_Scancode.SDL_SCANCODE_APPLICATION,
    /// <summary>The left Ctrl key.</summary>
    LCtrl = (int)Native.SDL_Scancode.SDL_SCANCODE_LCTRL,
    /// <summary>The left Shift key.</summary>
    LShift = (int)Native.SDL_Scancode.SDL_SCANCODE_LSHIFT,
    /// <summary>The left Alt key (alt, option).</summary>
    LAlt = (int)Native.SDL_Scancode.SDL_SCANCODE_LALT,
    /// <summary>The left GUI key (windows, command (apple), meta).</summary>
    LGui = (int)Native.SDL_Scancode.SDL_SCANCODE_LGUI,
    /// <summary>The right Ctrl key.</summary>
    RCtrl = (int)Native.SDL_Scancode.SDL_SCANCODE_RCTRL,
    /// <summary>The right Shift key.</summary>
    RShift = (int)Native.SDL_Scancode.SDL_SCANCODE_RSHIFT,
    /// <summary>The right Alt key (alt gr, option).</summary>
    RAlt = (int)Native.SDL_Scancode.SDL_SCANCODE_RALT,
    /// <summary>The right GUI key (windows, command (apple), meta).</summary>
    RGui = (int)Native.SDL_Scancode.SDL_SCANCODE_RGUI,
}
