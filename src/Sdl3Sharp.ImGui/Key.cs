using Sdl3Sharp.ImGui.Native;

namespace Sdl3Sharp.ImGui;

/// <summary>
/// A key identifier representing Keyboard, Mouse, and Gamepad values.
/// </summary>
/// <remarks>
/// <para>
/// All named keys are >= 512. Keys value 0 to 511 are unused (legacy native/opaque key values were removed in 1.91.5).
/// </para>
/// <para>
/// Note that "Keys" relate to physical keys and are not the same concept as input "Characters",
/// which are submitted via <see cref="IO.AddInputCharacter"/>.
/// </para>
/// <para>
/// The keyboard key enum values are named after the keys on a standard US keyboard,
/// and on other keyboard types the keys reported may not match the keycaps.
/// </para>
/// </remarks>
public enum Key
{
    /// <summary>
    /// No key.
    /// </summary>
    None = ImGuiKey.None,

    /// <summary>
    /// Tab key.
    /// </summary>
    Tab = ImGuiKey.Tab,

    /// <summary>
    /// Left arrow key.
    /// </summary>
    LeftArrow = ImGuiKey.LeftArrow,

    /// <summary>
    /// Right arrow key.
    /// </summary>
    RightArrow = ImGuiKey.RightArrow,

    /// <summary>
    /// Up arrow key.
    /// </summary>
    UpArrow = ImGuiKey.UpArrow,

    /// <summary>
    /// Down arrow key.
    /// </summary>
    DownArrow = ImGuiKey.DownArrow,

    /// <summary>
    /// Page Up key.
    /// </summary>
    PageUp = ImGuiKey.PageUp,

    /// <summary>
    /// Page Down key.
    /// </summary>
    PageDown = ImGuiKey.PageDown,

    /// <summary>
    /// Home key.
    /// </summary>
    Home = ImGuiKey.Home,

    /// <summary>
    /// End key.
    /// </summary>
    End = ImGuiKey.End,

    /// <summary>
    /// Insert key.
    /// </summary>
    Insert = ImGuiKey.Insert,

    /// <summary>
    /// Delete key.
    /// </summary>
    Delete = ImGuiKey.Delete,

    /// <summary>
    /// Backspace key.
    /// </summary>
    Backspace = ImGuiKey.Backspace,

    /// <summary>
    /// Space key.
    /// </summary>
    Space = ImGuiKey.Space,

    /// <summary>
    /// Enter key.
    /// </summary>
    Enter = ImGuiKey.Enter,

    /// <summary>
    /// Escape key.
    /// </summary>
    Escape = ImGuiKey.Escape,

    /// <summary>
    /// Left Ctrl key.
    /// </summary>
    LeftCtrl = ImGuiKey.LeftCtrl,

    /// <summary>
    /// Left Shift key.
    /// </summary>
    LeftShift = ImGuiKey.LeftShift,

    /// <summary>
    /// Left Alt key.
    /// </summary>
    LeftAlt = ImGuiKey.LeftAlt,

    /// <summary>
    /// Left Super (Windows/Command) key.
    /// </summary>
    LeftSuper = ImGuiKey.LeftSuper,

    /// <summary>
    /// Right Ctrl key.
    /// </summary>
    RightCtrl = ImGuiKey.RightCtrl,

    /// <summary>
    /// Right Shift key.
    /// </summary>
    RightShift = ImGuiKey.RightShift,

    /// <summary>
    /// Right Alt key.
    /// </summary>
    RightAlt = ImGuiKey.RightAlt,

    /// <summary>
    /// Right Super (Windows/Command) key.
    /// </summary>
    RightSuper = ImGuiKey.RightSuper,

    /// <summary>
    /// Menu key.
    /// </summary>
    Menu = ImGuiKey.Menu,

    /// <summary>
    /// 0 key.
    /// </summary>
    Zero = ImGuiKey.Zero,

    /// <summary>
    /// 1 key.
    /// </summary>
    One = ImGuiKey.One,

    /// <summary>
    /// 2 key.
    /// </summary>
    Two = ImGuiKey.Two,

    /// <summary>
    /// 3 key.
    /// </summary>
    Three = ImGuiKey.Three,

    /// <summary>
    /// 4 key.
    /// </summary>
    Four = ImGuiKey.Four,

    /// <summary>
    /// 5 key.
    /// </summary>
    Five = ImGuiKey.Five,

    /// <summary>
    /// 6 key.
    /// </summary>
    Six = ImGuiKey.Six,

    /// <summary>
    /// 7 key.
    /// </summary>
    Seven = ImGuiKey.Seven,

    /// <summary>
    /// 8 key.
    /// </summary>
    Eight = ImGuiKey.Eight,

    /// <summary>
    /// 9 key.
    /// </summary>
    Nine = ImGuiKey.Nine,

    /// <summary>
    /// A key.
    /// </summary>
    A = ImGuiKey.A,

    /// <summary>
    /// B key.
    /// </summary>
    B = ImGuiKey.B,

    /// <summary>
    /// C key.
    /// </summary>
    C = ImGuiKey.C,

    /// <summary>
    /// D key.
    /// </summary>
    D = ImGuiKey.D,

    /// <summary>
    /// E key.
    /// </summary>
    E = ImGuiKey.E,

    /// <summary>
    /// F key.
    /// </summary>
    F = ImGuiKey.F,

    /// <summary>
    /// G key.
    /// </summary>
    G = ImGuiKey.G,

    /// <summary>
    /// H key.
    /// </summary>
    H = ImGuiKey.H,

    /// <summary>
    /// I key.
    /// </summary>
    I = ImGuiKey.I,

    /// <summary>
    /// J key.
    /// </summary>
    J = ImGuiKey.J,

    /// <summary>
    /// K key.
    /// </summary>
    K = ImGuiKey.K,

    /// <summary>
    /// L key.
    /// </summary>
    L = ImGuiKey.L,

    /// <summary>
    /// M key.
    /// </summary>
    M = ImGuiKey.M,

    /// <summary>
    /// N key.
    /// </summary>
    N = ImGuiKey.N,

    /// <summary>
    /// O key.
    /// </summary>
    O = ImGuiKey.O,

    /// <summary>
    /// P key.
    /// </summary>
    P = ImGuiKey.P,

    /// <summary>
    /// Q key.
    /// </summary>
    Q = ImGuiKey.Q,

    /// <summary>
    /// R key.
    /// </summary>
    R = ImGuiKey.R,

    /// <summary>
    /// S key.
    /// </summary>
    S = ImGuiKey.S,

    /// <summary>
    /// T key.
    /// </summary>
    T = ImGuiKey.T,

    /// <summary>
    /// U key.
    /// </summary>
    U = ImGuiKey.U,

    /// <summary>
    /// V key.
    /// </summary>
    V = ImGuiKey.V,

    /// <summary>
    /// W key.
    /// </summary>
    W = ImGuiKey.W,

    /// <summary>
    /// X key.
    /// </summary>
    X = ImGuiKey.X,

    /// <summary>
    /// Y key.
    /// </summary>
    Y = ImGuiKey.Y,

    /// <summary>
    /// Z key.
    /// </summary>
    Z = ImGuiKey.Z,

    /// <summary>
    /// F1 key.
    /// </summary>
    F1 = ImGuiKey.F1,

    /// <summary>
    /// F2 key.
    /// </summary>
    F2 = ImGuiKey.F2,

    /// <summary>
    /// F3 key.
    /// </summary>
    F3 = ImGuiKey.F3,

    /// <summary>
    /// F4 key.
    /// </summary>
    F4 = ImGuiKey.F4,

    /// <summary>
    /// F5 key.
    /// </summary>
    F5 = ImGuiKey.F5,

    /// <summary>
    /// F6 key.
    /// </summary>
    F6 = ImGuiKey.F6,

    /// <summary>
    /// F7 key.
    /// </summary>
    F7 = ImGuiKey.F7,

    /// <summary>
    /// F8 key.
    /// </summary>
    F8 = ImGuiKey.F8,

    /// <summary>
    /// F9 key.
    /// </summary>
    F9 = ImGuiKey.F9,

    /// <summary>
    /// F10 key.
    /// </summary>
    F10 = ImGuiKey.F10,

    /// <summary>
    /// F11 key.
    /// </summary>
    F11 = ImGuiKey.F11,

    /// <summary>
    /// F12 key.
    /// </summary>
    F12 = ImGuiKey.F12,

    /// <summary>
    /// F13 key.
    /// </summary>
    F13 = ImGuiKey.F13,

    /// <summary>
    /// F14 key.
    /// </summary>
    F14 = ImGuiKey.F14,

    /// <summary>
    /// F15 key.
    /// </summary>
    F15 = ImGuiKey.F15,

    /// <summary>
    /// F16 key.
    /// </summary>
    F16 = ImGuiKey.F16,

    /// <summary>
    /// F17 key.
    /// </summary>
    F17 = ImGuiKey.F17,

    /// <summary>
    /// F18 key.
    /// </summary>
    F18 = ImGuiKey.F18,

    /// <summary>
    /// F19 key.
    /// </summary>
    F19 = ImGuiKey.F19,

    /// <summary>
    /// F20 key.
    /// </summary>
    F20 = ImGuiKey.F20,

    /// <summary>
    /// F21 key.
    /// </summary>
    F21 = ImGuiKey.F21,

    /// <summary>
    /// F22 key.
    /// </summary>
    F22 = ImGuiKey.F22,

    /// <summary>
    /// F23 key.
    /// </summary>
    F23 = ImGuiKey.F23,

    /// <summary>
    /// F24 key.
    /// </summary>
    F24 = ImGuiKey.F24,

    /// <summary>
    /// Apostrophe (') key.
    /// </summary>
    Apostrophe = ImGuiKey.Apostrophe,

    /// <summary>
    /// Comma (,) key.
    /// </summary>
    Comma = ImGuiKey.Comma,

    /// <summary>
    /// Minus (-) key.
    /// </summary>
    Minus = ImGuiKey.Minus,

    /// <summary>
    /// Period (.) key.
    /// </summary>
    Period = ImGuiKey.Period,

    /// <summary>
    /// Slash (/) key.
    /// </summary>
    Slash = ImGuiKey.Slash,

    /// <summary>
    /// Semicolon (;) key.
    /// </summary>
    Semicolon = ImGuiKey.Semicolon,

    /// <summary>
    /// Equal (=) key.
    /// </summary>
    Equal = ImGuiKey.Equal,

    /// <summary>
    /// Left bracket ([) key.
    /// </summary>
    LeftBracket = ImGuiKey.LeftBracket,

    /// <summary>
    /// Backslash (\) key.
    /// </summary>
    Backslash = ImGuiKey.Backslash,

    /// <summary>
    /// Right bracket (]) key.
    /// </summary>
    RightBracket = ImGuiKey.RightBracket,

    /// <summary>
    /// Grave accent (`) key.
    /// </summary>
    GraveAccent = ImGuiKey.GraveAccent,

    /// <summary>
    /// Caps Lock key.
    /// </summary>
    CapsLock = ImGuiKey.CapsLock,

    /// <summary>
    /// Scroll Lock key.
    /// </summary>
    ScrollLock = ImGuiKey.ScrollLock,

    /// <summary>
    /// Num Lock key.
    /// </summary>
    NumLock = ImGuiKey.NumLock,

    /// <summary>
    /// Print Screen key.
    /// </summary>
    PrintScreen = ImGuiKey.PrintScreen,

    /// <summary>
    /// Pause key.
    /// </summary>
    Pause = ImGuiKey.Pause,

    /// <summary>
    /// Keypad 0 key.
    /// </summary>
    Keypad0 = ImGuiKey.Keypad0,

    /// <summary>
    /// Keypad 1 key.
    /// </summary>
    Keypad1 = ImGuiKey.Keypad1,

    /// <summary>
    /// Keypad 2 key.
    /// </summary>
    Keypad2 = ImGuiKey.Keypad2,

    /// <summary>
    /// Keypad 3 key.
    /// </summary>
    Keypad3 = ImGuiKey.Keypad3,

    /// <summary>
    /// Keypad 4 key.
    /// </summary>
    Keypad4 = ImGuiKey.Keypad4,

    /// <summary>
    /// Keypad 5 key.
    /// </summary>
    Keypad5 = ImGuiKey.Keypad5,

    /// <summary>
    /// Keypad 6 key.
    /// </summary>
    Keypad6 = ImGuiKey.Keypad6,

    /// <summary>
    /// Keypad 7 key.
    /// </summary>
    Keypad7 = ImGuiKey.Keypad7,

    /// <summary>
    /// Keypad 8 key.
    /// </summary>
    Keypad8 = ImGuiKey.Keypad8,

    /// <summary>
    /// Keypad 9 key.
    /// </summary>
    Keypad9 = ImGuiKey.Keypad9,

    /// <summary>
    /// Keypad decimal key.
    /// </summary>
    KeypadDecimal = ImGuiKey.KeypadDecimal,

    /// <summary>
    /// Keypad divide key.
    /// </summary>
    KeypadDivide = ImGuiKey.KeypadDivide,

    /// <summary>
    /// Keypad multiply key.
    /// </summary>
    KeypadMultiply = ImGuiKey.KeypadMultiply,

    /// <summary>
    /// Keypad subtract key.
    /// </summary>
    KeypadSubtract = ImGuiKey.KeypadSubtract,

    /// <summary>
    /// Keypad add key.
    /// </summary>
    KeypadAdd = ImGuiKey.KeypadAdd,

    /// <summary>
    /// Keypad enter key.
    /// </summary>
    KeypadEnter = ImGuiKey.KeypadEnter,

    /// <summary>
    /// Keypad equal key.
    /// </summary>
    KeypadEqual = ImGuiKey.KeypadEqual,

    /// <summary>
    /// App back key (often referred to as "Browser Back").
    /// </summary>
    AppBack = ImGuiKey.AppBack,

    /// <summary>
    /// App forward key.
    /// </summary>
    AppForward = ImGuiKey.AppForward,

    /// <summary>
    /// OEM 102 key (non-US backslash).
    /// </summary>
    Oem102 = ImGuiKey.Oem102,

    /// <summary>
    /// Gamepad Start button (Menu on Xbox, + on Switch, Options on PlayStation).
    /// </summary>
    GamepadStart = ImGuiKey.GamepadStart,

    /// <summary>
    /// Gamepad Back button (View on Xbox, - on Switch, Share on PlayStation).
    /// </summary>
    GamepadBack = ImGuiKey.GamepadBack,

    /// <summary>
    /// Gamepad Face Left button (X on Xbox, Y on Switch, Square on PlayStation).
    /// Tap to toggle menu, hold for windowing mode.
    /// </summary>
    GamepadFaceLeft = ImGuiKey.GamepadFaceLeft,

    /// <summary>
    /// Gamepad Face Right button (B on Xbox, A on Switch, Circle on PlayStation).
    /// Cancel / Close / Exit.
    /// </summary>
    GamepadFaceRight = ImGuiKey.GamepadFaceRight,

    /// <summary>
    /// Gamepad Face Up button (Y on Xbox, X on Switch, Triangle on PlayStation).
    /// Text Input / On-screen Keyboard.
    /// </summary>
    GamepadFaceUp = ImGuiKey.GamepadFaceUp,

    /// <summary>
    /// Gamepad Face Down button (A on Xbox, B on Switch, Cross on PlayStation).
    /// Activate / Open / Toggle / Tweak.
    /// </summary>
    GamepadFaceDown = ImGuiKey.GamepadFaceDown,

    /// <summary>
    /// Gamepad D-pad Left.
    /// </summary>
    GamepadDpadLeft = ImGuiKey.GamepadDpadLeft,

    /// <summary>
    /// Gamepad D-pad Right.
    /// </summary>
    GamepadDpadRight = ImGuiKey.GamepadDpadRight,

    /// <summary>
    /// Gamepad D-pad Up.
    /// </summary>
    GamepadDpadUp = ImGuiKey.GamepadDpadUp,

    /// <summary>
    /// Gamepad D-pad Down.
    /// </summary>
    GamepadDpadDown = ImGuiKey.GamepadDpadDown,

    /// <summary>
    /// Gamepad L1 (L Bumper on Xbox, L on Switch, L1 on PlayStation).
    /// Tweak Slower / Focus Previous.
    /// </summary>
    GamepadL1 = ImGuiKey.GamepadL1,

    /// <summary>
    /// Gamepad R1 (R Bumper on Xbox, R on Switch, R1 on PlayStation).
    /// Tweak Faster / Focus Next.
    /// </summary>
    GamepadR1 = ImGuiKey.GamepadR1,

    /// <summary>
    /// Gamepad L2 (L Trigger on Xbox, ZL on Switch, L2 on PlayStation). Analog.
    /// </summary>
    GamepadL2 = ImGuiKey.GamepadL2,

    /// <summary>
    /// Gamepad R2 (R Trigger on Xbox, ZR on Switch, R2 on PlayStation). Analog.
    /// </summary>
    GamepadR2 = ImGuiKey.GamepadR2,

    /// <summary>
    /// Gamepad L3 (L Stick press on Xbox, L3 on Switch/PlayStation).
    /// </summary>
    GamepadL3 = ImGuiKey.GamepadL3,

    /// <summary>
    /// Gamepad R3 (R Stick press on Xbox, R3 on Switch/PlayStation).
    /// </summary>
    GamepadR3 = ImGuiKey.GamepadR3,

    /// <summary>
    /// Gamepad Left Stick Left. Analog.
    /// </summary>
    GamepadLStickLeft = ImGuiKey.GamepadLStickLeft,

    /// <summary>
    /// Gamepad Left Stick Right. Analog.
    /// </summary>
    GamepadLStickRight = ImGuiKey.GamepadLStickRight,

    /// <summary>
    /// Gamepad Left Stick Up. Analog.
    /// </summary>
    GamepadLStickUp = ImGuiKey.GamepadLStickUp,

    /// <summary>
    /// Gamepad Left Stick Down. Analog.
    /// </summary>
    GamepadLStickDown = ImGuiKey.GamepadLStickDown,

    /// <summary>
    /// Gamepad Right Stick Left. Analog.
    /// </summary>
    GamepadRStickLeft = ImGuiKey.GamepadRStickLeft,

    /// <summary>
    /// Gamepad Right Stick Right. Analog.
    /// </summary>
    GamepadRStickRight = ImGuiKey.GamepadRStickRight,

    /// <summary>
    /// Gamepad Right Stick Up. Analog.
    /// </summary>
    GamepadRStickUp = ImGuiKey.GamepadRStickUp,

    /// <summary>
    /// Gamepad Right Stick Down. Analog.
    /// </summary>
    GamepadRStickDown = ImGuiKey.GamepadRStickDown,

    /// <summary>
    /// Mouse Left button.
    /// </summary>
    MouseLeft = ImGuiKey.MouseLeft,

    /// <summary>
    /// Mouse Right button.
    /// </summary>
    MouseRight = ImGuiKey.MouseRight,

    /// <summary>
    /// Mouse Middle button.
    /// </summary>
    MouseMiddle = ImGuiKey.MouseMiddle,

    /// <summary>
    /// Mouse X1 button.
    /// </summary>
    MouseX1 = ImGuiKey.MouseX1,

    /// <summary>
    /// Mouse X2 button.
    /// </summary>
    MouseX2 = ImGuiKey.MouseX2,

    /// <summary>
    /// Mouse Wheel X axis.
    /// </summary>
    MouseWheelX = ImGuiKey.MouseWheelX,

    /// <summary>
    /// Mouse Wheel Y axis.
    /// </summary>
    MouseWheelY = ImGuiKey.MouseWheelY,

    /// <summary>
    /// No modifier.
    /// </summary>
    ModNone = ImGuiKey.ImGuiMod_None,

    /// <summary>
    /// Ctrl modifier (non-macOS) or Cmd modifier (macOS).
    /// </summary>
    ModCtrl = ImGuiKey.ImGuiMod_Ctrl,

    /// <summary>
    /// Shift modifier.
    /// </summary>
    ModShift = ImGuiKey.ImGuiMod_Shift,

    /// <summary>
    /// Alt/Option modifier.
    /// </summary>
    ModAlt = ImGuiKey.ImGuiMod_Alt,

    /// <summary>
    /// Windows/Super modifier (non-macOS) or Ctrl modifier (macOS).
    /// </summary>
    ModSuper = ImGuiKey.ImGuiMod_Super
}
