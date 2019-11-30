using System;

namespace SdlSharp.Sound
{
    /// <summary>
    /// An audio specification.
    /// </summary>
    public readonly struct AudioSpec
    {
        /// <summary>
        /// The frequency.
        /// </summary>
        public readonly int Frequency { get; }

        /// <summary>
        /// The format.
        /// </summary>
        public readonly AudioFormat Format { get; }

        /// <summary>
        /// Number of channels.
        /// </summary>
        public readonly byte Channels { get; }

        /// <summary>
        /// The silence value.
        /// </summary>
        public readonly byte Silence { get; }

        /// <summary>
        /// The number of samples.
        /// </summary>
        public readonly ushort Samples { get; }

        /// <summary>
        /// The size of the stream.
        /// </summary>
        public readonly uint Size { get; }

        /// <summary>
        /// A callback for audio data.
        /// </summary>
        public readonly AudioCallback? Callback { get; }

        /// <summary>
        /// User data to pass to the callback.
        /// </summary>
        public readonly IntPtr Userdata { get; }

        public AudioSpec(int frequency, AudioFormat format, byte channels, byte silence, ushort samples, uint size, AudioCallback? callback, IntPtr userdata)
        {
            Frequency = frequency;
            Format = format;
            Channels = channels;
            Silence = silence;
            Samples = samples;
            Size = size;
            Callback = callback;
            Userdata = userdata;
        }

        public AudioSpec(Native.SDL_AudioSpec spec)
        {
            Frequency = spec.Frequency;
            Format = spec.Format;
            Channels = spec.Channels;
            Silence = spec.Silence;
            Samples = spec.Samples;
            Size = spec.Size;
            Callback = spec.Callback == null ? (AudioCallback?)null : new ReverseCallbackWrapper(spec.Callback).Callback;
            Userdata = spec.Userdata;
        }

        public unsafe Native.SDL_AudioSpec ToNative() =>
            new Native.SDL_AudioSpec(
                Frequency,
                Format,
                Channels,
                Silence,
                Samples,
                Size,
                Callback == null ? (Native.SDL_AudioCallback?)null : new CallbackWrapper(Callback).Callback,
                Userdata);

        private sealed unsafe class CallbackWrapper
        {
            private readonly AudioCallback _callback;

            public CallbackWrapper(AudioCallback callback)
            {
                _callback = callback;
            }

            public void Callback(IntPtr userdata, byte* stream, int length) => _callback(new Span<byte>(stream, length), userdata);
        }

        private sealed unsafe class ReverseCallbackWrapper
        {
            private readonly Native.SDL_AudioCallback _callback;

            public ReverseCallbackWrapper(Native.SDL_AudioCallback callback)
            {
                _callback = callback;
            }

            public void Callback(Span<byte> stream, IntPtr userdata)
            {
                fixed (byte* streamBuffer = stream)
                {
                    _callback(userdata, streamBuffer, stream.Length);
                }
            }
        }
    }
}
