namespace SdlSharp.Gui;

/// <summary>
/// A key identifier used by ImGui input queries.
/// Named keys start at 512 to leave room for legacy/platform values.
/// Mod flags (<see cref="ModCtrl"/>, <see cref="ModShift"/>, <see cref="ModAlt"/>, <see cref="ModSuper"/>)
/// can be binary-or'd with a regular key to form a key chord (e.g. <c>ModCtrl | S</c>).
/// </summary>
public enum Key
{
    /// <summary>No key.</summary>
    None = 0,

    // --- Keyboard: navigation / editing ---
    /// <summary>Tab key.</summary>
    Tab = 512,
    /// <summary>Left arrow.</summary>
    LeftArrow,
    /// <summary>Right arrow.</summary>
    RightArrow,
    /// <summary>Up arrow.</summary>
    UpArrow,
    /// <summary>Down arrow.</summary>
    DownArrow,
    /// <summary>Page Up.</summary>
    PageUp,
    /// <summary>Page Down.</summary>
    PageDown,
    /// <summary>Home.</summary>
    Home,
    /// <summary>End.</summary>
    End,
    /// <summary>Insert.</summary>
    Insert,
    /// <summary>Delete.</summary>
    Delete,
    /// <summary>Backspace.</summary>
    Backspace,
    /// <summary>Space.</summary>
    Space,
    /// <summary>Enter / Return.</summary>
    Enter,
    /// <summary>Escape.</summary>
    Escape,

    // --- Keyboard: modifier keys ---
    /// <summary>Left Control.</summary>
    LeftCtrl,
    /// <summary>Left Shift.</summary>
    LeftShift,
    /// <summary>Left Alt.</summary>
    LeftAlt,
    /// <summary>Left Super (Windows/Cmd).</summary>
    LeftSuper,
    /// <summary>Right Control.</summary>
    RightCtrl,
    /// <summary>Right Shift.</summary>
    RightShift,
    /// <summary>Right Alt.</summary>
    RightAlt,
    /// <summary>Right Super (Windows/Cmd).</summary>
    RightSuper,
    /// <summary>Menu / Application key.</summary>
    Menu,

    // --- Keyboard: top-row digits ---
    /// <summary>Top-row 0.</summary>
    D0,
    /// <summary>Top-row 1.</summary>
    D1,
    /// <summary>Top-row 2.</summary>
    D2,
    /// <summary>Top-row 3.</summary>
    D3,
    /// <summary>Top-row 4.</summary>
    D4,
    /// <summary>Top-row 5.</summary>
    D5,
    /// <summary>Top-row 6.</summary>
    D6,
    /// <summary>Top-row 7.</summary>
    D7,
    /// <summary>Top-row 8.</summary>
    D8,
    /// <summary>Top-row 9.</summary>
    D9,

    // --- Keyboard: letters ---
    /// <summary>A.</summary>
    A,
    /// <summary>B.</summary>
    B,
    /// <summary>C.</summary>
    C,
    /// <summary>D.</summary>
    D,
    /// <summary>E.</summary>
    E,
    /// <summary>F.</summary>
    F,
    /// <summary>G.</summary>
    G,
    /// <summary>H.</summary>
    H,
    /// <summary>I.</summary>
    I,
    /// <summary>J.</summary>
    J,
    /// <summary>K.</summary>
    K,
    /// <summary>L.</summary>
    L,
    /// <summary>M.</summary>
    M,
    /// <summary>N.</summary>
    N,
    /// <summary>O.</summary>
    O,
    /// <summary>P.</summary>
    P,
    /// <summary>Q.</summary>
    Q,
    /// <summary>R.</summary>
    R,
    /// <summary>S.</summary>
    S,
    /// <summary>T.</summary>
    T,
    /// <summary>U.</summary>
    U,
    /// <summary>V.</summary>
    V,
    /// <summary>W.</summary>
    W,
    /// <summary>X.</summary>
    X,
    /// <summary>Y.</summary>
    Y,
    /// <summary>Z.</summary>
    Z,

    // --- Keyboard: function keys ---
    /// <summary>F1.</summary>
    F1,
    /// <summary>F2.</summary>
    F2,
    /// <summary>F3.</summary>
    F3,
    /// <summary>F4.</summary>
    F4,
    /// <summary>F5.</summary>
    F5,
    /// <summary>F6.</summary>
    F6,
    /// <summary>F7.</summary>
    F7,
    /// <summary>F8.</summary>
    F8,
    /// <summary>F9.</summary>
    F9,
    /// <summary>F10.</summary>
    F10,
    /// <summary>F11.</summary>
    F11,
    /// <summary>F12.</summary>
    F12,
    /// <summary>F13.</summary>
    F13,
    /// <summary>F14.</summary>
    F14,
    /// <summary>F15.</summary>
    F15,
    /// <summary>F16.</summary>
    F16,
    /// <summary>F17.</summary>
    F17,
    /// <summary>F18.</summary>
    F18,
    /// <summary>F19.</summary>
    F19,
    /// <summary>F20.</summary>
    F20,
    /// <summary>F21.</summary>
    F21,
    /// <summary>F22.</summary>
    F22,
    /// <summary>F23.</summary>
    F23,
    /// <summary>F24.</summary>
    F24,

    // --- Keyboard: punctuation ---
    /// <summary>Apostrophe (').</summary>
    Apostrophe,
    /// <summary>Comma (,).</summary>
    Comma,
    /// <summary>Minus (-).</summary>
    Minus,
    /// <summary>Period (.).</summary>
    Period,
    /// <summary>Slash (/).</summary>
    Slash,
    /// <summary>Semicolon (;).</summary>
    Semicolon,
    /// <summary>Equal (=).</summary>
    Equal,
    /// <summary>Left bracket ([).</summary>
    LeftBracket,
    /// <summary>Backslash (\\).</summary>
    Backslash,
    /// <summary>Right bracket (]).</summary>
    RightBracket,
    /// <summary>Grave accent (`).</summary>
    GraveAccent,

    // --- Keyboard: locks / system keys ---
    /// <summary>Caps Lock.</summary>
    CapsLock,
    /// <summary>Scroll Lock.</summary>
    ScrollLock,
    /// <summary>Num Lock.</summary>
    NumLock,
    /// <summary>Print Screen.</summary>
    PrintScreen,
    /// <summary>Pause.</summary>
    Pause,

    // --- Keypad ---
    /// <summary>Keypad 0.</summary>
    Keypad0,
    /// <summary>Keypad 1.</summary>
    Keypad1,
    /// <summary>Keypad 2.</summary>
    Keypad2,
    /// <summary>Keypad 3.</summary>
    Keypad3,
    /// <summary>Keypad 4.</summary>
    Keypad4,
    /// <summary>Keypad 5.</summary>
    Keypad5,
    /// <summary>Keypad 6.</summary>
    Keypad6,
    /// <summary>Keypad 7.</summary>
    Keypad7,
    /// <summary>Keypad 8.</summary>
    Keypad8,
    /// <summary>Keypad 9.</summary>
    Keypad9,
    /// <summary>Keypad decimal.</summary>
    KeypadDecimal,
    /// <summary>Keypad divide.</summary>
    KeypadDivide,
    /// <summary>Keypad multiply.</summary>
    KeypadMultiply,
    /// <summary>Keypad subtract.</summary>
    KeypadSubtract,
    /// <summary>Keypad add.</summary>
    KeypadAdd,
    /// <summary>Keypad Enter.</summary>
    KeypadEnter,
    /// <summary>Keypad equal.</summary>
    KeypadEqual,

    // --- Extended / multimedia ---
    /// <summary>App Back (browser back).</summary>
    AppBack,
    /// <summary>App Forward (browser forward).</summary>
    AppForward,
    /// <summary>OEM 102 (non-US backslash).</summary>
    Oem102,

    // --- Gamepad ---
    /// <summary>Gamepad Start / Menu / Options.</summary>
    GamepadStart,
    /// <summary>Gamepad Back / View / Share.</summary>
    GamepadBack,
    /// <summary>Gamepad face button, left (X / Y / Square).</summary>
    GamepadFaceLeft,
    /// <summary>Gamepad face button, right (B / A / Circle).</summary>
    GamepadFaceRight,
    /// <summary>Gamepad face button, up (Y / X / Triangle).</summary>
    GamepadFaceUp,
    /// <summary>Gamepad face button, down (A / B / Cross).</summary>
    GamepadFaceDown,
    /// <summary>Gamepad D-pad left.</summary>
    GamepadDpadLeft,
    /// <summary>Gamepad D-pad right.</summary>
    GamepadDpadRight,
    /// <summary>Gamepad D-pad up.</summary>
    GamepadDpadUp,
    /// <summary>Gamepad D-pad down.</summary>
    GamepadDpadDown,
    /// <summary>Gamepad L1 / Left Bumper.</summary>
    GamepadL1,
    /// <summary>Gamepad R1 / Right Bumper.</summary>
    GamepadR1,
    /// <summary>Gamepad L2 / Left Trigger (analog).</summary>
    GamepadL2,
    /// <summary>Gamepad R2 / Right Trigger (analog).</summary>
    GamepadR2,
    /// <summary>Gamepad L3 / Left Stick click.</summary>
    GamepadL3,
    /// <summary>Gamepad R3 / Right Stick click.</summary>
    GamepadR3,
    /// <summary>Gamepad left stick, left (analog).</summary>
    GamepadLStickLeft,
    /// <summary>Gamepad left stick, right (analog).</summary>
    GamepadLStickRight,
    /// <summary>Gamepad left stick, up (analog).</summary>
    GamepadLStickUp,
    /// <summary>Gamepad left stick, down (analog).</summary>
    GamepadLStickDown,
    /// <summary>Gamepad right stick, left (analog).</summary>
    GamepadRStickLeft,
    /// <summary>Gamepad right stick, right (analog).</summary>
    GamepadRStickRight,
    /// <summary>Gamepad right stick, up (analog).</summary>
    GamepadRStickUp,
    /// <summary>Gamepad right stick, down (analog).</summary>
    GamepadRStickDown,

    // --- Mouse aliases (mirror io.MouseDown[] / MouseWheel) ---
    /// <summary>Mouse left button (alias).</summary>
    MouseLeft,
    /// <summary>Mouse right button (alias).</summary>
    MouseRight,
    /// <summary>Mouse middle button (alias).</summary>
    MouseMiddle,
    /// <summary>Mouse X1 (side button).</summary>
    MouseX1,
    /// <summary>Mouse X2 (side button).</summary>
    MouseX2,
    /// <summary>Mouse wheel horizontal.</summary>
    MouseWheelX,
    /// <summary>Mouse wheel vertical.</summary>
    MouseWheelY,

    // --- Modifier flags (or-able with keys to form key chords) ---
    /// <summary>Ctrl modifier (Cmd on macOS).</summary>
    ModCtrl = 1 << 12,
    /// <summary>Shift modifier.</summary>
    ModShift = 1 << 13,
    /// <summary>Alt / Option modifier.</summary>
    ModAlt = 1 << 14,
    /// <summary>Super modifier (Windows / Cmd on non-macOS, Ctrl on macOS).</summary>
    ModSuper = 1 << 15,
}
