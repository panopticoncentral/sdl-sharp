using System.Collections.Generic;
using SdlSharp.Touch;

namespace SdlSharp
{
    /// <summary>
    /// A class that represents a haptic effect.
    /// </summary>
    public sealed unsafe class Haptic : NativePointerBase<Native.SDL_Haptic, Haptic>
    {
        private static ItemCollection<HapticInfo>? s_hapticInfos;

        /// <summary>
        /// An infinity value.
        /// </summary>
        public const uint Infinity = uint.MaxValue;

        /// <summary>
        /// The current haptic effects.
        /// </summary>
        public static IReadOnlyCollection<HapticInfo> Haptics => s_hapticInfos ??= new ItemCollection<HapticInfo>(
            index => Native.CheckNotNull(HapticInfo.IndexToInstance(index)),
            Native.SDL_NumHaptics);

        /// <summary>
        /// Information about this effect.
        /// </summary>
        public HapticInfo Info =>
            HapticInfo.IndexToInstance(Native.SDL_HapticIndex(Pointer));

        /// <summary>
        /// The number of effects that can be stored.
        /// </summary>
        public int MaxEffects =>
            Native.SDL_HapticNumEffects(Pointer);

        /// <summary>
        /// The effects currently playing.
        /// </summary>
        public int EffectsPlaying =>
            Native.SDL_HapticNumEffectsPlaying(Pointer);

        /// <summary>
        /// The capabilities of the haptic device.
        /// </summary>
        public HapticCapabilities Capabilities =>
            (HapticCapabilities)Native.SDL_HapticQuery(Pointer) & HapticCapabilities.All;

        /// <summary>
        /// The number of axes.
        /// </summary>
        public int AxisCount =>
            Native.SDL_HapticNumAxes(Pointer);

        /// <summary>
        /// Whether rumble is supported.
        /// </summary>
        public bool RumbleSupported => Native.CheckErrorBool(Native.SDL_HapticRumbleSupported(Pointer));

        /// <inheritdoc/>
        public override void Dispose()
        {
            Native.SDL_HapticClose(Pointer);
            base.Dispose();
        }

        /// <summary>
        /// Determines whether the effect is supported.
        /// </summary>
        /// <param name="effect">The effect.</param>
        /// <returns>Whether the effect is supported.</returns>
        public bool EffectSupported(HapticEffect effect)
        {
            var nativeEffect = effect.ToNative();
            return Native.SDL_HapticEffectSupported(Pointer, in nativeEffect);
        }

        /// <summary>
        /// Creates a new instance of a haptic effect.
        /// </summary>
        /// <param name="effect">The effect.</param>
        /// <returns>The effect instance.</returns>
        public HapticEffectInstance NewEffect(HapticEffect effect)
        {
            var nativeEffect = effect.ToNative();
            return HapticEffectInstance.IndexToInstance(Pointer, Native.CheckError(Native.SDL_HapticNewEffect(Pointer, in nativeEffect)));
        }

        /// <summary>
        /// Sets the gain.
        /// </summary>
        /// <param name="gain">The gain amount.</param>
        public void SetGain(int gain) =>
            Native.CheckError(Native.SDL_HapticSetGain(Pointer, gain));

        /// <summary>
        /// Sets the global autocenter of the device.
        /// </summary>
        /// <param name="autocenter">The autocenter value.</param>
        public void SetAutocenter(int autocenter) =>
            Native.CheckError(Native.SDL_HapticSetAutocenter(Pointer, autocenter));

        /// <summary>
        /// Pauses the effect.
        /// </summary>
        public void Pause() =>
            Native.CheckError(Native.SDL_HapticPause(Pointer));

        /// <summary>
        /// Unpauses the effect.
        /// </summary>
        public void Unpause() =>
            Native.CheckError(Native.SDL_HapticUnpause(Pointer));

        /// <summary>
        /// Stops all effect instances.
        /// </summary>
        public void StopAll() =>
            Native.CheckError(Native.SDL_HapticStopAll(Pointer));

        /// <summary>
        /// Initializes rumble support.
        /// </summary>
        public void RumbleInit() =>
            Native.CheckError(Native.SDL_HapticRumbleInit(Pointer));

        /// <summary>
        /// Plays a rumble effect.
        /// </summary>
        /// <param name="strength">The strength of the effect.</param>
        /// <param name="length">The length of the effect.</param>
        public void RumblePlay(float strength, uint length) =>
            Native.CheckError(Native.SDL_HapticRumblePlay(Pointer, strength, length));

        /// <summary>
        /// Stops the rumble effect.
        /// </summary>
        public void RumbleStop() =>
            Native.CheckError(Native.SDL_HapticRumbleStop(Pointer));
    }
}
