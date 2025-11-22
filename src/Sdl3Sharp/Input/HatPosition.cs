using static Sdl3Sharp.Native.Joystick;

namespace Sdl3Sharp.Input;

/// <summary>
/// Represents the position of a POV hat on a joystick.
/// </summary>
[Flags]
public enum HatPosition : byte
{
    /// <summary>Hat is centered (no direction).</summary>
    Centered = SDL_HAT_CENTERED,
    /// <summary>Hat is pushed up.</summary>
    Up = SDL_HAT_UP,
    /// <summary>Hat is pushed right.</summary>
    Right = SDL_HAT_RIGHT,
    /// <summary>Hat is pushed down.</summary>
    Down = SDL_HAT_DOWN,
    /// <summary>Hat is pushed left.</summary>
    Left = SDL_HAT_LEFT,
    /// <summary>Hat is pushed right and up.</summary>
    RightUp = SDL_HAT_RIGHTUP,
    /// <summary>Hat is pushed right and down.</summary>
    RightDown = SDL_HAT_RIGHTDOWN,
    /// <summary>Hat is pushed left and up.</summary>
    LeftUp = SDL_HAT_LEFTUP,
    /// <summary>Hat is pushed left and down.</summary>
    LeftDown = SDL_HAT_LEFTDOWN
}
