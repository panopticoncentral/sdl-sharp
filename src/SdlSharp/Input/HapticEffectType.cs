namespace SdlSharp.Input
{
    /// <summary>
    /// The type of a haptic effect.
    /// </summary>
    public enum HapticEffectType
    {
        /// <summary>
        /// Applies a constant force in the specified direction to the joystick.
        /// </summary>
        Constant = Native.SDL_HAPTIC_CONSTANT,

        /// <summary>
        /// Sine wave-shaped effect.
        /// </summary>
        Sine = Native.SDL_HAPTIC_SINE,

        /// <summary>
        /// An effect that explicitly controls the large and small motors.
        /// </summary>
        LeftRight = Native.SDL_HAPTIC_LEFTRIGHT,

        /// <summary>
        /// Triangle wave-shaped effect.
        /// </summary>
        Triangle = Native.SDL_HAPTIC_TRIANGLE,

        /// <summary>
        /// Sawtooth up wave-shaped effect.
        /// </summary>
        SawToothUp = Native.SDL_HAPTIC_SAWTOOTHUP,

        /// <summary>
        /// Sawtooth down wave-shaped effect.
        /// </summary>
        SawToothDown = Native.SDL_HAPTIC_SAWTOOTHDOWN,

        /// <summary>
        /// A linear ramp effect.
        /// </summary>
        Ramp = Native.SDL_HAPTIC_RAMP,

        /// <summary>
        /// An effect based on axes position.
        /// </summary>
        Spring = Native.SDL_HAPTIC_SPRING,

        /// <summary>
        /// An effect based on axes velocity.
        /// </summary>
        Damper = Native.SDL_HAPTIC_DAMPER,

        /// <summary>
        /// An effect based on axes acceleration.
        /// </summary>
        Inertia = Native.SDL_HAPTIC_INERTIA,

        /// <summary>
        /// An effect based on axes movement.
        /// </summary>
        Friction = Native.SDL_HAPTIC_FRICTION,

        /// <summary>
        /// A custom effect.
        /// </summary>
        Custom = Native.SDL_HAPTIC_CUSTOM
    }
}
