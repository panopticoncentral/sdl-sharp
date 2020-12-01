using System;

namespace SdlSharp.Touch
{
    /// <summary>
    /// A condition haptic effect.
    /// </summary>
    public sealed class ConditionHapticEffect : HapticEffect
    {
        /// <summary>
        /// The type of the condition effect.
        /// </summary>
        public HapticEffectType Type { get; }

        private Native.SDL_HapticType NativeFlags => Type switch
        {
            HapticEffectType.Spring => Native.SDL_HapticType.Spring,

            HapticEffectType.Damper => Native.SDL_HapticType.Damper,

            HapticEffectType.Inertia => Native.SDL_HapticType.Inertia,

            HapticEffectType.Friction => Native.SDL_HapticType.Friction,

            _ => Native.SDL_HapticType.None,
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
        /// Level when joystick is to the positive side.
        /// </summary>
        public (ushort XAxis, ushort YAxis, ushort ZAxis) RightSat { get; }

        /// <summary>
        /// Level with joystick is to the negative side.
        /// </summary>
        public (ushort XAxis, ushort YAxis, ushort ZAxis) LeftSat { get; }

        /// <summary>
        /// How fast to increase the force towards the positive side.
        /// </summary>
        public (short XAxis, short YAxis, short ZAxis) RightCoefficient { get; }

        /// <summary>
        /// How fast to increase the force towards the negative side.
        /// </summary>
        public (short XAxis, short YAxis, short ZAxis) LeftCoefficient { get; }

        /// <summary>
        /// Size of the dead zone.
        /// </summary>
        public (ushort XAxis, ushort YAxis, ushort ZAxis) Deadband { get; }

        /// <summary>
        /// Position of the dead zone.
        /// </summary>
        public (short XAxis, short YAxis, short ZAxis) Center { get; }

        /// <summary>
        /// Creates a new condition haptic effect.
        /// </summary>
        /// <param name="type">The type of the condition effect.</param>
        /// <param name="direction">The direction of the effect.</param>
        /// <param name="length">The length of the effect.</param>
        /// <param name="delay">The delay before the effect.</param>
        /// <param name="button">The button that triggers the effect.</param>
        /// <param name="interval">Minimum interval between triggers.</param>
        /// <param name="rightSat">Level when joystick is to the positive side.</param>
        /// <param name="leftSat">Level with joystick is to the negative side.</param>
        /// <param name="rightCoefficient">How fast to increase the force towards the positive side.</param>
        /// <param name="leftCoefficient">How fast to increase the force towards the negative side.</param>
        /// <param name="deadband">Size of the dead zone.</param>
        /// <param name="center">Position of the dead zone.</param>
        public ConditionHapticEffect(HapticEffectType type, HapticDirection direction, uint length, ushort delay, ushort button, ushort interval, (ushort XAxis, ushort YAxis, ushort ZAxis) rightSat, (ushort XAxis, ushort YAxis, ushort ZAxis) leftSat, (short XAxis, short YAxis, short ZAxis) rightCoefficient, (short XAxis, short YAxis, short ZAxis) leftCoefficient, (ushort XAxis, ushort YAxis, ushort ZAxis) deadband, (short XAxis, short YAxis, short ZAxis) center)
        {
            switch (type)
            {
                case HapticEffectType.Spring:
                case HapticEffectType.Damper:
                case HapticEffectType.Inertia:
                case HapticEffectType.Friction:
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
            RightSat = rightSat;
            LeftSat = leftSat;
            RightCoefficient = rightCoefficient;
            LeftCoefficient = leftCoefficient;
            Deadband = deadband;
            Center = center;
        }

        internal override Native.SDL_HapticEffect ToNative() =>
            new()
            {
                _condition = new Native.SDL_HapticCondition(NativeFlags, Direction.ToNative(), Length, Delay, Button, Interval, RightSat, LeftSat, RightCoefficient, LeftCoefficient, Deadband, Center)
            };
    }
}
