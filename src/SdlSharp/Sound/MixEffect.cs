using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SdlSharp.Sound
{
    /// <summary>
    /// The base class of a mixer effect.
    /// </summary>
    public abstract unsafe class MixEffect
    {
        private static Dictionary<int, MixEffect>? s_effectCallbacks;

        private static Dictionary<int, MixEffect> EffectCallbacks => s_effectCallbacks ??= new();

        /// <summary>
        /// Runs the effect.
        /// </summary>
        /// <param name="stream">The stream.</param>
        protected abstract void Effect(Span<byte> stream);

        /// <summary>
        /// Called when the effect is done.
        /// </summary>
        protected abstract void Done();

        internal void Register(int channel)
        {
            EffectCallbacks[GetHashCode()] = this;
            _ = Native.CheckErrorZero(Native.Mix_RegisterEffect(channel, &EffectFunctionCallback, &EffectDoneCallback, GetHashCode()));
        }

        internal void Unregister(int channel)
        {
            _ = EffectCallbacks.Remove(GetHashCode());
            if (EffectCallbacks.Count == 0)
            {
                _ = Native.CheckErrorZero(Native.Mix_UnregisterEffect(channel, &EffectFunctionCallback));
            }
        }

        [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        internal static unsafe void EffectFunctionCallback(int channel, byte* stream, int len, nuint data)
        {
            if (EffectCallbacks.TryGetValue((int)data, out var instance))
            {
                instance.Effect(new Span<byte>(stream, len));
            }
        }

        [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        internal static unsafe void EffectDoneCallback(int channel, nuint data)
        {
            if (EffectCallbacks.TryGetValue((int)data, out var instance))
            {
                instance.Done();
                _ = EffectCallbacks.Remove(instance.GetHashCode());
            }
        }
    }
}
