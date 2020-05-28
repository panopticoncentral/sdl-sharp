// Bitwise operations on scancode even though it's not a flag enum.
#pragma warning disable RCS1130

namespace SdlSharp.Input
{
    /// <summary>
    /// Key codes.
    /// </summary>
    public enum Keycode
    {

        /// <summary>
        /// Unknown key
        /// </summary>
        Unknown = 0,

        /// <summary>
        /// Backspace key
        /// </summary>
        Backspace = '\b',

        /// <summary>
        /// Tab key
        /// </summary>
        Tab = '\t',

        /// <summary>
        /// Return key
        /// </summary>
        Return = '\r',

        /// <summary>
        /// Escape key
        /// </summary>
        Escape = 27,

        /// <summary>
        /// Space key
        /// </summary>
        Space = ' ',

        /// <summary>
        /// ExclamationMark key
        /// </summary>
        ExclamationMark = '!',

        /// <summary>
        /// DoubleQuote key
        /// </summary>
        DoubleQuote = '"',

        /// <summary>
        /// Hash key
        /// </summary>
        Hash = '#',

        /// <summary>
        /// Dollar key
        /// </summary>
        Dollar = '$',

        /// <summary>
        /// Percent key
        /// </summary>
        Percent = '%',

        /// <summary>
        /// Ampersand key
        /// </summary>
        Ampersand = '&',

        /// <summary>
        /// Quote key
        /// </summary>
        Quote = '\'',

        /// <summary>
        /// LeftParen key
        /// </summary>
        LeftParen = '(',

        /// <summary>
        /// RightParen key
        /// </summary>
        RightParen = ')',

        /// <summary>
        /// Asterisk key
        /// </summary>
        Asterisk = '*',

        /// <summary>
        /// Plus key
        /// </summary>
        Plus = '+',

        /// <summary>
        /// Comma key
        /// </summary>
        Comma = ',',

        /// <summary>
        /// Minus key
        /// </summary>
        Minus = '-',

        /// <summary>
        /// Period key
        /// </summary>
        Period = '.',

        /// <summary>
        /// Slash key
        /// </summary>
        Slash = '/',

        /// <summary>
        /// Number0 key
        /// </summary>
        Number0 = '0',

        /// <summary>
        /// Number1 key
        /// </summary>
        Number1 = '1',

        /// <summary>
        /// Number2 key
        /// </summary>
        Number2 = '2',

        /// <summary>
        /// Number3 key
        /// </summary>
        Number3 = '3',

        /// <summary>
        /// Number4 key
        /// </summary>
        Number4 = '4',

        /// <summary>
        /// Number5 key
        /// </summary>
        Number5 = '5',

        /// <summary>
        /// Number6 key
        /// </summary>
        Number6 = '6',

        /// <summary>
        /// Number7 key
        /// </summary>
        Number7 = '7',

        /// <summary>
        /// Number8 key
        /// </summary>
        Number8 = '8',

        /// <summary>
        /// Number9 key
        /// </summary>
        Number9 = '9',

        /// <summary>
        /// Colon key
        /// </summary>
        Colon = ':',

        /// <summary>
        /// Semicolon key
        /// </summary>
        Semicolon = ';',

        /// <summary>
        /// Less key
        /// </summary>
        Less = '<',

        /// <summary>
        /// Equals key
        /// </summary>
        Equals = '=',

        /// <summary>
        /// Greater key
        /// </summary>
        Greater = '>',

        /// <summary>
        /// QuestionMark key
        /// </summary>
        QuestionMark = '?',

        /// <summary>
        /// At key
        /// </summary>
        At = '@',

        /// <summary>
        /// LeftBracket key
        /// </summary>
        LeftBracket = '[',

        /// <summary>
        /// Backslash key
        /// </summary>
        Backslash = '\\',

        /// <summary>
        /// RightBracket key
        /// </summary>
        RightBracket = ']',

        /// <summary>
        /// Caret key
        /// </summary>
        Caret = '^',

        /// <summary>
        /// Underscore key
        /// </summary>
        Underscore = '_',

        /// <summary>
        /// Backquote key
        /// </summary>
        Backquote = '`',

        /// <summary>
        /// a key
        /// </summary>
        a = 'a',

        /// <summary>
        /// b key
        /// </summary>
        b = 'b',

        /// <summary>
        /// c key
        /// </summary>
        c = 'c',

        /// <summary>
        /// d key
        /// </summary>
        d = 'd',

        /// <summary>
        /// e key
        /// </summary>
        e = 'e',

        /// <summary>
        /// f key
        /// </summary>
        f = 'f',

        /// <summary>
        /// g key
        /// </summary>
        g = 'g',

        /// <summary>
        /// h key
        /// </summary>
        h = 'h',

        /// <summary>
        /// i key
        /// </summary>
        i = 'i',

        /// <summary>
        /// j key
        /// </summary>
        j = 'j',

        /// <summary>
        /// k key
        /// </summary>
        k = 'k',

        /// <summary>
        /// l key
        /// </summary>
        l = 'l',

        /// <summary>
        /// m key
        /// </summary>
        m = 'm',

        /// <summary>
        /// n key
        /// </summary>
        n = 'n',

        /// <summary>
        /// o key
        /// </summary>
        o = 'o',

        /// <summary>
        /// p key
        /// </summary>
        p = 'p',

        /// <summary>
        /// q key
        /// </summary>
        q = 'q',

        /// <summary>
        /// r key
        /// </summary>
        r = 'r',

        /// <summary>
        /// s key
        /// </summary>
        s = 's',

        /// <summary>
        /// t key
        /// </summary>
        t = 't',

        /// <summary>
        /// u key
        /// </summary>
        u = 'u',

        /// <summary>
        /// v key
        /// </summary>
        v = 'v',

        /// <summary>
        /// w key
        /// </summary>
        w = 'w',

        /// <summary>
        /// x key
        /// </summary>
        x = 'x',

        /// <summary>
        /// y key
        /// </summary>
        y = 'y',

        /// <summary>
        /// z key
        /// </summary>
        z = 'z',

        /// <summary>
        /// Delete key
        /// </summary>
        Delete = 127,

        /// <summary>
        /// Capslock key
        /// </summary>
        Capslock = Scancode.Capslock | Scancode.Mask,

        /// <summary>
        /// F1 key
        /// </summary>
        F1 = Scancode.F1 | Scancode.Mask,

        /// <summary>
        /// F2 key
        /// </summary>
        F2 = Scancode.F2 | Scancode.Mask,

        /// <summary>
        /// F3 key
        /// </summary>
        F3 = Scancode.F3 | Scancode.Mask,

        /// <summary>
        /// F4 key
        /// </summary>
        F4 = Scancode.F4 | Scancode.Mask,

        /// <summary>
        /// F5 key
        /// </summary>
        F5 = Scancode.F5 | Scancode.Mask,

        /// <summary>
        /// F6 key
        /// </summary>
        F6 = Scancode.F6 | Scancode.Mask,

        /// <summary>
        /// F7 key
        /// </summary>
        F7 = Scancode.F7 | Scancode.Mask,

        /// <summary>
        /// F8 key
        /// </summary>
        F8 = Scancode.F8 | Scancode.Mask,

        /// <summary>
        /// F9 key
        /// </summary>
        F9 = Scancode.F9 | Scancode.Mask,

        /// <summary>
        /// F10 key
        /// </summary>
        F10 = Scancode.F10 | Scancode.Mask,

        /// <summary>
        /// F11 key
        /// </summary>
        F11 = Scancode.F11 | Scancode.Mask,

        /// <summary>
        /// F12 key
        /// </summary>
        F12 = Scancode.F12 | Scancode.Mask,

        /// <summary>
        /// PrintScreen key
        /// </summary>
        PrintScreen = Scancode.PrintScreen | Scancode.Mask,

        /// <summary>
        /// ScrollLock key
        /// </summary>
        ScrollLock = Scancode.ScrollLock | Scancode.Mask,

        /// <summary>
        /// Pause key
        /// </summary>
        Pause = Scancode.Pause | Scancode.Mask,

        /// <summary>
        /// Insert key
        /// </summary>
        Insert = Scancode.Insert | Scancode.Mask,

        /// <summary>
        /// Home key
        /// </summary>
        Home = Scancode.Home | Scancode.Mask,

        /// <summary>
        /// PageUp key
        /// </summary>
        PageUp = Scancode.PageUp | Scancode.Mask,

        /// <summary>
        /// End key
        /// </summary>
        End = Scancode.End | Scancode.Mask,

        /// <summary>
        /// PageDown key
        /// </summary>
        PageDown = Scancode.PageDown | Scancode.Mask,

        /// <summary>
        /// Right key
        /// </summary>
        Right = Scancode.Right | Scancode.Mask,

        /// <summary>
        /// Left key
        /// </summary>
        Left = Scancode.Left | Scancode.Mask,

        /// <summary>
        /// Down key
        /// </summary>
        Down = Scancode.Down | Scancode.Mask,

        /// <summary>
        /// Up key
        /// </summary>
        Up = Scancode.Up | Scancode.Mask,

        /// <summary>
        /// NumLockClear key
        /// </summary>
        NumLockClear = Scancode.NumLockClear | Scancode.Mask,

        /// <summary>
        /// NumPadDivide key
        /// </summary>
        NumPadDivide = Scancode.NumPadDivide | Scancode.Mask,

        /// <summary>
        /// NumPadMultiply key
        /// </summary>
        NumPadMultiply = Scancode.NumPadMultiply | Scancode.Mask,

        /// <summary>
        /// NumPadMinus key
        /// </summary>
        NumPadMinus = Scancode.NumPadMinus | Scancode.Mask,

        /// <summary>
        /// NumPadPlus key
        /// </summary>
        NumPadPlus = Scancode.NumPadPlus | Scancode.Mask,

        /// <summary>
        /// NumPadEnter key
        /// </summary>
        NumPadEnter = Scancode.NumPadEnter | Scancode.Mask,

        /// <summary>
        /// NumPad1 key
        /// </summary>
        NumPad1 = Scancode.NumPad1 | Scancode.Mask,

        /// <summary>
        /// NumPad2 key
        /// </summary>
        NumPad2 = Scancode.NumPad2 | Scancode.Mask,

        /// <summary>
        /// NumPad3 key
        /// </summary>
        NumPad3 = Scancode.NumPad3 | Scancode.Mask,

        /// <summary>
        /// NumPad4 key
        /// </summary>
        NumPad4 = Scancode.NumPad4 | Scancode.Mask,

        /// <summary>
        /// NumPad5 key
        /// </summary>
        NumPad5 = Scancode.NumPad5 | Scancode.Mask,

        /// <summary>
        /// NumPad6 key
        /// </summary>
        NumPad6 = Scancode.NumPad6 | Scancode.Mask,

        /// <summary>
        /// NumPad7 key
        /// </summary>
        NumPad7 = Scancode.NumPad7 | Scancode.Mask,

        /// <summary>
        /// NumPad8 key
        /// </summary>
        NumPad8 = Scancode.NumPad8 | Scancode.Mask,

        /// <summary>
        /// NumPad9 key
        /// </summary>
        NumPad9 = Scancode.NumPad9 | Scancode.Mask,

        /// <summary>
        /// NumPad0 key
        /// </summary>
        NumPad0 = Scancode.NumPad0 | Scancode.Mask,

        /// <summary>
        /// NumPadPeriod key
        /// </summary>
        NumPadPeriod = Scancode.NumPadPeriod | Scancode.Mask,

        /// <summary>
        /// Application key
        /// </summary>
        Application = Scancode.Application | Scancode.Mask,

        /// <summary>
        /// Power key
        /// </summary>
        Power = Scancode.Power | Scancode.Mask,

        /// <summary>
        /// NumPadEquals key
        /// </summary>
        NumPadEquals = Scancode.NumPadEquals | Scancode.Mask,

        /// <summary>
        /// F13 key
        /// </summary>
        F13 = Scancode.F13 | Scancode.Mask,

        /// <summary>
        /// F14 key
        /// </summary>
        F14 = Scancode.F14 | Scancode.Mask,

        /// <summary>
        /// F15 key
        /// </summary>
        F15 = Scancode.F15 | Scancode.Mask,

        /// <summary>
        /// F16 key
        /// </summary>
        F16 = Scancode.F16 | Scancode.Mask,

        /// <summary>
        /// F17 key
        /// </summary>
        F17 = Scancode.F17 | Scancode.Mask,

        /// <summary>
        /// F18 key
        /// </summary>
        F18 = Scancode.F18 | Scancode.Mask,

        /// <summary>
        /// F19 key
        /// </summary>
        F19 = Scancode.F19 | Scancode.Mask,

        /// <summary>
        /// F20 key
        /// </summary>
        F20 = Scancode.F20 | Scancode.Mask,

        /// <summary>
        /// F21 key
        /// </summary>
        F21 = Scancode.F21 | Scancode.Mask,

        /// <summary>
        /// F22 key
        /// </summary>
        F22 = Scancode.F22 | Scancode.Mask,

        /// <summary>
        /// F23 key
        /// </summary>
        F23 = Scancode.F23 | Scancode.Mask,

        /// <summary>
        /// F24 key
        /// </summary>
        F24 = Scancode.F24 | Scancode.Mask,

        /// <summary>
        /// Execute key
        /// </summary>
        Execute = Scancode.Execute | Scancode.Mask,

        /// <summary>
        /// Help key
        /// </summary>
        Help = Scancode.Help | Scancode.Mask,

        /// <summary>
        /// Menu key
        /// </summary>
        Menu = Scancode.Menu | Scancode.Mask,

        /// <summary>
        /// Select key
        /// </summary>
        Select = Scancode.Select | Scancode.Mask,

        /// <summary>
        /// Stop key
        /// </summary>
        Stop = Scancode.Stop | Scancode.Mask,

        /// <summary>
        /// Again key
        /// </summary>
        Again = Scancode.Again | Scancode.Mask,

        /// <summary>
        /// Undo key
        /// </summary>
        Undo = Scancode.Undo | Scancode.Mask,

        /// <summary>
        /// Cut key
        /// </summary>
        Cut = Scancode.Cut | Scancode.Mask,

        /// <summary>
        /// Copy key
        /// </summary>
        Copy = Scancode.Copy | Scancode.Mask,

        /// <summary>
        /// Paste key
        /// </summary>
        Paste = Scancode.Paste | Scancode.Mask,

        /// <summary>
        /// Find key
        /// </summary>
        Find = Scancode.Find | Scancode.Mask,

        /// <summary>
        /// Mute key
        /// </summary>
        Mute = Scancode.Mute | Scancode.Mask,

        /// <summary>
        /// VolumeUp key
        /// </summary>
        VolumeUp = Scancode.VolumeUp | Scancode.Mask,

        /// <summary>
        /// VolumeDown key
        /// </summary>
        VolumeDown = Scancode.VolumeDown | Scancode.Mask,

        /// <summary>
        /// NumPadComma key
        /// </summary>
        NumPadComma = Scancode.NumPadComma | Scancode.Mask,

        /// <summary>
        /// NumPadEqualsAS400 key
        /// </summary>
        NumPadEqualsAS400 = Scancode.NumPadEqualsAS400 | Scancode.Mask,

        /// <summary>
        /// AltErase key
        /// </summary>
        AltErase = Scancode.AltErase | Scancode.Mask,

        /// <summary>
        /// SysReq key
        /// </summary>
        SysReq = Scancode.SysReq | Scancode.Mask,

        /// <summary>
        /// Cancel key
        /// </summary>
        Cancel = Scancode.Cancel | Scancode.Mask,

        /// <summary>
        /// Clear key
        /// </summary>
        Clear = Scancode.Clear | Scancode.Mask,

        /// <summary>
        /// Prior key
        /// </summary>
        Prior = Scancode.Prior | Scancode.Mask,

        /// <summary>
        /// Return2 key
        /// </summary>
        Return2 = Scancode.Return2 | Scancode.Mask,

        /// <summary>
        /// Separator key
        /// </summary>
        Separator = Scancode.Separator | Scancode.Mask,

        /// <summary>
        /// Out key
        /// </summary>
        Out = Scancode.Out | Scancode.Mask,

        /// <summary>
        /// Oper key
        /// </summary>
        Oper = Scancode.Oper | Scancode.Mask,

        /// <summary>
        /// ClearAgain key
        /// </summary>
        ClearAgain = Scancode.ClearAgain | Scancode.Mask,

        /// <summary>
        /// CrSel key
        /// </summary>
        CrSel = Scancode.CrSel | Scancode.Mask,

        /// <summary>
        /// ExSel key
        /// </summary>
        ExSel = Scancode.ExSel | Scancode.Mask,

        /// <summary>
        /// NumPad00 key
        /// </summary>
        NumPad00 = Scancode.NumPad00 | Scancode.Mask,

        /// <summary>
        /// NumPad000 key
        /// </summary>
        NumPad000 = Scancode.NumPad000 | Scancode.Mask,

        /// <summary>
        /// ThousandsSeparator key
        /// </summary>
        ThousandsSeparator = Scancode.ThousandsSeparator | Scancode.Mask,

        /// <summary>
        /// DecimalSeparator key
        /// </summary>
        DecimalSeparator = Scancode.DecimalSeparator | Scancode.Mask,

        /// <summary>
        /// CurrencyUnit key
        /// </summary>
        CurrencyUnit = Scancode.CurrencyUnit | Scancode.Mask,

        /// <summary>
        /// CurrencySubunit key
        /// </summary>
        CurrencySubunit = Scancode.CurrencySubunit | Scancode.Mask,

        /// <summary>
        /// NumPadLeftParen key
        /// </summary>
        NumPadLeftParen = Scancode.NumPadLeftParen | Scancode.Mask,

        /// <summary>
        /// NumPadRightParen key
        /// </summary>
        NumPadRightParen = Scancode.NumPadRightParen | Scancode.Mask,

        /// <summary>
        /// NumPadLeftBrace key
        /// </summary>
        NumPadLeftBrace = Scancode.NumPadLeftBrace | Scancode.Mask,

        /// <summary>
        /// NumPadRightBrace key
        /// </summary>
        NumPadRightBrace = Scancode.NumPadRightBrace | Scancode.Mask,

        /// <summary>
        /// NumPadTab key
        /// </summary>
        NumPadTab = Scancode.NumPadTab | Scancode.Mask,

        /// <summary>
        /// NumPadBackspace key
        /// </summary>
        NumPadBackspace = Scancode.NumPadBackspace | Scancode.Mask,

        /// <summary>
        /// NumPadA key
        /// </summary>
        NumPadA = Scancode.NumPadA | Scancode.Mask,

        /// <summary>
        /// NumPadB key
        /// </summary>
        NumPadB = Scancode.NumPadB | Scancode.Mask,

        /// <summary>
        /// NumPadC key
        /// </summary>
        NumPadC = Scancode.NumPadC | Scancode.Mask,

        /// <summary>
        /// NumPadD key
        /// </summary>
        NumPadD = Scancode.NumPadD | Scancode.Mask,

        /// <summary>
        /// NumPadE key
        /// </summary>
        NumPadE = Scancode.NumPadE | Scancode.Mask,

        /// <summary>
        /// NumPadF key
        /// </summary>
        NumPadF = Scancode.NumPadF | Scancode.Mask,

        /// <summary>
        /// NumPadXor key
        /// </summary>
        NumPadXor = Scancode.NumPadXor | Scancode.Mask,

        /// <summary>
        /// NumPadPower key
        /// </summary>
        NumPadPower = Scancode.NumPadPower | Scancode.Mask,

        /// <summary>
        /// NumPadPercent key
        /// </summary>
        NumPadPercent = Scancode.NumPadPercent | Scancode.Mask,

        /// <summary>
        /// NumPadLess key
        /// </summary>
        NumPadLess = Scancode.NumPadLess | Scancode.Mask,

        /// <summary>
        /// NumPadGreater key
        /// </summary>
        NumPadGreater = Scancode.NumPadGreater | Scancode.Mask,

        /// <summary>
        /// NumPadAmpersand key
        /// </summary>
        NumPadAmpersand = Scancode.NumPadAmpersand | Scancode.Mask,

        /// <summary>
        /// NumPadDoubleAmpersand key
        /// </summary>
        NumPadDoubleAmpersand = Scancode.NumPadDoubleAmpersand | Scancode.Mask,

        /// <summary>
        /// NumPadVerticalBar key
        /// </summary>
        NumPadVerticalBar = Scancode.NumPadVerticalBar | Scancode.Mask,

        /// <summary>
        /// NumPadDoubleVerticalBar key
        /// </summary>
        NumPadDoubleVerticalBar = Scancode.NumPadDoubleVerticalBar | Scancode.Mask,

        /// <summary>
        /// NumPadColon key
        /// </summary>
        NumPadColon = Scancode.NumPadColon | Scancode.Mask,

        /// <summary>
        /// NumPadHash key
        /// </summary>
        NumPadHash = Scancode.NumPadHash | Scancode.Mask,

        /// <summary>
        /// NumPadSpace key
        /// </summary>
        NumPadSpace = Scancode.NumPadSpace | Scancode.Mask,

        /// <summary>
        /// NumPadAt key
        /// </summary>
        NumPadAt = Scancode.NumPadAt | Scancode.Mask,

        /// <summary>
        /// NumPadExclamation key
        /// </summary>
        NumPadExclamation = Scancode.NumPadExclamation | Scancode.Mask,

        /// <summary>
        /// NumPadMemStore key
        /// </summary>
        NumPadMemStore = Scancode.NumPadMemStore | Scancode.Mask,

        /// <summary>
        /// NumPadMemRecall key
        /// </summary>
        NumPadMemRecall = Scancode.NumPadMemRecall | Scancode.Mask,

        /// <summary>
        /// NumPadMemClear key
        /// </summary>
        NumPadMemClear = Scancode.NumPadMemClear | Scancode.Mask,

        /// <summary>
        /// NumPadMemAdd key
        /// </summary>
        NumPadMemAdd = Scancode.NumPadMemAdd | Scancode.Mask,

        /// <summary>
        /// NumPadMemSubtract key
        /// </summary>
        NumPadMemSubtract = Scancode.NumPadMemSubtract | Scancode.Mask,

        /// <summary>
        /// NumPadMemMultiply key
        /// </summary>
        NumPadMemMultiply = Scancode.NumPadMemMultiply | Scancode.Mask,

        /// <summary>
        /// NumPadMemDivide key
        /// </summary>
        NumPadMemDivide = Scancode.NumPadMemDivide | Scancode.Mask,

        /// <summary>
        /// NumPadPlusMinus key
        /// </summary>
        NumPadPlusMinus = Scancode.NumPadPlusMinus | Scancode.Mask,

        /// <summary>
        /// NumPadClear key
        /// </summary>
        NumPadClear = Scancode.NumPadClear | Scancode.Mask,

        /// <summary>
        /// NumPadClearEntry key
        /// </summary>
        NumPadClearEntry = Scancode.NumPadClearEntry | Scancode.Mask,

        /// <summary>
        /// NumPadBinary key
        /// </summary>
        NumPadBinary = Scancode.NumPadBinary | Scancode.Mask,

        /// <summary>
        /// NumPadOctal key
        /// </summary>
        NumPadOctal = Scancode.NumPadOctal | Scancode.Mask,

        /// <summary>
        /// NumPadDecimal key
        /// </summary>
        NumPadDecimal = Scancode.NumPadDecimal | Scancode.Mask,

        /// <summary>
        /// NumPadHexadecimal key
        /// </summary>
        NumPadHexadecimal = Scancode.NumPadHexadecimal | Scancode.Mask,

        /// <summary>
        /// LeftCtrl key
        /// </summary>
        LeftCtrl = Scancode.LeftCtrl | Scancode.Mask,

        /// <summary>
        /// LeftShift key
        /// </summary>
        LeftShift = Scancode.LeftShift | Scancode.Mask,

        /// <summary>
        /// LeftAlt key
        /// </summary>
        LeftAlt = Scancode.LeftAlt | Scancode.Mask,

        /// <summary>
        /// LeftGui key
        /// </summary>
        LeftGui = Scancode.LeftGui | Scancode.Mask,

        /// <summary>
        /// RightCtrl key
        /// </summary>
        RightCtrl = Scancode.RightCtrl | Scancode.Mask,

        /// <summary>
        /// RightShift key
        /// </summary>
        RightShift = Scancode.RightShift | Scancode.Mask,

        /// <summary>
        /// RightAlt key
        /// </summary>
        RightAlt = Scancode.RightAlt | Scancode.Mask,

        /// <summary>
        /// RightGui key
        /// </summary>
        RightGui = Scancode.RightGui | Scancode.Mask,

        /// <summary>
        /// Mode key
        /// </summary>
        Mode = Scancode.Mode | Scancode.Mask,

        /// <summary>
        /// AudioNext key
        /// </summary>
        AudioNext = Scancode.AudioNext | Scancode.Mask,

        /// <summary>
        /// AudioPrev key
        /// </summary>
        AudioPrev = Scancode.AudioPrev | Scancode.Mask,

        /// <summary>
        /// AudioStop key
        /// </summary>
        AudioStop = Scancode.AudioStop | Scancode.Mask,

        /// <summary>
        /// AudioPlay key
        /// </summary>
        AudioPlay = Scancode.AudioPlay | Scancode.Mask,

        /// <summary>
        /// AudioMute key
        /// </summary>
        AudioMute = Scancode.AudioMute | Scancode.Mask,

        /// <summary>
        /// MediaSelect key
        /// </summary>
        MediaSelect = Scancode.MediaSelect | Scancode.Mask,

        /// <summary>
        /// WWW key
        /// </summary>
        WWW = Scancode.WWW | Scancode.Mask,

        /// <summary>
        /// Mail key
        /// </summary>
        Mail = Scancode.Mail | Scancode.Mask,

        /// <summary>
        /// Calculator key
        /// </summary>
        Calculator = Scancode.Calculator | Scancode.Mask,

        /// <summary>
        /// Computer key
        /// </summary>
        Computer = Scancode.Computer | Scancode.Mask,

        /// <summary>
        /// ApplicationSearch key
        /// </summary>
        ApplicationSearch = Scancode.ApplicationSearch | Scancode.Mask,

        /// <summary>
        /// ApplicationHome key
        /// </summary>
        ApplicationHome = Scancode.ApplicationHome | Scancode.Mask,

        /// <summary>
        /// ApplicationBack key
        /// </summary>
        ApplicationBack = Scancode.ApplicationBack | Scancode.Mask,

        /// <summary>
        /// ApplicationForward key
        /// </summary>
        ApplicationForward = Scancode.ApplicationForward | Scancode.Mask,

        /// <summary>
        /// ApplicationStop key
        /// </summary>
        ApplicationStop = Scancode.ApplicationStop | Scancode.Mask,

        /// <summary>
        /// ApplicationRefresh key
        /// </summary>
        ApplicationRefresh = Scancode.ApplicationRefresh | Scancode.Mask,

        /// <summary>
        /// ApplicationBookmarks key
        /// </summary>
        ApplicationBookmarks = Scancode.ApplicationBookmarks | Scancode.Mask,

        /// <summary>
        /// BrightnessDown key
        /// </summary>
        BrightnessDown = Scancode.BrightnessDown | Scancode.Mask,

        /// <summary>
        /// BrightnessUp key
        /// </summary>
        BrightnessUp = Scancode.BrightnessUp | Scancode.Mask,

        /// <summary>
        /// DisplaySwitch key
        /// </summary>
        DisplaySwitch = Scancode.DisplaySwitch | Scancode.Mask,

        /// <summary>
        /// KeyboardIlluminationToggle key
        /// </summary>
        KeyboardIlluminationToggle = Scancode.KeyboardIlluminationToggle | Scancode.Mask,

        /// <summary>
        /// KeyboardIlluminationDown key
        /// </summary>
        KeyboardIlluminationDown = Scancode.KeyboardIlluminationDown | Scancode.Mask,

        /// <summary>
        /// KeyboardIlluminationUp key
        /// </summary>
        KeyboardIlluminationUp = Scancode.KeyboardIlluminationUp | Scancode.Mask,

        /// <summary>
        /// Eject key
        /// </summary>
        Eject = Scancode.Eject | Scancode.Mask,

        /// <summary>
        /// Sleep key
        /// </summary>
        Sleep = Scancode.Sleep | Scancode.Mask,

        /// <summary>
        /// App1 key
        /// </summary>
        App1 = Scancode.App1 | Scancode.Mask,

        /// <summary>
        /// App2 key
        /// </summary>
        App2 = Scancode.App2 | Scancode.Mask,

        /// <summary>
        /// AudioRewind key
        /// </summary>
        AudioRewind = Scancode.AudioRewind | Scancode.Mask,

        /// <summary>
        /// AudioFastForward key
        /// </summary>
        AudioFastForward = Scancode.AudioFastForward | Scancode.Mask
    }
}
