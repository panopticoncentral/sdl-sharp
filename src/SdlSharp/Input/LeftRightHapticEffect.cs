using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Threading.Channels;

namespace SdlSharp.Input
{
    /// <summary>
    /// A left-right haptic effect.
    /// </summary>
    public sealed class LeftRightHapticEffect : HapticEffect
    {
        /// <summary>
        /// The length of the effect.
        /// </summary>
        public uint Length { get; }

        /// <summary>
        /// The large motor magniture.
        /// </summary>
        public ushort LargeMagnitude { get; }

        /// <summary>
        /// The small motor magnitude.
        /// </summary>
        public ushort SmallMagnitude { get; }

        /// <summary>
        /// A left-right haptic effect.
        /// </summary>
        /// <param name="length">The length of the effect.</param>
        /// <param name="largeMagnitude">The large motor magniture.</param>
        /// <param name="smallMagnitude">The small motor magnitude.</param>
        public LeftRightHapticEffect(uint length, ushort largeMagnitude, ushort smallMagnitude)
        {
            Length = length;
            LargeMagnitude = largeMagnitude;
            SmallMagnitude = smallMagnitude;
        }

        internal override unsafe T NativeCall<T>(NativeHapticAction<T> call)
        {
            Native.SDL_HapticEffect nativeEffect =
                new()
                {
                    leftright = new()
                    {
                        type = Native.SDL_HAPTIC_LEFTRIGHT,
                        length = Length,
                        large_magnitude = LargeMagnitude,
                        small_magnitude = SmallMagnitude
                    }
                };

            return call(&nativeEffect);
        }
    }
}
