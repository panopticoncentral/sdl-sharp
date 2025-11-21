using static Sdl3Sharp.Native.Scancode;

namespace Sdl3Sharp;

/// <summary>
/// Represents a keyboard scancode - the physical representation of a key on the keyboard,
/// independent of language and keyboard mapping.
/// </summary>
/// <remarks>
/// Values are based on the USB usage page standard.
/// See: https://wiki.libsdl.org/SDL3/BestKeyboardPractices
/// </remarks>
public enum Scancode
{
    /// <summary>Unknown scancode.</summary>
    Unknown = SDL_Scancode.SDL_SCANCODE_UNKNOWN,

    // Letter keys

    /// <summary>The A key.</summary>
    A = SDL_Scancode.SDL_SCANCODE_A,
    /// <summary>The B key.</summary>
    B = SDL_Scancode.SDL_SCANCODE_B,
    /// <summary>The C key.</summary>
    C = SDL_Scancode.SDL_SCANCODE_C,
    /// <summary>The D key.</summary>
    D = SDL_Scancode.SDL_SCANCODE_D,
    /// <summary>The E key.</summary>
    E = SDL_Scancode.SDL_SCANCODE_E,
    /// <summary>The F key.</summary>
    F = SDL_Scancode.SDL_SCANCODE_F,
    /// <summary>The G key.</summary>
    G = SDL_Scancode.SDL_SCANCODE_G,
    /// <summary>The H key.</summary>
    H = SDL_Scancode.SDL_SCANCODE_H,
    /// <summary>The I key.</summary>
    I = SDL_Scancode.SDL_SCANCODE_I,
    /// <summary>The J key.</summary>
    J = SDL_Scancode.SDL_SCANCODE_J,
    /// <summary>The K key.</summary>
    K = SDL_Scancode.SDL_SCANCODE_K,
    /// <summary>The L key.</summary>
    L = SDL_Scancode.SDL_SCANCODE_L,
    /// <summary>The M key.</summary>
    M = SDL_Scancode.SDL_SCANCODE_M,
    /// <summary>The N key.</summary>
    N = SDL_Scancode.SDL_SCANCODE_N,
    /// <summary>The O key.</summary>
    O = SDL_Scancode.SDL_SCANCODE_O,
    /// <summary>The P key.</summary>
    P = SDL_Scancode.SDL_SCANCODE_P,
    /// <summary>The Q key.</summary>
    Q = SDL_Scancode.SDL_SCANCODE_Q,
    /// <summary>The R key.</summary>
    R = SDL_Scancode.SDL_SCANCODE_R,
    /// <summary>The S key.</summary>
    S = SDL_Scancode.SDL_SCANCODE_S,
    /// <summary>The T key.</summary>
    T = SDL_Scancode.SDL_SCANCODE_T,
    /// <summary>The U key.</summary>
    U = SDL_Scancode.SDL_SCANCODE_U,
    /// <summary>The V key.</summary>
    V = SDL_Scancode.SDL_SCANCODE_V,
    /// <summary>The W key.</summary>
    W = SDL_Scancode.SDL_SCANCODE_W,
    /// <summary>The X key.</summary>
    X = SDL_Scancode.SDL_SCANCODE_X,
    /// <summary>The Y key.</summary>
    Y = SDL_Scancode.SDL_SCANCODE_Y,
    /// <summary>The Z key.</summary>
    Z = SDL_Scancode.SDL_SCANCODE_Z,

    // Number keys

    /// <summary>The 1 key.</summary>
    Number1 = SDL_Scancode.SDL_SCANCODE_1,
    /// <summary>The 2 key.</summary>
    Number2 = SDL_Scancode.SDL_SCANCODE_2,
    /// <summary>The 3 key.</summary>
    Number3 = SDL_Scancode.SDL_SCANCODE_3,
    /// <summary>The 4 key.</summary>
    Number4 = SDL_Scancode.SDL_SCANCODE_4,
    /// <summary>The 5 key.</summary>
    Number5 = SDL_Scancode.SDL_SCANCODE_5,
    /// <summary>The 6 key.</summary>
    Number6 = SDL_Scancode.SDL_SCANCODE_6,
    /// <summary>The 7 key.</summary>
    Number7 = SDL_Scancode.SDL_SCANCODE_7,
    /// <summary>The 8 key.</summary>
    Number8 = SDL_Scancode.SDL_SCANCODE_8,
    /// <summary>The 9 key.</summary>
    Number9 = SDL_Scancode.SDL_SCANCODE_9,
    /// <summary>The 0 key.</summary>
    Number0 = SDL_Scancode.SDL_SCANCODE_0,

    // Control keys

    /// <summary>The Return/Enter key.</summary>
    Return = SDL_Scancode.SDL_SCANCODE_RETURN,
    /// <summary>The Escape key.</summary>
    Escape = SDL_Scancode.SDL_SCANCODE_ESCAPE,
    /// <summary>The Backspace key.</summary>
    Backspace = SDL_Scancode.SDL_SCANCODE_BACKSPACE,
    /// <summary>The Tab key.</summary>
    Tab = SDL_Scancode.SDL_SCANCODE_TAB,
    /// <summary>The Space key.</summary>
    Space = SDL_Scancode.SDL_SCANCODE_SPACE,

    // Punctuation and symbol keys

    /// <summary>The Minus/Hyphen key.</summary>
    Minus = SDL_Scancode.SDL_SCANCODE_MINUS,
    /// <summary>The Equals key.</summary>
    Equals = SDL_Scancode.SDL_SCANCODE_EQUALS,
    /// <summary>The Left Bracket key.</summary>
    LeftBracket = SDL_Scancode.SDL_SCANCODE_LEFTBRACKET,
    /// <summary>The Right Bracket key.</summary>
    RightBracket = SDL_Scancode.SDL_SCANCODE_RIGHTBRACKET,
    /// <summary>The Backslash key.</summary>
    Backslash = SDL_Scancode.SDL_SCANCODE_BACKSLASH,
    /// <summary>The Non-US Hash key (ISO keyboards).</summary>
    NonUsHash = SDL_Scancode.SDL_SCANCODE_NONUSHASH,
    /// <summary>The Semicolon key.</summary>
    Semicolon = SDL_Scancode.SDL_SCANCODE_SEMICOLON,
    /// <summary>The Apostrophe key.</summary>
    Apostrophe = SDL_Scancode.SDL_SCANCODE_APOSTROPHE,
    /// <summary>The Grave/Tilde key.</summary>
    Grave = SDL_Scancode.SDL_SCANCODE_GRAVE,
    /// <summary>The Comma key.</summary>
    Comma = SDL_Scancode.SDL_SCANCODE_COMMA,
    /// <summary>The Period key.</summary>
    Period = SDL_Scancode.SDL_SCANCODE_PERIOD,
    /// <summary>The Slash key.</summary>
    Slash = SDL_Scancode.SDL_SCANCODE_SLASH,

    /// <summary>The Caps Lock key.</summary>
    CapsLock = SDL_Scancode.SDL_SCANCODE_CAPSLOCK,

    // Function keys

    /// <summary>The F1 key.</summary>
    F1 = SDL_Scancode.SDL_SCANCODE_F1,
    /// <summary>The F2 key.</summary>
    F2 = SDL_Scancode.SDL_SCANCODE_F2,
    /// <summary>The F3 key.</summary>
    F3 = SDL_Scancode.SDL_SCANCODE_F3,
    /// <summary>The F4 key.</summary>
    F4 = SDL_Scancode.SDL_SCANCODE_F4,
    /// <summary>The F5 key.</summary>
    F5 = SDL_Scancode.SDL_SCANCODE_F5,
    /// <summary>The F6 key.</summary>
    F6 = SDL_Scancode.SDL_SCANCODE_F6,
    /// <summary>The F7 key.</summary>
    F7 = SDL_Scancode.SDL_SCANCODE_F7,
    /// <summary>The F8 key.</summary>
    F8 = SDL_Scancode.SDL_SCANCODE_F8,
    /// <summary>The F9 key.</summary>
    F9 = SDL_Scancode.SDL_SCANCODE_F9,
    /// <summary>The F10 key.</summary>
    F10 = SDL_Scancode.SDL_SCANCODE_F10,
    /// <summary>The F11 key.</summary>
    F11 = SDL_Scancode.SDL_SCANCODE_F11,
    /// <summary>The F12 key.</summary>
    F12 = SDL_Scancode.SDL_SCANCODE_F12,

    /// <summary>The Print Screen key.</summary>
    PrintScreen = SDL_Scancode.SDL_SCANCODE_PRINTSCREEN,
    /// <summary>The Scroll Lock key.</summary>
    ScrollLock = SDL_Scancode.SDL_SCANCODE_SCROLLLOCK,
    /// <summary>The Pause key.</summary>
    Pause = SDL_Scancode.SDL_SCANCODE_PAUSE,
    /// <summary>The Insert key.</summary>
    Insert = SDL_Scancode.SDL_SCANCODE_INSERT,
    /// <summary>The Home key.</summary>
    Home = SDL_Scancode.SDL_SCANCODE_HOME,
    /// <summary>The Page Up key.</summary>
    PageUp = SDL_Scancode.SDL_SCANCODE_PAGEUP,
    /// <summary>The Delete key.</summary>
    Delete = SDL_Scancode.SDL_SCANCODE_DELETE,
    /// <summary>The End key.</summary>
    End = SDL_Scancode.SDL_SCANCODE_END,
    /// <summary>The Page Down key.</summary>
    PageDown = SDL_Scancode.SDL_SCANCODE_PAGEDOWN,

    // Arrow keys

    /// <summary>The Right Arrow key.</summary>
    Right = SDL_Scancode.SDL_SCANCODE_RIGHT,
    /// <summary>The Left Arrow key.</summary>
    Left = SDL_Scancode.SDL_SCANCODE_LEFT,
    /// <summary>The Down Arrow key.</summary>
    Down = SDL_Scancode.SDL_SCANCODE_DOWN,
    /// <summary>The Up Arrow key.</summary>
    Up = SDL_Scancode.SDL_SCANCODE_UP,

    // Keypad keys

    /// <summary>The Num Lock key on PC; Clear on Mac.</summary>
    NumLockClear = SDL_Scancode.SDL_SCANCODE_NUMLOCKCLEAR,
    /// <summary>The Keypad Divide key.</summary>
    KeypadDivide = SDL_Scancode.SDL_SCANCODE_KP_DIVIDE,
    /// <summary>The Keypad Multiply key.</summary>
    KeypadMultiply = SDL_Scancode.SDL_SCANCODE_KP_MULTIPLY,
    /// <summary>The Keypad Minus key.</summary>
    KeypadMinus = SDL_Scancode.SDL_SCANCODE_KP_MINUS,
    /// <summary>The Keypad Plus key.</summary>
    KeypadPlus = SDL_Scancode.SDL_SCANCODE_KP_PLUS,
    /// <summary>The Keypad Enter key.</summary>
    KeypadEnter = SDL_Scancode.SDL_SCANCODE_KP_ENTER,
    /// <summary>The Keypad 1 key.</summary>
    Keypad1 = SDL_Scancode.SDL_SCANCODE_KP_1,
    /// <summary>The Keypad 2 key.</summary>
    Keypad2 = SDL_Scancode.SDL_SCANCODE_KP_2,
    /// <summary>The Keypad 3 key.</summary>
    Keypad3 = SDL_Scancode.SDL_SCANCODE_KP_3,
    /// <summary>The Keypad 4 key.</summary>
    Keypad4 = SDL_Scancode.SDL_SCANCODE_KP_4,
    /// <summary>The Keypad 5 key.</summary>
    Keypad5 = SDL_Scancode.SDL_SCANCODE_KP_5,
    /// <summary>The Keypad 6 key.</summary>
    Keypad6 = SDL_Scancode.SDL_SCANCODE_KP_6,
    /// <summary>The Keypad 7 key.</summary>
    Keypad7 = SDL_Scancode.SDL_SCANCODE_KP_7,
    /// <summary>The Keypad 8 key.</summary>
    Keypad8 = SDL_Scancode.SDL_SCANCODE_KP_8,
    /// <summary>The Keypad 9 key.</summary>
    Keypad9 = SDL_Scancode.SDL_SCANCODE_KP_9,
    /// <summary>The Keypad 0 key.</summary>
    Keypad0 = SDL_Scancode.SDL_SCANCODE_KP_0,
    /// <summary>The Keypad Period key.</summary>
    KeypadPeriod = SDL_Scancode.SDL_SCANCODE_KP_PERIOD,

    /// <summary>The Non-US Backslash key (ISO keyboards).</summary>
    NonUsBackslash = SDL_Scancode.SDL_SCANCODE_NONUSBACKSLASH,
    /// <summary>The Application/Context Menu key.</summary>
    Application = SDL_Scancode.SDL_SCANCODE_APPLICATION,
    /// <summary>The Power key.</summary>
    Power = SDL_Scancode.SDL_SCANCODE_POWER,
    /// <summary>The Keypad Equals key.</summary>
    KeypadEquals = SDL_Scancode.SDL_SCANCODE_KP_EQUALS,

    // Extended function keys

    /// <summary>The F13 key.</summary>
    F13 = SDL_Scancode.SDL_SCANCODE_F13,
    /// <summary>The F14 key.</summary>
    F14 = SDL_Scancode.SDL_SCANCODE_F14,
    /// <summary>The F15 key.</summary>
    F15 = SDL_Scancode.SDL_SCANCODE_F15,
    /// <summary>The F16 key.</summary>
    F16 = SDL_Scancode.SDL_SCANCODE_F16,
    /// <summary>The F17 key.</summary>
    F17 = SDL_Scancode.SDL_SCANCODE_F17,
    /// <summary>The F18 key.</summary>
    F18 = SDL_Scancode.SDL_SCANCODE_F18,
    /// <summary>The F19 key.</summary>
    F19 = SDL_Scancode.SDL_SCANCODE_F19,
    /// <summary>The F20 key.</summary>
    F20 = SDL_Scancode.SDL_SCANCODE_F20,
    /// <summary>The F21 key.</summary>
    F21 = SDL_Scancode.SDL_SCANCODE_F21,
    /// <summary>The F22 key.</summary>
    F22 = SDL_Scancode.SDL_SCANCODE_F22,
    /// <summary>The F23 key.</summary>
    F23 = SDL_Scancode.SDL_SCANCODE_F23,
    /// <summary>The F24 key.</summary>
    F24 = SDL_Scancode.SDL_SCANCODE_F24,

    // Application control keys

    /// <summary>The Execute key.</summary>
    Execute = SDL_Scancode.SDL_SCANCODE_EXECUTE,
    /// <summary>The Help key.</summary>
    Help = SDL_Scancode.SDL_SCANCODE_HELP,
    /// <summary>The Menu key.</summary>
    Menu = SDL_Scancode.SDL_SCANCODE_MENU,
    /// <summary>The Select key.</summary>
    Select = SDL_Scancode.SDL_SCANCODE_SELECT,
    /// <summary>The Stop key.</summary>
    Stop = SDL_Scancode.SDL_SCANCODE_STOP,
    /// <summary>The Again key (Redo/Repeat).</summary>
    Again = SDL_Scancode.SDL_SCANCODE_AGAIN,
    /// <summary>The Undo key.</summary>
    Undo = SDL_Scancode.SDL_SCANCODE_UNDO,
    /// <summary>The Cut key.</summary>
    Cut = SDL_Scancode.SDL_SCANCODE_CUT,
    /// <summary>The Copy key.</summary>
    Copy = SDL_Scancode.SDL_SCANCODE_COPY,
    /// <summary>The Paste key.</summary>
    Paste = SDL_Scancode.SDL_SCANCODE_PASTE,
    /// <summary>The Find key.</summary>
    Find = SDL_Scancode.SDL_SCANCODE_FIND,
    /// <summary>The Mute key.</summary>
    Mute = SDL_Scancode.SDL_SCANCODE_MUTE,
    /// <summary>The Volume Up key.</summary>
    VolumeUp = SDL_Scancode.SDL_SCANCODE_VOLUMEUP,
    /// <summary>The Volume Down key.</summary>
    VolumeDown = SDL_Scancode.SDL_SCANCODE_VOLUMEDOWN,

    /// <summary>The Keypad Comma key.</summary>
    KeypadComma = SDL_Scancode.SDL_SCANCODE_KP_COMMA,
    /// <summary>The Keypad Equals key (AS/400).</summary>
    KeypadEqualsAS400 = SDL_Scancode.SDL_SCANCODE_KP_EQUALSAS400,

    // International keys

    /// <summary>International key 1.</summary>
    International1 = SDL_Scancode.SDL_SCANCODE_INTERNATIONAL1,
    /// <summary>International key 2.</summary>
    International2 = SDL_Scancode.SDL_SCANCODE_INTERNATIONAL2,
    /// <summary>International key 3 (Yen).</summary>
    International3 = SDL_Scancode.SDL_SCANCODE_INTERNATIONAL3,
    /// <summary>International key 4.</summary>
    International4 = SDL_Scancode.SDL_SCANCODE_INTERNATIONAL4,
    /// <summary>International key 5.</summary>
    International5 = SDL_Scancode.SDL_SCANCODE_INTERNATIONAL5,
    /// <summary>International key 6.</summary>
    International6 = SDL_Scancode.SDL_SCANCODE_INTERNATIONAL6,
    /// <summary>International key 7.</summary>
    International7 = SDL_Scancode.SDL_SCANCODE_INTERNATIONAL7,
    /// <summary>International key 8.</summary>
    International8 = SDL_Scancode.SDL_SCANCODE_INTERNATIONAL8,
    /// <summary>International key 9.</summary>
    International9 = SDL_Scancode.SDL_SCANCODE_INTERNATIONAL9,

    // Language keys

    /// <summary>Language key 1 (Hangul/English toggle).</summary>
    Lang1 = SDL_Scancode.SDL_SCANCODE_LANG1,
    /// <summary>Language key 2 (Hanja conversion).</summary>
    Lang2 = SDL_Scancode.SDL_SCANCODE_LANG2,
    /// <summary>Language key 3 (Katakana).</summary>
    Lang3 = SDL_Scancode.SDL_SCANCODE_LANG3,
    /// <summary>Language key 4 (Hiragana).</summary>
    Lang4 = SDL_Scancode.SDL_SCANCODE_LANG4,
    /// <summary>Language key 5 (Zenkaku/Hankaku).</summary>
    Lang5 = SDL_Scancode.SDL_SCANCODE_LANG5,
    /// <summary>Language key 6 (reserved).</summary>
    Lang6 = SDL_Scancode.SDL_SCANCODE_LANG6,
    /// <summary>Language key 7 (reserved).</summary>
    Lang7 = SDL_Scancode.SDL_SCANCODE_LANG7,
    /// <summary>Language key 8 (reserved).</summary>
    Lang8 = SDL_Scancode.SDL_SCANCODE_LANG8,
    /// <summary>Language key 9 (reserved).</summary>
    Lang9 = SDL_Scancode.SDL_SCANCODE_LANG9,

    // Additional keys

    /// <summary>The Alt Erase key.</summary>
    AltErase = SDL_Scancode.SDL_SCANCODE_ALTERASE,
    /// <summary>The SysReq key.</summary>
    SysReq = SDL_Scancode.SDL_SCANCODE_SYSREQ,
    /// <summary>The Cancel key.</summary>
    Cancel = SDL_Scancode.SDL_SCANCODE_CANCEL,
    /// <summary>The Clear key.</summary>
    Clear = SDL_Scancode.SDL_SCANCODE_CLEAR,
    /// <summary>The Prior key.</summary>
    Prior = SDL_Scancode.SDL_SCANCODE_PRIOR,
    /// <summary>The Return2 key.</summary>
    Return2 = SDL_Scancode.SDL_SCANCODE_RETURN2,
    /// <summary>The Separator key.</summary>
    Separator = SDL_Scancode.SDL_SCANCODE_SEPARATOR,
    /// <summary>The Out key.</summary>
    Out = SDL_Scancode.SDL_SCANCODE_OUT,
    /// <summary>The Oper key.</summary>
    Oper = SDL_Scancode.SDL_SCANCODE_OPER,
    /// <summary>The Clear Again key.</summary>
    ClearAgain = SDL_Scancode.SDL_SCANCODE_CLEARAGAIN,
    /// <summary>The CrSel key.</summary>
    CrSel = SDL_Scancode.SDL_SCANCODE_CRSEL,
    /// <summary>The ExSel key.</summary>
    ExSel = SDL_Scancode.SDL_SCANCODE_EXSEL,

    // Extended keypad keys

    /// <summary>The Keypad 00 key.</summary>
    Keypad00 = SDL_Scancode.SDL_SCANCODE_KP_00,
    /// <summary>The Keypad 000 key.</summary>
    Keypad000 = SDL_Scancode.SDL_SCANCODE_KP_000,
    /// <summary>The Thousands Separator key.</summary>
    ThousandsSeparator = SDL_Scancode.SDL_SCANCODE_THOUSANDSSEPARATOR,
    /// <summary>The Decimal Separator key.</summary>
    DecimalSeparator = SDL_Scancode.SDL_SCANCODE_DECIMALSEPARATOR,
    /// <summary>The Currency Unit key.</summary>
    CurrencyUnit = SDL_Scancode.SDL_SCANCODE_CURRENCYUNIT,
    /// <summary>The Currency Subunit key.</summary>
    CurrencySubunit = SDL_Scancode.SDL_SCANCODE_CURRENCYSUBUNIT,
    /// <summary>The Keypad Left Parenthesis key.</summary>
    KeypadLeftParen = SDL_Scancode.SDL_SCANCODE_KP_LEFTPAREN,
    /// <summary>The Keypad Right Parenthesis key.</summary>
    KeypadRightParen = SDL_Scancode.SDL_SCANCODE_KP_RIGHTPAREN,
    /// <summary>The Keypad Left Brace key.</summary>
    KeypadLeftBrace = SDL_Scancode.SDL_SCANCODE_KP_LEFTBRACE,
    /// <summary>The Keypad Right Brace key.</summary>
    KeypadRightBrace = SDL_Scancode.SDL_SCANCODE_KP_RIGHTBRACE,
    /// <summary>The Keypad Tab key.</summary>
    KeypadTab = SDL_Scancode.SDL_SCANCODE_KP_TAB,
    /// <summary>The Keypad Backspace key.</summary>
    KeypadBackspace = SDL_Scancode.SDL_SCANCODE_KP_BACKSPACE,
    /// <summary>The Keypad A key.</summary>
    KeypadA = SDL_Scancode.SDL_SCANCODE_KP_A,
    /// <summary>The Keypad B key.</summary>
    KeypadB = SDL_Scancode.SDL_SCANCODE_KP_B,
    /// <summary>The Keypad C key.</summary>
    KeypadC = SDL_Scancode.SDL_SCANCODE_KP_C,
    /// <summary>The Keypad D key.</summary>
    KeypadD = SDL_Scancode.SDL_SCANCODE_KP_D,
    /// <summary>The Keypad E key.</summary>
    KeypadE = SDL_Scancode.SDL_SCANCODE_KP_E,
    /// <summary>The Keypad F key.</summary>
    KeypadF = SDL_Scancode.SDL_SCANCODE_KP_F,
    /// <summary>The Keypad XOR key.</summary>
    KeypadXor = SDL_Scancode.SDL_SCANCODE_KP_XOR,
    /// <summary>The Keypad Power key.</summary>
    KeypadPower = SDL_Scancode.SDL_SCANCODE_KP_POWER,
    /// <summary>The Keypad Percent key.</summary>
    KeypadPercent = SDL_Scancode.SDL_SCANCODE_KP_PERCENT,
    /// <summary>The Keypad Less Than key.</summary>
    KeypadLess = SDL_Scancode.SDL_SCANCODE_KP_LESS,
    /// <summary>The Keypad Greater Than key.</summary>
    KeypadGreater = SDL_Scancode.SDL_SCANCODE_KP_GREATER,
    /// <summary>The Keypad Ampersand key.</summary>
    KeypadAmpersand = SDL_Scancode.SDL_SCANCODE_KP_AMPERSAND,
    /// <summary>The Keypad Double Ampersand key.</summary>
    KeypadDoubleAmpersand = SDL_Scancode.SDL_SCANCODE_KP_DBLAMPERSAND,
    /// <summary>The Keypad Vertical Bar key.</summary>
    KeypadVerticalBar = SDL_Scancode.SDL_SCANCODE_KP_VERTICALBAR,
    /// <summary>The Keypad Double Vertical Bar key.</summary>
    KeypadDoubleVerticalBar = SDL_Scancode.SDL_SCANCODE_KP_DBLVERTICALBAR,
    /// <summary>The Keypad Colon key.</summary>
    KeypadColon = SDL_Scancode.SDL_SCANCODE_KP_COLON,
    /// <summary>The Keypad Hash key.</summary>
    KeypadHash = SDL_Scancode.SDL_SCANCODE_KP_HASH,
    /// <summary>The Keypad Space key.</summary>
    KeypadSpace = SDL_Scancode.SDL_SCANCODE_KP_SPACE,
    /// <summary>The Keypad At key.</summary>
    KeypadAt = SDL_Scancode.SDL_SCANCODE_KP_AT,
    /// <summary>The Keypad Exclamation key.</summary>
    KeypadExclam = SDL_Scancode.SDL_SCANCODE_KP_EXCLAM,
    /// <summary>The Keypad Memory Store key.</summary>
    KeypadMemStore = SDL_Scancode.SDL_SCANCODE_KP_MEMSTORE,
    /// <summary>The Keypad Memory Recall key.</summary>
    KeypadMemRecall = SDL_Scancode.SDL_SCANCODE_KP_MEMRECALL,
    /// <summary>The Keypad Memory Clear key.</summary>
    KeypadMemClear = SDL_Scancode.SDL_SCANCODE_KP_MEMCLEAR,
    /// <summary>The Keypad Memory Add key.</summary>
    KeypadMemAdd = SDL_Scancode.SDL_SCANCODE_KP_MEMADD,
    /// <summary>The Keypad Memory Subtract key.</summary>
    KeypadMemSubtract = SDL_Scancode.SDL_SCANCODE_KP_MEMSUBTRACT,
    /// <summary>The Keypad Memory Multiply key.</summary>
    KeypadMemMultiply = SDL_Scancode.SDL_SCANCODE_KP_MEMMULTIPLY,
    /// <summary>The Keypad Memory Divide key.</summary>
    KeypadMemDivide = SDL_Scancode.SDL_SCANCODE_KP_MEMDIVIDE,
    /// <summary>The Keypad Plus/Minus key.</summary>
    KeypadPlusMinus = SDL_Scancode.SDL_SCANCODE_KP_PLUSMINUS,
    /// <summary>The Keypad Clear key.</summary>
    KeypadClear = SDL_Scancode.SDL_SCANCODE_KP_CLEAR,
    /// <summary>The Keypad Clear Entry key.</summary>
    KeypadClearEntry = SDL_Scancode.SDL_SCANCODE_KP_CLEARENTRY,
    /// <summary>The Keypad Binary key.</summary>
    KeypadBinary = SDL_Scancode.SDL_SCANCODE_KP_BINARY,
    /// <summary>The Keypad Octal key.</summary>
    KeypadOctal = SDL_Scancode.SDL_SCANCODE_KP_OCTAL,
    /// <summary>The Keypad Decimal key.</summary>
    KeypadDecimal = SDL_Scancode.SDL_SCANCODE_KP_DECIMAL,
    /// <summary>The Keypad Hexadecimal key.</summary>
    KeypadHexadecimal = SDL_Scancode.SDL_SCANCODE_KP_HEXADECIMAL,

    // Modifier keys

    /// <summary>The Left Control key.</summary>
    LeftCtrl = SDL_Scancode.SDL_SCANCODE_LCTRL,
    /// <summary>The Left Shift key.</summary>
    LeftShift = SDL_Scancode.SDL_SCANCODE_LSHIFT,
    /// <summary>The Left Alt key (Option on Mac).</summary>
    LeftAlt = SDL_Scancode.SDL_SCANCODE_LALT,
    /// <summary>The Left GUI key (Windows, Command, Meta).</summary>
    LeftGui = SDL_Scancode.SDL_SCANCODE_LGUI,
    /// <summary>The Right Control key.</summary>
    RightCtrl = SDL_Scancode.SDL_SCANCODE_RCTRL,
    /// <summary>The Right Shift key.</summary>
    RightShift = SDL_Scancode.SDL_SCANCODE_RSHIFT,
    /// <summary>The Right Alt key (Alt Gr, Option on Mac).</summary>
    RightAlt = SDL_Scancode.SDL_SCANCODE_RALT,
    /// <summary>The Right GUI key (Windows, Command, Meta).</summary>
    RightGui = SDL_Scancode.SDL_SCANCODE_RGUI,

    /// <summary>The Mode key.</summary>
    Mode = SDL_Scancode.SDL_SCANCODE_MODE,

    // Media keys (USB consumer page)

    /// <summary>The Sleep key.</summary>
    Sleep = SDL_Scancode.SDL_SCANCODE_SLEEP,
    /// <summary>The Wake key.</summary>
    Wake = SDL_Scancode.SDL_SCANCODE_WAKE,

    /// <summary>The Channel Increment key.</summary>
    ChannelIncrement = SDL_Scancode.SDL_SCANCODE_CHANNEL_INCREMENT,
    /// <summary>The Channel Decrement key.</summary>
    ChannelDecrement = SDL_Scancode.SDL_SCANCODE_CHANNEL_DECREMENT,

    /// <summary>The Media Play key.</summary>
    MediaPlay = SDL_Scancode.SDL_SCANCODE_MEDIA_PLAY,
    /// <summary>The Media Pause key.</summary>
    MediaPause = SDL_Scancode.SDL_SCANCODE_MEDIA_PAUSE,
    /// <summary>The Media Record key.</summary>
    MediaRecord = SDL_Scancode.SDL_SCANCODE_MEDIA_RECORD,
    /// <summary>The Media Fast Forward key.</summary>
    MediaFastForward = SDL_Scancode.SDL_SCANCODE_MEDIA_FAST_FORWARD,
    /// <summary>The Media Rewind key.</summary>
    MediaRewind = SDL_Scancode.SDL_SCANCODE_MEDIA_REWIND,
    /// <summary>The Media Next Track key.</summary>
    MediaNextTrack = SDL_Scancode.SDL_SCANCODE_MEDIA_NEXT_TRACK,
    /// <summary>The Media Previous Track key.</summary>
    MediaPreviousTrack = SDL_Scancode.SDL_SCANCODE_MEDIA_PREVIOUS_TRACK,
    /// <summary>The Media Stop key.</summary>
    MediaStop = SDL_Scancode.SDL_SCANCODE_MEDIA_STOP,
    /// <summary>The Media Eject key.</summary>
    MediaEject = SDL_Scancode.SDL_SCANCODE_MEDIA_EJECT,
    /// <summary>The Media Play/Pause key.</summary>
    MediaPlayPause = SDL_Scancode.SDL_SCANCODE_MEDIA_PLAY_PAUSE,
    /// <summary>The Media Select key.</summary>
    MediaSelect = SDL_Scancode.SDL_SCANCODE_MEDIA_SELECT,

    // Application control keys

    /// <summary>The AC New key.</summary>
    AcNew = SDL_Scancode.SDL_SCANCODE_AC_NEW,
    /// <summary>The AC Open key.</summary>
    AcOpen = SDL_Scancode.SDL_SCANCODE_AC_OPEN,
    /// <summary>The AC Close key.</summary>
    AcClose = SDL_Scancode.SDL_SCANCODE_AC_CLOSE,
    /// <summary>The AC Exit key.</summary>
    AcExit = SDL_Scancode.SDL_SCANCODE_AC_EXIT,
    /// <summary>The AC Save key.</summary>
    AcSave = SDL_Scancode.SDL_SCANCODE_AC_SAVE,
    /// <summary>The AC Print key.</summary>
    AcPrint = SDL_Scancode.SDL_SCANCODE_AC_PRINT,
    /// <summary>The AC Properties key.</summary>
    AcProperties = SDL_Scancode.SDL_SCANCODE_AC_PROPERTIES,

    /// <summary>The AC Search key.</summary>
    AcSearch = SDL_Scancode.SDL_SCANCODE_AC_SEARCH,
    /// <summary>The AC Home key.</summary>
    AcHome = SDL_Scancode.SDL_SCANCODE_AC_HOME,
    /// <summary>The AC Back key.</summary>
    AcBack = SDL_Scancode.SDL_SCANCODE_AC_BACK,
    /// <summary>The AC Forward key.</summary>
    AcForward = SDL_Scancode.SDL_SCANCODE_AC_FORWARD,
    /// <summary>The AC Stop key.</summary>
    AcStop = SDL_Scancode.SDL_SCANCODE_AC_STOP,
    /// <summary>The AC Refresh key.</summary>
    AcRefresh = SDL_Scancode.SDL_SCANCODE_AC_REFRESH,
    /// <summary>The AC Bookmarks key.</summary>
    AcBookmarks = SDL_Scancode.SDL_SCANCODE_AC_BOOKMARKS,

    // Mobile keys

    /// <summary>The Soft Left key (mobile phones).</summary>
    SoftLeft = SDL_Scancode.SDL_SCANCODE_SOFTLEFT,
    /// <summary>The Soft Right key (mobile phones).</summary>
    SoftRight = SDL_Scancode.SDL_SCANCODE_SOFTRIGHT,
    /// <summary>The Call key (mobile phones).</summary>
    Call = SDL_Scancode.SDL_SCANCODE_CALL,
    /// <summary>The End Call key (mobile phones).</summary>
    EndCall = SDL_Scancode.SDL_SCANCODE_ENDCALL,

    /// <summary>Reserved for dynamic keycodes (400-500).</summary>
    Reserved = SDL_Scancode.SDL_SCANCODE_RESERVED,

    /// <summary>Not a key, marks the number of scancodes for array bounds.</summary>
    Count = SDL_Scancode.SDL_SCANCODE_COUNT
}
