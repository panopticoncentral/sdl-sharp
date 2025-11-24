namespace Sdl3Sharp.Input;

/// <summary>
/// The list of buttons available on a gamepad.
/// For controllers that use a diamond pattern for the face buttons, the
/// south/east/west/north buttons correspond to the locations in the diamond pattern.
/// </summary>
public enum GamepadButton
{
    /// <summary>Bottom face button (e.g. Xbox A button).</summary>
    South = 0,
    /// <summary>Right face button (e.g. Xbox B button).</summary>
    East,
    /// <summary>Left face button (e.g. Xbox X button).</summary>
    West,
    /// <summary>Top face button (e.g. Xbox Y button).</summary>
    North,
    /// <summary>Back/Select button.</summary>
    Back,
    /// <summary>Guide/Home button.</summary>
    Guide,
    /// <summary>Start button.</summary>
    Start,
    /// <summary>Left stick button (L3).</summary>
    LeftStick,
    /// <summary>Right stick button (R3).</summary>
    RightStick,
    /// <summary>Left shoulder button (L1/LB).</summary>
    LeftShoulder,
    /// <summary>Right shoulder button (R1/RB).</summary>
    RightShoulder,
    /// <summary>D-pad up.</summary>
    DPadUp,
    /// <summary>D-pad down.</summary>
    DPadDown,
    /// <summary>D-pad left.</summary>
    DPadLeft,
    /// <summary>D-pad right.</summary>
    DPadRight,
    /// <summary>Additional button (e.g. Xbox Series X share button, PS5 microphone button).</summary>
    Misc1,
    /// <summary>Upper or primary paddle, under your right hand (e.g. Xbox Elite paddle P1).</summary>
    RightPaddle1,
    /// <summary>Upper or primary paddle, under your left hand (e.g. Xbox Elite paddle P3).</summary>
    LeftPaddle1,
    /// <summary>Lower or secondary paddle, under your right hand (e.g. Xbox Elite paddle P2).</summary>
    RightPaddle2,
    /// <summary>Lower or secondary paddle, under your left hand (e.g. Xbox Elite paddle P4).</summary>
    LeftPaddle2,
    /// <summary>PS4/PS5 touchpad button.</summary>
    Touchpad,
    /// <summary>Additional button 2.</summary>
    Misc2,
    /// <summary>Additional button 3.</summary>
    Misc3,
    /// <summary>Additional button 4.</summary>
    Misc4,
    /// <summary>Additional button 5.</summary>
    Misc5,
    /// <summary>Additional button 6.</summary>
    Misc6
}
