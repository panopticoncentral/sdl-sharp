namespace SdlSharp.Touch
{
    /// <summary>
    /// An instance of a haptic effect.
    /// </summary>
    public sealed unsafe class HapticEffectInstance : NativeIndexBase<Native.SDL_Haptic, int, HapticEffectInstance>
    {
        /// <summary>
        /// Whether the effect instance is currently running.
        /// </summary>
        public bool EffectRunning =>
            Native.CheckErrorBool(Native.SDL_HapticGetEffectStatus(Pointer, Index));

        /// <summary>
        /// Updates the effect instance.
        /// </summary>
        /// <param name="effect">The effect to update to.</param>
        public void UpdateEffect(HapticEffect effect)
        {
            var nativeEffect = effect.ToNative();
            _ = Native.CheckError(Native.SDL_HapticUpdateEffect(Pointer, Index, in nativeEffect));
        }

        /// <summary>
        /// Runs the effect.
        /// </summary>
        /// <param name="iterations">The number of times to run the effect.</param>
        public void RunEffect(uint iterations) =>
            Native.CheckError(Native.SDL_HapticRunEffect(Pointer, Index, iterations));

        /// <summary>
        /// Stops the effect.
        /// </summary>
        public void StopEffect() =>
            Native.CheckError(Native.SDL_HapticStopEffect(Pointer, Index));

        /// <inheritdoc/>
        public override void Dispose() => Native.SDL_HapticDestroyEffect(Pointer, Index);
    }
}
