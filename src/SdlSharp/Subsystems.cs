using System;

namespace SdlSharp
{
    /// <summary>
    /// The SDL subsystems that can be initialized.
    /// </summary>
    [Flags]
    public enum Subsystems
    {
        /// <summary>
        /// No subsystems.
        /// </summary>
        None = 0,

        /// <summary>
        /// The timer subsystem.
        /// </summary>
        Timer = 0x00000001,

        /// <summary>
        /// The audio subsystem.
        /// </summary>
        Audio = 0x00000010,

        /// <summary>
        /// The video subsystem.
        /// </summary>
        Video = 0x00000020,

        /// <summary>
        /// The joystick subsystem.
        /// </summary>
        Joystick = 0x00000200,

        /// <summary>
        /// The haptic subsystem.
        /// </summary>
        Haptic = 0x00001000,

        /// <summary>
        /// The game controller subsystem.
        /// </summary>
        GameController = 0x00002000,

        /// <summary>
        /// The events subsystem.
        /// </summary>
        Events = 0x00004000,

        /// <summary>
        /// The sensor subsystem.
        /// </summary>
        Sensor = 0x00008000,

        /// <summary>
        /// All of the subsystems.
        /// </summary>
        Everything = Timer | Audio | Video | Joystick | Haptic | GameController | Events | Sensor
    }
}
