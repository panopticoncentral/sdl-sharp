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
        Constant,

        /// <summary>
        /// Sine wave-shaped effect.
        /// </summary>
        Sine,

        /// <summary>
        /// An effect that explicitly controls the large and small motors.
        /// </summary>
        LeftRight,

        /// <summary>
        /// Triangle wave-shaped effect.
        /// </summary>
        Triangle,

        /// <summary>
        /// Sawtooth up wave-shaped effect.
        /// </summary>
        SawToothUp,

        /// <summary>
        /// Sawtooth down wave-shaped effect.
        /// </summary>
        SawToothDown,

        /// <summary>
        /// A linear ramp effect.
        /// </summary>
        Ramp,

        /// <summary>
        /// An effect based on axes position.
        /// </summary>
        Spring,

        /// <summary>
        /// An effect based on axes velocity.
        /// </summary>
        Damper,

        /// <summary>
        /// An effect based on axes acceleration.
        /// </summary>
        Inertia,

        /// <summary>
        /// An effect based on axes movement.
        /// </summary>
        Friction,

        /// <summary>
        /// A custom effect.
        /// </summary>
        Custom
    }
}
