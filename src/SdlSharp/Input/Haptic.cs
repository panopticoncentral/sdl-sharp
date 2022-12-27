namespace SdlSharp.Input
{
    /// <summary>
    /// A class that represents a haptic effect.
    /// </summary>
    public sealed unsafe class Haptic : IDisposable
    {
        private readonly Native.SDL_Haptic* _haptic;

        /// <summary>
        /// An infinity value.
        /// </summary>
        public const uint Infinity = uint.MaxValue;

        /// <summary>
        /// The current haptic effects.
        /// </summary>
        public static IReadOnlyCollection<HapticInfo> Haptics =>
            Native.GetIndexedCollection(i => new HapticInfo(i), Native.SDL_NumHaptics);

        /// <summary>
        /// Information about this effect.
        /// </summary>
        public HapticInfo Info => new(Native.SDL_HapticIndex(_haptic));

        /// <summary>
        /// The number of effects that can be stored.
        /// </summary>
        public int MaxEffects =>
            Native.SDL_HapticNumEffects(_haptic);

        /// <summary>
        /// The effects currently playing.
        /// </summary>
        public int EffectsPlaying =>
            Native.SDL_HapticNumEffectsPlaying(_haptic);

        /// <summary>
        /// The capabilities of the haptic device.
        /// </summary>
        public HapticCapabilities Capabilities =>
            (HapticCapabilities)Native.SDL_HapticQuery(_haptic) & HapticCapabilities.All;

        /// <summary>
        /// The number of axes.
        /// </summary>
        public int AxisCount =>
            Native.SDL_HapticNumAxes(_haptic);

        /// <summary>
        /// Whether rumble is supported.
        /// </summary>
        public bool RumbleSupported => Native.CheckErrorBool(Native.SDL_HapticRumbleSupported(_haptic));

        internal Haptic(Native.SDL_Haptic* haptic)
        {
            _haptic = haptic;
        }

        /// <inheritdoc/>
        public void Dispose() => Native.SDL_HapticClose(_haptic);

        /// <summary>
        /// Determines whether the effect is supported.
        /// </summary>
        /// <param name="effect">The effect.</param>
        /// <returns>Whether the effect is supported.</returns>
        public bool EffectSupported(HapticEffect effect) =>
            effect.NativeCall(effect => Native.SDL_HapticEffectSupported(_haptic, effect)) != 0;

        /// <summary>
        /// Creates a new instance of a haptic effect.
        /// </summary>
        /// <param name="effect">The effect.</param>
        /// <returns>The effect instance.</returns>
        public HapticEffectInstance NewEffect(HapticEffect effect) =>
            new(_haptic, Native.CheckError(effect.NativeCall(effect => Native.SDL_HapticNewEffect(_haptic, effect))));

        /// <summary>
        /// Sets the gain.
        /// </summary>
        /// <param name="gain">The gain amount.</param>
        public void SetGain(int gain) =>
            Native.CheckError(Native.SDL_HapticSetGain(_haptic, gain));

        /// <summary>
        /// Sets the global autocenter of the device.
        /// </summary>
        /// <param name="autocenter">The autocenter value.</param>
        public void SetAutocenter(int autocenter) =>
            Native.CheckError(Native.SDL_HapticSetAutocenter(_haptic, autocenter));

        /// <summary>
        /// Pauses the effect.
        /// </summary>
        public void Pause() =>
            Native.CheckError(Native.SDL_HapticPause(_haptic));

        /// <summary>
        /// Unpauses the effect.
        /// </summary>
        public void Unpause() =>
            Native.CheckError(Native.SDL_HapticUnpause(_haptic));

        /// <summary>
        /// Stops all effect instances.
        /// </summary>
        public void StopAll() =>
            Native.CheckError(Native.SDL_HapticStopAll(_haptic));

        /// <summary>
        /// Initializes rumble support.
        /// </summary>
        public void RumbleInit() =>
            Native.CheckError(Native.SDL_HapticRumbleInit(_haptic));

        /// <summary>
        /// Plays a rumble effect.
        /// </summary>
        /// <param name="strength">The strength of the effect.</param>
        /// <param name="length">The length of the effect.</param>
        public void RumblePlay(float strength, uint length) =>
            Native.CheckError(Native.SDL_HapticRumblePlay(_haptic, strength, length));

        /// <summary>
        /// Stops the rumble effect.
        /// </summary>
        public void RumbleStop() =>
            Native.CheckError(Native.SDL_HapticRumbleStop(_haptic));
    }
}
