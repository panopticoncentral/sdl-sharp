using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SdlSharp.Sound
{
    /// <summary>
    /// A source of audio data.
    /// </summary>
    public abstract class AudioSource : IDisposable
    {
        private static Dictionary<nint, AudioSource>? s_audioCallbacks;

        private static Dictionary<nint, AudioSource> AudioCallbacks => s_audioCallbacks ??= new();

        /// <summary>
        /// Constructs a new audio source.
        /// </summary>
        protected AudioSource()
        {
            AudioCallbacks[GetHashCode()] = this;
        }

        /// <summary>
        /// Gets audio data.
        /// </summary>
        /// <param name="data">Storage for the data.</param>
        protected abstract void GetData(Span<byte> data);

        [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        internal static unsafe void AudioCallback(nint userData, byte* stream, int len)
        {
            if (AudioCallbacks.TryGetValue(userData, out var instance))
            {
                instance.GetData(new Span<byte>(stream, len));
            }
        }

        /// <summary>
        /// Disposes the audio source.
        /// </summary>
        public virtual void Dispose()
        {
            _ = AudioCallbacks.Remove(GetHashCode());
            GC.SuppressFinalize(this);
        }
    }
}
