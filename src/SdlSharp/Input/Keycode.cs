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
    /// <summary>The exclaim ('!') key.</summary>
    Exclaim = (uint)Native.SDL_Keycode.SDLK_EXCLAIM,
    /// <summary>The double apostrophe ('"') key.</summary>
    DblApostrophe = (uint)Native.SDL_Keycode.SDLK_DBLAPOSTROPHE,
    /// <summary>The hash ('#') key.</summary>
    Hash = (uint)Native.SDL_Keycode.SDLK_HASH,
    /// <summary>The dollar ('$') key.</summary>
    Dollar = (uint)Native.SDL_Keycode.SDLK_DOLLAR,
    /// <summary>The percent ('%') key.</summary>
    Percent = (uint)Native.SDL_Keycode.SDLK_PERCENT,
    /// <summary>The ampersand ('&amp;') key.</summary>
    Ampersand = (uint)Native.SDL_Keycode.SDLK_AMPERSAND,
    /// <summary>The apostrophe (''') key.</summary>
    Apostrophe = (uint)Native.SDL_Keycode.SDLK_APOSTROPHE,
    /// <summary>The left parenthesis ('(') key.</summary>
    LeftParen = (uint)Native.SDL_Keycode.SDLK_LEFTPAREN,
    /// <summary>The right parenthesis (')') key.</summary>
    RightParen = (uint)Native.SDL_Keycode.SDLK_RIGHTPAREN,
    /// <summary>The asterisk ('*') key.</summary>
    Asterisk = (uint)Native.SDL_Keycode.SDLK_ASTERISK,
    /// <summary>The plus ('+') key.</summary>
    Plus = (uint)Native.SDL_Keycode.SDLK_PLUS,
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
    /// <summary>The colon (':') key.</summary>
    Colon = (uint)Native.SDL_Keycode.SDLK_COLON,
    /// <summary>The semicolon (';') key.</summary>
    Semicolon = (uint)Native.SDL_Keycode.SDLK_SEMICOLON,
    /// <summary>The less-than ('&lt;') key.</summary>
    Less = (uint)Native.SDL_Keycode.SDLK_LESS,
    /// <summary>The equals ('=') key.</summary>
    Equals = (uint)Native.SDL_Keycode.SDLK_EQUALS,
    /// <summary>The greater-than ('&gt;') key.</summary>
    Greater = (uint)Native.SDL_Keycode.SDLK_GREATER,
    /// <summary>The question mark ('?') key.</summary>
    Question = (uint)Native.SDL_Keycode.SDLK_QUESTION,
    /// <summary>The at ('@') key.</summary>
    At = (uint)Native.SDL_Keycode.SDLK_AT,
    /// <summary>The left bracket ('[') key.</summary>
    LeftBracket = (uint)Native.SDL_Keycode.SDLK_LEFTBRACKET,
    /// <summary>The backslash ('\') key.</summary>
    Backslash = (uint)Native.SDL_Keycode.SDLK_BACKSLASH,
    /// <summary>The right bracket (']') key.</summary>
    RightBracket = (uint)Native.SDL_Keycode.SDLK_RIGHTBRACKET,
    /// <summary>The caret ('^') key.</summary>
    Caret = (uint)Native.SDL_Keycode.SDLK_CARET,
    /// <summary>The underscore ('_') key.</summary>
    Underscore = (uint)Native.SDL_Keycode.SDLK_UNDERSCORE,
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
    /// <summary>The left brace ('{') key.</summary>
    LeftBrace = (uint)Native.SDL_Keycode.SDLK_LEFTBRACE,
    /// <summary>The pipe ('|') key.</summary>
    Pipe = (uint)Native.SDL_Keycode.SDLK_PIPE,
    /// <summary>The right brace ('}') key.</summary>
    RightBrace = (uint)Native.SDL_Keycode.SDLK_RIGHTBRACE,
    /// <summary>The tilde ('~') key.</summary>
    Tilde = (uint)Native.SDL_Keycode.SDLK_TILDE,
    /// <summary>The Delete key.</summary>
    Delete = (uint)Native.SDL_Keycode.SDLK_DELETE,
    /// <summary>The plus/minus ('±') key.</summary>
    PlusMinus = (uint)Native.SDL_Keycode.SDLK_PLUSMINUS,
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
    /// <summary>The Num Lock (Clear on macOS keyboards) key.</summary>
    NumLockClear = (uint)Native.SDL_Keycode.SDLK_NUMLOCKCLEAR,
    /// <summary>The keypad divide (/) key.</summary>
    KpDivide = (uint)Native.SDL_Keycode.SDLK_KP_DIVIDE,
    /// <summary>The keypad multiply (*) key.</summary>
    KpMultiply = (uint)Native.SDL_Keycode.SDLK_KP_MULTIPLY,
    /// <summary>The keypad minus (-) key.</summary>
    KpMinus = (uint)Native.SDL_Keycode.SDLK_KP_MINUS,
    /// <summary>The keypad plus (+) key.</summary>
    KpPlus = (uint)Native.SDL_Keycode.SDLK_KP_PLUS,
    /// <summary>The keypad Enter key.</summary>
    KpEnter = (uint)Native.SDL_Keycode.SDLK_KP_ENTER,
    /// <summary>The keypad 1 key.</summary>
    Kp1 = (uint)Native.SDL_Keycode.SDLK_KP_1,
    /// <summary>The keypad 2 key.</summary>
    Kp2 = (uint)Native.SDL_Keycode.SDLK_KP_2,
    /// <summary>The keypad 3 key.</summary>
    Kp3 = (uint)Native.SDL_Keycode.SDLK_KP_3,
    /// <summary>The keypad 4 key.</summary>
    Kp4 = (uint)Native.SDL_Keycode.SDLK_KP_4,
    /// <summary>The keypad 5 key.</summary>
    Kp5 = (uint)Native.SDL_Keycode.SDLK_KP_5,
    /// <summary>The keypad 6 key.</summary>
    Kp6 = (uint)Native.SDL_Keycode.SDLK_KP_6,
    /// <summary>The keypad 7 key.</summary>
    Kp7 = (uint)Native.SDL_Keycode.SDLK_KP_7,
    /// <summary>The keypad 8 key.</summary>
    Kp8 = (uint)Native.SDL_Keycode.SDLK_KP_8,
    /// <summary>The keypad 9 key.</summary>
    Kp9 = (uint)Native.SDL_Keycode.SDLK_KP_9,
    /// <summary>The keypad 0 key.</summary>
    Kp0 = (uint)Native.SDL_Keycode.SDLK_KP_0,
    /// <summary>The keypad period (.) key.</summary>
    KpPeriod = (uint)Native.SDL_Keycode.SDLK_KP_PERIOD,
    /// <summary>The Application key (windows contextual menu, compose).</summary>
    Application = (uint)Native.SDL_Keycode.SDLK_APPLICATION,
    /// <summary>The Power key.</summary>
    Power = (uint)Native.SDL_Keycode.SDLK_POWER,
    /// <summary>The keypad equals (=) key.</summary>
    KpEquals = (uint)Native.SDL_Keycode.SDLK_KP_EQUALS,
    /// <summary>The F13 key.</summary>
    F13 = (uint)Native.SDL_Keycode.SDLK_F13,
    /// <summary>The F14 key.</summary>
    F14 = (uint)Native.SDL_Keycode.SDLK_F14,
    /// <summary>The F15 key.</summary>
    F15 = (uint)Native.SDL_Keycode.SDLK_F15,
    /// <summary>The F16 key.</summary>
    F16 = (uint)Native.SDL_Keycode.SDLK_F16,
    /// <summary>The F17 key.</summary>
    F17 = (uint)Native.SDL_Keycode.SDLK_F17,
    /// <summary>The F18 key.</summary>
    F18 = (uint)Native.SDL_Keycode.SDLK_F18,
    /// <summary>The F19 key.</summary>
    F19 = (uint)Native.SDL_Keycode.SDLK_F19,
    /// <summary>The F20 key.</summary>
    F20 = (uint)Native.SDL_Keycode.SDLK_F20,
    /// <summary>The F21 key.</summary>
    F21 = (uint)Native.SDL_Keycode.SDLK_F21,
    /// <summary>The F22 key.</summary>
    F22 = (uint)Native.SDL_Keycode.SDLK_F22,
    /// <summary>The F23 key.</summary>
    F23 = (uint)Native.SDL_Keycode.SDLK_F23,
    /// <summary>The F24 key.</summary>
    F24 = (uint)Native.SDL_Keycode.SDLK_F24,
    /// <summary>The Execute key.</summary>
    Execute = (uint)Native.SDL_Keycode.SDLK_EXECUTE,
    /// <summary>The Help key (AL Integrated Help Center).</summary>
    Help = (uint)Native.SDL_Keycode.SDLK_HELP,
    /// <summary>The Menu key (show menu).</summary>
    Menu = (uint)Native.SDL_Keycode.SDLK_MENU,
    /// <summary>The Select key.</summary>
    Select = (uint)Native.SDL_Keycode.SDLK_SELECT,
    /// <summary>The Stop key (AC Stop).</summary>
    Stop = (uint)Native.SDL_Keycode.SDLK_STOP,
    /// <summary>The Again key (AC Redo/Repeat).</summary>
    Again = (uint)Native.SDL_Keycode.SDLK_AGAIN,
    /// <summary>The Undo key (AC Undo).</summary>
    Undo = (uint)Native.SDL_Keycode.SDLK_UNDO,
    /// <summary>The Cut key (AC Cut).</summary>
    Cut = (uint)Native.SDL_Keycode.SDLK_CUT,
    /// <summary>The Copy key (AC Copy).</summary>
    Copy = (uint)Native.SDL_Keycode.SDLK_COPY,
    /// <summary>The Paste key (AC Paste).</summary>
    Paste = (uint)Native.SDL_Keycode.SDLK_PASTE,
    /// <summary>The Find key (AC Find).</summary>
    Find = (uint)Native.SDL_Keycode.SDLK_FIND,
    /// <summary>The Mute key.</summary>
    Mute = (uint)Native.SDL_Keycode.SDLK_MUTE,
    /// <summary>The Volume Up key.</summary>
    VolumeUp = (uint)Native.SDL_Keycode.SDLK_VOLUMEUP,
    /// <summary>The Volume Down key.</summary>
    VolumeDown = (uint)Native.SDL_Keycode.SDLK_VOLUMEDOWN,
    /// <summary>The keypad comma (,) key.</summary>
    KpComma = (uint)Native.SDL_Keycode.SDLK_KP_COMMA,
    /// <summary>The keypad equals sign (AS/400 keyboards) key.</summary>
    KpEqualsAs400 = (uint)Native.SDL_Keycode.SDLK_KP_EQUALSAS400,
    /// <summary>The Alt Erase key (Erase-Eaze).</summary>
    AltErase = (uint)Native.SDL_Keycode.SDLK_ALTERASE,
    /// <summary>The SysReq key.</summary>
    SysReq = (uint)Native.SDL_Keycode.SDLK_SYSREQ,
    /// <summary>The Cancel key (AC Cancel).</summary>
    Cancel = (uint)Native.SDL_Keycode.SDLK_CANCEL,
    /// <summary>The Clear key.</summary>
    Clear = (uint)Native.SDL_Keycode.SDLK_CLEAR,
    /// <summary>The Prior key.</summary>
    Prior = (uint)Native.SDL_Keycode.SDLK_PRIOR,
    /// <summary>The second Return key.</summary>
    Return2 = (uint)Native.SDL_Keycode.SDLK_RETURN2,
    /// <summary>The Separator key.</summary>
    Separator = (uint)Native.SDL_Keycode.SDLK_SEPARATOR,
    /// <summary>The Out key.</summary>
    Out = (uint)Native.SDL_Keycode.SDLK_OUT,
    /// <summary>The Oper key.</summary>
    Oper = (uint)Native.SDL_Keycode.SDLK_OPER,
    /// <summary>The Clear/Again key.</summary>
    ClearAgain = (uint)Native.SDL_Keycode.SDLK_CLEARAGAIN,
    /// <summary>The CrSel key.</summary>
    CrSel = (uint)Native.SDL_Keycode.SDLK_CRSEL,
    /// <summary>The ExSel key.</summary>
    ExSel = (uint)Native.SDL_Keycode.SDLK_EXSEL,
    /// <summary>The keypad 00 key.</summary>
    Kp00 = (uint)Native.SDL_Keycode.SDLK_KP_00,
    /// <summary>The keypad 000 key.</summary>
    Kp000 = (uint)Native.SDL_Keycode.SDLK_KP_000,
    /// <summary>The Thousands Separator key.</summary>
    ThousandsSeparator = (uint)Native.SDL_Keycode.SDLK_THOUSANDSSEPARATOR,
    /// <summary>The Decimal Separator key.</summary>
    DecimalSeparator = (uint)Native.SDL_Keycode.SDLK_DECIMALSEPARATOR,
    /// <summary>The Currency Unit key.</summary>
    CurrencyUnit = (uint)Native.SDL_Keycode.SDLK_CURRENCYUNIT,
    /// <summary>The Currency Subunit key.</summary>
    CurrencySubunit = (uint)Native.SDL_Keycode.SDLK_CURRENCYSUBUNIT,
    /// <summary>The keypad left parenthesis key.</summary>
    KpLeftParen = (uint)Native.SDL_Keycode.SDLK_KP_LEFTPAREN,
    /// <summary>The keypad right parenthesis key.</summary>
    KpRightParen = (uint)Native.SDL_Keycode.SDLK_KP_RIGHTPAREN,
    /// <summary>The keypad left brace key.</summary>
    KpLeftBrace = (uint)Native.SDL_Keycode.SDLK_KP_LEFTBRACE,
    /// <summary>The keypad right brace key.</summary>
    KpRightBrace = (uint)Native.SDL_Keycode.SDLK_KP_RIGHTBRACE,
    /// <summary>The keypad Tab key.</summary>
    KpTab = (uint)Native.SDL_Keycode.SDLK_KP_TAB,
    /// <summary>The keypad Backspace key.</summary>
    KpBackspace = (uint)Native.SDL_Keycode.SDLK_KP_BACKSPACE,
    /// <summary>The keypad A key.</summary>
    KpA = (uint)Native.SDL_Keycode.SDLK_KP_A,
    /// <summary>The keypad B key.</summary>
    KpB = (uint)Native.SDL_Keycode.SDLK_KP_B,
    /// <summary>The keypad C key.</summary>
    KpC = (uint)Native.SDL_Keycode.SDLK_KP_C,
    /// <summary>The keypad D key.</summary>
    KpD = (uint)Native.SDL_Keycode.SDLK_KP_D,
    /// <summary>The keypad E key.</summary>
    KpE = (uint)Native.SDL_Keycode.SDLK_KP_E,
    /// <summary>The keypad F key.</summary>
    KpF = (uint)Native.SDL_Keycode.SDLK_KP_F,
    /// <summary>The keypad XOR key.</summary>
    KpXor = (uint)Native.SDL_Keycode.SDLK_KP_XOR,
    /// <summary>The keypad power (^) key.</summary>
    KpPower = (uint)Native.SDL_Keycode.SDLK_KP_POWER,
    /// <summary>The keypad percent (%) key.</summary>
    KpPercent = (uint)Native.SDL_Keycode.SDLK_KP_PERCENT,
    /// <summary>The keypad less-than (&lt;) key.</summary>
    KpLess = (uint)Native.SDL_Keycode.SDLK_KP_LESS,
    /// <summary>The keypad greater-than (&gt;) key.</summary>
    KpGreater = (uint)Native.SDL_Keycode.SDLK_KP_GREATER,
    /// <summary>The keypad ampersand (&amp;) key.</summary>
    KpAmpersand = (uint)Native.SDL_Keycode.SDLK_KP_AMPERSAND,
    /// <summary>The keypad double ampersand (&amp;&amp;) key.</summary>
    KpDblAmpersand = (uint)Native.SDL_Keycode.SDLK_KP_DBLAMPERSAND,
    /// <summary>The keypad vertical bar (|) key.</summary>
    KpVerticalBar = (uint)Native.SDL_Keycode.SDLK_KP_VERTICALBAR,
    /// <summary>The keypad double vertical bar (||) key.</summary>
    KpDblVerticalBar = (uint)Native.SDL_Keycode.SDLK_KP_DBLVERTICALBAR,
    /// <summary>The keypad colon (:) key.</summary>
    KpColon = (uint)Native.SDL_Keycode.SDLK_KP_COLON,
    /// <summary>The keypad hash (#) key.</summary>
    KpHash = (uint)Native.SDL_Keycode.SDLK_KP_HASH,
    /// <summary>The keypad Space key.</summary>
    KpSpace = (uint)Native.SDL_Keycode.SDLK_KP_SPACE,
    /// <summary>The keypad at (@) key.</summary>
    KpAt = (uint)Native.SDL_Keycode.SDLK_KP_AT,
    /// <summary>The keypad exclaim (!) key.</summary>
    KpExclam = (uint)Native.SDL_Keycode.SDLK_KP_EXCLAM,
    /// <summary>The keypad Memory Store key.</summary>
    KpMemStore = (uint)Native.SDL_Keycode.SDLK_KP_MEMSTORE,
    /// <summary>The keypad Memory Recall key.</summary>
    KpMemRecall = (uint)Native.SDL_Keycode.SDLK_KP_MEMRECALL,
    /// <summary>The keypad Memory Clear key.</summary>
    KpMemClear = (uint)Native.SDL_Keycode.SDLK_KP_MEMCLEAR,
    /// <summary>The keypad Memory Add key.</summary>
    KpMemAdd = (uint)Native.SDL_Keycode.SDLK_KP_MEMADD,
    /// <summary>The keypad Memory Subtract key.</summary>
    KpMemSubtract = (uint)Native.SDL_Keycode.SDLK_KP_MEMSUBTRACT,
    /// <summary>The keypad Memory Multiply key.</summary>
    KpMemMultiply = (uint)Native.SDL_Keycode.SDLK_KP_MEMMULTIPLY,
    /// <summary>The keypad Memory Divide key.</summary>
    KpMemDivide = (uint)Native.SDL_Keycode.SDLK_KP_MEMDIVIDE,
    /// <summary>The keypad plus/minus (+/-) key.</summary>
    KpPlusMinus = (uint)Native.SDL_Keycode.SDLK_KP_PLUSMINUS,
    /// <summary>The keypad Clear key.</summary>
    KpClear = (uint)Native.SDL_Keycode.SDLK_KP_CLEAR,
    /// <summary>The keypad Clear Entry key.</summary>
    KpClearEntry = (uint)Native.SDL_Keycode.SDLK_KP_CLEARENTRY,
    /// <summary>The keypad Binary key.</summary>
    KpBinary = (uint)Native.SDL_Keycode.SDLK_KP_BINARY,
    /// <summary>The keypad Octal key.</summary>
    KpOctal = (uint)Native.SDL_Keycode.SDLK_KP_OCTAL,
    /// <summary>The keypad Decimal key.</summary>
    KpDecimal = (uint)Native.SDL_Keycode.SDLK_KP_DECIMAL,
    /// <summary>The keypad Hexadecimal key.</summary>
    KpHexadecimal = (uint)Native.SDL_Keycode.SDLK_KP_HEXADECIMAL,
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
    /// <summary>The Mode key (AltGr).</summary>
    Mode = (uint)Native.SDL_Keycode.SDLK_MODE,
    /// <summary>The Sleep key.</summary>
    Sleep = (uint)Native.SDL_Keycode.SDLK_SLEEP,
    /// <summary>The Wake key.</summary>
    Wake = (uint)Native.SDL_Keycode.SDLK_WAKE,
    /// <summary>The Channel Increment key.</summary>
    ChannelIncrement = (uint)Native.SDL_Keycode.SDLK_CHANNEL_INCREMENT,
    /// <summary>The Channel Decrement key.</summary>
    ChannelDecrement = (uint)Native.SDL_Keycode.SDLK_CHANNEL_DECREMENT,
    /// <summary>The Media Play key.</summary>
    MediaPlay = (uint)Native.SDL_Keycode.SDLK_MEDIA_PLAY,
    /// <summary>The Media Pause key.</summary>
    MediaPause = (uint)Native.SDL_Keycode.SDLK_MEDIA_PAUSE,
    /// <summary>The Media Record key.</summary>
    MediaRecord = (uint)Native.SDL_Keycode.SDLK_MEDIA_RECORD,
    /// <summary>The Media Fast Forward key.</summary>
    MediaFastForward = (uint)Native.SDL_Keycode.SDLK_MEDIA_FAST_FORWARD,
    /// <summary>The Media Rewind key.</summary>
    MediaRewind = (uint)Native.SDL_Keycode.SDLK_MEDIA_REWIND,
    /// <summary>The Media Next Track key.</summary>
    MediaNextTrack = (uint)Native.SDL_Keycode.SDLK_MEDIA_NEXT_TRACK,
    /// <summary>The Media Previous Track key.</summary>
    MediaPreviousTrack = (uint)Native.SDL_Keycode.SDLK_MEDIA_PREVIOUS_TRACK,
    /// <summary>The Media Stop key.</summary>
    MediaStop = (uint)Native.SDL_Keycode.SDLK_MEDIA_STOP,
    /// <summary>The Media Eject key.</summary>
    MediaEject = (uint)Native.SDL_Keycode.SDLK_MEDIA_EJECT,
    /// <summary>The Media Play/Pause key.</summary>
    MediaPlayPause = (uint)Native.SDL_Keycode.SDLK_MEDIA_PLAY_PAUSE,
    /// <summary>The Media Select key.</summary>
    MediaSelect = (uint)Native.SDL_Keycode.SDLK_MEDIA_SELECT,
    /// <summary>The AC New key.</summary>
    AcNew = (uint)Native.SDL_Keycode.SDLK_AC_NEW,
    /// <summary>The AC Open key.</summary>
    AcOpen = (uint)Native.SDL_Keycode.SDLK_AC_OPEN,
    /// <summary>The AC Close key.</summary>
    AcClose = (uint)Native.SDL_Keycode.SDLK_AC_CLOSE,
    /// <summary>The AC Exit key.</summary>
    AcExit = (uint)Native.SDL_Keycode.SDLK_AC_EXIT,
    /// <summary>The AC Save key.</summary>
    AcSave = (uint)Native.SDL_Keycode.SDLK_AC_SAVE,
    /// <summary>The AC Print key.</summary>
    AcPrint = (uint)Native.SDL_Keycode.SDLK_AC_PRINT,
    /// <summary>The AC Properties key.</summary>
    AcProperties = (uint)Native.SDL_Keycode.SDLK_AC_PROPERTIES,
    /// <summary>The AC Search key.</summary>
    AcSearch = (uint)Native.SDL_Keycode.SDLK_AC_SEARCH,
    /// <summary>The AC Home key.</summary>
    AcHome = (uint)Native.SDL_Keycode.SDLK_AC_HOME,
    /// <summary>The AC Back key.</summary>
    AcBack = (uint)Native.SDL_Keycode.SDLK_AC_BACK,
    /// <summary>The AC Forward key.</summary>
    AcForward = (uint)Native.SDL_Keycode.SDLK_AC_FORWARD,
    /// <summary>The AC Stop key.</summary>
    AcStop = (uint)Native.SDL_Keycode.SDLK_AC_STOP,
    /// <summary>The AC Refresh key.</summary>
    AcRefresh = (uint)Native.SDL_Keycode.SDLK_AC_REFRESH,
    /// <summary>The AC Bookmarks key.</summary>
    AcBookmarks = (uint)Native.SDL_Keycode.SDLK_AC_BOOKMARKS,
    /// <summary>The Soft Left key (mobile phones).</summary>
    SoftLeft = (uint)Native.SDL_Keycode.SDLK_SOFTLEFT,
    /// <summary>The Soft Right key (mobile phones).</summary>
    SoftRight = (uint)Native.SDL_Keycode.SDLK_SOFTRIGHT,
    /// <summary>The Call key (mobile phones).</summary>
    Call = (uint)Native.SDL_Keycode.SDLK_CALL,
    /// <summary>The End Call key (mobile phones).</summary>
    EndCall = (uint)Native.SDL_Keycode.SDLK_ENDCALL,
    /// <summary>The extended Left Tab key.</summary>
    LeftTab = (uint)Native.SDL_Keycode.SDLK_LEFT_TAB,
    /// <summary>The extended Level 5 Shift key.</summary>
    Level5Shift = (uint)Native.SDL_Keycode.SDLK_LEVEL5_SHIFT,
    /// <summary>The extended Multi-key Compose key.</summary>
    MultiKeyCompose = (uint)Native.SDL_Keycode.SDLK_MULTI_KEY_COMPOSE,
    /// <summary>The extended left Meta key.</summary>
    LMeta = (uint)Native.SDL_Keycode.SDLK_LMETA,
    /// <summary>The extended right Meta key.</summary>
    RMeta = (uint)Native.SDL_Keycode.SDLK_RMETA,
    /// <summary>The extended left Hyper key.</summary>
    LHyper = (uint)Native.SDL_Keycode.SDLK_LHYPER,
    /// <summary>The extended right Hyper key.</summary>
    RHyper = (uint)Native.SDL_Keycode.SDLK_RHYPER,
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
    /// <summary>The Level 5 Shift key is down.</summary>
    Level5 = (ushort)Native.SDL_Keymod.SDL_KMOD_LEVEL5,
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
