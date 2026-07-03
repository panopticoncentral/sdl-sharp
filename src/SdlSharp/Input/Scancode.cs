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
    /// <summary>The non-US "#" and "~" key (ISO keyboards, near the Return key).</summary>
    NonUsHash = (int)Native.SDL_Scancode.SDL_SCANCODE_NONUSHASH,
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
    /// <summary>The non-US backslash key (ISO keyboards, next to the left Shift key).</summary>
    NonUsBackslash = (int)Native.SDL_Scancode.SDL_SCANCODE_NONUSBACKSLASH,
    /// <summary>The Application key (windows contextual menu, compose).</summary>
    Application = (int)Native.SDL_Scancode.SDL_SCANCODE_APPLICATION,
    /// <summary>The Power key.</summary>
    Power = (int)Native.SDL_Scancode.SDL_SCANCODE_POWER,
    /// <summary>The keypad equals (=) key.</summary>
    KpEquals = (int)Native.SDL_Scancode.SDL_SCANCODE_KP_EQUALS,
    /// <summary>The F13 key.</summary>
    F13 = (int)Native.SDL_Scancode.SDL_SCANCODE_F13,
    /// <summary>The F14 key.</summary>
    F14 = (int)Native.SDL_Scancode.SDL_SCANCODE_F14,
    /// <summary>The F15 key.</summary>
    F15 = (int)Native.SDL_Scancode.SDL_SCANCODE_F15,
    /// <summary>The F16 key.</summary>
    F16 = (int)Native.SDL_Scancode.SDL_SCANCODE_F16,
    /// <summary>The F17 key.</summary>
    F17 = (int)Native.SDL_Scancode.SDL_SCANCODE_F17,
    /// <summary>The F18 key.</summary>
    F18 = (int)Native.SDL_Scancode.SDL_SCANCODE_F18,
    /// <summary>The F19 key.</summary>
    F19 = (int)Native.SDL_Scancode.SDL_SCANCODE_F19,
    /// <summary>The F20 key.</summary>
    F20 = (int)Native.SDL_Scancode.SDL_SCANCODE_F20,
    /// <summary>The F21 key.</summary>
    F21 = (int)Native.SDL_Scancode.SDL_SCANCODE_F21,
    /// <summary>The F22 key.</summary>
    F22 = (int)Native.SDL_Scancode.SDL_SCANCODE_F22,
    /// <summary>The F23 key.</summary>
    F23 = (int)Native.SDL_Scancode.SDL_SCANCODE_F23,
    /// <summary>The F24 key.</summary>
    F24 = (int)Native.SDL_Scancode.SDL_SCANCODE_F24,
    /// <summary>The Execute key.</summary>
    Execute = (int)Native.SDL_Scancode.SDL_SCANCODE_EXECUTE,
    /// <summary>The Help key (AL Integrated Help Center).</summary>
    Help = (int)Native.SDL_Scancode.SDL_SCANCODE_HELP,
    /// <summary>The Menu key (show menu).</summary>
    Menu = (int)Native.SDL_Scancode.SDL_SCANCODE_MENU,
    /// <summary>The Select key.</summary>
    Select = (int)Native.SDL_Scancode.SDL_SCANCODE_SELECT,
    /// <summary>The Stop key (AC Stop).</summary>
    Stop = (int)Native.SDL_Scancode.SDL_SCANCODE_STOP,
    /// <summary>The Again key (AC Redo/Repeat).</summary>
    Again = (int)Native.SDL_Scancode.SDL_SCANCODE_AGAIN,
    /// <summary>The Undo key (AC Undo).</summary>
    Undo = (int)Native.SDL_Scancode.SDL_SCANCODE_UNDO,
    /// <summary>The Cut key (AC Cut).</summary>
    Cut = (int)Native.SDL_Scancode.SDL_SCANCODE_CUT,
    /// <summary>The Copy key (AC Copy).</summary>
    Copy = (int)Native.SDL_Scancode.SDL_SCANCODE_COPY,
    /// <summary>The Paste key (AC Paste).</summary>
    Paste = (int)Native.SDL_Scancode.SDL_SCANCODE_PASTE,
    /// <summary>The Find key (AC Find).</summary>
    Find = (int)Native.SDL_Scancode.SDL_SCANCODE_FIND,
    /// <summary>The Mute key.</summary>
    Mute = (int)Native.SDL_Scancode.SDL_SCANCODE_MUTE,
    /// <summary>The Volume Up key.</summary>
    VolumeUp = (int)Native.SDL_Scancode.SDL_SCANCODE_VOLUMEUP,
    /// <summary>The Volume Down key.</summary>
    VolumeDown = (int)Native.SDL_Scancode.SDL_SCANCODE_VOLUMEDOWN,
    /// <summary>The keypad comma (,) key.</summary>
    KpComma = (int)Native.SDL_Scancode.SDL_SCANCODE_KP_COMMA,
    /// <summary>The keypad equals sign (AS/400 keyboards) key.</summary>
    KpEqualsAs400 = (int)Native.SDL_Scancode.SDL_SCANCODE_KP_EQUALSAS400,
    /// <summary>International key 1 (used on Asian keyboards; Ro on Japanese keyboards).</summary>
    International1 = (int)Native.SDL_Scancode.SDL_SCANCODE_INTERNATIONAL1,
    /// <summary>International key 2.</summary>
    International2 = (int)Native.SDL_Scancode.SDL_SCANCODE_INTERNATIONAL2,
    /// <summary>International key 3 (Yen).</summary>
    International3 = (int)Native.SDL_Scancode.SDL_SCANCODE_INTERNATIONAL3,
    /// <summary>International key 4.</summary>
    International4 = (int)Native.SDL_Scancode.SDL_SCANCODE_INTERNATIONAL4,
    /// <summary>International key 5.</summary>
    International5 = (int)Native.SDL_Scancode.SDL_SCANCODE_INTERNATIONAL5,
    /// <summary>International key 6.</summary>
    International6 = (int)Native.SDL_Scancode.SDL_SCANCODE_INTERNATIONAL6,
    /// <summary>International key 7.</summary>
    International7 = (int)Native.SDL_Scancode.SDL_SCANCODE_INTERNATIONAL7,
    /// <summary>International key 8.</summary>
    International8 = (int)Native.SDL_Scancode.SDL_SCANCODE_INTERNATIONAL8,
    /// <summary>International key 9.</summary>
    International9 = (int)Native.SDL_Scancode.SDL_SCANCODE_INTERNATIONAL9,
    /// <summary>Language key 1 (Hangul/English toggle).</summary>
    Lang1 = (int)Native.SDL_Scancode.SDL_SCANCODE_LANG1,
    /// <summary>Language key 2 (Hanja conversion).</summary>
    Lang2 = (int)Native.SDL_Scancode.SDL_SCANCODE_LANG2,
    /// <summary>Language key 3 (Katakana).</summary>
    Lang3 = (int)Native.SDL_Scancode.SDL_SCANCODE_LANG3,
    /// <summary>Language key 4 (Hiragana).</summary>
    Lang4 = (int)Native.SDL_Scancode.SDL_SCANCODE_LANG4,
    /// <summary>Language key 5 (Zenkaku/Hankaku).</summary>
    Lang5 = (int)Native.SDL_Scancode.SDL_SCANCODE_LANG5,
    /// <summary>Language key 6 (reserved).</summary>
    Lang6 = (int)Native.SDL_Scancode.SDL_SCANCODE_LANG6,
    /// <summary>Language key 7 (reserved).</summary>
    Lang7 = (int)Native.SDL_Scancode.SDL_SCANCODE_LANG7,
    /// <summary>Language key 8 (reserved).</summary>
    Lang8 = (int)Native.SDL_Scancode.SDL_SCANCODE_LANG8,
    /// <summary>Language key 9 (reserved).</summary>
    Lang9 = (int)Native.SDL_Scancode.SDL_SCANCODE_LANG9,
    /// <summary>The Alt Erase key (Erase-Eaze).</summary>
    AltErase = (int)Native.SDL_Scancode.SDL_SCANCODE_ALTERASE,
    /// <summary>The SysReq key.</summary>
    SysReq = (int)Native.SDL_Scancode.SDL_SCANCODE_SYSREQ,
    /// <summary>The Cancel key (AC Cancel).</summary>
    Cancel = (int)Native.SDL_Scancode.SDL_SCANCODE_CANCEL,
    /// <summary>The Clear key.</summary>
    Clear = (int)Native.SDL_Scancode.SDL_SCANCODE_CLEAR,
    /// <summary>The Prior key.</summary>
    Prior = (int)Native.SDL_Scancode.SDL_SCANCODE_PRIOR,
    /// <summary>The second Return key.</summary>
    Return2 = (int)Native.SDL_Scancode.SDL_SCANCODE_RETURN2,
    /// <summary>The Separator key.</summary>
    Separator = (int)Native.SDL_Scancode.SDL_SCANCODE_SEPARATOR,
    /// <summary>The Out key.</summary>
    Out = (int)Native.SDL_Scancode.SDL_SCANCODE_OUT,
    /// <summary>The Oper key.</summary>
    Oper = (int)Native.SDL_Scancode.SDL_SCANCODE_OPER,
    /// <summary>The Clear/Again key.</summary>
    ClearAgain = (int)Native.SDL_Scancode.SDL_SCANCODE_CLEARAGAIN,
    /// <summary>The CrSel key.</summary>
    CrSel = (int)Native.SDL_Scancode.SDL_SCANCODE_CRSEL,
    /// <summary>The ExSel key.</summary>
    ExSel = (int)Native.SDL_Scancode.SDL_SCANCODE_EXSEL,
    /// <summary>The keypad 00 key.</summary>
    Kp00 = (int)Native.SDL_Scancode.SDL_SCANCODE_KP_00,
    /// <summary>The keypad 000 key.</summary>
    Kp000 = (int)Native.SDL_Scancode.SDL_SCANCODE_KP_000,
    /// <summary>The Thousands Separator key.</summary>
    ThousandsSeparator = (int)Native.SDL_Scancode.SDL_SCANCODE_THOUSANDSSEPARATOR,
    /// <summary>The Decimal Separator key.</summary>
    DecimalSeparator = (int)Native.SDL_Scancode.SDL_SCANCODE_DECIMALSEPARATOR,
    /// <summary>The Currency Unit key.</summary>
    CurrencyUnit = (int)Native.SDL_Scancode.SDL_SCANCODE_CURRENCYUNIT,
    /// <summary>The Currency Subunit key.</summary>
    CurrencySubunit = (int)Native.SDL_Scancode.SDL_SCANCODE_CURRENCYSUBUNIT,
    /// <summary>The keypad left parenthesis key.</summary>
    KpLeftParen = (int)Native.SDL_Scancode.SDL_SCANCODE_KP_LEFTPAREN,
    /// <summary>The keypad right parenthesis key.</summary>
    KpRightParen = (int)Native.SDL_Scancode.SDL_SCANCODE_KP_RIGHTPAREN,
    /// <summary>The keypad left brace key.</summary>
    KpLeftBrace = (int)Native.SDL_Scancode.SDL_SCANCODE_KP_LEFTBRACE,
    /// <summary>The keypad right brace key.</summary>
    KpRightBrace = (int)Native.SDL_Scancode.SDL_SCANCODE_KP_RIGHTBRACE,
    /// <summary>The keypad Tab key.</summary>
    KpTab = (int)Native.SDL_Scancode.SDL_SCANCODE_KP_TAB,
    /// <summary>The keypad Backspace key.</summary>
    KpBackspace = (int)Native.SDL_Scancode.SDL_SCANCODE_KP_BACKSPACE,
    /// <summary>The keypad A key.</summary>
    KpA = (int)Native.SDL_Scancode.SDL_SCANCODE_KP_A,
    /// <summary>The keypad B key.</summary>
    KpB = (int)Native.SDL_Scancode.SDL_SCANCODE_KP_B,
    /// <summary>The keypad C key.</summary>
    KpC = (int)Native.SDL_Scancode.SDL_SCANCODE_KP_C,
    /// <summary>The keypad D key.</summary>
    KpD = (int)Native.SDL_Scancode.SDL_SCANCODE_KP_D,
    /// <summary>The keypad E key.</summary>
    KpE = (int)Native.SDL_Scancode.SDL_SCANCODE_KP_E,
    /// <summary>The keypad F key.</summary>
    KpF = (int)Native.SDL_Scancode.SDL_SCANCODE_KP_F,
    /// <summary>The keypad XOR key.</summary>
    KpXor = (int)Native.SDL_Scancode.SDL_SCANCODE_KP_XOR,
    /// <summary>The keypad power (^) key.</summary>
    KpPower = (int)Native.SDL_Scancode.SDL_SCANCODE_KP_POWER,
    /// <summary>The keypad percent (%) key.</summary>
    KpPercent = (int)Native.SDL_Scancode.SDL_SCANCODE_KP_PERCENT,
    /// <summary>The keypad less-than (&lt;) key.</summary>
    KpLess = (int)Native.SDL_Scancode.SDL_SCANCODE_KP_LESS,
    /// <summary>The keypad greater-than (&gt;) key.</summary>
    KpGreater = (int)Native.SDL_Scancode.SDL_SCANCODE_KP_GREATER,
    /// <summary>The keypad ampersand (&amp;) key.</summary>
    KpAmpersand = (int)Native.SDL_Scancode.SDL_SCANCODE_KP_AMPERSAND,
    /// <summary>The keypad double ampersand (&amp;&amp;) key.</summary>
    KpDblAmpersand = (int)Native.SDL_Scancode.SDL_SCANCODE_KP_DBLAMPERSAND,
    /// <summary>The keypad vertical bar (|) key.</summary>
    KpVerticalBar = (int)Native.SDL_Scancode.SDL_SCANCODE_KP_VERTICALBAR,
    /// <summary>The keypad double vertical bar (||) key.</summary>
    KpDblVerticalBar = (int)Native.SDL_Scancode.SDL_SCANCODE_KP_DBLVERTICALBAR,
    /// <summary>The keypad colon (:) key.</summary>
    KpColon = (int)Native.SDL_Scancode.SDL_SCANCODE_KP_COLON,
    /// <summary>The keypad hash (#) key.</summary>
    KpHash = (int)Native.SDL_Scancode.SDL_SCANCODE_KP_HASH,
    /// <summary>The keypad Space key.</summary>
    KpSpace = (int)Native.SDL_Scancode.SDL_SCANCODE_KP_SPACE,
    /// <summary>The keypad at (@) key.</summary>
    KpAt = (int)Native.SDL_Scancode.SDL_SCANCODE_KP_AT,
    /// <summary>The keypad exclaim (!) key.</summary>
    KpExclam = (int)Native.SDL_Scancode.SDL_SCANCODE_KP_EXCLAM,
    /// <summary>The keypad Memory Store key.</summary>
    KpMemStore = (int)Native.SDL_Scancode.SDL_SCANCODE_KP_MEMSTORE,
    /// <summary>The keypad Memory Recall key.</summary>
    KpMemRecall = (int)Native.SDL_Scancode.SDL_SCANCODE_KP_MEMRECALL,
    /// <summary>The keypad Memory Clear key.</summary>
    KpMemClear = (int)Native.SDL_Scancode.SDL_SCANCODE_KP_MEMCLEAR,
    /// <summary>The keypad Memory Add key.</summary>
    KpMemAdd = (int)Native.SDL_Scancode.SDL_SCANCODE_KP_MEMADD,
    /// <summary>The keypad Memory Subtract key.</summary>
    KpMemSubtract = (int)Native.SDL_Scancode.SDL_SCANCODE_KP_MEMSUBTRACT,
    /// <summary>The keypad Memory Multiply key.</summary>
    KpMemMultiply = (int)Native.SDL_Scancode.SDL_SCANCODE_KP_MEMMULTIPLY,
    /// <summary>The keypad Memory Divide key.</summary>
    KpMemDivide = (int)Native.SDL_Scancode.SDL_SCANCODE_KP_MEMDIVIDE,
    /// <summary>The keypad plus/minus (+/-) key.</summary>
    KpPlusMinus = (int)Native.SDL_Scancode.SDL_SCANCODE_KP_PLUSMINUS,
    /// <summary>The keypad Clear key.</summary>
    KpClear = (int)Native.SDL_Scancode.SDL_SCANCODE_KP_CLEAR,
    /// <summary>The keypad Clear Entry key.</summary>
    KpClearEntry = (int)Native.SDL_Scancode.SDL_SCANCODE_KP_CLEARENTRY,
    /// <summary>The keypad Binary key.</summary>
    KpBinary = (int)Native.SDL_Scancode.SDL_SCANCODE_KP_BINARY,
    /// <summary>The keypad Octal key.</summary>
    KpOctal = (int)Native.SDL_Scancode.SDL_SCANCODE_KP_OCTAL,
    /// <summary>The keypad Decimal key.</summary>
    KpDecimal = (int)Native.SDL_Scancode.SDL_SCANCODE_KP_DECIMAL,
    /// <summary>The keypad Hexadecimal key.</summary>
    KpHexadecimal = (int)Native.SDL_Scancode.SDL_SCANCODE_KP_HEXADECIMAL,
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

    // Mode key
    /// <summary>The Mode key (used with translated keycodes for an alt-like modifier).</summary>
    Mode = (int)Native.SDL_Scancode.SDL_SCANCODE_MODE,

    // Usage page 0x0C (Consumer) keys
    /// <summary>The Sleep key.</summary>
    Sleep = (int)Native.SDL_Scancode.SDL_SCANCODE_SLEEP,
    /// <summary>The Wake key.</summary>
    Wake = (int)Native.SDL_Scancode.SDL_SCANCODE_WAKE,
    /// <summary>The Channel Increment key.</summary>
    ChannelIncrement = (int)Native.SDL_Scancode.SDL_SCANCODE_CHANNEL_INCREMENT,
    /// <summary>The Channel Decrement key.</summary>
    ChannelDecrement = (int)Native.SDL_Scancode.SDL_SCANCODE_CHANNEL_DECREMENT,
    /// <summary>The Media Play key.</summary>
    MediaPlay = (int)Native.SDL_Scancode.SDL_SCANCODE_MEDIA_PLAY,
    /// <summary>The Media Pause key.</summary>
    MediaPause = (int)Native.SDL_Scancode.SDL_SCANCODE_MEDIA_PAUSE,
    /// <summary>The Media Record key.</summary>
    MediaRecord = (int)Native.SDL_Scancode.SDL_SCANCODE_MEDIA_RECORD,
    /// <summary>The Media Fast Forward key.</summary>
    MediaFastForward = (int)Native.SDL_Scancode.SDL_SCANCODE_MEDIA_FAST_FORWARD,
    /// <summary>The Media Rewind key.</summary>
    MediaRewind = (int)Native.SDL_Scancode.SDL_SCANCODE_MEDIA_REWIND,
    /// <summary>The Media Next Track key.</summary>
    MediaNextTrack = (int)Native.SDL_Scancode.SDL_SCANCODE_MEDIA_NEXT_TRACK,
    /// <summary>The Media Previous Track key.</summary>
    MediaPreviousTrack = (int)Native.SDL_Scancode.SDL_SCANCODE_MEDIA_PREVIOUS_TRACK,
    /// <summary>The Media Stop key.</summary>
    MediaStop = (int)Native.SDL_Scancode.SDL_SCANCODE_MEDIA_STOP,
    /// <summary>The Media Eject key.</summary>
    MediaEject = (int)Native.SDL_Scancode.SDL_SCANCODE_MEDIA_EJECT,
    /// <summary>The Media Play/Pause key.</summary>
    MediaPlayPause = (int)Native.SDL_Scancode.SDL_SCANCODE_MEDIA_PLAY_PAUSE,
    /// <summary>The Media Select key.</summary>
    MediaSelect = (int)Native.SDL_Scancode.SDL_SCANCODE_MEDIA_SELECT,
    /// <summary>The AC New key.</summary>
    AcNew = (int)Native.SDL_Scancode.SDL_SCANCODE_AC_NEW,
    /// <summary>The AC Open key.</summary>
    AcOpen = (int)Native.SDL_Scancode.SDL_SCANCODE_AC_OPEN,
    /// <summary>The AC Close key.</summary>
    AcClose = (int)Native.SDL_Scancode.SDL_SCANCODE_AC_CLOSE,
    /// <summary>The AC Exit key.</summary>
    AcExit = (int)Native.SDL_Scancode.SDL_SCANCODE_AC_EXIT,
    /// <summary>The AC Save key.</summary>
    AcSave = (int)Native.SDL_Scancode.SDL_SCANCODE_AC_SAVE,
    /// <summary>The AC Print key.</summary>
    AcPrint = (int)Native.SDL_Scancode.SDL_SCANCODE_AC_PRINT,
    /// <summary>The AC Properties key.</summary>
    AcProperties = (int)Native.SDL_Scancode.SDL_SCANCODE_AC_PROPERTIES,
    /// <summary>The AC Search key.</summary>
    AcSearch = (int)Native.SDL_Scancode.SDL_SCANCODE_AC_SEARCH,
    /// <summary>The AC Home key.</summary>
    AcHome = (int)Native.SDL_Scancode.SDL_SCANCODE_AC_HOME,
    /// <summary>The AC Back key.</summary>
    AcBack = (int)Native.SDL_Scancode.SDL_SCANCODE_AC_BACK,
    /// <summary>The AC Forward key.</summary>
    AcForward = (int)Native.SDL_Scancode.SDL_SCANCODE_AC_FORWARD,
    /// <summary>The AC Stop key.</summary>
    AcStop = (int)Native.SDL_Scancode.SDL_SCANCODE_AC_STOP,
    /// <summary>The AC Refresh key.</summary>
    AcRefresh = (int)Native.SDL_Scancode.SDL_SCANCODE_AC_REFRESH,
    /// <summary>The AC Bookmarks key.</summary>
    AcBookmarks = (int)Native.SDL_Scancode.SDL_SCANCODE_AC_BOOKMARKS,

    // Mobile keys
    /// <summary>The Soft Left key, usually situated below the display on phones and used as a multi-function feature key for selecting a software-defined function shown on the bottom left of the display.</summary>
    SoftLeft = (int)Native.SDL_Scancode.SDL_SCANCODE_SOFTLEFT,
    /// <summary>The Soft Right key, usually situated below the display on phones and used as a multi-function feature key for selecting a software-defined function shown on the bottom right of the display.</summary>
    SoftRight = (int)Native.SDL_Scancode.SDL_SCANCODE_SOFTRIGHT,
    /// <summary>The Call key, used for accepting phone calls.</summary>
    Call = (int)Native.SDL_Scancode.SDL_SCANCODE_CALL,
    /// <summary>The End Call key, used for rejecting phone calls.</summary>
    EndCall = (int)Native.SDL_Scancode.SDL_SCANCODE_ENDCALL,

    // Reserved and bookkeeping values
    /// <summary>Reserved; 400-500 reserved for dynamic keycodes.</summary>
    Reserved = (int)Native.SDL_Scancode.SDL_SCANCODE_RESERVED,
    /// <summary>Not a key; marks the number of scancodes for array bounds.</summary>
    Count = (int)Native.SDL_Scancode.SDL_SCANCODE_COUNT,
}
