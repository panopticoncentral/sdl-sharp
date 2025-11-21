// We are intentionally exposing the P/Invoke calls so people can do low-level calls if needed
#pragma warning disable CA1401 // P/Invokes should not be visible

namespace Sdl3Sharp.Native;

/// <summary>
/// P/Invoke bindings for SDL_scancode.h - Keyboard scancode definitions.
/// </summary>
/// <remarks>
/// An SDL scancode is the physical representation of a key on the keyboard,
/// independent of language and keyboard mapping. Values are based on the USB usage page standard.
/// See: https://wiki.libsdl.org/SDL3/BestKeyboardPractices
/// </remarks>
public static unsafe partial class Scancode
{
    /// <summary>
    /// The SDL keyboard scancode representation.
    /// An SDL scancode is the physical representation of a key on the keyboard,
    /// independent of language and keyboard mapping.
    /// Values are based on the USB usage page standard: https://usb.org/sites/default/files/hut1_5.pdf
    /// </summary>
    public enum SDL_Scancode
    {
        /// <summary>Unknown scancode.</summary>
        SDL_SCANCODE_UNKNOWN = 0,

        // Usage page 0x07 (USB keyboard page)

        /// <summary>The A key.</summary>
        SDL_SCANCODE_A = 4,
        /// <summary>The B key.</summary>
        SDL_SCANCODE_B = 5,
        /// <summary>The C key.</summary>
        SDL_SCANCODE_C = 6,
        /// <summary>The D key.</summary>
        SDL_SCANCODE_D = 7,
        /// <summary>The E key.</summary>
        SDL_SCANCODE_E = 8,
        /// <summary>The F key.</summary>
        SDL_SCANCODE_F = 9,
        /// <summary>The G key.</summary>
        SDL_SCANCODE_G = 10,
        /// <summary>The H key.</summary>
        SDL_SCANCODE_H = 11,
        /// <summary>The I key.</summary>
        SDL_SCANCODE_I = 12,
        /// <summary>The J key.</summary>
        SDL_SCANCODE_J = 13,
        /// <summary>The K key.</summary>
        SDL_SCANCODE_K = 14,
        /// <summary>The L key.</summary>
        SDL_SCANCODE_L = 15,
        /// <summary>The M key.</summary>
        SDL_SCANCODE_M = 16,
        /// <summary>The N key.</summary>
        SDL_SCANCODE_N = 17,
        /// <summary>The O key.</summary>
        SDL_SCANCODE_O = 18,
        /// <summary>The P key.</summary>
        SDL_SCANCODE_P = 19,
        /// <summary>The Q key.</summary>
        SDL_SCANCODE_Q = 20,
        /// <summary>The R key.</summary>
        SDL_SCANCODE_R = 21,
        /// <summary>The S key.</summary>
        SDL_SCANCODE_S = 22,
        /// <summary>The T key.</summary>
        SDL_SCANCODE_T = 23,
        /// <summary>The U key.</summary>
        SDL_SCANCODE_U = 24,
        /// <summary>The V key.</summary>
        SDL_SCANCODE_V = 25,
        /// <summary>The W key.</summary>
        SDL_SCANCODE_W = 26,
        /// <summary>The X key.</summary>
        SDL_SCANCODE_X = 27,
        /// <summary>The Y key.</summary>
        SDL_SCANCODE_Y = 28,
        /// <summary>The Z key.</summary>
        SDL_SCANCODE_Z = 29,

        /// <summary>The 1 key.</summary>
        SDL_SCANCODE_1 = 30,
        /// <summary>The 2 key.</summary>
        SDL_SCANCODE_2 = 31,
        /// <summary>The 3 key.</summary>
        SDL_SCANCODE_3 = 32,
        /// <summary>The 4 key.</summary>
        SDL_SCANCODE_4 = 33,
        /// <summary>The 5 key.</summary>
        SDL_SCANCODE_5 = 34,
        /// <summary>The 6 key.</summary>
        SDL_SCANCODE_6 = 35,
        /// <summary>The 7 key.</summary>
        SDL_SCANCODE_7 = 36,
        /// <summary>The 8 key.</summary>
        SDL_SCANCODE_8 = 37,
        /// <summary>The 9 key.</summary>
        SDL_SCANCODE_9 = 38,
        /// <summary>The 0 key.</summary>
        SDL_SCANCODE_0 = 39,

        /// <summary>The Return/Enter key.</summary>
        SDL_SCANCODE_RETURN = 40,
        /// <summary>The Escape key.</summary>
        SDL_SCANCODE_ESCAPE = 41,
        /// <summary>The Backspace key.</summary>
        SDL_SCANCODE_BACKSPACE = 42,
        /// <summary>The Tab key.</summary>
        SDL_SCANCODE_TAB = 43,
        /// <summary>The Space key.</summary>
        SDL_SCANCODE_SPACE = 44,

        /// <summary>The Minus/Hyphen key.</summary>
        SDL_SCANCODE_MINUS = 45,
        /// <summary>The Equals key.</summary>
        SDL_SCANCODE_EQUALS = 46,
        /// <summary>The Left Bracket key.</summary>
        SDL_SCANCODE_LEFTBRACKET = 47,
        /// <summary>The Right Bracket key.</summary>
        SDL_SCANCODE_RIGHTBRACKET = 48,
        /// <summary>
        /// The Backslash key. Located at the lower left of the return key on ISO keyboards
        /// and at the right end of the QWERTY row on ANSI keyboards.
        /// </summary>
        SDL_SCANCODE_BACKSLASH = 49,
        /// <summary>
        /// ISO USB keyboards use this code instead of 49 for the same key, but all OSes
        /// treat the two codes identically. You should generally use SDL_SCANCODE_BACKSLASH instead.
        /// </summary>
        SDL_SCANCODE_NONUSHASH = 50,
        /// <summary>The Semicolon key.</summary>
        SDL_SCANCODE_SEMICOLON = 51,
        /// <summary>The Apostrophe key.</summary>
        SDL_SCANCODE_APOSTROPHE = 52,
        /// <summary>
        /// The Grave/Tilde key. Located in the top left corner on both ANSI and ISO keyboards.
        /// </summary>
        SDL_SCANCODE_GRAVE = 53,
        /// <summary>The Comma key.</summary>
        SDL_SCANCODE_COMMA = 54,
        /// <summary>The Period key.</summary>
        SDL_SCANCODE_PERIOD = 55,
        /// <summary>The Slash key.</summary>
        SDL_SCANCODE_SLASH = 56,

        /// <summary>The Caps Lock key.</summary>
        SDL_SCANCODE_CAPSLOCK = 57,

        /// <summary>The F1 key.</summary>
        SDL_SCANCODE_F1 = 58,
        /// <summary>The F2 key.</summary>
        SDL_SCANCODE_F2 = 59,
        /// <summary>The F3 key.</summary>
        SDL_SCANCODE_F3 = 60,
        /// <summary>The F4 key.</summary>
        SDL_SCANCODE_F4 = 61,
        /// <summary>The F5 key.</summary>
        SDL_SCANCODE_F5 = 62,
        /// <summary>The F6 key.</summary>
        SDL_SCANCODE_F6 = 63,
        /// <summary>The F7 key.</summary>
        SDL_SCANCODE_F7 = 64,
        /// <summary>The F8 key.</summary>
        SDL_SCANCODE_F8 = 65,
        /// <summary>The F9 key.</summary>
        SDL_SCANCODE_F9 = 66,
        /// <summary>The F10 key.</summary>
        SDL_SCANCODE_F10 = 67,
        /// <summary>The F11 key.</summary>
        SDL_SCANCODE_F11 = 68,
        /// <summary>The F12 key.</summary>
        SDL_SCANCODE_F12 = 69,

        /// <summary>The Print Screen key.</summary>
        SDL_SCANCODE_PRINTSCREEN = 70,
        /// <summary>The Scroll Lock key.</summary>
        SDL_SCANCODE_SCROLLLOCK = 71,
        /// <summary>The Pause key.</summary>
        SDL_SCANCODE_PAUSE = 72,
        /// <summary>The Insert key. On PC keyboards; Help on some Mac keyboards.</summary>
        SDL_SCANCODE_INSERT = 73,
        /// <summary>The Home key.</summary>
        SDL_SCANCODE_HOME = 74,
        /// <summary>The Page Up key.</summary>
        SDL_SCANCODE_PAGEUP = 75,
        /// <summary>The Delete key.</summary>
        SDL_SCANCODE_DELETE = 76,
        /// <summary>The End key.</summary>
        SDL_SCANCODE_END = 77,
        /// <summary>The Page Down key.</summary>
        SDL_SCANCODE_PAGEDOWN = 78,
        /// <summary>The Right Arrow key.</summary>
        SDL_SCANCODE_RIGHT = 79,
        /// <summary>The Left Arrow key.</summary>
        SDL_SCANCODE_LEFT = 80,
        /// <summary>The Down Arrow key.</summary>
        SDL_SCANCODE_DOWN = 81,
        /// <summary>The Up Arrow key.</summary>
        SDL_SCANCODE_UP = 82,

        /// <summary>The Num Lock key on PC; Clear on Mac keyboards.</summary>
        SDL_SCANCODE_NUMLOCKCLEAR = 83,
        /// <summary>The Keypad Divide key.</summary>
        SDL_SCANCODE_KP_DIVIDE = 84,
        /// <summary>The Keypad Multiply key.</summary>
        SDL_SCANCODE_KP_MULTIPLY = 85,
        /// <summary>The Keypad Minus key.</summary>
        SDL_SCANCODE_KP_MINUS = 86,
        /// <summary>The Keypad Plus key.</summary>
        SDL_SCANCODE_KP_PLUS = 87,
        /// <summary>The Keypad Enter key.</summary>
        SDL_SCANCODE_KP_ENTER = 88,
        /// <summary>The Keypad 1 key.</summary>
        SDL_SCANCODE_KP_1 = 89,
        /// <summary>The Keypad 2 key.</summary>
        SDL_SCANCODE_KP_2 = 90,
        /// <summary>The Keypad 3 key.</summary>
        SDL_SCANCODE_KP_3 = 91,
        /// <summary>The Keypad 4 key.</summary>
        SDL_SCANCODE_KP_4 = 92,
        /// <summary>The Keypad 5 key.</summary>
        SDL_SCANCODE_KP_5 = 93,
        /// <summary>The Keypad 6 key.</summary>
        SDL_SCANCODE_KP_6 = 94,
        /// <summary>The Keypad 7 key.</summary>
        SDL_SCANCODE_KP_7 = 95,
        /// <summary>The Keypad 8 key.</summary>
        SDL_SCANCODE_KP_8 = 96,
        /// <summary>The Keypad 9 key.</summary>
        SDL_SCANCODE_KP_9 = 97,
        /// <summary>The Keypad 0 key.</summary>
        SDL_SCANCODE_KP_0 = 98,
        /// <summary>The Keypad Period key.</summary>
        SDL_SCANCODE_KP_PERIOD = 99,

        /// <summary>
        /// The additional key that ISO keyboards have over ANSI ones, located between left shift and Z.
        /// </summary>
        SDL_SCANCODE_NONUSBACKSLASH = 100,
        /// <summary>The Application/Context Menu key (Windows contextual menu, compose).</summary>
        SDL_SCANCODE_APPLICATION = 101,
        /// <summary>The Power key. Some Mac keyboards have a power key.</summary>
        SDL_SCANCODE_POWER = 102,
        /// <summary>The Keypad Equals key.</summary>
        SDL_SCANCODE_KP_EQUALS = 103,
        /// <summary>The F13 key.</summary>
        SDL_SCANCODE_F13 = 104,
        /// <summary>The F14 key.</summary>
        SDL_SCANCODE_F14 = 105,
        /// <summary>The F15 key.</summary>
        SDL_SCANCODE_F15 = 106,
        /// <summary>The F16 key.</summary>
        SDL_SCANCODE_F16 = 107,
        /// <summary>The F17 key.</summary>
        SDL_SCANCODE_F17 = 108,
        /// <summary>The F18 key.</summary>
        SDL_SCANCODE_F18 = 109,
        /// <summary>The F19 key.</summary>
        SDL_SCANCODE_F19 = 110,
        /// <summary>The F20 key.</summary>
        SDL_SCANCODE_F20 = 111,
        /// <summary>The F21 key.</summary>
        SDL_SCANCODE_F21 = 112,
        /// <summary>The F22 key.</summary>
        SDL_SCANCODE_F22 = 113,
        /// <summary>The F23 key.</summary>
        SDL_SCANCODE_F23 = 114,
        /// <summary>The F24 key.</summary>
        SDL_SCANCODE_F24 = 115,
        /// <summary>The Execute key.</summary>
        SDL_SCANCODE_EXECUTE = 116,
        /// <summary>The Help key (AL Integrated Help Center).</summary>
        SDL_SCANCODE_HELP = 117,
        /// <summary>The Menu key (show menu).</summary>
        SDL_SCANCODE_MENU = 118,
        /// <summary>The Select key.</summary>
        SDL_SCANCODE_SELECT = 119,
        /// <summary>The Stop key (AC Stop).</summary>
        SDL_SCANCODE_STOP = 120,
        /// <summary>The Again key (AC Redo/Repeat).</summary>
        SDL_SCANCODE_AGAIN = 121,
        /// <summary>The Undo key (AC Undo).</summary>
        SDL_SCANCODE_UNDO = 122,
        /// <summary>The Cut key (AC Cut).</summary>
        SDL_SCANCODE_CUT = 123,
        /// <summary>The Copy key (AC Copy).</summary>
        SDL_SCANCODE_COPY = 124,
        /// <summary>The Paste key (AC Paste).</summary>
        SDL_SCANCODE_PASTE = 125,
        /// <summary>The Find key (AC Find).</summary>
        SDL_SCANCODE_FIND = 126,
        /// <summary>The Mute key.</summary>
        SDL_SCANCODE_MUTE = 127,
        /// <summary>The Volume Up key.</summary>
        SDL_SCANCODE_VOLUMEUP = 128,
        /// <summary>The Volume Down key.</summary>
        SDL_SCANCODE_VOLUMEDOWN = 129,

        /// <summary>The Keypad Comma key.</summary>
        SDL_SCANCODE_KP_COMMA = 133,
        /// <summary>The Keypad Equals key (AS/400).</summary>
        SDL_SCANCODE_KP_EQUALSAS400 = 134,

        /// <summary>International key 1, used on Asian keyboards.</summary>
        SDL_SCANCODE_INTERNATIONAL1 = 135,
        /// <summary>International key 2.</summary>
        SDL_SCANCODE_INTERNATIONAL2 = 136,
        /// <summary>International key 3 (Yen).</summary>
        SDL_SCANCODE_INTERNATIONAL3 = 137,
        /// <summary>International key 4.</summary>
        SDL_SCANCODE_INTERNATIONAL4 = 138,
        /// <summary>International key 5.</summary>
        SDL_SCANCODE_INTERNATIONAL5 = 139,
        /// <summary>International key 6.</summary>
        SDL_SCANCODE_INTERNATIONAL6 = 140,
        /// <summary>International key 7.</summary>
        SDL_SCANCODE_INTERNATIONAL7 = 141,
        /// <summary>International key 8.</summary>
        SDL_SCANCODE_INTERNATIONAL8 = 142,
        /// <summary>International key 9.</summary>
        SDL_SCANCODE_INTERNATIONAL9 = 143,
        /// <summary>Language key 1 (Hangul/English toggle).</summary>
        SDL_SCANCODE_LANG1 = 144,
        /// <summary>Language key 2 (Hanja conversion).</summary>
        SDL_SCANCODE_LANG2 = 145,
        /// <summary>Language key 3 (Katakana).</summary>
        SDL_SCANCODE_LANG3 = 146,
        /// <summary>Language key 4 (Hiragana).</summary>
        SDL_SCANCODE_LANG4 = 147,
        /// <summary>Language key 5 (Zenkaku/Hankaku).</summary>
        SDL_SCANCODE_LANG5 = 148,
        /// <summary>Language key 6 (reserved).</summary>
        SDL_SCANCODE_LANG6 = 149,
        /// <summary>Language key 7 (reserved).</summary>
        SDL_SCANCODE_LANG7 = 150,
        /// <summary>Language key 8 (reserved).</summary>
        SDL_SCANCODE_LANG8 = 151,
        /// <summary>Language key 9 (reserved).</summary>
        SDL_SCANCODE_LANG9 = 152,

        /// <summary>The Alt Erase key (Erase-Eaze).</summary>
        SDL_SCANCODE_ALTERASE = 153,
        /// <summary>The SysReq key.</summary>
        SDL_SCANCODE_SYSREQ = 154,
        /// <summary>The Cancel key (AC Cancel).</summary>
        SDL_SCANCODE_CANCEL = 155,
        /// <summary>The Clear key.</summary>
        SDL_SCANCODE_CLEAR = 156,
        /// <summary>The Prior key.</summary>
        SDL_SCANCODE_PRIOR = 157,
        /// <summary>The Return2 key.</summary>
        SDL_SCANCODE_RETURN2 = 158,
        /// <summary>The Separator key.</summary>
        SDL_SCANCODE_SEPARATOR = 159,
        /// <summary>The Out key.</summary>
        SDL_SCANCODE_OUT = 160,
        /// <summary>The Oper key.</summary>
        SDL_SCANCODE_OPER = 161,
        /// <summary>The Clear Again key.</summary>
        SDL_SCANCODE_CLEARAGAIN = 162,
        /// <summary>The CrSel key.</summary>
        SDL_SCANCODE_CRSEL = 163,
        /// <summary>The ExSel key.</summary>
        SDL_SCANCODE_EXSEL = 164,

        /// <summary>The Keypad 00 key.</summary>
        SDL_SCANCODE_KP_00 = 176,
        /// <summary>The Keypad 000 key.</summary>
        SDL_SCANCODE_KP_000 = 177,
        /// <summary>The Thousands Separator key.</summary>
        SDL_SCANCODE_THOUSANDSSEPARATOR = 178,
        /// <summary>The Decimal Separator key.</summary>
        SDL_SCANCODE_DECIMALSEPARATOR = 179,
        /// <summary>The Currency Unit key.</summary>
        SDL_SCANCODE_CURRENCYUNIT = 180,
        /// <summary>The Currency Subunit key.</summary>
        SDL_SCANCODE_CURRENCYSUBUNIT = 181,
        /// <summary>The Keypad Left Parenthesis key.</summary>
        SDL_SCANCODE_KP_LEFTPAREN = 182,
        /// <summary>The Keypad Right Parenthesis key.</summary>
        SDL_SCANCODE_KP_RIGHTPAREN = 183,
        /// <summary>The Keypad Left Brace key.</summary>
        SDL_SCANCODE_KP_LEFTBRACE = 184,
        /// <summary>The Keypad Right Brace key.</summary>
        SDL_SCANCODE_KP_RIGHTBRACE = 185,
        /// <summary>The Keypad Tab key.</summary>
        SDL_SCANCODE_KP_TAB = 186,
        /// <summary>The Keypad Backspace key.</summary>
        SDL_SCANCODE_KP_BACKSPACE = 187,
        /// <summary>The Keypad A key.</summary>
        SDL_SCANCODE_KP_A = 188,
        /// <summary>The Keypad B key.</summary>
        SDL_SCANCODE_KP_B = 189,
        /// <summary>The Keypad C key.</summary>
        SDL_SCANCODE_KP_C = 190,
        /// <summary>The Keypad D key.</summary>
        SDL_SCANCODE_KP_D = 191,
        /// <summary>The Keypad E key.</summary>
        SDL_SCANCODE_KP_E = 192,
        /// <summary>The Keypad F key.</summary>
        SDL_SCANCODE_KP_F = 193,
        /// <summary>The Keypad XOR key.</summary>
        SDL_SCANCODE_KP_XOR = 194,
        /// <summary>The Keypad Power key.</summary>
        SDL_SCANCODE_KP_POWER = 195,
        /// <summary>The Keypad Percent key.</summary>
        SDL_SCANCODE_KP_PERCENT = 196,
        /// <summary>The Keypad Less Than key.</summary>
        SDL_SCANCODE_KP_LESS = 197,
        /// <summary>The Keypad Greater Than key.</summary>
        SDL_SCANCODE_KP_GREATER = 198,
        /// <summary>The Keypad Ampersand key.</summary>
        SDL_SCANCODE_KP_AMPERSAND = 199,
        /// <summary>The Keypad Double Ampersand key.</summary>
        SDL_SCANCODE_KP_DBLAMPERSAND = 200,
        /// <summary>The Keypad Vertical Bar key.</summary>
        SDL_SCANCODE_KP_VERTICALBAR = 201,
        /// <summary>The Keypad Double Vertical Bar key.</summary>
        SDL_SCANCODE_KP_DBLVERTICALBAR = 202,
        /// <summary>The Keypad Colon key.</summary>
        SDL_SCANCODE_KP_COLON = 203,
        /// <summary>The Keypad Hash key.</summary>
        SDL_SCANCODE_KP_HASH = 204,
        /// <summary>The Keypad Space key.</summary>
        SDL_SCANCODE_KP_SPACE = 205,
        /// <summary>The Keypad At key.</summary>
        SDL_SCANCODE_KP_AT = 206,
        /// <summary>The Keypad Exclamation key.</summary>
        SDL_SCANCODE_KP_EXCLAM = 207,
        /// <summary>The Keypad Memory Store key.</summary>
        SDL_SCANCODE_KP_MEMSTORE = 208,
        /// <summary>The Keypad Memory Recall key.</summary>
        SDL_SCANCODE_KP_MEMRECALL = 209,
        /// <summary>The Keypad Memory Clear key.</summary>
        SDL_SCANCODE_KP_MEMCLEAR = 210,
        /// <summary>The Keypad Memory Add key.</summary>
        SDL_SCANCODE_KP_MEMADD = 211,
        /// <summary>The Keypad Memory Subtract key.</summary>
        SDL_SCANCODE_KP_MEMSUBTRACT = 212,
        /// <summary>The Keypad Memory Multiply key.</summary>
        SDL_SCANCODE_KP_MEMMULTIPLY = 213,
        /// <summary>The Keypad Memory Divide key.</summary>
        SDL_SCANCODE_KP_MEMDIVIDE = 214,
        /// <summary>The Keypad Plus/Minus key.</summary>
        SDL_SCANCODE_KP_PLUSMINUS = 215,
        /// <summary>The Keypad Clear key.</summary>
        SDL_SCANCODE_KP_CLEAR = 216,
        /// <summary>The Keypad Clear Entry key.</summary>
        SDL_SCANCODE_KP_CLEARENTRY = 217,
        /// <summary>The Keypad Binary key.</summary>
        SDL_SCANCODE_KP_BINARY = 218,
        /// <summary>The Keypad Octal key.</summary>
        SDL_SCANCODE_KP_OCTAL = 219,
        /// <summary>The Keypad Decimal key.</summary>
        SDL_SCANCODE_KP_DECIMAL = 220,
        /// <summary>The Keypad Hexadecimal key.</summary>
        SDL_SCANCODE_KP_HEXADECIMAL = 221,

        /// <summary>The Left Control key.</summary>
        SDL_SCANCODE_LCTRL = 224,
        /// <summary>The Left Shift key.</summary>
        SDL_SCANCODE_LSHIFT = 225,
        /// <summary>The Left Alt key (Option on Mac).</summary>
        SDL_SCANCODE_LALT = 226,
        /// <summary>The Left GUI key (Windows, Command on Apple, Meta).</summary>
        SDL_SCANCODE_LGUI = 227,
        /// <summary>The Right Control key.</summary>
        SDL_SCANCODE_RCTRL = 228,
        /// <summary>The Right Shift key.</summary>
        SDL_SCANCODE_RSHIFT = 229,
        /// <summary>The Right Alt key (Alt Gr, Option on Mac).</summary>
        SDL_SCANCODE_RALT = 230,
        /// <summary>The Right GUI key (Windows, Command on Apple, Meta).</summary>
        SDL_SCANCODE_RGUI = 231,

        /// <summary>The Mode key. Has a special SDL_KMOD_MODE modifier.</summary>
        SDL_SCANCODE_MODE = 257,

        // Usage page 0x0C (USB consumer page)

        /// <summary>The Sleep key.</summary>
        SDL_SCANCODE_SLEEP = 258,
        /// <summary>The Wake key.</summary>
        SDL_SCANCODE_WAKE = 259,

        /// <summary>The Channel Increment key.</summary>
        SDL_SCANCODE_CHANNEL_INCREMENT = 260,
        /// <summary>The Channel Decrement key.</summary>
        SDL_SCANCODE_CHANNEL_DECREMENT = 261,

        /// <summary>The Media Play key.</summary>
        SDL_SCANCODE_MEDIA_PLAY = 262,
        /// <summary>The Media Pause key.</summary>
        SDL_SCANCODE_MEDIA_PAUSE = 263,
        /// <summary>The Media Record key.</summary>
        SDL_SCANCODE_MEDIA_RECORD = 264,
        /// <summary>The Media Fast Forward key.</summary>
        SDL_SCANCODE_MEDIA_FAST_FORWARD = 265,
        /// <summary>The Media Rewind key.</summary>
        SDL_SCANCODE_MEDIA_REWIND = 266,
        /// <summary>The Media Next Track key.</summary>
        SDL_SCANCODE_MEDIA_NEXT_TRACK = 267,
        /// <summary>The Media Previous Track key.</summary>
        SDL_SCANCODE_MEDIA_PREVIOUS_TRACK = 268,
        /// <summary>The Media Stop key.</summary>
        SDL_SCANCODE_MEDIA_STOP = 269,
        /// <summary>The Media Eject key.</summary>
        SDL_SCANCODE_MEDIA_EJECT = 270,
        /// <summary>The Media Play/Pause key.</summary>
        SDL_SCANCODE_MEDIA_PLAY_PAUSE = 271,
        /// <summary>The Media Select key.</summary>
        SDL_SCANCODE_MEDIA_SELECT = 272,

        /// <summary>The AC New key.</summary>
        SDL_SCANCODE_AC_NEW = 273,
        /// <summary>The AC Open key.</summary>
        SDL_SCANCODE_AC_OPEN = 274,
        /// <summary>The AC Close key.</summary>
        SDL_SCANCODE_AC_CLOSE = 275,
        /// <summary>The AC Exit key.</summary>
        SDL_SCANCODE_AC_EXIT = 276,
        /// <summary>The AC Save key.</summary>
        SDL_SCANCODE_AC_SAVE = 277,
        /// <summary>The AC Print key.</summary>
        SDL_SCANCODE_AC_PRINT = 278,
        /// <summary>The AC Properties key.</summary>
        SDL_SCANCODE_AC_PROPERTIES = 279,

        /// <summary>The AC Search key.</summary>
        SDL_SCANCODE_AC_SEARCH = 280,
        /// <summary>The AC Home key.</summary>
        SDL_SCANCODE_AC_HOME = 281,
        /// <summary>The AC Back key.</summary>
        SDL_SCANCODE_AC_BACK = 282,
        /// <summary>The AC Forward key.</summary>
        SDL_SCANCODE_AC_FORWARD = 283,
        /// <summary>The AC Stop key.</summary>
        SDL_SCANCODE_AC_STOP = 284,
        /// <summary>The AC Refresh key.</summary>
        SDL_SCANCODE_AC_REFRESH = 285,
        /// <summary>The AC Bookmarks key.</summary>
        SDL_SCANCODE_AC_BOOKMARKS = 286,

        // Mobile keys

        /// <summary>
        /// The Soft Left key. Usually situated below the display on phones and used as a
        /// multi-function feature key for selecting a software defined function shown on
        /// the bottom left of the display.
        /// </summary>
        SDL_SCANCODE_SOFTLEFT = 287,
        /// <summary>
        /// The Soft Right key. Usually situated below the display on phones and used as a
        /// multi-function feature key for selecting a software defined function shown on
        /// the bottom right of the display.
        /// </summary>
        SDL_SCANCODE_SOFTRIGHT = 288,
        /// <summary>The Call key. Used for accepting phone calls.</summary>
        SDL_SCANCODE_CALL = 289,
        /// <summary>The End Call key. Used for rejecting phone calls.</summary>
        SDL_SCANCODE_ENDCALL = 290,

        /// <summary>Reserved for dynamic keycodes (400-500).</summary>
        SDL_SCANCODE_RESERVED = 400,

        /// <summary>Not a key, just marks the number of scancodes for array bounds.</summary>
        SDL_SCANCODE_COUNT = 512
    }
}
