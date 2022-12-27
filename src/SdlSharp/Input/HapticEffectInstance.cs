namespace SdlSharp.Input
{
    /// <summary>
    /// An instance of a haptic effect.
    /// </summary>
    public sealed unsafe class HapticEffectInstance : IDisposable
    {
        private readonly Native.SDL_Haptic* _haptic;
        private readonly int _index;

        /// <summary>
        /// Whether the effect instance is currently running.
        /// </summary>
        public bool EffectRunning =>
            Native.CheckErrorBool(Native.SDL_HapticGetEffectStatus(_haptic, _index));

        internal HapticEffectInstance(Native.SDL_Haptic* haptic, int index)
        {
            _haptic = haptic;
            _index = index;
        }

        /// <summary>
        /// Updates the effect instance.
        /// </summary>
        /// <param name="effect">The effect to update to.</param>
        public void UpdateEffect(HapticEffect effect) =>
            _ = Native.CheckError(effect.NativeCall(effect => Native.SDL_HapticUpdateEffect(_haptic, _index, effect)));

        /// <summary>
        /// Runs the effect.
        /// </summary>
        /// <param name="iterations">The number of times to run the effect.</param>
        public void RunEffect(uint iterations) =>
            Native.CheckError(Native.SDL_HapticRunEffect(_haptic, _index, iterations));

        /// <summary>
        /// Stops the effect.
        /// </summary>
        public void StopEffect() =>
            Native.CheckError(Native.SDL_HapticStopEffect(_haptic, _index));

        /// <inheritdoc/>
        public void Dispose() => Native.SDL_HapticDestroyEffect(_haptic, _index);
    }
}
