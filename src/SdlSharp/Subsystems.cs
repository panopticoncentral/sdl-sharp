using System;

namespace SdlSharp
{
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
        /// The timer subsystem.
        /// </summary>
        Timer = Native.SDL_INIT_TIMER,

        /// <summary>
        /// The audio subsystem.
        /// </summary>
        Audio = Native.SDL_INIT_AUDIO,

        /// <summary>
        /// The video subsystem.
        /// </summary>
        Video = Native.SDL_INIT_VIDEO,

        /// <summary>
        /// The joystick subsystem.
        /// </summary>
        Joystick = Native.SDL_INIT_JOYSTICK,

        /// <summary>
        /// The haptic subsystem.
        /// </summary>
        Haptic = Native.SDL_INIT_HAPTIC,

        /// <summary>
        /// The game controller subsystem.
        /// </summary>
        GameController = Native.SDL_INIT_GAMECONTROLLER,

        /// <summary>
        /// The events subsystem.
        /// </summary>
        Events = Native.SDL_INIT_EVENTS,

        /// <summary>
        /// The sensor subsystem.
        /// </summary>
        Sensor = Native.SDL_INIT_SENSOR,

        /// <summary>
        /// All of the subsystems.
        /// </summary>
        Everything = Native.SDL_INIT_EVERYTHING
    }
}
