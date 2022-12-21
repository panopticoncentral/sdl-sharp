namespace SdlSharp.Input
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

        private ushort NativeFlags => Type switch
        {
            HapticEffectType.Spring => Native.SDL_HAPTIC_SPRING,

            HapticEffectType.Damper => Native.SDL_HAPTIC_DAMPER,

            HapticEffectType.Inertia => Native.SDL_HAPTIC_INERTIA,

            HapticEffectType.Friction => Native.SDL_HAPTIC_FRICTION,

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

        internal override unsafe T NativeCall<T>(NativeHapticAction<T> call)
        {
            Native.SDL_HapticEffect nativeEffect =
                new()
                {
                    condition = new()
                    {
                        type = NativeFlags,
                        direction = Direction.ToNative(),
                        length = Length,
                        delay = Delay,
                        button = Button,
                        interval = Interval,
                        right_sat0 = RightSat.XAxis,
                        right_sat1 = RightSat.YAxis,
                        right_sat2 = RightSat.ZAxis,
                        left_sat0 = LeftSat.XAxis,
                        left_sat1 = LeftSat.YAxis,
                        left_sat2 = LeftSat.ZAxis,
                        right_coeff0 = RightCoefficient.XAxis,
                        right_coeff1 = RightCoefficient.YAxis,
                        right_coeff2 = RightCoefficient.ZAxis,
                        left_coeff0 = LeftCoefficient.XAxis,
                        left_coeff1 = LeftCoefficient.YAxis,
                        left_coeff2 = LeftCoefficient.ZAxis,
                        deadband0 = Deadband.XAxis,
                        deadband1 = Deadband.YAxis,
                        deadband2 = Deadband.ZAxis,
                        center0 = Center.XAxis,
                        center1 = Center.YAxis,
                        center2 = Center.ZAxis
                    }
                };

            return call(&nativeEffect);
        }
    }
}
