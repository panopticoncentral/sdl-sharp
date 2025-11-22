using static Sdl3Sharp.Native.Keycode;

namespace Sdl3Sharp.Input;

/// <summary>
/// Represents a keyboard keycode - the virtual key representation using the current keyboard layout.
/// </summary>
/// <remarks>
/// Values include Unicode values representing the unmodified character that would be generated
/// by pressing the key, or named constants for keys that do not generate characters.
/// See: https://wiki.libsdl.org/SDL3/BestKeyboardPractices
/// </remarks>
public enum Keycode : uint
{
    /// <summary>Unknown key.</summary>
    Unknown = SDL_Keycode.SDLK_UNKNOWN,

    // Character keys

    /// <summary>Backspace key.</summary>
    Backspace = SDL_Keycode.SDLK_BACKSPACE,
    /// <summary>Tab key.</summary>
    Tab = SDL_Keycode.SDLK_TAB,
    /// <summary>Return/Enter key.</summary>
    Return = SDL_Keycode.SDLK_RETURN,
    /// <summary>Escape key.</summary>
    Escape = SDL_Keycode.SDLK_ESCAPE,
    /// <summary>Space key.</summary>
    Space = SDL_Keycode.SDLK_SPACE,
    /// <summary>Exclamation mark key.</summary>
    Exclaim = SDL_Keycode.SDLK_EXCLAIM,
    /// <summary>Double apostrophe/quote key.</summary>
    DoubleApostrophe = SDL_Keycode.SDLK_DBLAPOSTROPHE,
    /// <summary>Hash key.</summary>
    Hash = SDL_Keycode.SDLK_HASH,
    /// <summary>Dollar sign key.</summary>
    Dollar = SDL_Keycode.SDLK_DOLLAR,
    /// <summary>Percent key.</summary>
    Percent = SDL_Keycode.SDLK_PERCENT,
    /// <summary>Ampersand key.</summary>
    Ampersand = SDL_Keycode.SDLK_AMPERSAND,
    /// <summary>Apostrophe key.</summary>
    Apostrophe = SDL_Keycode.SDLK_APOSTROPHE,
    /// <summary>Left parenthesis key.</summary>
    LeftParen = SDL_Keycode.SDLK_LEFTPAREN,
    /// <summary>Right parenthesis key.</summary>
    RightParen = SDL_Keycode.SDLK_RIGHTPAREN,
    /// <summary>Asterisk key.</summary>
    Asterisk = SDL_Keycode.SDLK_ASTERISK,
    /// <summary>Plus key.</summary>
    Plus = SDL_Keycode.SDLK_PLUS,
    /// <summary>Comma key.</summary>
    Comma = SDL_Keycode.SDLK_COMMA,
    /// <summary>Minus key.</summary>
    Minus = SDL_Keycode.SDLK_MINUS,
    /// <summary>Period key.</summary>
    Period = SDL_Keycode.SDLK_PERIOD,
    /// <summary>Forward slash key.</summary>
    Slash = SDL_Keycode.SDLK_SLASH,

    // Number keys

    /// <summary>The 0 key.</summary>
    Number0 = SDL_Keycode.SDLK_0,
    /// <summary>The 1 key.</summary>
    Number1 = SDL_Keycode.SDLK_1,
    /// <summary>The 2 key.</summary>
    Number2 = SDL_Keycode.SDLK_2,
    /// <summary>The 3 key.</summary>
    Number3 = SDL_Keycode.SDLK_3,
    /// <summary>The 4 key.</summary>
    Number4 = SDL_Keycode.SDLK_4,
    /// <summary>The 5 key.</summary>
    Number5 = SDL_Keycode.SDLK_5,
    /// <summary>The 6 key.</summary>
    Number6 = SDL_Keycode.SDLK_6,
    /// <summary>The 7 key.</summary>
    Number7 = SDL_Keycode.SDLK_7,
    /// <summary>The 8 key.</summary>
    Number8 = SDL_Keycode.SDLK_8,
    /// <summary>The 9 key.</summary>
    Number9 = SDL_Keycode.SDLK_9,

    // Punctuation keys

    /// <summary>Colon key.</summary>
    Colon = SDL_Keycode.SDLK_COLON,
    /// <summary>Semicolon key.</summary>
    Semicolon = SDL_Keycode.SDLK_SEMICOLON,
    /// <summary>Less than key.</summary>
    Less = SDL_Keycode.SDLK_LESS,
    /// <summary>Equals key.</summary>
    Equals = SDL_Keycode.SDLK_EQUALS,
    /// <summary>Greater than key.</summary>
    Greater = SDL_Keycode.SDLK_GREATER,
    /// <summary>Question mark key.</summary>
    Question = SDL_Keycode.SDLK_QUESTION,
    /// <summary>At sign key.</summary>
    At = SDL_Keycode.SDLK_AT,

    // Bracket keys

    /// <summary>Left bracket key.</summary>
    LeftBracket = SDL_Keycode.SDLK_LEFTBRACKET,
    /// <summary>Backslash key.</summary>
    Backslash = SDL_Keycode.SDLK_BACKSLASH,
    /// <summary>Right bracket key.</summary>
    RightBracket = SDL_Keycode.SDLK_RIGHTBRACKET,
    /// <summary>Caret key.</summary>
    Caret = SDL_Keycode.SDLK_CARET,
    /// <summary>Underscore key.</summary>
    Underscore = SDL_Keycode.SDLK_UNDERSCORE,
    /// <summary>Grave accent/backtick key.</summary>
    Grave = SDL_Keycode.SDLK_GRAVE,

    // Letter keys

    /// <summary>The A key.</summary>
    A = SDL_Keycode.SDLK_A,
    /// <summary>The B key.</summary>
    B = SDL_Keycode.SDLK_B,
    /// <summary>The C key.</summary>
    C = SDL_Keycode.SDLK_C,
    /// <summary>The D key.</summary>
    D = SDL_Keycode.SDLK_D,
    /// <summary>The E key.</summary>
    E = SDL_Keycode.SDLK_E,
    /// <summary>The F key.</summary>
    F = SDL_Keycode.SDLK_F,
    /// <summary>The G key.</summary>
    G = SDL_Keycode.SDLK_G,
    /// <summary>The H key.</summary>
    H = SDL_Keycode.SDLK_H,
    /// <summary>The I key.</summary>
    I = SDL_Keycode.SDLK_I,
    /// <summary>The J key.</summary>
    J = SDL_Keycode.SDLK_J,
    /// <summary>The K key.</summary>
    K = SDL_Keycode.SDLK_K,
    /// <summary>The L key.</summary>
    L = SDL_Keycode.SDLK_L,
    /// <summary>The M key.</summary>
    M = SDL_Keycode.SDLK_M,
    /// <summary>The N key.</summary>
    N = SDL_Keycode.SDLK_N,
    /// <summary>The O key.</summary>
    O = SDL_Keycode.SDLK_O,
    /// <summary>The P key.</summary>
    P = SDL_Keycode.SDLK_P,
    /// <summary>The Q key.</summary>
    Q = SDL_Keycode.SDLK_Q,
    /// <summary>The R key.</summary>
    R = SDL_Keycode.SDLK_R,
    /// <summary>The S key.</summary>
    S = SDL_Keycode.SDLK_S,
    /// <summary>The T key.</summary>
    T = SDL_Keycode.SDLK_T,
    /// <summary>The U key.</summary>
    U = SDL_Keycode.SDLK_U,
    /// <summary>The V key.</summary>
    V = SDL_Keycode.SDLK_V,
    /// <summary>The W key.</summary>
    W = SDL_Keycode.SDLK_W,
    /// <summary>The X key.</summary>
    X = SDL_Keycode.SDLK_X,
    /// <summary>The Y key.</summary>
    Y = SDL_Keycode.SDLK_Y,
    /// <summary>The Z key.</summary>
    Z = SDL_Keycode.SDLK_Z,

    // Brace and special character keys

    /// <summary>Left brace key.</summary>
    LeftBrace = SDL_Keycode.SDLK_LEFTBRACE,
    /// <summary>Pipe key.</summary>
    Pipe = SDL_Keycode.SDLK_PIPE,
    /// <summary>Right brace key.</summary>
    RightBrace = SDL_Keycode.SDLK_RIGHTBRACE,
    /// <summary>Tilde key.</summary>
    Tilde = SDL_Keycode.SDLK_TILDE,
    /// <summary>Delete key.</summary>
    Delete = SDL_Keycode.SDLK_DELETE,
    /// <summary>Plus-minus key.</summary>
    PlusMinus = SDL_Keycode.SDLK_PLUSMINUS,

    // Extended keys

    /// <summary>Left Tab key (extended).</summary>
    LeftTab = SDL_Keycode.SDLK_LEFT_TAB,
    /// <summary>Level 5 Shift key (extended).</summary>
    Level5Shift = SDL_Keycode.SDLK_LEVEL5_SHIFT,
    /// <summary>Multi-key Compose key (extended).</summary>
    MultiKeyCompose = SDL_Keycode.SDLK_MULTI_KEY_COMPOSE,
    /// <summary>Left Meta key (extended).</summary>
    LeftMeta = SDL_Keycode.SDLK_LMETA,
    /// <summary>Right Meta key (extended).</summary>
    RightMeta = SDL_Keycode.SDLK_RMETA,
    /// <summary>Left Hyper key (extended).</summary>
    LeftHyper = SDL_Keycode.SDLK_LHYPER,
    /// <summary>Right Hyper key (extended).</summary>
    RightHyper = SDL_Keycode.SDLK_RHYPER,

    // Function and navigation keys

    /// <summary>Caps Lock key.</summary>
    CapsLock = SDL_Keycode.SDLK_CAPSLOCK,
    /// <summary>F1 key.</summary>
    F1 = SDL_Keycode.SDLK_F1,
    /// <summary>F2 key.</summary>
    F2 = SDL_Keycode.SDLK_F2,
    /// <summary>F3 key.</summary>
    F3 = SDL_Keycode.SDLK_F3,
    /// <summary>F4 key.</summary>
    F4 = SDL_Keycode.SDLK_F4,
    /// <summary>F5 key.</summary>
    F5 = SDL_Keycode.SDLK_F5,
    /// <summary>F6 key.</summary>
    F6 = SDL_Keycode.SDLK_F6,
    /// <summary>F7 key.</summary>
    F7 = SDL_Keycode.SDLK_F7,
    /// <summary>F8 key.</summary>
    F8 = SDL_Keycode.SDLK_F8,
    /// <summary>F9 key.</summary>
    F9 = SDL_Keycode.SDLK_F9,
    /// <summary>F10 key.</summary>
    F10 = SDL_Keycode.SDLK_F10,
    /// <summary>F11 key.</summary>
    F11 = SDL_Keycode.SDLK_F11,
    /// <summary>F12 key.</summary>
    F12 = SDL_Keycode.SDLK_F12,

    /// <summary>Print Screen key.</summary>
    PrintScreen = SDL_Keycode.SDLK_PRINTSCREEN,
    /// <summary>Scroll Lock key.</summary>
    ScrollLock = SDL_Keycode.SDLK_SCROLLLOCK,
    /// <summary>Pause key.</summary>
    Pause = SDL_Keycode.SDLK_PAUSE,
    /// <summary>Insert key.</summary>
    Insert = SDL_Keycode.SDLK_INSERT,
    /// <summary>Home key.</summary>
    Home = SDL_Keycode.SDLK_HOME,
    /// <summary>Page Up key.</summary>
    PageUp = SDL_Keycode.SDLK_PAGEUP,
    /// <summary>End key.</summary>
    End = SDL_Keycode.SDLK_END,
    /// <summary>Page Down key.</summary>
    PageDown = SDL_Keycode.SDLK_PAGEDOWN,
    /// <summary>Right arrow key.</summary>
    Right = SDL_Keycode.SDLK_RIGHT,
    /// <summary>Left arrow key.</summary>
    Left = SDL_Keycode.SDLK_LEFT,
    /// <summary>Down arrow key.</summary>
    Down = SDL_Keycode.SDLK_DOWN,
    /// <summary>Up arrow key.</summary>
    Up = SDL_Keycode.SDLK_UP,

    // Keypad keys

    /// <summary>Num Lock / Clear key.</summary>
    NumLockClear = SDL_Keycode.SDLK_NUMLOCKCLEAR,
    /// <summary>Keypad divide key.</summary>
    KeypadDivide = SDL_Keycode.SDLK_KP_DIVIDE,
    /// <summary>Keypad multiply key.</summary>
    KeypadMultiply = SDL_Keycode.SDLK_KP_MULTIPLY,
    /// <summary>Keypad minus key.</summary>
    KeypadMinus = SDL_Keycode.SDLK_KP_MINUS,
    /// <summary>Keypad plus key.</summary>
    KeypadPlus = SDL_Keycode.SDLK_KP_PLUS,
    /// <summary>Keypad enter key.</summary>
    KeypadEnter = SDL_Keycode.SDLK_KP_ENTER,
    /// <summary>Keypad 1 key.</summary>
    Keypad1 = SDL_Keycode.SDLK_KP_1,
    /// <summary>Keypad 2 key.</summary>
    Keypad2 = SDL_Keycode.SDLK_KP_2,
    /// <summary>Keypad 3 key.</summary>
    Keypad3 = SDL_Keycode.SDLK_KP_3,
    /// <summary>Keypad 4 key.</summary>
    Keypad4 = SDL_Keycode.SDLK_KP_4,
    /// <summary>Keypad 5 key.</summary>
    Keypad5 = SDL_Keycode.SDLK_KP_5,
    /// <summary>Keypad 6 key.</summary>
    Keypad6 = SDL_Keycode.SDLK_KP_6,
    /// <summary>Keypad 7 key.</summary>
    Keypad7 = SDL_Keycode.SDLK_KP_7,
    /// <summary>Keypad 8 key.</summary>
    Keypad8 = SDL_Keycode.SDLK_KP_8,
    /// <summary>Keypad 9 key.</summary>
    Keypad9 = SDL_Keycode.SDLK_KP_9,
    /// <summary>Keypad 0 key.</summary>
    Keypad0 = SDL_Keycode.SDLK_KP_0,
    /// <summary>Keypad period key.</summary>
    KeypadPeriod = SDL_Keycode.SDLK_KP_PERIOD,

    // Application and power keys

    /// <summary>Application/Menu key.</summary>
    Application = SDL_Keycode.SDLK_APPLICATION,
    /// <summary>Power key.</summary>
    Power = SDL_Keycode.SDLK_POWER,
    /// <summary>Keypad equals key.</summary>
    KeypadEquals = SDL_Keycode.SDLK_KP_EQUALS,

    // Extended function keys

    /// <summary>F13 key.</summary>
    F13 = SDL_Keycode.SDLK_F13,
    /// <summary>F14 key.</summary>
    F14 = SDL_Keycode.SDLK_F14,
    /// <summary>F15 key.</summary>
    F15 = SDL_Keycode.SDLK_F15,
    /// <summary>F16 key.</summary>
    F16 = SDL_Keycode.SDLK_F16,
    /// <summary>F17 key.</summary>
    F17 = SDL_Keycode.SDLK_F17,
    /// <summary>F18 key.</summary>
    F18 = SDL_Keycode.SDLK_F18,
    /// <summary>F19 key.</summary>
    F19 = SDL_Keycode.SDLK_F19,
    /// <summary>F20 key.</summary>
    F20 = SDL_Keycode.SDLK_F20,
    /// <summary>F21 key.</summary>
    F21 = SDL_Keycode.SDLK_F21,
    /// <summary>F22 key.</summary>
    F22 = SDL_Keycode.SDLK_F22,
    /// <summary>F23 key.</summary>
    F23 = SDL_Keycode.SDLK_F23,
    /// <summary>F24 key.</summary>
    F24 = SDL_Keycode.SDLK_F24,

    // System and editing keys

    /// <summary>Execute key.</summary>
    Execute = SDL_Keycode.SDLK_EXECUTE,
    /// <summary>Help key.</summary>
    Help = SDL_Keycode.SDLK_HELP,
    /// <summary>Menu key.</summary>
    Menu = SDL_Keycode.SDLK_MENU,
    /// <summary>Select key.</summary>
    Select = SDL_Keycode.SDLK_SELECT,
    /// <summary>Stop key.</summary>
    Stop = SDL_Keycode.SDLK_STOP,
    /// <summary>Again/Redo key.</summary>
    Again = SDL_Keycode.SDLK_AGAIN,
    /// <summary>Undo key.</summary>
    Undo = SDL_Keycode.SDLK_UNDO,
    /// <summary>Cut key.</summary>
    Cut = SDL_Keycode.SDLK_CUT,
    /// <summary>Copy key.</summary>
    Copy = SDL_Keycode.SDLK_COPY,
    /// <summary>Paste key.</summary>
    Paste = SDL_Keycode.SDLK_PASTE,
    /// <summary>Find key.</summary>
    Find = SDL_Keycode.SDLK_FIND,
    /// <summary>Mute key.</summary>
    Mute = SDL_Keycode.SDLK_MUTE,
    /// <summary>Volume Up key.</summary>
    VolumeUp = SDL_Keycode.SDLK_VOLUMEUP,
    /// <summary>Volume Down key.</summary>
    VolumeDown = SDL_Keycode.SDLK_VOLUMEDOWN,

    // Additional keypad keys

    /// <summary>Keypad comma key.</summary>
    KeypadComma = SDL_Keycode.SDLK_KP_COMMA,
    /// <summary>Keypad equals key (AS/400).</summary>
    KeypadEqualsAS400 = SDL_Keycode.SDLK_KP_EQUALSAS400,

    // System keys

    /// <summary>Alt Erase key.</summary>
    AltErase = SDL_Keycode.SDLK_ALTERASE,
    /// <summary>SysReq key.</summary>
    SysReq = SDL_Keycode.SDLK_SYSREQ,
    /// <summary>Cancel key.</summary>
    Cancel = SDL_Keycode.SDLK_CANCEL,
    /// <summary>Clear key.</summary>
    Clear = SDL_Keycode.SDLK_CLEAR,
    /// <summary>Prior key.</summary>
    Prior = SDL_Keycode.SDLK_PRIOR,
    /// <summary>Return2 key.</summary>
    Return2 = SDL_Keycode.SDLK_RETURN2,
    /// <summary>Separator key.</summary>
    Separator = SDL_Keycode.SDLK_SEPARATOR,
    /// <summary>Out key.</summary>
    Out = SDL_Keycode.SDLK_OUT,
    /// <summary>Oper key.</summary>
    Oper = SDL_Keycode.SDLK_OPER,
    /// <summary>Clear Again key.</summary>
    ClearAgain = SDL_Keycode.SDLK_CLEARAGAIN,
    /// <summary>CrSel key.</summary>
    CrSel = SDL_Keycode.SDLK_CRSEL,
    /// <summary>ExSel key.</summary>
    ExSel = SDL_Keycode.SDLK_EXSEL,

    // Extended keypad keys

    /// <summary>Keypad 00 key.</summary>
    Keypad00 = SDL_Keycode.SDLK_KP_00,
    /// <summary>Keypad 000 key.</summary>
    Keypad000 = SDL_Keycode.SDLK_KP_000,
    /// <summary>Thousands separator key.</summary>
    ThousandsSeparator = SDL_Keycode.SDLK_THOUSANDSSEPARATOR,
    /// <summary>Decimal separator key.</summary>
    DecimalSeparator = SDL_Keycode.SDLK_DECIMALSEPARATOR,
    /// <summary>Currency unit key.</summary>
    CurrencyUnit = SDL_Keycode.SDLK_CURRENCYUNIT,
    /// <summary>Currency subunit key.</summary>
    CurrencySubunit = SDL_Keycode.SDLK_CURRENCYSUBUNIT,
    /// <summary>Keypad left parenthesis key.</summary>
    KeypadLeftParen = SDL_Keycode.SDLK_KP_LEFTPAREN,
    /// <summary>Keypad right parenthesis key.</summary>
    KeypadRightParen = SDL_Keycode.SDLK_KP_RIGHTPAREN,
    /// <summary>Keypad left brace key.</summary>
    KeypadLeftBrace = SDL_Keycode.SDLK_KP_LEFTBRACE,
    /// <summary>Keypad right brace key.</summary>
    KeypadRightBrace = SDL_Keycode.SDLK_KP_RIGHTBRACE,
    /// <summary>Keypad tab key.</summary>
    KeypadTab = SDL_Keycode.SDLK_KP_TAB,
    /// <summary>Keypad backspace key.</summary>
    KeypadBackspace = SDL_Keycode.SDLK_KP_BACKSPACE,
    /// <summary>Keypad A key.</summary>
    KeypadA = SDL_Keycode.SDLK_KP_A,
    /// <summary>Keypad B key.</summary>
    KeypadB = SDL_Keycode.SDLK_KP_B,
    /// <summary>Keypad C key.</summary>
    KeypadC = SDL_Keycode.SDLK_KP_C,
    /// <summary>Keypad D key.</summary>
    KeypadD = SDL_Keycode.SDLK_KP_D,
    /// <summary>Keypad E key.</summary>
    KeypadE = SDL_Keycode.SDLK_KP_E,
    /// <summary>Keypad F key.</summary>
    KeypadF = SDL_Keycode.SDLK_KP_F,
    /// <summary>Keypad XOR key.</summary>
    KeypadXor = SDL_Keycode.SDLK_KP_XOR,
    /// <summary>Keypad power key.</summary>
    KeypadPower = SDL_Keycode.SDLK_KP_POWER,
    /// <summary>Keypad percent key.</summary>
    KeypadPercent = SDL_Keycode.SDLK_KP_PERCENT,
    /// <summary>Keypad less than key.</summary>
    KeypadLess = SDL_Keycode.SDLK_KP_LESS,
    /// <summary>Keypad greater than key.</summary>
    KeypadGreater = SDL_Keycode.SDLK_KP_GREATER,
    /// <summary>Keypad ampersand key.</summary>
    KeypadAmpersand = SDL_Keycode.SDLK_KP_AMPERSAND,
    /// <summary>Keypad double ampersand key.</summary>
    KeypadDoubleAmpersand = SDL_Keycode.SDLK_KP_DBLAMPERSAND,
    /// <summary>Keypad vertical bar key.</summary>
    KeypadVerticalBar = SDL_Keycode.SDLK_KP_VERTICALBAR,
    /// <summary>Keypad double vertical bar key.</summary>
    KeypadDoubleVerticalBar = SDL_Keycode.SDLK_KP_DBLVERTICALBAR,
    /// <summary>Keypad colon key.</summary>
    KeypadColon = SDL_Keycode.SDLK_KP_COLON,
    /// <summary>Keypad hash key.</summary>
    KeypadHash = SDL_Keycode.SDLK_KP_HASH,
    /// <summary>Keypad space key.</summary>
    KeypadSpace = SDL_Keycode.SDLK_KP_SPACE,
    /// <summary>Keypad at key.</summary>
    KeypadAt = SDL_Keycode.SDLK_KP_AT,
    /// <summary>Keypad exclamation key.</summary>
    KeypadExclam = SDL_Keycode.SDLK_KP_EXCLAM,
    /// <summary>Keypad memory store key.</summary>
    KeypadMemStore = SDL_Keycode.SDLK_KP_MEMSTORE,
    /// <summary>Keypad memory recall key.</summary>
    KeypadMemRecall = SDL_Keycode.SDLK_KP_MEMRECALL,
    /// <summary>Keypad memory clear key.</summary>
    KeypadMemClear = SDL_Keycode.SDLK_KP_MEMCLEAR,
    /// <summary>Keypad memory add key.</summary>
    KeypadMemAdd = SDL_Keycode.SDLK_KP_MEMADD,
    /// <summary>Keypad memory subtract key.</summary>
    KeypadMemSubtract = SDL_Keycode.SDLK_KP_MEMSUBTRACT,
    /// <summary>Keypad memory multiply key.</summary>
    KeypadMemMultiply = SDL_Keycode.SDLK_KP_MEMMULTIPLY,
    /// <summary>Keypad memory divide key.</summary>
    KeypadMemDivide = SDL_Keycode.SDLK_KP_MEMDIVIDE,
    /// <summary>Keypad plus/minus key.</summary>
    KeypadPlusMinus = SDL_Keycode.SDLK_KP_PLUSMINUS,
    /// <summary>Keypad clear key.</summary>
    KeypadClear = SDL_Keycode.SDLK_KP_CLEAR,
    /// <summary>Keypad clear entry key.</summary>
    KeypadClearEntry = SDL_Keycode.SDLK_KP_CLEARENTRY,
    /// <summary>Keypad binary key.</summary>
    KeypadBinary = SDL_Keycode.SDLK_KP_BINARY,
    /// <summary>Keypad octal key.</summary>
    KeypadOctal = SDL_Keycode.SDLK_KP_OCTAL,
    /// <summary>Keypad decimal key.</summary>
    KeypadDecimal = SDL_Keycode.SDLK_KP_DECIMAL,
    /// <summary>Keypad hexadecimal key.</summary>
    KeypadHexadecimal = SDL_Keycode.SDLK_KP_HEXADECIMAL,

    // Modifier keys

    /// <summary>Left Control key.</summary>
    LeftCtrl = SDL_Keycode.SDLK_LCTRL,
    /// <summary>Left Shift key.</summary>
    LeftShift = SDL_Keycode.SDLK_LSHIFT,
    /// <summary>Left Alt key.</summary>
    LeftAlt = SDL_Keycode.SDLK_LALT,
    /// <summary>Left GUI key (Windows, Command, etc.).</summary>
    LeftGui = SDL_Keycode.SDLK_LGUI,
    /// <summary>Right Control key.</summary>
    RightCtrl = SDL_Keycode.SDLK_RCTRL,
    /// <summary>Right Shift key.</summary>
    RightShift = SDL_Keycode.SDLK_RSHIFT,
    /// <summary>Right Alt key.</summary>
    RightAlt = SDL_Keycode.SDLK_RALT,
    /// <summary>Right GUI key (Windows, Command, etc.).</summary>
    RightGui = SDL_Keycode.SDLK_RGUI,

    // Mode and power management keys

    /// <summary>Mode key.</summary>
    Mode = SDL_Keycode.SDLK_MODE,
    /// <summary>Sleep key.</summary>
    Sleep = SDL_Keycode.SDLK_SLEEP,
    /// <summary>Wake key.</summary>
    Wake = SDL_Keycode.SDLK_WAKE,

    // Media keys

    /// <summary>Channel increment key.</summary>
    ChannelIncrement = SDL_Keycode.SDLK_CHANNEL_INCREMENT,
    /// <summary>Channel decrement key.</summary>
    ChannelDecrement = SDL_Keycode.SDLK_CHANNEL_DECREMENT,
    /// <summary>Media play key.</summary>
    MediaPlay = SDL_Keycode.SDLK_MEDIA_PLAY,
    /// <summary>Media pause key.</summary>
    MediaPause = SDL_Keycode.SDLK_MEDIA_PAUSE,
    /// <summary>Media record key.</summary>
    MediaRecord = SDL_Keycode.SDLK_MEDIA_RECORD,
    /// <summary>Media fast forward key.</summary>
    MediaFastForward = SDL_Keycode.SDLK_MEDIA_FAST_FORWARD,
    /// <summary>Media rewind key.</summary>
    MediaRewind = SDL_Keycode.SDLK_MEDIA_REWIND,
    /// <summary>Media next track key.</summary>
    MediaNextTrack = SDL_Keycode.SDLK_MEDIA_NEXT_TRACK,
    /// <summary>Media previous track key.</summary>
    MediaPreviousTrack = SDL_Keycode.SDLK_MEDIA_PREVIOUS_TRACK,
    /// <summary>Media stop key.</summary>
    MediaStop = SDL_Keycode.SDLK_MEDIA_STOP,
    /// <summary>Media eject key.</summary>
    MediaEject = SDL_Keycode.SDLK_MEDIA_EJECT,
    /// <summary>Media play/pause key.</summary>
    MediaPlayPause = SDL_Keycode.SDLK_MEDIA_PLAY_PAUSE,
    /// <summary>Media select key.</summary>
    MediaSelect = SDL_Keycode.SDLK_MEDIA_SELECT,

    // Application control keys

    /// <summary>AC New key.</summary>
    AcNew = SDL_Keycode.SDLK_AC_NEW,
    /// <summary>AC Open key.</summary>
    AcOpen = SDL_Keycode.SDLK_AC_OPEN,
    /// <summary>AC Close key.</summary>
    AcClose = SDL_Keycode.SDLK_AC_CLOSE,
    /// <summary>AC Exit key.</summary>
    AcExit = SDL_Keycode.SDLK_AC_EXIT,
    /// <summary>AC Save key.</summary>
    AcSave = SDL_Keycode.SDLK_AC_SAVE,
    /// <summary>AC Print key.</summary>
    AcPrint = SDL_Keycode.SDLK_AC_PRINT,
    /// <summary>AC Properties key.</summary>
    AcProperties = SDL_Keycode.SDLK_AC_PROPERTIES,
    /// <summary>AC Search key.</summary>
    AcSearch = SDL_Keycode.SDLK_AC_SEARCH,
    /// <summary>AC Home key.</summary>
    AcHome = SDL_Keycode.SDLK_AC_HOME,
    /// <summary>AC Back key.</summary>
    AcBack = SDL_Keycode.SDLK_AC_BACK,
    /// <summary>AC Forward key.</summary>
    AcForward = SDL_Keycode.SDLK_AC_FORWARD,
    /// <summary>AC Stop key.</summary>
    AcStop = SDL_Keycode.SDLK_AC_STOP,
    /// <summary>AC Refresh key.</summary>
    AcRefresh = SDL_Keycode.SDLK_AC_REFRESH,
    /// <summary>AC Bookmarks key.</summary>
    AcBookmarks = SDL_Keycode.SDLK_AC_BOOKMARKS,

    // Mobile/phone keys

    /// <summary>Soft left key (mobile).</summary>
    SoftLeft = SDL_Keycode.SDLK_SOFTLEFT,
    /// <summary>Soft right key (mobile).</summary>
    SoftRight = SDL_Keycode.SDLK_SOFTRIGHT,
    /// <summary>Call key (mobile).</summary>
    Call = SDL_Keycode.SDLK_CALL,
    /// <summary>End call key (mobile).</summary>
    EndCall = SDL_Keycode.SDLK_ENDCALL
}