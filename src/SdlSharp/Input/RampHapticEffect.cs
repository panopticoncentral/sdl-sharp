using System.Numerics;

namespace SdlSharp.Input
{
    /// <summary>
    /// A ramp haptic effect.
    /// </summary>
    public sealed class RampHapticEffect : HapticEffect
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
        /// Minimum interval between triggers.
        /// </summary>
        public ushort Interval { get; }

        /// <summary>
        /// The start value.
        /// </summary>
        public short Start { get; }

        /// <summary>
        /// The end value.
        /// </summary>
        public short End { get; }

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
        /// Creates a new ramp haptic effect.
        /// </summary>
        /// <param name="direction">The direction of the effect.</param>
        /// <param name="length">The length of the effect.</param>
        /// <param name="delay">The delay before the effect.</param>
        /// <param name="button">The button that triggers the effect.</param>
        /// <param name="interval">Minimum interval between effects.</param>
        /// <param name="start">The start value.</param>
        /// <param name="end">The end value.</param>
        /// <param name="attackLength">The attach length of the effect.</param>
        /// <param name="attackLevel">The attack level of the effect.</param>
        /// <param name="fadeLength">The fade length of the effect.</param>
        /// <param name="fadeLevel">The fade level of the effect.</param>
        public RampHapticEffect(HapticDirection direction, uint length, ushort delay, ushort button, ushort interval, short start, short end, ushort attackLength, ushort attackLevel, ushort fadeLength, ushort fadeLevel)
        {
            Direction = direction;
            Length = length;
            Delay = delay;
            Button = button;
            Interval = interval;
            Start = start;
            End = end;
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
                    ramp = new()
                    {
                        type = Native.SDL_HAPTIC_RAMP,
                        direction = Direction.ToNative(),
                        length = Length,
                        delay = Delay,
                        button = Button,
                        interval = Interval,
                        start = Start,
                        end = End,
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
