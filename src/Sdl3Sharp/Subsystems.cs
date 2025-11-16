using static Sdl3Sharp.Native.Init;

namespace Sdl3Sharp;

/// <summary>
/// The SDL subsystems that can be initialized.
/// </summary>
[Flags]
public enum Subsystems : uint
{
    /// <summary>
    /// No subsystems.
    /// </summary>
    None = 0,

    /// <summary>
    /// The audio subsystem.
    /// </summary>
    Audio = SDL_InitFlags.SDL_INIT_AUDIO,

    /// <summary>
    /// The video subsystem.
    /// </summary>
    Video = SDL_InitFlags.SDL_INIT_VIDEO,

    /// <summary>
    /// The joystick subsystem.
    /// </summary>
    Joystick = SDL_InitFlags.SDL_INIT_JOYSTICK,

    /// <summary>
    /// The haptic subsystem.
    /// </summary>
    Haptic = SDL_InitFlags.SDL_INIT_HAPTIC,

    /// <summary>
    /// The gamepad subsystem.
    /// </summary>
    GamePad = SDL_InitFlags.SDL_INIT_GAMEPAD,

    /// <summary>
    /// The events subsystem.
    /// </summary>
    Events = SDL_InitFlags.SDL_INIT_EVENTS,

    /// <summary>
    /// The sensor subsystem.
    /// </summary>
    Sensor = SDL_InitFlags.SDL_INIT_SENSOR,

    /// <summary>
    /// The camera subsystem.
    /// </summary>
    Camera = SDL_InitFlags.SDL_INIT_CAMERA
}
