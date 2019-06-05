using System;
using System.Collections.Generic;

namespace SdlSharp.Sound
{
    /// <summary>
    /// The base class of a mixer effect.
    /// </summary>
    public abstract class MixEffect
    {
        private static readonly Dictionary<MixEffect, int> s_instances = new Dictionary<MixEffect, int>();

        private readonly Native.MixEffectDelegate _effect;
        private readonly Native.MixEffectDoneDelegate _done;

        protected virtual IntPtr UserData { get; }

        protected MixEffect()
        {
            _effect = new Native.MixEffectDelegate(Effect);
            _done = new Native.MixEffectDoneDelegate(DoneWrapper);
        }

        protected virtual void Effect(int channel, IntPtr stream, int length, IntPtr userData)
        {
        }

        private void DoneWrapper(int channel, IntPtr userData)
        {
            Done(channel, userData);

            if (s_instances.TryGetValue(this, out var refCount))
            {
                if (refCount == 1)
                {
                    s_instances.Remove(this);
                }
                else
                {
                    s_instances[this] = refCount - 1;
                }
            }
        }

        protected virtual void Done(int channel, IntPtr userData)
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
        public void Unregister(MixChannel channel)
        {
            _ = Native.CheckErrorZero(Native.Mix_UnregisterEffect(channel.Index, _effect));
        }
    }
}
