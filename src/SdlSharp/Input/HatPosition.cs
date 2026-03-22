// src/SdlSharp/Input/HatPosition.cs
namespace SdlSharp.Input;

/// <summary>
/// Joystick hat positions.
/// </summary>
[Flags]
public enum HatPosition : byte
{
    Centered = Native.Joystick.SDL_HAT_CENTERED,
    Up = Native.Joystick.SDL_HAT_UP,
    Right = Native.Joystick.SDL_HAT_RIGHT,
    Down = Native.Joystick.SDL_HAT_DOWN,
    Left = Native.Joystick.SDL_HAT_LEFT,
    RightUp = Native.Joystick.SDL_HAT_RIGHTUP,
    RightDown = Native.Joystick.SDL_HAT_RIGHTDOWN,
    LeftUp = Native.Joystick.SDL_HAT_LEFTUP,
    LeftDown = Native.Joystick.SDL_HAT_LEFTDOWN,
}
