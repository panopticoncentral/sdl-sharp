namespace SdlSharp.Input;

/// <summary>
/// Joystick hat positions.
/// </summary>
[Flags]
public enum HatPosition : byte
{
    /// <summary>Hat is centered (no direction pressed).</summary>
    Centered = Native.Joystick.SDL_HAT_CENTERED,
    /// <summary>Hat is pushed up.</summary>
    Up = Native.Joystick.SDL_HAT_UP,
    /// <summary>Hat is pushed right.</summary>
    Right = Native.Joystick.SDL_HAT_RIGHT,
    /// <summary>Hat is pushed down.</summary>
    Down = Native.Joystick.SDL_HAT_DOWN,
    /// <summary>Hat is pushed left.</summary>
    Left = Native.Joystick.SDL_HAT_LEFT,
    /// <summary>Hat is pushed right and up.</summary>
    RightUp = Native.Joystick.SDL_HAT_RIGHTUP,
    /// <summary>Hat is pushed right and down.</summary>
    RightDown = Native.Joystick.SDL_HAT_RIGHTDOWN,
    /// <summary>Hat is pushed left and up.</summary>
    LeftUp = Native.Joystick.SDL_HAT_LEFTUP,
    /// <summary>Hat is pushed left and down.</summary>
    LeftDown = Native.Joystick.SDL_HAT_LEFTDOWN,
}
