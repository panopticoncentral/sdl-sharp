using System;

namespace SdlSharp.Sound
{
    /// <summary>
    /// A format for audio data.
    /// </summary>
    public readonly struct AudioFormat
    {
        private const ushort BitsizeMask = 0xFF;
        private const ushort DataTypeMask = 1 << 8;
        private const ushort EndianMask = 1 << 12;
        private const ushort SignedMask = 1 << 15;

        /// <summary>
        /// Unsigned 8-bit samples.
        /// </summary>
        public static AudioFormat Unsigned8Bit { get; } = new AudioFormat(0x0008);

        /// <summary>
        /// Signed 8-bit samples
        /// </summary>
        public static AudioFormat Signed8Bit { get; } = new AudioFormat(0x8008);

        /// <summary>
        /// Unsigned 16-bit little-endian samples.
        /// </summary>
        public static AudioFormat Unsigned16BitLittleEndian { get; } = new AudioFormat(0x0010);

        /// <summary>
        /// Signed 16-bit little-endian samples.
        /// </summary>
        public static AudioFormat Signed16BitLittleEndian { get; } = new AudioFormat(0x8010);

        /// <summary>
        /// Unsigned 16-bit big-endian samples.
        /// </summary>
        public static AudioFormat Unsigned16BitBigEndian { get; } = new AudioFormat(0x1010);

        /// <summary>
        /// Signed 16-bit big-endian samples.
        /// </summary>
        public static AudioFormat Signed16BitBigEndian { get; } = new AudioFormat(0x9010);

        /// <summary>
        /// Default unsigned 16-bit sample format (little-endian).
        /// </summary>
        public static AudioFormat Unsigned16Bit { get; } = Unsigned16BitLittleEndian;

        /// <summary>
        /// Default signed 16-bit sample format (little-endian).
        /// </summary>
        public static AudioFormat Signed16Bit { get; } = Signed16BitLittleEndian;

        /// <summary>
        /// Signed 32-bit little-endian sample format.
        /// </summary>
        public static AudioFormat Signed32BitLittleEndian { get; } = new AudioFormat(0x8020);

        /// <summary>
        /// Signed 32-bit big-endian sample format.
        /// </summary>
        public static AudioFormat Signed32BitBigEndian { get; } = new AudioFormat(0x9020);

        /// <summary>
        /// Default signed 32-bit sample format (little-endian).
        /// </summary>
        public static AudioFormat Signed32Bit { get; } = Signed32BitLittleEndian;

        /// <summary>
        /// Float 32-bit little-endian sample format.
        /// </summary>
        public static AudioFormat Float32BitLittleEndian { get; } = new AudioFormat(0x8120);

        /// <summary>
        /// FLoat 32-bit big-endian sample format.
        /// </summary>
        public static AudioFormat Float32BitBigEndian { get; } = new AudioFormat(0x9120);

        /// <summary>
        /// Default float 32-bit sample format (little-endian).
        /// </summary>
        public static AudioFormat Float32Bit { get; } = Float32BitLittleEndian;

        /// <summary>
        /// Unsigned 16-bit system endian sample format.
        /// </summary>
        public static AudioFormat Unsigned16BitSystem { get; } = BitConverter.IsLittleEndian ? Unsigned16BitLittleEndian : Unsigned16BitBigEndian;

        /// <summary>
        /// Signed 16-bit system endian sample format.
        /// </summary>
        public static AudioFormat Signed16BitSystem { get; } = BitConverter.IsLittleEndian ? Signed16BitLittleEndian : Signed16BitBigEndian;

        /// <summary>
        /// Unsigned 32-bit system endian sample format.
        /// </summary>
        public static AudioFormat Signed32BitSystem { get; } = BitConverter.IsLittleEndian ? Signed32BitLittleEndian : Signed32BitBigEndian;

        /// <summary>
        /// Float 32-bit system endian sample format.
        /// </summary>
        public static AudioFormat Float32BitSystem { get; } = BitConverter.IsLittleEndian ? Float32BitLittleEndian : Float32BitBigEndian;

        /// <summary>
        /// The bit size of the format.
        /// </summary>
        public readonly ushort Bitsize => (ushort)(_format & BitsizeMask);

        /// <summary>
        /// Whether the format is integers.
        /// </summary>
        public readonly bool IsInt => !IsFloat;

        /// <summary>
        /// Whether the format is floats.
        /// </summary>
        public readonly bool IsFloat => (_format & DataTypeMask) != 0;

        /// <summary>
        /// Whether the format is little-endian.
        /// </summary>
        public readonly bool IsLittleEndian => !IsBigEndian;

        /// <summary>
        /// Whether the format is big-endian.
        /// </summary>
        public readonly bool IsBigEndian => (_format & EndianMask) != 0;

        /// <summary>
        /// Whether the format is signed.
        /// </summary>
        public readonly bool IsUnsigned => !IsSigned;

        /// <summary>
        /// Whether the format is unsigned.
        /// </summary>
        public readonly bool IsSigned => (_format & SignedMask) != 0;

        private readonly ushort _format;

        private AudioFormat(ushort format)
        {
            _format = format;
        }
    }
}
