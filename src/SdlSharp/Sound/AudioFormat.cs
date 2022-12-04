namespace SdlSharp.Sound
{
    /// <summary>
    /// A format for audio data.
    /// </summary>
    public readonly record struct AudioFormat(Native.SDL_AudioFormat Format)
    {
        /// <summary>
        /// No specified audio format.
        /// </summary>
        public static AudioFormat None { get; } = new(new(0));

        /// <summary>
        /// Unsigned 8-bit samples.
        /// </summary>
        public static AudioFormat Unsigned8Bit { get; } = new(Native.AUDIO_U8);

        /// <summary>
        /// Signed 8-bit samples
        /// </summary>
        public static AudioFormat Signed8Bit { get; } = new(Native.AUDIO_S8);

        /// <summary>
        /// Unsigned 16-bit little-endian samples.
        /// </summary>
        public static AudioFormat Unsigned16BitLittleEndian { get; } = new(Native.AUDIO_U16LSB);

        /// <summary>
        /// Signed 16-bit little-endian samples.
        /// </summary>
        public static AudioFormat Signed16BitLittleEndian { get; } = new(Native.AUDIO_S16LSB);

        /// <summary>
        /// Unsigned 16-bit big-endian samples.
        /// </summary>
        public static AudioFormat Unsigned16BitBigEndian { get; } = new(Native.AUDIO_U16MSB);

        /// <summary>
        /// Signed 16-bit big-endian samples.
        /// </summary>
        public static AudioFormat Signed16BitBigEndian { get; } = new(Native.AUDIO_S16MSB);

        /// <summary>
        /// Default unsigned 16-bit sample format (little-endian).
        /// </summary>
        public static AudioFormat Unsigned16Bit { get; } = new(Native.AUDIO_U16LSB);

        /// <summary>
        /// Default signed 16-bit sample format (little-endian).
        /// </summary>
        public static AudioFormat Signed16Bit { get; } = new(Native.AUDIO_S16LSB);

        /// <summary>
        /// Signed 32-bit little-endian sample format.
        /// </summary>
        public static AudioFormat Signed32BitLittleEndian { get; } = new(Native.AUDIO_S32LSB);

        /// <summary>
        /// Signed 32-bit big-endian sample format.
        /// </summary>
        public static AudioFormat Signed32BitBigEndian { get; } = new(Native.AUDIO_S32MSB);

        /// <summary>
        /// Default signed 32-bit sample format (little-endian).
        /// </summary>
        public static AudioFormat Signed32Bit { get; } = new(Native.AUDIO_S32);

        /// <summary>
        /// Float 32-bit little-endian sample format.
        /// </summary>
        public static AudioFormat Float32BitLittleEndian { get; } = new(Native.AUDIO_F32LSB);

        /// <summary>
        /// FLoat 32-bit big-endian sample format.
        /// </summary>
        public static AudioFormat Float32BitBigEndian { get; } = new(Native.AUDIO_F32MSB);

        /// <summary>
        /// Default float 32-bit sample format (little-endian).
        /// </summary>
        public static AudioFormat Float32Bit { get; } = new(Native.AUDIO_F32);

        /// <summary>
        /// Unsigned 16-bit system endian sample format.
        /// </summary>
        public static AudioFormat Unsigned16BitSystem { get; } = new(Native.AUDIO_U16SYS);

        /// <summary>
        /// Signed 16-bit system endian sample format.
        /// </summary>
        public static AudioFormat Signed16BitSystem { get; } = new(Native.AUDIO_S16SYS);

        /// <summary>
        /// Unsigned 32-bit system endian sample format.
        /// </summary>
        public static AudioFormat Signed32BitSystem { get; } = new(Native.AUDIO_S32SYS);

        /// <summary>
        /// Float 32-bit system endian sample format.
        /// </summary>
        public static AudioFormat Float32BitSystem { get; } = new(Native.AUDIO_F32SYS);

        /// <summary>
        /// The bit size of the format.
        /// </summary>
        public readonly byte Bitsize => Native.SDL_AUDIO_BITSIZE(Format);

        /// <summary>
        /// Whether the format is integers.
        /// </summary>
        public readonly bool IsInt => Native.SDL_AUDIO_ISINT(Format);

        /// <summary>
        /// Whether the format is floats.
        /// </summary>
        public readonly bool IsFloat => Native.SDL_AUDIO_ISFLOAT(Format);

        /// <summary>
        /// Whether the format is little-endian.
        /// </summary>
        public readonly bool IsLittleEndian => Native.SDL_AUDIO_ISLITTLEENDIAN(Format);

        /// <summary>
        /// Whether the format is big-endian.
        /// </summary>
        public readonly bool IsBigEndian => Native.SDL_AUDIO_ISBIGENDIAN(Format);

        /// <summary>
        /// Whether the format is signed.
        /// </summary>
        public readonly bool IsUnsigned => Native.SDL_AUDIO_ISUNSIGNED(Format);

        /// <summary>
        /// Whether the format is unsigned.
        /// </summary>
        public readonly bool IsSigned => Native.SDL_AUDIO_ISSIGNED(Format);
    }
}
