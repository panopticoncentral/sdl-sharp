namespace SdlSharp.Sound
{
    /// <summary>
    /// An audio specification.
    /// </summary>
    public readonly struct AudioSpecification
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
        /// Creates an audio specification.
        /// </summary>
        /// <param name="frequency">The frequency.</param>
        /// <param name="format">The audio format.</param>
        /// <param name="channels">The number of channels.</param>
        public AudioSpecification(int frequency, AudioFormat format, byte channels)
        {
            Frequency = frequency;
            Format = format;
            Channels = channels;
            Silence = 0;
            Samples = 0;
            Size = 0;
        }

        internal AudioSpecification(Native.SDL_AudioSpec spec)
        {
            Frequency = spec.freq;
            Format = new(spec.format);
            Channels = spec.channels;
            Silence = spec.silence;
            Samples = spec.samples;
            Size = spec.size;
        }
    }
}
