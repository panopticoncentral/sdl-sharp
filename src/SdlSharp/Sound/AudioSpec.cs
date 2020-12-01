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
        public readonly nint Userdata { get; }

        /// <summary>
        /// Creates a new audio specification.
        /// </summary>
        /// <param name="frequency">The frequency.</param>
        /// <param name="format">The audio format.</param>
        /// <param name="channels">The number of channels.</param>
        /// <param name="silence">The silence value.</param>
        /// <param name="samples">The number of samples.</param>
        /// <param name="size">The size.</param>
        /// <param name="callback">The callback.</param>
        /// <param name="userdata">The user data.</param>
        public AudioSpec(int frequency, AudioFormat format, byte channels, byte silence, ushort samples, uint size, AudioCallback? callback, nint userdata)
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

        /// <summary>
        /// Creates a new audiospecification.
        /// </summary>
        /// <param name="spec">The audio specification.</param>
        public AudioSpec(Native.SDL_AudioSpec spec)
        {
            Frequency = spec.Frequency;
            Format = spec.Format;
            Channels = spec.Channels;
            Silence = spec.Silence;
            Samples = spec.Samples;
            Size = spec.Size;
            Callback = spec.Callback == null ? null : new ReverseCallbackWrapper(spec.Callback).Callback;
            Userdata = spec.Userdata;
        }

        /// <summary>
        /// Converts the audio specification to its native form.
        /// </summary>
        /// <returns>The native audio specification.</returns>
        public unsafe Native.SDL_AudioSpec ToNative() =>
            new(
                Frequency,
                Format,
                Channels,
                Silence,
                Samples,
                Size,
                Callback == null ? null : new CallbackWrapper(Callback).Callback,
                Userdata);

        private sealed unsafe class CallbackWrapper
        {
            private readonly AudioCallback _callback;

            public CallbackWrapper(AudioCallback callback)
            {
                _callback = callback;
            }

            public void Callback(nint userdata, byte* stream, int length) => _callback(new Span<byte>(stream, length), userdata);
        }

        private sealed unsafe class ReverseCallbackWrapper
        {
            private readonly Native.SDL_AudioCallback _callback;

            public ReverseCallbackWrapper(Native.SDL_AudioCallback callback)
            {
                _callback = callback;
            }

            public void Callback(Span<byte> stream, nint userdata)
            {
                fixed (byte* streamBuffer = stream)
                {
                    _callback(userdata, streamBuffer, stream.Length);
                }
            }
        }
    }
}
