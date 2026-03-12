namespace SdlSharp;

/// <summary>
/// Subsystem initialization flags for SDL.
/// </summary>
[Flags]
public enum InitFlags : uint
{
    /// <summary>Audio subsystem (implies Events).</summary>
    Audio = Native.SDL_InitFlags.SDL_INIT_AUDIO,

    /// <summary>Video subsystem (implies Events, should be initialized on the main thread).</summary>
    Video = Native.SDL_InitFlags.SDL_INIT_VIDEO,

    /// <summary>Joystick subsystem (implies Events).</summary>
    Joystick = Native.SDL_InitFlags.SDL_INIT_JOYSTICK,

    /// <summary>Haptic (force feedback) subsystem.</summary>
    Haptic = Native.SDL_InitFlags.SDL_INIT_HAPTIC,

    /// <summary>Gamepad subsystem (implies Joystick).</summary>
    Gamepad = Native.SDL_InitFlags.SDL_INIT_GAMEPAD,

    /// <summary>Events subsystem.</summary>
    Events = Native.SDL_InitFlags.SDL_INIT_EVENTS,

    /// <summary>Sensor subsystem (implies Events).</summary>
    Sensor = Native.SDL_InitFlags.SDL_INIT_SENSOR,

    /// <summary>Camera subsystem (implies Events).</summary>
    Camera = Native.SDL_InitFlags.SDL_INIT_CAMERA,
}
