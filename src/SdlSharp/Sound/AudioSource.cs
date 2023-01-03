using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SdlSharp.Sound
{
    /// <summary>
    /// A source of audio data.
    /// </summary>
    public sealed class AudioSource : IDisposable
    {
        private static Dictionary<nint, AudioSource>? s_audioCallbacks;

        private static Dictionary<nint, AudioSource> AudioCallbacks => s_audioCallbacks ??= new();

        private readonly AudioCallback _callback;

        /// <summary>
        /// Constructs a new audio source.
        /// </summary>
        /// <param name="callback">The callback for the audio data.</param>
        public AudioSource(AudioCallback callback)
        {
            AudioCallbacks[GetHashCode()] = this;
            _callback = callback;
        }

        [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        internal static unsafe void AudioCallback(nint userData, byte* stream, int len)
        {
            if (AudioCallbacks.TryGetValue(userData, out var instance))
            {
                instance._callback(new Span<byte>(stream, len));
            }
        }

        /// <summary>
        /// Disposes the audio source.
        /// </summary>
        public void Dispose()
        {
            _ = AudioCallbacks.Remove(GetHashCode());
            GC.SuppressFinalize(this);
        }
    }
}
