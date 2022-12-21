﻿namespace SdlSharp.Input
{
    /// <summary>
    /// A periodic haptic effect.
    /// </summary>
    public sealed class PeriodicHapticEffect : HapticEffect
    {
        /// <summary>
        /// The type of the effect.
        /// </summary>
        public HapticEffectType Type { get; }

        private ushort NativeFlags => Type switch
        {
            HapticEffectType.Sine => Native.SDL_HAPTIC_SINE,

            HapticEffectType.Triangle => Native.SDL_HAPTIC_TRIANGLE,

            HapticEffectType.SawToothUp => Native.SDL_HAPTIC_SAWTOOTHUP,

            HapticEffectType.SawToothDown => Native.SDL_HAPTIC_SAWTOOTHDOWN,

            _ => 0,
        };

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
        /// The period of the effect.
        /// </summary>
        public ushort Period { get; }

        /// <summary>
        /// The magnitude of the effect.
        /// </summary>
        public short Magnitude { get; }

        /// <summary>
        /// Mean value of the wave.
        /// </summary>
        public short Offset { get; }

        /// <summary>
        /// Positive phase shift.
        /// </summary>
        public ushort Phase { get; }

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
        /// Creates a new periodic haptic effect.
        /// </summary>
        /// <param name="type">The type of the effect.</param>
        /// <param name="direction">The direction of the effect.</param>
        /// <param name="length">The length of the effect.</param>
        /// <param name="delay">The delay before the effect.</param>
        /// <param name="button">The button that triggers the effect.</param>
        /// <param name="interval">Minimum interval between effects.</param>
        /// <param name="period">The period of the effect.</param>
        /// <param name="magnitude">The magnitude of the effect.</param>
        /// <param name="offset">Mean value of the wave.</param>
        /// <param name="phase">Positive phase shift.</param>
        /// <param name="attackLength">The attach length of the effect.</param>
        /// <param name="attackLevel">The attack level of the effect.</param>
        /// <param name="fadeLength">The fade length of the effect.</param>
        /// <param name="fadeLevel">The fade level of the effect.</param>
        public PeriodicHapticEffect(HapticEffectType type, HapticDirection direction, uint length, ushort delay, ushort button, ushort interval, ushort period, short magnitude, short offset, ushort phase, ushort attackLength, ushort attackLevel, ushort fadeLength, ushort fadeLevel)
        {
            switch (type)
            {
                case HapticEffectType.Sine:
                case HapticEffectType.Triangle:
                case HapticEffectType.SawToothUp:
                case HapticEffectType.SawToothDown:
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(type));
            }

            Type = type;
            Direction = direction;
            Length = length;
            Delay = delay;
            Button = button;
            Interval = interval;
            Period = period;
            Magnitude = magnitude;
            Offset = offset;
            Phase = phase;
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
                    periodic = new()
                    {
                        type = NativeFlags,
                        direction = Direction.ToNative(),
                        length = Length,
                        delay = Delay,
                        button = Button,
                        interval = Interval,
                        period = Period,
                        magnitude = Magnitude,
                        offset = Offset,
                        phase = Phase,
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
