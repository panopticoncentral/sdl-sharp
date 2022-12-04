namespace SdlSharp.Sound
{
    /// <summary>
    /// The base class of a mixer effect.
    /// </summary>
    public abstract class MixEffect
    {
        private static readonly Dictionary<MixEffect, int> s_instances = new();

        private readonly Native.MixEffectCallback _effect;
        private readonly Native.MixEffectDoneCallback _done;

        /// <summary>
        /// User data for this effect.
        /// </summary>
        protected virtual nint UserData { get; }

        /// <summary>
        /// Creates a new mix effect.
        /// </summary>
        protected MixEffect()
        {
            _effect = new Native.MixEffectCallback(Effect);
            _done = new Native.MixEffectDoneCallback(DoneWrapper);
        }

        /// <summary>
        /// Runs the effect.
        /// </summary>
        /// <param name="channel">The channel.</param>
        /// <param name="stream">The stream.</param>
        /// <param name="length">The length of the stream.</param>
        /// <param name="userData">The effect's user data.</param>
        protected virtual void Effect(int channel, nint stream, int length, nint userData)
        {
        }

        private void DoneWrapper(int channel, nint userData)
        {
            Done(channel, userData);

            if (s_instances.TryGetValue(this, out var refCount))
            {
                if (refCount == 1)
                {
                    _ = s_instances.Remove(this);
                }
                else
                {
                    s_instances[this] = refCount - 1;
                }
            }
        }

        /// <summary>
        /// Called when the effect is done.
        /// </summary>
        /// <param name="channel">The channel.</param>
        /// <param name="userData">The effect's user data.</param>
        protected virtual void Done(int channel, nint userData)
        {
        }

        /// <summary>
        /// Registers the effect in the channel.
        /// </summary>
        /// <param name="channel">The channel.</param>
        public void Register(MixChannel channel)
        {
            if (!s_instances.TryGetValue(this, out var refCount))
            {
                refCount = 0;
            }

            s_instances[this] = refCount + 1;
            _ = Native.CheckErrorZero(Native.Mix_RegisterEffect(channel.Index, _effect, _done, UserData));
        }

        /// <summary>
        /// Unregisters the effect in the channel.
        /// </summary>
        /// <param name="channel">The channel.</param>
        public void Unregister(MixChannel channel) => _ = Native.CheckErrorZero(Native.Mix_UnregisterEffect(channel.Index, _effect));
    }
}
