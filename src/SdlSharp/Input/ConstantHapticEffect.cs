namespace SdlSharp.Input
{
    /// <summary>
    /// A constant haptic effect.
    /// </summary>
    public sealed class ConstantHapticEffect : HapticEffect
    {
        /// <summary>
        /// The direction of the effect.
        /// </summary>
        public HapticDirection Direction { get; }

        /// <summary>
        /// The length of the effect.
        /// </summary>
        public uint Length { get; }

        /// <summary>
        /// The delay before the effect.
        /// </summary>
        public ushort Delay { get; }

        /// <summary>
        /// The button that triggers the effect.
        /// </summary>
        public ushort Button { get; }

        /// <summary>
        /// Minimum interval between effects.
        /// </summary>
        public ushort Interval { get; }

        /// <summary>
        /// The level of the effect.
        /// </summary>
        public short Level { get; }

        /// <summary>
        /// The attach length of the effect.
        /// </summary>
        public ushort AttackLength { get; }

        /// <summary>
        /// The attack level of the effect.
        /// </summary>
        public ushort AttackLevel { get; }

        /// <summary>
        /// The fade length of the effect.
        /// </summary>
        public ushort FadeLength { get; }

        /// <summary>
        /// The fade level of the effect.
        /// </summary>
        public ushort FadeLevel { get; }

        /// <summary>
        /// Constructs a new constant haptic effect.
        /// </summary>
        /// <param name="direction">The direction of the effect.</param>
        /// <param name="length">The length of the effect.</param>
        /// <param name="delay">The delay before the effect.</param>
        /// <param name="button">The button that triggers the effect.</param>
        /// <param name="interval">Minimum interval between effects.</param>
        /// <param name="level">The level of the effect.</param>
        /// <param name="attackLength">The attach length of the effect.</param>
        /// <param name="attackLevel">The attack level of the effect.</param>
        /// <param name="fadeLength">The fade length of the effect.</param>
        /// <param name="fadeLevel">The fade level of the effect.</param>
        public ConstantHapticEffect(HapticDirection direction, uint length, ushort delay, ushort button, ushort interval, short level, ushort attackLength, ushort attackLevel, ushort fadeLength, ushort fadeLevel)
        {
            Direction = direction;
            Length = length;
            Delay = delay;
            Button = button;
            Interval = interval;
            Level = level;
            AttackLength = attackLength;
            AttackLevel = attackLevel;
            FadeLength = fadeLength;
            FadeLevel = fadeLevel;
        }

        internal override unsafe T NativeCall<T>(NativeHapticAction<T> call)
        {
            Native.SDL_HapticEffect nativeEffect =
                new()
                {
                    constant = new()
                    {
                        type = Native.SDL_HAPTIC_CONSTANT,
                        direction = Direction.ToNative(),
                        length = Length,
                        delay = Delay,
                        button = Button,
                        interval = Interval,
                        level = Level,
                        attack_length = AttackLength,
                        attack_level = AttackLevel,
                        fade_length = FadeLength,
                        fade_level = FadeLevel
                    }
                };

            return call(&nativeEffect);
        }
    }
}
